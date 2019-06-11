using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Api.Services.Trading.Data.Historical;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Historical;

namespace Mbs.Api.Host.IntegrationTests.Controllers.Trading.Data.Historical
{
    /// <inheritdoc/>
    public class MockEuronextHistoricalDataService : IEuronextHistoricalDataService
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<Ohlcv>> FetchOhlcvAsync(HistoricalDataRequest historicalDataRequest)
        {
            return await Task<IEnumerable<Ohlcv>>.Run(() => new List<Ohlcv>
            {
                new Ohlcv(),
            });
        }
    }
}
