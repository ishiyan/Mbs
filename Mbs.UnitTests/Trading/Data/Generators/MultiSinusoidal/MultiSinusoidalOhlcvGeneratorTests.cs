using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.MultiSinusoidal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class MultiSinusoidalOhlcvGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void MultiSinusoidalOhlcvGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new MultiSinusoidalOhlcvGenerator(new MultiSinusoidalOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.CandlestickShadowFraction, generator.CandlestickShadowFraction, "default candlestick shadow fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickBodyFraction, generator.CandlestickBodyFraction, "default candlestick body fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, generator.Volume, "default volume");
        }
    }
}
