using Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.RepetitiveSample
{
    [TestClass]
    public class RepetitiveSampleTradeGeneratorOutputExampleProviderTests
    {
        [TestMethod]
        public void RepetitiveSampleTradeGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = new RepetitiveSampleTradeGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(RepetitiveSampleTradeGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(RepetitiveSampleTradeGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(RepetitiveSampleTradeGeneratorOutputExampleProvider.Price1, example.Data[0].Price, "data[0] price");
            Assert.AreEqual(RepetitiveSampleTradeGeneratorOutputExampleProvider.Volume1, example.Data[0].Volume, "data[0] volume");

            Assert.AreEqual(RepetitiveSampleTradeGeneratorOutputExampleProvider.Price2, example.Data[1].Price, "data[1] price");
            Assert.AreEqual(RepetitiveSampleTradeGeneratorOutputExampleProvider.Volume2, example.Data[1].Volume, "data[1] volume");
        }
    }
}
