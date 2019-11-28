using Microsoft.AspNetCore.Builder;

namespace Mbs.Api.Extensions.ExceptionHandling
{
    /// <summary>
    /// Adds <see cref="ExceptionHandlingMiddleware"/> to the <see cref="IApplicationBuilder"/> request execution pipeline.
    /// </summary>
    public static class ExceptionHandlingApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds the MbsApi <see cref="ExceptionHandlingMiddleware"/> to the <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="app">The application builder interface.</param>
        /// <returns>Returns the application builder interface.</returns>
        public static IApplicationBuilder UseMbsApiExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
