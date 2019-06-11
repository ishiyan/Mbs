using Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionScalarGeneratorOutputExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void FractionalBrownianMotionScalarGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = (SyntheticDataGeneratorOutput<Scalar>) new FractionalBrownianMotionScalarGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(FractionalBrownianMotionScalarGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(FractionalBrownianMotionScalarGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");
            Assert.AreEqual(FractionalBrownianMotionScalarGeneratorOutputExampleProvider.Price1, example.Data[0].Value, "data[0] price");
            Assert.AreEqual(FractionalBrownianMotionScalarGeneratorOutputExampleProvider.Price2, example.Data[1].Value, "data[1] price");
        }
    }
}
