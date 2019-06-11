using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Historical
{
    /// <summary>
    /// Provides an access to the intraday historical trade time series stored in CSV files as an enumerable trade time series.
    /// </summary>
    public sealed class CsvTradeHistoricalData : IHistoricalData<Trade>
    {
        /// <inheritdoc />
        public string Provider => CsvRepository.Provider;

        /// <summary>
        /// Given a historical data request, creates an interface to enumerate the trade time series.
        /// <para />
        /// To enumerate the all available data, pass the <c>DateTime.MinValue</c> as a begin time and <c>DateTime.MaxValue</c> as an end time.
        /// </summary>
        /// <param name="historicalDataRequest">The historical data request.</param>
        /// <returns>An enumerable interface.</returns>
        public async Task<IEnumerable<Trade>> FetchAsync(HistoricalDataRequest historicalDataRequest)
        {
            return await Task.Run(() => Fetch(historicalDataRequest));
        }

        private IEnumerable<Trade> Fetch(HistoricalDataRequest historicalDataRequest)
        {
            const string trade = "trade";
            if (historicalDataRequest.TimeGranularity.Equals(TimeGranularity.Aperiodic))
            {
                InstrumentCsvInfo instrumentCsvInfo = CsvRepository.InstrumentInfo(historicalDataRequest.Instrument);
                if (instrumentCsvInfo == null)
                    return new List<Trade>();
                CsvInfo csvInfo = instrumentCsvInfo.GetFirstData(CsvDataType.Trade);
                if (csvInfo == null)
                {
                    Log.Error(CsvRepository.InstrumentHasNoData(trade));
                    return new List<Trade>();
                }

                historicalDataRequest.IsDataAdjusted = csvInfo.IsAdjustedData;
                var csvRequest = new CsvRequest { StartDate = historicalDataRequest.StartDate, EndDate = historicalDataRequest.EndDate };
                return CsvRepository.EnumerateTradeAsync(csvInfo, csvRequest);
            }

            Log.Error(CsvRepository.TimeGranularityNotSupported(historicalDataRequest.TimeGranularity, trade));
            return new List<Trade>();
        }
    }
}
