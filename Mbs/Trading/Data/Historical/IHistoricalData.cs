using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
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
        /// To enumerate the all available data, pass the <see cref="DateTime.MinValue"/> as a start time and the
        /// <see cref="DateTime.MaxValue"/> as an end time in the <see cref="HistoricalDataRequest"/>.
        /// </remarks>
        /// <param name="historicalDataRequest">A historical data series specification.</param>
        /// <returns>An enumerable interface.</returns>
        Task<IEnumerable<T>> FetchAsyncE(HistoricalDataRequest historicalDataRequest);

        /// <summary>
        /// An async enumerable interface to enumerate a series of historical data events asynchronously in a temporal order.
        /// </summary>
        /// <remarks>
        /// To enumerate the all available data, pass the <see cref="DateTime.MinValue"/> as a start time and the
        /// <see cref="DateTime.MaxValue"/> as an end time in the <see cref="HistoricalDataRequest"/>.
        /// </remarks>
        /// <param name="historicalDataRequest">A historical data series specification.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>An async enumerable interface.</returns>
        IAsyncEnumerable<T> FetchAsync(HistoricalDataRequest historicalDataRequest, CancellationToken cancellationToken);
    }
}
