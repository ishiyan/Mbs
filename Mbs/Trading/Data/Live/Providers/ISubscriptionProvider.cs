using System;
using System.Collections.ObjectModel;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;

// ReSharper disable once CheckNamespace
namespace Mbs.Trading.Data.Live
{
    /// <summary>
    /// Generic subscription provider.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public interface ISubscriptionProvider<T>
        where T : TemporalEntity
    {
        /// <summary>
        /// Gets the name of the data provider.
        /// </summary>
        string Provider { get; }

        /// <summary>
        /// Gets a read only collection of all current subscriptions excluding the canceled ones.
        /// </summary>
        ReadOnlyCollection<ISubscription<T>> Subscriptions { get; }

        /// <summary>
        /// Subscribes to a new subscription.
        /// </summary>
        /// <param name="instrument">The instrument to subscribe.</param>
        /// <param name="timeGranularity">The time granularity to subscribe.</param>
        /// <returns>The subscription interface.</returns>
        ISubscription<T> Subscribe(Instrument instrument, TimeGranularity timeGranularity);

        /// <summary>
        /// Subscribes to a new subscription.
        /// </summary>
        /// <param name="instrument">The instrument to subscribe.</param>
        /// <param name="timeGranularity">The time granularity to subscribe.</param>
        /// <param name="action">A caller-specified action.</param>
        /// <returns>The subscription interface.</returns>
        ISubscription<T> Subscribe(Instrument instrument, TimeGranularity timeGranularity, Action<T> action);

        /// <summary>
        /// Connects all current not active subscriptions.
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects all current active subscriptions.
        /// </summary>
        void Disconnect();
    }
}
