using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;
using Mbs.Utilities;
#pragma warning disable S3172 // Delegates should not be subtracted

namespace Mbs.Trading.Data.Timelines
{
    /// <summary>
    /// Aligns a series of different temporal entities, and emits them as a single timeline-based stream of mixed entities.
    /// <para>Use this interface as a data back-testing facility, simulating live market via historic data.</para>
    /// </summary>
    public sealed class Timeline
    {
        private readonly object scalarSubscriptionProvidersDictionaryLock = new object();
        private readonly object tradeSubscriptionProvidersDictionaryLock = new object();
        private readonly object quoteSubscriptionProvidersDictionaryLock = new object();
        private readonly object ohlcvSubscriptionProvidersDictionaryLock = new object();
        private readonly object stateLock = new object();
        private readonly object startedLock = new object();
        private readonly object stoppedLock = new object();
        private readonly object timeChangedLock = new object();
        private readonly object mergingEnumerableLock = new object();
        private readonly MergingEnumerable<TemporalEntity> mergingEnumerable = new MergingEnumerable<TemporalEntity>();

        private readonly Dictionary<IHistoricalData<Scalar>, TimelineSubscriptionProvider<Scalar>>
            scalarSubscriptionProvidersDictionary = new Dictionary<IHistoricalData<Scalar>, TimelineSubscriptionProvider<Scalar>>();

        private readonly Dictionary<IHistoricalData<Trade>, TimelineSubscriptionProvider<Trade>>
            tradeSubscriptionProvidersDictionary = new Dictionary<IHistoricalData<Trade>, TimelineSubscriptionProvider<Trade>>();

        private readonly Dictionary<IHistoricalData<Quote>, TimelineSubscriptionProvider<Quote>>
            quoteSubscriptionProvidersDictionary = new Dictionary<IHistoricalData<Quote>, TimelineSubscriptionProvider<Quote>>();

        private readonly Dictionary<IHistoricalData<Ohlcv>, TimelineSubscriptionProvider<Ohlcv>>
            ohlcvSubscriptionProvidersDictionary = new Dictionary<IHistoricalData<Ohlcv>, TimelineSubscriptionProvider<Ohlcv>>();

        private int interactiveDelayMilliseconds = 100;
        private TimelineState state = TimelineState.Stop;
        private Action started;
        private Action stopped;
        private Action<DateTime> timeChanged;
        private IEnumerator<MergingEnumerable<TemporalEntity>.Pair> enumerator;
        private DateTime time = new DateTime(0L);
        private DateTime beginTime = new DateTime(0L);

        /// <summary>
        /// Initializes a new instance of the <see cref="Timeline"/> class.
        /// </summary>
        public Timeline()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timeline"/> class.
        /// </summary>
        /// <param name="tradeHistoricalData">A trade series enumerable provider, may be null.</param>
        /// <param name="quoteHistoricalData">A quote series enumerable provider, may be null.</param>
        /// <param name="ohlcvHistoricalData">A ohlcv series enumerable provider, may be null.</param>
        /// <param name="scalarHistoricalData">A scalar series enumerable provider, may be null.</param>
        public Timeline(
            IHistoricalData<Trade> tradeHistoricalData,
            IHistoricalData<Quote> quoteHistoricalData,
            IHistoricalData<Ohlcv> ohlcvHistoricalData,
            IHistoricalData<Scalar> scalarHistoricalData)
        {
            Add(tradeHistoricalData);
            Add(quoteHistoricalData);
            Add(ohlcvHistoricalData);
            Add(scalarHistoricalData);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timeline"/> class.
        /// </summary>
        /// <param name="tradeEnumerableProviderCollection">A trade series enumerable provider collection, may be null.</param>
        /// <param name="quoteEnumerableProviderCollection">A quote series enumerable provider collection, may be null.</param>
        /// <param name="ohlcvEnumerableProviderCollection">A ohlcv series enumerable provider collection, may be null.</param>
        /// <param name="scalarEnumerableProviderCollection">A scalar series enumerable provider collection, may be null.</param>
        public Timeline(
            IEnumerable<IHistoricalData<Trade>> tradeEnumerableProviderCollection,
            IEnumerable<IHistoricalData<Quote>> quoteEnumerableProviderCollection,
            IEnumerable<IHistoricalData<Ohlcv>> ohlcvEnumerableProviderCollection,
            IEnumerable<IHistoricalData<Scalar>> scalarEnumerableProviderCollection)
        {
            if (tradeEnumerableProviderCollection != null)
            {
                foreach (var v in tradeEnumerableProviderCollection)
                {
                    Add(v);
                }
            }

            if (quoteEnumerableProviderCollection != null)
            {
                foreach (var v in quoteEnumerableProviderCollection)
                {
                    Add(v);
                }
            }

            if (ohlcvEnumerableProviderCollection != null)
            {
                foreach (var v in ohlcvEnumerableProviderCollection)
                {
                    Add(v);
                }
            }

            if (scalarEnumerableProviderCollection != null)
            {
                foreach (var v in scalarEnumerableProviderCollection)
                {
                    Add(v);
                }
            }
        }

        /// <summary>
        /// Notifies when the timeline is started.
        /// </summary>
        public event Action Started
        {
            add
            {
                lock (startedLock)
                {
                    started += value;
                }
            }

            remove
            {
                lock (startedLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    started -= value;
                }
            }
        }

        /// <summary>
        /// Notifies when the timeline is stopped.
        /// </summary>
        public event Action Stopped
        {
            add
            {
                lock (stoppedLock)
                {
                    stopped += value;
                }
            }

            remove
            {
                lock (stoppedLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    stopped -= value;
                }
            }
        }

        /// <summary>
        /// Notifies the time change.
        /// </summary>
        public event Action<DateTime> TimeChanged
        {
            add
            {
                lock (timeChangedLock)
                {
                    timeChanged += value;
                }
            }

            remove
            {
                lock (timeChangedLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    timeChanged -= value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of milliseconds to sleep between sequential events in interactive mode.
        /// </summary>
        public int InteractiveDelayMilliseconds
        {
            get => interactiveDelayMilliseconds;
            set => interactiveDelayMilliseconds = value < 0 ? 0 : value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the timeline runs asynchronous.
        /// </summary>
        public bool IsAsynchronous { get; set; } = true;

        /// <summary>
        /// Gets the current timeline state.
        /// </summary>
        public TimelineState State
        {
            get
            {
                lock (stateLock)
                {
                    return state;
                }
            }

            private set
            {
                lock (stateLock)
                {
                    state = value;
                }
            }
        }

        /// <summary>
        /// Gets the current timeline time.
        /// </summary>
        public DateTime Time
        {
            get => time;

            private set
            {
                time = value;
                OnTimeChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets begin of the historical time interval.
        /// </summary>
        public DateTime BeginTime
        {
            get => beginTime;

            set
            {
                beginTime = value;
                time = value;
            }
        }

        /// <summary>
        /// Gets or sets the end of the historical time interval.
        /// </summary>
        public DateTime EndTime { get; set; } = new DateTime(0L);

        /// <summary>
        /// Returns a read-only collection of available subscription providers of specified type.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <returns>A read-only collection of subscription providers.</returns>
        public ReadOnlyCollection<ISubscriptionProvider<T>> SubscriptionProviders<T>()
            where T : TemporalEntity
        {
            Type type = typeof(T);
            if (type == Types.OhlcvType)
            {
                lock (ohlcvSubscriptionProvidersDictionaryLock)
                {
                    return ohlcvSubscriptionProvidersDictionary.Values.Cast<ISubscriptionProvider<T>>().ToList().AsReadOnly();
                }
            }

            if (type == Types.ScalarType)
            {
                lock (scalarSubscriptionProvidersDictionaryLock)
                {
                    return scalarSubscriptionProvidersDictionary.Values.Cast<ISubscriptionProvider<T>>().ToList().AsReadOnly();
                }
            }

            if (type == Types.TradeType)
            {
                lock (tradeSubscriptionProvidersDictionaryLock)
                {
                    return tradeSubscriptionProvidersDictionary.Values.Cast<ISubscriptionProvider<T>>().ToList().AsReadOnly();
                }
            }

            if (type == Types.QuoteType)
            {
                lock (quoteSubscriptionProvidersDictionaryLock)
                {
                    return quoteSubscriptionProvidersDictionary.Values.Cast<ISubscriptionProvider<T>>().ToList().AsReadOnly();
                }
            }

            return new ReadOnlyCollection<ISubscriptionProvider<T>>(new List<ISubscriptionProvider<T>>());
        }

        /// <summary>
        /// Adds a new series enumerable provider of the specified type.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="historicalData">The enumerable provider of the specified type.</param>
        public void Add<T>(IHistoricalData<T> historicalData)
            where T : TemporalEntity
        {
            if (historicalData == null)
            {
                return;
            }

            Type type = typeof(T);
            if (type == Types.OhlcvType)
            {
                var sep = (IHistoricalData<Ohlcv>)historicalData;
                lock (ohlcvSubscriptionProvidersDictionaryLock)
                {
                    if (!ohlcvSubscriptionProvidersDictionary.TryGetValue(sep, out _))
                    {
                        ohlcvSubscriptionProvidersDictionary.Add(sep, new TimelineSubscriptionProvider<Ohlcv>(this, sep));
                    }
                }
            }
            else if (type == Types.ScalarType)
            {
                var sep = (IHistoricalData<Scalar>)historicalData;
                lock (scalarSubscriptionProvidersDictionaryLock)
                {
                    if (!scalarSubscriptionProvidersDictionary.TryGetValue(sep, out _))
                    {
                        scalarSubscriptionProvidersDictionary.Add(sep, new TimelineSubscriptionProvider<Scalar>(this, sep));
                    }
                }
            }
            else if (type == Types.TradeType)
            {
                var sep = (IHistoricalData<Trade>)historicalData;
                lock (tradeSubscriptionProvidersDictionaryLock)
                {
                    if (!tradeSubscriptionProvidersDictionary.TryGetValue(sep, out _))
                    {
                        tradeSubscriptionProvidersDictionary.Add(sep, new TimelineSubscriptionProvider<Trade>(this, sep));
                    }
                }
            }
            else if (type == Types.QuoteType)
            {
                var sep = (IHistoricalData<Quote>)historicalData;
                lock (quoteSubscriptionProvidersDictionaryLock)
                {
                    if (!quoteSubscriptionProvidersDictionary.TryGetValue(sep, out _))
                    {
                        quoteSubscriptionProvidersDictionary.Add(sep, new TimelineSubscriptionProvider<Quote>(this, sep));
                    }
                }
            }
        }

        /// <summary>
        /// Removes a series enumerable provider of the specified type.
        /// </summary>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <param name="historicalData">The enumerable provider of the specified type.</param>
        public void Remove<T>(IHistoricalData<T> historicalData)
            where T : TemporalEntity
        {
            if (historicalData == null)
            {
                return;
            }

            Type type = typeof(T);
            if (type == Types.OhlcvType)
            {
                var sep = (IHistoricalData<Ohlcv>)historicalData;
                lock (ohlcvSubscriptionProvidersDictionaryLock)
                {
                    if (ohlcvSubscriptionProvidersDictionary.TryGetValue(sep, out _))
                    {
                        ohlcvSubscriptionProvidersDictionary.Remove(sep);
                    }
                }
            }
            else if (type == Types.ScalarType)
            {
                var sep = (IHistoricalData<Scalar>)historicalData;
                lock (scalarSubscriptionProvidersDictionaryLock)
                {
                    if (scalarSubscriptionProvidersDictionary.TryGetValue(sep, out _))
                    {
                        scalarSubscriptionProvidersDictionary.Remove(sep);
                    }
                }
            }
            else if (type == Types.TradeType)
            {
                var sep = (IHistoricalData<Trade>)historicalData;
                lock (tradeSubscriptionProvidersDictionaryLock)
                {
                    if (tradeSubscriptionProvidersDictionary.TryGetValue(sep, out _))
                    {
                        tradeSubscriptionProvidersDictionary.Remove(sep);
                    }
                }
            }
            else if (type == Types.QuoteType)
            {
                var sep = (IHistoricalData<Quote>)historicalData;
                lock (quoteSubscriptionProvidersDictionaryLock)
                {
                    if (quoteSubscriptionProvidersDictionary.TryGetValue(sep, out _))
                    {
                        quoteSubscriptionProvidersDictionary.Remove(sep);
                    }
                }
            }
        }

        /// <summary>
        /// Cleans all subscriptions and all actions in the timeline.
        /// </summary>
        public void Clean()
        {
            CleanStarted();
            CleanStopped();
            lock (ohlcvSubscriptionProvidersDictionaryLock)
            {
                foreach (var v in ohlcvSubscriptionProvidersDictionary)
                {
                    v.Value.Disconnect();
                }
            }

            lock (tradeSubscriptionProvidersDictionaryLock)
            {
                foreach (var v in tradeSubscriptionProvidersDictionary)
                {
                    v.Value.Disconnect();
                }
            }

            lock (scalarSubscriptionProvidersDictionaryLock)
            {
                foreach (var v in scalarSubscriptionProvidersDictionary)
                {
                    v.Value.Disconnect();
                }
            }

            lock (quoteSubscriptionProvidersDictionaryLock)
            {
                foreach (var v in quoteSubscriptionProvidersDictionary)
                {
                    v.Value.Disconnect();
                }
            }
        }

        /// <summary>
        /// Replays the timeline in non-interactive mode.
        /// The replaying cannot be paused, stepped, continued and prematurely stopped.
        /// Starts a new thread to replay if <c>Asynchronous</c> property is set to <c>true</c>.
        /// Otherwise, replays the timeline in the current thread.
        /// </summary>
        public void Replay()
        {
            ExecuteAction(() =>
            {
                lock (mergingEnumerableLock)
                {
                    State = TimelineState.Run;
                    NotifyStarted();
                    foreach (var entity in mergingEnumerable)
                    {
                        Log.Debug($"timeline.Replay: {entity.Value}");
                        Time = entity.Value.Time;
                        entity.Action(entity.Value);
                    }

                    NotifyStopped();
                    State = TimelineState.Stop;
                }
            });
        }

        /// <summary>
        /// Starts the replaying of the the timeline in interactive mode.
        /// The replaying can be paused, stepped, continued and prematurely stopped.
        /// Starts a new thread to replay if <see cref="IsAsynchronous"/> property is set to <c>true</c>.
        /// Otherwise, replays the timeline in the current thread.
        /// </summary>
        public void Start()
        {
            ExecuteAction(() =>
            {
                IEnumerator<MergingEnumerable<TemporalEntity>.Pair> e;
                lock (mergingEnumerableLock)
                {
                    TimelineState currentState = State;
                    if (currentState != TimelineState.Stop)
                    {
                        throw new ApplicationException($"Invalid timeline state <{currentState}>, Start() expects <Stop>.");
                    }

                    e = mergingEnumerable.GetEnumerator();
                    enumerator = e;
                    State = TimelineState.Run;
                    NotifyStarted();
                }

                while (e.MoveNext())
                {
                    lock (mergingEnumerableLock)
                    {
                        var entity = e.Current;

                        Log.Debug($"timeline.Start: {entity.Value}");
                        Time = entity.Value.Time;
                        entity.Action(entity.Value);
                    }

                    if (interactiveDelayMilliseconds > 0)
                    {
                        Thread.Sleep(interactiveDelayMilliseconds);
                    }

                    switch (State)
                    {
                        case TimelineState.Stop:
                            return;
                        case TimelineState.Step:
                        case TimelineState.Pause:
                            State = TimelineState.Pause;
                            return;
                    }
                }

                lock (mergingEnumerableLock)
                {
                    enumerator = null;
                    NotifyStopped();
                    State = TimelineState.Stop;
                }
            });
        }

        /// <summary>
        /// Stops the interactive replaying of the timeline.
        /// </summary>
        public void Stop()
        {
            ExecuteAction(() =>
            {
                lock (mergingEnumerableLock)
                {
                    TimelineState currentState = State;
                    if (currentState != TimelineState.Pause && currentState != TimelineState.Run)
                    {
                        throw new ApplicationException($"Invalid timeline state <{currentState}>, Stop() expects <Run> or <Pause>.");
                    }

                    enumerator = null;
                    NotifyStopped();
                    State = TimelineState.Stop;
                }
            });
        }

        /// <summary>
        /// Pauses the interactive replaying of the the timeline.
        /// </summary>
        public void Pause()
        {
            lock (mergingEnumerableLock)
            {
                TimelineState currentState = State;
                if (currentState != TimelineState.Run)
                {
                    throw new ApplicationException($"Invalid timeline state <{currentState}>, Pause() expects <Run>.");
                }

                State = TimelineState.Pause;
            }
        }

        /// <summary>
        /// Advances the interactive timeline one step.
        /// </summary>
        public void Step()
        {
            ExecuteAction(() =>
            {
                lock (mergingEnumerableLock)
                {
                    TimelineState currentState = State;
                    if (currentState != TimelineState.Pause && currentState != TimelineState.Stop)
                    {
                        throw new ApplicationException($"Invalid timeline state <{currentState}>, Step() expects <Stop> or <Pause>.");
                    }

                    State = TimelineState.Step;
                    if (enumerator == null)
                    {
                        enumerator = mergingEnumerable.GetEnumerator();
                        NotifyStarted();
                    }

                    if (enumerator.MoveNext())
                    {
                        var entity = enumerator.Current;

                        Log.Debug($"timeline.Step: {entity.Value}");
                        Time = entity.Value.Time;
                        entity.Action(entity.Value);
                        State = TimelineState.Pause;
                    }
                    else
                    {
                        enumerator = null;
                        NotifyStopped();
                        State = TimelineState.Stop;
                    }
                }
            });
        }

        /// <summary>
        /// Continues the interactive replaying of the the timeline.
        /// </summary>
        public void Continue()
        {
            ExecuteAction(() =>
            {
                IEnumerator<MergingEnumerable<TemporalEntity>.Pair> e;
                lock (mergingEnumerableLock)
                {
                    TimelineState currentState = State;
                    if (currentState != TimelineState.Pause)
                    {
                        throw new ApplicationException($"Invalid timeline state <{currentState}>, Continue() expects <Pause>.");
                    }

                    e = enumerator;
                    if (e == null)
                    {
                        return;
                    }

                    State = TimelineState.Run;
                }

                while (e.MoveNext())
                {
                    lock (mergingEnumerableLock)
                    {
                        var entity = e.Current;

                        Log.Debug($"timeline.Continue: {entity.Value}");
                        Time = entity.Value.Time;
                        entity.Action(entity.Value);
                    }

                    if (interactiveDelayMilliseconds > 0)
                    {
                        Thread.Sleep(interactiveDelayMilliseconds);
                    }

                    switch (State)
                    {
                        case TimelineState.Stop:
                            return;
                        case TimelineState.Step:
                        case TimelineState.Pause:
                            State = TimelineState.Pause;
                            return;
                    }
                }

                lock (mergingEnumerableLock)
                {
                    enumerator = null;
                    NotifyStopped();
                    State = TimelineState.Stop;
                }
            });
        }

        private void CleanStarted()
        {
            lock (startedLock)
            {
                if (started == null)
                {
                    return;
                }

                var handlers = (Delegate[])started.GetInvocationList().Clone();
                foreach (Delegate handler in handlers)
                {
                    // ReSharper disable once DelegateSubtraction
                    started -= (Action)handler;
                }
            }
        }

        private void NotifyStarted()
        {
            lock (startedLock)
            {
                if (started != null)
                {
                    Delegate[] handlers = started.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        ((Action)handler)();
                    }
                }
            }
        }

        private void CleanStopped()
        {
            lock (stoppedLock)
            {
                if (stopped == null)
                {
                    return;
                }

                var handlers = (Delegate[])stopped.GetInvocationList().Clone();
                foreach (Delegate handler in handlers)
                {
                    // ReSharper disable once DelegateSubtraction
                    stopped -= (Action)handler;
                }
            }
        }

        private void NotifyStopped()
        {
            lock (stoppedLock)
            {
                if (stopped != null)
                {
                    Delegate[] handlers = stopped.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        ((Action)handler)();
                    }
                }
            }
        }

        private void OnTimeChanged(DateTime dateTime)
        {
            lock (timeChangedLock)
            {
                if (timeChanged != null)
                {
                    Delegate[] handlers = timeChanged.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var subscriber = handler as Action<DateTime>;
                        subscriber?.Invoke(dateTime);
                    }
                }
            }
        }

        private void ExecuteAction(Action action)
        {
            if (IsAsynchronous)
            {
                var thread = new Thread(new ThreadStart(action)) { Name = "Timeline", Priority = ThreadPriority.Normal };
                thread.Start();
            }
            else
            {
                action();
            }
        }

        private class TimelineSubscriptionProvider<T> : SubscriptionProvider<T>, ISubscriptionProvider<T>
            where T : TemporalEntity
        {
            /// <summary>
            /// The series enumerable provider.
            /// </summary>
            private readonly IHistoricalData<T> historicalData;

            private readonly object activeProvidersLock = new object();
            private readonly Dictionary<Topic, TimelineProvider> activeProviders = new Dictionary<Topic, TimelineProvider>();
            private readonly object enumerableDictionaryLock = new object();
            private readonly Dictionary<Topic, IEnumerable<TemporalEntity>> enumerableDictionary = new Dictionary<Topic, IEnumerable<TemporalEntity>>();
            private readonly Timeline timeline;

            /// <summary>
            /// Initializes a new instance of the <see cref="TimelineSubscriptionProvider{T}"/> class.
            /// </summary>
            /// <param name="timeline">The timeline instance.</param>
            /// <param name="historicalData">The series enumerable provider.</param>
            internal TimelineSubscriptionProvider(Timeline timeline, IHistoricalData<T> historicalData)
                : base(historicalData.Provider)
            {
                this.timeline = timeline;
                this.historicalData = historicalData;
            }

            /// <summary>
            /// Subscribes to a new subscription.
            /// </summary>
            /// <param name="instrument">The instrument to subscribe.</param>
            /// <param name="timeGranularity">The time granularity to subscribe.</param>
            /// <returns>A subscription interface.</returns>
            public ISubscription<T> Subscribe(Instrument instrument, TimeGranularity timeGranularity)
            {
                return base.Subscribe(instrument, timeGranularity);
            }

            /// <summary>
            /// Subscribes to a new subscription.
            /// </summary>
            /// <param name="instrument">The instrument to subscribe.</param>
            /// <param name="timeGranularity">The time granularity to subscribe.</param>
            /// <param name="action">A caller-specified action.</param>
            /// <returns>A subscription interface.</returns>
            public new ISubscription<T> Subscribe(Instrument instrument, TimeGranularity timeGranularity, Action<T> action)
            {
                return base.Subscribe(instrument, timeGranularity, action);
            }

            /// <summary>
            /// Creates a new timeline provider from a given topic.
            /// </summary>
            /// <param name="topic">The topic.</param>
            /// <param name="provider">A data provider name.</param>
            /// <returns>The timeline provider.</returns>
            protected sealed override GenericProvider CreateProvider(Topic topic, string provider)
            {
                return new TimelineProvider(this, topic, provider);
            }

            private sealed class TimelineProvider : GenericProvider
            {
                private readonly TimelineSubscriptionProvider<T> parent;

                /// <summary>
                /// Initializes a new instance of the <see cref="TimelineProvider"/> class.
                /// </summary>
                /// <param name="parent">The parent timeline subscription provider.</param>
                /// <param name="topic">The topic.</param>
                /// <param name="provider">A data provider name.</param>
                internal TimelineProvider(TimelineSubscriptionProvider<T> parent, Topic topic, string provider)
                    : base(topic, provider, true)
                {
                    this.parent = parent;
                }

                /// <summary>
                /// Performs the activation of this subscription.
                /// </summary>
                protected override void OnConnect()
                {
                    lock (parent.activeProvidersLock)
                    {
                        parent.activeProviders.Add(Topic, this);
                    }

                    if (parent.historicalData == null)
                    {
                        return;
                    }

                    Topic topic = Topic;
                    IEnumerable<T> enumerableT = parent.historicalData.FetchAsync(
                        new HistoricalDataRequest(topic.Instrument, parent.timeline.BeginTime, parent.timeline.EndTime, topic.TimeGranularity)).GetAwaiter().GetResult();
                    IEnumerable<TemporalEntity> enumerable = enumerableT;

                    lock (parent.enumerableDictionaryLock)
                    {
                        parent.enumerableDictionary.Add(topic, enumerable);
                    }

                    lock (parent.timeline.mergingEnumerableLock)
                    {
                        parent.timeline.mergingEnumerable.Add(enumerable, t => OnEvent((T)t), topic.TimeGranularity);
                    }
                }

                /// <summary>
                /// Performs the cancelling of this subscription.
                /// </summary>
                protected override void OnDisconnect()
                {
                    lock (parent.activeProvidersLock)
                    {
                        parent.activeProviders.Remove(Topic);
                    }

                    IEnumerable<TemporalEntity> enumerable;
                    lock (parent.enumerableDictionaryLock)
                    {
                        if (parent.enumerableDictionary.TryGetValue(Topic, out enumerable))
                        {
                            parent.enumerableDictionary.Remove(Topic);
                        }
                    }

                    if (enumerable != null)
                    {
                        lock (parent.timeline.mergingEnumerableLock)
                        {
                            parent.timeline.mergingEnumerable.Remove(enumerable);
                        }
                    }
                }
            }
        }
    }
}
