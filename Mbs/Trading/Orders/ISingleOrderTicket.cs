using System;
using System.Collections.ObjectModel;

namespace Mbs.Trading.Orders
{
    /// <summary>
    /// A single order ticket interface.
    /// </summary>
    public interface ISingleOrderTicket
    {
        /// <summary>
        /// Gets the underlying order for this ticket. If there were any successful order replacements, this will be the most recent version.
        /// </summary>
        SingleOrder Order { get; }

        /// <summary>
        /// Gets the unique identifier for an order as assigned by the buy-side (institution, broker, intermediary etc.).
        /// </summary>
        string ClientOrderId { get; }

        /// <summary>
        /// Gets the unique identifier for an order as assigned by the sell-side (exchange, ECN, etc.).
        /// </summary>
        string OrderId { get; }

        /// <summary>
        /// Gets the current state of an order as understood by the broker.
        /// </summary>
        OrderStatus OrderStatus { get; }

        /// <summary>
        /// Gets the price of the last fill.
        /// </summary>
        double LastPrice { get; }

        /// <summary>
        /// Gets the quantity bought/sold on the last fill.
        /// </summary>
        double LastQuantity { get; }

#pragma warning disable 1584, 1581, 1580
        /// <summary>
        /// Gets the quantity open for further execution.
        /// If the order status is <see cref="OrderStatus.Canceled"/>, <see cref="OrderStatus.Expired"/> or <see cref="OrderStatus.Rejected"/> (in which case the order is no longer active) then this could be 0, otherwise <c>Order.Quantity - CumulativeQuantity</c>.
        /// </summary>
#pragma warning restore 1584, 1581, 1580
        double LeavesQuantity { get; }

        /// <summary>
        /// Gets the total quantity filled.
        /// </summary>
        double CumulativeQuantity { get; }

        /// <summary>
        /// Gets the calculated average price of all fills on this order.
        /// </summary>
        double AveragePrice { get; }

        /// <summary>
        /// Gets the commission of the last fill.
        /// </summary>
        double LastCommission { get; }

        /// <summary>
        /// Gets the total commission.
        /// </summary>
        double CumulativeCommission { get; }

        /// <summary>
        /// Replaces a pending order. If the order has been completed (successfully or not), does nothing.
        /// </summary>
        /// <param name="replacementOrder">The replacement order.</param>
        void Replace(SingleOrder replacementOrder);

        /// <summary>
        /// Cancels this order. If order has been already completed (successfully or not), does nothing.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Gets the last order report, <c>null</c> if not any.
        /// </summary>
        SingleOrderReport LastReport { get; }

        /// <summary>
        /// Gets a list of all order reports in the chronological order.
        /// </summary>
        ReadOnlyCollection<SingleOrderReport> Reports { get; }

        /// <summary>
        /// Notifies when a report has been received.
        /// </summary>
        event Action<ISingleOrderTicket, SingleOrderReport> OrderReport;

        /// <summary>
        /// Notifies when an order has been completed. This is called after the order has been moved to the terminal state.
        /// </summary>
        event Action<ISingleOrderTicket> OrderCompleted;
    }
}
