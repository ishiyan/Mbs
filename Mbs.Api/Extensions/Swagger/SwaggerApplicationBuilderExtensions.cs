using Microsoft.AspNetCore.Builder;

namespace Mbs.Api.Extensions
{
    /// <summary>
    /// Registers MbsApi Swagger middleware in the Http pipeline.
    /// </summary>
    public static class SwaggerApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds the MbsApi Swagger middleware to the <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="app">An application builder instance.</param>
        /// <returns>An updated application builder instance.</returns>
        public static IApplicationBuilder UseMbsApiSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(SwaggerServiceCollectionExtensions.ConfigureSwaggerUi);

            return app;
        }
   }
}