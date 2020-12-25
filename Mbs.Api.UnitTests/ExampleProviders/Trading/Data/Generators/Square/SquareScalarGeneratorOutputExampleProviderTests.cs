using Mbs.Api.ExampleProviders.Trading.Data.Generators.Square;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.Square
{
    [TestClass]
    public class SquareScalarGeneratorOutputExampleProviderTests
    {
        [TestMethod]
        public void SquareScalarGeneratorOutputExampleProvider_GetExamples_CorrectValues()
        {
            var example = new SquareScalarGeneratorOutputExampleProvider().GetExamples();

            Assert.AreEqual(SquareScalarGeneratorOutputExampleProvider.Name, example.Name, "name");
            Assert.AreEqual(SquareScalarGeneratorOutputExampleProvider.Moniker, example.Moniker, "moniker");

            Assert.IsNotNull(example.Data, "data length");
            Assert.AreEqual(2, example.Data.Length, "data length");

            Assert.AreEqual(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), example.Data[0].Time, "data[0] time");
            Assert.AreEqual(SquareScalarGeneratorOutputExampleProvider.Price1, example.Data[0].Value, "data[0] price");
            Assert.AreEqual(SquareScalarGeneratorOutputExampleProvider.Price2, example.Data[1].Value, "data[1] price");
        }
    }
}
