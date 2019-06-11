namespace Mbs.Trading.Brokers.PaperBrokers
{
    /// <summary>
    /// The fill on ohlcv mode.
    /// </summary>
    public enum FillOnOhlcv
    {
        /// <summary>
        /// Ignore ohlcv bars for all orders.
        /// </summary>
        None,

        /// <summary>
        /// Fill an order with the last available ohlcv closing price.
        /// </summary>
        LastClose,

        /// <summary>
        /// Fill an order with the next available ohlcv opening price.
        /// </summary>
        NextOpen,

        /// <summary>
        /// Fill an order with the next available ohlcv closing price.
        /// </summary>
        NextClose,

        /// <summary>
        /// Fill an order with the next available ohlcv best possible price: buy on lowest price, sell on highest price.
        /// </summary>
        NextBest,

        /// <summary>
        /// Fill an order with the next available ohlcv worst possible price: buy on highest price, sell on lowest price.
        /// </summary>
        NextWorst,

        /// <summary>
        /// Fill an order with the next available ohlcv median price (the average of the high and low prices).
        /// </summary>
        NextMedian,

        /// <summary>
        /// Fill an order with the next available ohlcv typical price (the average of the high, low, and closing prices).
        /// </summary>
        NextTypical,

        /// <summary>
        /// Fill an order with the next available ohlcv weighted close price (the average of twice the closing price plus the high and low prices).
        /// </summary>
        NextWeighted
    }
}
