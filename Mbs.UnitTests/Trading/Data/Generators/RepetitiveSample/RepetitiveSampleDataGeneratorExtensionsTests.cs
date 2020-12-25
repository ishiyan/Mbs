using System.Threading.Tasks;
using Mbs.Trading.Data.Generators.RepetitiveSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.RepetitiveSample
{
    [TestClass]
    public class RepetitiveSampleDataGeneratorExtensionsTests
    {
        [TestMethod]
        public async Task RepetitiveSampleDataGeneratorExtensions_GenerateAsync_Ohlcv_CorrectLength()
        {
            const int length = 16;
            var parameters = new RepetitiveSampleGeneratorParameters();

            var output = await parameters.GenerateOhlcvAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task RepetitiveSampleDataGeneratorExtensions_GenerateAsync_Quote_CorrectLength()
        {
            const int length = 16;
            var parameters = new RepetitiveSampleGeneratorParameters();

            var output = await parameters.GenerateQuoteAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task RepetitiveSampleDataGeneratorExtensions_GenerateAsync_Trade_CorrectLength()
        {
            const int length = 16;
            var parameters = new RepetitiveSampleGeneratorParameters();

            var output = await parameters.GenerateTradeAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task RepetitiveSampleDataGeneratorExtensions_GenerateAsync_Scalar_CorrectLength()
        {
            const int length = 16;
            var parameters = new RepetitiveSampleGeneratorParameters();

            var output = await parameters.GenerateScalarAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }
    }
}
