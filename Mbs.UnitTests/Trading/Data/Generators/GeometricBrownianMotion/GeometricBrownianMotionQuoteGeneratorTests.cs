using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.GeometricBrownianMotion
{
    [TestClass]
    public class GeometricBrownianMotionQuoteGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void GeometricBrownianMotionQuoteGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new GeometricBrownianMotionQuoteGenerator(new GeometricBrownianMotionQuoteGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.SpreadFraction, generator.SpreadFraction, "default spread fraction");
            Assert.AreEqual(DefaultParameterValues.AskSize, generator.AskSize, "default ask size");
            Assert.AreEqual(DefaultParameterValues.BidSize, generator.BidSize, "default bid size");
        }
    }
}
