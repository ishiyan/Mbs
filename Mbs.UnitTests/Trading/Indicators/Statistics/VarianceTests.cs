using System;
using System.Linq;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;
using Mbs.Trading.Indicators.Statistics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Indicators.Statistics
{
    [TestClass]
    public class VarianceTests
    {
        private const int Decimals = 13;

        /// <summary>
        /// Variance input test data.
        /// </summary>
        private static readonly double[] VarianceInput =
        {
            1, 2, 8, 4, 9, 6, 7, 13, 9, 10, 3, 12,
        };

        /// <summary>
        /// Excel (VAR.P) output of population variance length 3.
        /// </summary>
        private static readonly double[] PopulationVarianceExpected3 =
        {
            double.NaN, double.NaN,
            9.55555555555556000, 6.22222222222222000, 4.66666666666667000, 4.22222222222222000, 1.55555555555556000,
            9.55555555555556000, 6.22222222222222000, 2.88888888888889000, 9.55555555555556000, 14.88888888888890000,
        };

        /// <summary>
        /// Excel (VAR.P) output of population variance length 5.
        /// </summary>
        private static readonly double[] PopulationVarianceExpected5 =
        {
            double.NaN, double.NaN, double.NaN, double.NaN,
            10.16000, 6.56000, 2.96000, 9.36000,
            5.76000, 6.00000, 11.04000, 12.24000,
        };

        /// <summary>
        /// Excel (VAR.S) output of sample variance length 3.
        /// </summary>
        private static readonly double[] SampleVarianceExpected3 =
        {
            double.NaN, double.NaN,
            14.3333333333333000, 9.3333333333333400, 7.0000000000000000, 6.3333333333333400, 2.3333333333333300,
            14.3333333333333000, 9.3333333333333400, 4.3333333333333400, 14.3333333333333000, 22.3333333333333000,
        };

        /// <summary>
        /// Excel (VAR.S) output of sample variance length 5.
        /// </summary>
        private static readonly double[] SampleVarianceExpected5 =
        {
            double.NaN, double.NaN, double.NaN, double.NaN,
            12.7000, 8.2000, 3.7000, 11.7000,
            7.2000, 7.5000, 13.8000, 15.3000,
        };

        [TestMethod]
        public void Variance_Metadata_ValuesAreCorrect()
        {
            var parameters = new Variance.Parameters { Length = 7, IsUnbiased = true };

            var target = new Variance(parameters);
            var metadata = target.Metadata;

            Assert.AreEqual(IndicatorType.Variance, metadata.IndicatorType);
            Assert.IsNotNull(metadata.Outputs);
            Assert.AreEqual(1, metadata.Outputs.Length);
            Assert.AreEqual((int)Variance.OutputKind.Value, metadata.Outputs[0].Kind);
            Assert.AreEqual(IndicatorOutputType.Scalar, metadata.Outputs[0].Type);
        }

        [TestMethod]
        public void Variance_Metadata_SampleVariance_NameAndDescriptionAreCorrect()
        {
            var parameters = new Variance.Parameters { Length = 7, IsUnbiased = true };

            var target = new Variance(parameters);
            var metadata = target.Metadata;

            Assert.AreEqual("var.s(7,c)", metadata.Outputs[0].Name);
            Assert.AreEqual("Unbiased estimation of the sample variance var.s(7,c)", metadata.Outputs[0].Description);
        }

        [TestMethod]
        public void Variance_Metadata_PopulationVariance_NameAndDescriptionAreCorrect()
        {
            var parameters = new Variance.Parameters { Length = 7, IsUnbiased = false };

            var target = new Variance(parameters);
            var metadata = target.Metadata;

            Assert.AreEqual("var.p(7,c)", metadata.Outputs[0].Name);
            Assert.AreEqual("Estimation of the population variance var.p(7,c)", metadata.Outputs[0].Description);
        }

        [TestMethod]
        public void Variance_Update_PopulationVariance_Single_ReturnsCorrectValues()
        {
            IndicatorOutput output;
            var scalar = new Scalar(DateTime.Now, 1d);
            int count = VarianceInput.Length;

            var parameters = new Variance.Parameters { Length = 3, IsUnbiased = false };
            var target = new Variance(parameters);

            for (int i = 0; i < 2; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 2; i < count; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.AreEqual(Math.Round(PopulationVarianceExpected3[i], Decimals), Math.Round(scalar.Value, Decimals));
            }

            parameters.Length = 5;
            target = new Variance(parameters);

            for (int i = 0; i < 4; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 4; i < count; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.AreEqual(Math.Round(PopulationVarianceExpected5[i], Decimals), Math.Round(scalar.Value, Decimals));
            }
        }

        [TestMethod]
        public void Variance_Update_PopulationVariance_Collection_ReturnsCorrectValues()
        {
            int count = VarianceInput.Length;
            var scalarArray = new Scalar[count];
            for (int i = 0; i < count; ++i)
            {
                scalarArray[i] = new Scalar { Time = DateTime.UtcNow, Value = VarianceInput[i] };
            }

            var parameters = new Variance.Parameters { Length = 3, IsUnbiased = false };
            var target = new Variance(parameters);

            var outputs = target.Update(scalarArray).ToList();
            Assert.AreEqual(count, outputs.Count);

            for (int i = 0; i < 2; ++i)
            {
                var scalar = (Scalar)outputs[i].Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 2; i < count; ++i)
            {
                var scalar = (Scalar)outputs[i].Outputs[0];
                Assert.AreEqual(Math.Round(PopulationVarianceExpected3[i], Decimals), Math.Round(scalar.Value, Decimals));
            }

            parameters.Length = 5;
            target = new Variance(parameters);

            outputs = target.Update(scalarArray).ToList();
            Assert.AreEqual(count, outputs.Count);

            for (int i = 0; i < 4; ++i)
            {
                var scalar = (Scalar)outputs[i].Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 4; i < count; ++i)
            {
                var scalar = (Scalar)outputs[i].Outputs[0];
                Assert.AreEqual(Math.Round(PopulationVarianceExpected5[i], Decimals), Math.Round(scalar.Value, Decimals));
            }
        }

        [TestMethod]
        public void Variance_Update_SampleVariance_Single_ReturnsCorrectValues()
        {
            IndicatorOutput output;
            var scalar = new Scalar(DateTime.Now, 1d);
            int count = VarianceInput.Length;

            var parameters = new Variance.Parameters { Length = 3, IsUnbiased = true };
            var target = new Variance(parameters);

            for (int i = 0; i < 2; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 2; i < count; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.AreEqual(Math.Round(SampleVarianceExpected3[i], Decimals), Math.Round(scalar.Value, Decimals));
            }

            parameters.Length = 5;
            target = new Variance(parameters);

            for (int i = 0; i < 4; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 4; i < count; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.AreEqual(Math.Round(SampleVarianceExpected5[i], Decimals), Math.Round(scalar.Value, Decimals));
            }
        }

        [TestMethod]
        public void Variance_Update_SampleVariance_Collection_ReturnsCorrectValues()
        {
            int count = VarianceInput.Length;
            var scalarArray = new Scalar[count];
            for (int i = 0; i < count; ++i)
            {
                scalarArray[i] = new Scalar { Time = DateTime.UtcNow, Value = VarianceInput[i] };
            }

            var parameters = new Variance.Parameters { Length = 3, IsUnbiased = true };
            var target = new Variance(parameters);

            var outputs = target.Update(scalarArray).ToList();
            Assert.AreEqual(count, outputs.Count);

            for (int i = 0; i < 2; ++i)
            {
                var scalar = (Scalar)outputs[i].Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 2; i < count; ++i)
            {
                var scalar = (Scalar)outputs[i].Outputs[0];
                Assert.AreEqual(Math.Round(SampleVarianceExpected3[i], Decimals), Math.Round(scalar.Value, Decimals));
            }

            parameters.Length = 5;
            target = new Variance(parameters);

            outputs = target.Update(scalarArray).ToList();
            Assert.AreEqual(count, outputs.Count);

            for (int i = 0; i < 4; ++i)
            {
                var scalar = (Scalar)outputs[i].Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 4; i < count; ++i)
            {
                var scalar = (Scalar)outputs[i].Outputs[0];
                Assert.AreEqual(Math.Round(SampleVarianceExpected5[i], Decimals), Math.Round(scalar.Value, Decimals));
            }
        }

        [TestMethod]
        public void Variance_UpdateSample_ReturnsTheSameValuesAsUpdate()
        {
            var scalar = new Scalar(DateTime.Now, 1d);
            int count = VarianceInput.Length;

            var parameters = new Variance.Parameters { Length = 3, IsUnbiased = false };
            var target = new Variance(parameters);
            var target2 = new Variance(parameters);

            for (int i = 0; i < 2; ++i)
            {
                scalar.Value = VarianceInput[i];
                target.Update(scalar);
                var d = target2.UpdateSample(VarianceInput[i]);
                Assert.IsTrue(double.IsNaN(d));
            }

            for (int i = 2; i < count; ++i)
            {
                scalar.Value = VarianceInput[i];
                var output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                var d = target2.UpdateSample(VarianceInput[i]);
                Assert.AreEqual(scalar.Value, d);
            }
        }

        [TestMethod]
        public void Variance_Reset_FunctionsCorrectly()
        {
            IndicatorOutput output;
            var scalar = new Scalar(DateTime.Now, 1d);
            int count = VarianceInput.Length;

            var parameters = new Variance.Parameters { Length = 3, IsUnbiased = false };
            var target = new Variance(parameters);

            for (int i = 0; i < 2; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 2; i < count; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.AreEqual(Math.Round(PopulationVarianceExpected3[i], Decimals), Math.Round(scalar.Value, Decimals));
            }

            target.Reset();

            for (int i = 0; i < 2; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.IsTrue(double.IsNaN(scalar.Value));
            }

            for (int i = 2; i < count; ++i)
            {
                scalar.Value = VarianceInput[i];
                output = target.Update(scalar);
                scalar = (Scalar)output.Outputs[0];
                Assert.AreEqual(Math.Round(PopulationVarianceExpected3[i], Decimals), Math.Round(scalar.Value, Decimals));
            }
        }

        [TestMethod]
        public void Variance_Constructor_CreatesInstance()
        {
            var parameters = new Variance.Parameters { Length = 5 };

            var target = new Variance(parameters);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Variance_Constructor_LengthIsNegative_ThrowsException()
        {
            var parameters = new Variance.Parameters { Length = -1 };

            var target = new Variance(parameters);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Variance_Constructor_LengthIsZero_ThrowsException()
        {
            var parameters = new Variance.Parameters();

            var target = new Variance(parameters);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Variance_Constructor_LengthIsOne_ThrowsException()
        {
            var parameters = new Variance.Parameters { Length = 1 };

            var target = new Variance(parameters);

            Assert.IsNotNull(target);
        }
    }
}
