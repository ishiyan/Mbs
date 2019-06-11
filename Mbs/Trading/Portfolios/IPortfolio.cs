using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mbs.Trading;
using Mbs.Trading.Currencies;
using Mbs.Trading.Data;
using Mbs.Trading.Instruments;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// A portfolio interface.
    /// </summary>
    public interface IPortfolio
    {
        /// <summary>
        /// Gets the collection of the portfolio positions.
        /// </summary>
        ReadOnlyCollection<PortfolioPosition> Positions { get; }

        /// <summary>
        /// Gets the collection of the opened portfolio positions.
        /// </summary>
        ReadOnlyCollection<PortfolioPosition> OpenedPositions { get; }

        /// <summary>
        /// Gets the collection of the closed portfolio positions.
        /// </summary>
        ReadOnlyCollection<PortfolioPosition> ClosedPositions { get; }

        /// <summary>
        /// Gets the collection of the executions.
        /// </summary>
        ReadOnlyCollection<PortfolioExecution> Executions { get; }

        /// <summary>
        /// Gets or sets the monitoring status of the portfolio.
        /// </summary>
        PortfolioMonitors Monitors { get; set; }

        /// <summary>
        /// Notifies when a portfolio position has been changed.
        /// </summary>
        event Action<Portfolio, PortfolioPosition> PositionChanged;

        /// <summary>
        /// Notifies when a portfolio position has been opened.
        /// </summary>
        event Action<Portfolio, PortfolioPosition> PositionOpened;

        /// <summary>
        /// Notifies when a portfolio position has been closed.
        /// </summary>
        event Action<Portfolio, PortfolioPosition> PositionClosed;

        /// <summary>
        /// Notifies when a portfolio position has been executed.
        /// </summary>
        event Action<Portfolio, PortfolioExecution> PositionExecuted;

        /// <summary>
        /// The portfolio position of the instrument.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <returns>The portfolio position if the portfolio has the specified instrument.</returns>
        PortfolioPosition GetPosition(Instrument instrument);

        /// <summary>
        /// Gets the cash flow in home currency.
        /// </summary>
        double CashFlow { get; }

        /// <summary>
        /// Gets the read-only collection of the cash flow in home currency per date-time.
        /// </summary>
        IEnumerable<Scalar> CashFlowHistory { get; }

        /// <summary>
        /// Gets the net cash flow in home currency.
        /// </summary>
        double NetCashFlow { get; }

        /// <summary>
        /// Gets the read-only collection of the net cash flow in home currency per date-time.
        /// </summary>
        IEnumerable<Scalar> NetCashFlowHistory { get; }

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
        double DebtEquityRatio { get; }

        /// <summary>
        /// Gets the read-only collection of the debt/equity ratio per date-time.
        /// </summary>
        IEnumerable<Scalar> DebtEquityRatioHistory { get; }

        /// <summary>
        /// Gets the leverage is the degree to which an investor is utilizing borrowed money.
        /// </summary>
        double Leverage { get; }

        /// <summary>
        /// Gets the read-only collection of the leverage per date-time.
        /// </summary>
        IEnumerable<Scalar> LeverageHistory { get; }

        /// <summary>
        /// Gets the marked to market portfolio debt in home currency.
        /// </summary>
        double Debt { get; }

        /// <summary>
        /// Gets the read-only collection of the marked to market portfolio debt in home currency per date-time.
        /// </summary>
        IEnumerable<Scalar> DebtHistory { get; }

        /// <summary>
        /// Gets the marked to market portfolio margin in home currency.
        /// </summary>
        double Margin { get; }

        /// <summary>
        /// Gets the read-only collection of the  marked to market portfolio margin in home currency per date-time.
        /// </summary>
        IEnumerable<Scalar> MarginHistory { get; }

        /// <summary>
        /// Gets the marked to market value of all portfolio positions in home currency.
        /// </summary>
        double PositionEquity { get; }

        /// <summary>
        /// Gets the read-only collection of the marked to market value of all portfolio positions in home currency per date-time.
        /// </summary>
        IEnumerable<Scalar> PositionEquityHistory { get; }

        /// <summary>
        /// Gets the equity expressed in home currency.
        /// <para/>
        /// Consists of the marked to market portfolio positions plus the current value of the associated account (including converted foreign currencies).
        /// </summary>
        double Equity { get; }

        /// <summary>
        /// Gets the read-only collection of the equity in home currency per date-time.
        /// <para/>
        /// The equity consists of the marked to market portfolio positions plus the current value of the associated account (including converted foreign currencies).
        /// </summary>
        IEnumerable<Scalar> EquityHistory { get; }

        /// <summary>
        /// Gets the total equity expressed in home currency.
        /// <para/>
        /// Consists of the marked to market portfolio positions minus debt plus the current value of the associated account (including converted foreign currencies).
        /// </summary>
        double TotalEquity { get; }

        /// <summary>
        /// Gets the read-only collection of the total equity in home currency per date-time.
        /// <para/>
        /// The total equity consists of the marked to market portfolio positions minus debt plus the current value of the associated account (including converted foreign currencies).
        /// </summary>
        IEnumerable<Scalar> TotalEquityHistory { get; }

        /// <summary>
        /// Gets the current drawdown percentage [0, -1].
        /// </summary>
        double Drawdown { get; }

        /// <summary>
        /// Gets the current maximal drawdown percentage [0, -1].
        /// </summary>
        double MaximalDrawdown { get; }

        /// <summary>
        /// Gets the read-only collection of the drawdown percentage [0, -1] per date-time.
        /// </summary>
        ReadOnlyCollection<Scalar> DrawdownHistory { get; }

        /// <summary>
        /// Gets the read-only collection of the maximal drawdown percentage [0, -1] per date-time.
        /// </summary>
        ReadOnlyCollection<Scalar> DrawdownMaxHistory { get; }
    }
}
