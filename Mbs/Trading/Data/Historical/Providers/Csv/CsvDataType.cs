namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    /// <summary>
    /// Identifies a data type of a CSV file.
    /// </summary>
    public enum CsvDataType
    {
        /// <summary>
        /// Time, open, high, low, close and volume.
        /// </summary>
        Ohlcv,

        /// <summary>
        /// Time, price and volume.
        /// </summary>
        Trade,

        /// <summary>
        /// Time, bid/ask price and size.
        /// </summary>
        Quote,

        /// <summary>
        /// Time and value.
        /// </summary>
        Scalar,
    }
}
