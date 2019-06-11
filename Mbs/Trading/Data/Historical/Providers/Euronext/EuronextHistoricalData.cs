using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Historical
{
    /// <summary>
    /// Downloads and parses historical endofday data from the Euronext website.
    /// </summary>
    public static class EuronextHistoricalData
    {
        private const int DefaultEndofdayClosingHour = 19;
        private const int DefaultEndofdayClosingMinute = 0;
        private const int DefaultEndofdayClosingSecond = 0;
        internal const string Prefix = "Euronext historical data:";
        private const string Skipping = "skipping.";

        private static readonly DateTime Year1970 = new DateTime(1970, 1, 1);
        private static readonly DateTime Year1999 = new DateTime(1999, 1, 1);
        private static readonly HttpClient HttpClient = CreateHttpClient();
        private static readonly object CacheDictionaryLock = new object();
        private static readonly Dictionary<string, List<Ohlcv>> CacheDictionary = new Dictionary<string, List<Ohlcv>>();
        private static readonly TimeSpan DefaultEndofdayClosingTime =
            TimeSpan.FromSeconds(DefaultEndofdayClosingSecond + DefaultEndofdayClosingMinute * 60 + DefaultEndofdayClosingHour * 3600);

        private static long isDataCached = 1L;
        private static long retries = 3L;
        private static long perRequestTimeoutSeconds = 180L;

        /// <summary>
        /// Identifies a data provider.
        /// </summary>
        internal const string Provider = "Euronext";

        /// <summary>
        /// Clears the data cache.
        /// </summary>
        public static void ClearDataCache()
        {
            lock (CacheDictionaryLock)
                CacheDictionary.Clear();
        }

        /// <summary>
        /// Gets or sets a value indicating whether downloaded data will be cached.
        /// </summary>
        public static bool IsDataCached
        {
            get => 0L != Interlocked.Read(ref isDataCached);

            set => Interlocked.Exchange(ref isDataCached, value ? 1L : 0L);
        }

        /// <summary>
        /// Gets or sets per request download timeout in seconds.
        /// </summary>
        public static int TimeoutSeconds
        {
            get => (int)Interlocked.Read(ref perRequestTimeoutSeconds);

            set => Interlocked.Exchange(ref perRequestTimeoutSeconds, value);
        }

        /// <summary>
        /// Gets or sets the number of download retries.
        /// </summary>
        public static int Retries
        {
            get => (int)Interlocked.Read(ref retries);

            set => Interlocked.Exchange(ref retries, value);
        }

        /// <summary>
        /// An enumerable interface to enumerate a series of historical data events in the temporal order.
        /// </summary>
        /// <param name="historicalDataRequest">A historical data series specification.</param>
        /// <param name="httpClient">An optional http client./></param>
        /// <returns>An enumerable of parsed daily <see cref="Ohlcv"/> elements.</returns>
        public static async Task<List<Ohlcv>> FetchAsync(HistoricalDataRequest historicalDataRequest, HttpClient httpClient = null)
        {
            string mic = historicalDataRequest.Instrument.Exchange.Mic.ToString().ToUpperInvariant();
            string isin = historicalDataRequest.Instrument.GetSecurityIdAs(InstrumentSecurityIdSource.Isin);
            InstrumentType instrumentType = historicalDataRequest.Instrument.Type;
            TimeGranularity timeGranularity = historicalDataRequest.TimeGranularity;
            historicalDataRequest.IsDataAdjusted = null;

            Validate(mic, isin, instrumentType, timeGranularity);
            string url = CreateUrl(
                mic,
                isin,
                instrumentType,
                historicalDataRequest.AdjustedDataIfPresent,
                historicalDataRequest.StartDate,
                historicalDataRequest.EndDate,
                out string referer);

            List<Ohlcv> list;
            bool isCached = IsDataCached;
            if (isCached)
            {
                lock (CacheDictionaryLock)
                    CacheDictionary.TryGetValue(url, out list);
                if (list != null)
                {
                    historicalDataRequest.IsDataAdjusted = historicalDataRequest.AdjustedDataIfPresent;
                    return list;
                }
            }

            string json = await DownloadUrlAsync(url, referer, httpClient);
            if (json == null)
                return new List<Ohlcv>();
            list = Parse(json, isin, historicalDataRequest.EndofdayClosingTime);
            Log.DailyOhlcvBarsDownloaded(nameof(EuronextHistoricalData), list.Count, url);
            if (isCached && list.Count > 0)
            {
                lock (CacheDictionaryLock)
                    if (!CacheDictionary.ContainsKey(url))
                        CacheDictionary.Add(url, list);
            }

            historicalDataRequest.IsDataAdjusted = historicalDataRequest.AdjustedDataIfPresent;
            return list;
        }

        /// <summary>
        /// Parses <see cref="Ohlcv"/> elements from the given JSON file.
        /// </summary>
        /// <param name="json">A JSON downloaded by <see cref="DownloadUrlAsync"/>.</param>
        /// <param name="isin">The International Securities Identification Number of the security.</param>
        /// <param name="endofdayClosingTime">An end of trading session time to assign to all daily <see cref="Ohlcv"/> instances.</param>
        /// <returns>An enumerable of parsed daily <see cref="Ohlcv"/> elements.</returns>
        public static List<Ohlcv> Parse(string json, string isin, TimeSpan? endofdayClosingTime = null)
        {
            var list = new List<Ohlcv>(1024);
            var arrayDelimiterSpan = "},{".AsSpan();
            var arrayTrailerSpan = "}]".AsSpan();

            int i = json.IndexOf("[{", StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error($"{Prefix} no JSON data found in <{json}>, skipping.");
                return list;
            }

            var span = json.AsSpan().Slice(i + 2);
            Ohlcv ohlcv;
            while ((i = span.IndexOf(arrayDelimiterSpan, StringComparison.Ordinal)) >= 0)
            {
                if ((ohlcv = ParseJsonSpan(span.Slice(0, i), endofdayClosingTime, isin)) != null)
                    list.Add(ohlcv);
                span = span.Slice(i + 3);
            }

            i = span.IndexOf(arrayTrailerSpan, StringComparison.Ordinal);
            if ((ohlcv = ParseJsonSpan(span.Slice(0, i), endofdayClosingTime, isin)) != null)
                list.Add(ohlcv);
            return list;
        }

        private static HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            var retryingHandler = new RetryingHandler(handler)
            {
                LogPrefix = () => Prefix,
                Retries = () => Retries,
                TimeoutSeconds = () => TimeoutSeconds
            };

            // var httpClient = HttpClientFactory.Create(retryingHandler);
            // httpClient.MaxResponseContentBufferSize = 100 * 1024 * 1024;
            var httpClient = new HttpClient(retryingHandler)
            {
                MaxResponseContentBufferSize = 100 * 1024 * 1024
            };

            // ReSharper disable StringLiteralTypo
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident / 6.0)");

            // ReSharper restore StringLiteralTypo
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/javascript"));
            httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US", 0.9));
            return httpClient;
        }

        private static async Task<string> DownloadUrlAsync(string url, string referer, HttpClient httpClient = null)
        {
#pragma warning disable CA1031 // Do not catch general exception types
            if (httpClient == null)
                httpClient = HttpClient;

            Log.Downloading(nameof(EuronextHistoricalData), url);
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    using (var cancelSource = new CancellationTokenSource())
                    {
                        request.Headers.Referrer = new Uri(referer);
                        using (HttpResponseMessage response =
                            await httpClient.SendAsync(request, cancelSource.Token))
                        using (Stream responseStream = await GetResponseStreamAsync(response))
                        {
                            return await ReadResponseStreamAsync(responseStream);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Log.DownloadFailed(nameof(EuronextHistoricalData), url, exception);
            }

            return null;
#pragma warning restore CA1031 // Do not catch general exception types
        }

        private static void Validate(string mic, string isin, InstrumentType instrumentType, TimeGranularity timeGranularity = TimeGranularity.Day1)
        {
            mic = mic.ToUpperInvariant();
            if (mic != "XAMS" && mic != "XPAR" && mic != "XBRU" && mic != "XLIS" && mic != "TNLA" && mic != "TNLB" && mic != "XHFT" && mic != "XLON" &&
                mic != "ALXA" && mic != "ALXB" && mic != "ALXL" && mic != "ALXP" && mic != "MLXB" && mic != "ENXB" && mic != "ENXL")
            {
                var exception = new ArgumentException($"MIC {mic} is not supported", nameof(mic));
                Log.ExceptionHasBeenThrown(nameof(EuronextHistoricalData), exception);
                throw exception;
            }

            if (string.IsNullOrWhiteSpace(isin))
            {
                var exception = new ArgumentException("Instruments without ISIN are not supported", nameof(isin));
                Log.ExceptionHasBeenThrown(nameof(EuronextHistoricalData), exception);
                throw exception;
            }

            if (instrumentType != InstrumentType.Index &&
                instrumentType != InstrumentType.Stock &&
                instrumentType != InstrumentType.Etf &&
                instrumentType != InstrumentType.Etv &&
                instrumentType != InstrumentType.Inav &&
                instrumentType != InstrumentType.Fund)
            {
                var exception = new ArgumentException($"Instrument type {instrumentType} is not supported", nameof(instrumentType));
                Log.ExceptionHasBeenThrown(nameof(EuronextHistoricalData), exception);
                throw exception;
            }

            if (timeGranularity.IsIntraday())
            {
                var exception = new ArgumentException($"Intraday time granularity {timeGranularity} is not supported", nameof(timeGranularity));
                Log.ExceptionHasBeenThrown(nameof(EuronextHistoricalData), exception);
                throw exception;
            }
        }

        private static string CreateUrl(string mic, string isin, InstrumentType instrumentType, bool isAdjusted, DateTime beginDate, DateTime endDate, out string referer)
        {
            mic = mic.ToUpperInvariant();
            if (beginDate == DateTime.MinValue)
                beginDate = Year1999;
            if (endDate == DateTime.MaxValue)
                endDate = DateTime.Today;

            // The number of milliseconds since 1 january 1970.
            var millisecondsFrom = (long)(beginDate - Year1970).TotalMilliseconds;
            var millisecondsTo = (long)(endDate - Year1970).TotalMilliseconds;

            string adjusted = isAdjusted ? "1" : "0"; // "1" = adjusted data, "0" = not adjusted data.
            string url;

            if (instrumentType == InstrumentType.Index)
            {
                // https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=historical_data&adjusted=1&from=1346198400000&to=1346803200000&isin=NL0000000107&mic=XAMS&dateFormat=d/m/Y&locale=null
                const string indexUrlFormat = "https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=historical_data&adjusted={0}&from={1}&to={2}&isin={3}&mic={4}&dateFormat=d/m/Y&locale=null";

                // https://indices.nyx.com/nl/products/indices/NL0000000107-XAMS/quotes
                const string indexRefererFormat = "https://indices.nyx.com/nl/products/indices/{0}-{1}/quotes";

                url = string.Format(CultureInfo.InvariantCulture, indexUrlFormat, adjusted, millisecondsFrom, millisecondsTo, isin, mic);
                referer = string.Format(CultureInfo.InvariantCulture, indexRefererFormat, isin, mic);
            }
            else if (instrumentType == InstrumentType.Stock)
            {
                // https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=historical_data&adjusted=1&from=1346198400000&to=1346803200000&isin=FR0010533075&mic=XPAR&dateFormat=d/m/Y&locale=null
                const string stockUrlFormat = "https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=historical_data&adjusted={0}&from={1}&to={2}&isin={3}&mic={4}&dateFormat=d/m/Y&locale=null";

                // https://europeanequities.nyx.com/en/products/equities/FR0010533075-XPAR/quotes
                const string stockRefererFormat = "https://europeanequities.nyx.com/en/products/equities/{0}-{1}/quotes";

                url = string.Format(CultureInfo.InvariantCulture, stockUrlFormat, adjusted, millisecondsFrom, millisecondsTo, isin, mic);
                referer = string.Format(CultureInfo.InvariantCulture, stockRefererFormat, isin, mic);
            }
            else if (instrumentType == InstrumentType.Etf
                 || instrumentType == InstrumentType.Etv
                 || instrumentType == InstrumentType.Inav
                 || instrumentType == InstrumentType.Fund)
            {
                // https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=historical_data&adjusted=1&from=1346112000000&to=1346716800000&isin=PTGFIBIM0004&mic=XLIS&dateFormat=d/m/Y&locale=null
                const string etpUrlFormat = "https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=historical_data&adjusted={0}&from={1}&to={2}&isin={3}&mic={4}&dateFormat=d/m/Y&locale=null";

                // https://etp.nyx.com/en/products/funds/PTGFIBIM0004-XLIS/quotes
                const string etpRefererFormat = "https://etp.nyx.com/en/products/funds/{0}-{1}/quotes";

                url = string.Format(CultureInfo.InvariantCulture, etpUrlFormat, adjusted, millisecondsFrom, millisecondsTo, isin, mic);
                referer = string.Format(CultureInfo.InvariantCulture, etpRefererFormat, isin, mic);
            }
            else
            {
                var exception = new ArgumentException($"Instrument type {instrumentType} is not supported", nameof(instrumentType));
                Log.ExceptionHasBeenThrown(nameof(EuronextHistoricalData), exception);
                throw exception;
            }

            return url;
        }

        private static async Task<Stream> GetResponseStreamAsync(HttpResponseMessage response)
        {
            Stream stream;
            if (response.Content == null || (stream = await response.Content.ReadAsStreamAsync()) == null)
            {
                var exception = new Exception("got <null> HTTP response stream.");
                Log.ExceptionHasBeenThrown(nameof(EuronextHistoricalData), exception);
                throw exception;
            }

            return stream;
        }

        private static async Task<string> ReadResponseStreamAsync(Stream stream)
        {
            string json;
            using (var streamReader = new StreamReader(stream))
                json = await streamReader.ReadToEndAsync();

            if (json == null || json == "[]" || json == "null")
            {
                Log.NoDataDownloadedSkipping(nameof(EuronextHistoricalData));
                return null;
            }

            return json;
        }

        private class EntryInfo
        {
            public static readonly char[] DelimiterArray = @",""".ToCharArray();

            public static readonly char[] NullArray = "null".ToCharArray();

            public static readonly char[] IsinPatternArray = @"""ISIN"":""".ToCharArray();

            public static readonly char[] DatePatternArray = @"date"":""".ToCharArray();

            // ReSharper disable once StringLiteralTypo
            public static readonly char[] VolumePatternArray1 = @"nymberofshares"":""".ToCharArray();

            // ReSharper disable once StringLiteralTypo
            public static readonly char[] VolumePatternArray2 = @"numberofshares"":""".ToCharArray();

            public string Name { get; }

            public int Offset { get; }

            public int LengthSubtractor { get; }

            public char[] PatternArray { get; }

            public EntryInfo(string name, int offset, int lengthSubtractor, string array)
            {
                Name = name;
                Offset = offset;
                LengthSubtractor = lengthSubtractor;
                PatternArray = array.ToCharArray();
            }
        }

        private static readonly EntryInfo[] Entries =
        {
            new EntryInfo("open", 7, 8, @"open"":"""),
            new EntryInfo("high", 7, 8, @"high"":"""),
            new EntryInfo("low", 6, 7, @"low"":"""),
            new EntryInfo("close", 8, 9, @"close"":""")
        };

        private static bool TryParseDoubleSpan(ReadOnlySpan<char> span, ReadOnlySpan<char> entry, ReadOnlySpan<char> number, string invalidItem, out double value, string name)
        {
            // We could have used new Utf8Parser
            // if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(number.ToArray()), out value, out _))
            // but it does not parse thousands delimiter correctly.
            if (!double.TryParse(number.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out value))
            {
                Log.Error(name == null
                    ? $"{Prefix} {invalidItem} [{number.ToString()}] in [{entry.ToString()}] in [{span.ToString()}], {Skipping}"
                    : $"{Prefix} invalid [{name}] price [{number.ToString()}] in [{entry.ToString()}] in [{span.ToString()}], {Skipping}");
                value = double.NaN;
                return false;
            }

            return true;
        }

        private static bool TryParsePriceSpan(ReadOnlySpan<char> span, EntryInfo entryInfo, out double value, out int i)
        {
            i = span.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error($"{Prefix} invalid [{entryInfo.Name}] splitted item in [{span.ToString()}], {Skipping}");
                value = double.NaN;
                return false;
            }

            var entry = span.Slice(0, i);
            if (!entry.StartsWith(entryInfo.PatternArray, StringComparison.Ordinal) ||
                '\"' != entry[entry.Length - 1] || entry.Contains(EntryInfo.NullArray, StringComparison.Ordinal))
            {
                Log.Error($"{Prefix} invalid [{entryInfo.Name}] splitted item [{entry.ToString()}] in [{span.ToString()}], {Skipping}");
                value = double.NaN;
                return false;
            }

            var number = entry.Slice(entryInfo.Offset, entry.Length - entryInfo.LengthSubtractor);
            return TryParseDoubleSpan(span, entry, number, null, out value, entryInfo.Name);
        }

        private static bool TryParseVolumeSpan(ReadOnlySpan<char> span, out double value, out int i)
        {
            const string invalidItem = "invalid [number of shares] splitted item";

            i = span.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error($"{Prefix} {invalidItem} [{span.ToString()}], {Skipping}");
                value = double.NaN;
                return false;
            }

            // ReSharper disable CommentTypo
            // nymberofshares":"1,118.00"
            // numberofshares":"0,00"
            //           1111111111222222
            // 01234567890123456789012345
            // ReSharper restore CommentTypo
            var entry = span.Slice(0, i);
            if (!(entry.StartsWith(EntryInfo.VolumePatternArray1, StringComparison.Ordinal) || entry.StartsWith(EntryInfo.VolumePatternArray2, StringComparison.Ordinal))
                || '\"' != entry[entry.Length - 1]
                || entry.Contains(EntryInfo.NullArray, StringComparison.Ordinal))
            {
                Log.Error($"{Prefix} {invalidItem} [{entry.ToString()}] in [{span.ToString()}], {Skipping}");
                value = double.NaN;
                return false;
            }

            var number = entry.Slice(17, entry.Length - 18); // 1,118.00 // 0,00
            return TryParseDoubleSpan(span, entry, number, invalidItem, out value, null);
        }

        private static bool TryParseDateSpan(ReadOnlySpan<char> span, TimeSpan? endofdayClosingTime, out DateTime value, out int i)
        {
            const string invalidItem = "invalid [date] splitted item";

            i = span.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error($"{Prefix} {invalidItem} in [{span.ToString()}], {Skipping}");
                value = DateTime.MinValue;
                return false;
            }

            // date":"29\/08\/2012"
            //           1111111111
            // 01234567890123456789
            if (!span.StartsWith(EntryInfo.DatePatternArray, StringComparison.Ordinal)
                || 22 > span.Length
                || '\\' != span[9]
                || '/' != span[10]
                || '\\' != span[13]
                || '/' != span[14]
                || '"' != span[19])
            {
                Log.Error($"{Prefix} {invalidItem} in [{span.ToString()}], {Skipping}");
                value = DateTime.MinValue;
                return false;
            }

            int day = 10 * (span[7] - '0') + (span[8] - '0');
            int month = 10 * (span[11] - '0') + (span[12] - '0');
            int year = 1000 * (span[15] - '0') + 100 * (span[16] - '0') + 10 * (span[17] - '0') + (span[18] - '0');
            value = new DateTime(year, month, day, 0, 0, 0).Add(endofdayClosingTime ?? DefaultEndofdayClosingTime);
            return true;
        }

        private static bool TryParseIsinSpan(ReadOnlySpan<char> span, string isin, out int i)
        {
            const string invalidItem = "invalid [ISIN] splitted item";

            i = span.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error($"{Prefix} {invalidItem} in [{span.ToString()}], {Skipping}");
                return false;
            }

            // "ISIN":"FR0010930636"
            //           11111111112
            // 012345678901234567890
            var entry = span.Slice(0, i);
            if (!entry.StartsWith(EntryInfo.IsinPatternArray, StringComparison.Ordinal) || '\"' != entry[entry.Length - 1])
            {
                Log.Error($"{Prefix} {invalidItem} [{entry.ToString()}] in [{span.ToString()}], {Skipping}");
                return false;
            }

            var value = entry.Slice(8, entry.Length - 9); // FR0010930636
            if (!value.Equals(isin.AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                Log.Error($"{Prefix} ISIN in instrument context [{isin}] differs from [ISIN] item [{value.ToString()}] in [{entry.ToString()}] in [{span.ToString()}], {Skipping}");
                return false;
            }

            return true;
        }

        private static Ohlcv ParseJsonSpan(ReadOnlySpan<char> span, TimeSpan? endofdayClosingTime, string isin)
        {
            // ReSharper disable CommentTypo
            // "ISIN":"FR0010533075","MIC":"Euronext Paris, London","date":"29\/08\/2012","open":"5.85","high":"5.918","low":"5.84","close":"5.893","nymberofshares":"589,993","numoftrades":"1,467","turnover":"3,465,442.27","currency":"EUR"
            // "ISIN":"FR0010930636"
            //           11111111112
            // 012345678901234567890
            // ReSharper restore CommentTypo
            if (!TryParseIsinSpan(span, isin, out int i))
                return null;

            // MIC":"Euronext Paris, London"
            //           1111111111222222222
            // 01234567890123456789012345678
            span = span.Slice(i + EntryInfo.DelimiterArray.Length);
            i = span.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error($"{Prefix} invalid [MIC] splitted item in [{span.ToString()}], {Skipping}");
                return null;
            }

            // date":"29\/08\/2012"
            //           1111111111
            // 01234567890123456789
            span = span.Slice(i + EntryInfo.DelimiterArray.Length);
            if (!TryParseDateSpan(span, endofdayClosingTime, out DateTime dateTime, out i))
                return null;

            // open":"1,329.39"
            //           111111
            // 0123456789012345
            span = span.Slice(i + EntryInfo.DelimiterArray.Length);
            if (!TryParsePriceSpan(span, Entries[0], out double open, out i)) // 1,329.39
                return null;

            // high":"1,329.39"
            //           11111
            // 012345678901234
            span = span.Slice(i + EntryInfo.DelimiterArray.Length);
            if (!TryParsePriceSpan(span, Entries[1], out double high, out i))
                return null;

            // low":"1,329.39"
            //           11111
            // 012345678902345
            span = span.Slice(i + EntryInfo.DelimiterArray.Length);
            if (!TryParsePriceSpan(span, Entries[2], out double low, out i))
                return null;

            // close":"1,329.39"
            //           1111111
            // 01234567890123456
            span = span.Slice(i + EntryInfo.DelimiterArray.Length);
            if (!TryParsePriceSpan(span, Entries[3], out double close, out i))
                return null;

            // ReSharper disable CommentTypo
            // nymberofshares":"1,118.00"
            // numberofshares":"0,00"
            //           111111111122
            // 0123456789012345678901
            // ReSharper restore CommentTypo
            span = span.Slice(i + EntryInfo.DelimiterArray.Length);
            if (!TryParseVolumeSpan(span, out double volume, out i))
                return null;

            return new Ohlcv(dateTime, open, high, low, close, volume);
        }
    }
}
