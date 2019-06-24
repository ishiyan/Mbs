using System;
using Mbs.Api.Extensions;
using Mbs.Api.Host.Ng.Extensions;
using Mbs.Api.Services.Trading.Instruments;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Mbs.Api.Host.Ng
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            enableSwagger = configuration.GetSection("EnableSwagger").Get<bool>();
        }

        private readonly IConfiguration configuration;
        private readonly bool enableSwagger;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMbsApi();
            if (enableSwagger)
                services.AddMbsApiSwagger("Mbs.Api.Host.Ng");

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(conf => conf.RootPath = "wwwroot");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, ILoggerFactory loggerFactory, IInstrumentListDataService instrumentList)
        {
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            if (instrumentList == null)
                throw new ArgumentNullException(nameof(instrumentList));

            Log.SetLogger(loggerFactory.CreateLogger("Mbs"));
            instrumentList.AddListFromJsonFile("euronext", "euronext.json");

            // Registered before static files to always set header.
            app.UseNwebsec();
            app.UseMbsApiExceptionHandling();
            app.UseCorsConfiguration(configuration);
            if (enableSwagger)
                app.UseMbsApiSwagger();
            /* app.UseAngularRouting(); */

            app.UseSpaStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseSpa(spa =>
            {
                // See https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";
                spa.Options.StartupTimeout = new TimeSpan(0, 0, 2, 0);
                if (environment.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");

                    // When you want to launch the application: cd ClientApp; ng serve; cd ..; dotnet run
                    // spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
