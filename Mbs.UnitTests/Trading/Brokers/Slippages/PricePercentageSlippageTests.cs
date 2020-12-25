using Mbs.Trading.Brokers.Slippages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Brokers.Slippages
{
    [TestClass]
    public class PricePercentageSlippageTests
    {
        [TestMethod]
        public void PricePercentageSlippage_Amount_WhenCalled_ReturnsCorrectValue()
        {
            double[] prices = { 1.234, 12.34, 123.4 };
            double[] quantities = { 5.678, 56.78, 567.8 };
            double[] percentages = { 0.1, 0.01, 0.001 };

            var slippage = new PricePercentageSlippage();

            for (int i = 0; i < 3; ++i)
            {
                slippage.Percentage = percentages[i];
                var value = slippage.Amount(null, prices[i], quantities[i]);
                Assert.AreEqual(prices[i] * percentages[i], value, $"{i}");
            }
        }

        [TestMethod]
        public void PricePercentageSlippage_Amount_WhenCalledFromInterface_ReturnsCorrectValue()
        {
            const double price = 1.1;
            const double percentage = 0.001;

            ISlippage slippage = new PricePercentageSlippage { Percentage = percentage };

            var value = slippage.Amount(null, price, double.NaN);
            Assert.AreEqual(price * percentage, value);
        }
    }
}
