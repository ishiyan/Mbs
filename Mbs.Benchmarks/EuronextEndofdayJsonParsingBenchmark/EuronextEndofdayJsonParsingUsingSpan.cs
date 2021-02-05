using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Utilities;

// ReSharper disable once CheckNamespace
namespace EuronextEndofdayJsonParsing
{
    public static class EuronextEndofdayJsonParsingUsingSpan
    {
        private const int DefaultEndofdayClosingHour = 19;
        private const int DefaultEndofdayClosingMinute = 0;
        private const int DefaultEndofdayClosingSecond = 0;
        private const string Prefix = "EuronextEndofday";

        private static readonly TimeSpan DefaultEndofdayClosingTime =
            TimeSpan.FromSeconds(DefaultEndofdayClosingSecond + DefaultEndofdayClosingMinute * 60 + DefaultEndofdayClosingHour * 3600);

        private static readonly EntryInfo[] Entries =
        {
            new EntryInfo("open", 7, 8, @"open"":"""),
            new EntryInfo("high", 7, 8, @"high"":"""),
            new EntryInfo("low", 6, 7, @"low"":"""),
            new EntryInfo("close", 8, 9, @"close"":"""),
        };

        public static bool ParseJsonArray(string json, TimeSpan? endofdayClosingTime, string isin, List<Ohlcv> list)
        {
            var arrayDelimiterSpan = "},{".AsSpan();
            var arrayTrailerSpan = "}]".AsSpan();

            int i = json.IndexOf("[{", StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error($"{Prefix}: no endofday json data found in <{json}>, skipping.");
                return false;
            }

            var span = json.AsSpan()[(i + 2) ..];
            Ohlcv ohlcv;
            while ((i = span.IndexOf(arrayDelimiterSpan, StringComparison.Ordinal)) >= 0)
            {
                if ((ohlcv = ParseJsonSpan(span.Slice(0, i), endofdayClosingTime, isin)) != null)
                {
                    list.Add(ohlcv);
                }

                span = span[(i + 3) ..];
            }

            i = span.IndexOf(arrayTrailerSpan, StringComparison.Ordinal);
            if ((ohlcv = ParseJsonSpan(span.Slice(0, i), endofdayClosingTime, isin)) != null)
            {
                list.Add(ohlcv);
            }

            return true;
        }

        private static bool TryParseDoubleSpan(ReadOnlySpan<char> str, EntryInfo entryInfo, out double value, out int i)
        {
            i = str.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error(
                    $"{Prefix}: invalid endofday json: invalid [{entryInfo.Name}] splitted entry in [{str.ToString()}], skipping.");
                value = double.NaN;
                return false;
            }

            var entry = str.Slice(0, i);
            if (!entry.StartsWith(entryInfo.PatternArray, StringComparison.Ordinal) ||
                entry[^1] != '\"' || entry.Contains(EntryInfo.NullArray, StringComparison.Ordinal))
            {
                Log.Error(
                    $"{Prefix}: invalid endofday json: invalid [{entryInfo.Name}] splitted entry [{entry.ToString()}] in [{str.ToString()}], skipping.");
                value = double.NaN;
                return false;
            }

            var number = entry.Slice(entryInfo.Offset, entry.Length - entryInfo.LengthSubtractor).ToString();
            if (!double.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out value))
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [{entryInfo.Name}] [{number}] in [{entry.ToString()}] in [{str.ToString()}], skipping.");
                value = double.NaN;
                return false;
            }

            return true;
        }

        private static bool TryParseVolumeSpan(ReadOnlySpan<char> str, out double value, out int i)
        {
            i = str.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error(
                    $"{Prefix}: invalid endofday json: invalid [numberOfShares] splitted entry in [{str.ToString()}], skipping.");
                value = double.NaN;
                return false;
            }

            // ReSharper disable once CommentTypo
            // nymberofshares":"1,118.00"
            // ReSharper disable once CommentTypo
            // numberofshares":"0,00"
            //           1111111111222222
            // 01234567890123456789012345
            var entry = str.Slice(0, i);
            if (!(entry.StartsWith(EntryInfo.VolumePatternArray1, StringComparison.Ordinal) || entry.StartsWith(EntryInfo.VolumePatternArray2, StringComparison.Ordinal))
                || entry[^1] != '\"'
                || entry.Contains(EntryInfo.NullArray, StringComparison.Ordinal))
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [numberOfShares] splitted entry [{entry.ToString()}] in [{str.ToString()}], skipping.");
                value = double.NaN;
                return false;
            }

            var number = entry[17..^1]; // 1,118.00 // 0,00
            if (!Utf8Parser.TryParse(Encoding.UTF8.GetBytes(number.ToArray()), out value, out _))
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [numberOfShares] [{number.ToString()}] in [{entry.ToString()}] in [{str.ToString()}], skipping.");
                value = double.NaN;
                return false;
            }

            return true;
        }

        private static bool TryParseDateSpan(ReadOnlySpan<char> str, TimeSpan? endofdayClosingTime, out DateTime value, out int i)
        {
            i = str.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error(
                    $"{Prefix}: invalid endofday json: invalid [date] splitted entry in [{str.ToString()}], skipping.");
                value = DateTime.MinValue;
                return false;
            }

            // date":"29\/08\/2012"
            //           1111111111
            // 01234567890123456789
            if (!str.StartsWith(EntryInfo.DatePatternArray, StringComparison.Ordinal)
                || str.Length < 22
                || str[9] != '\\'
                || str[10] != '/'
                || str[13] != '\\'
                || str[14] != '/'
                || str[19] != '"')
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [date] splitted entry in [{str.ToString()}], skipping.");
                value = DateTime.MinValue;
                return false;
            }

            int day = 10 * (str[7] - '0') + (str[8] - '0');
            int month = 10 * (str[11] - '0') + (str[12] - '0');
            int year = 1000 * (str[15] - '0') + 100 * (str[16] - '0') + 10 * (str[17] - '0') + (str[18] - '0');
            value = new DateTime(year, month, day, 0, 0, 0).Add(endofdayClosingTime ?? DefaultEndofdayClosingTime);
            return true;
        }

        private static bool TryParseIsinSpan(ReadOnlySpan<char> str, string isin, out int i)
        {
            i = str.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [ISIN] splitted entry in [{new string(str.ToArray())}], skipping.");
                return false;
            }

            // "ISIN":"FR0010930636"
            //           11111111112
            // 012345678901234567890
            var entry = str.Slice(0, i);
            if (!entry.StartsWith(EntryInfo.IsinPatternArray, StringComparison.Ordinal) || entry[^1] != '\"')
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [ISIN] splitted entry [{new string(entry.ToArray())}] in [{new string(str.ToArray())}], skipping.");
                return false;
            }

            var value = entry[8..^1]; // FR0010930636
            if (!value.Equals(isin.AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                Log.Error($"{Prefix}: invalid endofday json: ISIN in instrument context [{isin}] differs from [ISIN] entry [{new string(value.ToArray())}] in [{new string(entry.ToArray())}] in [{new string(str.ToArray())}], skipping.");
                return false;
            }

            return true;
        }

        private static Ohlcv ParseJsonSpan(ReadOnlySpan<char> str, TimeSpan? endofdayClosingTime, string isin)
        {
            // ReSharper disable CommentTypo
            // "ISIN":"FR0010533075","MIC":"Euronext Paris, London","date":"29\/08\/2012","open":"5.85","high":"5.918","low":"5.84","close":"5.893","nymberofshares":"589,993","numoftrades":"1,467","turnover":"3,465,442.27","currency":"EUR"
            // ReSharper restore CommentTypo
            // "ISIN":"FR0010930636"
            //           11111111112
            // 012345678901234567890
            if (!TryParseIsinSpan(str, isin, out int i))
            {
                return null;
            }

            // MIC":"Euronext Paris, London"
            //           1111111111222222222
            // 01234567890123456789012345678
            str = str[(i + EntryInfo.DelimiterArray.Length) ..];
            i = str.IndexOf(EntryInfo.DelimiterArray, StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error(
                    $"{Prefix}: invalid endofday json: invalid [MIC] splitted entry in [{new string(str.ToArray())}], skipping.");
                return null;
            }

            // date":"29\/08\/2012"
            //           1111111111
            // 01234567890123456789
            str = str[(i + EntryInfo.DelimiterArray.Length) ..];
            if (!TryParseDateSpan(str, endofdayClosingTime, out DateTime dateTime, out i))
            {
                return null;
            }

            // open":"1,329.39"
            //           111111
            // 0123456789012345
            str = str[(i + EntryInfo.DelimiterArray.Length) ..];
            if (!TryParseDoubleSpan(str, Entries[0], out double open, out i))
            {
                // 1,329.39
                return null;
            }

            // high":"1,329.39"
            //           11111
            // 012345678901234
            str = str[(i + EntryInfo.DelimiterArray.Length) ..];
            if (!TryParseDoubleSpan(str, Entries[1], out double high, out i))
            {
                return null;
            }

            // low":"1,329.39"
            //           11111
            // 012345678902345
            str = str[(i + EntryInfo.DelimiterArray.Length) ..];
            if (!TryParseDoubleSpan(str, Entries[2], out double low, out i))
            {
                return null;
            }

            // close":"1,329.39"
            //           1111111
            // 01234567890123456
            str = str[(i + EntryInfo.DelimiterArray.Length) ..];
            if (!TryParseDoubleSpan(str, Entries[3], out double close, out i))
            {
                return null;
            }

            // ReSharper disable once CommentTypo
            // nymberofshares":"1,118.00"
            // ReSharper disable once CommentTypo
            // numberofshares":"0,00"
            //           111111111122
            // 0123456789012345678901
            str = str[(i + EntryInfo.DelimiterArray.Length) ..];
            if (!TryParseVolumeSpan(str, out double volume, out _))
            {
                return null;
            }

            return new Ohlcv(dateTime, open, high, low, close, volume);
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

            public EntryInfo(string name, int offset, int lengthSubtractor, string array)
            {
                Name = name;
                Offset = offset;
                LengthSubtractor = lengthSubtractor;
                PatternArray = array.ToCharArray();
            }

            public string Name { get; }

            public int Offset { get; }

            public int LengthSubtractor { get; }

            public char[] PatternArray { get; }
        }
    }
}
