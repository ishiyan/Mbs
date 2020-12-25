using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Chirp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpQuoteGeneratorTests
    {
        [TestMethod]
        public void ChirpQuoteGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new ChirpQuoteGenerator(new ChirpQuoteGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.SpreadFraction, generator.SpreadFraction, "default spread fraction");
            Assert.AreEqual(DefaultParameterValues.AskSize, generator.AskSize, "default ask size");
            Assert.AreEqual(DefaultParameterValues.BidSize, generator.BidSize, "default bid size");
        }
    }
}
