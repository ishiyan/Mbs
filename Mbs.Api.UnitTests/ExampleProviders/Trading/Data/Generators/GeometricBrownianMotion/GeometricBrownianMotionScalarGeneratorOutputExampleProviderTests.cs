using Mbs.Api.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion
{
    [TestClass]
    public class GeometricBrownianMotionScalarGeneratorOutputExampleProviderTests
    {
        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = new GeometricBrownianMotionScalarGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(GeometricBrownianMotionScalarGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(GeometricBrownianMotionScalarGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");
            Assert.AreEqual(GeometricBrownianMotionScalarGeneratorOutputExampleProvider.Price1, example.Data[0].Value, "data[0] price");
            Assert.AreEqual(GeometricBrownianMotionScalarGeneratorOutputExampleProvider.Price2, example.Data[1].Value, "data[1] price");
        }
    }
}
