using System;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    /// <summary>
    /// Encapsulates information about CSV data.
    /// </summary>
    public class CsvInfo
    {
        /// <summary>
        /// Time, open, high, low, close, volume. Data type is <see cref="CsvDataType.Ohlcv"/>.
        /// </summary>
        public const string ColumnNamesTohlcv = "tohlcv";

        /// <summary>
        /// Time, open, high, low, close (volume is NaN). Data type is <see cref="CsvDataType.Ohlcv"/>.
        /// </summary>
        public const string ColumnNamesTohlc = "tohlc";

        /// <summary>
        /// Time, open, close, volume (high and low will be selected from the open and close). Data type is <see cref="CsvDataType.Ohlcv"/>.
        /// </summary>
        public const string ColumnNamesTocv = "tocv";

        /// <summary>
        /// Time, open, close (volume is NaN, high and low will be selected from the open and close). Data type is <see cref="CsvDataType.Ohlcv"/>.
        /// </summary>
        public const string ColumnNamesToc = "toc";

        /// <summary>
        /// Time, trade price, volume. Data type is <see cref="CsvDataType.Trade"/>.
        /// </summary>
        public const string ColumnNamesTpv = "tpv";

        /// <summary>
        /// Time, trade price (volume is NaN). Data type is <see cref="CsvDataType.Trade"/>.
        /// </summary>
        public const string ColumnNamesTp = "tp";

        /// <summary>
        /// Time, ask price, bid price, ask size, bid size. Data type is <see cref="CsvDataType.Quote"/>.
        /// </summary>
        public const string ColumnNamesTabss = "tabss";

        /// <summary>
        /// Time, ask price, bid price (ask/bid size is NaN). Data type is <see cref="CsvDataType.Quote"/>.
        /// </summary>
        public const string ColumnNamesTab = "tab";

        /// <summary>
        /// Time, scalar value. Data type is <see cref="CsvDataType.Scalar"/>.
        /// </summary>
        public const string ColumnNamesTs = "ts";

        /// <summary>
        /// The default time granularity expressed as a string.
        /// </summary>
        public const string DefaultTimeGranularityString = "Day1";

        /// <summary>
        /// The default time granularity expressed as a string.
        /// </summary>
        public const TimeGranularity DefaultTimeGranularity = TimeGranularity.Day1;

        /// <summary>
        /// The default date and time format of all CSV data rows.
        /// </summary>
        public const string DefaultDateTimeFormat = "yyyy/MM/dd HH:mm:ss.fffffff";

        /// <summary>
        /// The default row comment character expressed as a string.
        /// </summary>
        public const string DefaultCommentCharacter = "";

        /// <summary>
        /// The default column separator expressed as a string.
        /// </summary>
        public const string DefaultSeparatorCharacter = ";";

        private readonly string keySuffix;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvInfo"/> class.
        /// </summary>
        /// <param name="filePath">A path to the CSV file. May be absolute or relative.</param>
        /// <param name="columnSet">A string representing predefined column set names: tohlcv, tohlc, tocv, toc, tpv, tp, tabss, tab, ts.</param>
        /// <param name="dateTimeFormat">The c# format of the date and time column. The default value is <see cref="DefaultDateTimeFormat"/>.</param>
        /// <param name="timeGranularity">A string representing the time granularity of CSV data. The default value is <see cref="DefaultTimeGranularity"/>.</param>
        /// <param name="separatorCharacter">A CSV column separator character expressed as a string. The default value is <see cref="DefaultSeparatorCharacter"/>.</param>
        /// <param name="commentCharacter">A row comment character expressed as a string. Should be in the very first position in a row. The default value is <see cref="DefaultCommentCharacter"/>. Empty string means no comments.</param>
        /// <param name="isAdjusted">Specifies if data is adjusted (true), not adjusted (false) or unknown (null).</param>
        public CsvInfo(
            string filePath,
            CsvColumnSet columnSet,
            string dateTimeFormat = DefaultDateTimeFormat,
            TimeGranularity timeGranularity = DefaultTimeGranularity,
            string separatorCharacter = DefaultSeparatorCharacter,
            string commentCharacter = DefaultCommentCharacter,
            bool? isAdjusted = null)
        {
            FilePath = filePath;
            switch (columnSet)
            {
                case CsvColumnSet.Tohlcv:
                    DataType = CsvDataType.Ohlcv;
                    HasHighLow = true;
                    HasVolume = true;
                    break;

                case CsvColumnSet.Tohlc:
                    DataType = CsvDataType.Ohlcv;
                    HasHighLow = true;
                    break;

                case CsvColumnSet.Tocv:
                    DataType = CsvDataType.Ohlcv;
                    HasVolume = true;
                    break;

                case CsvColumnSet.Toc:
                    DataType = CsvDataType.Ohlcv;
                    break;

                case CsvColumnSet.Ts:
                    DataType = CsvDataType.Scalar;
                    break;

                case CsvColumnSet.Tpv:
                    DataType = CsvDataType.Trade;
                    HasVolume = true;
                    break;

                case CsvColumnSet.Tp:
                    DataType = CsvDataType.Trade;
                    break;

                case CsvColumnSet.Tabss:
                    DataType = CsvDataType.Quote;
                    HasVolume = true;
                    break;

                case CsvColumnSet.Tab:
                    DataType = CsvDataType.Quote;
                    break;

                default:
                    throw new ArgumentException($"invalid column set \"{columnSet}\"", nameof(columnSet));
            }

            DateTimeFormat = dateTimeFormat;
            TimeGranularity = timeGranularity;
            SeparatorCharacter = ParseSeparatorCharacter(separatorCharacter);
            CommentCharacter = ParseCommentCharacter(commentCharacter, out bool hasCommentCharacter);
            HasCommentCharacter = hasCommentCharacter;
            IsAdjustedData = isAdjusted;
            keySuffix = GenerateKeySuffix();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvInfo"/> class.
        /// </summary>
        /// <param name="filePath">A path to the CSV file. May be absolute or relative.</param>
        /// <param name="columnSet">A string representing predefined column set names: tohlcv, tohlc, tocv, toc, tpv, tp, tabss, tab, ts.</param>
        /// <param name="dateTimeFormat">The c# format of the date and time column. The default value is <see cref="DefaultDateTimeFormat"/>.</param>
        /// <param name="timeGranularity">A string representing the time granularity of CSV data. The default value is <see cref="DefaultTimeGranularityString"/>.</param>
        /// <param name="separatorCharacter">A CSV column separator character expressed as a string. The default value is <see cref="DefaultSeparatorCharacter"/>.</param>
        /// <param name="commentCharacter">A row comment character expressed as a string. Should be in the very first position in a row. The default value is <see cref="DefaultCommentCharacter"/>. Empty string means no comments.</param>
        /// <param name="isAdjusted">Specifies if data is adjusted ('true'), not adjusted ('false') or unknown (null or any other string).</param>
        public CsvInfo(
            string filePath,
            string columnSet,
            string dateTimeFormat = DefaultDateTimeFormat,
            string timeGranularity = DefaultTimeGranularityString,
            string separatorCharacter = DefaultSeparatorCharacter,
            string commentCharacter = DefaultCommentCharacter,
            string isAdjusted = null)
        {
            FilePath = filePath;
            if (columnSet.Equals(ColumnNamesTohlcv, StringComparison.OrdinalIgnoreCase))
            {
                DataType = CsvDataType.Ohlcv;
                HasHighLow = true;
                HasVolume = true;
            }
            else if (columnSet.Equals(ColumnNamesTohlc, StringComparison.OrdinalIgnoreCase))
            {
                DataType = CsvDataType.Ohlcv;
                HasHighLow = true;
            }
            else if (columnSet.Equals(ColumnNamesTs, StringComparison.OrdinalIgnoreCase))
            {
                DataType = CsvDataType.Scalar;
            }
            else if (columnSet.Equals(ColumnNamesTpv, StringComparison.OrdinalIgnoreCase))
            {
                DataType = CsvDataType.Trade;
                HasVolume = true;
            }
            else if (columnSet.Equals(ColumnNamesTabss, StringComparison.OrdinalIgnoreCase))
            {
                DataType = CsvDataType.Quote;
                HasVolume = true;
            }
            else if (columnSet.Equals(ColumnNamesTab, StringComparison.OrdinalIgnoreCase))
            {
                DataType = CsvDataType.Quote;
            }
            else if (columnSet.Equals(ColumnNamesTp, StringComparison.OrdinalIgnoreCase))
            {
                DataType = CsvDataType.Trade;
            }
            else if (columnSet.Equals(ColumnNamesTocv, StringComparison.OrdinalIgnoreCase))
            {
                DataType = CsvDataType.Ohlcv;
                HasVolume = true;
            }
            else if (columnSet.Equals(ColumnNamesToc, StringComparison.OrdinalIgnoreCase))
            {
                DataType = CsvDataType.Ohlcv;
            }
            else
            {
                throw new ArgumentException($"invalid column set \"{columnSet}\"", nameof(columnSet));
            }

            DateTimeFormat = dateTimeFormat;
            TimeGranularity = ParseTimeGranularity(timeGranularity);
            SeparatorCharacter = ParseSeparatorCharacter(separatorCharacter);
            CommentCharacter = ParseCommentCharacter(commentCharacter, out bool hasCommentCharacter);
            HasCommentCharacter = hasCommentCharacter;
            IsAdjustedData = ParseIsAdjusted(isAdjusted);
            keySuffix = GenerateKeySuffix();
        }

        /// <summary>
        /// Gets the time granularity of the CSV data.
        /// </summary>
        public TimeGranularity TimeGranularity { get; }

        /// <summary>
        /// Gets the date and time format of all CSV data rows.
        /// </summary>
        public string DateTimeFormat { get; }

        /// <summary>
        /// Gets the CSV data type.
        /// </summary>
        public CsvDataType DataType { get; }

        /// <summary>
        /// Gets the CSV column separator character string.
        /// </summary>
        public string SeparatorCharacter { get; }

        /// <summary>
        /// Gets the CSV row comment character. Should be in the very first position in a row.
        /// </summary>
        public char CommentCharacter { get; }

        /// <summary>
        /// Gets a value indicating whether CSV data rows may be commented out.
        /// </summary>
        public bool HasCommentCharacter { get; }

        /// <summary>
        /// Gets a value indicating whether CSV data rows have volume part.
        /// </summary>
        public bool HasVolume { get; }

        /// <summary>
        /// Gets a value indicating whether CSV data rows have highest and lowest price parts.
        /// </summary>
        public bool HasHighLow { get; }

        /// <summary>
        /// Gets if data are adjusted. Null if unknown.
        /// </summary>
        public bool? IsAdjustedData { get; }

        /// <summary>
        /// Gets the path to the CSV file.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        internal string Key => string.Concat(FilePath.ToUpperInvariant(), keySuffix);

        private static TimeGranularity ParseTimeGranularity(string timeGranularity)
        {
            if (Enum.TryParse(timeGranularity, true, out TimeGranularity converted))
            {
                return converted;
            }

            throw new ArgumentException($"invalid time granularity \"{timeGranularity}\"", nameof(timeGranularity));
        }

        private static string ParseSeparatorCharacter(string separatorCharacter)
        {
            if (separatorCharacter.Length != 1)
            {
                throw new ArgumentException($"invalid separator character \"{separatorCharacter}\"", nameof(separatorCharacter));
            }

            return separatorCharacter;
        }

        private static char ParseCommentCharacter(string commentCharacter, out bool hasCommentCharacter)
        {
            if (string.IsNullOrWhiteSpace(commentCharacter))
            {
                hasCommentCharacter = false;
                return '\0';
            }

            if (commentCharacter.Length != 1)
            {
                throw new ArgumentException($"invalid comment character \"{commentCharacter}\"", nameof(commentCharacter));
            }

            hasCommentCharacter = true;
            return commentCharacter[0];
        }

        private static bool? ParseIsAdjusted(string isAdjusted)
        {
            if (!string.IsNullOrWhiteSpace(isAdjusted))
            {
                string value = isAdjusted.ToUpperInvariant();
                if (value.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (value.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return null;
        }

        private string GenerateKeySuffix()
        {
            const string underscore = "_";
            var adjustment = string.Empty;
            if (IsAdjustedData.HasValue)
            {
                adjustment = IsAdjustedData.Value ? "adj" : "nadj";
            }

            return string.Concat(
                underscore,
                DataType.ToString().ToUpperInvariant(),
                underscore,
                TimeGranularity.ToString().ToUpperInvariant(),
                underscore,
                adjustment);
        }
    }
}
