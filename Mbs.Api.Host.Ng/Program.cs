using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Mbs.Api.Host.Ng
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        config
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", false, true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                            .AddEnvironmentVariables();
                    });
                    webBuilder.ConfigureLogging((hostingContext, loggingBuilder) =>
                    {
                        // Remove console and debugger loggers provided by CreateDefaultBuilder().
                        loggingBuilder.ClearProviders();
                        loggingBuilder.AddConsole();
                    });
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
