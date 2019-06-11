using Mbs.Api.ExampleProviders.Trading.Data.Generators.MultiSinusoidal;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class MultiSinusoidalOhlcvGeneratorOutputExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void MultiSinusoidalOhlcvGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = (SyntheticDataGeneratorOutput<Ohlcv>) new MultiSinusoidalOhlcvGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.Open1, example.Data[0].Open, "data[0] open");
            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.High1, example.Data[0].High, "data[0] high");
            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.Low1, example.Data[0].Low, "data[0] low");
            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.Close1, example.Data[0].Close, "data[0] close");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, example.Data[0].Volume, "data[0] volume");

            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.Open2, example.Data[1].Open, "data[1] open");
            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.High2, example.Data[1].High, "data[1] high");
            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.Low2, example.Data[1].Low, "data[1] low");
            Assert.AreEqual(MultiSinusoidalOhlcvGeneratorOutputExampleProvider.Close2, example.Data[1].Close, "data[1] close");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, example.Data[1].Volume, "data[1] volume");
        }
    }
}
