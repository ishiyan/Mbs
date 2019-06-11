using System;
using System.Globalization;
using System.Threading;
using Mbs.Trading.Brokers.Commissions;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Trading.Orders;
using Mbs.Trading.Portfolios;
using Mbs.Trading.Time;

namespace Mbs.Trading.Brokers.PaperBrokers
{
    /// <summary>
    /// The paper broker.
    /// </summary>
    public sealed class PaperBroker : Broker, IDisposable
    {
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
                commissionCurrency = commission?.Currency ?? order.Instrument.Currency;
                this.sellSideAction = sellSideAction;
                this.currentTime = currentTime;
                this.sellSideOrderId = sellSideOrderId;
                this.sellSideReportId = sellSideReportId;
                allowPartialFills = 0d < fillQuantityRatio;
                this.fillQuantityRatio = fillQuantityRatio;
                this.fillOnQuote = fillOnQuote;
                this.fillOnTrade = fillOnTrade;
                this.fillOnOhlcv = fillOnOhlcv;

                clientOrderId = buySideOrderId();
                order.CreationTime = this.currentTime();
                leavesQuantity = order.Quantity;
                account = order.Account;
                portfolio = order.Portfolio;
                Submit();
            }

            private void SendReport(OrderReportType type, OrderStatus status)
            {
                OnReport(new SingleOrderReport(
                    currentTime(),
                    sellSideReportId(),
                    type,
                    status,
                    null,
                    lastPrice,
                    lastQuantity,
                    leavesQuantity,
                    cumulativeQuantity,
                    averagePrice,
                    lastCommission,
                    cumulativeCommission,
                    commissionCurrency));
            }

            private void SendReport(OrderReportType type, OrderStatus status, string text)
            {
                OnReport(new SingleOrderReport(
                    currentTime(),
                    sellSideReportId(),
                    type,
                    status,
                    text,
                    lastPrice,
                    lastQuantity,
                    leavesQuantity,
                    cumulativeQuantity,
                    averagePrice,
                    lastCommission,
                    cumulativeCommission,
                    commissionCurrency));
            }

            private void SendReport(OrderReportType type, OrderStatus status, string text, SingleOrder replaceSourceOrder, SingleOrder replaceTargetOrder)
            {
                OnReport(new SingleOrderReport(
                    currentTime(),
                    sellSideReportId(),
                    type,
                    status,
                    text,
                    lastPrice,
                    lastQuantity,
                    leavesQuantity,
                    cumulativeQuantity,
                    averagePrice,
                    lastCommission,
                    cumulativeCommission,
                    commissionCurrency,
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
                    lastPrice,
                    lastQuantity,
                    leavesQuantity,
                    cumulativeQuantity,
                    averagePrice,
                    lastCommission,
                    cumulativeCommission,
                    commissionCurrency));
            }

            private void SendReportWithCompletion(OrderReportType type, OrderStatus status, string text)
            {
                OnReportWithCompletion(new SingleOrderReport(
                    currentTime(),
                    sellSideReportId(),
                    type,
                    status,
                    text,
                    lastPrice,
                    lastQuantity,
                    leavesQuantity,
                    cumulativeQuantity,
                    averagePrice,
                    lastCommission,
                    cumulativeCommission,
                    commissionCurrency));
            }

            /// <inheritdoc />
            protected override void HandleSubmit()
            {
                sellSideAction(() =>
                {
                    OrderStatus rollbackStatus = orderStatus;
                    orderId = sellSideOrderId();
                    SendReport(OrderReportType.PendingNew, OrderStatus.PendingNew);

                    string reason = null;
                    switch (rollbackStatus)
                    {
                        case OrderStatus.Accepted:
                            break;
                        /* case OrderStatus.Filled: */
                        /* case OrderStatus.Expired: */
                        /* case OrderStatus.Canceled: */
                        /* case OrderStatus.Rejected: */
                        /* case OrderStatus.New: */
                        /* case OrderStatus.PartiallyFilled: */
                        /* case OrderStatus.PendingNew: */
                        /* case OrderStatus.PendingCancel: */
                        /* case OrderStatus.PendingReplace: */
                        default:
                            reason = string.Concat("Cannot submit order in the ", rollbackStatus.ToString(), " state.");
                            break;
                    }

                    SingleOrder singleOrder = order;
                    Instrument instrument = singleOrder.Instrument;
                    bool isBuy = IsBuy(singleOrder.Side);
                    if (null == reason)
                    {
                        if (!isBuy && !IsSell(singleOrder.Side))
                            reason = string.Concat("Order side ", order.Side.ToString(), " is not supported.");
                    }

                    if (null == reason)
                    {
                        switch (order.TimeInForce)
                        {
                            case OrderTimeInForce.AtOpen:
                                /* TODO: begin of session time? What is 'session' in 24-hour Forex trading? +1day: weekends, holidays? */
                                expiration = order.CreationTime.Date.AddDays(1);
                                break;
                            case OrderTimeInForce.AtClose:
                            case OrderTimeInForce.Day:
                                /* TODO: end of session time? What is 'session' in 24-hour Forex trading?  +1day: weekends, holidays? */
                                expiration = order.CreationTime.Date.AddDays(1);
                                break;
                            case OrderTimeInForce.ImmediateOrCancel:
                            case OrderTimeInForce.FillOrKill:
                                expiration = order.CreationTime;
                                break;
                            case OrderTimeInForce.GoodTillCanceled:
                                expiration = DateTime.MaxValue; // singleOrder.CreationTime.AddMonths(3);
                                break;
                            case OrderTimeInForce.GoodTillDate:
                                expiration = order.ExpirationTime;
                                break;
                            default:
                                reason = string.Concat("TimeInForce ", order.TimeInForce.ToString(), " is not supported.");
                                break;
                        }
                    }

                    if (null != reason)
                    {
                        SendReportWithCompletion(OrderReportType.Rejected, OrderStatus.Rejected, reason);
                        return;
                    }

                    SendReport(OrderReportType.New, OrderStatus.New);
                    if (OrderType.TrailingStop == singleOrder.Type)
                        trailingPrice = isBuy ? double.MaxValue : double.MinValue;

                    if (FillOnQuote.Last == fillOnQuote)
                    {
                        var quote = dataPublisher.Last<Quote>(instrument);
                        if (null != quote)
                        {
                            Process(singleOrder, quote, null, null);
                            if (IsCompleted)
                                return;
                        }
                    }

                    if (FillOnTrade.Last == fillOnTrade)
                    {
                        var trade = dataPublisher.Last<Trade>(instrument);
                        if (null != trade)
                        {
                            Process(singleOrder, null, trade, null);
                            if (IsCompleted)
                                return;
                        }
                    }

                    if (FillOnOhlcv.LastClose == fillOnOhlcv)
                    {
                        var ohlcv = dataPublisher.Last<Ohlcv>(instrument);
                        if (null != ohlcv)
                        {
                            Process(singleOrder, null, null, ohlcv);
                            if (IsCompleted)
                                return;
                        }
                    }

                    if (FillOnQuote.None != fillOnQuote)
                    {
                        quoteSubscription = dataPublisher.Monitor<Quote>(instrument);
                        if (null != quoteSubscription)
                        {
                            quoteSubscription.SubscriptionAction += OnQuote;
                            if (!quoteSubscription.IsConnected)
                                quoteSubscription.Connect();
                        }
                    }

                    if (FillOnTrade.None != fillOnTrade)
                    {
                        tradeSubscription = dataPublisher.Monitor<Trade>(instrument);
                        if (null != tradeSubscription)
                        {
                            tradeSubscription.SubscriptionAction += OnTrade;
                            if (!tradeSubscription.IsConnected)
                                tradeSubscription.Connect();
                        }
                    }

                    if (FillOnOhlcv.None != fillOnOhlcv)
                    {
                        ohlcvSubscription = dataPublisher.Monitor<Ohlcv>(instrument);
                        if (null != ohlcvSubscription)
                        {
                            ohlcvSubscription.SubscriptionAction += OnOhlcv;
                            if (!ohlcvSubscription.IsConnected)
                                ohlcvSubscription.Connect();
                        }
                    }
                });
            }

            /// <inheritdoc />
            protected override void HandleReplace(SingleOrder replacementOrder)
            {
                sellSideAction(() =>
                {
                    OrderStatus rollbackStatus = orderStatus;
                    SendReport(OrderReportType.PendingReplace, OrderStatus.PendingReplace);

                    // Valiate the replacement.
                    string reason = null;
                    SingleOrder sourceOrder = order;
                    switch (rollbackStatus)
                    {
                        case OrderStatus.Filled:
                        case OrderStatus.Expired:
                        case OrderStatus.Canceled:
                        case OrderStatus.Rejected:
                            reason = string.Concat("Cannot replace already completed order in the ", rollbackStatus.ToString(), " state.");
                            break;
                        case OrderStatus.New:
                        case OrderStatus.PartiallyFilled:
                            break;
                        /* case OrderStatus.Accepted: */
                        /* case OrderStatus.PendingNew: */
                        /* case OrderStatus.PendingCancel: */
                        /* case OrderStatus.PendingReplace: */
                        default:
                            reason = string.Concat("Cannot replace order in the ", rollbackStatus.ToString(), " state.");
                            break;
                    }

                    if (null == reason)
                    {
                        if (sourceOrder.Instrument != replacementOrder.Instrument)
                            reason = "Cannot replace order instrument.";
                        if (sourceOrder.Type != replacementOrder.Type)
                            reason = "Cannot replace order type.";
                        else if (sourceOrder.Side != replacementOrder.Side)
                            reason = "Cannot replace order side.";
                    }

                    if (null != reason)
                    {
                        SendReport(OrderReportType.ReplaceRejected, rollbackStatus, reason, sourceOrder, replacementOrder);
                    }
                    else
                    {
                        double leaves;
                        lock (fillLock)
                        {
                            leaves = replacementOrder.Quantity - cumulativeQuantity;
                            if (0d > leaves)
                            {
                                reason = "Cannot replace because of negative leaves quantity.";
                            }
                            else
                            {
                                leavesQuantity = leaves;
                                if (leaves < double.Epsilon)
                                {
                                    if (null != commission)
                                    {
                                        double amount = commission.Amount(order, 0d, 0d, 0d, cumulativeQuantity, averagePrice, cumulativeCommission);
                                        if (0d < amount)
                                        {
                                            lastCommission = amount;
                                            cumulativeCommission += amount;
                                        }
                                    }
                                }

                                order = replacementOrder;
                            }
                        }

                        if (null != reason)
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
                                lastPrice,
                                lastQuantity,
                                leavesQuantity,
                                cumulativeQuantity,
                                averagePrice,
                                lastCommission,
                                cumulativeCommission,
                                commissionCurrency));
                        }
                        else
                        {
                            if (OrderType.StopLimit == replacementOrder.Type)
                                isStopLimitReady = false;
                        }
                    }
                });
            }

            /// <inheritdoc />
            protected override void HandleCancel()
            {
                sellSideAction(() =>
                {
                    OrderStatus rollbackStatus = orderStatus;
                    SendReport(OrderReportType.PendingCancel, OrderStatus.PendingCancel);

                    // Valiate the cancellation.
                    string reason = null;
                    switch (rollbackStatus)
                    {
                        case OrderStatus.New:
                        case OrderStatus.PartiallyFilled:
                            break;
                        /* case OrderStatus.Filled: */
                        /* case OrderStatus.Expired: */
                        /* case OrderStatus.Canceled: */
                        /* case OrderStatus.Rejected: */
                        /* case OrderStatus.Accepted: */
                        /* case OrderStatus.PendingNew: */
                        /* case OrderStatus.PendingCancel: */
                        /* case OrderStatus.PendingReplace: */
                        default:
                            reason = string.Concat("Cannot cancel order in the ", rollbackStatus.ToString(), " state.");
                            break;
                    }

                    if (null != reason)
                    {
                        SendReport(OrderReportType.CancelRejected, rollbackStatus, reason);
                    }
                    else
                    {
                        Unsubscribe();
                        lock (fillLock)
                        {
                            leavesQuantity = 0d;
                            if (null != commission)
                            {
                                double amount = commission.Amount(order, 0d, 0d, 0d, cumulativeQuantity, averagePrice, cumulativeCommission);
                                if (0d < amount)
                                {
                                    lastCommission = amount;
                                    cumulativeCommission += amount;
                                }
                            }
                        }

                        SendReportWithCompletion(OrderReportType.Canceled, OrderStatus.Canceled);
                    }
                });
            }

            private void Process(SingleOrder singleOrder, Quote quote, Trade trade, Ohlcv ohlcv)
            {
                double priceMarket = CalculatePriceAndQuantity(singleOrder, quote, trade, ohlcv, out double priceHigh, out double priceLow, out double quantity);
                if (0d < priceMarket && 0d < quantity)
                    TryToExecute(singleOrder, priceMarket, priceHigh, priceLow, quantity);
            }

            private double CalculatePriceAndQuantity(SingleOrder singleOrder, Quote quote, Trade trade, Ohlcv ohlcv, out double high, out double low, out double quantity)
            {
                double price = double.NaN;
                if (null != quote && FillOnQuote.None != fillOnQuote)
                {
                    if (singleOrder.TimeInForce == OrderTimeInForce.AtOpen)
                    {
                        if (expiration > quote.Time)
                            goto labelSkipped;
                    }
                    else if (singleOrder.TimeInForce == OrderTimeInForce.AtClose)
                    {
                        if (expiration > quote.Time)
                            goto labelSkipped;
                        if (null != lastQuote)
                            quote = lastQuote;
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
                                if (OrderTimeInForce.FillOrKill == singleOrder.TimeInForce)
                                {
                                    if (quantity < singleOrder.Quantity)
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
                                if (OrderTimeInForce.FillOrKill == singleOrder.TimeInForce)
                                {
                                    if (quantity < singleOrder.Quantity)
                                        quantity = 0d;
                                }
                            }

                            high = price;
                            low = price;
                            return price;
                        }
                    }
                }

                if (null != trade && FillOnTrade.None != fillOnTrade)
                {
                    price = trade.Price;
                    if (!double.IsNaN(price))
                    {
                        if (singleOrder.TimeInForce == OrderTimeInForce.AtOpen)
                        {
                            if (expiration > trade.Time)
                                goto labelSkipped;
                        }
                        else if (singleOrder.TimeInForce == OrderTimeInForce.AtClose)
                        {
                            if (expiration > trade.Time)
                                goto labelSkipped;
                            if (null != lastTrade)
                                trade = lastTrade;
                        }

                        quantity = singleOrder.Quantity;
                        if (allowPartialFills)
                        {
                            double volume = trade.Volume;
                            volume = double.IsNaN(volume) ? 0 : Math.Floor(volume * fillQuantityRatio);
                            quantity = Math.Min(volume, quantity);
                            if (OrderTimeInForce.FillOrKill == singleOrder.TimeInForce)
                            {
                                if (quantity < singleOrder.Quantity)
                                    quantity = 0d;
                            }
                        }

                        high = price;
                        low = price;
                        return price;
                    }
                }

                if (null != ohlcv && FillOnOhlcv.None != fillOnOhlcv)
                {
                    if (singleOrder.TimeInForce == OrderTimeInForce.AtOpen)
                    {
                        if (expiration > ohlcv.Time)
                            goto labelSkipped;
                    }
                    else if (singleOrder.TimeInForce == OrderTimeInForce.AtClose)
                    {
                        if (expiration > ohlcv.Time)
                            goto labelSkipped;
                        if (null != lastOhlcv)
                            ohlcv = lastOhlcv;
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
                            if (OrderTimeInForce.FillOrKill == singleOrder.TimeInForce)
                            {
                                if (quantity < singleOrder.Quantity)
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
                                    high = ohlcv.IsCloseEmpty ?
                                        ohlcv.Open : // Open is initialized, close is not.
                                        Math.Max(ohlcv.Open, ohlcv.Close); // Both open and close are initialized.
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
                                    low = ohlcv.IsCloseEmpty ?
                                        ohlcv.Open : // Open is initialized, close is not.
                                        Math.Min(ohlcv.Open, ohlcv.Close); // Both open and close are initialized.
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
                            if (priceMarket <= singleOrder.LimitPrice) // Better than the limit price.
                                goto labelMarket;
                        }
                        else if (IsSell(singleOrder.Side))
                        {
                            if (priceMarket >= singleOrder.LimitPrice) // Better than the limit price.
                                goto labelMarket;
                        }

                        goto labelLimit;
                    case OrderType.Limit:
                    labelLimit:
                        if (IsBuy(singleOrder.Side))
                        {
                            if (priceLow <= singleOrder.LimitPrice)
                                ValidateAndSendExecutionReport(singleOrder, singleOrder.LimitPrice, quantity);
                        }
                        else if (IsSell(singleOrder.Side))
                        {
                            if (priceHigh >= singleOrder.LimitPrice)
                                ValidateAndSendExecutionReport(singleOrder, singleOrder.LimitPrice, quantity);
                        }

                        break;
                    case OrderType.Stop:
                        if (IsBuy(singleOrder.Side))
                        {
                            if (priceHigh >= singleOrder.StopPrice)
                                ValidateAndSendExecutionReport(singleOrder, singleOrder.StopPrice, quantity);
                        }
                        else if (IsSell(singleOrder.Side))
                        {
                            if (priceLow <= singleOrder.StopPrice)
                                ValidateAndSendExecutionReport(singleOrder, singleOrder.StopPrice, quantity);
                        }

                        break;
                    case OrderType.TrailingStop:
                        if (IsBuy(singleOrder.Side))
                        {
                            trailingPrice = Math.Min(trailingPrice, priceLow + singleOrder.TrailingDistance);
                            if (priceHigh >= trailingPrice)
                                ValidateAndSendExecutionReport(singleOrder, trailingPrice, quantity);
                        }
                        else if (IsSell(singleOrder.Side))
                        {
                            trailingPrice = Math.Max(trailingPrice, priceHigh - singleOrder.TrailingDistance);
                            if (priceLow <= trailingPrice)
                                ValidateAndSendExecutionReport(singleOrder, trailingPrice, quantity);
                        }

                        break;
                    case OrderType.StopLimit:
                        if (!isStopLimitReady)
                        {
                            if (IsBuy(singleOrder.Side))
                            {
                                if (priceHigh >= singleOrder.StopPrice)
                                    isStopLimitReady = true;
                            }
                            else if (IsSell(singleOrder.Side))
                            {
                                if (priceLow <= singleOrder.StopPrice)
                                    isStopLimitReady = true;
                            }
                        }

                        if (isStopLimitReady)
                            goto labelLimit;
                        break;
                }
            }

            private void ValidateAndSendExecutionReport(SingleOrder singleOrder, double price, double quantity)
            {
                if (ValidateAccountAndPortfolio(singleOrder, price, quantity))
                    SendExecutionReport(singleOrder, price, quantity);
            }

            private void SendExecutionReport(SingleOrder singleOrder, double price, double quantity)
            {
                if (IsCompleted)
                    return;
                SingleOrderReport report;
                bool completed;
                lock (fillLock)
                {
                    if (quantity < leavesQuantity)
                    {
                        completed = false;
                        averagePrice = (averagePrice * cumulativeQuantity + price * quantity) / (cumulativeQuantity + quantity);
                        lastQuantity = quantity;
                        leavesQuantity -= quantity;
                        lastPrice = price;
                        cumulativeQuantity += quantity;
                        if (null != commission)
                        {
                            price = commission.Amount(singleOrder, price, quantity, leavesQuantity, cumulativeQuantity, averagePrice, cumulativeCommission);
                            lastCommission = price;
                            cumulativeCommission += price;
                        }

                        report = new SingleOrderReport(
                            currentTime(),
                            sellSideReportId(),
                            OrderReportType.PartiallyFilled,
                            OrderStatus.PartiallyFilled,
                            string.Empty,
                            lastPrice,
                            lastQuantity,
                            leavesQuantity,
                            cumulativeQuantity,
                            averagePrice,
                            lastCommission,
                            cumulativeCommission,
                            commissionCurrency);
                    }
                    else
                    {
                        completed = true;
                        Unsubscribe();
                        quantity = leavesQuantity;
                        averagePrice = (averagePrice * cumulativeQuantity + price * quantity) / (cumulativeQuantity + quantity);
                        lastQuantity = quantity;
                        leavesQuantity = 0d;
                        cumulativeQuantity += quantity;
                        lastPrice = price;
                        if (null != commission)
                        {
                            price = commission.Amount(singleOrder, price, quantity, 0d, cumulativeQuantity, averagePrice, cumulativeCommission);
                            lastCommission = price;
                            cumulativeCommission += price;
                        }

                        report = new SingleOrderReport(
                            currentTime(),
                            sellSideReportId(),
                            OrderReportType.Filled,
                            OrderStatus.Filled,
                            string.Empty,
                            lastPrice,
                            lastQuantity,
                            leavesQuantity,
                            cumulativeQuantity,
                            averagePrice,
                            lastCommission,
                            cumulativeCommission,
                            commissionCurrency);
                    }

                    Log.Debug(
                        $"PaperBroker.SendExecutionReport: ({(IsBuy(singleOrder.Side) ? "buy" : "sell")}){lastQuantity}@{lastPrice} + commission={lastCommission}/{cumulativeCommission}");
                }

                if (completed)
                    OnReportWithCompletion(report);
                else
                    OnReport(report);

                portfolio?.Add(new PortfolioExecution(this, report));
            }

            private void Unsubscribe()
            {
                if (null != quoteSubscription)
                {
                    quoteSubscription.SubscriptionAction -= OnQuote;
                    quoteSubscription = null;
                }

                if (null != tradeSubscription)
                {
                    tradeSubscription.SubscriptionAction -= OnTrade;
                    tradeSubscription = null;
                }

                if (null != ohlcvSubscription)
                {
                    ohlcvSubscription.SubscriptionAction -= OnOhlcv;
                    ohlcvSubscription = null;
                }
            }

            private void OnQuote(Quote quote)
            {
                if (null == quote)
                    return;
                if (CanFill)
                {
                    SingleOrder singleOrder = order;
                    Process(singleOrder, quote, null, null);
                    ValidateTimeInForce(singleOrder, quote.Time);
                }

                lastQuote = quote;
            }

            private void OnTrade(Trade trade)
            {
                if (null == trade)
                    return;
                if (CanFill)
                {
                    SingleOrder singleOrder = order;
                    Process(singleOrder, null, trade, null);
                    ValidateTimeInForce(singleOrder, trade.Time);
                }

                lastTrade = trade;
            }

            private void OnOhlcv(Ohlcv ohlcv)
            {
                if (null == ohlcv)
                    return;
                if (CanFill)
                {
                    SingleOrder singleOrder = order;
                    Process(singleOrder, null, null, ohlcv);
                    ValidateTimeInForce(singleOrder, ohlcv.Time);
                }

                lastOhlcv = ohlcv;
            }

            private double CommissionValue(SingleOrder singleOrder, double price, double quantity)
            {
                if (null == commission)
                    return 0;

                lock (fillLock)
                {
                    if (quantity < leavesQuantity)
                    {
                        double avgPrice = (averagePrice * cumulativeQuantity + price * quantity) / (cumulativeQuantity + quantity);
                        return commission.Amount(singleOrder, price, quantity, leavesQuantity - quantity, cumulativeQuantity + quantity, avgPrice, cumulativeCommission);
                    }
                    else
                    {
                        quantity = leavesQuantity;
                        double avgPrice = (averagePrice * cumulativeQuantity + price * quantity) / (cumulativeQuantity + quantity);
                        return commission.Amount(singleOrder, price, quantity, 0d, cumulativeQuantity + quantity, avgPrice, cumulativeCommission);
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
                    if (null != account)
                    {
                        double buyingPower = account.Balance(instrument.Currency);

                        // TODO: Is instrument.Margin an absolute value or a percentage?
                        if ((price + instrument.Margin) * quantity + commissionValue > buyingPower)
                        {
                            string reason;
                            if (0 < instrument.Margin)
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
                    if (null != account)
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

                    if (null != portfolio && !isShort)
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
                    return;

                // At this point, posssible partial fill report has already been sent.
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
                            cancel = true;
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

            private static bool IsBuy(OrderSide orderSide)
            {
                return OrderSide.Buy == orderSide || OrderSide.BuyMinus == orderSide;
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
        }

        /// <summary>
        /// Gets or sets the data publisher interface.
        /// </summary>
        public IDataPublisher DataPublisher { get; set; }

        /// <summary>
        /// Gets or sets the commision interface.
        /// </summary>
        public ICommission Commission { get; set; }

        /// <summary>
        /// Gets or sets the timepiece interface.
        /// </summary>
        public ITimepiece Timepiece { get; set; }

        private DateTime Time()
        {
            ITimepiece t = Timepiece;
            return t?.Time ?? DateTime.Now;
        }

        private static long nextBuySideOrderId;
        private static long nextSellSideOrderId;
        private static long nextSellSideReportId;

        private string NextBuySideOrderId()
        {
            return $"{Time():yyyyMMddHHmmss}-pc-{Interlocked.Increment(ref nextBuySideOrderId)}".ToString(CultureInfo.InvariantCulture);
        }

        private string NextSellSideOrderId()
        {
            return $"{Time():yyyyMMddHHmmss}-po-{Interlocked.Increment(ref nextSellSideOrderId)}".ToString(CultureInfo.InvariantCulture);
        }

        private string NextSellSideReportId()
        {
            return $"{Time():yyyyMMddHHmmss}-pr-{Interlocked.Increment(ref nextSellSideReportId)}".ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the sell side is asynchronous.
        /// </summary>
        public bool SellSideAsynchronous { get; set; }

        private readonly CompareAndSwapQueue<Action> sellSideQueue = new CompareAndSwapQueue<Action>();
        private AutoResetEventThread sellSideThread;
        private volatile bool sellSideActive = true;

        private void SellSideAction(Action action)
        {
            // if (!sellSideActive)
            //     return;
            if (SellSideAsynchronous)
            {
                if (null == sellSideThread)
                {
                    sellSideThread = new AutoResetEventThread(() =>
                    {
                        while (sellSideActive)
                        {
                            Action a;
                            while (null != (a = sellSideQueue.Dequeue()))
                                a();
                            sellSideThread?.AutoResetEvent.WaitOne(10000, false);
                        }

                        // Dispose.
                        if (null != sellSideThread)
                        {
                            sellSideThread.AutoResetEvent.Close();
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

        private double fillQuantityRatio;

        /// <summary>
        /// Gets or sets a non-zero value ⋲(0,1] allows partial fills and specifies the fraction of the volume available for order filling.
        /// The value of zero forbids partial fills.
        /// </summary>
        public double FillQuantityRatio
        {
            get => fillQuantityRatio;
            set
            {
                if (0d > value)
                    value = 0d;
                if (1d < value)
                    value = 1d;
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
            if (0d > fillQuantityRatio)
                fillQuantityRatio = 0d;
            if (1d < fillQuantityRatio)
                fillQuantityRatio = 1d;
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

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null == sellSideThread)
                    return;
                sellSideActive = false;
                /* sellSideThread.AutoResetEvent.Close(); */
                sellSideThread.Dispose();
                sellSideThread = null;
            }
        }

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
    }
}
