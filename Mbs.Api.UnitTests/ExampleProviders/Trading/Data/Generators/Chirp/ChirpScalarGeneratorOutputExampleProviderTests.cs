using Mbs.Api.ExampleProviders.Trading.Data.Generators.Chirp;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpScalarGeneratorOutputExampleProviderTests
    {
        [TestMethod]
        public void ChirpScalarGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = new ChirpScalarGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(ChirpScalarGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(ChirpScalarGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");
            Assert.AreEqual(ChirpScalarGeneratorOutputExampleProvider.Price1, example.Data[0].Value, "data[0] price");
            Assert.AreEqual(ChirpScalarGeneratorOutputExampleProvider.Price2, example.Data[1].Value, "data[1] price");
        }
    }
}
