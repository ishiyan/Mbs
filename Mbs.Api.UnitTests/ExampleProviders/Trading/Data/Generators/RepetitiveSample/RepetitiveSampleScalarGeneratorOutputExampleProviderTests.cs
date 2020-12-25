using Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.RepetitiveSample
{
    [TestClass]
    public class RepetitiveSampleScalarGeneratorOutputExampleProviderTests
    {
        [TestMethod]
        public void RepetitiveSampleScalarGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = new RepetitiveSampleScalarGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(RepetitiveSampleScalarGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(RepetitiveSampleScalarGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");
            Assert.AreEqual(RepetitiveSampleScalarGeneratorOutputExampleProvider.Price1, example.Data[0].Value, "data[0] price");
            Assert.AreEqual(RepetitiveSampleScalarGeneratorOutputExampleProvider.Price2, example.Data[1].Value, "data[1] price");
        }
    }
}
