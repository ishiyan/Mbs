using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Sinusoidal
{
    [TestClass]
    public class SinusoidalOhlcvGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void SinusoidalOhlcvGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SinusoidalOhlcvGenerator(new SinusoidalOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.CandlestickShadowFraction, generator.CandlestickShadowFraction, "default candlestick shadow fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickBodyFraction, generator.CandlestickBodyFraction, "default candlestick body fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, generator.Volume, "default volume");
        }
    }
}
