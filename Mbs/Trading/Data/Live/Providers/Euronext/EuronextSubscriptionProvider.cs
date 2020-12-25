using System;
using System.Collections.Generic;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;
using Mbs.Utilities;

// ReSharper disable once CheckNamespace
namespace Mbs.Trading.Data.Live
{
    /// <summary>
    /// A generic Euronext subscription provider.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public sealed class EuronextSubscriptionProvider<T> : SubscriptionProvider<T>, ISubscriptionProvider<T>
        where T : TemporalEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EuronextSubscriptionProvider{T}"/> class.
        /// </summary>
        public EuronextSubscriptionProvider()
            : base("Euronext")
        {
            if (typeof(Ohlcv) != typeof(T) && typeof(Trade) == typeof(T) && typeof(Quote) == typeof(T))
            {
                throw new ArgumentException($"Unsupported TemporalEntity type parameter: {typeof(T)}.");
            }
        }

        /// <inheritdoc />
        public ISubscription<T> Subscribe(Instrument instrument, TimeGranularity timeGranularity)
        {
            return Subscribe(instrument, timeGranularity, null);
        }

        /// <inheritdoc />
        public new ISubscription<T> Subscribe(Instrument instrument, TimeGranularity timeGranularity, Action<T> action)
        {
            if (typeof(T) == Types.OhlcvType)
            {
                if (!timeGranularity.IsIntraday() && !timeGranularity.IsAperiodic())
                {
                    throw new ArgumentException($"Intraday or trade time granularity required, got {timeGranularity.ToString()}.", nameof(timeGranularity));
                }
            }
            else
            {
                if (!timeGranularity.IsAperiodic() || timeGranularity.NumberOfUnits() != 1)
                {
                    throw new ArgumentException($"Aperiodic (one-trade) time granularity required, got {timeGranularity.ToString()}.", nameof(timeGranularity));
                }

                timeGranularity = TimeGranularity.Aperiodic;
            }

            return base.Subscribe(instrument, timeGranularity, action);
        }

        /// <inheritdoc />
        protected override GenericProvider CreateProvider(Topic topic, string provider)
        {
            Type type = typeof(T);
            if (type == Types.OhlcvType)
            {
                return new OhlcvTopicProvider(topic, provider);
            }

            if (type == Types.TradeType)
            {
                return new TradeTopicProvider(topic, provider);
            }

            return new QuoteTopicProvider(topic, provider);
        }

        private sealed class QuoteTopicProvider : GenericProvider
        {
            private EuronextMonitor.InstrumentQuoteMonitor instrumentQuoteMonitor;

            /// <summary>
            /// Initializes a new instance of the <see cref="QuoteTopicProvider"/> class.
            /// </summary>
            /// <param name="topic">A topic.</param>
            /// <param name="vendor">A data provider name.</param>
            internal QuoteTopicProvider(Topic topic, string vendor)
                : base(topic, vendor)
            {
            }

            /// <inheritdoc />
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    instrumentQuoteMonitor.Dispose();
                }

                base.Dispose(disposing);
            }

            /// <inheritdoc />
            protected override void OnConnect()
            {
                instrumentQuoteMonitor = EuronextMonitor.GetInstrumentQuoteMonitor(Topic.Instrument);
                instrumentQuoteMonitor.Event += Receive;
            }

            /// <inheritdoc />
            protected override void OnDisconnect()
            {
                instrumentQuoteMonitor.Event -= Receive;
            }

            private void Receive(Quote quote)
            {
                OnEvent(quote as T);
            }
        }

        private sealed class OhlcvTopicProvider : GenericProvider
        {
            private readonly AutoResetEventThread inputThread;
            private readonly CompareAndSwapQueue<IEnumerable<Trade>> inputQueue = new CompareAndSwapQueue<IEnumerable<Trade>>();
            private EuronextMonitor.InstrumentTradeMonitor instrumentTradeMonitor;
            private bool inputActive;

            /// <summary>
            /// Initializes a new instance of the <see cref="OhlcvTopicProvider"/> class.
            /// </summary>
            /// <param name="topic">A topic.</param>
            /// <param name="provider">A data provider name.</param>
            internal OhlcvTopicProvider(Topic topic, string provider)
                : base(topic, provider)
            {
                inputThread = new AutoResetEventThread(() =>
                {
                    TimeGranularity timeGranularity = Topic.TimeGranularity;
                    Ohlcv ohlcv = null;
                    int numberOfUnits = timeGranularity.NumberOfUnits();
                    Func<Trade, int, DateTime> thresholdDateTime = null;
                    if (timeGranularity.IsSecond())
                    {
                        thresholdDateTime = AggregatingConverter.SecondBinThreshold;
                    }
                    else if (timeGranularity.IsMinute())
                    {
                        thresholdDateTime = AggregatingConverter.MinuteBinThreshold;
                    }
                    else if (timeGranularity.IsHour())
                    {
                        thresholdDateTime = AggregatingConverter.HourBinThreshold;
                    }

                    IEnumerable<Trade> enumerable;
                    if (thresholdDateTime != null)
                    {
                        var dateTime = new DateTime(0L);
                        void ActionWithThreshold()
                        {
                            while ((enumerable = inputQueue.Dequeue()) != null)
                            {
                                foreach (Trade trade in enumerable)
                                {
                                    if (ohlcv != null && dateTime > trade.Time)
                                    {
                                        ohlcv.Aggregate(trade);
                                    }
                                    else
                                    {
                                        if (ohlcv != null)
                                        {
                                            ohlcv.Time = dateTime;

                                            Log.Debug($"EuronextTopicProvider.Ohlcv: {ohlcv}");
                                            OnEvent(ohlcv as T);
                                        }

                                        ohlcv = Ohlcv.CloneAggregation(trade);
                                        dateTime = thresholdDateTime(trade, numberOfUnits);
                                    }
                                }
                            }
                        }

                        while (inputActive)
                        {
                            ActionWithThreshold();
                            if (inputActive)
                            {
                                inputThread.AutoResetEvent.WaitOne(10000, false);
                            }
                        }

                        // Process the trades left in the input queue.
                        // action()
                    }
                    else
                    {
                        int count = 0;

                        void Action()
                        {
                            while ((enumerable = inputQueue.Dequeue()) != null)
                            {
                                foreach (Trade trade in enumerable)
                                {
                                    if (++count >= numberOfUnits)
                                    {
                                        count = 0;
                                        if (ohlcv != null)
                                        {
                                            OnEvent(ohlcv as T);
                                        }

                                        ohlcv = Ohlcv.CloneAggregation(trade);
                                    }
                                    else
                                    {
                                        if (ohlcv == null)
                                        {
                                            ohlcv = Ohlcv.CloneAggregation(trade);
                                        }
                                        else
                                        {
                                            ohlcv.Aggregate(trade);
                                        }
                                    }
                                }
                            }
                        }

                        while (inputActive)
                        {
                            Action();
                            if (inputActive)
                            {
                                inputThread.AutoResetEvent.WaitOne(10000, false);
                            }
                        }

                        // Process the ohlcv entities left in the input queue.
                        // action()
                    }

                    // Output the unfinished ohlcv.
                    if (ohlcv != null)
                    {
                        OnEvent(ohlcv as T);
                    }

                    Dispose();
                }) { Thread = { IsBackground = true } };
            }

            /// <inheritdoc />
            protected override void OnConnect()
            {
                instrumentTradeMonitor = EuronextMonitor.GetInstrumentTradeMonitor(Topic.Instrument);
                inputActive = true;
                inputThread.Thread.Start();
                instrumentTradeMonitor.Event += Receive;
                instrumentTradeMonitor.AddGranularity(Topic.TimeGranularity);
            }

            /// <inheritdoc />
            protected override void OnDisconnect()
            {
                inputActive = false;
                instrumentTradeMonitor.Event -= Receive;
                instrumentTradeMonitor.RemoveGranularity(Topic.TimeGranularity);
            }

            /// <inheritdoc />
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    instrumentTradeMonitor.Dispose();
                    inputThread.Dispose();
                }

                base.Dispose(disposing);
            }

            private void Receive(IEnumerable<Trade> enumerable)
            {
                if (inputActive)
                {
                    inputQueue.Enqueue(enumerable);
                    if (inputActive)
                    {
                        inputThread.AutoResetEvent.Set();
                    }
                }
            }
        }

        private sealed class TradeTopicProvider : GenericProvider
        {
            private EuronextMonitor.InstrumentTradeMonitor instrumentTradeMonitor;

            /// <summary>
            /// Initializes a new instance of the <see cref="TradeTopicProvider"/> class.
            /// </summary>
            /// <param name="topic">A topic.</param>
            /// <param name="provider">A data provider name.</param>
            internal TradeTopicProvider(Topic topic, string provider)
                : base(topic, provider)
            {
            }

            /// <inheritdoc />
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    instrumentTradeMonitor.Dispose();
                }

                base.Dispose(disposing);
            }

            /// <inheritdoc />
            protected override void OnConnect()
            {
                instrumentTradeMonitor = EuronextMonitor.GetInstrumentTradeMonitor(Topic.Instrument);
                instrumentTradeMonitor.Event += Receive;
                instrumentTradeMonitor.AddGranularity(Topic.TimeGranularity);
            }

            /// <inheritdoc />
            protected override void OnDisconnect()
            {
                instrumentTradeMonitor.Event -= Receive;
                instrumentTradeMonitor.RemoveGranularity(Topic.TimeGranularity);
            }

            private void Receive(IEnumerable<Trade> enumerable)
            {
                foreach (Trade t in enumerable)
                {
                    OnEvent(t as T);
                }
            }
        }
    }
}
