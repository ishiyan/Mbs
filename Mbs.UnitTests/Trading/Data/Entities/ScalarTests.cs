using System;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Entities
{
    [TestClass]
    public class ScalarTests
    {
        private const double TestEpsilon = 1e-14;
        private const double TestValue = 7.77777777777777;
        private const double DefaultValue = 2.22222222222222;

        private static readonly DateTime DefaultDateTime = DateTime.Now;
        private static readonly DateTime GreaterDateTime = new DateTime(2099, 5, 5, 12, 4, 6);

        [TestMethod]
        public void Scalar_Time_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultDateTime, target.Time);

            target.Time = GreaterDateTime;
            Assert.AreEqual(GreaterDateTime, target.Time);
        }

        [TestMethod]
        public void Scalar_Value_CorrectValue()
        {
            var target = CreateDefaultInstance();
            Assert.AreEqual(DefaultValue, target.Value, TestEpsilon);

            target.Value = TestValue;
            Assert.AreEqual(TestValue, target.Value, TestEpsilon);
        }

        [TestMethod]
        public void Scalar_IsEmpty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            Assert.IsFalse(target.IsEmpty);

            target.Value = double.NaN;
            Assert.IsTrue(target.IsEmpty);
        }

        [TestMethod]
        public void Scalar_Clone_CorrectResult()
        {
            var target = CreateDefaultInstance();
            var targetCloned = target.Clone as Scalar;

            Assert.IsNotNull(targetCloned);
            Assert.AreEqual(target.Time, targetCloned.Time);
            Assert.AreEqual(target.Value, targetCloned.Value);
        }

        [TestMethod]
        public void Scalar_Empty_CorrectResult()
        {
            var target = CreateDefaultInstance();
            target.Empty();

            Assert.IsTrue(double.IsNaN(target.Value));
            Assert.AreEqual(DefaultDateTime, target.Time);
        }

        [TestMethod]
        public void Scalar_Constructor_AllParameters_CorrectResult()
        {
            var target = CreateDefaultInstance();

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.AreEqual(DefaultValue, target.Value);
        }

        [TestMethod]
        public void Scalar_Constructor_NoParameters_CorrectResult()
        {
            var target = new Scalar(DefaultDateTime);

            Assert.AreEqual(DefaultDateTime, target.Time);
            Assert.IsTrue(double.IsNaN(target.Value));
        }

        private static Scalar CreateDefaultInstance()
        {
            return new Scalar(DefaultDateTime, DefaultValue);
        }
    }
}
