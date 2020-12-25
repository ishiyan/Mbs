using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Sinusoidal
{
    [TestClass]
    public class SinusoidalTradeGeneratorTests
    {
        [TestMethod]
        public void SinusoidalTradeGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SinusoidalTradeGenerator(new SinusoidalTradeGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.TradeVolume, generator.Volume, "default volume");
        }
    }
}
