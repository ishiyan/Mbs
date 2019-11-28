using Mbs.Api.Extensions.ExceptionHandling;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders
{
    /// <inheritdoc />
    internal class InternalErrorExampleProvider : IExamplesProvider<InternalError>
    {
        /// <inheritdoc />
        public InternalError GetExamples()
        {
            return new InternalError()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "An internal server error occurred. Not enough memory available."
            };
        }
    }
}
