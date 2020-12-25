using System.Threading.Tasks;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators.GeometricBrownianMotion
{
    [TestClass]
    public class GeometricBrownianMotionDataGeneratorExtensionsTests
    {
        [TestMethod]
        public async Task GeometricBrownianMotionDataGeneratorExtensions_GenerateAsync_Ohlcv_CorrectLength()
        {
            const int length = 16;
            var parameters = new GeometricBrownianMotionOhlcvGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task GeometricBrownianMotionDataGeneratorExtensions_GenerateAsync_Quote_CorrectLength()
        {
            const int length = 16;
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task GeometricBrownianMotionDataGeneratorExtensions_GenerateAsync_Trade_CorrectLength()
        {
            const int length = 16;
            var parameters = new GeometricBrownianMotionTradeGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }

        [TestMethod]
        public async Task GeometricBrownianMotionDataGeneratorExtensions_GenerateAsync_Scalar_CorrectLength()
        {
            const int length = 16;
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters();

            var output = await parameters.GenerateAsync(length);

            Assert.IsNotNull(output.Data, "data is not null");
            Assert.AreEqual(length, output.Data.Length, "data length is correct");
        }
    }
}
