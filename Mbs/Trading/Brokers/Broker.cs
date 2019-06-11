using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mbs.Trading.Currencies;
using Mbs.Trading.Orders;

// ReSharper disable InconsistentNaming
#pragma warning disable SA1214 // Readonly fields should appear before non-readonly fields
#pragma warning disable CA1051 // Do not declare visible instance fields

namespace Mbs.Trading.Brokers
{
    /// <summary>
    /// The abstract base for all broker implementations.
    /// </summary>
    public abstract class Broker : IBroker
    {
        /// <summary>
        /// Extend this class to store private implementation-specific per-order information, and implement order lifetime transitions.
        /// </summary>
        protected abstract class SingleOrderTicket : ISingleOrderTicket
        {
            /// <summary>
            /// The comparison threschold.
            /// </summary>
            private const double Epsilon = 0.0000001;

            /// <summary>
            /// Gets a value indicating whether this order is completed.
            /// </summary>
            protected bool IsCompleted =>
                orderStatus == OrderStatus.Filled ||
                orderStatus == OrderStatus.Expired ||
                orderStatus == OrderStatus.Rejected ||
                orderStatus == OrderStatus.Canceled;

            /// <summary>
            /// Gets a value indicating whether this order can be filled.
            /// </summary>
            protected bool CanFill =>
                orderStatus == OrderStatus.New ||
                orderStatus == OrderStatus.PartiallyFilled;

            /// <summary>
            /// The parent.
            /// </summary>
            private readonly Broker parent;

            /// <summary>
            /// Identifies the current state of an order as understood by the broker.
            /// </summary>
            protected OrderStatus orderStatus;

            /// <summary>
            /// Gets the current state of an order as understood by the broker.
            /// </summary>
            public OrderStatus OrderStatus => orderStatus;

            /// <summary>
            /// The underlying order for this ticket. If there were any successful order replacements, this will be the most recent version.
            /// </summary>
            protected SingleOrder order;

            /// <summary>
            /// Gets the underlying order for this ticket. If there were any successful order replacements, this will be the most recent version.
            /// </summary>
            public SingleOrder Order => order;

            /// <summary>
            /// The unique identifier for an order as assigned by the buy-side (institution, broker, intermediary etc.).
            /// </summary>
            protected string clientOrderId;

            /// <summary>
            /// Gets the unique identifier for an order as assigned by the buy-side (institution, broker, intermediary etc.).
            /// </summary>
            public string ClientOrderId => clientOrderId;

            /// <summary>
            /// The unique identifier for an order as assigned by the sell-side (exchange, ECN, etc.).
            /// </summary>
            protected string orderId;

            /// <summary>
            /// Gets the unique identifier for an order as assigned by the sell-side (exchange, ECN, etc.).
            /// </summary>
            public string OrderId => orderId;

            /// <summary>
            /// The commission currency.
            /// </summary>
            protected CurrencyCode commissionCurrency = CurrencyCode.Xxx;

            /// <summary>
            /// Use when updating quantities/prices after (partial) fill.
            /// </summary>
            protected readonly object fillLock = new object();

            /// <summary>
            /// The price of the last fill. Use the <c>fillLock</c> to update the value.
            /// </summary>
            protected double lastPrice;

            /// <summary>
            /// Gets the price of the last fill.
            /// </summary>
            public double LastPrice
            {
                get
                {
                    lock (fillLock)
                    {
                        return lastPrice;
                    }
                }
            }

            /// <summary>
            /// The quantity bought/sold on the last fill. Use the <c>fillLock</c> to update the value.
            /// </summary>
            protected double lastQuantity;

            /// <summary>
            /// Gets the quantity bought/sold on the last fill.
            /// </summary>
            public double LastQuantity
            {
                get
                {
                    lock (fillLock)
                    {
                        return lastQuantity;
                    }
                }
            }

            /// <summary>
            /// The quantity open for further execution. Use the <c>fillLock</c> to update the value.
            /// </summary>
            protected double leavesQuantity;

            /// <summary>
            /// Gets the quantity open for further execution.
            /// </summary>
            public double LeavesQuantity
            {
                get
                {
                    lock (fillLock)
                    {
                        return leavesQuantity;
                    }
                }
            }

            /// <summary>
            /// The total quantity filled. Use the <c>fillLock</c> to update the value.
            /// </summary>
            protected double cumulativeQuantity;

            /// <summary>
            /// Gets the total quantity filled.
            /// </summary>
            public double CumulativeQuantity
            {
                get
                {
                    lock (fillLock)
                    {
                        return cumulativeQuantity;
                    }
                }
            }

            /// <summary>
            /// The average price. Use the <c>fillLock</c> to update the value.
            /// </summary>
            protected double averagePrice;

            /// <summary>
            /// Gets the average price.
            /// </summary>
            public double AveragePrice
            {
                get
                {
                    lock (fillLock)
                    {
                        return averagePrice;
                    }
                }
            }

            /// <summary>
            /// The commission of the last fill. Use the <see cref="fillLock"/> to update the value.
            /// </summary>
            protected double lastCommission;

            /// <summary>
            /// Gets the commission of the last fill.
            /// </summary>
            public double LastCommission
            {
                get
                {
                    lock (fillLock)
                    {
                        return lastCommission;
                    }
                }
            }

            /// <summary>
            /// The total cumulative commission. Use the <see cref="fillLock"/> to update the value.
            /// </summary>
            protected double cumulativeCommission;

            /// <summary>
            /// Gets the total cumulative commission.
            /// </summary>
            public double CumulativeCommission
            {
                get
                {
                    lock (fillLock)
                    {
                        return cumulativeCommission;
                    }
                }
            }

            private readonly object reportsLock = new object();
            private readonly List<SingleOrderReport> reports = new List<SingleOrderReport>();

            /// <summary>
            /// Gets a list of all order reports in the chronological order.
            /// </summary>
            public ReadOnlyCollection<SingleOrderReport> Reports
            {
                get
                {
                    lock (reportsLock)
                    {
                        return new ReadOnlyCollection<SingleOrderReport>(reports);
                    }
                }
            }

            private SingleOrderReport lastReport;

            /// <summary>
            /// Gets the last order report, <c>null</c> if not any.
            /// </summary>
            public SingleOrderReport LastReport => lastReport;

            private readonly object orderReportLock = new object();
            private Action<ISingleOrderTicket, SingleOrderReport> orderReport;

            private void OnOrderReport(SingleOrderReport e)
            {
                lock (orderReportLock)
                {
                    if (null != orderReport)
                    {
                        Delegate[] handlers = orderReport.GetInvocationList();
                        foreach (Delegate handler in handlers)
                        {
                            var subscriber = handler as Action<ISingleOrderTicket, SingleOrderReport>;
                            subscriber?.Invoke(this, e);
                        }
                    }
                }
            }

            /// <summary>
            /// Notifies when a report has been received.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Reviewed")]
            public event Action<ISingleOrderTicket, SingleOrderReport> OrderReport
            {
                add
                {
                    lock (orderReportLock)
                    {
                        orderReport += value;
                    }
                }

                remove
                {
                    lock (orderReportLock)
                    {
                        // ReSharper disable once DelegateSubtraction
                        orderReport -= value;
                    }
                }
            }

            private readonly object orderCompletedLock = new object();
            private Action<ISingleOrderTicket> orderCompleted;

            private void OnOrderCompleted()
            {
                lock (orderCompletedLock)
                {
                    if (null != orderCompleted)
                    {
                        Delegate[] handlers = orderCompleted.GetInvocationList();
                        foreach (Delegate handler in handlers)
                        {
                            var subscriber = handler as Action<ISingleOrderTicket>;
                            subscriber?.Invoke(this);
                        }
                    }
                }
            }

            /// <summary>
            /// Notifies when an order has been completed. This is called after the order has been moved to the terminal state.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Reviewed")]
            public event Action<ISingleOrderTicket> OrderCompleted
            {
                add
                {
                    lock (orderCompletedLock)
                    {
                        orderCompleted += value;
                    }
                }

                remove
                {
                    lock (orderCompletedLock)
                    {
                        // ReSharper disable once DelegateSubtraction
                        orderCompleted -= value;
                    }
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SingleOrderTicket"/> class.
            /// </summary>
            /// <param name="order">The order to create an order ticket for.</param>
            /// <param name="parent">The parent <see cref="Broker"/>.</param>
            /// <param name="reportAction">Called when a report has been received.</param>
            /// <param name="completionAction">Called when the order has been completed.</param>
            protected SingleOrderTicket(
                SingleOrder order,
                Broker parent,
                Action<ISingleOrderTicket, SingleOrderReport> reportAction,
                Action<ISingleOrderTicket> completionAction)
            {
                if (Epsilon > order.Quantity)
                    throw new ArgumentOutOfRangeException(nameof(order), "Illegal order quantity value: " + order.Quantity);
                this.parent = parent;
                this.order = order;
                leavesQuantity = order.Quantity;
                if (null != reportAction)
                    OrderReport += reportAction;
                if (null != completionAction)
                    OrderCompleted += completionAction;
            }

            /// <summary>
            /// Replaces this order with the new one.
            /// </summary>
            /// <param name="replacementOrder">The replacement order.</param>
            public void Replace(SingleOrder replacementOrder)
            {
                HandleReplace(replacementOrder);
            }

            /// <summary>
            /// Cancels this order.
            /// </summary>
            public void Cancel()
            {
                HandleCancel();
            }

            /// <summary>
            /// Submits this order.
            /// </summary>
            protected void Submit()
            {
                lock (parent.ticketDictionaryLock)
                {
                    if (parent.ticketDictionary.ContainsKey(clientOrderId))
                        throw new InvalidOperationException("Duplicate client order id: " + clientOrderId);
                    parent.ticketDictionary.Add(clientOrderId, this);
                }

                HandleSubmit();
            }

            /// <summary>
            /// Implement response to a  <see cref="Submit"/> call. Must assign a unique <see cref="clientOrderId"/>.
            /// </summary>
            protected abstract void HandleSubmit();

            /// <summary>
            /// Implement response to a <see cref="Replace"/> call.
            /// </summary>
            /// <param name="replacementOrder">The replacement order.</param>
            protected abstract void HandleReplace(SingleOrder replacementOrder);

            /// <summary>
            /// Implement response to a <see cref="Replace"/> call.
            /// </summary>
            protected abstract void HandleCancel();

            /// <summary>
            /// Archives the report and fires the order report event.
            /// </summary>
            /// <param name="report">The single order report.</param>
            protected void OnReport(SingleOrderReport report)
            {
                orderStatus = report.Status;
                lock (reportsLock)
                {
                    reports.Add(report);
                }

                lastReport = report;
                OnOrderReport(report);
            }

            /// <summary>
            /// Archives the report and fires the order report event, generating a completion.
            /// </summary>
            /// <param name="report">The single order report.</param>
            protected void OnReportWithCompletion(SingleOrderReport report)
            {
                OnReport(report);
                OnOrderCompleted();
            }
        }

        private readonly object ticketDictionaryLock = new object();
        private readonly Dictionary<string, ISingleOrderTicket> ticketDictionary = new Dictionary<string, ISingleOrderTicket>();

        /// <inheritdoc />
        public ISingleOrderTicket PlaceOrder(SingleOrder order)
        {
            return CreateOrderTicket(order, this, null, null);
        }

        /// <inheritdoc />
        public ISingleOrderTicket PlaceOrder(SingleOrder order, Action<ISingleOrderTicket, SingleOrderReport> reportAction)
        {
            return CreateOrderTicket(order, this, reportAction, null);
        }

        /// <inheritdoc />
        public ISingleOrderTicket PlaceOrder(SingleOrder order, Action<ISingleOrderTicket> completionAction)
        {
            return CreateOrderTicket(order, this, null, completionAction);
        }

        /// <inheritdoc />
        public ISingleOrderTicket PlaceOrder(SingleOrder order, Action<ISingleOrderTicket, SingleOrderReport> reportAction, Action<ISingleOrderTicket> completionAction)
        {
            return CreateOrderTicket(order, this, reportAction, completionAction);
        }

        /// <inheritdoc />
        public ReadOnlyCollection<ISingleOrderTicket> SingleOrderTickets
        {
            get
            {
                lock (ticketDictionaryLock)
                {
                    return new ReadOnlyCollection<ISingleOrderTicket>(ticketDictionary.Values.ToList());
                }
            }
        }

        /// <summary>
        /// Implement this in a derived class.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="reportAction">Called when a report has been received.</param>
        /// <param name="completionAction">Called when the order has been completed.</param>
        /// <returns>The order ticket interface.</returns>
        protected abstract ISingleOrderTicket CreateOrderTicket(SingleOrder order, Broker parent, Action<ISingleOrderTicket, SingleOrderReport> reportAction, Action<ISingleOrderTicket> completionAction);
    }
}
