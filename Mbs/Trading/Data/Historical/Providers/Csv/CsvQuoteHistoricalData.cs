using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Time;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    /// <summary>
    /// Provides an access to the intraday historical quote time series stored in CSV files as an enumerable quote time series.
    /// </summary>
    public sealed class CsvQuoteHistoricalData : IHistoricalData<Quote>
    {
        /// <inheritdoc />
        public string Provider => CsvRepository.Provider;

        /// <summary>
        /// Given a historical data request, creates an interface to enumerate the quote time series.
        /// <para />
        /// To enumerate the all available data, pass the <c>DateTime.MinValue</c> as a begin time and <c>DateTime.MaxValue</c> as an end time.
        /// </summary>
        /// <param name="historicalDataRequest">The historical data request.</param>
        /// <returns>An enumerable interface.</returns>
        public async Task<IEnumerable<Quote>> FetchAsync(HistoricalDataRequest historicalDataRequest)
        {
            return await Task.Run(() => Fetch(historicalDataRequest));
        }

        public IAsyncEnumerable<Quote> FetchAsyncE(HistoricalDataRequest historicalDataRequest)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<Quote> Fetch(HistoricalDataRequest historicalDataRequest)
        {
            const string quote = "quote";
            if (historicalDataRequest.TimeGranularity.Equals(TimeGranularity.Aperiodic))
            {
                InstrumentCsvInfo instrumentCsvInfo = CsvRepository.InstrumentInfo(historicalDataRequest.Instrument);
                if (instrumentCsvInfo == null)
                {
                    return new List<Quote>();
                }

                CsvInfo csvInfo = instrumentCsvInfo.GetFirstData(CsvDataType.Quote);
                if (csvInfo == null)
                {
                    Log.Error(CsvRepository.InstrumentHasNoData(quote));
                    return new List<Quote>();
                }

                historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                var csvRequest = new CsvRequest { StartDate = historicalDataRequest.StartDate, EndDate = historicalDataRequest.EndDate };
                return CsvRepository.EnumerateQuoteAsync(csvInfo, csvRequest);
            }

            Log.Error(CsvRepository.TimeGranularityNotSupported(historicalDataRequest.TimeGranularity, quote));
            return new List<Quote>();
        }
    }
}
