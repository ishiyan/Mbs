using Mbs.Api.ExampleProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders
{
    [TestClass]
    public class InternalErrorExampleProviderTests
    {
        [TestMethod]
        public void InternalErrorExampleProvider_GetExamples_CorrectValues()
        {
            var example = new InternalErrorExampleProvider().GetExamples();

            Assert.AreEqual(StatusCodes.Status500InternalServerError, example.StatusCode, "status code");
            Assert.AreEqual("An internal server error occurred. Not enough memory available.", example.Message, "message");
        }
    }
}
