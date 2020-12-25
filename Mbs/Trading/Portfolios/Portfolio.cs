using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mbs.Trading.Currencies;
using Mbs.Trading.Data;
using Mbs.Trading.Instruments;
using Mbs.Trading.Portfolios.Enumerations;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// A portfolio.
    /// </summary>
    public class Portfolio : IPortfolio
    {
        private readonly IDataPublisher dataPublisher;
        private readonly Account account;
        private readonly CurrencyCode currency;
        private readonly ICurrencyConverter converter;
        private readonly object accessLock = new object();
        private readonly Dictionary<Instrument, PortfolioPosition> positionDictionary = new Dictionary<Instrument, PortfolioPosition>();
        private readonly Dictionary<Instrument, PortfolioPosition> openedPositionDictionary = new Dictionary<Instrument, PortfolioPosition>();
        private readonly Dictionary<Instrument, PortfolioPosition> closedPositionDictionary = new Dictionary<Instrument, PortfolioPosition>();
        private readonly List<PortfolioExecution> executionList = new List<PortfolioExecution>();
        private readonly object monitorLock = new object();
        private readonly object positionChangedDelegateLock = new object();
        private readonly object positionOpenedDelegateLock = new object();
        private readonly object positionClosedDelegateLock = new object();
        private readonly object positionExecutedDelegateLock = new object();
        private readonly ScalarList cashFlow = new ScalarList();
        private readonly ScalarList netCashFlow = new ScalarList();
        private readonly ScalarList debtEquityRatio = new ScalarList();
        private readonly ScalarList leverage = new ScalarList();
        private readonly ScalarList debt = new ScalarList();
        private readonly ScalarList margin = new ScalarList();
        private readonly ScalarList positionEquity = new ScalarList();
        private readonly ScalarList equity = new ScalarList();
        private readonly ScalarList totalEquity = new ScalarList();
        private readonly DrawdownScalarList drawdownScalarList = new DrawdownScalarList();

        private PortfolioMonitors monitors;
        private Action<Portfolio, PortfolioPosition> positionChangedDelegate;
        private Action<Portfolio, PortfolioPosition> positionOpenedDelegate;
        private Action<Portfolio, PortfolioPosition> positionClosedDelegate;
        private Action<Portfolio, PortfolioExecution> positionExecutedDelegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Portfolio"/> class.
        /// </summary>
        /// <param name="account">The associated account.</param>
        /// <param name="dataPublisher">The sell price data publisher.</param>
        public Portfolio(Account account, IDataPublisher dataPublisher)
        {
            this.dataPublisher = dataPublisher;
            this.account = account;
            currency = account.Currency;
            converter = account.CurrencyConverter;
            drawdownScalarList.Update(account.Timepiece.Time, account.Value());
        }

        /// <summary>
        /// Notifies when a portfolio position has been changed.
        /// </summary>
        public event Action<Portfolio, PortfolioPosition> PositionChanged
        {
            add
            {
                lock (positionChangedDelegateLock)
                {
                    positionChangedDelegate += value;
                }
            }

            remove
            {
                lock (positionChangedDelegateLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    positionChangedDelegate -= value;
                }
            }
        }

        /// <summary>
        /// Notifies when a portfolio position has been opened.
        /// </summary>
        public event Action<Portfolio, PortfolioPosition> PositionOpened
        {
            add
            {
                lock (positionOpenedDelegateLock)
                {
                    positionOpenedDelegate += value;
                }
            }

            remove
            {
                lock (positionOpenedDelegateLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    positionOpenedDelegate -= value;
                }
            }
        }

        /// <summary>
        /// Notifies when a portfolio position has been closed.
        /// </summary>
        public event Action<Portfolio, PortfolioPosition> PositionClosed
        {
            add
            {
                lock (positionClosedDelegateLock)
                {
                    positionClosedDelegate += value;
                }
            }

            remove
            {
                lock (positionClosedDelegateLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    positionClosedDelegate -= value;
                }
            }
        }

        /// <summary>
        /// Notifies when a portfolio position has been executed.
        /// </summary>
        public event Action<Portfolio, PortfolioExecution> PositionExecuted
        {
            add
            {
                lock (positionExecutedDelegateLock)
                {
                    positionExecutedDelegate += value;
                }
            }

            remove
            {
                lock (positionExecutedDelegateLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    positionExecutedDelegate -= value;
                }
            }
        }

        /// <summary>
        /// Gets the collection of the portfolio positions.
        /// </summary>
        public ReadOnlyCollection<PortfolioPosition> Positions
        {
            get
            {
                lock (accessLock)
                {
                    return positionDictionary.Values.ToList().AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets the collection of the opened portfolio positions.
        /// </summary>
        public ReadOnlyCollection<PortfolioPosition> OpenedPositions
        {
            get
            {
                lock (accessLock)
                {
                    return openedPositionDictionary.Values.ToList().AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets the collection of the closed portfolio positions.
        /// </summary>
        public ReadOnlyCollection<PortfolioPosition> ClosedPositions
        {
            get
            {
                lock (accessLock)
                {
                    return closedPositionDictionary.Values.ToList().AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets the collection of the executions.
        /// </summary>
        public ReadOnlyCollection<PortfolioExecution> Executions
        {
            get
            {
                lock (accessLock)
                {
                    return executionList.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets or sets the monitoring status of the portfolio.
        /// </summary>
        public PortfolioMonitors Monitors
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
                    if (monitors != value)
                    {
                        monitors = value;
                        lock (accessLock)
                        {
                            foreach (KeyValuePair<Instrument, PortfolioPosition> pair in openedPositionDictionary)
                            {
                                pair.Value.Monitors = value;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the cash flow in home currency.
        /// </summary>
        public double CashFlow => cashFlow.Value;

        /// <summary>
        /// Gets the read-only collection of the cash flow in home currency per date-time.
        /// </summary>
        public IEnumerable<Scalar> CashFlowHistory => cashFlow.Collection;

        /// <summary>
        /// Gets the net cash flow in home currency.
        /// </summary>
        public double NetCashFlow => netCashFlow.Value;

        /// <summary>
        /// Gets the read-only collection of the net cash flow in home currency per date-time.
        /// </summary>
        public IEnumerable<Scalar> NetCashFlowHistory => netCashFlow.Collection;

        /// <summary>
        /// Gets the debt/equity ratio is a measure of a financial leverage determined by dividing the long-term debt by total equity.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the ratio is greater than 1, the majority of assets are financed through debt.
        /// </para>
        /// <para>
        /// If it is smaller than 1, assets are primarily financed through equity.
        /// </para>
        /// </remarks>
        public double DebtEquityRatio => debtEquityRatio.Value;

        /// <summary>
        /// Gets the read-only collection of the debt/equity ratio per date-time.
        /// </summary>
        public IEnumerable<Scalar> DebtEquityRatioHistory => debtEquityRatio.Collection;

        /// <summary>
        /// Gets the leverage is the degree to which an investor is utilizing borrowed money.
        /// </summary>
        public double Leverage => leverage.Value;

        /// <summary>
        /// Gets the read-only collection of the leverage per date-time.
        /// </summary>
        public IEnumerable<Scalar> LeverageHistory => leverage.Collection;

        /// <summary>
        /// Gets the marked to market portfolio debt in home currency.
        /// </summary>
        public double Debt => debt.Value;

        /// <summary>
        /// Gets the read-only collection of the marked to market portfolio debt in home currency per date-time.
        /// </summary>
        public IEnumerable<Scalar> DebtHistory => debt.Collection;

        /// <summary>
        /// Gets the marked to market portfolio margin in home currency.
        /// </summary>
        public double Margin => margin.Value;

        /// <summary>
        /// Gets the read-only collection of the  marked to market portfolio margin in home currency per date-time.
        /// </summary>
        public IEnumerable<Scalar> MarginHistory => margin.Collection;

        /// <summary>
        /// Gets the marked to market value of all portfolio positions in home currency.
        /// </summary>
        public double PositionEquity => positionEquity.Value;

        /// <summary>
        /// Gets the read-only collection of the marked to market value of all portfolio positions in home currency per date-time.
        /// </summary>
        public IEnumerable<Scalar> PositionEquityHistory => positionEquity.Collection;

        /// <summary>
        /// Gets the equity expressed in home currency.
        /// <para/>
        /// Consists of the marked to market portfolio positions plus the current value of the associated account (including converted foreign currencies).
        /// </summary>
        public double Equity => equity.Value;

        /// <summary>
        /// Gets the read-only collection of the equity in home currency per date-time.
        /// <para/>
        /// The equity consists of the marked to market portfolio positions plus the current value of the associated account (including converted foreign currencies).
        /// </summary>
        public IEnumerable<Scalar> EquityHistory => equity.Collection;

        /// <summary>
        /// Gets the total equity expressed in home currency.
        /// <para/>
        /// Consists of the marked to market portfolio positions minus debt plus the current value of the associated account (including converted foreign currencies).
        /// </summary>
        public double TotalEquity => totalEquity.Value;

        /// <summary>
        /// Gets the read-only collection of the total equity in home currency per date-time.
        /// <para/>
        /// The total equity consists of the marked to market portfolio positions minus debt plus the current value of the associated account (including converted foreign currencies).
        /// </summary>
        public IEnumerable<Scalar> TotalEquityHistory => totalEquity.Collection;

        /// <summary>
        /// Gets the current drawdown percentage [0, -1].
        /// </summary>
        public double Drawdown => drawdownScalarList.Drawdown;

        /// <summary>
        /// Gets the current maximal drawdown percentage [0, -1].
        /// </summary>
        public double MaximalDrawdown => drawdownScalarList.DrawdownMax;

        /// <summary>
        /// Gets the read-only collection of the drawdown percentage [0, -1] per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> DrawdownHistory => drawdownScalarList.DrawdownCollection;

        /// <summary>
        /// Gets the read-only collection of the maximal drawdown percentage [0, -1] per date-time.
        /// </summary>
        public ReadOnlyCollection<Scalar> DrawdownMaxHistory => drawdownScalarList.DrawdownMaxCollection;

        /// <summary>
        /// The portfolio position of the instrument.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <returns>The portfolio position if the portfolio has the specified instrument.</returns>
        public PortfolioPosition GetPosition(Instrument instrument)
        {
            PortfolioPosition position;
            lock (accessLock)
            {
                if (!positionDictionary.TryGetValue(instrument, out position))
                {
                    return null;
                }
            }

            return position;
        }

        /// <summary>
        /// Adds a new execution to the portfolio.
        /// </summary>
        /// <param name="execution">The portfolio execution.</param>
        internal void Add(PortfolioExecution execution)
        {
            Instrument instrument = execution.Instrument;
            bool positionOpened = false, positionClosed = false;
            PortfolioPosition position;
            lock (accessLock)
            {
                if (!positionDictionary.TryGetValue(instrument, out position))
                {
                    // Open a new position.
                    position = new PortfolioPosition(execution, dataPublisher);
                    positionDictionary.Add(instrument, position);
                    openedPositionDictionary.Add(instrument, position);
                    positionOpened = true;
                    lock (monitorLock)
                    {
                        if (monitors != PortfolioMonitors.None)
                        {
                            position.Monitors = monitors;
                        }
                    }
                }
                else
                {
                    position.Add(execution);
                    if (closedPositionDictionary.ContainsKey(instrument))
                    {
                        // Re-open a closed position.
                        closedPositionDictionary.Remove(instrument);
                        openedPositionDictionary.Add(instrument, position);
                        positionOpened = true;
                        lock (monitorLock)
                        {
                            if (monitors != PortfolioMonitors.None)
                            {
                                position.Monitors = monitors;
                            }
                        }
                    }

                    if (position.Quantity < double.Epsilon)
                    {
                        lock (monitorLock)
                        {
                            position.Monitors = PortfolioMonitors.None;
                            position.Changed -= Update;
                        }

                        openedPositionDictionary.Remove(instrument);
                        closedPositionDictionary.Add(instrument, position);
                        positionClosed = true;
                    }
                }

                executionList.Add(execution);
                Update(execution.DateTime, true);
            }

            if (positionOpened && Monitors != PortfolioMonitors.None)
            {
                position.Changed += Update;
            }

            EmitPositionExecuted(execution);
            if (positionOpened)
            {
                EmitPositionOpened(position);
            }

            EmitPositionChanged(position);
            if (positionClosed)
            {
                EmitPositionClosed(position);
            }
        }

        private void EmitPositionChanged(PortfolioPosition position)
        {
            lock (positionChangedDelegateLock)
            {
                if (positionChangedDelegate != null)
                {
                    var handlers = positionChangedDelegate.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var theHandler = handler as Action<Portfolio, PortfolioPosition>;
                        theHandler?.Invoke(this, position);
                    }
                }
            }
        }

        private void EmitPositionOpened(PortfolioPosition position)
        {
            lock (positionOpenedDelegateLock)
            {
                if (positionOpenedDelegate != null)
                {
                    var handlers = positionOpenedDelegate.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var theHandler = handler as Action<Portfolio, PortfolioPosition>;
                        theHandler?.Invoke(this, position);
                    }
                }
            }
        }

        private void EmitPositionClosed(PortfolioPosition position)
        {
            lock (positionClosedDelegateLock)
            {
                if (positionClosedDelegate != null)
                {
                    var handlers = positionClosedDelegate.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var theHandler = handler as Action<Portfolio, PortfolioPosition>;
                        theHandler?.Invoke(this, position);
                    }
                }
            }
        }

        private void EmitPositionExecuted(PortfolioExecution execution)
        {
            lock (positionExecutedDelegateLock)
            {
                if (positionExecutedDelegate != null)
                {
                    var handlers = positionExecutedDelegate.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var theHandler = handler as Action<Portfolio, PortfolioExecution>;
                        theHandler?.Invoke(this, execution);
                    }
                }
            }
        }

        private void Update(PortfolioPosition position, DateTime dateTime)
        {
            Update(dateTime, false);
        }

        private void Update(DateTime dateTime, bool executed)
        {
            double newEquity = 0d, newDebt = 0d, newMargin = 0d, newCashFlow = 0d, newNetCashFlow = 0d;
            foreach (KeyValuePair<Instrument, PortfolioPosition> pair in openedPositionDictionary)
            {
                PortfolioPosition position = pair.Value;
                CurrencyCode positionCurrency = position.Currency;
                if (currency == positionCurrency || converter == null)
                {
                    newEquity += position.Value;
                    newDebt += position.Debt;
                    newMargin += position.Margin;
                    if (executed)
                    {
                        newCashFlow += position.CashFlow;
                        newNetCashFlow += position.NetCashFlow;
                    }
                }
                else
                {
                    newEquity += converter.Convert(position.Value, positionCurrency, currency);
                    newDebt += converter.Convert(position.Debt, positionCurrency, currency);
                    newMargin += converter.Convert(position.Margin, positionCurrency, currency);
                    if (executed)
                    {
                        newCashFlow += converter.Convert(position.CashFlow, positionCurrency, currency);
                        newNetCashFlow += converter.Convert(position.NetCashFlow, positionCurrency, currency);
                    }
                }
            }

            if (executed)
            {
                cashFlow.Add(dateTime, newCashFlow);
                netCashFlow.Add(dateTime, newNetCashFlow);
            }

            debt.Add(dateTime, newDebt);
            margin.Add(dateTime, newMargin);
            positionEquity.Add(dateTime, newEquity);
            newEquity += account.Value();
            equity.Add(dateTime, newEquity);
            leverage.Add(dateTime, Math.Abs(newMargin) < double.Epsilon ? 0d : newEquity / newMargin);
            newEquity -= newDebt;
            debtEquityRatio.Add(dateTime, Math.Abs(newEquity) < double.Epsilon ? 0d : newDebt / newEquity);
            totalEquity.Add(dateTime, newEquity);
            drawdownScalarList.Update(dateTime, newEquity);
        }
    }
}
