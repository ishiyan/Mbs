using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;
#pragma warning disable SA1401 // Fields should be private

// ReSharper disable once CheckNamespace
namespace Mbs.Trading.Data.Live
{
    /// <summary>
    /// Generic subscription provider.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public abstract class SubscriptionProvider<T>
        where T : TemporalEntity
    {
        private readonly object providersLock = new object();
        private readonly object subscriptionsLock = new object();
        private readonly Dictionary<Topic, GenericProvider> providers = new Dictionary<Topic, GenericProvider>();
        private readonly List<ISubscription<T>> subscriptions = new List<ISubscription<T>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionProvider{T}"/> class.
        /// </summary>
        /// <param name="provider">The data provider.</param>
        protected SubscriptionProvider(string provider)
        {
            Provider = provider;
        }

        /// <summary>
        /// Gets the data provider name of this subscription provider.
        /// </summary>
        public string Provider { get; }

        /// <summary>
        /// Gets all current subscriptions.
        /// </summary>
        public ReadOnlyCollection<ISubscription<T>> Subscriptions
        {
            get
            {
                lock (subscriptionsLock)
                {
                    return new ReadOnlyCollection<ISubscription<T>>(subscriptions);
                }
            }
        }

        /// <summary>
        /// Connect all current subscriptions.
        /// </summary>
        public void Connect()
        {
            var list = new List<ISubscription<T>>();
            lock (subscriptionsLock)
            {
                list.AddRange(subscriptions);
            }

            foreach (var subscriber in list)
            {
                if (!subscriber.IsConnected)
                {
                    subscriber.Connect();
                }
            }
        }

        /// <summary>
        /// Disconnect all current subscriptions.
        /// </summary>
        public void Disconnect()
        {
            var list = new List<ISubscription<T>>();
            lock (subscriptionsLock)
            {
                list.AddRange(subscriptions);
            }

            foreach (var subscriber in list)
            {
                if (subscriber.IsConnected)
                {
                    subscriber.Disconnect();
                }
            }
        }

        /// <summary>
        /// Creates a provider.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="provider">The data provider name.</param>
        /// <returns>The GenericProvider.</returns>
        protected abstract GenericProvider CreateProvider(Topic topic, string provider);

        /// <summary>
        /// Performs a subscription.
        /// </summary>
        /// <param name="instrument">The instrument.</param>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <param name="action">An action.</param>
        /// <returns>A subscription interface.</returns>
        protected ISubscription<T> Subscribe(Instrument instrument, TimeGranularity timeGranularity, Action<T> action = null)
        {
            var topic = new Topic(instrument, timeGranularity);
            GenericProvider genericProvider;
            lock (providersLock)
            {
                if (!providers.TryGetValue(topic, out genericProvider))
                {
                    genericProvider = CreateProvider(topic, Provider);
                    providers.Add(topic, genericProvider);
                }
            }

            return new Subscriber(this, genericProvider, action);
        }

        /// <summary>
        /// A generic provider.
        /// </summary>
        protected abstract class GenericProvider : IDisposable
        {
            private readonly AutoResetEventThread reTransmitterThread;
            private readonly CompareAndSwapQueue<T> reTransmitterQueue;
            private readonly object providerEventLock = new object();
            private readonly List<T> historyList = new List<T>(1024);
            private Action<T> providerEvent;
            private int referenceCount;
            private volatile bool active;

            /// <summary>
            /// Initializes a new instance of the <see cref="GenericProvider"/> class.
            /// </summary>
            /// <param name="topic">The topic.</param>
            /// <param name="provider">The data provider name.</param>
            /// <param name="isSynchronous">If in synchronous mode.</param>
            protected GenericProvider(Topic topic, string provider, bool isSynchronous = false)
            {
                Topic = topic;
                Provider = provider;
                IsSynchronous = isSynchronous;
                if (!isSynchronous)
                {
                    reTransmitterQueue = new CompareAndSwapQueue<T>();
                    reTransmitterThread = new AutoResetEventThread(() =>
                    {
                        void Action()
                        {
                            T entity;
                            while ((entity = reTransmitterQueue.Dequeue()) != null)
                            {
                                Delegate[] handlers = null;
                                lock (providerEventLock)
                                {
                                    if (providerEvent != null)
                                    {
                                        handlers = providerEvent.GetInvocationList();
                                    }
                                }

                                if (handlers == null)
                                {
                                    return;
                                }

                                foreach (Delegate currentHandler in handlers)
                                {
                                    var currentSubscriber = currentHandler as Action<T>;
                                    currentSubscriber?.Invoke(entity);
                                }
                            }
                        }

                        while (active)
                        {
                            Action();
                            if (active)
                            {
                                reTransmitterThread.AutoResetEvent.WaitOne(1000, false);
                            }
                        }

                        // Output the entities left in the re-transmitter queue.
                        // Action()
                        Dispose();
                    })
                    { Thread = { IsBackground = true } };
                }
            }

            /// <summary>
            /// The event handler.
            /// </summary>
            public event Action<T> Event
            {
                add
                {
                    lock (providerEventLock)
                    {
                        providerEvent += value;
                        foreach (var t in historyList)
                        {
                            value(t);
                        }
                    }
                }

                remove
                {
                    lock (providerEventLock)
                    {
                        // ReSharper disable once DelegateSubtraction
                        providerEvent -= value;
                    }
                }
            }

            /// <summary>
            /// Gets the data provider name.
            /// </summary>
            public string Provider { get; }

            /// <summary>
            /// Gets the topic.
            /// </summary>
            public Topic Topic { get; }

            /// <summary>
            /// Gets a value indicating whether the provider is in synchronous mode.
            /// </summary>
            private bool IsSynchronous { get; }

            /// <summary>
            /// Connects this subscription.
            /// </summary>
            public void Connect()
            {
                if (active)
                {
                    return;
                }

                active = true;
                if (!IsSynchronous)
                {
                    reTransmitterThread.Thread.Start();
                }

                OnConnect();
            }

            /// <summary>
            /// Disconnects this subscription.
            /// </summary>
            public void Disconnect()
            {
                if (!active)
                {
                    return;
                }

                active = false;
                if (!IsSynchronous)
                {
                    reTransmitterThread.AutoResetEvent.Set();
                }

                OnDisconnect();
            }

            /// <inheritdoc />
            public void Dispose()
            {
                // Dispose managed resources.
                Dispose(true);

                // Call GC.SuppressFinalize to take this object off the finalization queue
                // and prevent finalization code for this object from executing a second time.
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Increments the reference count.
            /// </summary>
            internal void ReferenceCountIncrement()
            {
                Interlocked.Increment(ref referenceCount);
            }

            /// <summary>
            /// Decrements the reference count.
            /// </summary>
            /// <returns>True if reference count is zero.</returns>
            internal bool ReferenceCountDecrement()
            {
                return Interlocked.Decrement(ref referenceCount) <= 0;
            }

            /// <summary>
            /// Fires an event for a specified data entity.
            /// </summary>
            /// <param name="entity">The data entity.</param>
            protected void OnEvent(T entity)
            {
                if (IsSynchronous)
                {
                    Delegate[] handlers = null;
                    lock (providerEventLock)
                    {
                        historyList.Add(entity);
                        if (providerEvent != null)
                        {
                            handlers = providerEvent.GetInvocationList();
                        }
                    }

                    if (handlers == null)
                    {
                        return;
                    }

                    foreach (Delegate currentHandler in handlers)
                    {
                        var currentSubscriber = currentHandler as Action<T>;
                        currentSubscriber?.Invoke(entity);
                    }
                }
                else
                {
                    lock (providerEventLock)
                    {
                        historyList.Add(entity);
                    }

                    if (active)
                    {
                        reTransmitterQueue.Enqueue(entity);
                        if (active)
                        {
                            reTransmitterThread.AutoResetEvent.Set();
                        }
                    }
                }
            }

            /// <summary>
            /// Performs the connection of this subscription.
            /// </summary>
            protected abstract void OnConnect();

            /// <summary>
            /// Performs the disconnection of this subscription.
            /// </summary>
            protected abstract void OnDisconnect();

            /// <summary>
            /// Implements <see cref="IDisposable"/>.
            /// </summary>
            /// <param name="disposing">If disposing.</param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing && !IsSynchronous)
                {
                    reTransmitterThread.Dispose();
                }
            }
        }

        /// <summary>
        /// A unique identity for a subscription.
        /// </summary>
        protected sealed class Topic
        {
            /// <summary>
            /// The instrument.
            /// </summary>
            internal readonly Instrument Instrument;

            /// <summary>
            /// The time granularity.
            /// </summary>
            internal readonly TimeGranularity TimeGranularity;

            /// <summary>
            /// Initializes a new instance of the <see cref="Topic"/> class.
            /// </summary>
            /// <param name="instrument">The instrument.</param>
            /// <param name="timeGranularity">The time granularity.</param>
            internal Topic(Instrument instrument, TimeGranularity timeGranularity)
            {
                Instrument = instrument;
                TimeGranularity = timeGranularity;
            }
        }

        private class Subscriber : ISubscription<T>
        {
            private readonly GenericProvider genericProvider;
            private readonly SubscriptionProvider<T> parent;
            private readonly object subscriptionActionLock = new object();
            private Action<T> subscriptionAction;

            /// <summary>
            /// Initializes a new instance of the <see cref="Subscriber"/> class.
            /// </summary>
            /// <param name="parent">The parent subscription provider object.</param>
            /// <param name="genericProvider">The GenericProvider.</param>
            /// <param name="action">A caller-specified action.</param>
            public Subscriber(SubscriptionProvider<T> parent, GenericProvider genericProvider, Action<T> action)
            {
                this.parent = parent;
                this.genericProvider = genericProvider;
                lock (parent.subscriptionsLock)
                {
                    parent.subscriptions.Add(this);
                }

                subscriptionAction = action;
            }

            /// <inheritdoc />
            public event Action<T> SubscriptionAction
            {
                add
                {
                    lock (subscriptionActionLock)
                    {
                        subscriptionAction += value;
                    }
                }

                remove
                {
                    lock (subscriptionActionLock)
                    {
                        // ReSharper disable once DelegateSubtraction
                        subscriptionAction -= value;
                    }
                }
            }

            /// <inheritdoc />
            public string Provider => parent.Provider;

            /// <inheritdoc />
            public Instrument Instrument => genericProvider.Topic.Instrument;

            /// <inheritdoc />
            public TimeGranularity TimeGranularity => genericProvider.Topic.TimeGranularity;

            /// <inheritdoc />
            public bool IsConnected { get; private set; }

            /// <inheritdoc />
            public void Connect()
            {
                if (IsConnected)
                {
                    return;
                }

                IsConnected = true;
                genericProvider.Event += Retransmit;
                genericProvider.ReferenceCountIncrement();
                genericProvider.Connect();
            }

            /// <inheritdoc />
            public void Disconnect()
            {
                bool cancel = false;
                if (IsConnected)
                {
                    IsConnected = false;
                    cancel = true;
                }

                if (cancel)
                {
                    genericProvider.Event -= Retransmit;
                    lock (parent.subscriptionsLock)
                    {
                        parent.subscriptions.Remove(this);
                    }

                    if (genericProvider.ReferenceCountDecrement())
                    {
                        lock (parent.providersLock)
                        {
                            parent.providers.Remove(genericProvider.Topic);
                        }

                        genericProvider.Disconnect();
                    }
                }
            }

            private void Retransmit(T entity)
            {
                Delegate[] handlers = null;
                lock (subscriptionActionLock)
                {
                    if (subscriptionAction != null)
                    {
                        handlers = subscriptionAction.GetInvocationList();
                    }
                }

                if (handlers == null)
                {
                    return;
                }

                foreach (Delegate currentHandler in handlers)
                {
                    var currentSubscriber = currentHandler as Action<T>;
                    currentSubscriber?.Invoke(entity);
                }
            }
        }
    }
}
