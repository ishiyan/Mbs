using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Time;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    /// <summary>
    /// Provides an access to the historical scalar data stored in CSV files as an enumerable scalar time series.
    /// </summary>
    public sealed class CsvScalarHistoricalData : IHistoricalData<Scalar>
    {
        /// <inheritdoc />
        public string Provider => CsvRepository.Provider;

        /// <summary>
        /// Given a historical data request, creates an interface to enumerate the scalar time series.
        /// <para />
        /// To enumerate the all available data, pass the <c>DateTime.MinValue</c> as a begin time and <c>DateTime.MaxValue</c> as an end time in the time series specification.
        /// </summary>
        /// <param name="historicalDataRequest">The historical data request.</param>
        /// <returns>An enumerable interface.</returns>
        public async Task<IEnumerable<Scalar>> FetchAsync(HistoricalDataRequest historicalDataRequest)
        {
            return await Task.Run(() => Fetch(historicalDataRequest));
        }

        public IAsyncEnumerable<Scalar> FetchAsyncE(HistoricalDataRequest historicalDataRequest)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Scalar> Fetch(HistoricalDataRequest historicalDataRequest)
        {
            InstrumentCsvInfo instrumentCsvInfo = CsvRepository.InstrumentInfo(historicalDataRequest.Instrument);
            if (instrumentCsvInfo == null)
            {
                return new List<Scalar>();
            }

            if (!instrumentCsvInfo.HasScalarData)
            {
                Log.Error(CsvRepository.InstrumentHasNoData("scalar"));
                return new List<Scalar>();
            }

            var csvRequest = new CsvRequest { StartDate = historicalDataRequest.StartDate, EndDate = historicalDataRequest.EndDate };
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

                return CsvRepository.EnumerateScalarAsync(csvInfo, csvRequest);
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

                IEnumerable<Scalar> enumerable = CsvRepository.EnumerateScalarAsync(csvInfo, csvRequest);
                return AggregatingConverter.Aggregate(enumerable, timeGranularity.NumberOfUnits(), thresholdDateTime);
            }

            Log.Error(CsvRepository.CannotComposeGranularity(timeGranularity));
            return new List<Scalar>();
        }
    }
}
