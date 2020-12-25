using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Chirp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpOhlcvGeneratorTests
    {
        [TestMethod]
        public void ChirpOhlcvGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new ChirpOhlcvGenerator(new ChirpOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.CandlestickShadowFraction, generator.CandlestickShadowFraction, "default candlestick shadow fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickBodyFraction, generator.CandlestickBodyFraction, "default candlestick body fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, generator.Volume, "default volume");
        }
    }
}
