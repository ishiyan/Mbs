using System;
using Mbs.Trading.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data
{
    [TestClass]
    public class QuoteTests
    {
        private const double TestEpsilon = 1e-14;
        private const double TestPrice = 6.66666666666666;
        private const double TestSize = 7.77777777777777;
        private const double DefaultAskPrice = 1.11111111111111;
        private const double DefaultAskSize = 8.88888888888888;
        private const double DefaultBidPrice = 2.22222222222222;
        private const double DefaultBidSize = 9.99999999999999;

        private static readonly DateTime DefaultDateTime = DateTime.Now;
        private static readonly DateTime GreaterDateTime = new DateTime(2099, 5, 5, 12, 4, 6);

        [TestMethod]
        public void Quote_Time_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultDateTime, target.Time);

            target.Time = GreaterDateTime;
            Assert.AreEqual(GreaterDateTime, target.Time);
        }

        [TestMethod]
        public void Quote_AskPrice_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultAskPrice, target.AskPrice, TestEpsilon);

            target.AskPrice = TestPrice;
            Assert.AreEqual(TestPrice, target.AskPrice, TestEpsilon);
        }

        [TestMethod]
        public void Quote_AskSize_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultAskSize, target.AskSize, TestEpsilon);

            target.AskSize = TestSize;
            Assert.AreEqual(TestSize, target.AskSize, TestEpsilon);
        }

        [TestMethod]
        public void Quote_BidPrice_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultBidPrice, target.BidPrice, TestEpsilon);

            target.BidPrice = TestPrice;
            Assert.AreEqual(TestPrice, target.BidPrice, TestEpsilon);
        }

        [TestMethod]
        public void Quote_BidSize_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultBidSize, target.BidSize, TestEpsilon);

            target.BidSize = TestSize;
            Assert.AreEqual(TestSize, target.BidSize, TestEpsilon);
        }

        [TestMethod]
        public void Quote_IsAskPriceEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsAskPriceEmpty);

            target.AskPrice = double.NaN;
            Assert.IsTrue(target.IsAskPriceEmpty);
        }

        [TestMethod]
        public void Quote_IsAskSizeEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsAskSizeEmpty);

            target.AskSize = double.NaN;
            Assert.IsTrue(target.IsAskSizeEmpty);
        }

        [TestMethod]
        public void Quote_IsBidPriceEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsBidPriceEmpty);

            target.BidPrice = double.NaN;
            Assert.IsTrue(target.IsBidPriceEmpty);
        }

        [TestMethod]
        public void Quote_IsBidSizeEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsBidSizeEmpty);

            target.BidSize = double.NaN;
            Assert.IsTrue(target.IsBidSizeEmpty);
        }

        [TestMethod]
        public void Quote_IsEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsEmpty);

            target.AskPrice = double.NaN;
            Assert.IsTrue(target.IsEmpty);

            target.AskPrice = TestPrice;
            target.AskSize = double.NaN;
            Assert.IsTrue(target.IsEmpty);

            target.AskSize = TestSize;
            target.BidPrice = double.NaN;
            Assert.IsTrue(target.IsEmpty);

            target.BidPrice = TestPrice;
            target.BidSize = double.NaN;
            Assert.IsTrue(target.IsEmpty);
        }

        [TestMethod]
        public void Quote_Clone_CorrectResult()
        {
            var target = CreateDefaultInstance();
            var targetCloned = target.Clone as Quote;

            Assert.IsNotNull(targetCloned);
            Assert.AreEqual(target.Time, targetCloned.Time);
            Assert.AreEqual(target.AskPrice, targetCloned.AskPrice);
            Assert.AreEqual(target.AskSize, targetCloned.AskSize);
            Assert.AreEqual(target.BidPrice, targetCloned.BidPrice);
            Assert.AreEqual(target.BidSize, targetCloned.BidSize);
        }

        [TestMethod]
        public void Quote_Empty_CorrectResults()
        {
            var target = CreateDefaultInstance();
            target.Empty();

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.IsTrue(double.IsNaN(target.AskPrice));
            Assert.IsTrue(double.IsNaN(target.AskSize));
            Assert.IsTrue(double.IsNaN(target.BidPrice));
            Assert.IsTrue(double.IsNaN(target.BidSize));
        }

        [TestMethod]
        public void Quote_Constructor_AllParameters_CorrectResult()
        {
            var target = CreateDefaultInstance();

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.AreEqual(DefaultAskPrice, target.AskPrice);
            Assert.AreEqual(DefaultAskSize, target.AskSize);
            Assert.AreEqual(DefaultBidPrice, target.BidPrice);
            Assert.AreEqual(DefaultBidSize, target.BidSize);
        }

        [TestMethod]
        public void Quote_Constructor_NoParameters_CorrectResult()
        {
            var target = new Quote(DefaultDateTime);

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.IsTrue(double.IsNaN(target.AskPrice));
            Assert.IsTrue(double.IsNaN(target.AskSize));
            Assert.IsTrue(double.IsNaN(target.BidPrice));
            Assert.IsTrue(double.IsNaN(target.BidSize));
        }

        private static Quote CreateDefaultInstance()
        {
            return new Quote(DefaultDateTime, DefaultBidPrice, DefaultBidSize, DefaultAskPrice, DefaultAskSize);
        }
    }
}
