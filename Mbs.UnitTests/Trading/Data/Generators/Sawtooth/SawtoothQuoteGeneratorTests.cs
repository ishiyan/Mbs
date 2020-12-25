using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Sawtooth
{
    [TestClass]
    public class SawtoothQuoteGeneratorTests
    {
        [TestMethod]
        public void SawtoothQuoteGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SawtoothQuoteGenerator(new SawtoothQuoteGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.SpreadFraction, generator.SpreadFraction, "default spread fraction");
            Assert.AreEqual(DefaultParameterValues.AskSize, generator.AskSize, "default ask size");
            Assert.AreEqual(DefaultParameterValues.BidSize, generator.BidSize, "default bid size");
        }
    }
}
