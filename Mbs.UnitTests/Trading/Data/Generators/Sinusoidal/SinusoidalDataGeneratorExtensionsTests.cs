using System.Threading.Tasks;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Sinusoidal
{
    [TestClass]
    public class SinusoidalDataGeneratorExtensionsTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public async Task SinusoidalDataGeneratorExtensions_GenerateAsync_Ohlcv_CorrectLength()
        {
            const int length = 16;
            var parameters = new SinusoidalOhlcvGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task SinusoidalDataGeneratorExtensions_GenerateAsync_Quote_CorrectLength()
        {
            const int length = 16;
            var parameters = new SinusoidalQuoteGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task SinusoidalDataGeneratorExtensions_GenerateAsync_Trade_CorrectLength()
        {
            const int length = 16;
            var parameters = new SinusoidalTradeGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task SinusoidalDataGeneratorExtensions_GenerateAsync_Scalar_CorrectLength()
        {
            const int length = 16;
            var parameters = new SinusoidalScalarGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }
    }
}
