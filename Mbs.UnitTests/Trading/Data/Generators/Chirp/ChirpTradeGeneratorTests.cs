using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Chirp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpTradeGeneratorTests
    {
        [TestMethod]
        public void ChirpTradeGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new ChirpTradeGenerator(new ChirpTradeGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.TradeVolume, generator.Volume, "default volume");
        }
    }
}
