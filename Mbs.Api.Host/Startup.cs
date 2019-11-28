using System;
using Mbs.Api.Extensions;
using Mbs.Api.Extensions.ExceptionHandling;
using Mbs.Api.Extensions.Swagger;
using Mbs.Api.Host.Extensions;
using Mbs.Api.Services.Trading.Instruments;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            services
                .AddControllers()
                /* use newtonsoft because swagger uses it anyway */
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });
                /* use microsoft json; this still doesn't work together with swagger */
                /*.AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
                });*/
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IInstrumentListDataService instrumentList)
        {
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            if (instrumentList == null)
                throw new ArgumentNullException(nameof(instrumentList));

            Log.SetLogger(loggerFactory.CreateLogger("Mbs"));
            Log.SetLoggerFactory(loggerFactory);
            instrumentList.AddListFromJsonFile("euronext", "euronext.json");

            app.UseNwebsec();
            app.UseMbsApiExceptionHandling();
            app.UseCorsConfiguration(configuration);
            if (enableSwagger)
                app.UseMbsApiSwagger();

            app.UseStaticFiles();
            app.UseRouting();
            /* app.UseHttpsRedirection(); */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });

        }
    }
}
