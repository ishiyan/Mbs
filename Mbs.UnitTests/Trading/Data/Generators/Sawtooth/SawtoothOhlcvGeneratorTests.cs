using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Sawtooth
{
    [TestClass]
    public class SawtoothOhlcvGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void SawtoothOhlcvGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SawtoothOhlcvGenerator(new SawtoothOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.CandlestickShadowFraction, generator.CandlestickShadowFraction, "default candlestick shadow fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickBodyFraction, generator.CandlestickBodyFraction, "default candlestick body fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, generator.Volume, "default volume");
        }
    }
}
