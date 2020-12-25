using Mbs.Api.ExampleProviders.Trading.Data.Generators;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators
{
    [TestClass]
    public class WaveformDataGeneratorBadRequestExampleProviderTests
    {
        [TestMethod]
        public void WaveformDataGeneratorBadRequestExampleProvider_GetExamples_CorrectValues()
        {
            var example = new WaveformDataGeneratorBadRequestExampleProvider().GetExamples();

            Assert.AreEqual(StatusCodes.Status400BadRequest, example.StatusCode, "status code");
        }
    }
}
