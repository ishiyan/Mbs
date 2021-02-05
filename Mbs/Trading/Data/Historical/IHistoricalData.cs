using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Historical
{
    /// <summary>
    /// A generic enumerable data provider interface.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public interface IHistoricalData<T>
        where T : TemporalEntity
    {
        /// <summary>
        /// Gets the name of the data provider.
        /// </summary>
        string Provider { get; }

        /// <summary>
        /// An enumerable interface to enumerate a series of historical data events in the temporal order.
        /// </summary>
        /// <remarks>
        /// To enumerate the all available data, pass the <see cref="DateTime.MinValue"/> as a begin time and the
        /// <see cref="DateTime.MaxValue"/> as an end time in the <see cref="HistoricalDataRequest"/>.
        /// </remarks>
        /// <param name="historicalDataRequest">A historical data series specification.</param>
        /// <returns>An enumerable interface.</returns>
        Task<IEnumerable<T>> FetchAsync(HistoricalDataRequest historicalDataRequest);

        /// <summary>
        /// An enumerable interface to enumerate a series of historical data events asynchronously in the temporal order.
        /// </summary>
        /// <remarks>
        /// To enumerate the all available data, pass the <see cref="DateTime.MinValue"/> as a begin time and the
        /// <see cref="DateTime.MaxValue"/> as an end time in the <see cref="HistoricalDataRequest"/>.
        /// </remarks>
        /// <param name="historicalDataRequest">A historical data series specification.</param>
        /// <returns>An async enumerable interface.</returns>
        IAsyncEnumerable<T> FetchAsyncE(HistoricalDataRequest historicalDataRequest);
    }
}
