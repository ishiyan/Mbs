using Mbs.Api.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion
{
    [TestClass]
    public class GeometricBrownianMotionTradeGeneratorOutputExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void GeometricBrownianMotionTradeGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = (SyntheticDataGeneratorOutput<Trade>) new GeometricBrownianMotionTradeGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(GeometricBrownianMotionTradeGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(GeometricBrownianMotionTradeGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(GeometricBrownianMotionTradeGeneratorOutputExampleProvider.Price1, example.Data[0].Price, "data[0] price");
            Assert.AreEqual(DefaultParameterValues.TradeVolume, example.Data[0].Volume, "data[0] volume");

            Assert.AreEqual(GeometricBrownianMotionTradeGeneratorOutputExampleProvider.Price2, example.Data[1].Price, "data[1] price");
            Assert.AreEqual(DefaultParameterValues.TradeVolume, example.Data[1].Volume, "data[1] volume");
        }
    }
}
