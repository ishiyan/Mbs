using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Mbs.Api.Host.Ng.Extensions
{
    /// <summary>
    /// Adds the Angular routing middlewares to the <see cref="IApplicationBuilder"/> request execution pipeline.
    /// </summary>
    public static class AngularRoutingApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds the Angular routing middlewares to the <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="app">The application builder interface.</param>
        /// <returns>An updated application builder instance.</returns>
        public static IApplicationBuilder UseAngularRouting(this IApplicationBuilder app)
        {
            // To support Angular routing, we need to handle 404 errors.
            // In case a requested resource was not found on the server, it could be a Angular route.
            // This means we should redirect request, which results in a error 404, to the index.html.
            app.Use(async (context, next) =>
            {
                await next().ConfigureAwait(false);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/", StringComparison.Ordinal))
                {
                    context.Request.Path = "/";
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    await next().ConfigureAwait(false);
                }
            });

            return app;
        }
    }
}