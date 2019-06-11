namespace Mbs.Trading.Data.Historical
{
    /// <summary>
    /// Identifies column sets in a CSV file.
    /// </summary>
    public enum CsvColumnSet
    {
        /// <summary>
        /// Time, open, high, low, close, volume. Data type is <see cref="CsvDataType.Ohlcv"/>.
        /// </summary>
        Tohlcv,

        /// <summary>
        /// Time, open, high, low, close (volume is NaN). Data type is <see cref="CsvDataType.Ohlcv"/>.
        /// </summary>
        Tohlc,

        /// <summary>
        /// Time, open, close, volume (high and low will be selected from the open and close). Data type is <see cref="CsvDataType.Ohlcv"/>.
        /// </summary>
        Tocv,

        /// <summary>
        /// Time, open, close (volume is NaN, high and low will be selected from the open and close). Data type is <see cref="CsvDataType.Ohlcv"/>.
        /// </summary>
        Toc,

        /// <summary>
        /// Time, trade price, volume. Data type is <see cref="CsvDataType.Trade"/>.
        /// </summary>
        Tpv,

        /// <summary>
        /// Time, trade price (volume is NaN). Data type is <see cref="CsvDataType.Trade"/>.
        /// </summary>
        Tp,

        /// <summary>
        /// Time, ask price, bid price, ask size, bid size. Data type is <see cref="CsvDataType.Quote"/>.
        /// </summary>
        Tabss,

        /// <summary>
        /// Time, ask price, bid price (ask/bid size is NaN). Data type is <see cref="CsvDataType.Quote"/>.
        /// </summary>
        Tab,

        /// <summary>
        /// Time, scalar value. Data type is <see cref="CsvDataType.Scalar"/>.
        /// </summary>
        Ts
    }
}
