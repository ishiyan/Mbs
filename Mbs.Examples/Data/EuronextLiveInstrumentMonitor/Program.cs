using System;
using System.Collections.Generic;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EuronextLiveInstrumentMonitor
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once ConvertToStaticClass
    internal sealed class Program
    {
        private static ILogger logger;

        private Program()
        {
        }

        internal static void Main()
        {
            var serviceProvider = BuildServiceProvider();

            Log.SetLogger(serviceProvider.GetService<ILoggerFactory>().CreateLogger("Mbs"));
            logger = serviceProvider.GetService<ILogger<Program>>();

            var configuration = serviceProvider.GetService<IConfigurationRoot>();
            EuronextMonitor.IsSubscriptionWithHistory = configuration.GetSection("IsSubscriptionWithHistory").Get<bool>();
            EuronextMonitor.MinimalTradePollingPeriodMilliseconds = configuration.GetSection("MinimalTradePollingPeriodMilliseconds").Get<long>();

            IEnumerable<InstrumentContext> instrumentContexts = configuration.GetSection("InstrumentContexts").Get<InstrumentContext[]>();
            foreach (var ic in instrumentContexts)
            {
                var type = Connector.StringToType(ic.Type);
                if (type == typeof(Ohlcv))
                {
                    Connector.Subscribe<Ohlcv>(new Instrument(ic.Symbol, ic.Mic, ic.Isin), ic.TimeGranularity, ic.Type, logger);
                }
                else if (type == typeof(Trade))
                {
                    Connector.Subscribe<Trade>(new Instrument(ic.Symbol, ic.Mic, ic.Isin), ic.TimeGranularity, ic.Type, logger);
                }
                else
                {
                    Connector.Subscribe<Quote>(new Instrument(ic.Symbol, ic.Mic, ic.Isin), ic.TimeGranularity, ic.Type, logger);
                }
            }

            Console.WriteLine($"subscription with history: {EuronextMonitor.IsSubscriptionWithHistory}");
            Console.WriteLine($"minimal trade polling period ms: {EuronextMonitor.MinimalTradePollingPeriodMilliseconds}");
            Connector.ConnectAll();
            Console.WriteLine("press any key to disconnect all instruments and exit ...");
            Console.ReadKey();
            Console.WriteLine(string.Empty);
            Connector.DisconnectAll();
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();
            serviceCollection
                .AddSingleton(configuration)
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                });

            return serviceCollection.BuildServiceProvider();
        }
    }
}
