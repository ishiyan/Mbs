using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Historical;

namespace Mbs.Api.Services.Trading.Data.Historical
{
    /// <summary>
    /// Fetches an <see cref="Ohlcv"/> historical time series from the Euronext website.
    /// </summary>
    public interface IEuronextHistoricalDataService
    {
        /// <summary>
        /// Fetches an <see cref="Ohlcv"/> historical time series from the Euronext website.
        /// </summary>
        /// <param name="historicalDataRequest">The parameters to generate data.</param>
        /// <returns>A fetched <see cref="Ohlcv"/> time series.</returns>
        Task<IEnumerable<Ohlcv>> FetchOhlcvAsync(HistoricalDataRequest historicalDataRequest);
    }
}
