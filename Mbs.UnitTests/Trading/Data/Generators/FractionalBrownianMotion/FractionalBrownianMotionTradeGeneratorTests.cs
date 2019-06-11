using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionTradeGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void FractionalBrownianMotionTradeGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new FractionalBrownianMotionTradeGenerator(new FractionalBrownianMotionTradeGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.TradeVolume, generator.Volume, "default volume");
        }
    }
}
