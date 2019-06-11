using System;
using Mbs.Trading.Currencies;
using Mbs.Trading.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Orders
{
    [TestClass]
    public class SingleOrderReportTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void SingleOrderReport_TransactionTime_WhenConstructed_GetsCorrectValue()
        {
            var expectedValue = DateTime.Now;

            var target = new SingleOrderReport(expectedValue, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(expectedValue, target.TransactionTime);
        }

        [TestMethod]
        public void SingleOrderReport_Text_WhenConstructed_GetsCorrectValue()
        {
            const string expectedValue = "text";
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, expectedValue);
            Assert.AreEqual(expectedValue, target.Text);
        }

        [TestMethod]
        public void SingleOrderReport_Status_WhenConstructed_GetsCorrectValue()
        {
            const OrderStatus expectedValue = OrderStatus.Filled;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, expectedValue, string.Empty);
            Assert.AreEqual(expectedValue, target.Status);
        }

        [TestMethod]
        public void SingleOrderReport_ReportType_WhenConstructed_GetsCorrectValue()
        {
            const OrderReportType expectedValue = OrderReportType.CancelRejected;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, expectedValue, OrderStatus.New, string.Empty);
            Assert.AreEqual(expectedValue, target.ReportType);
        }

        [TestMethod]
        public void SingleOrderReport_ReportId_WhenConstructed_GetsCorrectValue()
        {
            const string expectedValue = "id";
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, expectedValue, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(expectedValue, target.ReportId);
        }

        [TestMethod]
        public void SingleOrderReport_ReplaceTargetOrder_WhenConstructed_GetsCorrectValue()
        {
            var expectedValue = new SingleOrder();
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.IsNull(target.ReplaceTargetOrder);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, 5d, 6d, CurrencyCode.Nok);
            Assert.IsNull(target.ReplaceTargetOrder);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, null, expectedValue);
            Assert.AreEqual(expectedValue, target.ReplaceTargetOrder);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, 5d, 6d, CurrencyCode.Nok, null, expectedValue);
            Assert.AreEqual(expectedValue, target.ReplaceTargetOrder);
        }

        [TestMethod]
        public void SingleOrderReport_ReplaceSourceOrder_WhenConstructed_GetsCorrectValue()
        {
            var expectedValue = new SingleOrder();
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.IsNull(target.ReplaceSourceOrder);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, 5d, 6d, CurrencyCode.Nok);
            Assert.IsNull(target.ReplaceSourceOrder);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, expectedValue, null);
            Assert.AreEqual(expectedValue, target.ReplaceSourceOrder);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, 5d, 6d, CurrencyCode.Nok, expectedValue, null);
            Assert.AreEqual(expectedValue, target.ReplaceSourceOrder);
        }

        [TestMethod]
        public void SingleOrderReport_LeavesQuantity_WhenConstructed_GetsCorrectValue()
        {
            const double defaultValue = 0d;
            const double expectedValue = 42d;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(defaultValue, target.LeavesQuantity);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, null, null);
            Assert.AreEqual(defaultValue, target.LeavesQuantity);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, expectedValue, 3d, 4d, 5d, 6d, CurrencyCode.Nok);
            Assert.AreEqual(expectedValue, target.LeavesQuantity);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, expectedValue, 3d, 4d, 5d, 6d, CurrencyCode.Nok, null, null);
            Assert.AreEqual(expectedValue, target.LeavesQuantity);
        }

        [TestMethod]
        public void SingleOrderReport_LastQuantity_WhenConstructed_GetsCorrectValue()
        {
            const double defaultValue = 0d;
            const double expectedValue = 42d;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(defaultValue, target.LastQuantity);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, null, null);
            Assert.AreEqual(defaultValue, target.LastQuantity);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, expectedValue, 2d, 3d, 4d, 5d, 6d, CurrencyCode.Nok);
            Assert.AreEqual(expectedValue, target.LastQuantity);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, expectedValue, 2d, 3d, 4d, 5d, 6d, CurrencyCode.Nok, null, null);
            Assert.AreEqual(expectedValue, target.LastQuantity);
        }

        [TestMethod]
        public void SingleOrderReport_LastPrice_WhenConstructed_GetsCorrectValue()
        {
            const double defaultValue = 0d;
            const double expectedValue = 42d;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(defaultValue, target.LastPrice);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, null, null);
            Assert.AreEqual(defaultValue, target.LastPrice);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, expectedValue, 1d, 2d, 3d, 4d, 5d, 6d, CurrencyCode.Nok);
            Assert.AreEqual(expectedValue, target.LastPrice);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, expectedValue, 1d, 2d, 3d, 4d, 5d, 6d, CurrencyCode.Nok, null, null);
            Assert.AreEqual(expectedValue, target.LastPrice);
        }

        [TestMethod]
        public void SingleOrderReport_LastCommission_WhenConstructed_GetsCorrectValue()
        {
            const double defaultValue = 0d;
            const double expectedValue = 42d;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(defaultValue, target.LastCommission);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, null, null);
            Assert.AreEqual(defaultValue, target.LastCommission);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, expectedValue, 6d, CurrencyCode.Nok);
            Assert.AreEqual(expectedValue, target.LastCommission);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, expectedValue, 6d, CurrencyCode.Nok, null, null);
            Assert.AreEqual(expectedValue, target.LastCommission);
        }

        [TestMethod]
        public void SingleOrderReport_CommissionCurrency_WhenConstructed_GetsCorrectValue()
        {
            const CurrencyCode defaultValue = CurrencyCode.Xxx;
            const CurrencyCode expectedValue = CurrencyCode.Sek;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(defaultValue, target.CommissionCurrency);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, null, null);
            Assert.AreEqual(defaultValue, target.CommissionCurrency);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, 5d, 6d, expectedValue);
            Assert.AreEqual(expectedValue, target.CommissionCurrency);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, 5d, 6d, expectedValue, null, null);
            Assert.AreEqual(expectedValue, target.CommissionCurrency);
        }

        [TestMethod]
        public void SingleOrderReport_CumulativeQuantity_WhenConstructed_GetsCorrectValue()
        {
            const double defaultValue = 0d;
            const double expectedValue = 42d;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(defaultValue, target.CumulativeQuantity);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, null, null);
            Assert.AreEqual(defaultValue, target.CumulativeQuantity);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, expectedValue, 4d, 5d, 6d, CurrencyCode.Nok);
            Assert.AreEqual(expectedValue, target.CumulativeQuantity);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, expectedValue, 4d, 5d, 6d, CurrencyCode.Nok, null, null);
            Assert.AreEqual(expectedValue, target.CumulativeQuantity);
        }

        [TestMethod]
        public void SingleOrderReport_CumulativeCommission_WhenConstructed_GetsCorrectValue()
        {
            const double defaultValue = 0d;
            const double expectedValue = 42d;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(defaultValue, target.CumulativeCommission);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, null, null);
            Assert.AreEqual(defaultValue, target.CumulativeCommission);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, 5d, expectedValue, CurrencyCode.Nok);
            Assert.AreEqual(expectedValue, target.CumulativeCommission);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, 4d, 5d, expectedValue, CurrencyCode.Nok, null, null);
            Assert.AreEqual(expectedValue, target.CumulativeCommission);
        }

        [TestMethod]
        public void SingleOrderReport_AveragePrice_WhenConstructed_GetsCorrectValue()
        {
            const double defaultValue = 0d;
            const double expectedValue = 42d;
            var dateTime = DateTime.Now;

            var target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty);
            Assert.AreEqual(defaultValue, target.AveragePrice);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, null, null);
            Assert.AreEqual(defaultValue, target.AveragePrice);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, expectedValue, 5d, 6d, CurrencyCode.Nok);
            Assert.AreEqual(expectedValue, target.AveragePrice);

            target = new SingleOrderReport(dateTime, string.Empty, OrderReportType.New, OrderStatus.New, string.Empty, 0d, 1d, 2d, 3d, expectedValue, 5d, 6d, CurrencyCode.Nok, null, null);
            Assert.AreEqual(expectedValue, target.AveragePrice);
        }
    }
}
