using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.MultiSinusoidal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class MultiSinusoidalTradeGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void MultiSinusoidalTradeGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new MultiSinusoidalTradeGenerator(new MultiSinusoidalTradeGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.TradeVolume, generator.Volume, "default volume");
        }
    }
}
