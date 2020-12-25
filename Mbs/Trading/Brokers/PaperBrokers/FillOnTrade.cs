namespace Mbs.Trading.Brokers.PaperBrokers
{
    /// <summary>
    /// The fill on trade mode.
    /// </summary>
    public enum FillOnTrade
    {
        /// <summary>
        /// Ignore trade prices for all orders.
        /// </summary>
        None,

        /// <summary>
        /// Fill an order with the last available trade price.
        /// </summary>
        Last,

        /// <summary>
        /// Fill an order with the next available trade price.
        /// </summary>
        Next,
    }
}
