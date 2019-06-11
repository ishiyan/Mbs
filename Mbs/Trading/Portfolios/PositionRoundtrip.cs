using System;
using Mbs.Trading.Instruments;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// Represents a position round trip.
    /// </summary>
    public class PositionRoundtrip
    {
        /// <summary>
        /// Gets or sets the traded instrument.
        /// </summary>
        public Instrument Instrument { get; set; }

        /// <summary>
        /// Gets or sets the date and time the position was opened.
        /// </summary>
        public DateTime EntryTime { get; set; }

        /// <summary>
        /// Gets or sets the (average) price at which the position was opened.
        /// </summary>
        public double EntryPrice { get; set; }

        /// <summary>
        /// Gets or sets the side of the position.
        /// </summary>
        public PositionSide Side { get; set; }

        /// <summary>
        /// Gets or sets the total unsigned quantity of the position.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the date and time the position was closed.
        /// </summary>
        public DateTime ExitTime { get; set; }

        /// <summary>
        /// Gets or sets the (average) price at which the position was closed.
        /// </summary>
        public double ExitPrice { get; set; }

        /// <summary>
        /// Gets or sets the gross profit and loss (PnL) of the round trip in instrument currency.
        /// </summary>
        public double ProfitAndLoss { get; set; }

        /// <summary>
        /// Gets or sets the total commission associated with the round trip in instrument currency. This is always a positive value.
        /// </summary>
        public double TotalCommission { get; set; }

        /// <summary>
        /// Gets or sets the Maximum Adverse Excursion (MAE) in instrument currency.
        /// </summary>
        public double MaximumAdverseExcursion { get; set; }

        /// <summary>
        /// Gets or sets the Maximum Favorable Excursion (MFE) in instrument currency.
        /// </summary>
        public double MaximumFavorableExcursion { get; set; }

        /// <summary>
        /// Gets the duration of the round trip. This is a calculated property.
        /// </summary>
        public TimeSpan Duration
        {
            get { return ExitTime - EntryTime; }
        }

        /// <summary>
        /// Gets the amount of profit given back before the position was closed.
        /// </summary>
        public double EndTradeDrawdown
        {
            get { return ProfitAndLoss - MaximumFavorableExcursion; }
        }
    }
}
