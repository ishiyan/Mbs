using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Time;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    /// <summary>
    /// Provides an access to the historical ohlcv data stored in CSV files as an enumerable ohlcv time series.
    /// </summary>
    public sealed class CsvOhlcvHistoricalData : IHistoricalData<Ohlcv>
    {
        /// <inheritdoc />
        public string Provider => CsvRepository.Provider;

        /// <summary>
        /// Given a historical data request, creates an interface to enumerate the ohlcv time series.
        /// <para />
        /// To enumerate the all available data, pass the <c>DateTime.MinValue</c> as a begin time and <c>DateTime.MaxValue</c> as an end time in the time series specification.
        /// </summary>
        /// <param name="historicalDataRequest">The historical data request.</param>
        /// <returns>An enumerable interface.</returns>
        public async Task<IEnumerable<Ohlcv>> FetchAsync(HistoricalDataRequest historicalDataRequest)
        {
            return await Task.Run(() => Fetch(historicalDataRequest));
        }

        public IAsyncEnumerable<Ohlcv> FetchAsyncE(HistoricalDataRequest historicalDataRequest)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Ohlcv> Fetch(HistoricalDataRequest historicalDataRequest)
        {
            InstrumentCsvInfo instrumentCsvInfo = CsvRepository.InstrumentInfo(historicalDataRequest.Instrument);
            if (instrumentCsvInfo == null)
            {
                return new List<Ohlcv>();
            }

            var csvRequest = new CsvRequest { StartDate = historicalDataRequest.StartDate, EndDate = historicalDataRequest.EndDate };
            TimeGranularity timeGranularity = historicalDataRequest.TimeGranularity;
            bool isEndofday = timeGranularity.IsEndofday();
            Func<TemporalEntity, int, DateTime> thresholdDateTime = AggregatingConverter.SelectThresholdDateTime(timeGranularity);

            if (historicalDataRequest.AdjustedDataIfPresent)
            {
                CsvInfo csvInfo = instrumentCsvInfo.GetMatchingAdjustedData(CsvDataType.Ohlcv, timeGranularity);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                    {
                        csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                    }

                    return CsvRepository.EnumerateOhlcvAsync(csvInfo, csvRequest);
                }

                csvInfo = instrumentCsvInfo.GetAggregateAdjustedOhlcvData(timeGranularity);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                    {
                        csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                    }

                    var enumerable = CsvRepository.EnumerateOhlcvAsync(csvInfo, csvRequest);
                    return AggregatingConverter.Aggregate(enumerable, timeGranularity, thresholdDateTime);
                }
            }

            if (instrumentCsvInfo.HasOhlcvData)
            {
                CsvInfo csvInfo = instrumentCsvInfo.GetMatchingData(CsvDataType.Ohlcv, timeGranularity);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                    {
                        csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                    }

                    return CsvRepository.EnumerateOhlcvAsync(csvInfo, csvRequest);
                }

                csvInfo = instrumentCsvInfo.GetAggregateOhlcvData(timeGranularity);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                    {
                        csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                    }

                    var enumerable = CsvRepository.EnumerateOhlcvAsync(csvInfo, csvRequest);
                    return AggregatingConverter.Aggregate(enumerable, timeGranularity, thresholdDateTime);
                }
            }

            if (instrumentCsvInfo.HasTradeData)
            {
                // Trade data can be always aggregated.
                CsvInfo csvInfo = instrumentCsvInfo.GetMatchingData(CsvDataType.Trade, TimeGranularity.Aperiodic);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    var enumerable = CsvRepository.EnumerateTradeAsync(csvInfo, csvRequest);
                    return AggregatingConverter.Aggregate(enumerable, timeGranularity, thresholdDateTime);
                }
            }

            if (instrumentCsvInfo.HasScalarData)
            {
                CsvInfo csvInfo = instrumentCsvInfo.GetMatchingData(CsvDataType.Scalar, timeGranularity);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                    {
                        csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                    }

                    var enumerable = CsvRepository.EnumerateScalarAsync(csvInfo, csvRequest);
                    return AggregatingConverter.ConvertToOhlcv(enumerable);
                }

                csvInfo = instrumentCsvInfo.GetAggregateScalarData(timeGranularity);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                    {
                        csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                    }

                    var enumerable = CsvRepository.EnumerateScalarAsync(csvInfo, csvRequest);
                    return AggregatingConverter.Aggregate(enumerable, timeGranularity, thresholdDateTime);
                }
            }

            Log.Error(CsvRepository.CannotComposeGranularity(timeGranularity));
            return new List<Ohlcv>();
        }
    }
}
