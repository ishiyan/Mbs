using Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionQuoteGeneratorOutputExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void FractionalBrownianMotionQuoteGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = (SyntheticDataGeneratorOutput<Quote>) new FractionalBrownianMotionQuoteGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(FractionalBrownianMotionQuoteGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(FractionalBrownianMotionQuoteGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(FractionalBrownianMotionQuoteGeneratorOutputExampleProvider.BidPrice1, example.Data[0].BidPrice, "data[0] bid price");
            Assert.AreEqual(DefaultParameterValues.BidSize, example.Data[0].BidSize, "data[0] bid size");
            Assert.AreEqual(FractionalBrownianMotionQuoteGeneratorOutputExampleProvider.AskPrice1, example.Data[0].AskPrice, "data[0] ask price");
            Assert.AreEqual(DefaultParameterValues.AskSize, example.Data[0].AskSize, "data[0] ask size");

            Assert.AreEqual(FractionalBrownianMotionQuoteGeneratorOutputExampleProvider.BidPrice2, example.Data[1].BidPrice, "data[1] bid price");
            Assert.AreEqual(DefaultParameterValues.BidSize, example.Data[1].BidSize, "data[0] bid size");
            Assert.AreEqual(FractionalBrownianMotionQuoteGeneratorOutputExampleProvider.AskPrice2, example.Data[1].AskPrice, "data[1] ask price");
            Assert.AreEqual(DefaultParameterValues.AskSize, example.Data[1].AskSize, "data[1] ask size");
        }
    }
}
