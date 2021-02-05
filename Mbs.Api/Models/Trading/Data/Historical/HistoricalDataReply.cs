using System.Collections.Generic;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;

namespace Mbs.Api.Models.Trading.Data.Historical
{
    /// <summary>
    /// A generic historical data reply.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public class HistoricalDataReply<T>
        where T : TemporalEntity
    {
        /// <summary>
        /// Gets if data is adjusted or null if unknown.
        /// </summary>
        public bool? IsDataAdjusted { get; internal set; }

        /// <summary>
        /// Gets the enumerable data.
        /// </summary>
        public IEnumerable<T> Data { get; internal set; }
    }
}
