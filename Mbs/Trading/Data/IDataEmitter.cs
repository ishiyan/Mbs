using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data
{
    /// <summary>
    /// A generic data emitter interface.
    /// </summary>
    public interface IDataEmitter : IDataPublisher
    {
        /// <summary>
        /// Adds a subscription provider of the specified type.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="subscriptionProvider">The subscription provider of the specified type.</param>
        void Add<T>(ISubscriptionProvider<T> subscriptionProvider)
            where T : TemporalEntity;

        /// <summary>
        /// Cancels all current active subscriptions.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Start monitoring the last entity for an instrument.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="instrument">The instrument.</param>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <param name="vendor">Specifies the vendor if not null or empty.</param>
        /// <returns>The subscription or null if failed to subscribe.</returns>
        ISubscription<T> Monitor<T>(Instrument instrument, TimeGranularity timeGranularity, string vendor)
            where T : TemporalEntity;

        /// <summary>
        /// The default time granularity of the monitoring.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <returns>The time granularity.</returns>
        TimeGranularity DefaultMonitorGranularity<T>()
            where T : TemporalEntity;

        /// <summary>
        /// Sets the default time granularity of the monitoring.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="timeGranularity">The time granularity.</param>
        void DefaultMonitorGranularity<T>(TimeGranularity timeGranularity)
            where T : TemporalEntity;
    }
}
