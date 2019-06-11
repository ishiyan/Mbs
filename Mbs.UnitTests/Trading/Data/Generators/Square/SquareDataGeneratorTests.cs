using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Square;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Square
{
    [TestClass]
    public class SquareDataGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void SquareDataGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SquareOhlcvGenerator(new SquareOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.SquareAmplitude, generator.SampleAmplitude, "default sample amplitude");
            Assert.AreEqual(DefaultParameterValues.SquareMinimalValue, generator.SampleMinimum, "default sample minimum");
        }
    }
}
