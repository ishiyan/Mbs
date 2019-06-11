using System.Threading.Tasks;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionDataGeneratorExtensionsTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public async Task FractionalBrownianMotionDataGeneratorExtensions_GenerateAsync_Ohlcv_CorrectLength()
        {
            const int length = 16;
            var parameters = new FractionalBrownianMotionOhlcvGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task FractionalBrownianMotionDataGeneratorExtensions_GenerateAsync_Quote_CorrectLength()
        {
            const int length = 16;
            var parameters = new FractionalBrownianMotionQuoteGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task FractionalBrownianMotionDataGeneratorExtensions_GenerateAsync_Trade_CorrectLength()
        {
            const int length = 16;
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task FractionalBrownianMotionDataGeneratorExtensions_GenerateAsync_Scalar_CorrectLength()
        {
            const int length = 16;
            var parameters = new FractionalBrownianMotionScalarGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }
    }
}
