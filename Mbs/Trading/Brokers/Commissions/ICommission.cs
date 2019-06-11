using Mbs.Trading.Currencies;
using Mbs.Trading.Orders;

namespace Mbs.Trading.Brokers.Commissions
{
    /// <summary>
    /// A generic commission calculation interface to use with simulation brokers.
    /// </summary>
    public interface ICommission
    {
        /// <summary>
        /// Gets the commission currency.
        /// </summary>
        CurrencyCode Currency { get; }

        /// <summary>
        /// The commission amount.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="lastPrice">The price of this fill.</param>
        /// <param name="lastQuantity">The quantity bought/sold on this fill.</param>
        /// <param name="leavesQuantity">The quantity open for further execution after this fill.</param>
        /// <param name="cumulativeQuantity">The total quantity filled after this fill.</param>
        /// <param name="averagePrice">The average price after this fill.</param>
        /// <param name="cumulativeCommission">The total commission before this fill.</param>
        /// <returns>The commission of this fill.</returns>
        double Amount(
            SingleOrder order,
            double lastPrice,
            double lastQuantity,
            double leavesQuantity,
            double cumulativeQuantity,
            double averagePrice,
            double cumulativeCommission);
    }
}
