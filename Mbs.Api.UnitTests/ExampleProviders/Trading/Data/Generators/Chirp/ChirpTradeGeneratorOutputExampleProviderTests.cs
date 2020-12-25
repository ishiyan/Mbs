using Mbs.Api.ExampleProviders.Trading.Data.Generators.Chirp;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpTradeGeneratorOutputExampleProviderTests
    {
        [TestMethod]
        public void ChirpTradeGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = new ChirpTradeGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(ChirpTradeGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(ChirpTradeGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(ChirpTradeGeneratorOutputExampleProvider.Price1, example.Data[0].Price, "data[0] price");
            Assert.AreEqual(DefaultParameterValues.TradeVolume, example.Data[0].Volume, "data[0] volume");

            Assert.AreEqual(ChirpTradeGeneratorOutputExampleProvider.Price2, example.Data[1].Price, "data[1] price");
            Assert.AreEqual(DefaultParameterValues.TradeVolume, example.Data[1].Volume, "data[1] volume");
        }
    }
}
