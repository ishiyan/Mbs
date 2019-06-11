using System;
using System.Collections.Generic;
using System.Linq;
using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data
{
    /// <summary>
    /// Acts as a proxy to a collection of subscription providers.
    /// </summary>
    public class DataEmitter : IDataEmitter
    {
        /// <summary>
        /// A unique identity for a subscription.
        /// </summary>
        private sealed class Topic
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

        /// <summary>
        /// An entity provider.
        /// </summary>
        private sealed class EntityProvider<T>
            where T : TemporalEntity
        {
            /// <summary>
            /// A container for the last entity.
            /// </summary>
            private sealed class LastContainer
            {
                /// <summary>
                /// The last entity.
                /// </summary>
                internal T Entity;
            }

            private readonly object providerLock = new object();
            private readonly List<ISubscriptionProvider<T>> providerList = new List<ISubscriptionProvider<T>>();

            private readonly object topicLock = new object();
            private readonly Dictionary<Topic, ISubscriptionProvider<T>> topicDictionary = new Dictionary<Topic, ISubscriptionProvider<T>>();

            private readonly object subscriptionListLock = new object();
            private readonly List<ISubscription<T>> subscriptionList = new List<ISubscription<T>>();

            private readonly object instrumentLock = new object();
            private readonly Dictionary<Instrument, LastContainer> instrumentDictionary = new Dictionary<Instrument, LastContainer>();

            /// <summary>
            /// The last entity for an instrument.
            /// </summary>
            /// <param name="instrument">The instrument.</param>
            /// <returns>The last entity or a null if not monitored.</returns>
            internal T Last(Instrument instrument)
            {
                lock (instrumentLock)
                {
                    instrumentDictionary.TryGetValue(instrument, out LastContainer last);
                    return last?.Entity;
                }
            }

            /// <summary>
            /// Monitor the last entity for a topic.
            /// Connect the subscription (if it is not activated yet) to actually start monitoring.
            /// </summary>
            /// <param name="topic">The topic to monitor.</param>
            /// <param name="vendor">Specifies the vendor if not null or empty.</param>
            /// <returns>The subscription or null if failed to subscribe.</returns>
            internal ISubscription<T> Monitor(Topic topic, string vendor)
            {
                LastContainer last;
                lock (instrumentLock)
                {
                    if (!instrumentDictionary.TryGetValue(topic.Instrument, out last))
                    {
                        last = new LastContainer();
                        instrumentDictionary.Add(topic.Instrument, last);
                    }
                }

                return null == last ? null : Subscribe(topic, vendor, t => last.Entity = t);
            }

            /// <summary>
            /// Monitor the last entity for an instrument at the lowest available time granularity.
            /// Connect the subscription (if it is not activated yet) to actually start monitoring.
            /// </summary>
            /// <param name="instrument">The instrument to monitor.</param>
            /// <param name="vendor">Specifies the vendor if not null or empty.</param>
            /// <returns>The subscription or null if failed to subscribe.</returns>
            internal ISubscription<T> Monitor(Instrument instrument, string vendor)
            {
                LastContainer last;
                lock (instrumentLock)
                {
                    if (!instrumentDictionary.TryGetValue(instrument, out last))
                    {
                        last = new LastContainer();
                        instrumentDictionary.Add(instrument, last);
                    }
                }

                return null == last ? null : Subscribe(instrument, vendor, t => last.Entity = t);
            }

            /// <summary>
            /// Adds a subscription provider of the specified type.
            /// </summary>
            /// <param name="provider">The subscription provider of the specified type.</param>
            internal void Add(ISubscriptionProvider<T> provider)
            {
                lock (providerLock)
                {
                    if (!providerList.Contains(provider))
                        providerList.Add(provider);
                }
            }

            /// <summary>
            /// Cancels all current active subscriptions.
            /// </summary>
            internal void Cancel()
            {
                lock (topicLock)
                {
                    topicDictionary.Clear();
                    lock (providerLock)
                    {
                        providerList.Clear();
                    }

                    lock (subscriptionListLock)
                    {
                        foreach (var subscription in subscriptionList)
                        {
                            if (subscription.IsConnected)
                                subscription.Disconnect();
                        }

                        subscriptionList.Clear();
                    }
                }
            }

            /// <summary>
            /// Activates all current not active subscriptions.
            /// </summary>
            internal void Activate()
            {
                lock (subscriptionListLock)
                {
                    foreach (var subscription in subscriptionList)
                    {
                        if (!subscription.IsConnected)
                            subscription.Connect();
                    }
                }
            }

            /// <summary>
            /// Subscribes to the specified topic.
            /// Connect the subscription (if it is not activated yet) to actually start receiving data.
            /// </summary>
            /// <param name="topic">The topic to subscribe to.</param>
            /// <param name="provider">Specifies the data provider if not null or empty.</param>
            /// <param name="action">The action.</param>
            /// <returns>The subscription or null if failed to subscribe.</returns>
            internal ISubscription<T> Subscribe(Topic topic, string provider, Action<T> action)
            {
                ISubscription<T> subscription = null;
                lock (topicLock)
                {
                    topicDictionary.TryGetValue(topic, out ISubscriptionProvider<T> subscriptionProvider);
                    if (string.IsNullOrEmpty(provider))
                    {
                        if (null != subscriptionProvider)
                        {
                            subscription = subscriptionProvider.Subscribe(topic.Instrument, topic.TimeGranularity, action);
                        }
                        else
                        {
                            lock (providerLock)
                            {
                                foreach (var p in providerList)
                                {
                                    try
                                    {
                                        subscription = p.Subscribe(topic.Instrument, topic.TimeGranularity, action);
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Log.Error(ex.Message);
                                        continue;
                                    }

                                    if (null == subscription)
                                        continue;
                                    topicDictionary.Add(topic, p);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        lock (providerLock)
                        {
                            foreach (var p in providerList)
                            {
                                if (provider.Equals(p.Provider, StringComparison.Ordinal))
                                {
                                    try
                                    {
                                        subscription = p.Subscribe(topic.Instrument, topic.TimeGranularity, action);
                                    }
                                    catch (ArgumentException)
                                    {
                                        continue;
                                    }

                                    if (null == subscription)
                                        continue;
                                    if (null == subscriptionProvider)
                                        topicDictionary.Add(topic, p);
                                    break;
                                }
                            }
                        }
                    }
                }

                if (null != subscription)
                {
                    lock (subscriptionListLock)
                    {
                        subscriptionList.Add(subscription);
                    }
                }

                return subscription;
            }

            /// <summary>
            /// Subscribes to the specified instrument at the lowest available time granularity.
            /// Connect the subscription (if it is not activated yet) to actually start receiving data.
            /// </summary>
            /// <param name="instrument">The instrument to subscribe to.</param>
            /// <param name="provider">Specifies the date provider name if not null or empty.</param>
            /// <param name="action">The action.</param>
            /// <returns>The subscription or null if failed to subscribe.</returns>
            internal ISubscription<T> Subscribe(Instrument instrument, string provider, Action<T> action)
            {
                ISubscription<T> subscription = null;
                lock (topicLock)
                {
                    Topic bestTopic = null;
                    ISubscriptionProvider<T> bestSubscriptionProvider = null;
                    IEnumerable<Topic> enumerable = topicDictionary.Keys.Where(t => instrument == t.Instrument);
                    if (null == provider)
                    {
                        foreach (var t in enumerable)
                        {
                            if (null == bestTopic)
                                bestTopic = t;
                            else if (bestTopic.TimeGranularity > t.TimeGranularity)
                                bestTopic = t;
                        }

                        if (null != bestTopic)
                            bestSubscriptionProvider = topicDictionary[bestTopic];
                    }
                    else
                    {
                        foreach (var t in enumerable)
                        {
                            ISubscriptionProvider<T> p = topicDictionary[t];
                            if (provider.Equals(p.Provider, StringComparison.Ordinal))
                            {
                                if (null == bestTopic || bestTopic.TimeGranularity > t.TimeGranularity)
                                {
                                    bestTopic = t;
                                    bestSubscriptionProvider = p;
                                }
                            }
                        }
                    }

                    if (null != bestSubscriptionProvider)
                    {
                        lock (providerLock)
                        {
                            subscription = bestSubscriptionProvider.Subscribe(bestTopic.Instrument, bestTopic.TimeGranularity, action);
                        }
                    }
                }

                if (null != subscription)
                {
                    lock (subscriptionListLock)
                    {
                        subscriptionList.Add(subscription);
                    }
                }

                return subscription;
            }
        }

        private const string TemporalEntityType = "Unknown TemporalEntity type.";

        private readonly EntityProvider<Ohlcv> ohlcvEntityProvider = new EntityProvider<Ohlcv>();
        private readonly EntityProvider<Trade> tradeEntityProvider = new EntityProvider<Trade>();
        private readonly EntityProvider<Quote> quoteEntityProvider = new EntityProvider<Quote>();
        private readonly EntityProvider<Scalar> scalarEntityProvider = new EntityProvider<Scalar>();
        private readonly object defaultMonitorGranularityLock = new object();

        private TimeGranularity ohlcvDefaultMonitorGranularity = TimeGranularity.Day1;
        private TimeGranularity tradeDefaultMonitorGranularity = TimeGranularity.Aperiodic;
        private TimeGranularity quoteDefaultMonitorGranularity = TimeGranularity.Aperiodic;
        private TimeGranularity scalarDefaultMonitorGranularity = TimeGranularity.Day1;

        /// <summary>
        /// The default time granularity of the monitoring.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <returns>The time granularity.</returns>
        public TimeGranularity DefaultMonitorGranularity<T>()
            where T : TemporalEntity
        {
            Type type = typeof(T);
            lock (defaultMonitorGranularityLock)
            {
                if (type == Types.OhlcvType)
                    return ohlcvDefaultMonitorGranularity;
                if (type == Types.ScalarType)
                    return scalarDefaultMonitorGranularity;
                if (type == Types.TradeType)
                    return tradeDefaultMonitorGranularity;
                if (type == Types.QuoteType)
                    return quoteDefaultMonitorGranularity;
                throw new ArgumentException(TemporalEntityType);
            }
        }

        /// <summary>
        /// Sets the default time granularity of the  monitoring.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="timeGranularity">The time granularity.</param>
        public void DefaultMonitorGranularity<T>(TimeGranularity timeGranularity)
            where T : TemporalEntity
        {
            Type type = typeof(T);
            lock (defaultMonitorGranularityLock)
            {
                if (type == Types.OhlcvType)
                    ohlcvDefaultMonitorGranularity = timeGranularity;
                else if (type == Types.ScalarType)
                    scalarDefaultMonitorGranularity = timeGranularity;
                else if (type == Types.TradeType)
                    tradeDefaultMonitorGranularity = timeGranularity;
                else if (type == Types.QuoteType)
                    quoteDefaultMonitorGranularity = timeGranularity;
                else
                    throw new ArgumentException(TemporalEntityType);
            }
        }

        /// <summary>
        /// Monitor the last entity for an instrument.
        /// Connect the subscription (if it is not activated yet) to actually start monitoring.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="instrument">The instrument.</param>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <param name="vendor">Specifies the vendor if not null or empty.</param>
        /// <returns>The subscription or null if failed to subscribe.</returns>
        public ISubscription<T> Monitor<T>(Instrument instrument, TimeGranularity timeGranularity, string vendor)
            where T : TemporalEntity
        {
            Type type = typeof(T);
            var topic = new Topic(instrument, timeGranularity);
            if (type == Types.OhlcvType)
                return ohlcvEntityProvider.Monitor(topic, vendor) as ISubscription<T>;
            if (type == Types.ScalarType)
                return scalarEntityProvider.Monitor(topic, vendor) as ISubscription<T>;
            if (type == Types.TradeType)
                return tradeEntityProvider.Monitor(topic, vendor) as ISubscription<T>;
            if (type == Types.QuoteType)
                return quoteEntityProvider.Monitor(topic, vendor) as ISubscription<T>;
            throw new ArgumentException(TemporalEntityType);
        }

        /// <inheritdoc />
        public ISubscription<T> Monitor<T>(Instrument instrument, TimeGranularity timeGranularity)
            where T : TemporalEntity
        {
            return Monitor<T>(instrument, timeGranularity, null);
        }

        /// <inheritdoc />
        public ISubscription<T> Monitor<T>(Instrument instrument)
            where T : TemporalEntity
        {
            Type type = typeof(T);

            // First, try to subscribe to the lowest available time granularity.
            ISubscription<T> subscription = null;
            if (type == Types.OhlcvType)
                subscription = ohlcvEntityProvider.Monitor(instrument, null) as ISubscription<T>;
            else if (type == Types.ScalarType)
                subscription = scalarEntityProvider.Monitor(instrument, null) as ISubscription<T>;
            else if (type == Types.TradeType)
                subscription = tradeEntityProvider.Monitor(instrument, null) as ISubscription<T>;
            else if (type == Types.QuoteType)
                subscription = quoteEntityProvider.Monitor(instrument, null) as ISubscription<T>;
            if (null != subscription)
                return subscription;

            lock (defaultMonitorGranularityLock)
            {
                if (type == Types.OhlcvType)
                    return ohlcvEntityProvider.Monitor(new Topic(instrument, ohlcvDefaultMonitorGranularity), null) as ISubscription<T>;
                if (type == Types.ScalarType)
                    return scalarEntityProvider.Monitor(new Topic(instrument, scalarDefaultMonitorGranularity), null) as ISubscription<T>;
                if (type == Types.TradeType)
                    return tradeEntityProvider.Monitor(new Topic(instrument, tradeDefaultMonitorGranularity), null) as ISubscription<T>;
                if (type == Types.QuoteType)
                    return quoteEntityProvider.Monitor(new Topic(instrument, quoteDefaultMonitorGranularity), null) as ISubscription<T>;
                throw new ArgumentException(TemporalEntityType);
            }
        }

        /// <summary>
        /// Adds a subscription provider of the specified type.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="subscriptionProvider">The subscription provider of the specified type.</param>
        public void Add<T>(ISubscriptionProvider<T> subscriptionProvider)
            where T : TemporalEntity
        {
            if (null == subscriptionProvider)
                return;
            Type type = typeof(T);
            if (type == Types.OhlcvType)
                ohlcvEntityProvider.Add((ISubscriptionProvider<Ohlcv>)subscriptionProvider);
            else if (type == Types.ScalarType)
                scalarEntityProvider.Add((ISubscriptionProvider<Scalar>)subscriptionProvider);
            else if (type == Types.TradeType)
                tradeEntityProvider.Add((ISubscriptionProvider<Trade>)subscriptionProvider);
            else if (type == Types.QuoteType)
                quoteEntityProvider.Add((ISubscriptionProvider<Quote>)subscriptionProvider);
            else
                throw new ArgumentException(TemporalEntityType);
        }

        /// <inheritdoc />
        public double LastBuyPrice(Instrument instrument)
        {
            Quote quote = quoteEntityProvider.Last(instrument);
            if (quote != null)
                return quote.BidPrice;
            Trade trade = tradeEntityProvider.Last(instrument);
            if (trade != null)
                return trade.Price;
            Ohlcv ohlcv = ohlcvEntityProvider.Last(instrument);
            return ohlcv?.Close ?? double.NaN;
        }

        /// <inheritdoc />
        public double LastSellPrice(Instrument instrument)
        {
            Quote quote = quoteEntityProvider.Last(instrument);
            if (quote != null)
                return quote.AskPrice;
            Trade trade = tradeEntityProvider.Last(instrument);
            if (trade != null)
                return trade.Price;
            Ohlcv ohlcv = ohlcvEntityProvider.Last(instrument);
            return ohlcv?.Close ?? double.NaN;
        }

        /// <inheritdoc />
        public T Last<T>(Instrument instrument)
            where T : TemporalEntity
        {
            Type type = typeof(T);
            if (type == Types.OhlcvType)
                return ohlcvEntityProvider.Last(instrument) as T;
            if (type == Types.ScalarType)
                return scalarEntityProvider.Last(instrument) as T;
            if (type == Types.TradeType)
                return tradeEntityProvider.Last(instrument) as T;
            if (type == Types.QuoteType)
                return quoteEntityProvider.Last(instrument) as T;
            throw new ArgumentException(TemporalEntityType);
        }

        /// <inheritdoc />
        public ISubscription<T> Subscribe<T>(Instrument instrument, TimeGranularity timeGranularity, Action<T> action)
            where T : TemporalEntity
        {
            var topic = new Topic(instrument, timeGranularity);
            Type type = typeof(T);
            if (type == Types.OhlcvType)
                return (ISubscription<T>)ohlcvEntityProvider.Subscribe(topic, null, action as Action<Ohlcv>);
            if (type == Types.ScalarType)
                return (ISubscription<T>)scalarEntityProvider.Subscribe(topic, null, action as Action<Scalar>);
            if (type == Types.TradeType)
                return (ISubscription<T>)tradeEntityProvider.Subscribe(topic, null, action as Action<Trade>);
            if (type == Types.QuoteType)
                return (ISubscription<T>)quoteEntityProvider.Subscribe(topic, null, action as Action<Quote>);
            throw new ArgumentException(TemporalEntityType);
        }

        /// <summary>
        /// Cancels all current active subscriptions.
        /// </summary>
        public void Cancel()
        {
            ohlcvEntityProvider.Cancel();
            scalarEntityProvider.Cancel();
            tradeEntityProvider.Cancel();
            quoteEntityProvider.Cancel();
        }

        /// <summary>
        /// Activates all current not active subscriptions.
        /// </summary>
        public void Activate()
        {
            ohlcvEntityProvider.Activate();
            scalarEntityProvider.Activate();
            tradeEntityProvider.Activate();
            quoteEntityProvider.Activate();
        }
    }
}
