using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Mbs;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Historical;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once IdentifierTypo
namespace CsvHistoricalInstrumentData
{
    internal class HostedService : IHostedService
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger logger;
        private readonly IApplicationLifetime appLifetime;
        private readonly IConfiguration configuration;

        private readonly CsvOhlcvHistoricalData csvOhlcvHistoricalData = new CsvOhlcvHistoricalData();
        private readonly CsvTradeHistoricalData csvTradeHistoricalData = new CsvTradeHistoricalData();
        private readonly CsvScalarHistoricalData csvScalarHistoricalData = new CsvScalarHistoricalData();
        private readonly CsvQuoteHistoricalData csvQuoteHistoricalData = new CsvQuoteHistoricalData();

        public HostedService(ILoggerFactory loggerFactory, ILogger<HostedService> logger, IApplicationLifetime appLifetime, IConfiguration configuration)
        {
            this.loggerFactory = loggerFactory;
            this.logger = logger;
            this.appLifetime = appLifetime;
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync has been called.");
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StopAsync has been called.");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Perform post-startup activities here.
        /// </summary>
        private void OnStarted()
        {
            logger.LogInformation("OnStarted has been called.");

            Log.SetLogger(loggerFactory.CreateLogger("Mbs"));
            CsvRepository.IsDataCache = configuration.GetValue<bool>("IsDataCached");
            CsvRepository.RepositoryPath = configuration.GetValue<string>("RepositoryPath");

            var csvInstrumentInfos = configuration.GetSection("CsvInstrumentInfos").Get<CsvInstrumentInfo[]>();
            foreach (var csv in csvInstrumentInfos)
                CsvRepository.Add(csv.Instrument, csv.CsvInfo);

            var instrumentHistoricalDataRequests = configuration.GetSection("InstrumentHistoricalDataRequests").Get<InstrumentHistoricalDataRequest[]>();
            foreach (var req in instrumentHistoricalDataRequests)
            {
                logger.LogInformation($"fetching {req.Moniker}");
                var hdr = req.HistoricalDataRequest;
                var list = csvOhlcvHistoricalData.FetchAsync(hdr).GetAwaiter().GetResult();
                foreach (var t in list)
                {
                    Console.WriteLine(
                        $"{t.Time.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}; {t.Open.ToString(CultureInfo.InvariantCulture)}; {t.High.ToString(CultureInfo.InvariantCulture)}; {t.Low.ToString(CultureInfo.InvariantCulture)}; {t.Close.ToString(CultureInfo.InvariantCulture)}; {t.Volume.ToString(CultureInfo.InvariantCulture)}");
                }
                logger.LogInformation($"fetched {req.Moniker}, IsDataAdjusted = {hdr.IsDataAdjusted}");
            }
        }

        /// <summary>
        /// Perform on-stopping activities here.
        /// </summary>
        private void OnStopping()
        {
            logger.LogInformation("OnStopping has been called.");
        }

        /// <summary>
        /// Perform post-stopped activities here.
        /// </summary>
        private void OnStopped()
        {
            logger.LogInformation("OnStopped has been called.");
        }
    }
}
