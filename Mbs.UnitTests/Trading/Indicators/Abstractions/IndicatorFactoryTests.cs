using System;
using System.Linq;
using Mbs.Trading.Indicators;
using Mbs.Trading.Indicators.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Indicators.Abstractions
{
    [TestClass]
    public class IndicatorFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IndicatorFactory_Create_UnsupportedIndicatorType_ThrowsException()
        {
            var input = new IndicatorInput { IndicatorType = IndicatorType.Unknown };

            var target = IndicatorFactory.Create(input);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void IndicatorFactory_Create_Collection_CreatesInstances()
        {
            var input = new IndicatorInput
            {
                IndicatorType = IndicatorType.SimpleMovingAverage,
                Parameters = new SimpleMovingAverage.Parameters { Length = 7 },
                OutputKinds = new[] { (int)SimpleMovingAverage.OutputKind.Value },
            };

            var target = IndicatorFactory.Create(new[] { input, input, input });

            Assert.IsNotNull(target);
            var array = target.ToArray();
            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.IsNotNull(array[1]);
            Assert.IsNotNull(array[2]);
        }

        [TestMethod]
        public void IndicatorFactory_Create_SimpleMovingAverage_ValidInput_CreatesInstance()
        {
            var input = new IndicatorInput
            {
                IndicatorType = IndicatorType.SimpleMovingAverage,
                Parameters = new SimpleMovingAverage.Parameters { Length = 7 },
                OutputKinds = new[] { (int)SimpleMovingAverage.OutputKind.Value },
            };

            var target = IndicatorFactory.Create(input);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IndicatorFactory_Create_SimpleMovingAverage_InvalidParametersType_ThrowsException()
        {
            var input = new IndicatorInput
            {
                IndicatorType = IndicatorType.SimpleMovingAverage,
                Parameters = new ExponentialMovingAverage.ParametersLength { Length = 7 },
                OutputKinds = new[] { (int)SimpleMovingAverage.OutputKind.Value },
            };

            var target = IndicatorFactory.Create(input);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void IndicatorFactory_Create_ExponentialMovingAverage_ValidInput_CreatesInstance()
        {
            var inputLength = new IndicatorInput
            {
                IndicatorType = IndicatorType.ExponentialMovingAverage,
                Parameters = new ExponentialMovingAverage.ParametersLength { Length = 7 },
                OutputKinds = new[] { (int)ExponentialMovingAverage.OutputKind.Value },
            };
            var inputSmoothingFactor = new IndicatorInput
            {
                IndicatorType = IndicatorType.ExponentialMovingAverage,
                Parameters = new ExponentialMovingAverage.ParametersSmoothingFactor { SmoothingFactor = 0.123 },
                OutputKinds = new[] { (int)ExponentialMovingAverage.OutputKind.Value },
            };

            var targetLength = IndicatorFactory.Create(inputLength);
            var targetSmoothingFactor = IndicatorFactory.Create(inputSmoothingFactor);

            Assert.IsNotNull(targetLength);
            Assert.IsNotNull(targetSmoothingFactor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IndicatorFactory_Create_ExponentialMovingAverage_InvalidParametersType_ThrowsException()
        {
            var input = new IndicatorInput
            {
                IndicatorType = IndicatorType.ExponentialMovingAverage,
                Parameters = new SimpleMovingAverage.Parameters { Length = 7 },
                OutputKinds = new[] { (int)ExponentialMovingAverage.OutputKind.Value },
            };

            var target = IndicatorFactory.Create(input);

            Assert.IsNotNull(target);
        }
    }
}
