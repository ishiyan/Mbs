using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    /// <summary>
    /// Provides an access to the historical data stored in CSV files.
    /// </summary>
    public static class CsvHistoricalData
    {
        /// <summary>
        /// The data provider name.
        /// </summary>
        internal const string Provider = "Csv";

        private const string Prefix = "CsvHistoricalData";

        private static readonly object CacheDictionaryLock = new object();
        private static readonly Dictionary<string, InstrumentCsvInfo> InfoCacheDictionary = new Dictionary<string, InstrumentCsvInfo>();

        /// <summary>
        /// Adds an instrument with an associated csv info to the repository.
        /// </summary>
        /// <param name="instrument">The instrument to add.</param>
        /// <param name="csvInfo">The csv info to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        public static void Add(Instrument instrument, CsvInfo csvInfo)
        {
            string file = csvInfo.FilePath;
            if (string.IsNullOrWhiteSpace(file) || !File.Exists(file))
            {
                Log.Error(FileDoesNotExist(file));
                return;
            }

            string key = Key(instrument);
            lock (CacheDictionaryLock)
            {
                if (!InfoCacheDictionary.TryGetValue(key, out var instrumentCsvInfo))
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
        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        internal static InstrumentCsvInfo InstrumentInfo(Instrument instrument)
        {
            string key = Key(instrument);
            InstrumentCsvInfo instrumentInfo;
            lock (CacheDictionaryLock)
            {
                instrumentInfo = InfoCacheDictionary.TryGetValue(key, out var instrumentCsvInfo)
                    ? instrumentCsvInfo
                    : null;
            }

            if (instrumentInfo == null)
            {
                Log.Error(MissingInstrument(key));
            }

            return instrumentInfo;
        }

        /// <summary>
        /// Asynchronously enumerates <see cref="Ohlcv"/> elements.
        /// </summary>
        /// <param name="csvInfo">The CSV info.</param>
        /// <param name="csvRequest">The CSV request.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An asynchronous enumerable interface.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        internal static async IAsyncEnumerable<Ohlcv> EnumerateOhlcvAsync(CsvInfo csvInfo, CsvRequest csvRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var hasEndofdayClosingTime = csvRequest.EndofdayClosingTime.HasValue;
            var info = new Info(csvInfo, csvRequest);
            long lineNumber = 0;

            await using var fileStream = File.OpenRead(csvInfo.FilePath);
            var pipeReader = PipeReader.Create(fileStream);
            while (true)
            {
                ReadResult read = await pipeReader.ReadAsync(cancellationToken);
                ReadOnlySequence<byte> buffer = read.Buffer;
                while (TryReadSequence(ref buffer, out ReadOnlySequence<byte> sequence))
                {
                    ++lineNumber;
                    var errorMessage = ProcessSequence(sequence, info, out Ohlcv ohlcv);
                    if (errorMessage == null)
                    {
                        if (ohlcv != null)
                        {
                            if (hasEndofdayClosingTime)
                            {
                                ohlcv.Time = ohlcv.Time.Date.Add(csvRequest.EndofdayClosingTime.Value);
                            }

                            yield return ohlcv;
                        }
                    }
                    else if (errorMessage.Length == 0)
                    {
                        yield break;
                    }
                    else
                    {
                        errorMessage = $"{Prefix}: file {csvInfo.FilePath}, line {lineNumber}: {errorMessage}, aborted";
                        Log.Error(errorMessage);
                        throw new AggregateException(errorMessage);
                    }
                }

                pipeReader.AdvanceTo(buffer.Start, buffer.End);
                if (read.IsCompleted)
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// Asynchronously enumerates <see cref="Scalar"/> elements.
        /// </summary>
        /// <param name="csvInfo">The CSV info.</param>
        /// <param name="csvRequest">The CSV request.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An asynchronous enumerable interface.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        internal static async IAsyncEnumerable<Scalar> EnumerateScalarAsync(CsvInfo csvInfo, CsvRequest csvRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var info = new Info(csvInfo, csvRequest);
            long lineNumber = 0;

            await using var fileStream = File.OpenRead(csvInfo.FilePath);
            var pipeReader = PipeReader.Create(fileStream);
            while (true)
            {
                ReadResult read = await pipeReader.ReadAsync(cancellationToken);
                ReadOnlySequence<byte> buffer = read.Buffer;
                while (TryReadSequence(ref buffer, out ReadOnlySequence<byte> sequence))
                {
                    ++lineNumber;
                    var errorMessage = ProcessSequence(sequence, info, out Scalar scalar);
                    if (errorMessage == null)
                    {
                        if (scalar != null)
                        {
                            yield return scalar;
                        }
                    }
                    else if (errorMessage.Length == 0)
                    {
                        yield break;
                    }
                    else
                    {
                        errorMessage = $"{Prefix}: file {csvInfo.FilePath}, line {lineNumber}: {errorMessage}, aborted";
                        Log.Error(errorMessage);
                        throw new AggregateException(errorMessage);
                    }
                }

                pipeReader.AdvanceTo(buffer.Start, buffer.End);
                if (read.IsCompleted)
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// Asynchronously enumerates <see cref="Trade"/> elements.
        /// </summary>
        /// <param name="csvInfo">The CSV info.</param>
        /// <param name="csvRequest">The CSV request.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An asynchronous enumerable interface.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        internal static async IAsyncEnumerable<Trade> EnumerateTradeAsync(CsvInfo csvInfo, CsvRequest csvRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var info = new Info(csvInfo, csvRequest);
            long lineNumber = 0;

            await using var fileStream = File.OpenRead(csvInfo.FilePath);
            var pipeReader = PipeReader.Create(fileStream);
            while (true)
            {
                ReadResult read = await pipeReader.ReadAsync(cancellationToken);
                ReadOnlySequence<byte> buffer = read.Buffer;
                while (TryReadSequence(ref buffer, out ReadOnlySequence<byte> sequence))
                {
                    ++lineNumber;
                    var errorMessage = ProcessSequence(sequence, info, out Trade trade);
                    if (errorMessage == null)
                    {
                        if (trade != null)
                        {
                            yield return trade;
                        }
                    }
                    else if (errorMessage.Length == 0)
                    {
                        yield break;
                    }
                    else
                    {
                        errorMessage = $"{Prefix}: file {csvInfo.FilePath}, line {lineNumber}: {errorMessage}, aborted";
                        Log.Error(errorMessage);
                        throw new AggregateException(errorMessage);
                    }
                }

                pipeReader.AdvanceTo(buffer.Start, buffer.End);
                if (read.IsCompleted)
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// Asynchronously enumerates <see cref="Quote"/> elements.
        /// </summary>
        /// <param name="csvInfo">The CSV info.</param>
        /// <param name="csvRequest">The CSV request.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An asynchronous enumerable interface.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        internal static async IAsyncEnumerable<Quote> EnumerateQuoteAsync(CsvInfo csvInfo, CsvRequest csvRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var info = new Info(csvInfo, csvRequest);
            long lineNumber = 0;

            await using var fileStream = File.OpenRead(csvInfo.FilePath);
            var pipeReader = PipeReader.Create(fileStream);
            while (true)
            {
                ReadResult read = await pipeReader.ReadAsync(cancellationToken);
                ReadOnlySequence<byte> buffer = read.Buffer;
                while (TryReadSequence(ref buffer, out ReadOnlySequence<byte> sequence))
                {
                    ++lineNumber;
                    var errorMessage = ProcessSequence(sequence, info, out Quote quote);
                    if (errorMessage == null)
                    {
                        if (quote != null)
                        {
                            yield return quote;
                        }
                    }
                    else if (errorMessage.Length == 0)
                    {
                        yield break;
                    }
                    else
                    {
                        errorMessage = $"{Prefix}: file {csvInfo.FilePath}, line {lineNumber}: {errorMessage}, aborted";
                        Log.Error(errorMessage);
                        throw new AggregateException(errorMessage);
                    }
                }

                pipeReader.AdvanceTo(buffer.Start, buffer.End);
                if (read.IsCompleted)
                {
                    yield break;
                }
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

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string Key(string mic, string symbol, string isin)
        {
            isin = isin?.ToUpperInvariant() ?? string.Empty;
            symbol = symbol?.ToUpperInvariant() ?? string.Empty;
            mic = mic?.ToUpperInvariant() ?? string.Empty;

            return string.Concat(symbol, "-", isin, "@", mic);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string Key(Instrument instrument)
        {
            return Key(
                instrument.Exchange.Mic.ToString(),
                instrument.Symbol,
                instrument.GetSecurityIdAs(InstrumentSecurityIdSource.Isin));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static bool TryReadSequence(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> sequence)
        {
            var position = buffer.PositionOf((byte)'\n');
            if (position == null)
            {
                sequence = default;
                return false;
            }

            sequence = buffer.Slice(0, position.Value);
            buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string ProcessSequence(ReadOnlySequence<byte> sequence, Info info, out Ohlcv ohlcv)
        {
            if (sequence.IsSingleSegment)
            {
                return Parse(sequence.FirstSpan, info, out ohlcv);
            }

            const int lengthLimit = 5120;
            var length = sequence.Length;
            if (length < lengthLimit)
            {
                Span<byte> span = stackalloc byte[(int)sequence.Length];
                sequence.CopyTo(span);
                return Parse(span, info, out ohlcv);
            }

            var buffer = ArrayPool<byte>.Shared.Rent((int)length);
            sequence.CopyTo(buffer);
            var errorCode = Parse(buffer, info, out ohlcv);
            ArrayPool<byte>.Shared.Return(buffer);
            return errorCode;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string ProcessSequence(ReadOnlySequence<byte> sequence, Info info, out Scalar scalar)
        {
            if (sequence.IsSingleSegment)
            {
                return Parse(sequence.FirstSpan, info, out scalar);
            }

            const int lengthLimit = 5120;
            var length = sequence.Length;
            if (length < lengthLimit)
            {
                Span<byte> span = stackalloc byte[(int)sequence.Length];
                sequence.CopyTo(span);
                return Parse(span, info, out scalar);
            }

            var buffer = ArrayPool<byte>.Shared.Rent((int)length);
            sequence.CopyTo(buffer);
            var errorCode = Parse(buffer, info, out scalar);
            ArrayPool<byte>.Shared.Return(buffer);
            return errorCode;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string ProcessSequence(ReadOnlySequence<byte> sequence, Info info, out Trade trade)
        {
            if (sequence.IsSingleSegment)
            {
                return Parse(sequence.FirstSpan, info, out trade);
            }

            const int lengthLimit = 5120;
            var length = sequence.Length;
            if (length < lengthLimit)
            {
                Span<byte> span = stackalloc byte[(int)sequence.Length];
                sequence.CopyTo(span);
                return Parse(span, info, out trade);
            }

            var buffer = ArrayPool<byte>.Shared.Rent((int)length);
            sequence.CopyTo(buffer);
            var errorCode = Parse(buffer, info, out trade);
            ArrayPool<byte>.Shared.Return(buffer);
            return errorCode;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string ProcessSequence(ReadOnlySequence<byte> sequence, Info info, out Quote quote)
        {
            if (sequence.IsSingleSegment)
            {
                return Parse(sequence.FirstSpan, info, out quote);
            }

            const int lengthLimit = 5120;
            var length = sequence.Length;
            if (length < lengthLimit)
            {
                Span<byte> span = stackalloc byte[(int)sequence.Length];
                sequence.CopyTo(span);
                return Parse(span, info, out quote);
            }

            var buffer = ArrayPool<byte>.Shared.Rent((int)length);
            sequence.CopyTo(buffer);
            var errorCode = Parse(buffer, info, out quote);
            ArrayPool<byte>.Shared.Return(buffer);
            return errorCode;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static ReadOnlySpan<byte> ParseChunk(byte delimiter, ref ReadOnlySpan<byte> span, ref int scanned, ref int position)
        {
            scanned += position + 1;

            position = span[scanned..].IndexOf(delimiter);
            if (position < 0)
            {
                var sp = span[scanned..];
                position = sp.Length;

                // The last chunk in a line might be terminated by the '\r'.
                return (!sp.IsEmpty && sp[^1] == (byte)'\r')
                    ? sp[..^1]
                    : sp;
            }

            return span.Slice(scanned, position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string Parse(ReadOnlySpan<byte> bytes, Info info, out Ohlcv ohlcv)
        {
            ohlcv = null;

            // Empty line might contain '\r'.
            if (bytes.Length < 2 || bytes[0] == info.Comment)
            {
                return null;
            }

            int scanned = -1;
            int position = 0;

            var span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
            string errorMessage = TryParseDateTime(span, info.TimeFormat, out DateTime dateTime, bytes);
            if (errorMessage != null)
            {
                return errorMessage;
            }

            if (dateTime < info.StartDateInclusive)
            {
                return null;
            }

            if (dateTime > info.EndDateInclusive)
            {
                return string.Empty;
            }

            span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
            errorMessage = TryParseDouble(span, "opening price", out double open, bytes);
            if (errorMessage != null)
            {
                return errorMessage;
            }

            double high, low, close, volume;
            if (info.HasHighLow)
            {
                span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
                errorMessage = TryParseDouble(span, "highest price", out high, bytes);
                if (errorMessage != null)
                {
                    return errorMessage;
                }

                span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
                errorMessage = TryParseDouble(span, "lowest price", out low, bytes);
                if (errorMessage != null)
                {
                    return errorMessage;
                }

                span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
                errorMessage = TryParseDouble(span, "closing price", out close, bytes);
                if (errorMessage != null)
                {
                    return errorMessage;
                }
            }
            else
            {
                span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
                errorMessage = TryParseDouble(span, "closing price", out close, bytes);
                if (errorMessage != null)
                {
                    return errorMessage;
                }

                high = Math.Max(open, close);
                low = Math.Min(open, close);
            }

            if (info.HasVolume)
            {
                span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
                errorMessage = TryParseDouble(span, "volume", out volume, bytes);
                if (errorMessage != null)
                {
                    return errorMessage;
                }
            }
            else
            {
                volume = 0;
            }

            ohlcv = new Ohlcv(dateTime, open, high, low, close, volume);
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string Parse(ReadOnlySpan<byte> bytes, Info info, out Scalar scalar)
        {
            scalar = null;

            // Empty line might contain '\r'.
            if (bytes.Length < 2 || bytes[0] == info.Comment)
            {
                return null;
            }

            int scanned = -1;
            int position = 0;

            var span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
            string errorMessage = TryParseDateTime(span, info.TimeFormat, out DateTime dateTime, bytes);
            if (errorMessage != null)
            {
                return errorMessage;
            }

            if (dateTime < info.StartDateInclusive)
            {
                return null;
            }

            if (dateTime > info.EndDateInclusive)
            {
                return string.Empty;
            }

            span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
            errorMessage = TryParseDouble(span, "scalar", out double value, bytes);
            if (errorMessage != null)
            {
                return errorMessage;
            }

            scalar = new Scalar(dateTime, value);
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string Parse(ReadOnlySpan<byte> bytes, Info info, out Trade trade)
        {
            trade = null;

            // Empty line might contain '\r'.
            if (bytes.Length < 2 || bytes[0] == info.Comment)
            {
                return null;
            }

            int scanned = -1;
            int position = 0;

            var span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
            string errorMessage = TryParseDateTime(span, info.TimeFormat, out DateTime dateTime, bytes);
            if (errorMessage != null)
            {
                return errorMessage;
            }

            if (dateTime < info.StartDateInclusive)
            {
                return null;
            }

            if (dateTime > info.EndDateInclusive)
            {
                return string.Empty;
            }

            span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
            errorMessage = TryParseDouble(span, "price", out double price, bytes);
            if (errorMessage != null)
            {
                return errorMessage;
            }

            double volume;
            if (info.HasVolume)
            {
                span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
                errorMessage = TryParseDouble(span, "volume", out volume, bytes);
                if (errorMessage != null)
                {
                    return errorMessage;
                }
            }
            else
            {
                volume = 0;
            }

            trade = new Trade(dateTime, price, volume);
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string Parse(ReadOnlySpan<byte> bytes, Info info, out Quote quote)
        {
            quote = null;

            // Empty line might contain '\r'.
            if (bytes.Length < 2 || bytes[0] == info.Comment)
            {
                return null;
            }

            int scanned = -1;
            int position = 0;

            var span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
            string errorMessage = TryParseDateTime(span, info.TimeFormat, out DateTime dateTime, bytes);
            if (errorMessage != null)
            {
                return errorMessage;
            }

            if (dateTime < info.StartDateInclusive)
            {
                return null;
            }

            if (dateTime > info.EndDateInclusive)
            {
                return string.Empty;
            }

            span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
            errorMessage = TryParseDouble(span, "ask price", out double askPrice, bytes);
            if (errorMessage != null)
            {
                return errorMessage;
            }

            span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
            errorMessage = TryParseDouble(span, "bid price", out double bidPrice, bytes);
            if (errorMessage != null)
            {
                return errorMessage;
            }

            double askSize, bidSize;
            if (info.HasVolume)
            {
                span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
                errorMessage = TryParseDouble(span, "ask size", out askSize, bytes);
                if (errorMessage != null)
                {
                    return errorMessage;
                }

                span = ParseChunk(info.Delimiter, ref bytes, ref scanned, ref position);
                errorMessage = TryParseDouble(span, "bid size", out bidSize, bytes);
                if (errorMessage != null)
                {
                    return errorMessage;
                }
            }
            else
            {
                askSize = 0;
                bidSize = 0;
            }

            quote = new Quote(dateTime, bidPrice, bidSize, askPrice, askSize);
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string TryParseDateTime(ReadOnlySpan<byte> span, string timeFormat, out DateTime dateTime, ReadOnlySpan<byte> line)
        {
            if (!span.IsEmpty)
            {
                Span<char> chars = stackalloc char[span.Length];
                Encoding.UTF8.GetChars(span, chars);
                if (DateTime.TryParseExact(chars, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    return null;
                }
            }

            dateTime = new DateTime(0L);
            return $"Invalid date-time [{timeFormat}] part [{ByteSpanToString(span)}] in [{ByteSpanToString(line)}]";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static string TryParseDouble(ReadOnlySpan<byte> span, string what, out double value, ReadOnlySpan<byte> line)
        {
            // 18.g29 => 18
            // if (!span.IsEmpty && Utf8Parser.TryParse(span, out value, out _, 'f')) return null
            if (!span.IsEmpty && (value = ParsePositiveDouble(span)) >= 0)
            {
                return null;
            }

            value = ParsePositiveDouble(span);
            return value < 0 ? $"Invalid {what} part [{ByteSpanToString(span)}] in [{ByteSpanToString(line)}]" : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining & MethodImplOptions.AggressiveOptimization)]
        private static double ParsePositiveDouble(ReadOnlySpan<byte> span)
        {
            if (span.IsEmpty)
            {
                return -1;
            }

            double v = 0;
            double factor = 1;
            bool pointSeen = false;

            foreach (var b in span)
            {
                if (b == (byte)'-')
                {
                    if (span[0] != b)
                    {
                        return -1;
                    }

                    factor *= -1;
                    continue;
                }

                if (b == (byte)'.')
                {
                    if (pointSeen)
                    {
                        return -1;
                    }

                    pointSeen = true;
                    continue;
                }

                if (b < (byte)'0' || b > (byte)'9')
                {
                    return -1;
                }

                v = v * 10 + b - (byte)'0';
                if (pointSeen)
                {
                    factor /= 10;
                }
            }

            return v * factor;
        }

        private static string ByteSpanToString(ReadOnlySpan<byte> span)
        {
            if (span.IsEmpty)
            {
                return string.Empty;
            }

            Span<char> chars = stackalloc char[span.Length];
            Encoding.UTF8.GetChars(span, chars);
            return chars.ToString();
        }

        private static string FileDoesNotExist(string csvFile)
        {
            return $"{Prefix}: file {csvFile} does not exist";
        }

        private static string MissingInstrument(string key)
        {
            return $"{Prefix}: failed to find instrument {key}";
        }

        private class Info
        {
            public Info(CsvInfo csvInfo, CsvRequest csvRequest)
            {
                Comment = csvInfo.HasCommentCharacter ? (byte)csvInfo.CommentCharacter : (byte)0;
                Delimiter = (byte)csvInfo.SeparatorCharacter[0];
                TimeFormat = csvInfo.DateTimeFormat ?? CsvInfo.DefaultDateTimeFormat;
                StartDateInclusive = csvRequest.StartDateTime;
                EndDateInclusive = csvRequest.EndDateTime;
                HasVolume = csvInfo.HasVolume;
                HasHighLow = csvInfo.HasHighLow;
            }

            public byte Comment { get; }

            public byte Delimiter { get; }

            public string TimeFormat { get; }

            public DateTime StartDateInclusive { get; }

            public DateTime EndDateInclusive { get; }

            public bool HasVolume { get; }

            public bool HasHighLow { get; }
        }
    }
}
