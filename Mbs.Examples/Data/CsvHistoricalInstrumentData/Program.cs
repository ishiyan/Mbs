using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Data.Historical.Providers.Csv;
using Mbs.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CsvHistoricalInstrumentData
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once ConvertToStaticClass
    internal sealed class Program
    {
        private Program()
        {
        }

        private static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false);
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging
                        .AddConfiguration(hostContext.Configuration.GetSection("Logging"))
                        .AddConsole()
                        .AddDebug();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<CsvInstrumentInfo[]>(hostContext.Configuration.GetSection("CsvInstrumentInfos"));
                    services.Configure<InstrumentHistoricalDataRequest[]>(hostContext.Configuration.GetSection("InstrumentHistoricalDataRequests"));
                })
                .Build();

            await Execute(host.Services);

            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        private static async Task Execute(IServiceProvider serviceProvider)
        {
            var csvOhlcvHistoricalData = new CsvOhlcvHistoricalData();
            var csvTradeHistoricalData = new CsvTradeHistoricalData();
            var csvQuoteHistoricalData = new CsvQuoteHistoricalData();
            var csvScalarHistoricalData = new CsvScalarHistoricalData();

            Log.SetLogger(serviceProvider.GetService<ILoggerFactory>().CreateLogger("Mbs"));
            var logger = serviceProvider.GetService<ILogger<Program>>();
            var configuration = serviceProvider.GetService<IConfiguration>();

            CsvRepository.IsDataCache = configuration.GetValue<bool>("IsDataCached");
            CsvRepository.RepositoryPath = configuration.GetValue<string>("RepositoryPath");

            var csvInstrumentInfos = configuration.GetSection("CsvInstrumentInfos").Get<CsvInstrumentInfo[]>();
            foreach (var csv in csvInstrumentInfos)
            {
                CsvRepository.Add(csv.Instrument, csv.CsvInfo);
            }

            var instrumentHistoricalDataRequests = configuration.GetSection("InstrumentHistoricalDataRequests").Get<InstrumentHistoricalDataRequest[]>();
            foreach (var req in instrumentHistoricalDataRequests)
            {
                logger.LogInformation($"fetching {req.Moniker}");
                var hdr = req.HistoricalDataRequest;
                var entityType = req.GetEntityType();

                if (entityType == typeof(Ohlcv))
                {
                    var enumerable = await csvOhlcvHistoricalData.FetchAsync(hdr);
                    foreach (var e in enumerable)
                    {
                        if (e != null)
                        {
                            Console.WriteLine(
                                $"{e.Time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}; {e.Open.ToString(CultureInfo.InvariantCulture)}; {e.High.ToString(CultureInfo.InvariantCulture)}; {e.Low.ToString(CultureInfo.InvariantCulture)}; {e.Close.ToString(CultureInfo.InvariantCulture)}; {e.Volume.ToString(CultureInfo.InvariantCulture)}");
                        }
                    }
                }
                else if (entityType == typeof(Trade))
                {
                    var enumerable = await csvTradeHistoricalData.FetchAsync(hdr);
                    foreach (var e in enumerable)
                    {
                        Console.WriteLine(
                            $"{e.Time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}; {e.Price.ToString(CultureInfo.InvariantCulture)}; {e.Volume.ToString(CultureInfo.InvariantCulture)};");
                    }
                }
                else if (entityType == typeof(Quote))
                {
                    var enumerable = await csvQuoteHistoricalData.FetchAsync(hdr);
                    foreach (var e in enumerable)
                    {
                        Console.WriteLine(
                            $"{e.Time.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}; {e.AskPrice.ToString(CultureInfo.InvariantCulture)}; {e.BidPrice.ToString(CultureInfo.InvariantCulture)}; {e.AskSize.ToString(CultureInfo.InvariantCulture)}; {e.BidSize.ToString(CultureInfo.InvariantCulture)}");
                    }
                }
                else if (entityType == typeof(Scalar))
                {
                    var enumerable = await csvScalarHistoricalData.FetchAsync(hdr);
                    foreach (var e in enumerable)
                    {
                        Console.WriteLine(
                            $"{e.Time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}; {e.Value.ToString(CultureInfo.InvariantCulture)};");
                    }
                }

                logger.LogInformation($"fetched {req.Moniker}, IsDataAdjusted = {hdr.IsDataAdjusted}");
            }
        }
    }
}
