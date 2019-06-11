using System.Threading.Tasks;
using Mbs.Trading.Data.Generators.Sawtooth;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.Sawtooth
{
    [TestClass]
    public class SawtoothDataGeneratorExtensionsTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public async Task SawtoothDataGeneratorExtensions_GenerateAsync_Ohlcv_CorrectLength()
        {
            const int length = 16;
            var parameters = new SawtoothOhlcvGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task SawtoothDataGeneratorExtensions_GenerateAsync_Quote_CorrectLength()
        {
            const int length = 16;
            var parameters = new SawtoothQuoteGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task SawtoothDataGeneratorExtensions_GenerateAsync_Trade_CorrectLength()
        {
            const int length = 16;
            var parameters = new SawtoothTradeGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task SawtoothDataGeneratorExtensions_GenerateAsync_Scalar_CorrectLength()
        {
            const int length = 16;
            var parameters = new SawtoothScalarGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }
    }
}
