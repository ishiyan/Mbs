using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionQuoteGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void FractionalBrownianMotionQuoteGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new FractionalBrownianMotionQuoteGenerator(new FractionalBrownianMotionQuoteGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.SpreadFraction, generator.SpreadFraction, "default spread fraction");
            Assert.AreEqual(DefaultParameterValues.AskSize, generator.AskSize, "default ask size");
            Assert.AreEqual(DefaultParameterValues.BidSize, generator.BidSize, "default bid size");
        }
    }
}
