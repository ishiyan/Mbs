using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionOhlcvGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void FractionalBrownianMotionOhlcvGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new FractionalBrownianMotionOhlcvGenerator(new FractionalBrownianMotionOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.CandlestickShadowFraction, generator.CandlestickShadowFraction, "default candlestick shadow fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickBodyFraction, generator.CandlestickBodyFraction, "default candlestick body fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, generator.Volume, "default volume");
        }
    }
}
