using Mbs.Trading.Brokers.Slippages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Brokers.Slippages
{
    [TestClass]
    public class ZeroSlippageTests
    {
        [TestMethod]
        public void ZeroSlippage_Amount_WhenCalled_ReturnsZeroValue()
        {
            double[] prices = { 1.234, 12.34, 123.4 };
            double[] quantities = { 5.678, 56.78, 567.8 };

            var slippage = new ZeroSlippage();

            for (int i = 0; i < 3; ++i)
            {
                var value = slippage.Amount(null, prices[i], quantities[i]);
                Assert.AreEqual(0, value, $"{i}");
            }
        }

        [TestMethod]
        public void ZeroSlippage_Amount_WhenCalledFromInterface_ReturnsZeroValue()
        {
            const double price = 1.1;

            ISlippage slippage = new ZeroSlippage();

            var value = slippage.Amount(null, price, double.NaN);
            Assert.AreEqual(0, value);
        }
    }
}
