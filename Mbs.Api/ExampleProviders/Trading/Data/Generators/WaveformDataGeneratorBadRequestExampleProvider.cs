using Mbs.Api.Extensions.ExceptionHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators
{
    /// <inheritdoc />
    internal class WaveformDataGeneratorBadRequestExampleProvider : IExamplesProvider
    {
        /// <inheritdoc />
        public object GetExamples()
        {
            var dic = new ModelStateDictionary();
            dic.AddModelError("WaveformParameters.NoiseAmplitudeFraction", "The field NoiseAmplitudeFraction must be in range [0, 1].");

            return new InternalError(dic)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}
