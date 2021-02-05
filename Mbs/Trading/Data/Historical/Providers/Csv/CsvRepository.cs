using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    /// <summary>
    /// Csv data repository utilities.
    /// </summary>
    public static class CsvRepository
    {
        /// <summary>
        /// The data provider name.
        /// </summary>
        internal const string Provider = "Csv";

        private const string Prefix = "CsvRepository";
        private const string InstrumentElement = "instrument";
        private const string MicAttribute = "mic";
        private const string SymbolAttribute = "symbol";
        private const string IsinAttribute = "isin";
        private const string FileAttribute = "file";
        private const string Underscore = "_";
        private const string CsvColumnIndicesAttribute = "csvColumnIndices";
        private const string CsvDateTimeFormatAttribute = "csvDateTimeFormat";
        private const string CsvTimeGranularityAttribute = "csvTimeGranularity";
        private const string CsvSeparatorCharacterAttribute = "csvSeparatorChar";
        private const string CsvCommentCharacterAttribute = "csvCommentChar";
        private const string CsvIsAdjustedDataAttribute = "csvIsAdjusted";

        private static readonly object RepositoryPathLock = new object();
        private static readonly object RepositoryIndexFileLock = new object();
        private static readonly object CacheDictionaryLock = new object();
        private static readonly Dictionary<string, InstrumentCsvInfo> InfoCacheDictionary = new Dictionary<string, InstrumentCsvInfo>();
        private static readonly Dictionary<string, List<Ohlcv>> OhlcvCacheDictionary = new Dictionary<string, List<Ohlcv>>();
        private static readonly Dictionary<string, List<Scalar>> ScalarCacheDictionary = new Dictionary<string, List<Scalar>>();
        private static readonly Dictionary<string, List<Trade>> TradeCacheDictionary = new Dictionary<string, List<Trade>>();
        private static readonly Dictionary<string, List<Quote>> QuoteCacheDictionary = new Dictionary<string, List<Quote>>();

        private static readonly string DirectorySeparator = Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture);
        private static readonly string AltDirectorySeparator = Path.AltDirectorySeparatorChar.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// An instance of the xml reader settings suitable to read repository index files.
        /// </summary>
        private static readonly XmlReaderSettings XmlReaderSettings = CreateXmlReaderSettings();

        private static string repositoryPath;
        private static string repositoryIndexFile;
        private static long cacheInstruments;

        /// <summary>
        /// Gets or sets a path to the repository directory. Ends with the directory separator character.
        /// </summary>
        public static string RepositoryPath
        {
            get
            {
                lock (RepositoryPathLock)
                {
                    return repositoryPath;
                }
            }

            set
            {
                lock (RepositoryPathLock)
                {
                    repositoryPath = value;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return;
                    }

                    if (!value.EndsWith(DirectorySeparator, StringComparison.Ordinal) &&
                        !value.EndsWith(AltDirectorySeparator, StringComparison.Ordinal))
                    {
                        repositoryPath = string.Concat(value, DirectorySeparator);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a path to the repository index file.
        /// </summary>
        public static string RepositoryIndexFile
        {
            get
            {
                lock (RepositoryIndexFileLock)
                {
                    return repositoryIndexFile;
                }
            }

            set
            {
                lock (RepositoryIndexFileLock)
                {
                    repositoryIndexFile = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CSV data will be cached.
        /// </summary>
        public static bool IsDataCache
        {
            get => Interlocked.Read(ref cacheInstruments) != 0L;

            set => Interlocked.Exchange(ref cacheInstruments, value ? 1L : 0L);
        }

        /// <summary>
        /// Clears the data cache.
        /// </summary>
        public static void ClearDataCache()
        {
            lock (CacheDictionaryLock)
            {
                InfoCacheDictionary.Clear();
                OhlcvCacheDictionary.Clear();
                ScalarCacheDictionary.Clear();
                TradeCacheDictionary.Clear();
                QuoteCacheDictionary.Clear();
            }
        }

        /// <summary>
        /// Adds an instrument with an associated csv info to the repository.
        /// </summary>
        /// <param name="instrument">The instrument to add.</param>
        /// <param name="csvInfo">The csv info to add.</param>
        public static void Add(Instrument instrument, CsvInfo csvInfo)
        {
            string file = csvInfo.FilePath;
            if (string.IsNullOrWhiteSpace(file))
            {
                return;
            }

            if (!File.Exists(file))
            {
                if (!Path.IsPathRooted(file))
                {
                    lock (RepositoryPathLock)
                    {
                        if (!string.IsNullOrWhiteSpace(repositoryPath))
                        {
                            file = Path.Combine(repositoryPath, file);
                        }
                    }

                    file = Path.GetFullPath(file);
                }

                if (!File.Exists(file))
                {
                    Log.Error(FileDoesNotExist(file));
                    return;
                }
            }

            csvInfo.FilePath = file;
            string key = Key(instrument);
            lock (CacheDictionaryLock)
            {
                if (!InfoCacheDictionary.TryGetValue(key, out InstrumentCsvInfo instrumentCsvInfo))
                {
                    instrumentCsvInfo = new InstrumentCsvInfo();
                    InfoCacheDictionary.Add(key, instrumentCsvInfo);
                }

                instrumentCsvInfo.Add(csvInfo);
            }
        }

        /// <summary>
        /// Gets the instrument CSV info or <c>null</c> if not found.
        /// </summary>
        /// <param name="instrument">The instrument to find.</param>
        /// <returns>The CSV info or null if not found.</returns>
        internal static InstrumentCsvInfo InstrumentInfo(Instrument instrument)
        {
            InstrumentCsvInfo instrumentInfo = FindInstrument(instrument);
            if (instrumentInfo == null)
            {
                Log.Error(MissingInstrument(instrument));
            }

            return instrumentInfo;
        }

        /// <summary>
        /// Given a <see cref="CsvInfo"/>, enumerates <see cref="Ohlcv"/> elements asynchronously.
        /// </summary>
        /// <param name="csvInfo">The csv info.</param>
        /// <param name="csvRequest">The csv request.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An asynchronous enumerable interface.</returns>
        internal static IEnumerable<Ohlcv> EnumerateOhlcvAsync(CsvInfo csvInfo, CsvRequest csvRequest, CancellationToken cancellationToken = default)
        {
            IEnumerable<Ohlcv> enumerable;
            try
            {
                enumerable = EnumerateOhlcvFileAsync(csvInfo, csvRequest, cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error(ErrorReadingFile(csvInfo.FilePath), ex);
                yield break;
            }

            using IEnumerator<Ohlcv> enumerator = enumerable.GetEnumerator();
            while (true)
            {
                try
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ErrorReadingFile(csvInfo.FilePath), ex);
                    break;
                }

                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Given a <see cref="CsvInfo"/>, enumerates <see cref="Scalar"/> elements asynchronously.
        /// </summary>
        /// <param name="csvInfo">The csv info.</param>
        /// <param name="csvRequest">The csv request.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An asynchronous enumerable interface.</returns>
        internal static IEnumerable<Scalar> EnumerateScalarAsync(CsvInfo csvInfo, CsvRequest csvRequest, CancellationToken cancellationToken = default)
        {
            IEnumerable<Scalar> enumerable;
            try
            {
                enumerable = EnumerateScalarFileAsync(csvInfo, csvRequest, cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error(ErrorReadingFile(csvInfo.FilePath), ex);
                yield break;
            }

            using IEnumerator<Scalar> enumerator = enumerable.GetEnumerator();
            while (true)
            {
                try
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ErrorReadingFile(csvInfo.FilePath), ex);
                    break;
                }

                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Given a <see cref="CsvInfo"/>, enumerates <see cref="Trade"/> elements asynchronously.
        /// </summary>
        /// <param name="csvInfo">The csv info.</param>
        /// <param name="csvRequest">The csv request.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An asynchronous enumerable interface.</returns>
        internal static IEnumerable<Trade> EnumerateTradeAsync(CsvInfo csvInfo, CsvRequest csvRequest, CancellationToken cancellationToken = default)
        {
            IEnumerable<Trade> enumerable;
            try
            {
                enumerable = EnumerateTradeFileAsync(csvInfo, csvRequest, cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error(ErrorReadingFile(csvInfo.FilePath), ex);
                yield break;
            }

            using IEnumerator<Trade> enumerator = enumerable.GetEnumerator();
            while (true)
            {
                try
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ErrorReadingFile(csvInfo.FilePath), ex);
                    break;
                }

                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Given a <see cref="CsvInfo"/>, enumerates <see cref="Quote"/> elements asynchronously.
        /// </summary>
        /// <param name="csvInfo">The csv info.</param>
        /// <param name="csvRequest">The csv request.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An asynchronous enumerable interface.</returns>
        internal static IEnumerable<Quote> EnumerateQuoteAsync(CsvInfo csvInfo, CsvRequest csvRequest, CancellationToken cancellationToken = default)
        {
            IEnumerable<Quote> enumerable;
            try
            {
                enumerable = EnumerateQuoteFileAsync(csvInfo, csvRequest, cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error(ErrorReadingFile(csvInfo.FilePath), ex);
                yield break;
            }

            using IEnumerator<Quote> enumerator = enumerable.GetEnumerator();
            while (true)
            {
                try
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ErrorReadingFile(csvInfo.FilePath), ex);
                    break;
                }

                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Gets an error message.
        /// </summary>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <returns>The error message.</returns>
        internal static string CannotComposeGranularity(TimeGranularity timeGranularity)
        {
            return $"{Prefix}: cannot compose the requested time granularity {timeGranularity}.";
        }

        /// <summary>
        /// Gets an error message.
        /// </summary>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <param name="name">The name of an entity.</param>
        /// <returns>The error message.</returns>
        internal static string TimeGranularityNotSupported(TimeGranularity timeGranularity, string name)
        {
            return $"{Prefix}: time granularity {timeGranularity} is not supported for {name} entities";
        }

        /// <summary>
        /// Gets an error message.
        /// </summary>
        /// <param name="name">The name of an entity.</param>
        /// <returns>The error message.</returns>
        internal static string InstrumentHasNoData(string name)
        {
            return $"{Prefix}: the specified instrument has no {name} data";
        }

        private static XmlReaderSettings CreateXmlReaderSettings()
        {
            return new XmlReaderSettings
            {
                CheckCharacters = false,
                CloseInput = true,
                ConformanceLevel = ConformanceLevel.Auto,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true,
                ValidationType = ValidationType.None,
            };
        }

        private static CsvInfo FindInRepository(string key)
        {
            string indexFile;
            lock (RepositoryIndexFileLock)
            {
                indexFile = repositoryIndexFile;
            }

            if (string.IsNullOrWhiteSpace(indexFile))
            {
                return null;
            }

            CsvInfo csvInfo = null;
            try
            {
                using XmlReader xmlReader = XmlReader.Create(indexFile, XmlReaderSettings);
                var xmlLineInfo = (IXmlLineInfo)xmlReader;
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element &&
                        xmlReader.LocalName == InstrumentElement)
                    {
                        string symbol = xmlReader.GetAttribute(SymbolAttribute);
                        if (string.IsNullOrEmpty(symbol))
                        {
                            Log.Error(MissingAttribute(SymbolAttribute, indexFile, xmlLineInfo.LineNumber));
                            continue;
                        }

                        string isin = xmlReader.GetAttribute(IsinAttribute);
                        string mic = xmlReader.GetAttribute(MicAttribute);
                        string key2 = Key(mic, symbol, isin);
                        if (key != key2)
                        {
                            continue;
                        }

                        string file = xmlReader.GetAttribute(FileAttribute);
                        if (string.IsNullOrEmpty(file))
                        {
                            Log.Error(MissingAttribute(FileAttribute, indexFile, xmlLineInfo.LineNumber));
                            continue;
                        }

                        if (!Path.IsPathRooted(file))
                        {
                            lock (RepositoryPathLock)
                            {
                                if (!string.IsNullOrWhiteSpace(repositoryPath))
                                {
                                    file = Path.Combine(repositoryPath, file);
                                }
                            }
                        }

                        file = Path.GetFullPath(file);
                        if (!File.Exists(file))
                        {
                            Log.Error(FileDoesNotExist(file, indexFile, xmlLineInfo.LineNumber));
                            break;
                        }

                        string csvColumnIndices = xmlReader.GetAttribute(CsvColumnIndicesAttribute);
                        if (string.IsNullOrWhiteSpace(csvColumnIndices))
                        {
                            Log.Error(MissingAttribute(CsvColumnIndicesAttribute, indexFile, xmlLineInfo.LineNumber));
                            break;
                        }

                        string csvDateTimeFormat = xmlReader.GetAttribute(CsvDateTimeFormatAttribute);
                        if (string.IsNullOrWhiteSpace(csvDateTimeFormat))
                        {
                            Log.Error(MissingAttribute(CsvDateTimeFormatAttribute, indexFile, xmlLineInfo.LineNumber));
                            break;
                        }

                        string csvTimeGranularity = xmlReader.GetAttribute(CsvTimeGranularityAttribute);
                        if (string.IsNullOrWhiteSpace(csvTimeGranularity))
                        {
                            Log.Error(MissingAttribute(CsvTimeGranularityAttribute, indexFile, xmlLineInfo.LineNumber));
                            break;
                        }

                        string csvSeparatorCharacter = xmlReader.GetAttribute(CsvSeparatorCharacterAttribute);
                        if (string.IsNullOrWhiteSpace(csvSeparatorCharacter))
                        {
                            Log.Error(MissingAttribute(CsvSeparatorCharacterAttribute, indexFile, xmlLineInfo.LineNumber));
                            break;
                        }

                        string csvCommentCharacter = xmlReader.GetAttribute(CsvCommentCharacterAttribute);
                        if (string.IsNullOrWhiteSpace(csvCommentCharacter))
                        {
                            Log.Error(MissingAttribute(CsvCommentCharacterAttribute, indexFile, xmlLineInfo.LineNumber));
                            break;
                        }

                        string csvIsAdjustedData = xmlReader.GetAttribute(CsvIsAdjustedDataAttribute);
                        csvInfo = new CsvInfo(
                            file,
                            csvColumnIndices,
                            csvDateTimeFormat,
                            csvTimeGranularity,
                            csvSeparatorCharacter,
                            csvCommentCharacter,
                            csvIsAdjustedData);
                        break;
                    }
                }
            }
            catch (XmlException e)
            {
                Log.Error($"{Prefix}: file {indexFile} XmlException: {e.Message}");
                csvInfo = null;
            }
            catch (IOException e)
            {
                Log.Error($"{Prefix}: file {indexFile} IOException: {e.Message}");
                csvInfo = null;
            }

            return csvInfo;
        }

        private static string Key(string mic, string symbol, string isin)
        {
            mic = mic == null ? string.Empty : mic.ToUpperInvariant();
            symbol = symbol == null ? string.Empty : symbol.ToUpperInvariant();
            isin = isin == null ? string.Empty : isin.ToUpperInvariant();
            return string.Concat(mic, Underscore, isin, Underscore, symbol);
        }

        private static string Key(Instrument instrument)
        {
            return Key(
                instrument.Exchange.Mic.ToString(),
                instrument.Symbol,
                instrument.GetSecurityIdAs(InstrumentSecurityIdSource.Isin));
        }

        private static InstrumentCsvInfo FindInstrument(Instrument instrument)
        {
            string key = Key(instrument);
            lock (CacheDictionaryLock)
            {
                if (InfoCacheDictionary.TryGetValue(key, out InstrumentCsvInfo instrumentCsvInfo))
                {
                    return instrumentCsvInfo;
                }
            }

            CsvInfo csvInfo = FindInRepository(key);
            if (csvInfo == null)
            {
                return null;
            }

            Add(instrument, csvInfo);
            lock (CacheDictionaryLock)
            {
                if (InfoCacheDictionary.TryGetValue(key, out InstrumentCsvInfo instrumentCsvInfo))
                {
                    return instrumentCsvInfo;
                }
            }

            return null;
        }

        private static IEnumerable<Ohlcv> EnumerateOhlcvFileAsync(CsvInfo csvInfo, CsvRequest csvRequest, CancellationToken cancellationToken)
        {
            bool hasVolume = csvInfo.HasVolume;
            bool hasHighLow = csvInfo.HasHighLow;
            char commentChar = csvInfo.CommentCharacter;
            string delimiter = csvInfo.SeparatorCharacter;

            long lineNumber = 0L;
            using var fileStream = File.Open(csvInfo.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var bufferedStream = new BufferedStream(fileStream);
            using var streamReader = new StreamReader(bufferedStream, Encoding.UTF8);
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Log.Warning($"{Prefix}: Cancellation requested in [{line}], line number {lineNumber}, file path {csvInfo.FilePath}");
                    break;
                }

                ++lineNumber;
                if (line.Length == 0 || line[0] == commentChar)
                {
                    continue;
                }

                var error = ParseOhlcvSpan(csvInfo, csvRequest, line, delimiter, hasVolume, hasHighLow, out Ohlcv ohlcv, out CsvEnumerationAction action);
                if (error == null)
                {
                    if (action == CsvEnumerationAction.Continue)
                    {
                        yield return ohlcv;
                    }
                    else if (action == CsvEnumerationAction.Break)
                    {
                        break;
                    }

                    continue;
                }

                Log.Error($"{Prefix}: {error} in [{line}], line number {lineNumber}, file path {csvInfo.FilePath}");
                break;
            }
        }

        private static string ParseOhlcvSpan(CsvInfo csvInfo, CsvRequest csvRequest, string line, string delimiter, bool hasVolume, bool hasHighLow, out Ohlcv ohlcv, out CsvEnumerationAction action)
        {
            ohlcv = null;
            action = CsvEnumerationAction.Continue;
            var lineSpan = line.AsSpan();
            var dateTimeFormatSpan = csvInfo.DateTimeFormat.AsSpan();
            var delimiterSpan = delimiter.AsSpan();
            int i = lineSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            if (i < 0)
            {
                return CannotFindDelimiter(delimiter);
            }

            var span = lineSpan.Slice(0, i);
            if (!DateTime.TryParseExact(span, dateTimeFormatSpan, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                return InvalidDateTimePart(span);
            }

            if (csvRequest.EndofdayClosingTime.HasValue)
            {
                dateTime = dateTime.Date.Add(csvRequest.EndofdayClosingTime.Value);
            }

            if (dateTime < csvRequest.StartDate)
            {
                action = CsvEnumerationAction.Skip;
                return null;
            }

            if (dateTime > csvRequest.EndDate)
            {
                action = CsvEnumerationAction.Break;
                return null;
            }

            var currentSpan = lineSpan.Slice(++i);
            i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            if (i < 0)
            {
                return CannotFindDelimiter(delimiter, currentSpan);
            }

            span = currentSpan.Slice(0, i);
            if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out double open, out _))
            {
                return InvalidPart("opening price", span, currentSpan);
            }

            double high = double.NaN, low = double.NaN;
            if (hasHighLow)
            {
                currentSpan = currentSpan.Slice(++i);
                i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
                if (i < 0)
                {
                    return CannotFindDelimiter(delimiter, currentSpan);
                }

                span = currentSpan.Slice(0, i);
                if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out high, out _))
                {
                    return InvalidPart("highest price", span, currentSpan);
                }

                currentSpan = currentSpan.Slice(++i);
                i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
                if (i < 0)
                {
                    return CannotFindDelimiter(delimiter, currentSpan);
                }

                span = currentSpan.Slice(0, i);
                if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out low, out _))
                {
                    return InvalidPart("lowest price", span, currentSpan);
                }
            }

            currentSpan = currentSpan.Slice(++i);
            i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            if (hasVolume)
            {
                if (i < 0)
                {
                    return CannotFindDelimiter(delimiter, currentSpan);
                }

                span = currentSpan.Slice(0, i);
            }
            else
            {
                span = i < 0 ? currentSpan : currentSpan.Slice(0, i);
            }

            if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out double close, out _))
            {
                return InvalidPart("closing price", span, currentSpan);
            }

            if (!hasHighLow)
            {
                low = Math.Min(open, close);
                high = Math.Max(open, close);
            }

            double volume = double.NaN;
            if (hasVolume)
            {
                currentSpan = currentSpan.Slice(++i);
                i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
                span = i < 0 ? currentSpan : currentSpan.Slice(0, i);
                if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out volume, out _))
                {
                    return InvalidPart("volume", span, currentSpan);
                }
            }

            ohlcv = new Ohlcv(dateTime, open, high, low, close, volume);
            return null;
        }

        private static IEnumerable<Scalar> EnumerateScalarFileAsync(CsvInfo csvInfo, CsvRequest csvRequest, CancellationToken cancellationToken)
        {
            char commentChar = csvInfo.CommentCharacter;
            string delimiter = csvInfo.SeparatorCharacter;

            long lineNumber = 0L;
            using var fileStream = File.Open(csvInfo.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var bufferedStream = new BufferedStream(fileStream);
            using var streamReader = new StreamReader(bufferedStream, Encoding.UTF8);
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Log.Warning($"{Prefix}: Cancellation requested in [{line}], line number {lineNumber}, file path {csvInfo.FilePath}");
                    break;
                }

                ++lineNumber;
                if (line.Length == 0 || line[0] == commentChar)
                {
                    continue;
                }

                var error = ParseScalarSpan(csvInfo, csvRequest, line, delimiter, out Scalar scalar, out CsvEnumerationAction action);
                if (error == null)
                {
                    if (action == CsvEnumerationAction.Continue)
                    {
                        yield return scalar;
                    }
                    else if (action == CsvEnumerationAction.Break)
                    {
                        break;
                    }

                    continue;
                }

                Log.Error($"{Prefix}: {error} in [{line}], line number {lineNumber}, file path {csvInfo.FilePath}");
                break;
            }
        }

        private static string ParseScalarSpan(CsvInfo csvInfo, CsvRequest csvRequest, string line, string delimiter, out Scalar scalar, out CsvEnumerationAction action)
        {
            scalar = null;
            action = CsvEnumerationAction.Continue;
            var lineSpan = line.AsSpan();
            var dateTimeFormatSpan = csvInfo.DateTimeFormat.AsSpan();
            var delimiterSpan = delimiter.AsSpan();
            int i = lineSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            if (i < 0)
            {
                return CannotFindDelimiter(delimiter);
            }

            var span = lineSpan.Slice(0, i);
            if (!DateTime.TryParseExact(span, dateTimeFormatSpan, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                return InvalidDateTimePart(span);
            }

            if (csvRequest.EndofdayClosingTime.HasValue)
            {
                dateTime = dateTime.Date.Add(csvRequest.EndofdayClosingTime.Value);
            }

            if (dateTime < csvRequest.StartDate)
            {
                action = CsvEnumerationAction.Skip;
                return null;
            }

            if (dateTime > csvRequest.EndDate)
            {
                action = CsvEnumerationAction.Break;
                return null;
            }

            var currentSpan = lineSpan.Slice(++i);
            i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            span = i < 0 ? currentSpan : currentSpan.Slice(0, i);
            if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out double value, out _))
            {
                return InvalidPart("value", span, currentSpan);
            }

            scalar = new Scalar(dateTime, value);
            return null;
        }

        private static IEnumerable<Trade> EnumerateTradeFileAsync(CsvInfo csvInfo, CsvRequest csvRequest, CancellationToken cancellationToken)
        {
            bool hasVolume = csvInfo.HasVolume;
            char commentChar = csvInfo.CommentCharacter;
            string delimiter = csvInfo.SeparatorCharacter;

            long lineNumber = 0L;
            using var fileStream = File.Open(csvInfo.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var bufferedStream = new BufferedStream(fileStream);
            using var streamReader = new StreamReader(bufferedStream, Encoding.UTF8);
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Log.Warning($"{Prefix}: Cancellation requested in [{line}], line number {lineNumber}, file path {csvInfo.FilePath}");
                    break;
                }

                ++lineNumber;
                if (line.Length == 0 || line[0] == commentChar)
                {
                    continue;
                }

                var error = ParseTradeSpan(csvInfo, csvRequest, line, delimiter, hasVolume, out Trade trade, out CsvEnumerationAction action);
                if (error == null)
                {
                    if (action == CsvEnumerationAction.Continue)
                    {
                        yield return trade;
                    }
                    else if (action == CsvEnumerationAction.Break)
                    {
                        break;
                    }

                    continue;
                }

                Log.Error($"{Prefix}: {error} in [{line}], line number {lineNumber}, file path {csvInfo.FilePath}");
                break;
            }
        }

        private static string ParseTradeSpan(CsvInfo csvInfo, CsvRequest csvRequest, string line, string delimiter, bool hasVolume, out Trade trade, out CsvEnumerationAction action)
        {
            trade = null;
            action = CsvEnumerationAction.Continue;
            var lineSpan = line.AsSpan();
            var dateTimeFormatSpan = csvInfo.DateTimeFormat.AsSpan();
            var delimiterSpan = delimiter.AsSpan();
            int i = lineSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            if (i < 0)
            {
                return CannotFindDelimiter(delimiter);
            }

            var span = lineSpan.Slice(0, i);
            if (!DateTime.TryParseExact(span, dateTimeFormatSpan, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                return InvalidDateTimePart(span);
            }

            if (dateTime < csvRequest.StartDate)
            {
                action = CsvEnumerationAction.Skip;
                return null;
            }

            if (dateTime > csvRequest.EndDate)
            {
                action = CsvEnumerationAction.Break;
                return null;
            }

            var currentSpan = lineSpan.Slice(++i);
            i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            if (hasVolume)
            {
                if (i < 0)
                {
                    return CannotFindDelimiter(delimiter, currentSpan);
                }

                span = currentSpan.Slice(0, i);
            }
            else
            {
                span = i < 0 ? currentSpan : currentSpan.Slice(0, i);
            }

            if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out double price, out _))
            {
                return InvalidPart("price", span, currentSpan);
            }

            double volume;
            if (hasVolume)
            {
                currentSpan = currentSpan.Slice(++i);
                i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
                span = i < 0 ? currentSpan : currentSpan.Slice(0, i);
                if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out volume, out _))
                {
                    return InvalidPart("volume", span, currentSpan);
                }
            }
            else
            {
                volume = double.NaN;
            }

            trade = new Trade(dateTime, price, volume);
            return null;
        }

        private static IEnumerable<Quote> EnumerateQuoteFileAsync(CsvInfo csvInfo, CsvRequest csvRequest, CancellationToken cancellationToken)
        {
            bool hasVolume = csvInfo.HasVolume;
            char commentChar = csvInfo.CommentCharacter;
            string delimiter = csvInfo.SeparatorCharacter;

            long lineNumber = 0L;
            using var fileStream = File.Open(csvInfo.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var bufferedStream = new BufferedStream(fileStream);
            using var streamReader = new StreamReader(bufferedStream, Encoding.UTF8);
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Log.Warning($"{Prefix}: Cancellation requested in [{line}], line number {lineNumber}, file path {csvInfo.FilePath}");
                    break;
                }

                ++lineNumber;
                if (line.Length == 0 || line[0] == commentChar)
                {
                    continue;
                }

                var error = ParseQuoteSpan(csvInfo, csvRequest, line, delimiter, hasVolume, out Quote quote, out CsvEnumerationAction action);
                if (error == null)
                {
                    if (action == CsvEnumerationAction.Continue)
                    {
                        yield return quote;
                    }
                    else if (action == CsvEnumerationAction.Break)
                    {
                        break;
                    }

                    continue;
                }

                Log.Error($"{Prefix}: {error} in [{line}], line number {lineNumber}, file path {csvInfo.FilePath}");
                break;
            }
        }

        private static string ParseQuoteSpan(CsvInfo csvInfo, CsvRequest csvRequest, string line, string delimiter, bool hasVolume, out Quote quote, out CsvEnumerationAction action)
        {
            quote = null;
            action = CsvEnumerationAction.Continue;
            var lineSpan = line.AsSpan();
            var dateTimeFormatSpan = csvInfo.DateTimeFormat.AsSpan();
            var delimiterSpan = delimiter.AsSpan();
            int i = lineSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            if (i < 0)
            {
                return CannotFindDelimiter(delimiter);
            }

            var span = lineSpan.Slice(0, i);
            if (!DateTime.TryParseExact(span, dateTimeFormatSpan, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                return InvalidDateTimePart(span);
            }

            if (csvRequest.EndofdayClosingTime.HasValue)
            {
                dateTime = dateTime.Date.Add(csvRequest.EndofdayClosingTime.Value);
            }

            if (dateTime < csvRequest.StartDate)
            {
                action = CsvEnumerationAction.Skip;
                return null;
            }

            if (dateTime > csvRequest.EndDate)
            {
                action = CsvEnumerationAction.Break;
                return null;
            }

            var currentSpan = lineSpan.Slice(++i);
            i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            if (i < 0)
            {
                return CannotFindDelimiter(delimiter, currentSpan);
            }

            span = currentSpan.Slice(0, i);
            if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out double askPrice, out _))
            {
                return InvalidPart("ask price", span, currentSpan);
            }

            currentSpan = currentSpan.Slice(++i);
            i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
            if (hasVolume)
            {
                if (i < 0)
                {
                    return CannotFindDelimiter(delimiter, currentSpan);
                }

                span = currentSpan.Slice(0, i);
            }
            else
            {
                span = i < 0 ? currentSpan : currentSpan.Slice(0, i);
            }

            if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out double bidPrice, out _))
            {
                return InvalidPart("bid price", span, currentSpan);
            }

            double askSize = double.NaN, bidSize = double.NaN;
            if (hasVolume)
            {
                currentSpan = currentSpan.Slice(++i);
                i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
                if (i < 0)
                {
                    return CannotFindDelimiter(delimiter, currentSpan);
                }

                span = currentSpan.Slice(0, i);
                if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out askSize, out _))
                {
                    return InvalidPart("ask size", span, currentSpan);
                }

                currentSpan = currentSpan.Slice(++i);
                i = currentSpan.IndexOf(delimiterSpan, StringComparison.Ordinal);
                span = i < 0 ? currentSpan : currentSpan.Slice(0, i);
                if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(span.ToArray()), out bidSize, out _))
                {
                    return InvalidPart("bid size", span, currentSpan);
                }
            }

            quote = new Quote(dateTime, bidPrice, bidSize, askPrice, askSize);
            return null;
        }

        private static string MissingAttribute(string attribute, string file, int line)
        {
            return $"{Prefix}: missing {attribute} attribute, file: {file}, line: {line}";
        }

        private static string FileDoesNotExist(string csvFile, string file, int line)
        {
            return $"{Prefix}: file {csvFile} does not exist, file: {file}, line: {line}";
        }

        private static string FileDoesNotExist(string csvFile)
        {
            return $"{Prefix}: file {csvFile} does not exist";
        }

        private static string MissingInstrument(Instrument instrument)
        {
            return $"{Prefix}: failed to find instrument {instrument?.Symbol}-{instrument?.SecurityId}@{instrument?.Exchange.Mic}";
        }

        private static string ErrorReadingFile(string file)
        {
            return $"{Prefix}: exception while reading file {file}";
        }

        private static string CannotFindDelimiter(string delimiter)
        {
            return $"Cannot find delimiter {delimiter}";
        }

        private static string CannotFindDelimiter(string delimiter, ReadOnlySpan<char> currentSpan)
        {
            return $"Cannot find delimiter {delimiter} in [{currentSpan.ToString()}]";
        }

        private static string InvalidDateTimePart(ReadOnlySpan<char> span)
        {
            return $"Invalid date-time part [{span.ToString()}]";
        }

        private static string InvalidPart(string name, ReadOnlySpan<char> span, ReadOnlySpan<char> currentSpan)
        {
            return $"Invalid {name} [{span.ToString()}] in [{currentSpan.ToString()}]";
        }
    }
}
