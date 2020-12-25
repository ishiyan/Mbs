using Mbs.Api.ExampleProviders.Trading.Data.Generators.Sawtooth;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.Sawtooth
{
    [TestClass]
    public class SawtoothOhlcvGeneratorOutputExampleProviderTests
    {
        [TestMethod]
        public void SawtoothOhlcvGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = new SawtoothOhlcvGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");

            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.Open1, example.Data[0].Open, "data[0] open");
            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.High1, example.Data[0].High, "data[0] high");
            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.Low1, example.Data[0].Low, "data[0] low");
            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.Close1, example.Data[0].Close, "data[0] close");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, example.Data[0].Volume, "data[0] volume");

            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.Open2, example.Data[1].Open, "data[1] open");
            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.High2, example.Data[1].High, "data[1] high");
            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.Low2, example.Data[1].Low, "data[1] low");
            Assert.AreEqual(SawtoothOhlcvGeneratorOutputExampleProvider.Close2, example.Data[1].Close, "data[1] close");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, example.Data[1].Volume, "data[1] volume");
        }
    }
}
