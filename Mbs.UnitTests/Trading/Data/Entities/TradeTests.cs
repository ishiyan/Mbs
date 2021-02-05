using System;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Entities
{
    [TestClass]
    public class TradeTests
    {
        private const double TestEpsilon = 1e-14;
        private const double TestPrice = 7.77777777777777;
        private const double TestVolume = 8.88888888888888;
        private const double DefaultPrice = 2.22222222222222;
        private const double DefaultVolume = 9.99999999999999;

        private static readonly DateTime DefaultDateTime = DateTime.Now;
        private static readonly DateTime GreaterDateTime = new DateTime(2099, 5, 5, 12, 4, 6);

        [TestMethod]
        public void Trade_Time_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultDateTime, target.Time);

            target.Time = GreaterDateTime;
            Assert.AreEqual(GreaterDateTime, target.Time);
        }

        [TestMethod]
        public void Trade_Price_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultPrice, target.Price, TestEpsilon);

            target.Price = TestPrice;
            Assert.AreEqual(TestPrice, target.Price, TestEpsilon);
        }

        [TestMethod]
        public void Trade_Volume_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultVolume, target.Volume, TestEpsilon);

            target.Volume = TestVolume;
            Assert.AreEqual(TestVolume, target.Volume, TestEpsilon);
        }

        [TestMethod]
        public void Trade_IsPriceEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsPriceEmpty);

            target.Price = double.NaN;
            Assert.IsTrue(target.IsPriceEmpty);
        }

        [TestMethod]
        public void Trade_IsVolumeEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsVolumeEmpty);

            target.Volume = double.NaN;
            Assert.IsTrue(target.IsVolumeEmpty);
        }

        [TestMethod]
        public void Trade_IsEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsEmpty);

            target.Price = double.NaN;
            Assert.IsTrue(target.IsEmpty);

            target.Price = DefaultPrice;
            target.Volume = double.NaN;
            Assert.IsTrue(target.IsEmpty);
        }

        [TestMethod]
        public void Trade_Empty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            target.Empty();

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.IsTrue(double.IsNaN(target.Price));
            Assert.IsTrue(double.IsNaN(target.Volume));
        }

        [TestMethod]
        public void Trade_Clone_CorrectResult()
        {
            var target = CreateDefaultInstance();
            var targetCloned = target.Clone as Trade;

            Assert.IsNotNull(targetCloned);
            Assert.AreEqual(target.Time, targetCloned.Time);
            Assert.AreEqual(target.Price, targetCloned.Price);
            Assert.AreEqual(target.Volume, targetCloned.Volume);
        }

        [TestMethod]
        public void Trade_Constructor_AllParameters_CorrectResult()
        {
            var target = CreateDefaultInstance();

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.AreEqual(DefaultPrice, target.Price);
            Assert.AreEqual(DefaultVolume, target.Volume);
        }

        [TestMethod]
        public void Trade_Constructor_NoParameters_CorrectResult()
        {
            var target = new Trade(DefaultDateTime);

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.IsTrue(double.IsNaN(target.Price));
            Assert.IsTrue(double.IsNaN(target.Volume));
        }

        private static Trade CreateDefaultInstance()
        {
            return new Trade(DefaultDateTime, DefaultPrice, DefaultVolume);
        }
    }
}
