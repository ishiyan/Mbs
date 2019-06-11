using Mbs.Api.ExampleProviders.Trading.Data.Generators;
using Mbs.Api.Extensions.ExceptionHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class WaveformDataGeneratorBadRequestExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void WaveformDataGeneratorBadRequestExampleProvider_GetExamples_CorrectValues()
        {
            var example = (InternalError) new WaveformDataGeneratorBadRequestExampleProvider().GetExamples();

            Assert.AreEqual(StatusCodes.Status400BadRequest, example.StatusCode, "status code");
        }
    }
}
