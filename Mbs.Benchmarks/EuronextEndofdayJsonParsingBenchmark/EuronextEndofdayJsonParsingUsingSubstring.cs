using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Mbs.Trading.Data;
using Mbs.Utilities;

// ReSharper disable once CheckNamespace
namespace EuronextEndofdayJsonParsing
{
    public static class EuronextEndofdayJsonParsingUsingSubstring
    {
        private const int DefaultEndofdayClosingHour = 19;
        private const int DefaultEndofdayClosingMinute = 0;
        private const int DefaultEndofdayClosingSecond = 0;
        private const string Prefix = "EuronextEndofday";

        private static readonly TimeSpan DefaultEndofdayClosingTime =
            TimeSpan.FromSeconds(DefaultEndofdayClosingSecond + DefaultEndofdayClosingMinute * 60 +
                                 DefaultEndofdayClosingHour * 3600);

        public static bool ParseJsonArray(string json, TimeSpan? endofdayClosingTime, string isin, List<Ohlcv> list)
        {
            int i = json.IndexOf("[{", StringComparison.Ordinal);
            if (i < 0)
            {
                Log.Error($"{Prefix}: no endofday json data found in <{json}>, skipping.");
                return false;
            }

            Ohlcv ohlcv;
            json = json.Substring(i + 2);
            while ((i = json.IndexOf("},{", StringComparison.Ordinal)) >= 0)
            {
                if ((ohlcv = ParseJson(json.Substring(0, i), endofdayClosingTime, isin)) != null)
                {
                    list.Add(ohlcv);
                }

                json = json.Substring(i + 3);
            }

            i = json.IndexOf("}]", StringComparison.Ordinal);
            if ((ohlcv = ParseJson(json.Substring(0, i), endofdayClosingTime, isin)) != null)
            {
                list.Add(ohlcv);
            }

            return true;
        }

        private static Ohlcv ParseJson(string str, TimeSpan? endofdayClosingTime, string isin)
        {
            // ReSharper disable CommentTypo
            // "ISIN":"FR0010533075","MIC":"Euronext Paris, London","date":"29\/08\/2012","open":"5.85","high":"5.918","low":"5.84","close":"5.893","nymberofshares":"589,993","numoftrades":"1,467","turnover":"3,465,442.27","currency":"EUR"
            // ReSharper restore CommentTypo
            string[] splitted = Regex.Split(str, @",""");
            if (splitted.Length <= 7)
            {
                Log.Error($"{Prefix}: invalid endofday json: illegal number of splitted entries in [{str}], skipping.");
                return null;
            }

            string entry = splitted[0];

            // "ISIN":"FR0010930636"
            //           11111111112
            // 012345678901234567890
            if (!entry.ToUpperInvariant().StartsWith(@"""ISIN"":""", StringComparison.Ordinal) || entry[^1] != '\"')
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [ISIN] splitted entry [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = entry.Substring(8, entry.Length - 9); // FR0010930636
            if (!string.Equals(entry, isin, StringComparison.InvariantCultureIgnoreCase))
            {
                Log.Error($"{Prefix}: invalid endofday json: ISIN in instrument context [{isin}] differs from [ISIN] entry [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = splitted[2].Replace(" ", string.Empty, StringComparison.Ordinal);

            // date":"29\/08\/2012"
            //           1111111111
            // 01234567890123456789
            if (!entry.ToUpperInvariant().StartsWith(@"DATE"":""", StringComparison.Ordinal) || entry.Length != 20 || entry[9] != '\\' || entry[10] != '/' || entry[13] != '\\' || entry[14] != '/')
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [date] splitted entry [{entry}] in [{str}], skipping.");
                return null;
            }

            int day = 10 * (entry[7] - '0') + (entry[8] - '0');
            int month = 10 * (entry[11] - '0') + (entry[12] - '0');
            int year = 1000 * (entry[15] - '0') + 100 * (entry[16] - '0') + 10 * (entry[17] - '0') + (entry[18] - '0');
            var dateTime = new DateTime(year, month, day, 0, 0, 0).Add(endofdayClosingTime ?? DefaultEndofdayClosingTime);

            entry = splitted[3];

            // open":"1,329.39"
            //           111111
            // 0123456789012345
            if (!entry.ToUpperInvariant().StartsWith(@"OPEN"":""", StringComparison.Ordinal)
                || entry[^1] != '\"'
                || entry.ToUpperInvariant().Contains("NULL", StringComparison.Ordinal))
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [open] splitted entry [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = entry.Substring(7, entry.Length - 8); // 1,329.39
            entry = entry.Replace(",", string.Empty, StringComparison.Ordinal); // 1329.39
            double open;
            try
            {
                open = double.Parse(entry, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [open] [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = splitted[4];

            // high":"1,329.39"
            //           11111
            // 012345678901234
            if (!entry.ToUpperInvariant().StartsWith(@"HIGH"":""", StringComparison.Ordinal)
                || entry[^1] != '\"'
                || entry.ToUpperInvariant().Contains("NULL", StringComparison.Ordinal))
            {
                Log.Error($"{Prefix}: Invalid endofday json: invalid [high] splitted entry [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = entry.Substring(7, entry.Length - 8); // 1,329.39
            entry = entry.Replace(",", string.Empty, StringComparison.Ordinal); // 1329.39
            double high;
            try
            {
                high = double.Parse(entry, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                Log.Error($"{Prefix}: Invalid endofday json: invalid [high] [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = splitted[5];

            // low":"1,329.39"
            //           11111
            // 012345678902345
            if (!entry.ToUpperInvariant().StartsWith(@"LOW"":""", StringComparison.Ordinal)
                || entry[^1] != '\"'
                || entry.ToUpperInvariant().Contains("NULL", StringComparison.Ordinal))
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [low] splitted entry [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = entry.Substring(6, entry.Length - 7); // 1,329.39
            entry = entry.Replace(",", string.Empty, StringComparison.Ordinal); // 1329.39
            double low;
            try
            {
                low = double.Parse(entry, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [low] [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = splitted[6];

            // close":"1,329.39"
            //           1111111
            // 01234567890123456
            if (!entry.ToUpperInvariant().StartsWith(@"CLOSE"":""", StringComparison.Ordinal)
                || entry[^1] != '\"'
                || entry.ToUpperInvariant().Contains("NULL", StringComparison.Ordinal))
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [close] splitted entry [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = entry.Substring(8, entry.Length - 9); // 1,329.39
            entry = entry.Replace(",", string.Empty, StringComparison.Ordinal); // 1329.39
            double close;
            try
            {
                close = double.Parse(entry, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [close] [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = splitted[7];

            // ReSharper disable once CommentTypo
            // nymberofshares":"1,118.00"
            // ReSharper disable once CommentTypo
            // nymberofshares":"0,00"
            //           111111111122
            // 0123456789012345678901
            // ReSharper disable StringLiteralTypo
            if (!(entry.ToUpperInvariant().StartsWith(@"NYMBEROFSHARES"":""", StringComparison.Ordinal)
                || entry.ToUpperInvariant().StartsWith(@"NUMBEROFSHARES"":""", StringComparison.Ordinal))
                || entry[^1] != '\"'
                || entry.ToUpperInvariant().Contains("NULL", StringComparison.Ordinal))
            {
                // ReSharper restore StringLiteralTypo
                Log.Error($"{Prefix}: invalid endofday json: invalid [numberOfShares] splitted entry [{entry}] in [{str}], skipping.");
                return null;
            }

            entry = entry.Substring(17, entry.Length - 18); // 1,118.00 // 0,00
            entry = entry.Replace(",", string.Empty, StringComparison.Ordinal); // 1118.00 // 000
            double volume;
            try
            {
                volume = double.Parse(entry, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                Log.Error($"{Prefix}: invalid endofday json: invalid [numberOfShares] [{entry}] in [{str}], skipping.");
                return null;
            }

            return new Ohlcv(dateTime, open, high, low, close, volume);
        }
    }
}
