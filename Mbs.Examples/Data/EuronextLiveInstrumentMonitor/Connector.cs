using System;
using System.Collections.Generic;
using System.Globalization;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;
using Microsoft.Extensions.Logging;

namespace EuronextLiveInstrumentMonitor
{
    internal static class Connector
    {
        private static readonly EuronextSubscriptionProvider<Ohlcv> OhlcvProvider = new EuronextSubscriptionProvider<Ohlcv>();
        private static readonly EuronextSubscriptionProvider<Trade> TradeProvider = new EuronextSubscriptionProvider<Trade>();
        private static readonly EuronextSubscriptionProvider<Quote> QuoteProvider = new EuronextSubscriptionProvider<Quote>();

        private static readonly List<ISubscription<Ohlcv>> OhlcvSubscriptions = new List<ISubscription<Ohlcv>>();
        private static readonly List<ISubscription<Trade>> TradeSubscriptions = new List<ISubscription<Trade>>();
        private static readonly List<ISubscription<Quote>> QuoteSubscriptions = new List<ISubscription<Quote>>();

        internal static Type StringToType(string type)
        {
            switch (type.ToUpper(CultureInfo.InvariantCulture))
            {
                case "OHLCV": return typeof(Ohlcv);
                case "TRADE": return typeof(Trade);
                case "QUOTE": return typeof(Quote);
            }

            throw new ArgumentException($"temporal entity {type} is not supported.", nameof(type));
        }

        internal static void Subscribe<T>(Instrument instrument, TimeGranularity timeGranularity, string type, ILogger logger)
            where T : TemporalEntity
        {
            string instr = $"{instrument.Symbol}@{instrument.Exchange.Mic}@{timeGranularity}@{type}";
            Console.Write($"subscribing {instr} ...");

            ISubscriptionProvider<T> provider;
            Action<T> action;

            if (typeof(T) == typeof(Ohlcv))
            {
                provider = OhlcvProvider as ISubscriptionProvider<T>;
                action = t =>
                {
                    if (t is Ohlcv ohlcv)
                        Console.WriteLine(
                            $"{instr}; {t.Time.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}; {ohlcv.Open.ToString(CultureInfo.InvariantCulture)}; {ohlcv.High.ToString(CultureInfo.InvariantCulture)}; {ohlcv.Low.ToString(CultureInfo.InvariantCulture)}; {ohlcv.Close.ToString(CultureInfo.InvariantCulture)}; {ohlcv.Volume.ToString(CultureInfo.InvariantCulture)}");
                };
            }
            else if (typeof(T) == typeof(Trade))
            {
                provider = TradeProvider as ISubscriptionProvider<T>;
                action = t =>
                {
                    if (t is Trade trade)
                        Console.WriteLine(
                            $"{instr}; {t.Time.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}; {trade.Price.ToString(CultureInfo.InvariantCulture)}; {trade.Volume.ToString(CultureInfo.InvariantCulture)}");
                };
            }
            else if (typeof(T) == typeof(Quote))
            {
                provider = QuoteProvider as ISubscriptionProvider<T>;
                action = t =>
                {
                    if (t is Quote quote)
                        Console.WriteLine(
                            $"{instr}; {t.Time.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}; {quote.AskPrice.ToString(CultureInfo.InvariantCulture)}; {quote.BidPrice.ToString(CultureInfo.InvariantCulture)}; {quote.AskSize.ToString(CultureInfo.InvariantCulture)}; {quote.BidSize.ToString(CultureInfo.InvariantCulture)}");
                };
            }
            else
            {
                throw new ArgumentException($"temporal entity {type} is not supported.", nameof(type));
            }

            try
            {
                if (provider == null)
                    throw new ArgumentException($"temporal entity {type} is not supported.", nameof(type));

                var subscription = provider.Subscribe(instrument, timeGranularity, action);

                AddSubscription(subscription);
                Console.WriteLine(" subscribed");
            }
            catch (Exception exception)
            {
                logger.LogCritical($"exception: {exception.Message}");
            }
        }

        private static void AddSubscription<T>(ISubscription<T> subscription)
            where T : TemporalEntity
        {
            if (typeof(T) == typeof(Ohlcv))
            {
                OhlcvSubscriptions.Add(subscription as ISubscription<Ohlcv>);
            }
            else if (typeof(T) == typeof(Trade))
            {
                TradeSubscriptions.Add(subscription as ISubscription<Trade>);
            }
            else if (typeof(T) == typeof(Quote))
            {
                QuoteSubscriptions.Add(subscription as ISubscription<Quote>);
            }
        }

        internal static void ConnectAll()
        {
            foreach (var s in TradeSubscriptions)
            {

                Console.Write($"connecting {s.Instrument.Symbol}@{s.Instrument.Exchange.Mic}@{s.TimeGranularity}@Trade ...");
                s.Connect();
                Console.WriteLine(" connected");
            }

            foreach (var s in OhlcvSubscriptions)
            {
                Console.Write($"connecting {s.Instrument.Symbol}@{s.Instrument.Exchange.Mic}@{s.TimeGranularity}@Ohlcv ...");
                s.Connect();
                Console.WriteLine(" connected");
            }

            foreach (var s in QuoteSubscriptions)
            {
                Console.Write($"connecting {s.Instrument.Symbol}@{s.Instrument.Exchange.Mic}@{s.TimeGranularity}@Quote ...");
                s.Connect();
                Console.WriteLine(" connected");
            }
        }

        internal static void DisconnectAll()
        {
            foreach (var s in QuoteSubscriptions)
            {
                Console.Write($"disconnecting {s.Instrument.Symbol}@{s.Instrument.Exchange.Mic}@{s.TimeGranularity}@Quote ...");
                s.Disconnect();
                Console.WriteLine(" disconnected");
            }

            foreach (var s in OhlcvSubscriptions)
            {
                Console.Write($"disconnecting {s.Instrument.Symbol}@{s.Instrument.Exchange.Mic}@{s.TimeGranularity}@Ohlcv ...");
                s.Disconnect();
                Console.WriteLine(" disconnected");
            }

            foreach (var s in TradeSubscriptions)
            {
                Console.Write($"disconnecting {s.Instrument.Symbol}@{s.Instrument.Exchange.Mic}@{s.TimeGranularity}@Trade ...");
                s.Disconnect();
                Console.WriteLine(" disconnected");
            }
        }
    }
}
