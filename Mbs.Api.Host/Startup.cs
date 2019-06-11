using Mbs.Api.Extensions;
using Mbs.Api.Host.Extensions;
using Mbs.Api.Services.Trading.Instruments;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Mbs.Api.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            enableSwagger = configuration.GetSection("EnableSwagger").Get<bool>();
        }

        private readonly IConfiguration configuration;
        private readonly bool enableSwagger;

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddMbsApi()
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            if (enableSwagger)
                services.AddMbsApiSwagger("Mbs.Api.Host");
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IInstrumentListDataService instrumentList)
        {
            Log.SetLogger(loggerFactory.CreateLogger("Mbs"));
            instrumentList.AddListFromJsonFile("euronext", "euronext.json");

            app
                .UseNwebsec()
                .UseMbsApiExceptionHandling()
                .UseCorsConfiguration(configuration);

            if (enableSwagger)
                app.UseMbsApiSwagger();

            /* app.UseHttpsRedirection(); */
            app.UseMvcWithDefaultRoute();
        }
    }
}
