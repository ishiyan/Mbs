using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Sinusoidal
{
    [TestClass]
    public class SinusoidalDataGeneratorTests
    {
        [TestMethod]
        public void SinusoidalDataGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SinusoidalOhlcvGenerator(new SinusoidalOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.SinusoidalAmplitude, generator.SampleAmplitude, "default sample amplitude");
            Assert.AreEqual(DefaultParameterValues.SinusoidalMinimalValue, generator.SampleMinimum, "default sample minimum");
            Assert.AreEqual(DefaultParameterValues.SinusoidalPeriod, generator.Period, "default period");
            Assert.AreEqual(DefaultParameterValues.SinusoidalPhaseInPi, generator.PhaseInPi, "default sample minimum");
        }
    }
}
