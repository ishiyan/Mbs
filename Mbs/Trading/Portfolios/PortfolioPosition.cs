using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mbs.Trading.Currencies;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Trading.Orders;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// A portfolio position.
    /// </summary>
    public class PortfolioPosition
    {
        private readonly object entryLock = new object();
        private readonly IDataPublisher dataPublisher;
        private readonly double instrumentFactor;
        private readonly ScalarList amount = new ScalarList();
        private readonly List<PortfolioExecution> executionList = new List<PortfolioExecution>();
        private readonly ScalarList cashFlow = new ScalarList();
        private readonly ScalarList netCashFlow = new ScalarList();
        private readonly ScalarList value = new ScalarList();
        private readonly ScalarList netProfitAndLoss = new ScalarList();
        private readonly ScalarList netProfitAndLossPercent = new ScalarList();
        private readonly ScalarList profitAndLoss = new ScalarList();
        private readonly ScalarList profitAndLossPercent = new ScalarList();
        private readonly ScalarList unrealizedProfitAndLoss = new ScalarList();
        private readonly DrawdownScalarList drawdownScalarList = new DrawdownScalarList();
        private readonly object executedDelegateLock = new object();
        private readonly object changedDelegateLock = new object();
        private readonly object monitorLock = new object();

        private double quantityBought;
        private double quantitySold;
        private double quantitySoldShort;
        private double quantity;
        private PositionSide side;
        private double profitAndLossQuantity;
        private int profitAndLossIndex;
        private double debt;
        private double margin;
        private double price;
        private Action<PortfolioPosition, PortfolioExecution> executedDelegate;
        private Action<PortfolioPosition, DateTime> changedDelegate;
        private ISubscription<Quote> quoteSubscription;
        private ISubscription<Trade> tradeSubscription;
        private ISubscription<Ohlcv> ohlcvSubscription;
        private PortfolioMonitors monitors;

        /// <summary>
        /// Gets the position instrument.
        /// </summary>
        public Instrument Instrument { get; }

        /// <summary>
        /// Gets the instrument currency.
        /// </summary>
        public CurrencyCode Currency { get; }

        /// <summary>
        /// Gets the entry value of this position in instrument currency.
        /// </summary>
        public double EntryValue { get; }

        /// <summary>
        /// Gets the position quantity bought.
        /// </summary>
        public double QuantityBought
        {
            get
            {
                lock (entryLock)
                {
                    return quantityBought;
                }
            }
        }

        /// <summary>
        /// Gets the position quantity sold.
        /// </summary>
        public double QuantitySold
        {
            get
            {
                lock (entryLock)
                {
                    return quantitySold;
                }
            }
        }

        /// <summary>
        /// Gets the position quantity sold short.
        /// </summary>
        public double QuantitySoldShort
        {
            get
            {
                lock (entryLock)
                {
                    return quantitySoldShort;
                }
            }
        }

        /// <summary>
        /// Gets the position quantity (the absolute value of the amount) in instrument currency.
        /// </summary>
        public double Quantity
        {
            get
            {
                lock (entryLock)
                {
                    return quantity;
                }
            }
        }

        /// <summary>
        /// Gets the position amount (quantity bought minus sold minus sold short) in instrument currency.
        /// </summary>
        public double Amount => amount.Value;

        /// <summary>
        /// Gets the read-only collection of the amount per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> AmountHistory => amount.Collection;

        /// <summary>
        /// Gets the position side (short or long).
        /// </summary>
        public PositionSide Side
        {
            get
            {
                lock (entryLock)
                {
                    return side;
                }
            }
        }

        /// <summary>
        /// Gets the position debt in instrument currency.
        /// </summary>
        public double Debt
        {
            get
            {
                lock (entryLock)
                {
                    return debt;
                }
            }
        }

        /// <summary>
        /// Gets the position margin.
        /// </summary>
        public double Margin
        {
            get
            {
                lock (entryLock)
                {
                    return margin;
                }
            }
        }

        /// <summary>
        /// Gets the read-only collection of the executions.
        /// </summary>
        public ReadOnlyCollection<PortfolioExecution> ExecutionHistory
        {
            get
            {
                lock (entryLock)
                {
                    return executionList.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets the cash flow (the sum of cash flows af all executions) in instrument currency.
        /// </summary>
        public double CashFlow => cashFlow.Value;

        /// <summary>
        /// Gets the read-only collection of the cash flow per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> CashFlowHistory => cashFlow.Collection;

        /// <summary>
        /// Gets the net cash flow (the sum of net cash flows af all executions) in instrument currency.
        /// </summary>
        public double NetCashFlow => netCashFlow.Value;

        /// <summary>
        /// Gets the read-only collection of the net cash flow per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> NetCashFlowHistory => netCashFlow.Collection;

        /// <summary>
        /// Gets the current price in instrument currency.
        /// </summary>
        public double Price
        {
            get
            {
                lock (entryLock)
                {
                    return price;
                }
            }
        }

        /// <summary>
        /// Gets the current position value (factored price times amount) in instrument currency.
        /// </summary>
        public double Value => value.Value;

        /// <summary>
        /// Gets the read-only collection of the value per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> ValueHistory => value.Collection;

        /// <summary>
        /// Gets the current position leverage in instrument currency.
        /// </summary>
        public double Leverage
        {
            get
            {
                double temp = Margin * CashFlow;
                return Math.Abs(temp) < double.Epsilon ? 0d : (Value / temp);
            }
        }

        /// <summary>
        /// Gets the net Profit and Loss (the sum of the value and the net cash flow) in instrument currency.
        /// </summary>
        public double NetProfitAndLoss => netProfitAndLoss.Value;

        /// <summary>
        /// Gets the read-only collection of the net profit and loss per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> NetProfitAndLossHistory => netProfitAndLoss.Collection;

        /// <summary>
        /// Gets the net Profit and Loss percentage (the net Profit and Loss divided by the entry execution value).
        /// </summary>
        public double NetProfitAndLossPercent => netProfitAndLossPercent.Value;

        /// <summary>
        /// Gets the read-only collection of the net profit and loss percent per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> NetProfitAndLossPercentHistory => netProfitAndLossPercent.Collection;

        /// <summary>
        /// Gets the Profit and Loss (the sum of the value and the cash flow) in instrument currency.
        /// </summary>
        public double ProfitAndLoss => profitAndLoss.Value;

        /// <summary>
        /// Gets the read-only collection of the profit and loss per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> ProfitAndLossHistory => profitAndLoss.Collection;

        /// <summary>
        /// Gets the Profit and Loss percentage (the Profit and Loss divided by the entry execution value).
        /// </summary>
        public double ProfitAndLossPercent => profitAndLossPercent.Value;

        /// <summary>
        /// Gets the read-only collection of the profit and loss percent per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> ProfitAndLossPercentHistory => profitAndLossPercent.Collection;

        /// <summary>
        /// Gets the unrealized Profit and Loss is the theoretical gain or loss on open position valued at current market rates.
        /// Unrealized gains and losses become ProfitAndLoss when position is closed.
        /// </summary>
        public double UnrealizedProfitAndLoss => unrealizedProfitAndLoss.Value;

        /// <summary>
        /// Gets the read-only collection of the unrealized profit and loss per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> UnrealizedProfitAndLossHistory => unrealizedProfitAndLoss.Collection;

        /// <summary>
        /// Gets the current drawdown percentage [0, -1].
        /// </summary>
        public double Drawdown => drawdownScalarList.Drawdown;

        /// <summary>
        /// Gets the current maximal drawdown percentage [0, -1].
        /// </summary>
        public double DrawdownMax => drawdownScalarList.DrawdownMax;

        /// <summary>
        /// Gets the read-only collection of the drawdown percentage [0, -1] per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> DrawdownHistory => drawdownScalarList.DrawdownCollection;

        /// <summary>
        /// Gets the read-only collection of the maximal drawdown percentage [0, -1] per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> DrawdownMaxHistory => drawdownScalarList.DrawdownMaxCollection;

        private void EmitExecuted(PortfolioExecution execution)
        {
            lock (executedDelegateLock)
            {
                if (executedDelegate != null)
                {
                    var handlers = executedDelegate.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var theHandler = handler as Action<PortfolioPosition, PortfolioExecution>;
                        theHandler?.Invoke(this, execution);
                    }
                }
            }
        }

        /// <summary>
        /// Notifies when this position has been executed.
        /// </summary>
        public event Action<PortfolioPosition, PortfolioExecution> Executed
        {
            add
            {
                lock (executedDelegateLock)
                {
                    executedDelegate += value;
                }
            }

            remove
            {
                lock (executedDelegateLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    executedDelegate -= value;
                }
            }
        }

        private void EmitChanged(DateTime dateTime)
        {
            lock (changedDelegateLock)
            {
                if (changedDelegate != null)
                {
                    var handlers = changedDelegate.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var theHandler = handler as Action<PortfolioPosition, DateTime>;
                        theHandler?.Invoke(this, dateTime);
                    }
                }
            }
        }

        /// <summary>
        /// Notifies when this position has been changed.
        /// </summary>
        public event Action<PortfolioPosition, DateTime> Changed
        {
            add
            {
                lock (changedDelegateLock)
                {
                    changedDelegate += value;
                }
            }

            remove
            {
                lock (changedDelegateLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    changedDelegate -= value;
                }
            }
        }

        private void OnChanged(double newPrice, DateTime dateTime)
        {
            lock (entryLock)
            {
                if (Math.Abs(price - newPrice) < double.Epsilon)
                    return;
                Update(newPrice, dateTime);
            }

            EmitChanged(dateTime);
        }

        private void OnQuote(Quote quote)
        {
            OnChanged(quote.AskPrice, quote.Time);
        }

        private void OnTrade(Trade trade)
        {
            OnChanged(trade.Price, trade.Time);
        }

        private void OnOhlcv(Ohlcv ohlcv)
        {
            OnChanged(ohlcv.Close, ohlcv.Time);
        }

        /// <summary>
        /// Gets or sets the monitoring status of this position.
        /// </summary>
        internal PortfolioMonitors Monitors
        {
            get
            {
                lock (monitorLock)
                {
                    return monitors;
                }
            }

            set
            {
                lock (monitorLock)
                {
                    if (value != monitors)
                    {
                        if ((value & PortfolioMonitors.Quote) == PortfolioMonitors.Quote)
                        {
                            if (quoteSubscription == null)
                            {
                                quoteSubscription = dataPublisher.Monitor<Quote>(Instrument);
                                if (quoteSubscription != null)
                                    quoteSubscription.SubscriptionAction += OnQuote;
                            }
                        }
                        else
                        {
                            if (quoteSubscription != null)
                            {
                                quoteSubscription.SubscriptionAction -= OnQuote;
                                quoteSubscription = null;
                            }
                        }

                        if ((value & PortfolioMonitors.Trade) == PortfolioMonitors.Trade)
                        {
                            if (tradeSubscription == null)
                            {
                                tradeSubscription = dataPublisher.Monitor<Trade>(Instrument);
                                if (tradeSubscription != null)
                                    tradeSubscription.SubscriptionAction += OnTrade;
                            }
                        }
                        else
                        {
                            if (tradeSubscription != null)
                            {
                                tradeSubscription.SubscriptionAction -= OnTrade;
                                tradeSubscription = null;
                            }
                        }

                        if ((value & PortfolioMonitors.Ohlcv) == PortfolioMonitors.Ohlcv)
                        {
                            if (ohlcvSubscription == null)
                            {
                                ohlcvSubscription = dataPublisher.Monitor<Ohlcv>(Instrument);
                                if (ohlcvSubscription != null)
                                {
                                    ohlcvSubscription.SubscriptionAction += OnOhlcv;
                                    ohlcvSubscription.Connect();
                                }
                            }
                        }
                        else
                        {
                            if (ohlcvSubscription != null)
                            {
                                ohlcvSubscription.SubscriptionAction -= OnOhlcv;
                                ohlcvSubscription.Disconnect();
                                ohlcvSubscription = null;
                            }
                        }

                        monitors = value;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioPosition"/> class.
        /// </summary>
        /// <param name="execution">The portfolio execution.</param>
        /// <param name="dataPublisher">The sell price data publisher.</param>
        internal PortfolioPosition(PortfolioExecution execution, IDataPublisher dataPublisher)
        {
            this.dataPublisher = dataPublisher;
            Instrument = execution.Instrument;
            Currency = Instrument.Currency;
            instrumentFactor = Instrument.Factor ?? 1d;
            price = execution.Price;
            DateTime executionDateTime = execution.DateTime;
            double executionQuantity = execution.Quantity;
            double executionValue = execution.Value;
            EntryValue = executionValue;
            UpdateAmount(execution, 0d);
            profitAndLossQuantity = executionQuantity;
            execution.ProfitAndLoss = -execution.Commission;
            execution.RealizedProfitAndLoss = 0d;
            value.Add(executionDateTime, executionValue);
            double tempNetCashFlow = execution.NetCashFlow;
            netCashFlow.Accumulate(executionDateTime, tempNetCashFlow);
            double temp = execution.CashFlow;
            cashFlow.Accumulate(executionDateTime, temp);
            temp += executionValue;
            profitAndLoss.Add(executionDateTime, temp);

            // The execution value is always non-negative, so the absolute value is not needed.
            bool isExecutionValueZero = executionValue < double.Epsilon;

            profitAndLossPercent.Add(executionDateTime, isExecutionValueZero ? 0 : (temp / executionValue));
            drawdownScalarList.Update(executionDateTime, temp);
            temp = executionValue + tempNetCashFlow;
            netProfitAndLoss.Add(executionDateTime, temp);
            netProfitAndLossPercent.Add(executionDateTime, isExecutionValueZero ? 0 : (temp / executionValue));
            unrealizedProfitAndLoss.Add(executionDateTime, executionValue);
            margin = execution.Margin;
            debt = execution.Debt;
            executionList.Add(execution);
            execution.SingleOrderTicket.Order.Account.Add(execution, tempNetCashFlow + debt, execution.SingleOrderReport.LastCommission);
        }

        /// <summary>
        /// Adds a new portfolio execution to this portfolio position.
        /// </summary>
        /// <param name="execution">The portfolio execution.</param>
        internal void Add(PortfolioExecution execution)
        {
            if (!Instrument.Equals(execution.Instrument))
                throw new ArgumentException("The position instrument differs from the execution instrument");
            DateTime dateTime = execution.DateTime;
            double debtAmount;
            lock (entryLock)
            {
                debtAmount = UpdateMarginAndDebt(execution);
                double currentAmount = amount.Value;
                UpdateExecutionProfitAndLoss(execution, currentAmount);
                UpdateAmount(execution, currentAmount);
                executionList.Add(execution);
                cashFlow.Accumulate(dateTime, execution.CashFlow);
                netCashFlow.Accumulate(dateTime, execution.NetCashFlow);
                Update(execution.Price, dateTime);
            }

            execution.SingleOrderTicket.Order.Account.Add(execution, execution.NetCashFlow + debtAmount, execution.SingleOrderReport.LastCommission);
            EmitExecuted(execution);
            EmitChanged(dateTime);
        }

        private void UpdateAmount(PortfolioExecution execution, double currentAmount)
        {
            double executionQuantity = execution.Quantity;
            switch (execution.Side)
            {
                case OrderSide.Buy:
                case OrderSide.BuyMinus:
                    quantityBought += executionQuantity;
                    currentAmount += executionQuantity;
                    break;
                case OrderSide.Sell:
                case OrderSide.SellPlus:
                    quantitySold += executionQuantity;
                    currentAmount -= executionQuantity;
                    break;
                case OrderSide.SellShort:
                case OrderSide.SellShortExempt:
                    quantitySoldShort += executionQuantity;
                    currentAmount -= executionQuantity;
                    break;
                default:
                    throw new ArgumentException($"Not supported order side: {execution.Side}.");
            }

            quantity = Math.Abs(currentAmount);
            side = currentAmount < 0 ? PositionSide.Short : PositionSide.Long;
            amount.Add(execution.DateTime, currentAmount);
        }

        private void UpdateExecutionProfitAndLoss(PortfolioExecution execution, double currentAmount)
        {
            int inverseAmountSign = -Math.Sign(execution.Amount);
            double executionPrice = execution.Price;
            double executionQuantity = execution.Quantity;
            double commission = 0, delta = currentAmount;

            // PositionSide.Long && sell order or PositionSide.Short && buy order
            if ((delta >= 0 && inverseAmountSign > 0) || (delta < 0 && inverseAmountSign < 0))
            {
                int executionCount = executionList.Count;
                double pnlQuantity = profitAndLossQuantity;
                double minimalQuantity = Math.Min(executionQuantity, pnlQuantity);
                PortfolioExecution pnlExecution = executionList[profitAndLossIndex];
                double executionRatio = execution.Commission / executionQuantity;

                // = minQty * (exComm/exQty + pnlComm/pnlQty)
                commission = minimalQuantity * (executionRatio + pnlExecution.Commission / pnlExecution.Quantity);

                // = minQty * (exPrice - pnlPrice) * inverseAmountSign
                delta = minimalQuantity * (executionPrice - pnlExecution.Price) * inverseAmountSign;

                int pnlIndex = profitAndLossIndex + 1;
                while (executionQuantity > pnlQuantity && pnlIndex < executionCount)
                {
                    pnlExecution = executionList[pnlIndex++];
                    if (inverseAmountSign != -Math.Sign(pnlExecution.Amount))
                    {
                        double pnlExecutionQuantity = pnlExecution.Quantity;
                        minimalQuantity = Math.Min(executionQuantity - pnlQuantity, pnlExecutionQuantity);

                        // += minQty * (exComm/exQty + pnlComm/pnlQty)
                        commission += minimalQuantity * (executionRatio + pnlExecution.Commission / pnlExecutionQuantity);

                        // = minQty * (exPrice - pnlPrice) * inverseAmountSign
                        delta += minimalQuantity * (executionPrice - pnlExecution.Price) * inverseAmountSign;
                        pnlQuantity += pnlExecutionQuantity;
                    }
                }

                profitAndLossQuantity = Math.Abs(executionQuantity - pnlQuantity);
                if (Math.Abs(executionQuantity - pnlQuantity) < double.Epsilon && pnlIndex == executionCount)
                    profitAndLossIndex = executionCount;
                else
                    profitAndLossIndex = executionQuantity > pnlQuantity ? executionCount : (pnlIndex - 1);
            }

            delta *= instrumentFactor;
            execution.ProfitAndLoss = delta - execution.Commission;
            execution.RealizedProfitAndLoss = delta - commission;
        }

        private double UpdateMarginAndDebt(PortfolioExecution execution)
        {
            double debtAmount = 0d;
            if (Math.Abs(execution.Margin) > double.Epsilon)
            {
                bool isLong = side == PositionSide.Long;
                bool isShort = side == PositionSide.Short;
                bool isBuy = false, isSell = false;
                switch (execution.Side)
                {
                    case OrderSide.Buy:
                    case OrderSide.BuyMinus:
                        isBuy = true;
                        break;
                    case OrderSide.Sell:
                    case OrderSide.SellPlus:
                    case OrderSide.SellShort:
                    case OrderSide.SellShortExempt:
                        isSell = true;
                        break;
                }

                if ((isLong && isBuy) || (isShort && isSell))
                {
                    margin += execution.Margin;
                    debtAmount = execution.Debt;
                    debt += debtAmount;
                }

                if ((isLong && isSell) || (isShort && isBuy))
                {
                    double executionQuantity = execution.Quantity;
                    if (quantity > executionQuantity)
                    {
                        debtAmount = -debt * executionQuantity / quantity; // A fraction of the debt.
                        margin -= execution.Margin;
                        debt += debtAmount;
                    }
                    else if (quantity < executionQuantity)
                    {
                        debtAmount = executionQuantity - quantity;
                        double delta = debtAmount * execution.Price * instrumentFactor;
                        debtAmount *= Instrument.Margin;
                        margin = debtAmount;
                        delta -= debtAmount;
                        debtAmount = delta - debt;
                        debt = delta;
                    }
                    else
                    {
                        // if (Math.Abs(quantity - executionQuantity) < double.Epsilon)
                        margin = 0d;
                        debtAmount = -execution.Debt;
                        debt = 0d;
                    }
                }
            }

            return debtAmount;
        }

        private void Update(double newPrice, DateTime dateTime)
        {
            price = newPrice;
            double tempAmount = amount.Value;
            double tempValue = newPrice * tempAmount * instrumentFactor;
            value.Add(dateTime, tempValue);
            double temp = tempValue + cashFlow.Value;
            profitAndLoss.Add(dateTime, temp);
            bool isEntryValueZero = Math.Abs(EntryValue) < double.Epsilon;
            profitAndLossPercent.Add(dateTime, isEntryValueZero ? 0d : (temp / EntryValue));
            drawdownScalarList.Update(dateTime, temp);
            temp = tempValue + netCashFlow.Value;
            netProfitAndLoss.Add(dateTime, temp);
            netProfitAndLossPercent.Add(dateTime, isEntryValueZero ? 0d : (temp / EntryValue));
            temp = 0d;
            if (Math.Abs(tempAmount) > double.Epsilon)
            {
                tempValue = profitAndLossQuantity * Math.Sign(tempAmount);
                int currentIndex = profitAndLossIndex, count = executionList.Count;
                do
                {
                    temp += (newPrice - executionList[currentIndex].Price) * tempValue;
                }
                while (++currentIndex < count);
                temp *= instrumentFactor;
            }

            unrealizedProfitAndLoss.Add(dateTime, temp);
        }
    }
}
