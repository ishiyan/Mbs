using Mbs.Api.ExampleProviders.Trading.Data.Generators.MultiSinusoidal;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class MultiSinusoidalScalarGeneratorOutputExampleProviderTests
    {
        [TestMethod]
        public void MultiSinusoidalScalarGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = new MultiSinusoidalScalarGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(MultiSinusoidalScalarGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(MultiSinusoidalScalarGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");
            Assert.AreEqual(MultiSinusoidalScalarGeneratorOutputExampleProvider.Price1, example.Data[0].Value, "data[0] price");
            Assert.AreEqual(MultiSinusoidalScalarGeneratorOutputExampleProvider.Price2, example.Data[1].Value, "data[1] price");
        }
    }
}
