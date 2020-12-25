using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.MultiSinusoidal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class MultiSinusoidalDataGeneratorTests
    {
        [TestMethod]
        public void MultiSinusoidalDataGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new MultiSinusoidalOhlcvGenerator(new MultiSinusoidalOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.MultiSinusoidalMinimalValue, generator.SampleMinimum, "default sample minimum");
            Assert.AreEqual(1, generator.Count, "default component count");
            Assert.IsNotNull(generator.Amplitudes, "default component amplitudes is not null");
            Assert.IsNotNull(generator.Periods, "default component periods is not null");
            Assert.IsNotNull(generator.PhasesInPi, "default component phases in pi is not null");
            Assert.AreEqual(1, generator.Amplitudes.Count, "default component amplitudes count");
            Assert.AreEqual(1, generator.Periods.Count, "default component periods count");
            Assert.AreEqual(1, generator.PhasesInPi.Count, "default component phases in pi count");
        }
    }
}
