namespace Mbs.Trading.Brokers.PaperBrokers
{
    /// <summary>
    /// The fill on quote mode.
    /// </summary>
    public enum FillOnQuote
    {
        /// <summary>
        /// Ignore bids/asks for all orders.
        /// </summary>
        None,

        /// <summary>
        /// Fill an order with the last available bid/ask price.
        /// </summary>
        Last,

        /// <summary>
        /// Fill an order with the next available bid/ask price.
        /// </summary>
        Next,
    }
}
