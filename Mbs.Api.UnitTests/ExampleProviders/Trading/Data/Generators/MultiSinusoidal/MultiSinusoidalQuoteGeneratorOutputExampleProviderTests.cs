using Mbs.Api.ExampleProviders.Trading.Data.Generators.MultiSinusoidal;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class MultiSinusoidalQuoteGeneratorOutputExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void MultiSinusoidalQuoteGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = (SyntheticDataGeneratorOutput<Quote>) new MultiSinusoidalQuoteGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(MultiSinusoidalQuoteGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(MultiSinusoidalQuoteGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(MultiSinusoidalQuoteGeneratorOutputExampleProvider.BidPrice1, example.Data[0].BidPrice, "data[0] bid price");
            Assert.AreEqual(DefaultParameterValues.BidSize, example.Data[0].BidSize, "data[0] bid size");
            Assert.AreEqual(MultiSinusoidalQuoteGeneratorOutputExampleProvider.AskPrice1, example.Data[0].AskPrice, "data[0] ask price");
            Assert.AreEqual(DefaultParameterValues.AskSize, example.Data[0].AskSize, "data[0] ask size");

            Assert.AreEqual(MultiSinusoidalQuoteGeneratorOutputExampleProvider.BidPrice2, example.Data[1].BidPrice, "data[1] bid price");
            Assert.AreEqual(DefaultParameterValues.BidSize, example.Data[1].BidSize, "data[0] bid size");
            Assert.AreEqual(MultiSinusoidalQuoteGeneratorOutputExampleProvider.AskPrice2, example.Data[1].AskPrice, "data[1] ask price");
            Assert.AreEqual(DefaultParameterValues.AskSize, example.Data[1].AskSize, "data[1] ask size");
        }
    }
}
