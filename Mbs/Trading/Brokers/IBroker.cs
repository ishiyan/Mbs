using System;
using System.Collections.ObjectModel;
using Mbs.Trading.Orders;

namespace Mbs.Trading.Brokers
{
    /// <summary>
    /// A generic broker interface.
    /// </summary>
    public interface IBroker
    {
        /// <summary>
        /// Places a single order.
        /// </summary>
        /// <param name="order">An order to be placed.</param>
        /// <returns>The order ticket interface.</returns>
        ISingleOrderTicket PlaceOrder(SingleOrder order);

        /// <summary>
        /// Places a single order.
        /// </summary>
        /// <param name="order">An order to be placed.</param>
        /// <param name="reportAction">Called when a report has been received.</param>
        /// <returns>The order ticket interface.</returns>
        ISingleOrderTicket PlaceOrder(SingleOrder order, Action<ISingleOrderTicket, SingleOrderReport> reportAction);

        /// <summary>
        /// Places a single order.
        /// </summary>
        /// <param name="order">An order to be placed.</param>
        /// <param name="completionAction">Called when the order has been completed.</param>
        /// <returns>The order ticket interface.</returns>
        ISingleOrderTicket PlaceOrder(SingleOrder order, Action<ISingleOrderTicket> completionAction);

        /// <summary>
        /// Places a single order.
        /// </summary>
        /// <param name="order">An order to be placed.</param>
        /// <param name="reportAction">Called when a report has been received.</param>
        /// <param name="completionAction">Called when the order has been completed.</param>
        /// <returns>The order ticket interface.</returns>
        ISingleOrderTicket PlaceOrder(SingleOrder order, Action<ISingleOrderTicket, SingleOrderReport> reportAction, Action<ISingleOrderTicket> completionAction);

        /// <summary>
        /// Gets a collection of all single order tickets.
        /// </summary>
        ReadOnlyCollection<ISingleOrderTicket> SingleOrderTickets { get; }
    }
}
