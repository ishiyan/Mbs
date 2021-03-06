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
    /// Provides an access to the intraday historical <see cref="Quote"/> time series stored in CSV files as an enumerable quote time series.
    /// </summary>
    public sealed class CsvQuoteHistoricalData : IHistoricalData<Quote>
    {
        /// <inheritdoc />
        public string Provider => CsvHistoricalData.Provider;

        public async Task<IEnumerable<Quote>> FetchAsyncE(HistoricalDataRequest historicalDataRequest)
        {
            return await Task.Run(() => new List<Quote>());
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<Quote> FetchAsync(HistoricalDataRequest historicalDataRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const string quote = "quote";
            if (historicalDataRequest.TimeGranularity.Equals(TimeGranularity.Aperiodic))
            {
                InstrumentCsvInfo instrumentCsvInfo = CsvHistoricalData.InstrumentInfo(historicalDataRequest.Instrument);
                if (instrumentCsvInfo == null)
                {
                    yield break;
                }

                CsvInfo csvInfo = instrumentCsvInfo.GetFirstData(CsvDataType.Quote);
                if (csvInfo == null)
                {
                    Log.Error(CsvHistoricalData.InstrumentHasNoData(quote));
                    yield break;
                }

                historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                var csvRequest = new CsvRequest { StartDateTime = historicalDataRequest.StartDate, EndDateTime = historicalDataRequest.EndDate };

                await foreach (var q in CsvHistoricalData.EnumerateQuoteAsync(csvInfo, csvRequest, cancellationToken))
                {
                    yield return q;
                }

                yield break;
            }

            Log.Error(CsvHistoricalData.TimeGranularityNotSupported(historicalDataRequest.TimeGranularity, quote));
        }
    }
}
