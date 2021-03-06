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
    /// Provides an access to the historical <see cref="Ohlcv"/> data stored in CSV files as an async enumerable ohlcv time series.
    /// </summary>
    public sealed class CsvOhlcvHistoricalData : IHistoricalData<Ohlcv>
    {
        /// <inheritdoc />
        public string Provider => CsvHistoricalData.Provider;

        public async Task<IEnumerable<Ohlcv>> FetchAsyncE(HistoricalDataRequest historicalDataRequest)
        {
            return await Task.Run(() => new List<Ohlcv>());
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<Ohlcv> FetchAsync(HistoricalDataRequest historicalDataRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            InstrumentCsvInfo instrumentCsvInfo = CsvHistoricalData.InstrumentInfo(historicalDataRequest.Instrument);
            if (instrumentCsvInfo == null)
            {
                yield break;
            }

            var csvRequest = new CsvRequest { StartDateTime = historicalDataRequest.StartDate, EndDateTime = historicalDataRequest.EndDate };
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

                    await foreach (var ohlcv in CsvHistoricalData.EnumerateOhlcvAsync(csvInfo, csvRequest, cancellationToken))
                    {
                        yield return ohlcv;
                    }

                    yield break;
                }

                csvInfo = instrumentCsvInfo.GetAggregateAdjustedOhlcvData(timeGranularity);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                    {
                        csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                    }

                    await foreach (var ohlcv in AggregatingConverter.AggregateAsync(
                        CsvHistoricalData.EnumerateOhlcvAsync(csvInfo, csvRequest, cancellationToken),
                        timeGranularity,
                        thresholdDateTime).WithCancellation(cancellationToken))
                    {
                        yield return ohlcv;
                    }

                    yield break;
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

                    await foreach (var ohlcv in CsvHistoricalData.EnumerateOhlcvAsync(csvInfo, csvRequest, cancellationToken))
                    {
                        yield return ohlcv;
                    }

                    yield break;
                }

                csvInfo = instrumentCsvInfo.GetAggregateOhlcvData(timeGranularity);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                    {
                        csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                    }

                    await foreach (var ohlcv in AggregatingConverter.AggregateAsync(
                        CsvHistoricalData.EnumerateOhlcvAsync(csvInfo, csvRequest, cancellationToken),
                        timeGranularity,
                        thresholdDateTime).WithCancellation(cancellationToken))
                    {
                        yield return ohlcv;
                    }

                    yield break;
                }
            }

            if (instrumentCsvInfo.HasTradeData)
            {
                // Trade data can be always aggregated.
                CsvInfo csvInfo = instrumentCsvInfo.GetMatchingData(CsvDataType.Trade, TimeGranularity.Aperiodic);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;

                    await foreach (var ohlcv in AggregatingConverter.AggregateAsync(
                        CsvHistoricalData.EnumerateTradeAsync(csvInfo, csvRequest, cancellationToken),
                        timeGranularity,
                        thresholdDateTime).WithCancellation(cancellationToken))
                    {
                        yield return ohlcv;
                    }

                    yield break;
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

                    await foreach (var ohlcv in AggregatingConverter.ConvertToOhlcvAsync(
                        CsvHistoricalData.EnumerateScalarAsync(csvInfo, csvRequest, cancellationToken))
                        .WithCancellation(cancellationToken))
                    {
                        yield return ohlcv;
                    }

                    yield break;
                }

                csvInfo = instrumentCsvInfo.GetAggregateScalarData(timeGranularity);
                if (csvInfo != null)
                {
                    historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                    if (isEndofday && csvInfo.TimeGranularity.IsEndofday())
                    {
                        csvRequest.EndofdayClosingTime = historicalDataRequest.EndofdayClosingTime;
                    }

                    await foreach (var ohlcv in AggregatingConverter.AggregateAsync(
                        CsvHistoricalData.EnumerateScalarAsync(csvInfo, csvRequest, cancellationToken),
                        timeGranularity,
                        thresholdDateTime).WithCancellation(cancellationToken))
                    {
                        yield return ohlcv;
                    }

                    yield break;
                }
            }

            Log.Error(CsvHistoricalData.CannotComposeGranularity(timeGranularity));
        }
    }
}
