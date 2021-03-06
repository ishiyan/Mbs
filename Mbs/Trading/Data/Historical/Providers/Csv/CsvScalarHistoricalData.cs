using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Time;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    /// <summary>
    /// Provides an access to the historical <see cref="Scalar"/> data stored in CSV files as an async enumerable scalar time series.
    /// </summary>
    public sealed class CsvScalarHistoricalData : IHistoricalData<Scalar>
    {
        /// <inheritdoc />
        public string Provider => CsvHistoricalData.Provider;

        public async Task<IEnumerable<Scalar>> FetchAsyncE(HistoricalDataRequest historicalDataRequest)
        {
            return await Task.Run(() => new List<Scalar>());
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<Scalar> FetchAsync(HistoricalDataRequest historicalDataRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            InstrumentCsvInfo instrumentCsvInfo = CsvHistoricalData.InstrumentInfo(historicalDataRequest.Instrument);
            if (instrumentCsvInfo == null)
            {
                yield break;
            }

            if (!instrumentCsvInfo.HasScalarData)
            {
                Log.Error(CsvHistoricalData.InstrumentHasNoData("scalar"));
                yield break;
            }

            var csvRequest = new CsvRequest { StartDateTime = historicalDataRequest.StartDate, EndDateTime = historicalDataRequest.EndDate };
            TimeGranularity timeGranularity = historicalDataRequest.TimeGranularity;
            bool isEndofday = timeGranularity.IsEndofday();
            CsvInfo csvInfo = instrumentCsvInfo.GetMatchingData(CsvDataType.Scalar, timeGranularity);
            if (csvInfo != null)
            {
                historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                {
                    csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                }

                await foreach (var s in CsvHistoricalData.EnumerateScalarAsync(csvInfo, csvRequest, cancellationToken))
                {
                    yield return s;
                }

                yield break;
            }

            csvInfo = instrumentCsvInfo.GetAggregateScalarData(timeGranularity);
            if (csvInfo != null)
            {
                Func<TemporalEntity, int, DateTime> thresholdDateTime;
                if (timeGranularity.IsDay())
                {
                    thresholdDateTime = AggregatingConverter.DayBinThreshold;
                }
                else if (timeGranularity.IsHour())
                {
                    thresholdDateTime = AggregatingConverter.HourBinThreshold;
                }
                else if (timeGranularity.IsMinute())
                {
                    thresholdDateTime = AggregatingConverter.MinuteBinThreshold;
                }
                else if (timeGranularity.IsSecond())
                {
                    thresholdDateTime = AggregatingConverter.SecondBinThreshold;
                }
                else if (timeGranularity.IsWeek())
                {
                    thresholdDateTime = AggregatingConverter.WeekBinThreshold;
                }
                else if (timeGranularity.IsMonth())
                {
                    thresholdDateTime = AggregatingConverter.MonthBinThreshold;
                }
                else if (timeGranularity.IsYear())
                {
                    thresholdDateTime = AggregatingConverter.YearBinThreshold;
                }
                else
                {
                    thresholdDateTime = null; // Time granularity is in trades.
                }

                historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                {
                    csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                }

                await foreach (var s in AggregatingConverter.AggregateScalarAsync(
                    CsvHistoricalData.EnumerateScalarAsync(csvInfo, csvRequest, cancellationToken),
                    timeGranularity.NumberOfUnits(),
                    thresholdDateTime).WithCancellation(cancellationToken))
                {
                    yield return s;
                }

                yield break;
            }

            Log.Error(CsvHistoricalData.CannotComposeGranularity(timeGranularity));
        }
    }
}
