using Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionOhlcvGeneratorOutputExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = (SyntheticDataGeneratorOutput<Ohlcv>) new FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.Open1, example.Data[0].Open, "data[0] open");
            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.High1, example.Data[0].High, "data[0] high");
            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.Low1, example.Data[0].Low, "data[0] low");
            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.Close1, example.Data[0].Close, "data[0] close");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, example.Data[0].Volume, "data[0] volume");

            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.Open2, example.Data[1].Open, "data[1] open");
            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.High2, example.Data[1].High, "data[1] high");
            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.Low2, example.Data[1].Low, "data[1] low");
            Assert.AreEqual(FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider.Close2, example.Data[1].Close, "data[1] close");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, example.Data[1].Volume, "data[1] volume");
        }
    }
}
