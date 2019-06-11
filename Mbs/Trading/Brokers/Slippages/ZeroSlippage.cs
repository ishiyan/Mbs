using Mbs.Trading.Orders;

namespace Mbs.Trading.Brokers.Slippages
{
    /// <summary>
    /// A zero slippage to use with simulation brokers.
    /// </summary>
    public sealed class ZeroSlippage : ISlippage
    {
        /// <inheritdoc />
        public double Amount(SingleOrder order, double lastPrice, double lastQuantity)
        {
            return 0;
        }
    }
}
