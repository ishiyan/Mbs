using System;
using Mbs.Trading.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data
{
    [TestClass]
    public class OhlcvTests
    {
        private const double TestEpsilon = 1e-14;
        private const double TestValue = 7.77777777777777;
        private const double DefaultOpen = 2.22222222222222;
        private const double DefaultHigh = 4.44444444444444;
        private const double DefaultLow = 1.11111111111111;
        private const double DefaultClose = 3.33333333333333;
        private const double DefaultVolume = 9.99999999999999;
        private const double DefaultMedian = (DefaultLow + DefaultHigh) / 2;
        private const double DefaultTypical = (DefaultLow + DefaultHigh + DefaultClose) / 3;
        private const double DefaultWeighted = (DefaultLow + DefaultHigh + 2 * DefaultClose) / 4;

        private static readonly DateTime DefaultDateTime = new DateTime(2017, 3, 3, 10, 1, 4);
        private static readonly DateTime LesserDateTime = new DateTime(2009, 4, 4, 11, 3, 5);
        private static readonly DateTime GreaterDateTime = new DateTime(2099, 5, 5, 12, 4, 6);

        private static Ohlcv CreateDefaultInstance()
        {
            return new Ohlcv(DefaultDateTime, DefaultOpen, DefaultHigh, DefaultLow, DefaultClose, DefaultVolume);
        }

        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void Ohlcv_Open_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultOpen, target.Open, TestEpsilon);

            target.Open = TestValue;
            Assert.AreEqual(TestValue, target.Open, TestEpsilon);
        }

        [TestMethod]
        public void Ohlcv_High_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultHigh, target.High, TestEpsilon);

            target.High = TestValue;
            Assert.AreEqual(TestValue, target.High, TestEpsilon);
        }

        [TestMethod]
        public void Ohlcv_Low_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultLow, target.Low, TestEpsilon);

            target.Low = TestValue;
            Assert.AreEqual(TestValue, target.Low, TestEpsilon);
        }

        [TestMethod]
        public void Ohlcv_Close_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultClose, target.Close, TestEpsilon);

            target.Close = TestValue;
            Assert.AreEqual(TestValue, target.Close, TestEpsilon);
        }

        [TestMethod]
        public void Ohlcv_Volume_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultVolume, target.Volume, TestEpsilon);

            target.Volume = TestValue;
            Assert.AreEqual(TestValue, target.Volume, TestEpsilon);
        }

        [TestMethod]
        public void Ohlcv_Median_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultMedian, target.Median, TestEpsilon);

            target.High = double.NaN;
            Assert.IsTrue(double.IsNaN(target.Median));

            const double testMedian = (DefaultLow + TestValue) / 2;
            target.High = TestValue;
            Assert.AreEqual(testMedian, target.Median);

            target.Low = double.NaN;
            Assert.IsTrue(double.IsNaN(target.Median));
        }

        [TestMethod]
        public void Ohlcv_Typical_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultTypical, target.Typical, TestEpsilon);

            target.Low = double.NaN;
            Assert.IsTrue(double.IsNaN(target.Typical));

            double testTypical = (DefaultLow + TestValue + DefaultClose) / 3;
            target.Low = DefaultLow;
            target.High = TestValue;
            Assert.AreEqual(testTypical, target.Typical, TestEpsilon);

            target.High = double.NaN;
            Assert.IsTrue(double.IsNaN(target.Typical));

            testTypical = (DefaultLow + DefaultHigh + TestValue) / 3;
            target.High = DefaultHigh;
            target.Close = TestValue;
            Assert.AreEqual(testTypical, target.Typical, TestEpsilon);

            target.Close = double.NaN;
            Assert.IsTrue(double.IsNaN(target.Typical));
        }

        [TestMethod]
        public void Ohlcv_Weighted_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultWeighted, target.Weighted, TestEpsilon);

            target.Low = double.NaN;
            Assert.IsTrue(double.IsNaN(target.Weighted));

            double testWeighted = (DefaultLow + TestValue + 2 * DefaultClose) / 4;
            target.Low = DefaultLow;
            target.High = TestValue;
            Assert.AreEqual(testWeighted, target.Weighted, TestEpsilon);

            target.High = double.NaN;
            Assert.IsTrue(double.IsNaN(target.Weighted));

            testWeighted = (DefaultLow + DefaultHigh + 2 * TestValue) / 4;
            target.High = DefaultHigh;
            target.Close = TestValue;
            Assert.AreEqual(testWeighted, target.Weighted, TestEpsilon);

            target.Close = double.NaN;
            Assert.IsTrue(double.IsNaN(target.Weighted));
        }

        [TestMethod]
        public void Ohlcv_Price_CorrectValue()
        {
            var target = CreateDefaultInstance();

            Assert.AreEqual(DefaultOpen, target.Price(OhlcvPriceType.Opening), TestEpsilon);
            Assert.AreEqual(DefaultHigh, target.Price(OhlcvPriceType.Highest), TestEpsilon);
            Assert.AreEqual(DefaultLow, target.Price(OhlcvPriceType.Lowest), TestEpsilon);
            Assert.AreEqual(DefaultClose, target.Price(OhlcvPriceType.Closing), TestEpsilon);
            Assert.AreEqual(DefaultMedian, target.Price(OhlcvPriceType.Median), TestEpsilon);
            Assert.AreEqual(DefaultTypical, target.Price(OhlcvPriceType.Typical), TestEpsilon);
            Assert.AreEqual(DefaultWeighted, target.Price(OhlcvPriceType.Weighted), TestEpsilon);
        }

        [TestMethod]
        public void Ohlcv_Component_CorrectValue()
        {
            var target = CreateDefaultInstance();

            Assert.AreEqual(DefaultOpen, target.Component(OhlcvComponent.OpeningPrice), TestEpsilon);
            Assert.AreEqual(DefaultHigh, target.Component(OhlcvComponent.HighestPrice), TestEpsilon);
            Assert.AreEqual(DefaultLow, target.Component(OhlcvComponent.LowestPrice), TestEpsilon);
            Assert.AreEqual(DefaultClose, target.Component(OhlcvComponent.ClosingPrice), TestEpsilon);
            Assert.AreEqual(DefaultMedian, target.Component(OhlcvComponent.MedianPrice), TestEpsilon);
            Assert.AreEqual(DefaultTypical, target.Component(OhlcvComponent.TypicalPrice), TestEpsilon);
            Assert.AreEqual(DefaultWeighted, target.Component(OhlcvComponent.WeightedPrice), TestEpsilon);
            Assert.AreEqual(DefaultVolume, target.Component(OhlcvComponent.Volume), TestEpsilon);
        }

        [TestMethod]
        public void Ohlcv_Time_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultDateTime, target.Time);

            target.Time = LesserDateTime;
            Assert.AreEqual(LesserDateTime, target.Time);
        }

        [TestMethod]
        public void Ohlcv_IsVolumeEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsVolumeEmpty);

            target.Volume = double.NaN;
            Assert.IsTrue(target.IsVolumeEmpty);
        }

        [TestMethod]
        public void Ohlcv_IsOpenEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsOpenEmpty);

            target.Open = double.NaN;
            Assert.IsTrue(target.IsOpenEmpty);
        }

        [TestMethod]
        public void Ohlcv_IsHighEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsHighEmpty);

            target.High = double.NaN;
            Assert.IsTrue(target.IsHighEmpty);
        }

        [TestMethod]
        public void Ohlcv_IsLowEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsLowEmpty);

            target.Low = double.NaN;
            Assert.IsTrue(target.IsLowEmpty);
        }

        [TestMethod]
        public void Ohlcv_IsCloseEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsCloseEmpty);

            target.Close = double.NaN;
            Assert.IsTrue(target.IsCloseEmpty);
        }

        [TestMethod]
        public void Ohlcv_IsMedianEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsMedianEmpty);

            target.High = double.NaN;
            Assert.IsTrue(target.IsMedianEmpty);

            target.High = DefaultHigh;
            target.Low = double.NaN;
            Assert.IsTrue(target.IsMedianEmpty);
        }

        [TestMethod]
        public void Ohlcv_IsTypicalEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsTypicalEmpty);

            target.High = double.NaN;
            Assert.IsTrue(target.IsTypicalEmpty);

            target.High = DefaultHigh;
            target.Low = double.NaN;
            Assert.IsTrue(target.IsTypicalEmpty);

            target.Low = DefaultHigh;
            target.Close = double.NaN;
            Assert.IsTrue(target.IsTypicalEmpty);
        }

        [TestMethod]
        public void Ohlcv_IsWeightedEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsWeightedEmpty);

            target.High = double.NaN;
            Assert.IsTrue(target.IsWeightedEmpty);

            target.High = DefaultHigh;
            target.Low = double.NaN;
            Assert.IsTrue(target.IsWeightedEmpty);

            target.Low = DefaultHigh;
            target.Close = double.NaN;
            Assert.IsTrue(target.IsWeightedEmpty);
        }

        [TestMethod]
        public void Ohlcv_IsRising_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultClose > DefaultOpen, target.IsRising);

            target.Close = double.NaN;
            Assert.IsFalse(target.IsRising);

            target.Close = DefaultClose;
            target.Open = double.NaN;
            Assert.IsFalse(target.IsRising);
        }

        [TestMethod]
        public void Ohlcv_IsFalling_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultClose < DefaultOpen, target.IsFalling);

            target.Close = double.NaN;
            Assert.IsFalse(target.IsFalling);

            target.Close = DefaultClose;
            target.Open = double.NaN;
            Assert.IsFalse(target.IsFalling);
        }

        [TestMethod]
        public void Ohlcv_IsPriceEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsPriceEmpty);

            target.Open = double.NaN;
            Assert.IsTrue(target.IsPriceEmpty);

            target.Open = DefaultOpen;
            target.High = double.NaN;
            Assert.IsTrue(target.IsPriceEmpty);

            target.High = DefaultHigh;
            target.Low = double.NaN;
            Assert.IsTrue(target.IsPriceEmpty);

            target.Low = DefaultLow;
            target.Close = double.NaN;
            Assert.IsTrue(target.IsPriceEmpty);
        }

        [TestMethod]
        public void Ohlcv_Clone_CorrectResult()
        {
            var target = CreateDefaultInstance();
            var targetCloned = target.Clone as Ohlcv;

            Assert.IsNotNull(targetCloned);
            Assert.AreEqual(target.Time, targetCloned.Time);
            Assert.AreEqual(target.Open, targetCloned.Open);
            Assert.AreEqual(target.High, targetCloned.High);
            Assert.AreEqual(target.Low, targetCloned.Low);
            Assert.AreEqual(target.Close, targetCloned.Close);
            Assert.AreEqual(target.Volume, targetCloned.Volume);
        }

        [TestMethod]
        public void Ohlcv_Empty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            target.Empty();

            Assert.IsTrue(double.IsNaN(target.Open));
            Assert.IsTrue(double.IsNaN(target.High));
            Assert.IsTrue(double.IsNaN(target.Low));
            Assert.IsTrue(double.IsNaN(target.Close));
            Assert.IsTrue(double.IsNaN(target.Volume));
            Assert.AreEqual(DefaultDateTime, target.Time);
        }

        [TestMethod]
        public void Ohlcv_CloneAggregation_Ohlcv_CorrectResult()
        {
            var source = CreateDefaultInstance();
            var target = source.CloneAggregation();

            Assert.AreEqual(source.Time, target.Time);
            Assert.AreEqual(source.Open, target.Open);
            Assert.AreEqual(source.High, target.High);
            Assert.AreEqual(source.Low, target.Low);
            Assert.AreEqual(source.Close, target.Close);
            Assert.AreEqual(source.Volume, target.Volume);
        }

        [TestMethod]
        public void Ohlcv_CloneAggregation_Trade_CorrectResult()
        {
            var source = new Trade(DefaultDateTime, DefaultClose, DefaultVolume);
            var target = Ohlcv.CloneAggregation(source);

            Assert.AreEqual(source.Time, target.Time);
            Assert.AreEqual(source.Price, target.Open);
            Assert.AreEqual(source.Price, target.High);
            Assert.AreEqual(source.Price, target.Low);
            Assert.AreEqual(source.Price, target.Close);
            Assert.AreEqual(source.Volume, target.Volume);
        }

        [TestMethod]
        public void Ohlcv_CloneAggregation_Scalar_CorrectResult()
        {
            var source = new Scalar(DefaultDateTime, DefaultClose);
            var target = Ohlcv.CloneAggregation(source);

            Assert.AreEqual(source.Time, target.Time);
            Assert.AreEqual(source.Value, target.Open);
            Assert.AreEqual(source.Value, target.High);
            Assert.AreEqual(source.Value, target.Low);
            Assert.AreEqual(source.Value, target.Close);
            Assert.IsTrue(double.IsNaN(target.Volume));
        }

        [TestMethod]
        public void Ohlcv_Aggregate_Ohlcv_CorrectResult()
        {
            const double NewOpen = DefaultOpen + 1d;
            const double NewHigh = DefaultHigh + 1d;
            const double NewLow = DefaultLow + 1d;
            const double NewClose = DefaultClose + 1d;
            const double NewVolume = DefaultVolume + 1d;

            var target = new Ohlcv(LesserDateTime);
            var other = CreateDefaultInstance();

            target.Aggregate(other);
            Assert.AreEqual(other.Time, target.Time);
            Assert.AreEqual(other.Open, target.Open);
            Assert.AreEqual(other.High, target.High);
            Assert.AreEqual(other.Low, target.Low);
            Assert.AreEqual(other.Close, target.Close);
            Assert.AreEqual(other.Volume, target.Volume);

            other = new Ohlcv(GreaterDateTime, NewOpen, NewHigh, NewLow, NewClose, NewVolume);
            target.Aggregate(other);
            Assert.AreEqual(other.Time, target.Time);
            Assert.AreEqual(DefaultOpen, target.Open);
            Assert.AreEqual(NewHigh, target.High);
            Assert.AreEqual(DefaultLow, target.Low);
            Assert.AreEqual(NewClose, target.Close);
            Assert.AreEqual(DefaultVolume + NewVolume, target.Volume);
        }

        [TestMethod]
        public void Ohlcv_Aggregate_Trade_CorrectResult()
        {
            const double OldPrice = DefaultClose;
            const double NewPrice = OldPrice + 1d;
            const double OldVolume = DefaultVolume;
            const double NewVolume = OldVolume + 1d;

            var target = new Ohlcv(LesserDateTime);
            var other = new Trade(DefaultDateTime, OldPrice, OldVolume);

            target.Aggregate(other);
            Assert.AreEqual(other.Time, target.Time);
            Assert.AreEqual(other.Price, target.Open);
            Assert.AreEqual(other.Price, target.High);
            Assert.AreEqual(other.Price, target.Low);
            Assert.AreEqual(other.Price, target.Close);
            Assert.AreEqual(other.Volume, target.Volume);

            other = new Trade(GreaterDateTime, NewPrice, NewVolume);
            target.Aggregate(other);
            Assert.AreEqual(other.Time, target.Time);
            Assert.AreEqual(OldPrice, target.Open);
            Assert.AreEqual(NewPrice, target.High);
            Assert.AreEqual(OldPrice, target.Low);
            Assert.AreEqual(NewPrice, target.Close);
            Assert.AreEqual(OldVolume + NewVolume, target.Volume);
        }

        [TestMethod]
        public void Ohlcv_Aggregate_Scalar_CorrectResult()
        {
            const double OldValue = DefaultClose;
            const double NewValue = OldValue + 1d;

            var target = new Ohlcv(LesserDateTime);
            var other = new Scalar(DefaultDateTime, OldValue);

            target.Aggregate(other);
            Assert.AreEqual(other.Time, target.Time);
            Assert.AreEqual(other.Value, target.Open);
            Assert.AreEqual(other.Value, target.High);
            Assert.AreEqual(other.Value, target.Low);
            Assert.AreEqual(other.Value, target.Close);
            Assert.IsTrue(double.IsNaN(target.Volume));

            other = new Scalar(GreaterDateTime, NewValue);
            target.Aggregate(other);
            Assert.AreEqual(other.Time, target.Time);
            Assert.AreEqual(OldValue, target.Open);
            Assert.AreEqual(NewValue, target.High);
            Assert.AreEqual(OldValue, target.Low);
            Assert.AreEqual(NewValue, target.Close);
            Assert.IsTrue(double.IsNaN(target.Volume));
        }

        [TestMethod]
        public void Ohlcv_Constructor_AllParameters_CorrectResult()
        {
            var target = CreateDefaultInstance();

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.AreEqual(DefaultOpen, target.Open);
            Assert.AreEqual(DefaultHigh, target.High);
            Assert.AreEqual(DefaultLow, target.Low);
            Assert.AreEqual(DefaultClose, target.Close);
            Assert.AreEqual(DefaultVolume, target.Volume);
        }

        [TestMethod]
        public void Ohlcv_Constructor_NoParameters_CorrectResults()
        {
            var target = new Ohlcv(DefaultDateTime);

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.IsTrue(double.IsNaN(target.Open));
            Assert.IsTrue(double.IsNaN(target.High));
            Assert.IsTrue(double.IsNaN(target.Low));
            Assert.IsTrue(double.IsNaN(target.Close));
            Assert.IsTrue(double.IsNaN(target.Volume));
        }
    }
}
