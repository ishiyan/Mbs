using System;
using Mbs.Api.Extensions;
using Mbs.Api.Extensions.ExceptionHandling;
using Mbs.Api.Extensions.Swagger;
using Mbs.Api.Host.Extensions;
using Mbs.Api.Services.Trading.Instruments;
using Mbs.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Mbs.Api.Host
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly bool enableSwagger;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            enableSwagger = configuration.GetSection("EnableSwagger").Get<bool>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMbsApi();
            if (enableSwagger)
            {
                services.AddMbsApiSwagger("Mbs.Api.Host");
            }

            services
                .AddControllers()
                /* use newtonsoft because swagger uses it anyway */
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });
#pragma warning disable S125 // Sections of code should not be commented out
            /* use microsoft json; this still doesn't work together with swagger */
            /*.AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
                });*/
#pragma warning restore S125
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IInstrumentListDataService instrumentList)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (instrumentList == null)
            {
                throw new ArgumentNullException(nameof(instrumentList));
            }

            Log.SetLogger(loggerFactory.CreateLogger("Mbs"));
            Log.SetLoggerFactory(loggerFactory);
            instrumentList.AddListFromJsonFile("euronext", "euronext.json");

            app.UseNwebsec();
            app.UseMbsApiExceptionHandling();
            app.UseCorsConfiguration(configuration);
            if (enableSwagger)
            {
                app.UseMbsApiSwagger();
            }

            app.UseStaticFiles();
            app.UseRouting();
            /* app.UseHttpsRedirection() */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
