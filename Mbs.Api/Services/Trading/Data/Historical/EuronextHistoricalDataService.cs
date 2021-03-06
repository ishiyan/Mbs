using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Data.Historical.Providers.Euronext;

namespace Mbs.Api.Services.Trading.Data.Historical
{
    /// <inheritdoc/>
    public class EuronextHistoricalDataService : IEuronextHistoricalDataService
    {
        private static readonly EuronextOhlcvHistoricalData EuronextOhlcvHistoricalData = new EuronextOhlcvHistoricalData();

        /// <inheritdoc/>
        public async Task<IEnumerable<Ohlcv>> FetchOhlcvAsync(HistoricalDataRequest historicalDataRequest)
        {
            return await EuronextOhlcvHistoricalData.FetchAsyncE(historicalDataRequest).ConfigureAwait(false);
        }
    }
}
