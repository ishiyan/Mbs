using System;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Live
{
    /// <summary>
    /// Generic subscription interface.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public interface ISubscription<out T>
        where T : TemporalEntity
    {
        /// <summary>
        /// An action for the subscription-related events.
        /// </summary>
        event Action<T> SubscriptionAction;

        /// <summary>
        /// Gets a value indicating whether this subscription is active or not.
        /// </summary>
        /// <remarks>
        /// Subscription is not connected before the first call to <see cref="Connect"/>,
        /// and after the first call to <see cref="Disconnect"/>.
        /// </remarks>
        bool IsConnected { get; }

        /// <summary>
        /// Gets the name of the data provider.
        /// </summary>
        string Provider { get; }

        /// <summary>
        /// Gets the time granularity for this subscription.
        /// </summary>
        TimeGranularity TimeGranularity { get; }

        /// <summary>
        /// Gets the instrument.
        /// </summary>
        Instrument Instrument { get; }

        /// <summary>
        /// Connects the subscription. No events may arrive before this is called.
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects the subscription. This makes the subscription no longer usable.
        /// </summary>
        void Disconnect();
    }
}
