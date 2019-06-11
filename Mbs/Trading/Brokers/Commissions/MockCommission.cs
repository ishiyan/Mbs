using System;
using Mbs.Trading.Currencies;
using Mbs.Trading.Orders;

namespace Mbs.Trading.Brokers.Commissions
{
    /// <summary>
    /// Models a mock commission as
    /// <para/><code>
    /// max(MinimumPerOrder, min(MaximumPerOrder, AbsolutePerOrder + PercentPerOrder/100 * qty * price + AbsolutePerShare * qty))
    /// </code>
    /// </summary>
    public sealed class MockCommission : ICommission
    {
        /// <summary>
        /// Gets or sets the minimum commission per order.
        /// </summary>
        public double MinimumPerOrder { get; set; }

        /// <summary>
        /// Gets or sets the maximum commission per order.
        /// </summary>
        public double MaximumPerOrder { get; set; } = double.MaxValue;

        /// <summary>
        /// Gets or sets the absolute commission per order.
        /// </summary>
        public double AbsolutePerOrder { get; set; }

        /// <summary>
        /// Gets or sets the commission percent per order, 100% is 100.
        /// </summary>
        public double PercentPerOrder { get; set; }

        /// <summary>
        /// Gets or sets the absolute commission per share.
        /// </summary>
        public double AbsolutePerShare { get; set; }

        /// <inheritdoc />
        public CurrencyCode Currency { get; set; }

        /// <inheritdoc />
        public double Amount(
            SingleOrder order,
            double lastPrice,
            double lastQuantity,
            double leavesQuantity,
            double cumulativeQuantity,
            double averagePrice,
            double cumulativeCommission)
        {
            if (0d >= MaximumPerOrder || MaximumPerOrder <= cumulativeCommission)
                return 0d;
            double commission = (0d < AbsolutePerOrder && Math.Abs(cumulativeCommission) < double.Epsilon) ?
                AbsolutePerOrder : cumulativeCommission;

            if (0d < PercentPerOrder)
                commission += lastQuantity * lastPrice * PercentPerOrder;

            if (0d < AbsolutePerShare)
                commission += lastQuantity * AbsolutePerShare;

            if (Math.Abs(leavesQuantity) < double.Epsilon && 0d < MinimumPerOrder && commission < MinimumPerOrder)
                commission = MinimumPerOrder;

            if (MaximumPerOrder < commission)
                commission -= MaximumPerOrder;
            commission -= cumulativeCommission;
            return 0d > commission ? 0d : commission;
        }
    }
}
