using System;
using Mbs.Trading.Currencies;
using Mbs.Trading.Data;
using Mbs.Trading.Instruments;
using Mbs.Trading.Orders;
using Mbs.Trading.Portfolios;
using Mbs.Trading.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Orders
{
    [TestClass]
    public class SingleOrderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void SingleOrder_Type_WhenSet_GetsCorrectValue()
        {
            const OrderType defaultValue = OrderType.Market;
            const OrderType expectedValue = OrderType.TrailingStop;

            var target = new SingleOrder();
            Assert.AreEqual(defaultValue, target.Type);

            target.Type = expectedValue;
            Assert.AreEqual(expectedValue, target.Type);
        }

        [TestMethod]
        public void SingleOrder_TrailingDistance_WhenSet_GetsCorrectValue()
        {
            const double expectedValue = 9.9;

            var target = new SingleOrder();
            Assert.IsTrue(double.IsNaN(target.TrailingDistance));

            target.TrailingDistance = expectedValue;
            Assert.AreEqual(expectedValue, target.TrailingDistance);
        }

        [TestMethod]
        public void SingleOrder_TimeInForce_WhenSet_GetsCorrectValue()
        {
            const OrderTimeInForce defaultValue = OrderTimeInForce.Day;
            const OrderTimeInForce expectedValue = OrderTimeInForce.FillOrKill;

            var target = new SingleOrder();
            Assert.AreEqual(defaultValue, target.TimeInForce);

            target.TimeInForce = expectedValue;
            Assert.AreEqual(expectedValue, target.TimeInForce);
        }

        [TestMethod]
        public void SingleOrder_Text_WhenSet_GetsCorrectValue()
        {
            const string expectedValue = "foobar";

            var target = new SingleOrder();
            Assert.IsNull(target.Text);

            target.Text = expectedValue;
            Assert.AreEqual(expectedValue, target.Text);
        }

        [TestMethod]
        public void SingleOrder_StopPrice_WhenSet_GetsCorrectValue()
        {
            const double expectedValue = 9.9;

            var target = new SingleOrder();
            Assert.IsTrue(double.IsNaN(target.StopPrice));

            target.StopPrice = expectedValue;
            Assert.AreEqual(expectedValue, target.StopPrice);
        }

        [TestMethod]
        public void SingleOrder_Side_WhenSet_GetsCorrectValue()
        {
            const OrderSide defaultValue = OrderSide.Buy;
            const OrderSide expectedValue = OrderSide.SellShort;

            var target = new SingleOrder();
            Assert.AreEqual(defaultValue, target.Side);

            target.Side = expectedValue;
            Assert.AreEqual(expectedValue, target.Side);
        }

        [TestMethod]
        public void SingleOrder_Quantity_WhenSet_GetsCorrectValue()
        {
            const double expectedValue = 9.9;

            var target = new SingleOrder();
            Assert.IsTrue(double.IsNaN(target.Quantity));

            target.Quantity = expectedValue;
            Assert.AreEqual(expectedValue, target.Quantity);
        }

        [TestMethod]
        public void SingleOrder_Portfolio_WhenSet_GetsCorrectValue()
        {
            var target = new SingleOrder();
            Assert.IsNull(target.Portfolio);

            var timepiece = new SlaveStepTimepiece(new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            var currencyConverter = new FixedRateCurrencyConverter();
            var dataEmitter = new DataEmitter();
            var account = new Account("foobar", CurrencyCode.Nok, 0d, timepiece, currencyConverter, dataEmitter);
            var portfolio = new Portfolio(account, dataEmitter);

            target.Portfolio = portfolio;
            Assert.AreEqual(portfolio, target.Portfolio);
        }

        [TestMethod]
        public void SingleOrder_Account_WhenSet_GetsCorrectValue()
        {
            var target = new SingleOrder();
            Assert.IsNull(target.Account);

            var timepiece = new SlaveStepTimepiece(new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            var currencyConverter = new FixedRateCurrencyConverter();
            var dataEmitter = new DataEmitter();
            var account = new Account("foobar", CurrencyCode.Nok, 0d, timepiece, currencyConverter, dataEmitter);

            target.Account = account;
            Assert.AreEqual(account, target.Account);
        }

        [TestMethod]
        public void SingleOrder_MinimumQuantity_WhenSet_GetsCorrectValue()
        {
            const double expectedValue = 9.9;

            var target = new SingleOrder();
            Assert.IsTrue(double.IsNaN(target.MinimumQuantity));

            target.MinimumQuantity = expectedValue;
            Assert.AreEqual(expectedValue, target.MinimumQuantity);
        }

        [TestMethod]
        public void SingleOrder_LimitPrice_WhenSet_GetsCorrectValue()
        {
            const double expectedValue = 9.9;

            var target = new SingleOrder();
            Assert.IsTrue(double.IsNaN(target.LimitPrice));

            target.LimitPrice = expectedValue;
            Assert.AreEqual(expectedValue, target.LimitPrice);
        }

        [TestMethod]
        public void SingleOrder_Instrument_WhenSet_GetsCorrectValue()
        {
            var target = new SingleOrder();
            Assert.IsNull(target.Instrument);

            var expectedValue = new Instrument();
            target.Instrument = expectedValue;
            Assert.AreEqual(expectedValue, target.Instrument);
        }

        [TestMethod]
        public void SingleOrder_ExpirationTime_WhenSet_GetsCorrectValue()
        {
            var defaultValue = new DateTime(0L);
            var target = new SingleOrder();
            Assert.AreEqual(defaultValue, target.ExpirationTime);

            var expectedValue = DateTime.Now;
            target.ExpirationTime = expectedValue;
            Assert.AreEqual(expectedValue, target.ExpirationTime);
        }

        [TestMethod]
        public void SingleOrder_CreationTime_WhenSet_GetsCorrectValue()
        {
            var defaultValue = new DateTime(0L);
            var target = new SingleOrder();
            Assert.AreEqual(defaultValue, target.CreationTime);

            var expectedValue = DateTime.Now;
            target.CreationTime = expectedValue;
            Assert.AreEqual(expectedValue, target.CreationTime);
        }

        [TestMethod]
        public void SingleOrder_Constructor_WhenConstructedWithoutArguments_PropertiesHaveCorrectValues()
        {
            var defaultDateTimeValue = new DateTime(0L);
            var defaultTypeValue = OrderType.Market;
            var defaultTimeInForceValue = OrderTimeInForce.Day;
            var defaultSideValue = OrderSide.Buy;

            var target = new SingleOrder();

            Assert.AreEqual(defaultDateTimeValue, target.CreationTime);
            Assert.AreEqual(defaultDateTimeValue, target.ExpirationTime);
            Assert.IsNull(target.Instrument);
            Assert.IsNull(target.Text);
            Assert.IsNull(target.Portfolio);
            Assert.IsNull(target.Account);
            Assert.IsTrue(double.IsNaN(target.LimitPrice));
            Assert.IsTrue(double.IsNaN(target.MinimumQuantity));
            Assert.IsTrue(double.IsNaN(target.Quantity));
            Assert.IsTrue(double.IsNaN(target.StopPrice));
            Assert.IsTrue(double.IsNaN(target.TrailingDistance));
            Assert.AreEqual(defaultTypeValue, target.Type);
            Assert.AreEqual(defaultTimeInForceValue, target.TimeInForce);
            Assert.AreEqual(defaultSideValue, target.Side);
        }
    }
}
