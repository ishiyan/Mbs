using System;
using System.Collections.ObjectModel;
using Mbs.Trading.Orders.Enumerations;

namespace Mbs.Trading.Orders
{
    /// <summary>
    /// A single order ticket interface.
    /// </summary>
    public interface ISingleOrderTicket
    {
        /// <summary>
        /// Notifies when a report has been received.
        /// </summary>
        event Action<ISingleOrderTicket, SingleOrderReport> OrderReport;

        /// <summary>
        /// Notifies when an order has been completed. This is called after the order has been moved to the terminal state.
        /// </summary>
        event Action<ISingleOrderTicket> OrderCompleted;

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
        double LastFillPrice { get; }

        /// <summary>
        /// Gets the quantity bought/sold on the last fill.
        /// </summary>
        double LastFillQuantity { get; }

        /// <summary>
        /// Gets the quantity open for further execution.
        /// If the order status is <c>OrderStatus.Canceled</c>, <c>OrderStatus.Expired</c> or <c>OrderStatus.Rejected</c> (in which case the order is no longer active) then this could be 0, otherwise <c>Order.Quantity - CumulativeQuantity</c>.
        /// </summary>
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
        double LastFillCommission { get; }

        /// <summary>
        /// Gets the total commission.
        /// </summary>
        double CumulativeCommission { get; }

        /// <summary>
        /// Gets the last order report, <c>null</c> if not any.
        /// </summary>
        SingleOrderReport LastReport { get; }

        /// <summary>
        /// Gets a list of all order reports in the chronological order.
        /// </summary>
        ReadOnlyCollection<SingleOrderReport> Reports { get; }

        /// <summary>
        /// Replaces a pending order. If the order has been completed (successfully or not), does nothing.
        /// </summary>
        /// <param name="replacementOrder">The replacement order.</param>
        void Replace(SingleOrder replacementOrder);

        /// <summary>
        /// Cancels this order. If order has been already completed (successfully or not), does nothing.
        /// </summary>
        void Cancel();
    }
}
