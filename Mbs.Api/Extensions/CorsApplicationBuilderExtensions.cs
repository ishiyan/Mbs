using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Mbs.Api.Extensions
{
    /// <summary>
    /// Registers CORS middleware in the Http pipeline.
    /// </summary>
    public static class CorsApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds CORS middleware to the <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="app">An application builder instance.</param>
        /// <param name="configuration">The configuration interface.</param>
        /// <returns>An updated application builder instance.</returns>
        public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            IConfigurationSection configurationSection = configuration.GetSection("Cors");
            var origins = configurationSection.GetSection("Origins").Get<string[]>();
            var allowAll = configurationSection.GetSection("AllowAll").Get<bool>();

            app.UseCors(builder =>
            {
                if (allowAll)
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                }
                else if (origins != null && origins.Any())
                {
                    builder.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
                }
            });

            return app;
        }
    }
}