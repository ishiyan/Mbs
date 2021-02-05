using System;

namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    internal class CsvRequest
    {
        /// <summary>
        /// Gets or sets a time-of-day to apply to the end-of-day dates during data fetching.
        /// If <c>null</c>, time is not changed.
        /// </summary>
        public TimeSpan? EndofdayClosingTime { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the first element of the time series.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the last element of the time series.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}