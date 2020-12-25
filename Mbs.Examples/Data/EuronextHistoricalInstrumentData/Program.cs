using System;
using System.Globalization;
using Mbs.Trading.Data.Historical;
using Mbs.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EuronextHistoricalInstrumentData
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once ConvertToStaticClass
    internal sealed class Program
    {
        private Program()
        {
        }

        internal static void Main()
        {
            var serviceProvider = BuildServiceProvider();

            Log.SetLogger(serviceProvider.GetService<ILoggerFactory>().CreateLogger("Mbs"));
            var logger = serviceProvider.GetService<ILogger<Program>>();

            var configuration = serviceProvider.GetService<IConfigurationRoot>();
            var request = configuration.GetSection("InstrumentHistoricalDataRequest").Get<InstrumentHistoricalDataRequest>();

            EuronextHistoricalData.IsDataCached = false;
            logger.LogInformation($"fetching {request.Moniker}");

            try
            {
                var list = EuronextHistoricalData.FetchAsync(request.HistoricalDataRequest).GetAwaiter().GetResult();
                foreach (var t in list)
                {
                    Console.WriteLine(
                        $"{t.Time.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}; {t.Open.ToString(CultureInfo.InvariantCulture)}; {t.High.ToString(CultureInfo.InvariantCulture)}; {t.Low.ToString(CultureInfo.InvariantCulture)}; {t.Close.ToString(CultureInfo.InvariantCulture)}; {t.Volume.ToString(CultureInfo.InvariantCulture)}");
                }

                logger.LogInformation("fetched, press any key to exit");
                Console.ReadKey();
            }
            catch (Exception exception)
            {
                logger.LogCritical($"exception: {exception.Message}");
            }
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
