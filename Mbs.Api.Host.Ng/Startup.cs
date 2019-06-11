using System;
using System.Collections.Generic;
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMbsApi();
            services.AddMbsApiSwagger("Mbs.Api.Host.Ng");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot");
        }

        public void Configure(IApplicationBuilder app, IConfiguration configuration, IHostingEnvironment environment, ILoggerFactory loggerFactory, IInstrumentListDataService instrumentList)
        {
            Log.SetLogger(loggerFactory.CreateLogger("Mbs"));
            instrumentList.AddListFromJsonFile("euronext", "euronext.json");

            if (environment.IsDevelopment())
                app.UseDeveloperExceptionPage();
            /* else
                app.UseExceptionHandler("/Error"); */

            // Registered before static files to always set header.
            app.UseNwebsec();
            app.UseCorsConfiguration(configuration);
            /* app.UseAngularRouting(); */
            /* app.UseHttpsRedirection(); */

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMbsApiSwagger();
            app.UseMbsApiExceptionHandling();

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
