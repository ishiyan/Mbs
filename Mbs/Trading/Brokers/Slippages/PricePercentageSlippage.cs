using Mbs.Trading.Orders;

namespace Mbs.Trading.Brokers.Slippages
{
    /// <summary>
    /// Models the slippage as a percentage of the price.
    /// </summary>
    public sealed class PricePercentageSlippage : ISlippage
    {
        /// <summary>
        /// Gets or sets the percentage in range [0, 1) of a price that will be used as a slippage.
        /// </summary>
        public double Percentage { get; set; }

        /// <inheritdoc />
        public double Amount(SingleOrder order, double lastPrice, double lastQuantity)
        {
            return lastPrice * Percentage;
        }
    }
}
