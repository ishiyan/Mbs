﻿using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Mbs.Api.Host
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            Serilog.Log.Information("Stopping logger");
            Serilog.Log.CloseAndFlush();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            IConfigurationRoot configurationRoot = BuildConfiguration();
            return WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                        .AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, loggingBuilder) =>
                {
                    // Remove console and debugger loggers provided by CreateDefaultBuilder().
                    loggingBuilder.ClearProviders();
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    Serilog.Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .CreateLogger();
                    Serilog.Log.Information($"Created logger, environment is {hostingContext.HostingEnvironment.EnvironmentName}");

                    /* services.AddApplicationInsightsTelemetry(hostingContext.Configuration); */
                })
                .UseSerilog()
                .UseConfiguration(configurationRoot)
                .UseStartup<Startup>();
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
