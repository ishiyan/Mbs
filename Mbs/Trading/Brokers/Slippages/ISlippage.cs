using Mbs.Trading.Orders;

namespace Mbs.Trading.Brokers.Slippages
{
    /// <summary>
    /// A generic slippage interface to use with simulation brokers.
    /// </summary>
    public interface ISlippage
    {
        /// <summary>
        /// The slippage amount.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="lastPrice">The price of this fill.</param>
        /// <param name="lastQuantity">The quantity bought/sold on this fill.</param>
        /// <returns>The slippage of this fill.</returns>
        double Amount(SingleOrder order, double lastPrice, double lastQuantity);
    }
}
