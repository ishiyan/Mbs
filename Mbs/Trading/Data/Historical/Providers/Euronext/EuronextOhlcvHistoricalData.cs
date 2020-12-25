﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Time;

// ReSharper disable once CheckNamespace
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
            {
                return list;
            }

            DateTime startDate = list[0].Time;
            if (startDate < historicalDataRequest.StartDate)
            {
                startDate = historicalDataRequest.StartDate;
            }

            DateTime endDate = list[--count].Time;
            if (endDate > historicalDataRequest.EndDate)
            {
                endDate = historicalDataRequest.EndDate;
            }

            return historicalDataRequest.TimeGranularity switch
            {
                TimeGranularity.Week1 => list.AggregateWeeks(startDate, endDate, 1),
                TimeGranularity.Week2 => list.AggregateWeeks(startDate, endDate, 2),
                TimeGranularity.Week3 => list.AggregateWeeks(startDate, endDate, 3),
                TimeGranularity.Month1 => list.AggregateMonths(startDate, endDate, 1),
                TimeGranularity.Month2 => list.AggregateMonths(startDate, endDate, 2),
                TimeGranularity.Month3 => list.AggregateMonths(startDate, endDate, 3),
                TimeGranularity.Month4 => list.AggregateMonths(startDate, endDate, 4),
                TimeGranularity.Month6 => list.AggregateMonths(startDate, endDate, 6),
                TimeGranularity.Year1 => list.AggregateYears(startDate, endDate, 1),
                _ => list.Range(startDate, endDate)
            };
        }
    }
}
