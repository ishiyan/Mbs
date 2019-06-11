using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Sawtooth
{
    [TestClass]
    public class SawtoothDataGeneratorTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void SawtoothDataGenerator_Construction_DefaultConstructor_PropertyValuesCorrect()
        {
            var generator = new SawtoothOhlcvGenerator(new SawtoothOhlcvGeneratorParameters());

            Assert.AreEqual(DefaultParameterValues.SawtoothAmplitude, generator.SampleAmplitude, "default sample amplitude");
            Assert.AreEqual(DefaultParameterValues.SawtoothMinimalValue, generator.SampleMinimum, "default sample minimum");
            Assert.AreEqual(DefaultParameterValues.SawtoothIsBiDirectional, generator.IsBiDirectional, "default is bi-directional");
            Assert.AreEqual(DefaultParameterValues.SawtoothShape, generator.Shape, "default shape");
        }
    }
}
