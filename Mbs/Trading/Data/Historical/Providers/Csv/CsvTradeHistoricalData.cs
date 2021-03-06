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
    /// Provides an access to the intraday historical <see cref="Trade"/> time series stored in CSV files as an async enumerable trade time series.
    /// </summary>
    public sealed class CsvTradeHistoricalData : IHistoricalData<Trade>
    {
        /// <inheritdoc />
        public string Provider => CsvHistoricalData.Provider;

        public async Task<IEnumerable<Trade>> FetchAsyncE(HistoricalDataRequest historicalDataRequest)
        {
            return await Task.Run(() => new List<Trade>());
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<Trade> FetchAsync(HistoricalDataRequest historicalDataRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const string trade = "trade";
            if (historicalDataRequest.TimeGranularity.Equals(TimeGranularity.Aperiodic))
            {
                InstrumentCsvInfo instrumentCsvInfo = CsvHistoricalData.InstrumentInfo(historicalDataRequest.Instrument);
                if (instrumentCsvInfo == null)
                {
                    yield break;
                }

                CsvInfo csvInfo = instrumentCsvInfo.GetFirstData(CsvDataType.Trade);
                if (csvInfo == null)
                {
                    Log.Error(CsvHistoricalData.InstrumentHasNoData(trade));
                    yield break;
                }

                historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                var csvRequest = new CsvRequest { StartDateTime = historicalDataRequest.StartDate, EndDateTime = historicalDataRequest.EndDate };

                await foreach (var t in CsvHistoricalData.EnumerateTradeAsync(csvInfo, csvRequest, cancellationToken))
                {
                    yield return t;
                }
            }

            Log.Error(CsvHistoricalData.TimeGranularityNotSupported(historicalDataRequest.TimeGranularity, trade));
        }
    }
}
