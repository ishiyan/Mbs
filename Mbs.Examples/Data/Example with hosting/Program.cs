using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Mbs;
using Mbs.Trading.Data.Historical;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once StringLiteralTypo
namespace CsvHistoricalInstrumentData
{
    internal class Program
    {
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
                    services.AddHostedService<HostedService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
