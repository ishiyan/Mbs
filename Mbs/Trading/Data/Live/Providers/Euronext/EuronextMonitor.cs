using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Mbs.Trading.Holidays;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;
using Mbs.Utilities;

// ReSharper disable once CheckNamespace
namespace Mbs.Trading.Data.Live
{
    /// <summary>
    /// Monitors data on Euronext website.
    /// </summary>
    public static class EuronextMonitor
    {
        internal const int DefaultMinimumQuotePollingPeriodMilliseconds = LowerLimitQuotePollingPeriodMilliseconds;
        internal const int DefaultMinimumTradePollingPeriodMilliseconds = LowerLimitTradePollingPeriodMilliseconds;
        internal const int DefaultMaximumTradePollingPeriodMilliseconds = 3600000; // One hour.

        internal const int DefaultTradePollingDownloadTimeoutMilliseconds = 240000;
        internal const int DefaultQuotePollingDownloadTimeoutMilliseconds = 180000;

        private const int LowerLimitTradePollingPeriodMilliseconds = 5000;
        private const int LowerLimitQuotePollingPeriodMilliseconds = 5000;
        private const int TradeEventDispatcherThreadWaitMilliseconds = 1000;
        private const int QuoteEventDispatcherThreadWaitMilliseconds = 1000;

        private static readonly object InstrumentTradeMonitorDictionaryLock = new object();
        private static readonly Dictionary<Instrument, InstrumentTradeMonitor> InstrumentTradeMonitorDictionary =
            new Dictionary<Instrument, InstrumentTradeMonitor>();

        private static readonly object InstrumentQuoteMonitorDictionaryLock = new object();
        private static readonly Dictionary<Instrument, InstrumentQuoteMonitor> InstrumentQuoteMonitorDictionary =
            new Dictionary<Instrument, InstrumentQuoteMonitor>();

        private static long tradePollingDownloadTimeoutMilliseconds = DefaultTradePollingDownloadTimeoutMilliseconds;
        private static long quotePollingDownloadTimeoutMilliseconds = DefaultQuotePollingDownloadTimeoutMilliseconds;
        private static long minimalTradePollingPeriodMilliseconds = DefaultMinimumTradePollingPeriodMilliseconds;
        private static long maximalTradePollingPeriodMilliseconds = DefaultMaximumTradePollingPeriodMilliseconds;

        /// <summary>
        /// Gets or sets the user agent name.
        /// </summary>
        public static string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";

        /// <summary>
        /// Gets or sets a value indicating whether to include historical data on start of a subscription.
        /// </summary>
        public static bool IsSubscriptionWithHistory { get; set; } = true;

        /// <summary>
        /// Gets or sets the trade monitor polling download timeout in milliseconds.
        /// </summary>
        public static int TradePollingDownloadTimeoutMilliseconds
        {
            get => (int)Interlocked.Read(ref tradePollingDownloadTimeoutMilliseconds);

            set
            {
                long val = value > LowerLimitTradePollingPeriodMilliseconds ? value : LowerLimitTradePollingPeriodMilliseconds;
                Interlocked.Exchange(ref tradePollingDownloadTimeoutMilliseconds, val);
            }
        }

        /// <summary>
        /// Gets or sets the quote monitor polling download timeout in milliseconds.
        /// </summary>
        public static int QuotePollingDownloadTimeoutMilliseconds
        {
            get => (int)Interlocked.Read(ref quotePollingDownloadTimeoutMilliseconds);

            set
            {
                long val = value > LowerLimitQuotePollingPeriodMilliseconds ? value : LowerLimitQuotePollingPeriodMilliseconds;
                Interlocked.Exchange(ref quotePollingDownloadTimeoutMilliseconds, val);
            }
        }

        /// <summary>
        /// Gets or sets the trade monitor minimal polling period for all instruments.
        /// </summary>
        public static long MinimalTradePollingPeriodMilliseconds
        {
            get => Interlocked.Read(ref minimalTradePollingPeriodMilliseconds);

            set
            {
                // Must not exceed the maximum.
                if (value > MaximalTradePollingPeriodMilliseconds)
                {
                    value = MaximalTradePollingPeriodMilliseconds;
                }

                // Must be greater than the absolute minimum.
                if (value < LowerLimitTradePollingPeriodMilliseconds)
                {
                    value = LowerLimitTradePollingPeriodMilliseconds;
                }

                Interlocked.Exchange(ref minimalTradePollingPeriodMilliseconds, value);
                lock (InstrumentTradeMonitorDictionaryLock)
                {
                    InstrumentTradeMonitorDictionary.Values.ToList().ForEach(m => m.ApplyMinimalPeriodMilliseconds(value));
                }
            }
        }

        /// <summary>
        /// Gets or sets the trade monitor maximal polling period for all instruments.
        /// </summary>
        public static long MaximalTradePollingPeriodMilliseconds
        {
            get => Interlocked.Read(ref maximalTradePollingPeriodMilliseconds);

            set
            {
                // Must be greater than the minimum.
                if (MinimalTradePollingPeriodMilliseconds > value)
                {
                    value = MinimalTradePollingPeriodMilliseconds;
                }

                // Must be greater than the absolute minimum.
                if (value < LowerLimitTradePollingPeriodMilliseconds)
                {
                    value = LowerLimitTradePollingPeriodMilliseconds;
                }

                Interlocked.Exchange(ref maximalTradePollingPeriodMilliseconds, value);
                lock (InstrumentTradeMonitorDictionaryLock)
                {
                    InstrumentTradeMonitorDictionary.Values.ToList().ForEach(m => m.ApplyMaximalPeriodMilliseconds(value));
                }
            }
        }

        /// <summary>
        /// Get the trade polling period of the instrument in milliseconds.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <returns>The trade polling period in milliseconds.</returns>
        public static long GetTradePollingPeriodMilliseconds(Instrument instrument)
        {
            return GetInstrumentTradeMonitor(instrument).PeriodMilliseconds;
        }

        /// <summary>
        /// Set the trade polling period of the instrument in milliseconds.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <param name="period">The trade polling period in milliseconds. Set it to zero to use the <see cref="MinimalTradePollingPeriodMilliseconds"/> value.</param>
        public static void SetTradePollingPeriodMilliseconds(Instrument instrument, long period)
        {
            if (period <= 0)
            {
                period = MinimalTradePollingPeriodMilliseconds;
            }
            else if (period < LowerLimitTradePollingPeriodMilliseconds)
            {
                period = LowerLimitTradePollingPeriodMilliseconds;
            }

            GetInstrumentTradeMonitor(instrument).PeriodMilliseconds = period;
        }

        /// <summary>
        /// Get the quote polling period of the instrument in milliseconds.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <returns>The quote polling period in milliseconds.</returns>
        public static long GetQuotePollingPeriodMilliseconds(Instrument instrument)
        {
            return GetInstrumentQuoteMonitor(instrument).PeriodMilliseconds;
        }

        /// <summary>
        /// Set the quote polling period of the instrument in milliseconds.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <param name="period">The quote polling period in milliseconds.</param>
        public static void SetQuotePollingPeriodMilliseconds(Instrument instrument, long period)
        {
            if (period < LowerLimitQuotePollingPeriodMilliseconds)
            {
                period = LowerLimitQuotePollingPeriodMilliseconds;
            }

            GetInstrumentQuoteMonitor(instrument).PeriodMilliseconds = period;
        }

        /// <summary>
        /// If the instrument accumulates volume of the consecutive trades with the same time and price.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <returns>The volume accumulation status.</returns>
        public static bool GetTradeVolumeAccumulation(Instrument instrument)
        {
            return GetInstrumentTradeMonitor(instrument).Accumulate;
        }

        /// <summary>
        /// If the instrument accumulates volume of the consecutive trades with the same time and price.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <param name="status">The volume accumulation status.</param>
        public static void SetTradeVolumeAccumulation(Instrument instrument, bool status)
        {
            GetInstrumentTradeMonitor(instrument).Accumulate = status;
        }

        /// <summary>Instrument.Instrument
        /// Returns an trade monitor for a given instrument.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <returns>The trade monitor.</returns>
        internal static InstrumentTradeMonitor GetInstrumentTradeMonitor(Instrument instrument)
        {
            InstrumentTradeMonitor instrumentTradeMonitor;
            lock (InstrumentTradeMonitorDictionaryLock)
            {
                if (!InstrumentTradeMonitorDictionary.TryGetValue(instrument, out instrumentTradeMonitor))
                {
                    instrumentTradeMonitor = new InstrumentTradeMonitor(instrument);
                    InstrumentTradeMonitorDictionary.Add(instrument, instrumentTradeMonitor);
                }
            }

            return instrumentTradeMonitor;
        }

        /// <summary>
        /// Returns a quote monitor for a given instrument.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <returns>The quote monitor.</returns>
        internal static InstrumentQuoteMonitor GetInstrumentQuoteMonitor(Instrument instrument)
        {
            InstrumentQuoteMonitor instrumentQuoteMonitor;
            lock (InstrumentQuoteMonitorDictionaryLock)
            {
                if (!InstrumentQuoteMonitorDictionary.TryGetValue(instrument, out instrumentQuoteMonitor))
                {
                    instrumentQuoteMonitor = new InstrumentQuoteMonitor(instrument);
                    InstrumentQuoteMonitorDictionary.Add(instrument, instrumentQuoteMonitor);
                }
            }

            return instrumentQuoteMonitor;
        }

        /// <summary>
        /// Monitors the intraday trade csv data for a single instrument.
        /// </summary>
        internal sealed class InstrumentTradeMonitor : IDisposable
        {
            private static readonly string[] Splitter = { @",""" };
            private static readonly DateTime Year1970 = new DateTime(1970, 1, 1);

            private readonly Instrument instrument;
            private readonly string url;
            private readonly string referer;
            private readonly object timerLock = new object();
            private readonly object tradeListLock = new object();
            private readonly List<Trade> tradeList = new List<Trade>(1024);
            private readonly object subscribedPeriodListLock = new object();
            private readonly List<long> subscribedPeriodList = new List<long>();
            private readonly object subscriberEventLock = new object();
            private long lastItemTicks; // Should be zero initially.
            private long periodMilliseconds = DefaultMinimumTradePollingPeriodMilliseconds;
            private volatile Timer timer;
            private volatile bool timerFree;
            private volatile bool firstDownload = true;
            private AutoResetEventThread thread;
            private CompareAndSwapQueue<Stack<Trade>> queue;
            private TradeEnumerableEventHandler subscriberEvent;
            private int subscriberCount;

            /// <summary>
            /// Initializes a new instance of the <see cref="InstrumentTradeMonitor"/> class.
            /// </summary>
            /// <param name="instrument">The instrument.</param>
            internal InstrumentTradeMonitor(Instrument instrument)
            {
                string isin = instrument.GetSecurityIdAs(InstrumentSecurityIdSource.Isin);
                if (isin == null)
                {
                    throw new ArgumentException("instrument.Isin");
                }

                this.instrument = instrument;
                InstrumentType type = instrument.Type;
                string mic = instrument.Exchange.Mic.ToString().ToUpperInvariant();
#pragma warning disable S1075 // URIs should not be hardcoded
                if (type == InstrumentType.Index)
                {
                    // https://indices.nyx.com/sites/indices.nyx.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&from=1346198400000&isin=NL0000000107&mic=XAMS&dateFormat=d/m/Y&locale=null
                    // const string indexUriFormat = "https://indices.nyx.com/sites/indices.nyx.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&isin={0}&mic={1}&dateFormat=d/m/Y&locale=null&from="
                    // https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&from=1346198400000&isin=NL0000000107&mic=XAMS&dateFormat=d/m/Y&locale=null
                    const string indexUriFormat = "https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&isin={0}&mic={1}&dateFormat=d/m/Y&locale=null&from=";

                    // https://indices.nyx.com/nl/products/indices/NL0000000107-XAMS/quotes
                    const string indexRefererFormat = "https://indices.nyx.com/nl/products/indices/{0}-{1}/quotes";

                    url = string.Format(CultureInfo.InvariantCulture, indexUriFormat, isin, mic);
                    referer = string.Format(CultureInfo.InvariantCulture, indexRefererFormat, isin, mic);
                }
                else if (type == InstrumentType.Stock)
                {
                    // https://europeanequities.nyx.com/sites/europeanequities.nyx.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&from=1346198400000&isin=FR0010533075&mic=XPAR&dateFormat=d/m/Y&locale=null
                    // const string stockUriFormat = "https://europeanequities.nyx.com/sites/europeanequities.nyx.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&isin={0}&mic={1}&dateFormat=d/m/Y&locale=null&from="
                    // https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&from=1346198400000&isin=FR0010533075&mic=XPAR&dateFormat=d/m/Y&locale=null
                    const string stockUriFormat = "https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&isin={0}&mic={1}&dateFormat=d/m/Y&locale=null&from=";

                    // https://europeanequities.nyx.com/en/products/equities/FR0010533075-XPAR/quotes
                    const string stockRefererFormat = "https://europeanequities.nyx.com/en/products/equities/{0}-{1}/quotes";

                    url = string.Format(CultureInfo.InvariantCulture, stockUriFormat, isin, mic);
                    referer = string.Format(CultureInfo.InvariantCulture, stockRefererFormat, isin, mic);
                }
                else
                {
                    // Etf, Etv, Fund, Inav
                    // https://etp.nyx.com/sites/etp.nyx.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&from=1346198400000&isin=PTGFIBIM0004&mic=XLIS&dateFormat=d/m/Y&locale=null
                    // const string etpUriFormat = "https://etp.nyx.com/sites/etp.nyx.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&isin={0}&mic={1}&dateFormat=d/m/Y&locale=null&from="
                    // https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&from=1346198400000&isin=PTGFIBIM0004&mic=XLIS&dateFormat=d/m/Y&locale=null
                    const string etpUriFormat = "https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=intraday_data&isin={0}&mic={1}&dateFormat=d/m/Y&locale=null&from=";

                    // https://etp.nyx.com/en/products/funds/PTGFIBIM0004-XLIS/quotes
                    const string etpRefererFormat = "https://etp.nyx.com/en/products/funds/{0}-{1}/quotes";

                    url = string.Format(CultureInfo.InvariantCulture, etpUriFormat, isin, mic);
                    referer = string.Format(CultureInfo.InvariantCulture, etpRefererFormat, isin, mic);
                }
#pragma warning restore S1075 // URIs should not be hardcoded
            }

            /// <summary>
            /// The <see cref="Trade"/> enumerable event handler.
            /// </summary>
            /// <param name="enumerable">The enumerable.</param>
            internal delegate void TradeEnumerableEventHandler(IEnumerable<Trade> enumerable);

            /// <summary>
            /// The event handler for the subscription-related events.
            /// </summary>
            internal event TradeEnumerableEventHandler Event
            {
                add
                {
                    lock (subscriberEventLock)
                    {
                        ++subscriberCount;
                        subscriberEvent += value;
                        if (IsSubscriptionWithHistory)
                        {
                            lock (tradeListLock)
                            {
                                value(tradeList);
                            }
                        }

                        TimerActive = true;
                    }
                }

                remove
                {
                    bool stop = false;
                    lock (subscriberEventLock)
                    {
                        // ReSharper disable once DelegateSubtraction
                        subscriberEvent -= value;
                        if (--subscriberCount < 1)
                        {
                            subscriberCount = 0;
                            stop = true;
                        }

                        if (stop)
                        {
                            TimerActive = false;
                            lock (InstrumentTradeMonitorDictionaryLock)
                            {
                                if (InstrumentTradeMonitorDictionary.ContainsKey(instrument))
                                {
                                    InstrumentTradeMonitorDictionary.Remove(instrument);
                                }
                            }

                            lock (tradeListLock)
                            {
                                tradeList.Clear();
                            }
                        }
                    }
                }
            }

            /// <summary>
            /// Gets or sets the sequential download timer period in milliseconds.
            /// </summary>
            public long PeriodMilliseconds
            {
                get => Interlocked.Read(ref periodMilliseconds);

                set
                {
                    long delta = value - Interlocked.Exchange(ref periodMilliseconds, value);
                    if (delta < 0L)
                    {
                        TimerChange(0, value);
                    }
                }
            }

            /// <summary>
            /// Gets or sets a value indicating whether to accumulate volume for sequential trades with the same time and price.
            /// </summary>
            internal bool Accumulate { get; set; } = true;

            private bool TimerActive
            {
                set
                {
                    lock (timerLock)
                    {
                        if (!value)
                        {
                            if (timer != null)
                            {
                                Timer timerToDispose = timer;
                                timer = null;
                                timerToDispose.Change(Timeout.Infinite, Timeout.Infinite);
                                timerToDispose.Dispose();
                            }
                        }
                        else
                        {
                            if (timer == null)
                            {
                                queue = new CompareAndSwapQueue<Stack<Trade>>();
                                thread = EventDispatcherThread();
                                timer = PollingTimer();
                                timerFree = true;
                                timer.Change(0, PeriodMilliseconds);
                                thread.Thread.Start();
                            }
                        }
                    }
                }
            }

            private long MinimalSubscribedPeriod
            {
                get
                {
                    long period = -1L;
                    lock (subscribedPeriodListLock)
                    {
                        if (subscribedPeriodList.Count > 0)
                        {
                            period = subscribedPeriodList.Min() / 3;
                            if (period < MinimalTradePollingPeriodMilliseconds)
                            {
                                period = MinimalTradePollingPeriodMilliseconds;
                            }
                        }
                    }

                    return period;
                }
            }

            /// <inheritdoc />
            public void Dispose()
            {
                Dispose(true);
            }

            /// <summary>
            /// Adds a granularity value to update the polling interval of the trade monitor.
            /// </summary>
            /// <param name="granularity">The granularity.</param>
            internal void AddGranularity(TimeGranularity granularity)
            {
                long period = PeriodFromGranularity(granularity);
                lock (subscribedPeriodListLock)
                {
                    subscribedPeriodList.Add(period);
                }

                SetUnforcedPeriodMilliseconds();
            }

            /// <summary>
            /// Removes a granularity value to update the polling interval of the trade monitor.
            /// </summary>
            /// <param name="granularity">The granularity.</param>
            internal void RemoveGranularity(TimeGranularity granularity)
            {
                long period = PeriodFromGranularity(granularity);
                lock (subscribedPeriodListLock)
                {
                    subscribedPeriodList.Remove(period);
                }

                SetUnforcedPeriodMilliseconds();
            }

            /// <summary>
            /// Update the minimum polling interval of the trade monitor.
            /// </summary>
            /// <param name="period">The period in milliseconds.</param>
            internal void ApplyMinimalPeriodMilliseconds(long period)
            {
                if (PeriodMilliseconds < period)
                {
                    PeriodMilliseconds = period;
                }
            }

            /// <summary>
            /// Update the maximum polling interval of the trade monitor.
            /// </summary>
            /// <param name="period">The period in milliseconds.</param>
            internal void ApplyMaximalPeriodMilliseconds(long period)
            {
                if (PeriodMilliseconds > period)
                {
                    PeriodMilliseconds = period;
                }
            }

            private static long PeriodFromGranularity(TimeGranularity granularity)
            {
                int numberOfUnits = granularity.NumberOfUnits();
                if (granularity.IsSecond())
                {
                    return 1000L * numberOfUnits;
                }

                if (granularity.IsMinute())
                {
                    return 60000L * numberOfUnits;
                }

                if (granularity.IsHour())
                {
                    return 3600000L * numberOfUnits;
                }

                // Assume trade units.
                return MinimalTradePollingPeriodMilliseconds;
            }

            private static bool ParseJs(string str, ref DateTime dateTime, ref double price, ref double volume, DateTime firstTimeToFetch, out bool beforeFirstTimeToFetch, out bool ignore)
            {
                beforeFirstTimeToFetch = true;
                ignore = false;
                string[] splitted = str.Split(Splitter, StringSplitOptions.None);
                if (splitted.Length < 7)
                {
                    Log.Error($"EuronextMonitor.Trade: invalid intraday js: illegal number of splitted entries {splitted.Length} (should be 7) in [{str}], skipping");
                    return false;
                }

                string entry = splitted[4];

                // dateAndTime":"29\/08\/2012 09:00:02"
                //           11111111112222222222333333
                // 012345678901234567890123456789012345
                if (!entry.StartsWith(@"dateAndTime"":""", StringComparison.Ordinal) || entry.Length != 36 || entry[16] != '\\' || entry[17] != '/' ||
                    entry[20] != '\\' || entry[21] != '/' || entry[26] != ' ' || entry[29] != ':' || entry[32] != ':')
                {
                    Log.Error($"EuronextMonitor.Trade: invalid intraday js: invalid [dateAndTime] splitted entry [{entry}] in [{str}], skipping");
                    return false;
                }

                int day = 10 * (entry[14] - '0') + (entry[15] - '0');
                int month = 10 * (entry[18] - '0') + (entry[19] - '0');
                int year = 1000 * (entry[22] - '0') + 100 * (entry[23] - '0') + 10 * (entry[24] - '0') + (entry[25] - '0');
                int hour = 10 * (entry[27] - '0') + (entry[28] - '0');
                int minute = 10 * (entry[30] - '0') + (entry[31] - '0');
                int second = 10 * (entry[33] - '0') + (entry[34] - '0');
                dateTime = new DateTime(year, month, day, hour, minute, second);
                if (dateTime < firstTimeToFetch)
                {
                    return true;
                }

                beforeFirstTimeToFetch = false;

                // price":"1,329.39"
                //           11111
                // 012345678901234
                entry = splitted[5];
                if (!entry.StartsWith(@"price"":""", StringComparison.Ordinal) || entry[^1] != '\"')
                {
                    Log.Error($"EuronextMonitor.Trade: invalid intraday js: invalid [price] splitted entry [{entry}] in [{str}], skipping");
                    return false;
                }

                entry = entry[8..^1]; // 1,329.39
                entry = entry.Replace(",", string.Empty, StringComparison.Ordinal); // 1329.39
                if (!double.TryParse(entry, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out price))
                {
                    Log.Error($"EuronextMonitor.Trade: invalid intraday js: invalid [price] [{entry}] in [{str}], skipping");
                    return false;
                }

                // numberOfShares":"1,118.00"
                // numberOfShares":"0,00"
                //           111111111122
                // 0123456789012345678901
                entry = splitted[6];
                if (entry.StartsWith(@"numberOfShares"":null", StringComparison.Ordinal))
                {
                    volume = double.NaN;
                }
                else
                {
                    if (!entry.StartsWith(@"numberOfShares"":""", StringComparison.Ordinal) || entry[^1] != '\"')
                    {
                        Log.Error($"EuronextMonitor.Trade: invalid intraday js: invalid [numberOfShares] splitted entry [{entry}] in [{str}], skipping");
                        return false;
                    }

                    entry = entry[17..^1]; // 1,118.00 // 0,00
                    entry = entry.Replace(",", string.Empty, StringComparison.Ordinal); // 1118.00 // 000
                    if (!double.TryParse(entry, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out volume))
                    {
                        Log.Error($"EuronextMonitor.Trade: invalid intraday js: invalid [numberOfShares] [{entry}] in [{str}], skipping");
                        return false;
                    }
                }

                // TRADE_QUALIFIER":"OffBook Out of market"
                // TRADE_QUALIFIER":"OffBook Delta Neutral"
                // TRADE_QUALIFIER":"Exchange Continuous"
                //           111111111122
                // 0123456789012345678901
                entry = splitted[7];
                if (entry.StartsWith(@"TRADE_QUALIFIER"":""", StringComparison.Ordinal) && entry[^1] == '\"')
                {
                    entry = entry[18..^1];
                    if (entry.StartsWith("Automatic indicative index", StringComparison.Ordinal) ||
                        entry.StartsWith("Options liquidation index", StringComparison.Ordinal) ||
                        entry.StartsWith("Closing Reference index", StringComparison.Ordinal) ||
                        entry.StartsWith("OffBook", StringComparison.Ordinal))
                    {
                        ignore = true;
                    }
                }

                return true;
            }

            private static Stack<Trade> Fetch(string url, string referer, long lastItemTicks, bool accumulate, bool firstDownload)
            {
                DateTime dateTime = DateTime.UtcNow.AddHours(1); // Time zone 1.
                if (firstDownload)
                {
                    if (dateTime.Hour < 8)
                    {
                        dateTime = dateTime.AddDays(-1);
                    }

                    // Roll back to the last business date.
                    while (dateTime.IsEuronextHoliday())
                    {
                        dateTime = dateTime.AddDays(-1);
                    }

                    dateTime = dateTime.Date;
                    url = string.Concat(url, MillisecondsSinceBegin1970(dateTime).ToString(NumberFormatInfo.InvariantInfo));
                }
                else
                {
                    DateTime dateTimeLast = new DateTime(lastItemTicks);
                    if (dateTimeLast.Day != dateTime.Day)
                    {
                        // Next day. Skip till 8:00.
                        if (dateTime.Hour < 8)
                        {
                            return new Stack<Trade>();
                        }

                        dateTime = dateTime.Date;
                        url = string.Concat(url, MillisecondsSinceBegin1970(dateTime).ToString(NumberFormatInfo.InvariantInfo));
                    }
                    else
                    {
                        // The same day. Add one second to the last fetched time.
                        dateTime = dateTimeLast.AddSeconds(1);
                        url = string.Concat(url, MillisecondsSinceBegin1970(dateTime.AddHours(-1)).ToString(NumberFormatInfo.InvariantInfo));
                    }
                }

                Log.Trace($"EuronextMonitor.Trade: downloading {url}");
                var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                webRequest.Proxy = WebRequest.DefaultWebProxy;

                // DefaultCredentials represents the system credentials for the current
                // security context in which the application is running. For a client-side
                // application, these are usually the Windows credentials
                // (user name, password, and domain) of the user running the application.
                webRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                webRequest.Timeout = TradePollingDownloadTimeoutMilliseconds;
                webRequest.Referer = referer;
                webRequest.UserAgent = UserAgent;
                webRequest.Accept = "application/json, text/javascript, */*";
                webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us,en;q=0.5");
                webRequest.Headers.Add(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                webRequest.KeepAlive = true;
                WebResponse webResponse = webRequest.GetResponse();
                Stream responseStream = webResponse.GetResponseStream();
                if (responseStream == null)
                {
                    Log.Error("EuronextMonitor.Trade: received null response stream.");
                    return null;
                }

                string json;
                using (var streamReader = new StreamReader(responseStream))
                {
                    json = streamReader.ReadToEnd();
                }

                if (string.IsNullOrEmpty(json))
                {
                    Log.Error("EuronextMonitor.Trade: no data downloaded, skipping");
                    return null;
                }

                if (!json.EndsWith("}]}", StringComparison.Ordinal))
                {
                    Log.Error($"EuronextMonitor.Trade: no intraday data found in downloaded json [{json}], skipping");
                    return null;
                }

                json = json[0..^3];
                int i = json.IndexOf("[{", StringComparison.Ordinal);
                if (i < 0)
                {
                    Log.Error($"EuronextMonitor.Trade: no intraday data found in downloaded json [{json}], skipping");
                    return null;
                }

                json = json.Substring(i + 2);
                var stack = new Stack<Trade>(1024);
                bool beforeFirstTimeToFetch;
                DateTime firstTimeToFetch = dateTime;

                Log.Trace($"EuronextMonitor.Trade: firstTimeToFetch {firstTimeToFetch}");
                double price = 0, volume = 0;
                Trade lastTrade = null;
                while ((i = json.LastIndexOf("},{", StringComparison.Ordinal)) >= 0)
                {
                    if (!ParseJs(json.Substring(i + 3), ref dateTime, ref price, ref volume, firstTimeToFetch, out beforeFirstTimeToFetch, out var ignore))
                    {
                        return stack;
                    }

                    if (beforeFirstTimeToFetch)
                    {
                        return stack;
                    }

                    if (ignore)
                    {
                        json = json.Substring(0, i);
                        continue;
                    }

                    Log.Trace($"EuronextMonitor.Trade: >> take [{dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture)} >= {firstTimeToFetch.ToString("HH:mm:ss", CultureInfo.InvariantCulture)}]");
                    if (accumulate)
                    {
                        if (lastTrade == null)
                        {
                            lastTrade = new Trade(dateTime, price, volume);
                            stack.Push(lastTrade);
                        }
                        else
                        {
                            // ReSharper disable once CompareOfFloatsByEqualityOperator
                            if (lastTrade.Time.Ticks == dateTime.Ticks && lastTrade.Price == price)
                            {
                                lastTrade.Volume += volume;
                            }
                            else
                            {
                                lastTrade = new Trade(dateTime, price, volume);
                                stack.Push(lastTrade);
                            }
                        }
                    }
                    else
                    {
                        stack.Push(new Trade(dateTime, price, volume));
                    }

                    json = json.Substring(0, i);
                }

                // Here we have the very first trade.
                if (!ParseJs(json, ref dateTime, ref price, ref volume, firstTimeToFetch, out beforeFirstTimeToFetch, out _))
                {
                    return stack;
                }

                if (json.Contains("Automatic indicative index", StringComparison.Ordinal))
                {
                    Log.Information("EuronextMonitor.Trade: dropped the very first entry with trade qualifier [Automatic indicative index]");
                    return stack;
                }

                if (beforeFirstTimeToFetch)
                {
                    return stack;
                }

                if (firstDownload && lastTrade != null && dateTime.Ticks > lastTrade.Time.Ticks && (dateTime.Hour >= 16 && dateTime.Hour <= 23))
                {
                    // The very first sample is sometimes the last sample of the previous day,
                    // so the timestamps are decreasing. This happens only with indices (tradeId = 0).
                    Log.Information($"EuronextMonitor.Trade: dropped the very first entry with decreasing timestamp: first [{dateTime}], second [{lastTrade.Time}]");
                    return stack;
                }

                Log.Trace($"EuronextMonitor.Trade: >> take [{dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture)} >= {firstTimeToFetch.ToString("HH:mm:ss", CultureInfo.InvariantCulture)}]");
                if (accumulate)
                {
                    if (lastTrade == null)
                    {
                        stack.Push(new Trade(dateTime, price, volume));
                    }
                    else
                    {
                        // ReSharper disable once CompareOfFloatsByEqualityOperator
                        if (lastTrade.Time.Ticks == dateTime.Ticks && lastTrade.Price == price)
                        {
                            lastTrade.Volume += volume;
                        }
                        else
                        {
                            stack.Push(new Trade(dateTime, price, volume));
                        }
                    }
                }
                else
                {
                    stack.Push(new Trade(dateTime, price, volume));
                }

                return stack;
            }

            /// <summary>
            /// The number of milliseconds since 1 january 1970.
            /// </summary>
            private static long MillisecondsSinceBegin1970(DateTime dateTime)
            {
                return (long)(dateTime - Year1970).TotalMilliseconds;
            }

            private void TimerChange(long dueTime, long period)
            {
                lock (timerLock)
                {
                    if (timer != null && timerFree)
                    {
                        timer.Change(dueTime, period);
                    }
                }
            }

            private void SetUnforcedPeriodMilliseconds()
            {
                long period = MinimalSubscribedPeriod;
                if (period < 0L)
                {
                    return;
                }

                if (MinimalTradePollingPeriodMilliseconds > period)
                {
                    period = MinimalTradePollingPeriodMilliseconds;
                }

                if (MaximalTradePollingPeriodMilliseconds < period)
                {
                    period = MaximalTradePollingPeriodMilliseconds;
                }

                if (PeriodMilliseconds != period)
                {
                    PeriodMilliseconds = period;
                }
            }

            private AutoResetEventThread EventDispatcherThread()
            {
                var t = new AutoResetEventThread(() =>
                {
                    bool firstFetch = true;
                    while (timer != null)
                    {
                        Stack<Trade> enumerable;
                        while ((enumerable = queue.Dequeue()) != null)
                        {
                            Console.WriteLine($"---------[1] thread {Thread.CurrentThread.ManagedThreadId}, firstFetch {firstFetch}, count {enumerable.Count}, first {enumerable.First().Time.ToShortTimeString()}, last {enumerable.Last().Time.ToShortTimeString()}");
                            if (firstFetch && enumerable.Count > 0)
                            {
                                firstFetch = false;
                                if (!IsSubscriptionWithHistory)
                                {
                                    Trade trade = enumerable.Last(); // TODO
                                    enumerable = new Stack<Trade>();
                                    enumerable.Push(trade);
                                    Console.WriteLine($"---------[2] thread {Thread.CurrentThread.ManagedThreadId}, firstFetch False, count {enumerable.Count}, first {enumerable.First().Time.ToShortTimeString()}, last {enumerable.Last().Time.ToShortTimeString()}");
                                }
                            }

                            lock (tradeListLock)
                            {
                                tradeList.AddRange(enumerable);
                            }

                            lock (subscriberEventLock)
                            {
                                TradeEnumerableEventHandler handler = subscriberEvent;
                                if (handler != null)
                                {
                                    Delegate[] handlers = handler.GetInvocationList();
                                    foreach (Delegate currentHandler in handlers)
                                    {
                                        var currentSubscriber = currentHandler as TradeEnumerableEventHandler;
                                        currentSubscriber?.Invoke(enumerable);
                                    }
                                }
                            }

                            if (timer == null)
                            {
                                break;
                            }
                        }

                        if (timer != null)
                        {
                            thread.AutoResetEvent.WaitOne(TradeEventDispatcherThreadWaitMilliseconds);
                        }
                    }

                    Dispose();
                }) { Thread = { IsBackground = true } };
                return t;
            }

            private Timer PollingTimer()
            {
                return new Timer(
                    x =>
                    {
                        timerFree = false;
                        if (timer == null)
                        {
                            return;
                        }

                        timer.Change(Timeout.Infinite, Timeout.Infinite);
                        long period = PeriodMilliseconds;
                        long millisecondsDelay = period + DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                        if (timer == null)
                        {
                            return;
                        }

                        Stack<Trade> stack;
                        try
                        {
                            stack = Fetch(url, referer, lastItemTicks, Accumulate, firstDownload);
                        }
                        catch (TimeoutException e)
                        {
                            stack = null;
                            ThreadPool.QueueUserWorkItem(state => Log.Error($"EuronextMonitor.Trade: TimeoutException: {e.Message}"));
                        }
                        catch (WebException e)
                        {
                            stack = null;
                            ThreadPool.QueueUserWorkItem(state => Log.Error($"EuronextMonitor.Trade: WebException: status={e.Status}, {e.Message}"));
                        }

                        if (timer == null)
                        {
                            return;
                        }

                        if (stack != null && stack.Count > 0)
                        {
                            var lastItem = stack.Last();
                            Console.WriteLine($"---------[0] thread {Thread.CurrentThread.ManagedThreadId}, firstDownload {firstDownload}, count {stack.Count}, first {stack.First().Time.ToShortTimeString()}, last {lastItem.Time.ToShortTimeString()}, period ms {period}");
                            if (firstDownload)
                            {
                                firstDownload = false;
                            }

                            lastItemTicks = lastItem.Time.Ticks;
                            queue.Enqueue(stack);
                            if (timer != null)
                            {
                                thread.AutoResetEvent.Set();
                            }
                        }

                        millisecondsDelay -= DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                        if (millisecondsDelay < 0)
                        {
                            Log.Warning($"EuronextMonitor.Trade: out of sync {millisecondsDelay} of {period} ms");
                            millisecondsDelay = 0;
                        }

                        if (timer == null)
                        {
                            return;
                        }

                        timer.Change(millisecondsDelay, PeriodMilliseconds);
                        if (millisecondsDelay > 100)
                        {
                            timerFree = true;
                        }
                    },
                    this,
                    Timeout.Infinite,
                    Timeout.Infinite);
            }

            /// <summary>
            /// <see cref="IDisposable"/> implementation.
            /// </summary>
            /// <param name="disposing">Indicates the disposing condition.</param>
            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    lock (timerLock)
                    {
                        if (timer != null)
                        {
                            timer.Dispose();
                            timer = null;
                        }
                    }

                    if (thread != null)
                    {
                        thread.Dispose();
                        thread = null;
                    }
                }
            }
        }

        /// <summary>
        /// Monitors the intraday quote data for a single instrument.
        /// </summary>
        internal sealed class InstrumentQuoteMonitor : IDisposable
        {
            private readonly Instrument instrument;
            private readonly string url;
            private readonly string referer;
            private readonly object timerLock = new object();
            private readonly object quoteListLock = new object();
            private readonly List<Quote> quoteList = new List<Quote>(1024);
            private readonly object subscriberEventLock = new object();
            private volatile bool firstDownload = true;
            private AutoResetEventThread thread;
            private CompareAndSwapQueue<Quote> queue;
            private Quote quotePrevious;
            private Timer timer;
            private bool timerFree;
            private long periodMilliseconds = DefaultMinimumQuotePollingPeriodMilliseconds;
            private QuoteEventHandler subscriberEvent;
            private int subscriberCount;

            /// <summary>
            /// Initializes a new instance of the <see cref="InstrumentQuoteMonitor"/> class.
            /// </summary>
            /// <param name="instrument">The instrument.</param>
            internal InstrumentQuoteMonitor(Instrument instrument)
            {
                string isin = instrument.GetSecurityIdAs(InstrumentSecurityIdSource.Isin);
                if (isin == null)
                {
                    throw new ArgumentException("Instrument does not have Isin.", nameof(instrument));
                }

                this.instrument = instrument;
                InstrumentType type = instrument.Type;
                string mic = instrument.Exchange.Mic.ToString().ToUpperInvariant();
#pragma warning disable S1075 // URIs should not be hardcoded
                if (type == InstrumentType.Index)
                {
                    // https://europeanequities.nyx.com/en/nyx_eu_listings/real-time/quote?isin=FR0000130809&mic=XPAR
                    const string indexUriFormat = "https://europeanequities.nyx.com/en/nyx_eu_listings/real-time/quote?isin={0}&mic={1}";

                    // https://indices.nyx.com/nl/products/indices/NL0000000107-XAMS/quotes
                    const string indexRefererFormat = "https://indices.nyx.com/nl/products/indices/{0}-{1}/quotes";

                    url = string.Format(CultureInfo.InvariantCulture, indexUriFormat, isin, mic);
                    referer = string.Format(CultureInfo.InvariantCulture, indexRefererFormat, isin, mic);
                }
                else if (type == InstrumentType.Stock)
                {
                    // https://www.euronext.com/en/nyx_eu_listings/real-time/quote?isin=FR0000130809&mic=XPAR
                    const string stockUriFormat = "https://www.euronext.com/en/nyx_eu_listings/real-time/quote?isin={0}&mic={1}";

                    // https://www.euronext.com/en/products/equities/FR0010533075-XPAR/quotes
                    const string stockRefererFormat = "https://www.euronext.com/en/products/equities/{0}-{1}/quotes";

                    url = string.Format(CultureInfo.InvariantCulture, stockUriFormat, isin, mic);
                    referer = string.Format(CultureInfo.InvariantCulture, stockRefererFormat, isin, mic);
                }
                else
                {
                    // Etf, Etv, Fund, Inav
                    // https://www.euronext.com/en/nyx_eu_listings/real-time/quote?isin=FR0000130809&mic=XPAR
                    const string etpUriFormat = "https://www.euronext.com/en/nyx_eu_listings/real-time/quote?isin={0}&mic={1}";

                    // https://www.euronext.com/en/products/etfs/PTGFIBIM0004-XLIS/quotes
                    const string etpRefererFormat = "https://www.euronext.com/en/products/etfs/{0}-{1}/quotes";

                    url = string.Format(CultureInfo.InvariantCulture, etpUriFormat, isin, mic);
                    referer = string.Format(CultureInfo.InvariantCulture, etpRefererFormat, isin, mic);
                }
#pragma warning restore S1075 // URIs should not be hardcoded
            }

            /// <summary>
            /// The quote event handler.
            /// </summary>
            /// <param name="quote">The quote.</param>
            internal delegate void QuoteEventHandler(Quote quote);

            /// <summary>
            /// The event handler for the subscription-related events.
            /// </summary>
            internal event QuoteEventHandler Event
            {
                add
                {
                    lock (subscriberEventLock)
                    {
                        ++subscriberCount;
                        subscriberEvent += value;
                        if (IsSubscriptionWithHistory)
                        {
                            lock (quoteListLock)
                            {
                                foreach (Quote quote in quoteList)
                                {
                                    value(quote);
                                }
                            }
                        }

                        TimerActive = true;
                    }
                }

                remove
                {
                    bool stop = false;
                    lock (subscriberEventLock)
                    {
                        // ReSharper disable once DelegateSubtraction
                        subscriberEvent -= value;
                        if (--subscriberCount < 1)
                        {
                            subscriberCount = 0;
                            stop = true;
                        }
                    }

                    if (stop)
                    {
                        TimerActive = false;
                        lock (InstrumentQuoteMonitorDictionaryLock)
                        {
                            if (InstrumentQuoteMonitorDictionary.ContainsKey(instrument))
                            {
                                InstrumentQuoteMonitorDictionary.Remove(instrument);
                            }
                        }

                        lock (quoteListLock)
                        {
                            quoteList.Clear();
                        }
                    }
                }
            }

            /// <summary>
            /// Gets or sets the sequential download timer period in milliseconds.
            /// </summary>
            internal long PeriodMilliseconds
            {
                get => Interlocked.Read(ref periodMilliseconds);

                set
                {
                    long delta = value - Interlocked.Exchange(ref periodMilliseconds, value);
                    if (delta < 0L)
                    {
                        TimerChange(0, value);
                    }
                }
            }

            private bool TimerActive
            {
                set
                {
                    lock (timerLock)
                    {
                        if (value == false)
                        {
                            if (timer != null)
                            {
                                Timer timerToDispose = timer;
                                timer = null;
                                timerToDispose.Change(Timeout.Infinite, Timeout.Infinite);
                                timerToDispose.Dispose();
                            }
                        }
                        else
                        {
                            if (timer == null)
                            {
                                queue = new CompareAndSwapQueue<Quote>();
                                thread = EventDispatcherThread();
                                timer = PollingTimer();
                                timer.Change(0, PeriodMilliseconds);
                                thread.Thread.Start();
                            }
                        }
                    }
                }
            }

            /// <inheritdoc />
            public void Dispose()
            {
                Dispose(true);
            }

            private static Quote Fetch(string url, string referer, bool firstDownload)
            {
                const string errorFormat1 = "EuronextMonitor.Quote: failed to parse {0} [{1}] from [{2}], aborting quote";
                const string errorFormat2 = "EuronextMonitor.Quote: unexpected line [{0}] failed to find [{1}], aborting quote";
                const string errorFormat3 = "EuronextMonitor.Quote: failed to parse double value [{0}] from [{1}], aborting quote";
                Log.Trace($"EuronextMonitor.Quote: downloading URL {url}");
                var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                webRequest.Proxy = WebRequest.DefaultWebProxy;

                // DefaultCredentials represents the system credentials for the current
                // security context in which the application is running. For a client-side
                // application, these are usually the Windows credentials
                // (user name, password, and domain) of the user running the application.
                webRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                webRequest.Timeout = QuotePollingDownloadTimeoutMilliseconds;
                webRequest.Referer = referer;
                webRequest.UserAgent = UserAgent;
                webRequest.Accept = "text/html, */*";

                // webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate")
                webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
                webRequest.Headers.Add(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                webRequest.KeepAlive = true;
                WebResponse webResponse = webRequest.GetResponse();
                Stream responseStream = webResponse.GetResponseStream();
                if (responseStream == null)
                {
                    ThreadPool.QueueUserWorkItem(state => Log.Error("EuronextMonitor.Quote: received null response stream."));
                    return null;
                }

                using var streamReader = new StreamReader(responseStream);

                // ReSharper disable StringLiteralTypo
                const string pattern1 = "<span class=\"trade-date\" id=\"datetimeLastvalue\">";
                const int pattern1Len = 48;
                const string pattern2 = "</span>";
                const string pattern3 = "<span class=\"price-bottom\" id=\"tradingStatusvalue\">";
                const int pattern3Len = 51;
                const string pattern4 = "<span id=\"bidPricevalue\">";
                const int pattern4Len = 25;
                const string pattern5 = "<span id=\"bidVolumevalue\">";
                const int pattern5Len = 26;
                const string pattern6 = "<span id=\"askPricevalue\">";
                const int pattern6Len = 25;
                const string pattern7 = "<span id=\"askVolumevalue\">";
                const int pattern7Len = 26;

                // ReSharper restore StringLiteralTypo
                bool statusNotFound = true;
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    int i = line.IndexOf(pattern1, StringComparison.Ordinal);
                    if (i > -1 && line.IndexOf(pattern2, i, StringComparison.Ordinal) >= 0)
                    {
                        // ReSharper disable once CommentTypo
                        // [... <span class="trade-date" id="datetimeLastvalue">08/02/2013 17:38 CET</span>]
                        Log.Trace($"EuronextMonitor.Quote: >{line}");
                        string s = line.Substring(i + pattern1Len);

                        // [08/02/2013 17:38 CET</span>]
                        if (!ParseDdsMmsYyyy(s, out var year, out var month, out var day))
                        {
                            Log.Error(errorFormat1, "date", s, line);
                            return null;
                        }

                        s = s.Substring(11); // [17:38 CET</span>]
                        DateTime dateTime = ParseHhcMm(s, year, month, day);
                        if (dateTime.IsZero())
                        {
                            Log.Error(errorFormat1, "time", s, line);
                            return null;
                        }

                        line = streamReader.ReadLine();
                        while (line != null)
                        {
                            if (statusNotFound)
                            {
                                // ReSharper disable once CommentTypo
                                // [... <span class="price-bottom" id="tradingStatusvalue">Closed</span>]
                                i = line.IndexOf(pattern3, StringComparison.Ordinal);
                                if (i > -1 /*&& 0 < line.IndexOf(pattern2, i, StringComparison.Ordinal)*/)
                                {
                                    Log.Trace($"EuronextMonitor.Quote: >{line}");
                                    s = line.Substring(i + pattern3Len);
                                    if (s.StartsWith("Closed", StringComparison.Ordinal) && !firstDownload)
                                    {
                                        return null;
                                    }

                                    line = streamReader.ReadLine();
                                    statusNotFound = false;
                                    continue;
                                }
                            }

                            // ReSharper disable once CommentTypo
                            // [... <span id="bidPricevalue">31.945</span>]
                            i = line.IndexOf(pattern4, StringComparison.Ordinal);
                            if (i > -1)
                            {
                                Log.Trace($"EuronextMonitor.Quote: >{line}");
                                s = line.Substring(i + pattern4Len);
                                i = s.IndexOf(pattern2, 0, StringComparison.Ordinal);
                                if (i < 0)
                                {
                                    Log.Error(errorFormat2, line, pattern2);
                                    return null;
                                }

                                s = s.Substring(0, i);
                                if (!double.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var bid))
                                {
                                    Log.Error(errorFormat3, s, line);
                                    return null;
                                }

                                line = streamReader.ReadLine();
                                while (line != null)
                                {
                                    // ReSharper disable once CommentTypo
                                    // [... <span id="bidVolumevalue">578</span>]
                                    i = line.IndexOf(pattern5, StringComparison.Ordinal);
                                    if (i > -1)
                                    {
                                        Log.Trace($"EuronextMonitor.Quote: >{line}");
                                        s = line.Substring(i + pattern5Len);
                                        i = s.IndexOf(pattern2, 0, StringComparison.Ordinal);
                                        if (i < 0)
                                        {
                                            Log.Error(errorFormat2, line, pattern2);
                                            return null;
                                        }

                                        s = s.Substring(0, i);
                                        if (!double.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var bidQty))
                                        {
                                            Log.Error(errorFormat3, s, line);
                                            return null;
                                        }

                                        line = streamReader.ReadLine();
                                        while (line != null)
                                        {
                                            // ReSharper disable once CommentTypo
                                            // [... <span id="askPricevalue">31.96</span>]
                                            i = line.IndexOf(pattern6, StringComparison.Ordinal);
                                            if (i > -1)
                                            {
                                                Log.Trace($"EuronextMonitor.Quote: >{line}");
                                                s = line.Substring(i + pattern6Len);
                                                i = s.IndexOf(pattern2, 0, StringComparison.Ordinal);
                                                if (i < 0)
                                                {
                                                    Log.Error(errorFormat2, line, pattern2);
                                                    return null;
                                                }

                                                s = s.Substring(0, i);
                                                if (!double.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var ask))
                                                {
                                                    Log.Error(errorFormat3, s, line);
                                                    return null;
                                                }

                                                line = streamReader.ReadLine();
                                                while (line != null)
                                                {
                                                    // ReSharper disable once CommentTypo
                                                    // [... <span id="askVolumevalue">507</span>]
                                                    i = line.IndexOf(pattern7, StringComparison.Ordinal);
                                                    if (i > -1)
                                                    {
                                                        Log.Trace($"EuronextMonitor.Quote: >{line}");
                                                        s = line.Substring(i + pattern7Len);
                                                        i = s.IndexOf(pattern2, 0, StringComparison.Ordinal);
                                                        if (i < 0)
                                                        {
                                                            Log.Error(errorFormat2, line, pattern2);
                                                            return null;
                                                        }

                                                        s = s.Substring(0, i);
                                                        if (!double.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var askQty))
                                                        {
                                                            Log.Error(errorFormat3, s, line);
                                                            return null;
                                                        }

                                                        var quote = new Quote(dateTime, bid, bidQty, ask, askQty);

                                                        Log.Trace($"EuronextMonitor.Quote: <{quote}");
                                                        return quote;
                                                    }

                                                    line = streamReader.ReadLine();
                                                }
                                            }

                                            line = streamReader.ReadLine();
                                        }
                                    }

                                    line = streamReader.ReadLine();
                                }
                            }

                            line = streamReader.ReadLine();
                        }
                    }

                    line = streamReader.ReadLine();
                }

                return null;
            }

            private static DateTime ParseHhcMm(string hhCmm, int year, int month, int day)
            {
                if (hhCmm.Length > 5)
                {
                    char c = hhCmm[0];
                    if (c >= '0' && c <= '9')
                    {
                        int hour = 10 * (c - '0');
                        c = hhCmm[1];
                        if (c >= '0' && c <= '9')
                        {
                            hour += c - '0';
                            if (hour >= 0 && hour < 24)
                            {
                                // Hour
                                c = hhCmm[2];
                                if (c == ':')
                                {
                                    c = hhCmm[3];
                                    if (c >= '0' && c <= '9')
                                    {
                                        int minute = 10 * (c - '0');
                                        c = hhCmm[4];
                                        if (c >= '0' && c <= '9')
                                        {
                                            minute += c - '0';
                                            if (minute >= 0 && minute < 60)
                                            {
                                                // Minute
                                                c = hhCmm[5];
                                                if (c == ':')
                                                {
                                                    c = hhCmm[6];
                                                    if (c >= '0' && c <= '9')
                                                    {
                                                        int second = 10 * (c - '0');
                                                        c = hhCmm[7];
                                                        if (c >= '0' && c <= '9')
                                                        {
                                                            second += c - '0';
                                                            return new DateTime(year, month, day, hour, minute, second);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    return new DateTime(year, month, day, hour, minute, 0);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return new DateTime(0L);
            }

            // ReSharper disable once IdentifierTypo
            private static bool ParseDdsMmsYyyy(string ddSmmSyyyy, out int year, out int month, out int day)
            {
                if (ddSmmSyyyy.Length > 9)
                {
                    char c = ddSmmSyyyy[0];
                    if (c >= '0' && c <= '9')
                    {
                        day = 10 * (c - '0');
                        c = ddSmmSyyyy[1];
                        if (c >= '0' && c <= '9')
                        {
                            day += c - '0';
                            c = ddSmmSyyyy[2];
                            if (c == '/')
                            {
                                c = ddSmmSyyyy[3];
                                if (c >= '0' && c <= '9')
                                {
                                    month = 10 * (c - '0');
                                    c = ddSmmSyyyy[4];
                                    if (c >= '0' && c <= '9')
                                    {
                                        month += c - '0';
                                        c = ddSmmSyyyy[5];
                                        if (c == '/')
                                        {
                                            c = ddSmmSyyyy[6];
                                            if (c >= '0' && c <= '9')
                                            {
                                                year = 1000 * (c - '0');
                                                c = ddSmmSyyyy[7];
                                                if (c >= '0' && c <= '9')
                                                {
                                                    year += 100 * (c - '0');
                                                    c = ddSmmSyyyy[8];
                                                    if (c >= '0' && c <= '9')
                                                    {
                                                        year += 10 * (c - '0');
                                                        c = ddSmmSyyyy[9];
                                                        if (c >= '0' && c <= '9')
                                                        {
                                                            year += c - '0';
                                                            return true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                day = 0;
                month = 0;
                year = 0;
                return false;
            }

            private void TimerChange(long dueTime, long period)
            {
                lock (timerLock)
                {
                    if (timer != null && timerFree)
                    {
                        timer.Change(dueTime, period);
                    }
                }
            }

            private AutoResetEventThread EventDispatcherThread()
            {
                var t = new AutoResetEventThread(() =>
                {
                    bool firstFetch = true;
                    while (timer != null)
                    {
                        Quote quote;
                        while ((quote = queue.Dequeue()) != null)
                        {
                            lock (quoteListLock)
                            {
                                quoteList.Add(quote);
                            }

                            if (firstFetch)
                            {
                                firstFetch = false;
                                if (!IsSubscriptionWithHistory)
                                {
                                    continue;
                                }
                            }

                            lock (subscriberEventLock)
                            {
                                QuoteEventHandler handler = subscriberEvent;
                                if (handler != null)
                                {
                                    Delegate[] handlers = handler.GetInvocationList();
                                    foreach (Delegate currentHandler in handlers)
                                    {
                                        var currentSubscriber = currentHandler as QuoteEventHandler;
                                        currentSubscriber?.Invoke(quote);
                                    }
                                }
                            }

                            if (timer == null)
                            {
                                break;
                            }
                        }

                        if (timer != null)
                        {
                            thread.AutoResetEvent.WaitOne(QuoteEventDispatcherThreadWaitMilliseconds);
                        }
                    }

                    Dispose();
                }) { Thread = { IsBackground = true } };
                return t;
            }

            private Timer PollingTimer()
            {
                return new Timer(
                    x =>
                    {
                        timerFree = false;
                        if (timer == null)
                        {
                            return;
                        }

                        timer.Change(Timeout.Infinite, Timeout.Infinite);
                        long period = PeriodMilliseconds;
                        long millisecondsDelay = period + DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                        Quote quote;
                        if (timer == null)
                        {
                            return;
                        }

                        try
                        {
                            quote = Fetch(url, referer, firstDownload);
                        }
                        catch (TimeoutException e)
                        {
                            quote = null;
                            ThreadPool.QueueUserWorkItem(state => Log.Error($"EuronextMonitor.Quote: TimeoutException, {e.Message}"));
                        }
                        catch (WebException e)
                        {
                            quote = null;
                            ThreadPool.QueueUserWorkItem(state => Log.Error($"EuronextMonitor.Quote: WebException: status={e.Status}, {e.Message}"));
                        }

                        firstDownload = false;
                        if (timer == null)
                        {
                            return;
                        }

                        if (quote != null)
                        {
                            if (quotePrevious == null
                                || Math.Abs(quotePrevious.AskPrice - quote.AskPrice) > double.Epsilon
                                || Math.Abs(quotePrevious.BidPrice - quote.BidPrice) > double.Epsilon
                                || Math.Abs(quotePrevious.AskSize - quote.AskSize) > double.Epsilon
                                || Math.Abs(quotePrevious.BidSize - quote.BidSize) > double.Epsilon /*|| !quotePrevious.Time.Equals(quote.Time)*/)
                            {
                                queue.Enqueue(quote);
                                if (timer != null)
                                {
                                    thread.AutoResetEvent.Set();
                                }
                            }

                            quotePrevious = quote;
                        }

                        millisecondsDelay -= DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                        if (millisecondsDelay < 0L)
                        {
                            Log.Warning($"EuronextMonitor.Quote: out of sync {millisecondsDelay} of {period} ms");
                            millisecondsDelay = 0L;
                        }

                        if (timer == null)
                        {
                            return;
                        }

                        timer.Change(millisecondsDelay, PeriodMilliseconds);
                        if (millisecondsDelay > 100)
                        {
                            timerFree = true;
                        }
                    },
                    this,
                    Timeout.Infinite,
                    Timeout.Infinite);
            }

            /// <summary>
            /// <see cref="IDisposable"/> implementation.
            /// </summary>
            /// <param name="disposing">Indicates the disposing condition.</param>
            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    lock (timerLock)
                    {
                        if (timer != null)
                        {
                            timer.Dispose();
                            timer = null;
                        }
                    }

                    if (thread != null)
                    {
                        thread.Dispose();
                        thread = null;
                    }
                }
            }
        }
    }
}
