using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Historical
{
    /// <summary>
    /// Provides an access to the Euronext online endofday historical data.
    /// </summary>
    public sealed class EuronextOhlcvHistoricalData : IHistoricalData<Ohlcv>
    {
        /// <inheritdoc />
        public string Provider => EuronextHistoricalData.Provider;

        /// <inheritdoc />
        public async Task<IEnumerable<Ohlcv>> FetchAsync(HistoricalDataRequest historicalDataRequest)
        {
            List<Ohlcv> list = await EuronextHistoricalData.FetchAsync(historicalDataRequest);
            int count = list.Count;
            if (count < 2)
                return list;

            DateTime startDate = list[0].Time;
            if (startDate < historicalDataRequest.StartDate)
                startDate = historicalDataRequest.StartDate;

            DateTime endDate = list[--count].Time;
            if (endDate > historicalDataRequest.EndDate)
                endDate = historicalDataRequest.EndDate;

            switch (historicalDataRequest.TimeGranularity)
            {
                case TimeGranularity.Week1:
                    return list.AggregateWeeks(startDate, endDate, 1);
                case TimeGranularity.Week2:
                    return list.AggregateWeeks(startDate, endDate, 2);
                case TimeGranularity.Week3:
                    return list.AggregateWeeks(startDate, endDate, 3);
                case TimeGranularity.Month1:
                    return list.AggregateMonths(startDate, endDate, 1);
                case TimeGranularity.Month2:
                    return list.AggregateMonths(startDate, endDate, 2);
                case TimeGranularity.Month3:
                    return list.AggregateMonths(startDate, endDate, 3);
                case TimeGranularity.Month4:
                    return list.AggregateMonths(startDate, endDate, 4);
                case TimeGranularity.Month6:
                    return list.AggregateMonths(startDate, endDate, 6);
                case TimeGranularity.Year1:
                    return list.AggregateYears(startDate, endDate, 1);
            }

            return list.Range(startDate, endDate);
        }
    }
}
