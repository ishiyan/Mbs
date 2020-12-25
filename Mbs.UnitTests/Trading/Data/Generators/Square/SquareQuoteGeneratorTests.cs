﻿using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Square;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Square
{
    [TestClass]
    public class SquareQuoteGeneratorTests
    {
        [TestMethod]
        public void SquareQuoteGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SquareQuoteGenerator(new SquareQuoteGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.SpreadFraction, generator.SpreadFraction, "default spread fraction");
            Assert.AreEqual(DefaultParameterValues.AskSize, generator.AskSize, "default ask size");
            Assert.AreEqual(DefaultParameterValues.BidSize, generator.BidSize, "default bid size");
        }
    }
}
