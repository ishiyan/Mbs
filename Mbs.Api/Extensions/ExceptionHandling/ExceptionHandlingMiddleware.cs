using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Mbs.Api.Extensions.ExceptionHandling
{
    /// <summary>
    /// A middleware to handle exceptions.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ExceptionHandlingMiddleware
    {
        private const string InternalServerErrorMessage = "An internal server error occurred. ";
        private const string TimeoutMessage = "A request timeout occurred. ";
        private const string ApplicationJson = "application/json";

        private readonly IHostingEnvironment env;
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next handler in the chain.</param>
        /// <param name="env">A hosting environment.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        /// <summary>
        /// Invokes this handler.
        /// </summary>
        /// <param name="context">Th HTTP context.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var error = new InternalError();
            if (IsTimeOut(ex))
            {
                error.StatusCode = StatusCodes.Status408RequestTimeout;
                error.Message = TimeoutMessage;
            }
            else
            {
                error.StatusCode = StatusCodes.Status500InternalServerError;
                error.Message = InternalServerErrorMessage;
            }

            error.Message += ex.Message;
            if (env.IsDevelopment())
            {
                error.Details = new[] { InnerException(ex) };
            }

            context.Response.ContentType = ApplicationJson;
            context.Response.StatusCode = error.StatusCode;

            var result = JsonConvert.SerializeObject(error);
            await context.Response.WriteAsync(result);
        }

        private static bool IsTimeOut(Exception exception)
        {
            if (exception is WebException webException)
            {
                return webException.Status == WebExceptionStatus.Timeout;
            }

            return exception is TimeoutException;
        }

        private static InnerError InnerException(Exception ex)
        {
            return ex.InnerException == null ? null : new InnerError
            {
                Message = ex.InnerException.Message,
                Details = InnerException(ex.InnerException)
            };
        }
    }
}