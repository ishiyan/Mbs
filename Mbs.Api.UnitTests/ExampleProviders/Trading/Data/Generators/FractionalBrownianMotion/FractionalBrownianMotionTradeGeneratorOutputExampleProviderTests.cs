using Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionTradeGeneratorOutputExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = (SyntheticDataGeneratorOutput<Trade>) new FractionalBrownianMotionTradeGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(FractionalBrownianMotionTradeGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(FractionalBrownianMotionTradeGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(FractionalBrownianMotionTradeGeneratorOutputExampleProvider.Price1, example.Data[0].Price, "data[0] price");
            Assert.AreEqual(DefaultParameterValues.TradeVolume, example.Data[0].Volume, "data[0] volume");

            Assert.AreEqual(FractionalBrownianMotionTradeGeneratorOutputExampleProvider.Price2, example.Data[1].Price, "data[1] price");
            Assert.AreEqual(DefaultParameterValues.TradeVolume, example.Data[1].Volume, "data[1] volume");
        }
    }
}
