using System.Threading.Tasks;
using Mbs.Trading.Data.Generators.Chirp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpDataGeneratorExtensionsTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public async Task ChirpDataGeneratorExtensions_GenerateAsync_Ohlcv_CorrectLength()
        {
            const int length = 16;
            var parameters = new ChirpOhlcvGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task ChirpDataGeneratorExtensions_GenerateAsync_Quote_CorrectLength()
        {
            const int length = 16;
            var parameters = new ChirpQuoteGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task ChirpDataGeneratorExtensions_GenerateAsync_Trade_CorrectLength()
        {
            const int length = 16;
            var parameters = new ChirpTradeGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task ChirpDataGeneratorExtensions_GenerateAsync_Scalar_CorrectLength()
        {
            const int length = 16;
            var parameters = new ChirpScalarGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }
    }
}
