using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Chirp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpDataGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void ChirpDataGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new ChirpOhlcvGenerator(new ChirpOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.ChirpAmplitude, generator.SampleAmplitude, "default sample amplitude");
            Assert.AreEqual(DefaultParameterValues.ChirpMinimalValue, generator.SampleMinimum, "default sample minimum");
            Assert.AreEqual(DefaultParameterValues.ChirpInitialPeriod, generator.InitialPeriod, "default initial period");
            Assert.AreEqual(DefaultParameterValues.ChirpFinalPeriod, generator.FinalPeriod, "default final period");
            Assert.AreEqual(DefaultParameterValues.ChirpPhaseInPi, generator.PhaseInPi, "default phase in pi");
            Assert.AreEqual(DefaultParameterValues.ChirpSweep, generator.ChirpSweep, "default chirp sweep");
            Assert.AreEqual(DefaultParameterValues.ChirpIsBiDirectional, generator.IsBiDirectional, "default is bi-directional");
        }
    }
}
