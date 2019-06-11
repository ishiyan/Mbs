using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Square;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Square
{
    [TestClass]
    public class SquareTradeGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void SquareTradeGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SquareTradeGenerator(new SquareTradeGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.TradeVolume, generator.Volume, "default volume");
        }
    }
}
