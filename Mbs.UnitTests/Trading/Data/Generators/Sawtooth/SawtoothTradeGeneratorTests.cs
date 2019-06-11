using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Sawtooth
{
    [TestClass]
    public class SawtoothTradeGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void SawtoothTradeGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SawtoothTradeGenerator(new SawtoothTradeGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.TradeVolume, generator.Volume, "default volume");
        }
    }
}
