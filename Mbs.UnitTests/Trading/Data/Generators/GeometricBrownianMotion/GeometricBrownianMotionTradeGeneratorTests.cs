using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.GeometricBrownianMotion
{
    [TestClass]
    public class GeometricBrownianMotionTradeGeneratorTests
    {
        [TestMethod]
        public void GeometricBrownianMotionTradeGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new GeometricBrownianMotionTradeGenerator(new GeometricBrownianMotionTradeGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.TradeVolume, generator.Volume, "default volume");
        }
    }
}
