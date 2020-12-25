using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mbs.Trading.Currencies;
using Mbs.Trading.Orders;
using Mbs.Trading.Orders.Enumerations;

namespace Mbs.Trading.Brokers
{
    /// <summary>
    /// The abstract base for all broker implementations.
    /// </summary>
    public abstract class Broker : IBroker
    {
        private readonly object ticketDictionaryLock = new object();
        private readonly Dictionary<string, ISingleOrderTicket> ticketDictionary = new Dictionary<string, ISingleOrderTicket>();

        /// <inheritdoc />
        public ReadOnlyCollection<ISingleOrderTicket> GetAllTickets()
        {
                lock (ticketDictionaryLock)
                {
                    return new ReadOnlyCollection<ISingleOrderTicket>(ticketDictionary.Values.ToList());
                }
        }

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

        /// <summary>
        /// Implement this in a derived class.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="reportAction">Called when a report has been received.</param>
        /// <param name="completionAction">Called when the order has been completed.</param>
        /// <returns>The order ticket interface.</returns>
        protected abstract ISingleOrderTicket CreateOrderTicket(SingleOrder order, Broker parent, Action<ISingleOrderTicket, SingleOrderReport> reportAction, Action<ISingleOrderTicket> completionAction);

        /// <summary>
        /// Extend this class to store private implementation-specific per-order information, and implement order lifetime transitions.
        /// </summary>
        protected abstract class SingleOrderTicket : ISingleOrderTicket
        {
            /// <summary>
            /// The comparison threshold.
            /// </summary>
            private const double Epsilon = 0.0000001;

            /// <summary>
            /// The parent.
            /// </summary>
            private readonly Broker parent;

            private readonly object reportsLock = new object();
            private readonly object orderReportLock = new object();
            private readonly object orderCompletedLock = new object();
            private readonly List<SingleOrderReport> reports = new List<SingleOrderReport>();

            private SingleOrderReport lastReport;
            private Action<ISingleOrderTicket, SingleOrderReport> orderReport;
            private Action<ISingleOrderTicket> orderCompleted;

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
                if (order.Quantity < Epsilon)
                {
                    throw new ArgumentOutOfRangeException(nameof(order), $"Illegal order quantity value: {order.Quantity}");
                }

                this.parent = parent;
                this.UnderlyingOrder = order;
                CurrentLeavesQuantity = order.Quantity;
                if (reportAction != null)
                {
                    OrderReport += reportAction;
                }

                if (completionAction != null)
                {
                    OrderCompleted += completionAction;
                }
            }

            /// <summary>
            /// Notifies when a report has been received.
            /// </summary>
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

            /// <summary>
            /// Notifies when an order has been completed. This is called after the order has been moved to the terminal state.
            /// </summary>
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
            /// Gets the current state of an order as understood by the broker.
            /// </summary>
            public OrderStatus OrderStatus => UnderlyingOrderStatus;

            /// <summary>
            /// Gets the underlying order for this ticket. If there were any successful order replacements, this will be the most recent version.
            /// </summary>
            public SingleOrder Order => UnderlyingOrder;

            /// <summary>
            /// Gets the unique identifier for an order as assigned by the buy-side (institution, broker, intermediary etc.).
            /// </summary>
            public string ClientOrderId => UnderlyingClientOrderId;

            /// <summary>
            /// Gets the unique identifier for an order as assigned by the sell-side (exchange, ECN, etc.).
            /// </summary>
            public string OrderId => UnderlyingOrderId;

            /// <summary>
            /// Gets the price of the last fill.
            /// </summary>
            public double LastFillPrice
            {
                get
                {
                    lock (FillLock)
                    {
                        return LastPrice;
                    }
                }
            }

            /// <summary>
            /// Gets the quantity bought/sold on the last fill.
            /// </summary>
            public double LastFillQuantity
            {
                get
                {
                    lock (FillLock)
                    {
                        return LastQuantity;
                    }
                }
            }

            /// <summary>
            /// Gets the quantity open for further execution.
            /// </summary>
            public double LeavesQuantity
            {
                get
                {
                    lock (FillLock)
                    {
                        return CurrentLeavesQuantity;
                    }
                }
            }

            /// <summary>
            /// Gets the total quantity filled.
            /// </summary>
            public double CumulativeQuantity
            {
                get
                {
                    lock (FillLock)
                    {
                        return CurrentCumulativeQuantity;
                    }
                }
            }

            /// <summary>
            /// Gets the average price.
            /// </summary>
            public double AveragePrice
            {
                get
                {
                    lock (FillLock)
                    {
                        return CurrentAveragePrice;
                    }
                }
            }

            /// <summary>
            /// Gets the commission of the last fill.
            /// </summary>
            public double LastFillCommission
            {
                get
                {
                    lock (FillLock)
                    {
                        return LastCommission;
                    }
                }
            }

            /// <summary>
            /// Gets the total cumulative commission.
            /// </summary>
            public double CumulativeCommission
            {
                get
                {
                    lock (FillLock)
                    {
                        return CurrentCumulativeCommission;
                    }
                }
            }

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

            /// <summary>
            /// Gets the last order report, <c>null</c> if not any.
            /// </summary>
            public SingleOrderReport LastReport => lastReport;

            /// <summary>
            /// Gets the fill lock. Use when updating quantities/prices after (partial) fill.
            /// </summary>
            protected object FillLock { get; } = new object();

            /// <summary>
            /// Gets the current state of an order as understood by the broker.
            /// </summary>
            protected OrderStatus UnderlyingOrderStatus { get; private set; }

            /// <summary>
            /// Gets or sets the underlying order for this ticket. If there were any successful order replacements, this will be the most recent version.
            /// </summary>
            protected SingleOrder UnderlyingOrder { get; set; }

            /// <summary>
            /// Gets or sets the unique identifier for an order as assigned by the buy-side (institution, broker, intermediary etc.).
            /// </summary>
            protected string UnderlyingClientOrderId { get; set; }

            /// <summary>
            /// Gets or sets the unique identifier for an order as assigned by the sell-side (exchange, ECN, etc.).
            /// </summary>
            protected string UnderlyingOrderId { get; set; }

            /// <summary>
            /// Gets or sets the commission currency.
            /// </summary>
            protected CurrencyCode CommissionCurrency { get; set; } = CurrencyCode.Xxx;

            /// <summary>
            /// Gets or sets the price of the last fill. Use the <see cref="FillLock"/> to update the value.
            /// </summary>
            protected double LastPrice { get; set; }

            /// <summary>
            /// Gets or sets the quantity bought/sold on the last fill. Use the <see cref="FillLock"/> to update the value.
            /// </summary>
            protected double LastQuantity { get; set; }

            /// <summary>
            /// Gets or sets the quantity open for further execution. Use the <see cref="FillLock"/> to update the value.
            /// </summary>
            protected double CurrentLeavesQuantity { get; set; }

            /// <summary>
            /// Gets or sets the total quantity filled. Use the <see cref="FillLock"/> to update the value.
            /// </summary>
            protected double CurrentCumulativeQuantity { get; set; }

            /// <summary>
            /// Gets or sets the average price. Use the <see cref="FillLock"/> to update the value.
            /// </summary>
            protected double CurrentAveragePrice { get; set; }

            /// <summary>
            /// Gets or sets the commission of the last fill. Use the <see cref="FillLock"/> to update the value.
            /// </summary>
            protected double LastCommission { get; set; }

            /// <summary>
            /// Gets or sets the total cumulative commission. Use the <see cref="FillLock"/> to update the value.
            /// </summary>
            protected double CurrentCumulativeCommission { get; set; }

            /// <summary>
            /// Gets a value indicating whether this order is completed.
            /// </summary>
            protected bool IsCompleted =>
                UnderlyingOrderStatus == OrderStatus.Filled ||
                UnderlyingOrderStatus == OrderStatus.Expired ||
                UnderlyingOrderStatus == OrderStatus.Rejected ||
                UnderlyingOrderStatus == OrderStatus.Canceled;

            /// <summary>
            /// Gets a value indicating whether this order can be filled.
            /// </summary>
            protected bool CanFill =>
                UnderlyingOrderStatus == OrderStatus.New ||
                UnderlyingOrderStatus == OrderStatus.PartiallyFilled;

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
                    if (parent.ticketDictionary.ContainsKey(UnderlyingClientOrderId))
                    {
                        throw new InvalidOperationException($"Duplicate client order id: {UnderlyingClientOrderId}");
                    }

                    parent.ticketDictionary.Add(UnderlyingClientOrderId, this);
                }

                HandleSubmit();
            }

            /// <summary>
            /// Implement response to a  <see cref="Submit"/> call. Must assign a unique <see cref="UnderlyingClientOrderId"/>.
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
                UnderlyingOrderStatus = report.Status;
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

            private void OnOrderReport(SingleOrderReport e)
            {
                lock (orderReportLock)
                {
                    if (orderReport != null)
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

            private void OnOrderCompleted()
            {
                lock (orderCompletedLock)
                {
                    if (orderCompleted != null)
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
        }
    }
}
