using System;
using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data
{
    /// <summary>
    /// A generic data publishing interface.
    /// </summary>
    public interface IDataPublisher
    {
        /// <summary>
        /// Subscribes to a specified instrument.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="instrument">The instrument.</param>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <param name="action">The action.</param>
        /// <returns>The subscription interface or null is not successfull.</returns>
        ISubscription<T> Subscribe<T>(Instrument instrument, TimeGranularity timeGranularity, Action<T> action)
            where T : TemporalEntity;

        /// <summary>
        /// Start monitoring the last entity for an instrument.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="instrument">The instrument.</param>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <returns>The subscription or null if failed to subscribe.</returns>
        ISubscription<T> Monitor<T>(Instrument instrument, TimeGranularity timeGranularity)
            where T : TemporalEntity;

        /// <summary>
        /// Start monitoring the last entity for an instrument.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="instrument">The instrument.</param>
        /// <returns>The subscription or null if failed to subscribe.</returns>
        ISubscription<T> Monitor<T>(Instrument instrument)
            where T : TemporalEntity;

        /// <summary>
        /// The last known temporal entity data of a monitored instrument.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="instrument">The instrument. This instrument must be monitored.</param>
        /// <returns>The data or null if unknown or not monitored./>.</returns>
        T Last<T>(Instrument instrument)
            where T : TemporalEntity;

        /// <summary>
        /// The last known bid price of a monitored instrument.
        /// </summary>
        /// <param name="instrument">The instrument. This instrument must be monitored.</param>
        /// <returns>The price or NaN if unknown or not monitored.</returns>
        double LastBuyPrice(Instrument instrument);

        /// <summary>
        /// The last known ask price of a monitored instrument.
        /// </summary>
        /// <param name="instrument">The instrument. This instrument must be monitored.</param>
        /// <returns>The price or NaN if unknown or not monitored.</returns>
        double LastSellPrice(Instrument instrument);
    }
}
