using System;
using System.Globalization;
using System.Threading;
using Mbs.Trading.Brokers.Commissions;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Trading.Orders;
using Mbs.Trading.Orders.Enumerations;
using Mbs.Trading.Portfolios;
using Mbs.Trading.Time.Timepieces;
using Mbs.Utilities;

namespace Mbs.Trading.Brokers.PaperBrokers
{
    /// <summary>
    /// The paper broker.
    /// </summary>
    public sealed class PaperBroker : Broker, IDisposable
    {
        private static long nextBuySideOrderId;
        private static long nextSellSideOrderId;
        private static long nextSellSideReportId;

        private readonly CompareAndSwapQueue<Action> sellSideQueue = new CompareAndSwapQueue<Action>();
        private AutoResetEventThread sellSideThread;
        private double fillQuantityRatio;
        private volatile bool sellSideActive = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperBroker"/> class.
        /// </summary>
        /// <param name="timepiece">Provides the simulated time. Set to <c>null</c> to use the real time.</param>
        /// <param name="dataPublisher">The data publisher. Used to monitor prices.</param>
        /// <param name="commission">The commission provider.</param>
        /// <param name="fillQuantityRatio">If not zero, allows partial fills and specifies the quantity fill ratio. If zero, forbids the partial fills. The value range must be from 0 to 1 (inclusive).</param>
        /// <param name="fillOnQuote">The fill on quote mode.</param>
        /// <param name="fillOnTrade">The fill on trade mode.</param>
        /// <param name="fillOnOhlcv">The fill on ohlcv mode.</param>
        public PaperBroker(
            ITimepiece timepiece,
            IDataPublisher dataPublisher,
            ICommission commission,
            double fillQuantityRatio,
            FillOnQuote fillOnQuote,
            FillOnTrade fillOnTrade,
            FillOnOhlcv fillOnOhlcv)
        {
            Timepiece = timepiece;
            DataPublisher = dataPublisher;
            Commission = commission;
            if (fillQuantityRatio < 0d)
            {
                fillQuantityRatio = 0d;
            }

            if (fillQuantityRatio > 1d)
            {
                fillQuantityRatio = 1d;
            }

            this.fillQuantityRatio = fillQuantityRatio;
            FillOnQuote = fillOnQuote;
            FillOnTrade = fillOnTrade;
            FillOnOhlcv = fillOnOhlcv;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaperBroker"/> class.
        /// </summary>
        public PaperBroker()
        {
        }

        /// <summary>
        /// Gets or sets the data publisher interface.
        /// </summary>
        public IDataPublisher DataPublisher { get; set; }

        /// <summary>
        /// Gets or sets the commission interface.
        /// </summary>
        public ICommission Commission { get; set; }

        /// <summary>
        /// Gets or sets the timepiece interface.
        /// </summary>
        public ITimepiece Timepiece { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sell side is asynchronous.
        /// </summary>
        public bool SellSideAsynchronous { get; set; }

        /// <summary>
        /// Gets or sets a non-zero value ⋲(0,1] allows partial fills and specifies the fraction of the volume available for order filling.
        /// The value of zero forbids partial fills.
        /// </summary>
        public double FillQuantityRatio
        {
            get => fillQuantityRatio;
            set
            {
                if (value < 0d)
                {
                    value = 0d;
                }
                else if (value > 1d)
                {
                    value = 1d;
                }

                fillQuantityRatio = value;
            }
        }

        /// <summary>
        /// Gets or sets the fill on quote mode.
        /// </summary>
        public FillOnQuote FillOnQuote { get; set; } = FillOnQuote.Last;

        /// <summary>
        /// Gets or sets the fill on trade mode.
        /// </summary>
        public FillOnTrade FillOnTrade { get; set; } = FillOnTrade.Last;

        /// <summary>
        /// Gets or sets the fill on ohlcv mode.
        /// </summary>
        public FillOnOhlcv FillOnOhlcv { get; set; } = FillOnOhlcv.LastClose;

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// The implementation.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="parent">The parent broker.</param>
        /// <param name="reportAction">Called when a report has been received.</param>
        /// <param name="completionAction">Called when the order has been completed.</param>
        /// <returns>The order ticket interface.</returns>
        protected override ISingleOrderTicket CreateOrderTicket(
            SingleOrder order,
            Broker parent,
            Action<ISingleOrderTicket, SingleOrderReport> reportAction,
            Action<ISingleOrderTicket> completionAction)
        {
            return new PaperSingleOrderTicket(
                order,
                parent,
                DataPublisher,
                Commission,
                reportAction,
                completionAction,
                SellSideAction,
                Time,
                NextBuySideOrderId,
                NextSellSideOrderId,
                NextSellSideReportId,
                fillQuantityRatio,
                FillOnQuote,
                FillOnTrade,
                FillOnOhlcv);
        }

        private DateTime Time()
        {
            ITimepiece t = Timepiece;
            return t?.Time ?? DateTime.Now;
        }

        private string NextBuySideOrderId()
        {
            return $"{Time():yyyyMMddHHmmss}-pc-{Interlocked.Increment(ref nextBuySideOrderId)}"
                .ToString(CultureInfo.InvariantCulture);
        }

        private string NextSellSideOrderId()
        {
            return $"{Time():yyyyMMddHHmmss}-po-{Interlocked.Increment(ref nextSellSideOrderId)}"
                .ToString(CultureInfo.InvariantCulture);
        }

        private string NextSellSideReportId()
        {
            return $"{Time():yyyyMMddHHmmss}-pr-{Interlocked.Increment(ref nextSellSideReportId)}"
                .ToString(CultureInfo.InvariantCulture);
        }

        private void SellSideAction(Action action)
        {
            if (SellSideAsynchronous)
            {
                if (sellSideThread == null)
                {
                    sellSideThread = new AutoResetEventThread(() =>
                    {
                        while (sellSideActive)
                        {
                            Action a;
                            while ((a = sellSideQueue.Dequeue()) != null)
                            {
                                a();
                            }

                            sellSideThread?.AutoResetEvent.WaitOne(10000, false);
                        }

                        if (sellSideThread != null)
                        {
                            sellSideThread.AutoResetEvent.Close();
                            sellSideThread.Dispose();
                            sellSideThread = null;
                        }
                    });
                    sellSideThread.Thread.IsBackground = true;
                    sellSideThread.Thread.Start();
                }

                sellSideQueue.Enqueue(action);
                sellSideThread.AutoResetEvent.Set();
            }
            else
            {
                action();
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (sellSideThread == null)
                {
                    return;
                }

                sellSideActive = false;
                sellSideThread.AutoResetEvent?.Close();

                sellSideThread.Dispose();
                sellSideThread = null;
            }
        }

        /// <summary>
        /// The paper broker order ticket implementation.
        /// </summary>
        private sealed class PaperSingleOrderTicket : SingleOrderTicket
        {
            private readonly Action<Action> sellSideAction;
            private readonly Func<DateTime> currentTime;
            private readonly Func<string> sellSideOrderId;
            private readonly Func<string> sellSideReportId;
            private readonly IDataPublisher dataPublisher;
            private readonly FillOnQuote fillOnQuote;
            private readonly FillOnTrade fillOnTrade;
            private readonly FillOnOhlcv fillOnOhlcv;
            private readonly bool allowPartialFills;
            private readonly double fillQuantityRatio;
            private readonly ICommission commission;
            private readonly Account account;
            private readonly Portfolio portfolio;
            private double trailingPrice;
            private bool isStopLimitReady;
            private Quote lastQuote;
            private Trade lastTrade;
            private Ohlcv lastOhlcv;
            private ISubscription<Quote> quoteSubscription;
            private ISubscription<Trade> tradeSubscription;
            private ISubscription<Ohlcv> ohlcvSubscription;
            private DateTime expiration;

            /// <summary>
            /// Initializes a new instance of the <see cref="PaperSingleOrderTicket"/> class.
            /// </summary>
            /// <param name="order">The order.</param>
            /// <param name="parent">The parent broker.</param>
            /// <param name="dataPublisher">The data publisher.</param>
            /// <param name="commission">The commission interface.</param>
            /// <param name="reportAction">Called when a report has been received.</param>
            /// <param name="completionAction">Called when the order has been completed.</param>
            /// <param name="sellSideAction">The sellSide action invoker.</param>
            /// <param name="currentTime">The current time function.</param>
            /// <param name="buySideOrderId">The buy-side order id generator.</param>
            /// <param name="sellSideOrderId">The sell-side order id generator.</param>
            /// <param name="sellSideReportId">The sell-side report id generator.</param>
            /// <param name="fillQuantityRatio">A non-zero value ⋲(0,1] allows partial fills and specifies the fraction of the volume available for order filling. The value of zero forbids partial fills.</param>
            /// <param name="fillOnQuote">The fill on quote mode.</param>
            /// <param name="fillOnTrade">The fill on trade mode.</param>
            /// <param name="fillOnOhlcv">The fill on ohlcv mode.</param>
            internal PaperSingleOrderTicket(
                SingleOrder order,
                Broker parent,
                IDataPublisher dataPublisher,
                ICommission commission,
                Action<ISingleOrderTicket, SingleOrderReport> reportAction,
                Action<ISingleOrderTicket> completionAction,
                Action<Action> sellSideAction,
                Func<DateTime> currentTime,
                Func<string> buySideOrderId,
                Func<string> sellSideOrderId,
                Func<string> sellSideReportId,
                double fillQuantityRatio,
                FillOnQuote fillOnQuote,
                FillOnTrade fillOnTrade,
                FillOnOhlcv fillOnOhlcv)
                : base(order, parent, reportAction, completionAction)
            {
                this.dataPublisher = dataPublisher;
                this.commission = commission;
                CommissionCurrency = commission?.Currency ?? order.Instrument.Currency;
                this.sellSideAction = sellSideAction;
                this.currentTime = currentTime;
                this.sellSideOrderId = sellSideOrderId;
                this.sellSideReportId = sellSideReportId;
                allowPartialFills = fillQuantityRatio > 0d;
                this.fillQuantityRatio = fillQuantityRatio;
                this.fillOnQuote = fillOnQuote;
                this.fillOnTrade = fillOnTrade;
                this.fillOnOhlcv = fillOnOhlcv;

                UnderlyingClientOrderId = buySideOrderId();
                order.CreationTime = this.currentTime();
                CurrentLeavesQuantity = order.Quantity;
                account = order.Account;
                portfolio = order.Portfolio;
                Submit();
            }

            /// <inheritdoc />
            protected override void HandleSubmit()
            {
                sellSideAction?.Invoke(() =>
                {
                    OrderStatus rollbackStatus = UnderlyingOrderStatus;
                    UnderlyingOrderId = sellSideOrderId();
                    SendReport(OrderReportType.PendingNew, OrderStatus.PendingNew);

                    string reason = rollbackStatus == OrderStatus.Accepted
                        ? null
                        : $"Cannot submit order in the {rollbackStatus} state.";

                    SingleOrder singleOrder = UnderlyingOrder;
                    Instrument instrument = singleOrder.Instrument;
                    bool isBuy = IsBuy(singleOrder.Side);
                    if (reason == null && !isBuy && !IsSell(singleOrder.Side))
                    {
                        reason = $"Order side {UnderlyingOrder.Side} is not supported.";
                    }

                    if (reason == null)
                    {
                        switch (UnderlyingOrder.TimeInForce)
                        {
                            case OrderTimeInForce.AtOpen:
                                // TODO: begin of session time? What is 'session' in 24-hour Forex trading? +1day: weekends, holidays?
                                expiration = UnderlyingOrder.CreationTime.Date.AddDays(1);
                                break;
                            case OrderTimeInForce.AtClose:
                            case OrderTimeInForce.Day:
                                // TODO: end of session time? What is 'session' in 24-hour Forex trading?  +1day: weekends, holidays?
                                expiration = UnderlyingOrder.CreationTime.Date.AddDays(1);
                                break;
                            case OrderTimeInForce.ImmediateOrCancel:
                            case OrderTimeInForce.FillOrKill:
                                expiration = UnderlyingOrder.CreationTime;
                                break;
                            case OrderTimeInForce.GoodTillCanceled:
                                expiration = singleOrder.CreationTime.AddMonths(3);
                                break;
                            case OrderTimeInForce.GoodTillDate:
                                expiration = UnderlyingOrder.ExpirationTime;
                                break;
                            default:
                                reason = $"TimeInForce {UnderlyingOrder.TimeInForce} is not supported.";
                                break;
                        }
                    }

                    if (reason != null)
                    {
                        SendReportWithCompletion(OrderReportType.Rejected, OrderStatus.Rejected, reason);
                        return;
                    }

                    SendReport(OrderReportType.New, OrderStatus.New);
                    if (singleOrder.Type == OrderType.TrailingStop)
                    {
                        trailingPrice = isBuy ? double.MaxValue : double.MinValue;
                    }

                    if (fillOnQuote == FillOnQuote.Last)
                    {
                        var quote = dataPublisher.Last<Quote>(instrument);
                        if (quote != null)
                        {
                            Process(singleOrder, quote, null, null);
                            if (IsCompleted)
                            {
                                return;
                            }
                        }
                    }

                    if (fillOnTrade == FillOnTrade.Last)
                    {
                        var trade = dataPublisher.Last<Trade>(instrument);
                        if (trade != null)
                        {
                            Process(singleOrder, null, trade, null);
                            if (IsCompleted)
                            {
                                return;
                            }
                        }
                    }

                    if (fillOnOhlcv == FillOnOhlcv.LastClose)
                    {
                        var ohlcv = dataPublisher.Last<Ohlcv>(instrument);
                        if (ohlcv != null)
                        {
                            Process(singleOrder, null, null, ohlcv);
                            if (IsCompleted)
                            {
                                return;
                            }
                        }
                    }

                    if (fillOnQuote != FillOnQuote.None)
                    {
                        quoteSubscription = dataPublisher.Monitor<Quote>(instrument);
                        if (quoteSubscription != null)
                        {
                            quoteSubscription.SubscriptionAction += OnQuote;
                            if (!quoteSubscription.IsConnected)
                            {
                                quoteSubscription.Connect();
                            }
                        }
                    }

                    if (fillOnTrade != FillOnTrade.None)
                    {
                        tradeSubscription = dataPublisher.Monitor<Trade>(instrument);
                        if (tradeSubscription != null)
                        {
                            tradeSubscription.SubscriptionAction += OnTrade;
                            if (!tradeSubscription.IsConnected)
                            {
                                tradeSubscription.Connect();
                            }
                        }
                    }

                    if (fillOnOhlcv != FillOnOhlcv.None)
                    {
                        ohlcvSubscription = dataPublisher.Monitor<Ohlcv>(instrument);
                        if (ohlcvSubscription != null)
                        {
                            ohlcvSubscription.SubscriptionAction += OnOhlcv;
                            if (!ohlcvSubscription.IsConnected)
                            {
                                ohlcvSubscription.Connect();
                            }
                        }
                    }
                });
            }

            /// <inheritdoc />
            protected override void HandleReplace(SingleOrder replacementOrder)
            {
                sellSideAction(() =>
                {
                    OrderStatus rollbackStatus = UnderlyingOrderStatus;
                    SendReport(OrderReportType.PendingReplace, OrderStatus.PendingReplace);

                    // Validate the replacement.
                    string reason = null;
                    SingleOrder sourceOrder = UnderlyingOrder;
                    switch (rollbackStatus)
                    {
                        case OrderStatus.Filled:
                        case OrderStatus.Expired:
                        case OrderStatus.Canceled:
                        case OrderStatus.Rejected:
                            reason = $"Cannot replace already completed order in the {rollbackStatus} state.";
                            break;
                        case OrderStatus.New:
                        case OrderStatus.PartiallyFilled:
                            break;
                        default:
                            reason = $"Cannot replace order in the {rollbackStatus} state.";
                            break;
                    }

                    if (reason == null)
                    {
                        if (sourceOrder.Instrument != replacementOrder.Instrument)
                        {
                            reason = "Cannot replace order instrument.";
                        }
                        else if (sourceOrder.Type != replacementOrder.Type)
                        {
                            reason = "Cannot replace order type.";
                        }
                        else if (sourceOrder.Side != replacementOrder.Side)
                        {
                            reason = "Cannot replace order side.";
                        }
                    }

                    if (reason != null)
                    {
                        SendReport(OrderReportType.ReplaceRejected, rollbackStatus, reason, sourceOrder, replacementOrder);
                    }
                    else
                    {
                        double leaves;
                        lock (FillLock)
                        {
                            leaves = replacementOrder.Quantity - CurrentCumulativeQuantity;
                            if (leaves < 0d)
                            {
                                reason = "Cannot replace because of negative leaves quantity.";
                            }
                            else
                            {
                                CurrentLeavesQuantity = leaves;
                                if (leaves < double.Epsilon && commission != null)
                                {
                                    double amount = commission.Amount(UnderlyingOrder, 0d, 0d, 0d, CurrentCumulativeQuantity, CurrentAveragePrice, CurrentCumulativeCommission);
                                    if (amount > 0d)
                                    {
                                        LastCommission = amount;
                                        CurrentCumulativeCommission += amount;
                                    }
                                }

                                UnderlyingOrder = replacementOrder;
                            }
                        }

                        if (reason != null)
                        {
                            SendReport(OrderReportType.ReplaceRejected, rollbackStatus, reason, sourceOrder, replacementOrder);
                            return;
                        }

                        SendReport(OrderReportType.Replaced, rollbackStatus, null, sourceOrder, replacementOrder);
                        if (Math.Abs(leaves) < double.Epsilon)
                        {
                            Unsubscribe();
                            OnReportWithCompletion(new SingleOrderReport(
                                currentTime(),
                                sellSideReportId(),
                                OrderReportType.Filled,
                                OrderStatus.Filled,
                                string.Empty,
                                LastPrice,
                                LastQuantity,
                                CurrentLeavesQuantity,
                                CurrentCumulativeQuantity,
                                CurrentAveragePrice,
                                LastCommission,
                                CurrentCumulativeCommission,
                                CommissionCurrency));
                        }
                        else
                        {
                            if (replacementOrder.Type == OrderType.StopLimit)
                            {
                                isStopLimitReady = false;
                            }
                        }
                    }
                });
            }

            /// <inheritdoc />
            protected override void HandleCancel()
            {
                sellSideAction(() =>
                {
                    OrderStatus rollbackStatus = UnderlyingOrderStatus;
                    SendReport(OrderReportType.PendingCancel, OrderStatus.PendingCancel);

                    // Validate the cancellation.
                    string reason = null;
                    switch (rollbackStatus)
                    {
                        case OrderStatus.New:
                        case OrderStatus.PartiallyFilled:
                            break;
                        default:
                            reason = $"Cannot cancel order in the {rollbackStatus} state.";
                            break;
                    }

                    if (reason != null)
                    {
                        SendReport(OrderReportType.CancelRejected, rollbackStatus, reason);
                    }
                    else
                    {
                        Unsubscribe();
                        lock (FillLock)
                        {
                            CurrentLeavesQuantity = 0d;
                            if (commission != null)
                            {
                                double amount = commission.Amount(UnderlyingOrder, 0d, 0d, 0d, CurrentCumulativeQuantity, CurrentAveragePrice, CurrentCumulativeCommission);
                                if (amount > 0d)
                                {
                                    LastCommission = amount;
                                    CurrentCumulativeCommission += amount;
                                }
                            }
                        }

                        SendReportWithCompletion(OrderReportType.Canceled, OrderStatus.Canceled);
                    }
                });
            }

            private static bool IsBuy(OrderSide orderSide)
            {
                return orderSide == OrderSide.Buy || orderSide == OrderSide.BuyMinus;
            }

            private static bool IsSell(OrderSide orderSide)
            {
                switch (orderSide)
                {
                    case OrderSide.Sell:
                    case OrderSide.SellPlus:
                    case OrderSide.SellShort:
                    case OrderSide.SellShortExempt:
                        return true;
                }

                return false;
            }

            private static bool IsShort(OrderSide orderSide)
            {
                switch (orderSide)
                {
                    case OrderSide.SellPlus:
                    case OrderSide.SellShort:
                    case OrderSide.SellShortExempt:
                        return true;
                }

                return false;
            }

            private void SendReport(OrderReportType type, OrderStatus status)
            {
                OnReport(new SingleOrderReport(
                    currentTime(),
                    sellSideReportId(),
                    type,
                    status,
                    null,
                    LastPrice,
                    LastQuantity,
                    CurrentLeavesQuantity,
                    CurrentCumulativeQuantity,
                    CurrentAveragePrice,
                    LastCommission,
                    CurrentCumulativeCommission,
                    CommissionCurrency));
            }

            private void SendReport(OrderReportType type, OrderStatus status, string text)
            {
                OnReport(new SingleOrderReport(
                    currentTime(),
                    sellSideReportId(),
                    type,
                    status,
                    text,
                    LastPrice,
                    LastQuantity,
                    CurrentLeavesQuantity,
                    CurrentCumulativeQuantity,
                    CurrentAveragePrice,
                    LastCommission,
                    CurrentCumulativeCommission,
                    CommissionCurrency));
            }

            private void SendReport(OrderReportType type, OrderStatus status, string text, SingleOrder replaceSourceOrder, SingleOrder replaceTargetOrder)
            {
                OnReport(new SingleOrderReport(
                    currentTime(),
                    sellSideReportId(),
                    type,
                    status,
                    text,
                    LastPrice,
                    LastQuantity,
                    CurrentLeavesQuantity,
                    CurrentCumulativeQuantity,
                    CurrentAveragePrice,
                    LastCommission,
                    CurrentCumulativeCommission,
                    CommissionCurrency,
                    replaceSourceOrder,
                    replaceTargetOrder));
            }

            private void SendReportWithCompletion(OrderReportType type, OrderStatus status)
            {
                OnReportWithCompletion(new SingleOrderReport(
                    currentTime(),
                    sellSideReportId(),
                    type,
                    status,
                    null,
                    LastPrice,
                    LastQuantity,
                    CurrentLeavesQuantity,
                    CurrentCumulativeQuantity,
                    CurrentAveragePrice,
                    LastCommission,
                    CurrentCumulativeCommission,
                    CommissionCurrency));
            }

            private void SendReportWithCompletion(OrderReportType type, OrderStatus status, string text)
            {
                OnReportWithCompletion(new SingleOrderReport(
                    currentTime(),
                    sellSideReportId(),
                    type,
                    status,
                    text,
                    LastPrice,
                    LastQuantity,
                    CurrentLeavesQuantity,
                    CurrentCumulativeQuantity,
                    CurrentAveragePrice,
                    LastCommission,
                    CurrentCumulativeCommission,
                    CommissionCurrency));
            }

            private void Process(SingleOrder singleOrder, Quote quote, Trade trade, Ohlcv ohlcv)
            {
                double priceMarket = CalculatePriceAndQuantity(
                    singleOrder,
                    quote,
                    trade,
                    ohlcv,
                    out double priceHigh,
                    out double priceLow,
                    out double quantity);

                if (priceMarket > 0d && quantity > 0d)
                {
                    TryToExecute(singleOrder, priceMarket, priceHigh, priceLow, quantity);
                }
            }

#pragma warning disable S907 // "goto" statement should not be used
            private double CalculatePriceAndQuantity(SingleOrder singleOrder, Quote quote, Trade trade, Ohlcv ohlcv, out double high, out double low, out double quantity)
            {
                double price = double.NaN;
                if (quote != null && fillOnQuote != FillOnQuote.None)
                {
                    if (singleOrder.TimeInForce == OrderTimeInForce.AtOpen)
                    {
                        if (expiration > quote.Time)
                        {
                            goto labelSkipped;
                        }
                    }
                    else if (singleOrder.TimeInForce == OrderTimeInForce.AtClose)
                    {
                        if (expiration > quote.Time)
                        {
                            goto labelSkipped;
                        }

                        if (lastQuote != null)
                        {
                            quote = lastQuote;
                        }
                    }

                    if (IsBuy(singleOrder.Side))
                    {
                        price = quote.AskPrice;
                        if (!double.IsNaN(price))
                        {
                            quantity = singleOrder.Quantity;
                            if (allowPartialFills)
                            {
                                double askSize = quote.AskSize;
                                askSize = double.IsNaN(askSize) ? 0 : Math.Floor(askSize * fillQuantityRatio);
                                quantity = Math.Min(askSize, quantity);
                                if (singleOrder.TimeInForce == OrderTimeInForce.FillOrKill && quantity < singleOrder.Quantity)
                                {
                                    quantity = 0d;
                                }
                            }

                            high = price;
                            low = price;
                            return price;
                        }
                    }
                    else if (IsSell(singleOrder.Side))
                    {
                        price = quote.BidPrice;
                        if (!double.IsNaN(price))
                        {
                            quantity = singleOrder.Quantity;
                            if (allowPartialFills)
                            {
                                double bidSize = quote.BidSize;
                                bidSize = double.IsNaN(bidSize) ? 0 : Math.Floor(bidSize * fillQuantityRatio);
                                quantity = Math.Min(bidSize, quantity);
                                if (singleOrder.TimeInForce == OrderTimeInForce.FillOrKill && quantity < singleOrder.Quantity)
                                {
                                    quantity = 0d;
                                }
                            }

                            high = price;
                            low = price;
                            return price;
                        }
                    }
                }

                if (trade != null && fillOnTrade != FillOnTrade.None)
                {
                    price = trade.Price;
                    if (!double.IsNaN(price))
                    {
                        if (singleOrder.TimeInForce == OrderTimeInForce.AtOpen)
                        {
                            if (expiration > trade.Time)
                            {
                                goto labelSkipped;
                            }
                        }
                        else if (singleOrder.TimeInForce == OrderTimeInForce.AtClose)
                        {
                            if (expiration > trade.Time)
                            {
                                goto labelSkipped;
                            }

                            if (lastTrade != null)
                            {
                                trade = lastTrade;
                            }
                        }

                        quantity = singleOrder.Quantity;
                        if (allowPartialFills)
                        {
                            double volume = trade.Volume;
                            volume = double.IsNaN(volume) ? 0 : Math.Floor(volume * fillQuantityRatio);
                            quantity = Math.Min(volume, quantity);
                            if (singleOrder.TimeInForce == OrderTimeInForce.FillOrKill && quantity < singleOrder.Quantity)
                            {
                                quantity = 0d;
                            }
                        }

                        high = price;
                        low = price;
                        return price;
                    }
                }

                if (ohlcv != null && fillOnOhlcv != FillOnOhlcv.None)
                {
                    if (singleOrder.TimeInForce == OrderTimeInForce.AtOpen)
                    {
                        if (expiration > ohlcv.Time)
                        {
                            goto labelSkipped;
                        }
                    }
                    else if (singleOrder.TimeInForce == OrderTimeInForce.AtClose)
                    {
                        if (expiration > ohlcv.Time)
                        {
                            goto labelSkipped;
                        }

                        if (lastOhlcv != null)
                        {
                            ohlcv = lastOhlcv;
                        }
                    }

                    bool isBuy = IsBuy(singleOrder.Side);
                    switch (fillOnOhlcv)
                    {
                        case FillOnOhlcv.LastClose:
                        case FillOnOhlcv.NextClose:
                            price = ohlcv.Close;
                            break;
                        case FillOnOhlcv.NextOpen:
                            price = ohlcv.Open;
                            break;
                        case FillOnOhlcv.NextBest:
                            price = isBuy ? ohlcv.Low : ohlcv.High;
                            break;
                        case FillOnOhlcv.NextWorst:
                            price = isBuy ? ohlcv.High : ohlcv.Low;
                            break;
                        case FillOnOhlcv.NextMedian:
                            price = ohlcv.Median;
                            break;
                        case FillOnOhlcv.NextTypical:
                            price = ohlcv.Typical;
                            break;
                        case FillOnOhlcv.NextWeighted:
                            price = ohlcv.Weighted;
                            break;
                    }

                    if (!double.IsNaN(price))
                    {
                        quantity = singleOrder.Quantity;
                        if (allowPartialFills)
                        {
                            double volume = ohlcv.IsVolumeEmpty ? 0 : Math.Floor(ohlcv.Volume * fillQuantityRatio);
                            quantity = Math.Min(volume, quantity);
                            if (singleOrder.TimeInForce == OrderTimeInForce.FillOrKill && quantity < singleOrder.Quantity)
                            {
                                quantity = 0d;
                            }
                        }

                        low = ohlcv.Low;
                        high = ohlcv.High;
                        if (ohlcv.IsLowEmpty || ohlcv.IsHighEmpty)
                        {
                            if (!ohlcv.IsLowEmpty)
                            {
                                // Low is initialized, high is not.
                                low = ohlcv.Low;
                                if (!ohlcv.IsOpenEmpty)
                                {
                                    high = ohlcv.IsCloseEmpty
                                        ? ohlcv.Open // Open is initialized, close is not.
                                        : Math.Max(ohlcv.Open, ohlcv.Close); // Both open and close are initialized.
                                }
                                else if (!ohlcv.IsCloseEmpty)
                                {
                                    high = ohlcv.Close; // Close is initialized, open is not.
                                }
                            }
                            else
                            {
                                // High is initialized, low is not.
                                high = ohlcv.High;
                                if (!ohlcv.IsOpenEmpty)
                                {
                                    low = ohlcv.IsCloseEmpty
                                        ? ohlcv.Open // Open is initialized, close is not.
                                        : Math.Min(ohlcv.Open, ohlcv.Close); // Both open and close are initialized.
                                }
                                else if (!ohlcv.IsCloseEmpty)
                                {
                                    low = ohlcv.Close; // Close is initialized, open is not.
                                }
                            }
                        }

                        return price;
                    }
                }

                labelSkipped:
                high = 0d;
                low = 0d;
                quantity = 0d;
                return 0d;
            }

            private void TryToExecute(SingleOrder singleOrder, double priceMarket, double priceHigh, double priceLow, double quantity)
            {
                switch (singleOrder.Type)
                {
                    case OrderType.Market:
                    labelMarket:
                        ValidateAndSendExecutionReport(singleOrder, priceMarket, quantity);
                        break;
                    case OrderType.MarketIfTouched:
                        // When limit is hit, convert it to a market order.
                        if (IsBuy(singleOrder.Side))
                        {
                            // Better than the limit price.
                            if (priceMarket <= singleOrder.LimitPrice)
                            {
                                goto labelMarket;
                            }
                        }
                        else if (IsSell(singleOrder.Side) && priceMarket >= singleOrder.LimitPrice)
                        {
                            // Better than the limit price.
                            goto labelMarket;
                        }

                        goto labelLimit;
                    case OrderType.Limit:
                    labelLimit:
                        if (IsBuy(singleOrder.Side))
                        {
                            if (priceLow <= singleOrder.LimitPrice)
                            {
                                ValidateAndSendExecutionReport(singleOrder, singleOrder.LimitPrice, quantity);
                            }
                        }
                        else if (IsSell(singleOrder.Side) && priceHigh >= singleOrder.LimitPrice)
                        {
                            ValidateAndSendExecutionReport(singleOrder, singleOrder.LimitPrice, quantity);
                        }

                        break;
                    case OrderType.Stop:
                        if (IsBuy(singleOrder.Side))
                        {
                            if (priceHigh >= singleOrder.StopPrice)
                            {
                                ValidateAndSendExecutionReport(singleOrder, singleOrder.StopPrice, quantity);
                            }
                        }
                        else if (IsSell(singleOrder.Side) && priceLow <= singleOrder.StopPrice)
                        {
                            ValidateAndSendExecutionReport(singleOrder, singleOrder.StopPrice, quantity);
                        }

                        break;
                    case OrderType.TrailingStop:
                        if (IsBuy(singleOrder.Side))
                        {
                            trailingPrice = Math.Min(trailingPrice, priceLow + singleOrder.TrailingDistance);
                            if (priceHigh >= trailingPrice)
                            {
                                ValidateAndSendExecutionReport(singleOrder, trailingPrice, quantity);
                            }
                        }
                        else if (IsSell(singleOrder.Side))
                        {
                            trailingPrice = Math.Max(trailingPrice, priceHigh - singleOrder.TrailingDistance);
                            if (priceLow <= trailingPrice)
                            {
                                ValidateAndSendExecutionReport(singleOrder, trailingPrice, quantity);
                            }
                        }

                        break;
                    case OrderType.StopLimit:
                        if (!isStopLimitReady)
                        {
                            if (IsBuy(singleOrder.Side))
                            {
                                if (priceHigh >= singleOrder.StopPrice)
                                {
                                    isStopLimitReady = true;
                                }
                            }
                            else if (IsSell(singleOrder.Side) && priceLow <= singleOrder.StopPrice)
                            {
                                isStopLimitReady = true;
                            }
                        }

                        if (isStopLimitReady)
                        {
                            goto labelLimit;
                        }

                        break;
                }
            }
#pragma warning restore S907 // "goto" statement should not be used

            private void ValidateAndSendExecutionReport(SingleOrder singleOrder, double price, double quantity)
            {
                if (ValidateAccountAndPortfolio(singleOrder, price, quantity))
                {
                    SendExecutionReport(singleOrder, price, quantity);
                }
            }

            private void SendExecutionReport(SingleOrder singleOrder, double price, double quantity)
            {
                if (IsCompleted)
                {
                    return;
                }

                SingleOrderReport report;
                bool completed;
                lock (FillLock)
                {
                    if (quantity < CurrentLeavesQuantity)
                    {
                        completed = false;
                        CurrentAveragePrice = (CurrentAveragePrice * CurrentCumulativeQuantity + price * quantity) / (CurrentCumulativeQuantity + quantity);
                        LastQuantity = quantity;
                        CurrentLeavesQuantity -= quantity;
                        LastPrice = price;
                        CurrentCumulativeQuantity += quantity;
                        if (commission != null)
                        {
                            price = commission.Amount(singleOrder, price, quantity, CurrentLeavesQuantity, CurrentCumulativeQuantity, CurrentAveragePrice, CurrentCumulativeCommission);
                            LastCommission = price;
                            CurrentCumulativeCommission += price;
                        }

                        report = new SingleOrderReport(
                            currentTime(),
                            sellSideReportId(),
                            OrderReportType.PartiallyFilled,
                            OrderStatus.PartiallyFilled,
                            string.Empty,
                            LastPrice,
                            LastQuantity,
                            CurrentLeavesQuantity,
                            CurrentCumulativeQuantity,
                            CurrentAveragePrice,
                            LastCommission,
                            CurrentCumulativeCommission,
                            CommissionCurrency);
                    }
                    else
                    {
                        completed = true;
                        Unsubscribe();
                        quantity = CurrentLeavesQuantity;
                        CurrentAveragePrice = (CurrentAveragePrice * CurrentCumulativeQuantity + price * quantity) / (CurrentCumulativeQuantity + quantity);
                        LastQuantity = quantity;
                        CurrentLeavesQuantity = 0d;
                        CurrentCumulativeQuantity += quantity;
                        LastPrice = price;
                        if (commission != null)
                        {
                            price = commission.Amount(singleOrder, price, quantity, 0d, CurrentCumulativeQuantity, CurrentAveragePrice, CurrentCumulativeCommission);
                            LastCommission = price;
                            CurrentCumulativeCommission += price;
                        }

                        report = new SingleOrderReport(
                            currentTime(),
                            sellSideReportId(),
                            OrderReportType.Filled,
                            OrderStatus.Filled,
                            string.Empty,
                            LastPrice,
                            LastQuantity,
                            CurrentLeavesQuantity,
                            CurrentCumulativeQuantity,
                            CurrentAveragePrice,
                            LastCommission,
                            CurrentCumulativeCommission,
                            CommissionCurrency);
                    }

                    Log.Debug(
                        $"PaperBroker.SendExecutionReport: ({(IsBuy(singleOrder.Side) ? "buy" : "sell")}){LastQuantity}@{LastPrice} + commission={LastCommission}/{CurrentCumulativeCommission}");
                }

                if (completed)
                {
                    OnReportWithCompletion(report);
                }
                else
                {
                    OnReport(report);
                }

                portfolio?.Add(new PortfolioExecution(this, report));
            }

            private void Unsubscribe()
            {
                if (quoteSubscription != null)
                {
                    quoteSubscription.SubscriptionAction -= OnQuote;
                    quoteSubscription = null;
                }

                if (tradeSubscription != null)
                {
                    tradeSubscription.SubscriptionAction -= OnTrade;
                    tradeSubscription = null;
                }

                if (ohlcvSubscription != null)
                {
                    ohlcvSubscription.SubscriptionAction -= OnOhlcv;
                    ohlcvSubscription = null;
                }
            }

            private void OnQuote(Quote quote)
            {
                if (quote == null)
                {
                    return;
                }

                if (CanFill)
                {
                    SingleOrder singleOrder = UnderlyingOrder;
                    Process(singleOrder, quote, null, null);
                    ValidateTimeInForce(singleOrder, quote.Time);
                }

                lastQuote = quote;
            }

            private void OnTrade(Trade trade)
            {
                if (trade == null)
                {
                    return;
                }

                if (CanFill)
                {
                    SingleOrder singleOrder = UnderlyingOrder;
                    Process(singleOrder, null, trade, null);
                    ValidateTimeInForce(singleOrder, trade.Time);
                }

                lastTrade = trade;
            }

            private void OnOhlcv(Ohlcv ohlcv)
            {
                if (ohlcv == null)
                {
                    return;
                }

                if (CanFill)
                {
                    SingleOrder singleOrder = UnderlyingOrder;
                    Process(singleOrder, null, null, ohlcv);
                    ValidateTimeInForce(singleOrder, ohlcv.Time);
                }

                lastOhlcv = ohlcv;
            }

            private double CommissionValue(SingleOrder singleOrder, double price, double quantity)
            {
                if (commission == null)
                {
                    return 0;
                }

                lock (FillLock)
                {
                    if (quantity < CurrentLeavesQuantity)
                    {
                        double avgPrice = (CurrentAveragePrice * CurrentCumulativeQuantity + price * quantity) / (CurrentCumulativeQuantity + quantity);
                        return commission.Amount(singleOrder, price, quantity, CurrentLeavesQuantity - quantity, CurrentCumulativeQuantity + quantity, avgPrice, CurrentCumulativeCommission);
                    }
                    else
                    {
                        quantity = CurrentLeavesQuantity;
                        double avgPrice = (CurrentAveragePrice * CurrentCumulativeQuantity + price * quantity) / (CurrentCumulativeQuantity + quantity);
                        return commission.Amount(singleOrder, price, quantity, 0d, CurrentCumulativeQuantity + quantity, avgPrice, CurrentCumulativeCommission);
                    }
                }
            }

            private bool ValidateAccountAndPortfolio(SingleOrder singleOrder, double price, double quantity)
            {
                Instrument instrument = singleOrder.Instrument;
                OrderSide side = singleOrder.Side;
                double commissionValue = CommissionValue(singleOrder, price, quantity);
                if (IsBuy(side))
                {
                    if (account != null)
                    {
                        double buyingPower = account.Balance(instrument.Currency);

                        // TODO: Is instrument.Margin an absolute value or a percentage?
                        if ((price + instrument.Margin) * quantity + commissionValue > buyingPower)
                        {
                            string reason;
                            if (instrument.Margin > 0)
                            {
                                reason = string.Format(
                                    CultureInfo.InvariantCulture,
                                    "Not enough buying power: (price={0} + initial margin={1}) * qty={2} + commission={3} > buyingPower={4}.",
                                    price,
                                    instrument.Margin,
                                    quantity,
                                    commissionValue,
                                    buyingPower);
                            }
                            else
                            {
                                reason = string.Format(
                                    CultureInfo.InvariantCulture,
                                    "Not enough buying power: price={0} * qty={1} + commission={2} > buyingPower={3}.",
                                    price,
                                    quantity,
                                    commissionValue,
                                    buyingPower);
                            }

                            SendReportWithCompletion(OrderReportType.Rejected, OrderStatus.Rejected, reason);
                            Unsubscribe();
                            Log.Debug("PaperBroker.ValidateAccountAndPortfolio: " + reason);
                            return false;
                        }
                    }
                }
                else if (IsSell(side))
                {
                    bool isShort = IsShort(side);
                    if (account != null)
                    {
                        double buyingPower = account.Balance(instrument.Currency);

                        // TODO: initial margin for short sell: if (isShort) -> initial margin
                        if (commissionValue > buyingPower)
                        {
                            string reason = string.Format(
                                CultureInfo.InvariantCulture,
                                "Not enough buying power: commission={0} > buyingPower={1}.",
                                commissionValue,
                                buyingPower);
                            SendReportWithCompletion(OrderReportType.Rejected, OrderStatus.Rejected, reason);
                            Unsubscribe();
                            Log.Debug("PaperBroker.ValidateAccountAndPortfolio: " + reason);
                            return false;
                        }
                    }

                    if (portfolio != null && !isShort)
                    {
                        PortfolioPosition position = portfolio.GetPosition(instrument);
                        double positionQuantity = position?.Quantity ?? 0d;
                        if (quantity > positionQuantity)
                        {
                            string reason = string.Format(
                                CultureInfo.InvariantCulture,
                                "Not enough position quantity: quantity={0} > position quantity={1}.",
                                quantity,
                                positionQuantity);
                            SendReportWithCompletion(OrderReportType.Rejected, OrderStatus.Rejected, reason);
                            Unsubscribe();
                            Log.Debug("PaperBroker.ValidateAccountAndPortfolio: " + reason);
                            return false;
                        }
                    }
                }

                return true;
            }

            private void ValidateTimeInForce(SingleOrder singleOrder, DateTime dateTime)
            {
                if (IsCompleted)
                {
                    return;
                }

                // At this point, possible partial fill report has already been sent.
                bool cancel = false;
                switch (singleOrder.TimeInForce)
                {
                    case OrderTimeInForce.ImmediateOrCancel:
                    case OrderTimeInForce.FillOrKill:
                        cancel = true;
                        break;
                    case OrderTimeInForce.AtClose:
                    case OrderTimeInForce.AtOpen:
                    case OrderTimeInForce.Day:
                    case OrderTimeInForce.GoodTillCanceled:
                    case OrderTimeInForce.GoodTillDate:
                        if (expiration <= dateTime)
                        {
                            cancel = true;
                        }

                        break;
                }

                if (cancel)
                {
                    string reason = string.Concat(singleOrder.TimeInForce.ToString(), " time in force policy.");
                    SendReportWithCompletion(OrderReportType.Canceled, OrderStatus.Canceled, reason);
                    Unsubscribe();
                    Log.Debug("PaperBroker.ValidateTimeInForce: " + reason);
                }
            }
        }
    }
}
