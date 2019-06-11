using Mbs.Api.ExampleProviders.Trading.Data.Generators.Sawtooth;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.Sawtooth
{
    [TestClass]
    public class SawtoothScalarGeneratorOutputExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void SawtoothScalarGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = (SyntheticDataGeneratorOutput<Scalar>) new SawtoothScalarGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(SawtoothScalarGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(SawtoothScalarGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");
            Assert.AreEqual(SawtoothScalarGeneratorOutputExampleProvider.Price1, example.Data[0].Value, "data[0] price");
            Assert.AreEqual(SawtoothScalarGeneratorOutputExampleProvider.Price2, example.Data[1].Value, "data[1] price");
        }
    }
}
