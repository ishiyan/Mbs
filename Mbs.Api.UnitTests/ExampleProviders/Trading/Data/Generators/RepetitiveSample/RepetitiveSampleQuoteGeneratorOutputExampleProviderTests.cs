using Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.RepetitiveSample
{
    [TestClass]
    public class RepetitiveSampleQuoteGeneratorOutputExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void RepetitiveSampleQuoteGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = (SyntheticDataGeneratorOutput<Quote>) new RepetitiveSampleQuoteGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.BidPrice1, example.Data[0].BidPrice, "data[0] bid price");
            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.BidSize1, example.Data[0].BidSize, "data[0] bid size");
            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.AskPrice1, example.Data[0].AskPrice, "data[0] ask price");
            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.AskSize1, example.Data[0].AskSize, "data[0] ask size");

            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.BidPrice2, example.Data[1].BidPrice, "data[1] bid price");
            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.BidSize2, example.Data[1].BidSize, "data[0] bid size");
            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.AskPrice2, example.Data[1].AskPrice, "data[1] ask price");
            Assert.AreEqual(RepetitiveSampleQuoteGeneratorOutputExampleProvider.AskSize2, example.Data[1].AskSize, "data[1] ask size");
        }
    }
}
