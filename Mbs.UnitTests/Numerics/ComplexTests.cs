using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mbst.Numerics;

namespace Mbs.UnitTests.Numerics
{
    /// <summary>
    /// This is a test class for ComplexTest and is intended to contain all ComplexTest Unit Tests.
    /// </summary>
    [TestClass]
    public class ComplexTest
    {
        #region TestContext
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        #region Utils
        private static void Normalize2Pi(ref double value)
        {
            while (value < 0d)
                value += Constants.Pi * 2d;
            value = Math.IEEERemainder(value, Constants.Pi * 2d);
        }
        #endregion

        #region Construction tests
        #region ConstructorTest
        /// <summary>
        /// A test for Complex Constructor.
        ///  </summary>
        [TestMethod]
        public void ConstructorTest()
        {
            const double real = 3.5;
            const double imaginary = 4.6;
            var target = new Complex(real, imaginary);
            Assert.AreEqual(real, target.Real);
            Assert.AreEqual(imaginary, target.Imag);
        }
        #endregion

        #region ConstructorTest1
        /// <summary>
        /// A test for Complex Constructor.
        /// </summary>
        [TestMethod]
        public void ConstructorTest1()
        {
            const double real = 7.7;
            var target = new Complex(real);
            Assert.AreEqual(real, target.Real);
            Assert.AreEqual(0d, target.Imag);
        }
        #endregion

        #region FromModulusArgumentTest
        /// <summary>
        /// A test for FromModulusArgument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FromModulusArgumentTest()
        {
            double modulus = 8.8, argument = 9.9;
            Complex target = Complex.FromModulusArgument(modulus, argument);
            Assert.AreEqual(modulus, target.Modulus, "1.1");
            double output = target.Argument;
            Normalize2Pi(ref output);
            double input = argument;
            Normalize2Pi(ref input);
            Assert.IsTrue(Accuracy.AlmostEqual(input, output), "1.2");

            modulus = 1d; argument = 0d;
            target = Complex.FromModulusArgument(modulus, argument);
            Assert.AreEqual(modulus, target.Modulus, "2.1");
            output = target.Argument;
            Assert.AreEqual(argument, output, "2.2");

            modulus = 0d; argument = 0d;
            target = Complex.FromModulusArgument(modulus, argument);
            Assert.AreEqual(modulus, target.Modulus, "3.1");
            output = target.Argument;
            Assert.AreEqual(argument, output, "3.2");

            modulus = -1d; argument = 1d;
            /*target =*/ Complex.FromModulusArgument(modulus, argument);
        }
        #endregion

        #region FromRealImaginaryTest
        /// <summary>
        /// A test for FromRealImaginary.
        /// </summary>
        [TestMethod]
        public void FromRealImaginaryTest()
        {
            double real = 8.8, imaginary = 9.9;
            Complex target = Complex.FromRealImaginary(real, imaginary);
            Assert.AreEqual(real, target.Real, "1.1");
            Assert.AreEqual(imaginary, target.Imag, "1.2");

            real = 0d; imaginary = 9.9;
            target = Complex.FromRealImaginary(real, imaginary);
            Assert.AreEqual(real, target.Real, "2.1");
            Assert.AreEqual(imaginary, target.Imag, "2.2");

            real = 0d; imaginary = 0d;
            target = Complex.FromRealImaginary(real, imaginary);
            Assert.AreEqual(real, target.Real, "3.1");
            Assert.AreEqual(imaginary, target.Imag, "3.2");

            real = -9d; imaginary = 0d;
            target = Complex.FromRealImaginary(real, imaginary);
            Assert.AreEqual(real, target.Real, "4.1");
            Assert.AreEqual(imaginary, target.Imag, "4.2");

            real = -9d; imaginary = -8d;
            target = Complex.FromRealImaginary(real, imaginary);
            Assert.AreEqual(real, target.Real, "5.1");
            Assert.AreEqual(imaginary, target.Imag, "5.2");
        }
        #endregion
        #endregion

        #region Functions
        #region AbsTest
        /// <summary>
        /// A test for Abs.
        /// </summary>
        [TestMethod]
        public void AbsTest()
        {
            var target = new Complex(0d, 0d);
            Assert.AreEqual(0d, target.Abs(), "1.1");
            target = new Complex(0d, 1d);
            Assert.AreEqual(1d, target.Abs(), "1.2");
            target = new Complex(1d, 5d);
            Assert.AreEqual(Math.Sqrt(26.0), target.Abs(), "1.3");
            // Static
            Assert.AreEqual(Math.Sqrt(26.0), Complex.Abs(target), "2");
            // Matlab
            target = new Complex(1.1, 1.2);
            Assert.IsTrue(Math.Abs(1.627882059609971 - target.Abs()) < 1e-15, "3.1");
            target = new Complex(123.456, 789.123);
            Assert.IsTrue(Math.Abs(798.7217870228657 - target.Abs()) < 1e-12, "3.2");
        }
        #endregion

        #region InvTest
        /// <summary>
        /// A test for Inv.
        /// </summary>
        [TestMethod]
        public void InvTest()
        {
            // Matlab
            var number = new Complex(123.456, 789.123);
            Complex target = number.Inv();
            Assert.IsTrue(Math.Abs(0.000193517898700 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-0.001236954257192 - target.Imag) < 1e-15, "1.2");
            number = new Complex(1.2345, -1.6789);
            target = number.Inv();
            Assert.IsTrue(Math.Abs(0.284270451697757 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(0.386603208874334 - target.Imag) < 1e-15, "2.2");
            number = new Complex(1.2345, 1.6789);
            target = number.Inv();
            Assert.IsTrue(Math.Abs(0.284270451697757 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.386603208874334 - target.Imag) < 1e-15, "3.2");
            number = new Complex(-1.2345, 1.6789);
            target = number.Inv();
            Assert.IsTrue(Math.Abs(-0.284270451697757 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.386603208874334 - target.Imag) < 1e-15, "3.2");
            number = new Complex(-1.2345, -1.6789);
            target = number.Inv();
            Assert.IsTrue(Math.Abs(-0.284270451697757 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.386603208874334 - target.Imag) < 1e-15, "3.2");
            // Static
            target = Complex.Inv(number);
            Assert.IsTrue(Math.Abs(-0.284270451697757 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.386603208874334 - target.Imag) < 1e-15, "3.2");
        }
        #endregion
        #endregion

        #region Exponential functions
        #region ExpTest
        /// <summary>
        /// A test for Exp.
        /// </summary>
        [TestMethod]
        public void ExpTest()
        {
            // Matlab
            var number = new Complex(1d, 3d);
            Complex target = number.Exp();
            Assert.IsTrue(target.Abs() - Math.Exp(1d) < 0.0001 && target.Argument - 3 < 0.0001, "1.1");
            // Static
            target = Complex.Exp(number);
            Assert.IsTrue(target.Abs() - Math.Exp(1d) < 0.0001 && target.Argument - 3 < 0.0001, "1.1");

            // exp(0) = 1
            number = Complex.Zero;
            target = number.Exp();
            Assert.AreEqual(1d, target.Real, "2.1");
            Assert.AreEqual(0d, target.Imag, "2.2");

            // exp(1) = e
            number = Complex.One;
            target = number.Exp();
            Assert.AreEqual(Constants.E, target.Real, "3.1");
            Assert.AreEqual(0d, target.Imag, "3.2");

            // exp(i) = cos(1) + sin(1) * i
            number = Complex.ImaginaryOne;
            target = number.Exp();
            Assert.AreEqual(Math.Cos(1d), target.Real, "4.1");
            Assert.AreEqual(Math.Sin(1d), target.Imag, "4.2");

            // exp(-1) = 1/e
            number = -Complex.One;
            target = number.Exp();
            Assert.AreEqual(1d / Constants.E, target.Real, "5.1");
            Assert.AreEqual(0d, target.Imag, "5.2");

            // exp(-i) = cos(1) - sin(1) * i
            number = -Complex.ImaginaryOne;
            target = number.Exp();
            Assert.AreEqual(Math.Cos(1d), target.Real, "6.1");
            Assert.AreEqual(-Math.Sin(1d), target.Imag, "6.2");

            // exp(i+1) = e * cos(1) + e * sin(1) * i
            number = Complex.One + Complex.ImaginaryOne;
            target = number.Exp();
            Assert.AreEqual(Constants.E * Math.Cos(1d), target.Real, "7.1");
            Assert.AreEqual(Constants.E * Math.Sin(1d), target.Imag, "7.2");
        }
        #endregion

        #region LogTest
        /// <summary>
        /// A test for Log.
        /// </summary>
        [TestMethod]
        public void LogTest()
        {
            // Matlab comparisons
            var number = new Complex(0.1, 0.3);
            Complex target = number.Log();
            Assert.IsTrue(Math.Abs(target.Real - -1.151292546497023) < 1e-15, "m1.1");
            Assert.IsTrue(Math.Abs(target.Imag - 1.249045772398254) < 1e-15, "m1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Log();
            Assert.IsTrue(Math.Abs(target.Real - 0.407682406642097) < 1e-15, "m2.1");
            Assert.IsTrue(Math.Abs(target.Imag - 1.637364490570721) < 1e-15, "m2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Log();
            Assert.IsTrue(Math.Abs(target.Real - -0.539404830685965) < 1e-15, "m3.1");
            Assert.IsTrue(Math.Abs(target.Imag - -2.601173153319209) < 1e-15, "m3.2");
            number = new Complex(0.5, -1.2);
            target = number.Log();
            Assert.IsTrue(Math.Abs(target.Real - 0.262364264467491) < 1e-15, "m3.1");
            Assert.IsTrue(Math.Abs(target.Imag - -1.176005207095135) < 1e-15, "m3.2");

            // ln(0) = -infty
            number = Complex.Zero;
            target = number.Log();
            Assert.AreEqual(double.NegativeInfinity, target.Real, "1.1");
            Assert.AreEqual(0d, target.Imag, "1.2");

            // ln(1) = 0
            number = Complex.One;
            target = number.Log();
            Assert.AreEqual(0d, target.Real, "2.1");
            Assert.AreEqual(0d, target.Imag, "2.2");

            // ln(i) = Pi/2 * i
            number = Complex.ImaginaryOne;
            target = number.Log();
            Assert.AreEqual(0d, target.Real, "3.1");
            Assert.AreEqual(Constants.PiOver2, target.Imag, "3.2");

            // ln(-1) = Pi * i
            number = -Complex.One;
            target = number.Log();
            Assert.AreEqual(0d, target.Real, "4.1");
            Assert.AreEqual(Constants.Pi, target.Imag, "4.2");

            // ln(-i) = -Pi/2 * i
            number = -Complex.ImaginaryOne;
            target = number.Log();
            Assert.AreEqual(0d, target.Real, "5.1");
            Assert.AreEqual(-Constants.PiOver2, target.Imag, "5.2");

            // ln(i+1) = ln(2)/2 + Pi/4 * i
            number = Complex.One + Complex.ImaginaryOne;
            target = number.Log();
            Assert.AreEqual(Constants.Ln2 * 0.5, target.Real, "6.1");
            Assert.AreEqual(Constants.PiOver4, target.Imag, "6.2");

            // Static
            number = new Complex(-0.1, 1.5);
            target = number.Log();
            Assert.IsTrue(Math.Abs(target.Real - 0.407682406642097) < 1e-15, "s1.1");
            Assert.IsTrue(Math.Abs(target.Imag - 1.637364490570721) < 1e-15, "s1.2");
        }
        #endregion
        #endregion

        #region Power functions
        #region PowTest
        /// <summary>
        /// A test for Pow.
        /// </summary>
        [TestMethod]
        public void PowTest()
        {
            // Matlab
            var number = new Complex(0.5, -Math.Sqrt(3) / 2);
            Complex target = number.Pow(2d);
            Assert.IsTrue(Math.Abs(target.Real + 0.5) < 1e-15 && Math.Abs(target.Imag + Math.Sqrt(3) / 2) < 1e-15, "1.1");
            number = new Complex(3, 2);
            target = number.Pow(-2d);
            Complex temp = 1d / (number * number);
            Assert.IsTrue(Math.Abs(target.Real - temp.Real) < 1e-15 && Math.Abs(target.Imag - temp.Imag) < 1e-15, "1.2");
            //Static
            target = Complex.Pow(number, -2d);
            Assert.IsTrue(Math.Abs(target.Real - temp.Real) < 1e-15 && Math.Abs(target.Imag - temp.Imag) < 1e-15, "1.3");

            // (1)^(1) = 1
            number = Complex.One;
            target = number.Pow(number);
            Assert.AreEqual(1d, target.Real, "2.1");
            Assert.AreEqual(0d, target.Imag, "2.2");

            // (i)^(1) = i
            number = Complex.ImaginaryOne;
            target = number.Pow(Complex.One);
            Assert.IsTrue(Accuracy.AlmostEqual(0d, target.Real), "3.1");
            Assert.AreEqual(1d, target.Imag, "3.2");

            // (1)^(-1) = 1
            number = -Complex.One;
            target = Complex.One.Pow(number);
            Assert.AreEqual(1d, target.Real, "4.1");
            Assert.AreEqual(0d, target.Imag, "4.2");

            // (i)^(-1) = -i
            target = Complex.ImaginaryOne.Pow(-Complex.One);
            Assert.IsTrue(Accuracy.AlmostEqual(0d, target.Real), "5.1");
            Assert.AreEqual(-1d, target.Imag, "5.2");

            // (i)^(-i) = exp(Pi/2)
            number = -Complex.ImaginaryOne;
            target = Complex.ImaginaryOne.Pow(number);
            Assert.AreEqual(Math.Exp(Constants.PiOver2), target.Real, "6.1");
            Assert.AreEqual(0d, target.Imag, "6.2");

            // (0)^(0) = 1
            Assert.AreEqual(1d, Math.Pow(0d, 0d), "(0)^(0) = 1 (.Net Framework Sanity Check)");
            number = Complex.Zero;
            target = number.Pow(number);
            Assert.AreEqual(1d, target.Real, "7.1");
            Assert.AreEqual(0d, target.Imag, "7.1");

            // (0)^(2) = 0
            Assert.AreEqual(0d, Math.Pow(0d, 2d), "(0)^(2) = 0 (.Net Framework Sanity Check)");
            number = new Complex(2d, 0d);
            target = Complex.Zero.Pow(number);
            Assert.AreEqual(0d, target.Real, "8.1");
            Assert.AreEqual(0d, target.Imag, "8.1");

            // (0)^(-2) = infty
            Assert.AreEqual(double.PositiveInfinity, Math.Pow(0d, -2d), "(0)^(-2) = infty (.Net Framework Sanity Check)");
            number = Complex.FromRealImaginary(-2d, 0d);
            target = Complex.Zero.Pow(number);
            Assert.AreEqual(double.PositiveInfinity, target.Real, "9.1");
            Assert.AreEqual(0d, target.Imag, "9.2");

            // (0)^(ImaginaryOne) = NaN
            target = Complex.Zero.Pow(Complex.ImaginaryOne);
            Assert.AreEqual(double.NaN, target.Real, "10.1");
            Assert.AreEqual(double.NaN, target.Imag, "10.2");

            // (0)^(-ImaginaryOne) = NaN
            target = Complex.Zero.Pow(-Complex.ImaginaryOne);
            Assert.AreEqual(double.NaN, target.Real, "11.1");
            Assert.AreEqual(double.NaN, target.Imag, "11.2");

            // (0)^(1+ImaginaryOne) = 0
            number = Complex.One + Complex.ImaginaryOne;
            target = Complex.Zero.Pow(number);
            Assert.AreEqual(0d, target.Real, "12.1");
            Assert.AreEqual(0d, target.Imag, "12.2");

            // (0)^(1-ImaginaryOne) = 0
            number = Complex.One - Complex.ImaginaryOne;
            target = Complex.Zero.Pow(number);
            Assert.AreEqual(0d, target.Real, "13.1");
            Assert.AreEqual(0d, target.Imag, "13.2");

            // (0)^(-1+ImaginaryOne) = infty + infty * i
            number = new Complex(-1d, 1d);
            target = Complex.Zero.Pow(number);
            Assert.AreEqual(double.PositiveInfinity, target.Real, "14.1");
            Assert.AreEqual(double.PositiveInfinity, target.Imag, "14.2");
        }
        #endregion

        #region SquareTest
        /// <summary>
        /// A test for Square.
        /// </summary>
        [TestMethod]
        public void SquareTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Square();
            Assert.IsTrue(Math.Abs(-0.080000000000000 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.060000000000000 - target.Imag) < 1e-15, "1.2");

            number = new Complex(-0.1, 1.5);
            target = number.Square();
            Assert.IsTrue(Math.Abs(-2.240000000000000 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.300000000000000 - target.Imag) < 1e-15, "2.2");

            number = new Complex(-0.5, -1.3);
            target = number.Square();
            Assert.IsTrue(Math.Abs(-1.440000000000000 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(1.300000000000000 - target.Imag) < 1e-15, "3.2");

            number = new Complex(0.6, -1.1);
            target = number.Square();
            Assert.IsTrue(Math.Abs(-0.850000000000000 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-1.320000000000000 - target.Imag) < 1e-15, "4.2");
            // Static
            number = new Complex(0.678, -1.123);
            target = Complex.Square(number);
            Assert.IsTrue(Math.Abs(-0.801445000000000 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-1.522788000000000 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region SqrtTest
        /// <summary>
        /// A test for Sqrt.
        /// </summary>
        [TestMethod]
        public void SqrtTest()
        {
            // Matlab
            var number = new Complex(1d, 4d);
            Complex target = number.Sqrt();
            Assert.IsTrue(Math.Abs(1.600485180440241 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(1.249621067687653 - target.Imag) < 1e-15, "1.2");
            number = new Complex(2.3, -3.6);
            target = number.Sqrt();
            Assert.IsTrue(Math.Abs(1.812733001941925 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.992975798461062 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-3.1, -2.5);
            target = number.Sqrt();
            Assert.IsTrue(Math.Abs(0.664252041904267 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-1.881815818610841 - target.Imag) < 1e-15, "3.2");
            number = new Complex(-1.9, 5.3);
            target = number.Sqrt();
            Assert.IsTrue(Math.Abs(1.365700425441777 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(1.940396261605307 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Sqrt(number);
            Assert.IsTrue(Math.Abs(1.365700425441777 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(1.940396261605307 - target.Imag) < 1e-15, "5.2");

            // Matlab
            target = Complex.Sqrt(9.7);
            Assert.IsTrue(Math.Abs(3.114482300479487 - target.Real) < 1e-15, "6.1");
            Assert.IsTrue(Math.Abs(0d - target.Imag) < 1e-15, "6.2");
            target = Complex.Sqrt(0d);
            Assert.IsTrue(Math.Abs(0d - target.Real) < 1e-15, "7.1");
            Assert.IsTrue(Math.Abs(0d - target.Imag) < 1e-15, "7.2");
            target = Complex.Sqrt(1d);
            Assert.IsTrue(Math.Abs(1d - target.Real) < 1e-15, "8.1");
            Assert.IsTrue(Math.Abs(0d - target.Imag) < 1e-15, "8.2");
            target = Complex.Sqrt(-7);
            Assert.IsTrue(Math.Abs(0d - target.Real) < 1e-15, "9.1");
            Assert.IsTrue(Math.Abs(2.645751311064591 - target.Imag) < 1e-15, "9.2");
            target = Complex.Sqrt(-0.005);
            Assert.IsTrue(Math.Abs(0d - target.Real) < 1e-15, "10.1");
            Assert.IsTrue(Math.Abs(0.070710678118655 - target.Imag) < 1e-15, "10.2");
        }
        #endregion
        #endregion

        #region Trigonometric functions
        #region CosTest
        /// <summary>
        /// A test for Cos.
        /// </summary>
        [TestMethod]
        public void CosTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Cos();
            Assert.IsTrue(Math.Abs(1.040116175683759 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-0.030401301333123 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Cos();
            Assert.IsTrue(Math.Abs(2.340657365607109 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(0.212573242998012 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Cos();
            Assert.IsTrue(Math.Abs(0.917370851271881 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.145994805701806 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Cos();
            Assert.IsTrue(Math.Abs(1.588999751473591 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(0.723674323320711 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Cos(number);
            Assert.IsTrue(Math.Abs(1.588999751473591 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(0.723674323320711 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region SinTest
        /// <summary>
        /// A test for Sin.
        /// </summary>
        [TestMethod]
        public void SinTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Sin();
            Assert.IsTrue(Math.Abs(0.104359715418003 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.302998960391594 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Sin();
            Assert.IsTrue(Math.Abs(-0.234849089242584 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(2.118641926860268 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Sin();
            Assert.IsTrue(Math.Abs(-0.501161980159946 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.267241699270951 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Sin();
            Assert.IsTrue(Math.Abs(0.868074520591187 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-1.324676963357129 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Sin(number);
            Assert.IsTrue(Math.Abs(0.868074520591187 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-1.324676963357129 - target.Imag) < 1e-15, "4.2");
        }
        #endregion

        #region TanTest
        /// <summary>
        /// A test for Tan.
        /// </summary>
        [TestMethod]
        public void TanTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Tan();
            Assert.IsTrue(Math.Abs(0.091741590289446 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.293994104958268 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Tan();
            Assert.IsTrue(Math.Abs(-0.017982821488705 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(0.906781413088994 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Tan();
            Assert.IsTrue(Math.Abs(-0.487592316492139 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.368910396825564 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Tan();
            Assert.IsTrue(Math.Abs(0.138008291862926 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-0.896507390427591 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Tan(number);
            Assert.IsTrue(Math.Abs(0.138008291862926 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-0.896507390427591 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region CotTest
        /// <summary>
        /// A test for Cot.
        /// </summary>
        [TestMethod]
        public void CotTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Cot();
            Assert.IsTrue(Math.Abs(0.967237808425478 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-3.099599787541052 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Cot();
            Assert.IsTrue(Math.Abs(-0.021861595026880 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-1.102368059612034 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Cot();
            Assert.IsTrue(Math.Abs(-1.304276747265860 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.986810571310472 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Cot();
            Assert.IsTrue(Math.Abs(0.167735809112832 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(1.089618532909340 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Cot(number);
            Assert.IsTrue(Math.Abs(0.167735809112832 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(1.089618532909340 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region SecTest
        /// <summary>
        /// A test for Sec.
        /// </summary>
        [TestMethod]
        public void SecTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Sec();
            Assert.IsTrue(Math.Abs(0.960610393774748 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.028077446277266 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Sec();
            Assert.IsTrue(Math.Abs(0.423735494587800 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.038482705577257 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Sec();
            Assert.IsTrue(Math.Abs(1.063145340789472 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.169194058483704 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Sec();
            Assert.IsTrue(Math.Abs(0.521218545691264 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-0.237377304814261 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Sec(number);
            Assert.IsTrue(Math.Abs(0.521218545691264 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-0.237377304814261 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region CosecTest
        /// <summary>
        /// A test for Cosec.
        /// </summary>
        [TestMethod]
        public void CosecTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Cosec();
            Assert.IsTrue(Math.Abs(1.016167538541131 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-2.950350204850528 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Cosec();
            Assert.IsTrue(Math.Abs(-0.051685639257015 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.466271181632630 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Cosec();
            Assert.IsTrue(Math.Abs(-1.553598232470387 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.828447184874691 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Cosec();
            Assert.IsTrue(Math.Abs(0.346077725103826 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(0.528112712793212 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Cosec(number);
            Assert.IsTrue(Math.Abs(0.346077725103826 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(0.528112712793212 - target.Imag) < 1e-15, "5.2");
        }
        #endregion
        #endregion

        #region Inverse trigonometric functions
        #region AcosTest
        /// <summary>
        /// A test for Acos.
        /// </summary>
        [TestMethod]
        public void AcosTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Acos();
            Assert.IsTrue(Math.Abs(1.474903367519443 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-0.296999023408390 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Acos();
            Assert.IsTrue(Math.Abs(1.626235699131362 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-1.196042838552249 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Acos();
            Assert.IsTrue(Math.Abs(2.063835567380515 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.334299817774938 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Acos();
            Assert.IsTrue(Math.Abs(1.255110336239894 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(1.055304915437955 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Acos(number);
            Assert.IsTrue(Math.Abs(1.255110336239894 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(1.055304915437955 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region AsinTest
        /// <summary>
        /// A test for Asin.
        /// </summary>
        [TestMethod]
        public void AsinTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Asin();
            Assert.IsTrue(Math.Abs(0.095892959275454 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.296999023408390 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Asin();
            Assert.IsTrue(Math.Abs(-0.055439372336465 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(1.196042838552249 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Asin();
            Assert.IsTrue(Math.Abs(-0.493039240585618 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.334299817774938 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Asin();
            Assert.IsTrue(Math.Abs(0.315685990555002 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-1.055304915437955 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Asin(number);
            Assert.IsTrue(Math.Abs(0.315685990555002 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-1.055304915437955 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region AtanTest
        /// <summary>
        /// A test for Atan.
        /// </summary>
        [TestMethod]
        public void AtanTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Atan();
            Assert.IsTrue(Math.Abs(0.109334472936971 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.305943857905529 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Atan();
            Assert.IsTrue(Math.Abs(-1.492087890431601 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(0.795313458269654 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Atan();
            Assert.IsTrue(Math.Abs(-0.493711659900520 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.240948266464790 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Atan();
            Assert.IsTrue(Math.Abs(1.087389652523947 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-0.716288046641012 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Atan(number);
            Assert.IsTrue(Math.Abs(1.087389652523947 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-0.716288046641012 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region AcotTest
        /// <summary>
        /// A test for Acot.
        /// </summary>
        [TestMethod]
        public void AcotTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Acot();
            Assert.IsTrue(Math.Abs(1.461461853857926 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-0.305943857905529 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Acot();
            Assert.IsTrue(Math.Abs(Constants.Pi + -0.078708436363295 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.795313458269654 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Acot();
            Assert.IsTrue(Math.Abs(Constants.Pi + -1.077084666894376 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.240948266464790 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Acot();
            Assert.IsTrue(Math.Abs(0.483406674270949 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(0.716288046641012 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Acot(number);
            Assert.IsTrue(Math.Abs(0.483406674270949 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(0.716288046641012 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region AsecTest
        /// <summary>
        /// A test for Asec.
        /// </summary>
        [TestMethod]
        public void AsecTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Asec();
            Assert.IsTrue(Math.Abs(1.263192677264185 - target.Real) < 1e-14, "1.1");
            Assert.IsTrue(Math.Abs(1.864161544157883 - target.Imag) < 1e-14, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Asec();
            Assert.IsTrue(Math.Abs(1.607663511869202 - target.Real) < 1e-14, "2.1");
            Assert.IsTrue(Math.Abs(0.623065008994206 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Asec();
            Assert.IsTrue(Math.Abs(2.517873627505525 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-1.200699546128141 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Asec();
            Assert.IsTrue(Math.Abs(1.329643731985686 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-0.678053948499639 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Asec(number);
            Assert.IsTrue(Math.Abs(1.329643731985686 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-0.678053948499639 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region AcosecTest
        /// <summary>
        /// A test for Acosec.
        /// </summary>
        [TestMethod]
        public void AcosecTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Acosec();
            Assert.IsTrue(Math.Abs(0.307603649530711 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-1.864161544157883 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Acosec();
            Assert.IsTrue(Math.Abs(-0.036867185074306 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.623065008994206 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Acosec();
            Assert.IsTrue(Math.Abs(-0.947077300710628 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(1.200699546128141 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Acosec();
            Assert.IsTrue(Math.Abs(0.241152594809211 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(0.678053948499639 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Acosec(number);
            Assert.IsTrue(Math.Abs(0.241152594809211 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(0.678053948499639 - target.Imag) < 1e-15, "5.2");
        }
        #endregion
        #endregion

        #region Trigonometric hyperbolic functions
        #region CoshTest
        /// <summary>
        /// A test for Cosh.
        /// </summary>
        [TestMethod]
        public void CoshTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Cosh();
            Assert.IsTrue(Math.Abs(0.960117153467032 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.029601298666459 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Cosh();
            Assert.IsTrue(Math.Abs(0.071091182512645 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.099915830969216 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Cosh();
            Assert.IsTrue(Math.Abs(1.077262230647136 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.153994192369766 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Cosh();
            Assert.IsTrue(Math.Abs(0.408604012641776 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-0.485681192234205 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Cosh(number);
            Assert.IsTrue(Math.Abs(0.408604012641776 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-0.485681192234205 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region SinhTest
        /// <summary>
        /// A test for Sinh.
        /// </summary>
        [TestMethod]
        public void SinhTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Sinh();
            Assert.IsTrue(Math.Abs(0.095692951291080 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.296999039439359 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Sinh();
            Assert.IsTrue(Math.Abs(-0.007085515596552 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(1.002486619151843 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Sinh();
            Assert.IsTrue(Math.Abs(-0.497821359650232 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.333236258274482 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Sinh();
            Assert.IsTrue(Math.Abs(0.188822924767051 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-1.050991473923866 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Sinh(number);
            Assert.IsTrue(Math.Abs(0.188822924767051 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-1.050991473923866 - target.Imag) < 1e-15, "4.2");
        }
        #endregion

        #region TanhTest
        /// <summary>
        /// A test for Tanh.
        /// </summary>
        [TestMethod]
        public void TanhTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Tanh();
            Assert.IsTrue(Math.Abs(0.109101411029079 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.305972552334617 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Tanh();
            Assert.IsTrue(Math.Abs(-6.694628865714378 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(4.692385204651136 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Tanh();
            Assert.IsTrue(Math.Abs(-0.496197065773508 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.238405083338123 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Tanh();
            Assert.IsTrue(Math.Abs(1.458632584854141 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-0.838369302507491 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Tanh(number);
            Assert.IsTrue(Math.Abs(1.458632584854141 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-0.838369302507491 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region CothTest
        /// <summary>
        /// A test for Coth.
        /// </summary>
        [TestMethod]
        public void CothTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Coth();
            Assert.IsTrue(Math.Abs(1.033917851082447 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-2.899600296789029 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Coth();
            Assert.IsTrue(Math.Abs(-0.100164212730932 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.070206889624792 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Coth();
            Assert.IsTrue(Math.Abs(-1.637351930074588 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.786689503563990 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Coth();
            Assert.IsTrue(Math.Abs(0.515331906039676 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(0.296194158222197 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Coth(number);
            Assert.IsTrue(Math.Abs(0.515331906039676 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(0.296194158222197 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region SechTest
        /// <summary>
        /// A test for Sech.
        /// </summary>
        [TestMethod]
        public void SechTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Sech();
            Assert.IsTrue(Math.Abs(1.040550471593828 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-0.032081132157620 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Sech();
            Assert.IsTrue(Math.Abs(4.727709664840343 - target.Real) < 1e-14, "2.1");
            Assert.IsTrue(Math.Abs(6.644607995649714 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Sech();
            Assert.IsTrue(Math.Abs(0.909689950634526 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.130039803930286 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Sech();
            Assert.IsTrue(Math.Abs(1.014299730743966 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(1.205632561769404 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Sech(number);
            Assert.IsTrue(Math.Abs(1.014299730743966 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(1.205632561769404 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region CosechTest
        /// <summary>
        /// A test for Cosech.
        /// </summary>
        [TestMethod]
        public void CosechTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Cosech();
            Assert.IsTrue(Math.Abs(0.982821247207556 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-3.050349711478128 - target.Imag) < 1e-14, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Cosech();
            Assert.IsTrue(Math.Abs(-0.007050056448563 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.997469719407415 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Cosech();
            Assert.IsTrue(Math.Abs(-1.387181647643423 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.928564459613600 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Cosech();
            Assert.IsTrue(Math.Abs(0.165599691781259 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(0.921730580972834 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Cosech(number);
            Assert.IsTrue(Math.Abs(0.165599691781259 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(0.921730580972834 - target.Imag) < 1e-15, "5.2");
        }
        #endregion
        #endregion

        #region Inverse trigonometric hyperbolic functions
        #region AcoshTest
        /// <summary>
        /// A test for Acosh.
        /// </summary>
        [TestMethod]
        public void AcoshTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Acosh();
            Assert.IsTrue(Math.Abs(0.296999023408390 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(1.474903367519443 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Acosh();
            Assert.IsTrue(Math.Abs(1.196042838552249 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(1.626235699131362 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Acosh();
            Assert.IsTrue(Math.Abs(0.334299817774938 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-2.063835567380515 - target.Imag) < 1e-15, "2.2");
            number = new Complex(0.5, -1.2);
            target = number.Acosh();
            Assert.IsTrue(Math.Abs(1.055304915437955 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-1.255110336239894 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Acosh(number);
            Assert.IsTrue(Math.Abs(1.055304915437955 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-1.255110336239894 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region AsinhTest
        /// <summary>
        /// A test for Asinh.
        /// </summary>
        [TestMethod]
        public void AsinhTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Asinh();
            Assert.IsTrue(Math.Abs(0.104581498839370 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.302981107347608 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Asinh();
            Assert.IsTrue(Math.Abs(-0.967727114204379 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(1.481869611649372 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Asinh();
            Assert.IsTrue(Math.Abs(-0.497902942830288 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.269555641424950 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Asinh();
            Assert.IsTrue(Math.Abs(0.864263503049688 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-1.032909408267385 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Asinh(number);
            Assert.IsTrue(Math.Abs(0.864263503049688 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-1.032909408267385 - target.Imag) < 1e-15, "4.2");
        }
        #endregion

        #region AtanhTest
        /// <summary>
        /// A test for Atanh.
        /// </summary>
        [TestMethod]
        public void AtanhTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Atanh();
            Assert.IsTrue(Math.Abs(0.091931195031329 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.294001301773784 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Atanh();
            Assert.IsTrue(Math.Abs(-0.030713418276336 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(0.984212159158513 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Atanh();
            Assert.IsTrue(Math.Abs(-0.482240147685385 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(-0.368907530060232 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Atanh();
            Assert.IsTrue(Math.Abs(0.195224482279363 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(-0.925373074659344 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Atanh(number);
            Assert.IsTrue(Math.Abs(0.195224482279363 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-0.925373074659344 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region AcothTest
        /// <summary>
        /// A test for Acoth.
        /// </summary>
        [TestMethod]
        public void AcothTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Acoth();
            Assert.IsTrue(Math.Abs(0.091931195031329 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-1.276795025021113 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Acoth();
            Assert.IsTrue(Math.Abs(-0.030713418276336 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.586584167636384 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Acoth();
            Assert.IsTrue(Math.Abs(-0.482240147685385 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(1.201888796734664 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Acoth();
            Assert.IsTrue(Math.Abs(0.195224482279363 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(0.645423252135553 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Acoth(number);
            Assert.IsTrue(Math.Abs(0.195224482279363 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(0.645423252135553 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region AsechTest
        /// <summary>
        /// A test for Asech.
        /// </summary>
        [TestMethod]
        public void AsechTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Asech();
            Assert.IsTrue(Math.Abs(1.864161544157883 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-1.263192677264185 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Asech();
            Assert.IsTrue(Math.Abs(0.623065008994206 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-1.607663511869202 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Asech();
            Assert.IsTrue(Math.Abs(1.200699546128141 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(2.517873627505525 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Asech();
            Assert.IsTrue(Math.Abs(0.678053948499639 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(1.329643731985686 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Asech(number);
            Assert.IsTrue(Math.Abs(0.678053948499639 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(1.329643731985686 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region AcosechTest
        /// <summary>
        /// A test for Acosech.
        /// </summary>
        [TestMethod]
        public void AcosechTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Acosech();
            Assert.IsTrue(Math.Abs(1.824198702193883 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-1.233095217529344 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Acosech();
            Assert.IsTrue(Math.Abs(-0.059040931232830 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-0.724233722076607 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Acosech();
            Assert.IsTrue(Math.Abs(-1.276772226623233 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.474289102065753 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Acosech();
            Assert.IsTrue(Math.Abs(0.384546338338748 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(0.721631195207434 - target.Imag) < 1e-15, "4.2");
            // Static
            target = Complex.Acosech(number);
            Assert.IsTrue(Math.Abs(0.384546338338748 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(0.721631195207434 - target.Imag) < 1e-15, "5.2");
        }
        #endregion
        #endregion

        #region Accessors

        #region J*
        //// Ïðîâåðêà êîíñòàíòû j
        //Complex c12 = Complex.J;
        //Assert.IsTrue (c12.Re == 0 && c12.Im == 1, c12.ToString ());

        //Complex c13 = new Complex(2, 3);
        //Complex c14 = c13 + Complex.J;
        //Assert.IsTrue (c14.Re == 2 && c14.Im == 4, c14.ToString ());
        #endregion

        #region ITest
        /// <summary>
        /// A test for ImaginaryOne.
        /// </summary>
        [TestMethod]
        public void Itest()
        {
            Complex target = Complex.ImaginaryOne;
            Assert.IsTrue(target.Real == 0 && target.Imag == 1, target.ToString());
        }
        #endregion

        #region ImagTest
        /// <summary>
        /// A test for Imag.
        /// </summary>
        [TestMethod]
        public void ImagTest()
        {
            var target = new Complex(12.3, 45.6);
            Assert.AreEqual(target.Imag, 45.6);
            target = new Complex(78.9);
            Assert.AreEqual(target.Imag, 0d);
        }
        #endregion

        #region RealTest
        /// <summary>
        /// A test for Real.
        /// </summary>
        [TestMethod]
        public void RealTest()
        {
            var target = new Complex(12.3, 45.6);
            Assert.AreEqual(target.Imag, 45.6);
            target = new Complex(78.9);
            Assert.AreEqual(target.Imag, 0d);
        }
        #endregion

        #region InfinityTest
        /// <summary>
        /// A test for Infinity.
        /// </summary>
        [TestMethod]
        public void InfinityTest()
        {
            Complex target = Complex.Infinity;
            Assert.IsTrue(double.IsPositiveInfinity(target.Real));
            Assert.IsTrue(double.IsPositiveInfinity(target.Imag));
            Assert.IsTrue(double.IsInfinity(target.Real));
            Assert.IsTrue(double.IsInfinity(target.Imag));
            Assert.IsFalse(double.IsNegativeInfinity(target.Real));
            Assert.IsFalse(double.IsNegativeInfinity(target.Imag));
            target = new Complex(1d, 2d);
            Assert.IsFalse(double.IsPositiveInfinity(target.Real));
            Assert.IsFalse(double.IsPositiveInfinity(target.Imag));
            Assert.IsFalse(double.IsInfinity(target.Real));
            Assert.IsFalse(double.IsInfinity(target.Imag));
            Assert.IsFalse(double.IsNegativeInfinity(target.Real));
            Assert.IsFalse(double.IsNegativeInfinity(target.Imag));
        }
        #endregion

        #region IsITest
        /// <summary>
        /// A test for IsI.
        /// </summary>
        [TestMethod]
        public void IsITest()
        {
            var target = new Complex(2d, 3d);
            Assert.IsFalse(target.IsImaginaryOne);
            target = Complex.ImaginaryOne;
            Assert.IsTrue(target.IsImaginaryOne);
            target = Complex.Infinity;
            Assert.IsFalse(target.IsImaginaryOne);
            target = Complex.NaN;
            Assert.IsFalse(target.IsImaginaryOne);
        }
        #endregion

        #region IsImaginaryTest
        /// <summary>
        /// A test for IsImaginary.
        /// </summary>
        [TestMethod]
        public void IsImaginaryTest()
        {
            var target = new Complex(2d, 3d);
            Assert.IsFalse(target.IsImaginary);
            target = new Complex(0d, 4d);
            Assert.IsTrue(target.IsImaginary);
            target = Complex.ImaginaryOne;
            Assert.IsTrue(target.IsImaginary);
            target = Complex.Infinity;
            Assert.IsFalse(target.IsImaginary);
            target = Complex.NaN;
            Assert.IsFalse(target.IsImaginary);
        }
        #endregion

        #region IsInfinityTest
        /// <summary>
        /// A test for IsInfinity.
        /// </summary>
        [TestMethod]
        public void IsInfinityTest()
        {
            var target = new Complex(2d, 3d);
            Assert.IsFalse(target.IsInfinity);
            target = Complex.ImaginaryOne;
            Assert.IsFalse(target.IsInfinity);
            target = Complex.Infinity;
            Assert.IsTrue(target.IsInfinity);
            target = Complex.NaN;
            Assert.IsFalse(target.IsInfinity);
        }
        #endregion

        #region IsNaNTest
        /// <summary>
        /// A test for IsNaN.
        /// </summary>
        [TestMethod]
        public void IsNaNTest()
        {
            var target = new Complex(2d, 3d);
            Assert.IsFalse(target.IsNaN);
            target = Complex.ImaginaryOne;
            Assert.IsFalse(target.IsNaN);
            target = Complex.NaN;
            Assert.IsTrue(target.IsNaN);
            target = Complex.Infinity;
            Assert.IsFalse(target.IsNaN);
        }
        #endregion

        #region IsOneTest
        /// <summary>
        /// A test for IsOne.
        /// </summary>
        [TestMethod]
        public void IsOneTest()
        {
            var target = new Complex(2d, 3d);
            Assert.IsFalse(target.IsOne);
            target = Complex.ImaginaryOne;
            Assert.IsFalse(target.IsOne);
            target = Complex.Infinity;
            Assert.IsFalse(target.IsOne);
            target = Complex.NaN;
            Assert.IsFalse(target.IsOne);
            target = Complex.One;
            Assert.IsTrue(target.IsOne);
        }
        #endregion

        #region IsRealTest
        /// <summary>
        /// A test for IsReal.
        /// </summary>
        [TestMethod]
        public void IsRealTest()
        {
            var target = new Complex(2d, 3d);
            Assert.IsFalse(target.IsReal);
            target = new Complex(2d, 0d);
            Assert.IsTrue(target.IsReal);
            target = new Complex(-2d, 0d);
            Assert.IsTrue(target.IsReal);
            target = Complex.ImaginaryOne;
            Assert.IsFalse(target.IsReal);
            target = Complex.Infinity;
            Assert.IsFalse(target.IsReal);
            target = Complex.NaN;
            Assert.IsFalse(target.IsReal);
            target = Complex.One;
            Assert.IsTrue(target.IsReal);
        }
        #endregion

        #region IsRealNonNegativeTest
        /// <summary>
        /// A test for IsRealNonNegative.
        /// </summary>
        [TestMethod]
        public void IsRealNonNegativeTest()
        {
            var target = new Complex(2d, 3d);
            Assert.IsFalse(target.IsRealNonNegative);
            target = new Complex(2d, 0d);
            Assert.IsTrue(target.IsRealNonNegative);
            target = new Complex(-2d, 0d);
            Assert.IsFalse(target.IsRealNonNegative);
            target = Complex.ImaginaryOne;
            Assert.IsFalse(target.IsRealNonNegative);
            target = Complex.Infinity;
            Assert.IsFalse(target.IsRealNonNegative);
            target = Complex.NaN;
            Assert.IsFalse(target.IsRealNonNegative);
            target = Complex.One;
            Assert.IsTrue(target.IsRealNonNegative);
            target = Complex.Zero;
            Assert.IsTrue(target.IsRealNonNegative);
        }
        #endregion

        #region IsZeroTest
        /// <summary>
        /// A test for IsZero.
        /// </summary>
        [TestMethod]
        public void IsZeroTest()
        {
            var target = new Complex(2d, 3d);
            Assert.IsFalse(target.IsZero);
            target = new Complex(2d, 0d);
            Assert.IsFalse(target.IsZero);
            target = new Complex(-2d, 0d);
            Assert.IsFalse(target.IsZero);
            target = Complex.ImaginaryOne;
            Assert.IsFalse(target.IsZero);
            target = Complex.Infinity;
            Assert.IsFalse(target.IsZero);
            target = Complex.NaN;
            Assert.IsFalse(target.IsZero);
            target = Complex.One;
            Assert.IsFalse(target.IsZero);
            target = Complex.Zero;
            Assert.IsTrue(target.IsZero);
            target = new Complex(0d, 0d);
            Assert.IsTrue(target.IsZero);
        }
        #endregion

        #region NaNTest
        /// <summary>
        /// A test for NaN.
        /// </summary>
        [TestMethod]
        public void NaNTest()
        {
            Complex target = Complex.NaN;
            Assert.IsTrue(target.IsNaN);
            Assert.IsTrue(double.IsNaN(target.Real));
            Assert.IsTrue(double.IsNaN(target.Imag));
            target = Complex.Infinity;
            Assert.IsFalse(target.IsNaN);
        }
        #endregion

        #region OneTest
        /// <summary>
        /// A test for One.
        /// </summary>
        [TestMethod]
        public void OneTest()
        {
            Complex target = Complex.NaN;
            Assert.IsFalse(target.IsOne);
            target = Complex.Infinity;
            Assert.IsFalse(target.IsOne);
            target = Complex.One;
            Assert.IsTrue(target.IsOne);
            Assert.IsFalse(double.IsNaN(target.Real));
            Assert.IsFalse(double.IsNaN(target.Imag));
            Assert.AreEqual(target.Real, 1d);
            Assert.AreEqual(target.Imag, 0d);
        }
        #endregion

        #region ZeroTest
        /// <summary>
        /// A test for Zero.
        /// </summary>
        [TestMethod]
        public void ZeroTest()
        {
            Complex target = Complex.NaN;
            Assert.IsFalse(target.IsZero);
            target = Complex.Infinity;
            Assert.IsFalse(target.IsZero);
            target = Complex.One;
            Assert.IsFalse(target.IsZero);
            target = Complex.Zero;
            Assert.IsTrue(target.IsZero);
            Assert.AreEqual(target.Real, 0d);
            Assert.AreEqual(target.Imag, 0d);
        }
        #endregion

        #region ArgumentTest
        /// <summary>
        /// A test for Argument.
        /// </summary>
        [TestMethod]
        public void ArgumentTest()
        {
            var target = new Complex(1d, 5d);
            Assert.IsTrue(target.Argument - 1.373 < 0.001);
            target = new Complex(1d, 1d);
            Assert.IsTrue(target.Argument - 0.785 < 0.001);
            target = new Complex(-1d, 1d);
            Assert.IsTrue(target.Argument - 2.356 < 0.001);
            target = new Complex(-1d, -1d);
            Assert.IsTrue(target.Argument - 3.927 < 0.001);
            target = new Complex(1d, -1d);
            Assert.IsTrue(target.Argument - 5.498 < 0.001);
        }
        #endregion

        #region ConjugateTest
        /// <summary>
        /// A test for Conjugate.
        /// </summary>
        [TestMethod]
        public void ConjugateTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Conjugate;
            Assert.IsTrue(Math.Abs(0.100000000000000 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-0.300000000000000 - target.Imag) < 1e-15, "1.2");
            number = new Complex(-0.1, 1.5);
            target = number.Conjugate;
            Assert.IsTrue(Math.Abs(-0.100000000000000 - target.Real) < 1e-15, "2.1");
            Assert.IsTrue(Math.Abs(-1.500000000000000 - target.Imag) < 1e-15, "2.2");
            number = new Complex(-0.5, -0.3);
            target = number.Conjugate;
            Assert.IsTrue(Math.Abs(-0.500000000000000 - target.Real) < 1e-15, "3.1");
            Assert.IsTrue(Math.Abs(0.300000000000000 - target.Imag) < 1e-15, "3.2");
            number = new Complex(0.5, -1.2);
            target = number.Conjugate;
            Assert.IsTrue(Math.Abs(0.500000000000000 - target.Real) < 1e-15, "4.1");
            Assert.IsTrue(Math.Abs(1.200000000000000 - target.Imag) < 1e-15, "4.2");
            number = new Complex(123.4, 567.8);
            target.Conjugate = number;
            Assert.IsTrue(Math.Abs(123.4 - target.Real) < 1e-15, "5.1");
            Assert.IsTrue(Math.Abs(-567.8 - target.Imag) < 1e-15, "5.2");
        }
        #endregion

        #region ModulusTest
        /// <summary>
        /// A test for Modulus.
        /// </summary>
        [TestMethod]
        public void ModulusTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            double target = number.Modulus;
            Assert.IsTrue(Math.Abs(0.316227766016838 - target) < 1e-15, "1.1");
            number = new Complex(-0.1, 1.5);
            target = number.Modulus;
            Assert.IsTrue(Math.Abs(1.503329637837291 - target) < 1e-15, "2.1");
            number = new Complex(-0.5, -0.3);
            target = number.Modulus;
            Assert.IsTrue(Math.Abs(0.583095189484530 - target) < 1e-15, "3.1");
            number = new Complex(0.5, -1.2);
            target = number.Modulus;
            Assert.IsTrue(Math.Abs(1.300000000000000 - target) < 1e-15, "4.1");
        }
        #endregion

        #region ModulusSquaredTest
        /// <summary>
        /// A test for ModulusSquared.
        /// </summary>
        [TestMethod]
        public void ModulusSquaredTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            double target = number.ModulusSquared;
            Assert.IsTrue(Math.Abs(0.100000000000000 - target) < 1e-15, "1.1");
            number = new Complex(-0.1, 1.5);
            target = number.ModulusSquared;
            Assert.IsTrue(Math.Abs(2.260000000000000 - target) < 1e-15, "2.1");
            number = new Complex(-0.5, -0.3);
            target = number.ModulusSquared;
            Assert.IsTrue(Math.Abs(0.340000000000000 - target) < 1e-15, "3.1");
            number = new Complex(0.5, -1.2);
            target = number.ModulusSquared;
            Assert.IsTrue(Math.Abs(1.690000000000000 - target) < 1e-15, "4.1");
        }
        #endregion

        #region SignTest
        /// <summary>
        /// A test for Sign.
        /// </summary>
        [TestMethod]
        public void SignTest()
        {
            // Matlab
            var number = new Complex(0.1, 0.3);
            Complex target = number.Sign;
            Assert.IsFalse(target.IsNaN);
            //Assert.IsTrue(Math.Abs(1.249045772398254 - target.Sign) < 1e-15, "1.1");
            number = new Complex(-0.1, 1.5);
            target = number.Sign;
            Assert.IsFalse(target.IsNaN);
            //Assert.IsTrue(Math.Abs(1.637364490570721 - target) < 1e-15, "2.1");
            number = new Complex(-0.5, -0.3);
            target = number.Sign;
            Assert.IsFalse(target.IsNaN);
            //Assert.IsTrue(Math.Abs(-2.601173153319209 - target) < 1e-15, "3.1");
            number = new Complex(0.5, -1.2);
            target = number.Sign;
            //Assert.IsTrue(Math.Abs(-1.176005207095135 - target) < 1e-15, "4.1");
            Assert.IsFalse(target.IsNaN);
        }
        #endregion
        #endregion

        #region Operations
        #region op_AdditionTest
        /// <summary>
        /// A test for op_Addition.
        /// </summary>
        [TestMethod]
        public void OpAdditionTest()
        {
            const double lhs = 1d;
            var rhs = new Complex(1d, 5d);
            Complex target = lhs + rhs;
            Assert.AreEqual(2d, target.Real);
            Assert.AreEqual(5d, target.Imag);
        }

        /// <summary>
        /// A test for op_Addition.
        /// </summary>
        [TestMethod]
        public void OpAdditionTest1()
        {
            const double rhs = 1d;
            var lhs = new Complex(5d, 7d);
            Complex target = lhs + rhs;
            Assert.AreEqual(6d, target.Real);
            Assert.AreEqual(7d, target.Imag);
        }

        /// <summary>
        /// A test for op_Addition.
        /// </summary>
        [TestMethod]
        public void OpAdditionTest2()
        {
            var lhs = new Complex(2d, 3d);
            var rhs = new Complex(1d, 5d);
            Complex target = lhs + rhs;
            Assert.AreEqual(3d, target.Real);
            Assert.AreEqual(8d, target.Imag);
        }
        #endregion

        #region op_DivisionTest
        /// <summary>
        /// A test for op_Division.
        /// </summary>
        [TestMethod]
        public void OpDivisionTest()
        {
            var lhs = new Complex(4d, 7d);
            const double rhs = 7.5;
            Complex target = (lhs / rhs);
            Assert.IsTrue(Math.Abs(0.533333333333333 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(0.933333333333333 - target.Imag) < 1e-15, "1.2");
        }

        /// <summary>
        /// A test for op_Division.
        /// </summary>
        [TestMethod]
        public void OpDivisionTest1()
        {
            const double lhs = 7.5;
            var rhs = new Complex(4d, 7d);
            Complex target = (lhs / rhs);
            Assert.IsTrue(Math.Abs(0.461538461538462 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-0.807692307692308 - target.Imag) < 1e-15, "1.2");
        }

        /// <summary>
        /// A test for op_Division.
        /// </summary>
        [TestMethod]
        public void OpDivisionTest2()
        {
            var lhs = new Complex(3d, 5d);
            var rhs = new Complex(4d, 7d);
            Complex target = (lhs / rhs);
            Assert.IsTrue(Math.Abs(0.723076923076923 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(-0.015384615384615 - target.Imag) < 1e-15, "1.2");
        }
        #endregion

        #region op_EqualityTest
        /// <summary>
        /// A test for op_Equality.
        /// </summary>
        [TestMethod]
        public void OpEqualityTest()
        {
            var lhs = new Complex(3d, 7d);
            var rhs = new Complex(3d, 7d);
            Assert.AreEqual(true, (lhs == rhs));
            rhs = new Complex(3d, 6d);
            Assert.AreEqual(false, (lhs == rhs));
            rhs = new Complex(4d, 7d);
            Assert.AreEqual(false, (lhs == rhs));
            rhs = new Complex(2d, 1d);
            Assert.AreEqual(false, (lhs == rhs));
        }

        /// <summary>
        /// A test for op_Equality.
        /// </summary>
        [TestMethod]
        public void OpEqualityTest1()
        {
            const double lhs = 4.5;
            var rhs = new Complex(4.5, 0d);
            Assert.AreEqual(true, (lhs == rhs));
            rhs = new Complex(4.5, 6d);
            Assert.AreEqual(false, (lhs == rhs));
            rhs = new Complex(8d, 0d);
            Assert.AreEqual(false, (lhs == rhs));
            rhs = new Complex(7d, 6d);
            Assert.AreEqual(false, (lhs == rhs));
        }

        /// <summary>
        /// A test for op_Equality.
        /// </summary>
        [TestMethod]
        public void OpEqualityTest2()
        {
            var lhs = new Complex(4.5d, 0d);
            double rhs = 4.5;
            Assert.AreEqual(true, (lhs == rhs));
            rhs = 5.4;
            Assert.AreEqual(false, (lhs == rhs));
        }
        #endregion

        #region op_ImplicitTest
        /// <summary>
        /// A test for op_Implicit.
        /// </summary>
        [TestMethod]
        public void OpImplicitTest()
        {
            Complex target = 6.34;
            Assert.AreEqual(6.34, target.Real);
            Assert.AreEqual(0d, target.Imag);
        }
        #endregion

        #region op_InequalityTest
        /// <summary>
        /// A test for op_Inequality.
        /// </summary>
        [TestMethod]
        public void OpInequalityTest()
        {
            var lhs = new Complex(3d, 7d);
            var rhs = new Complex(3d, 7d);
            Assert.AreEqual(false, (lhs != rhs));
            rhs = new Complex(3d, 6d);
            Assert.AreEqual(true, (lhs != rhs));
            rhs = new Complex(4d, 7d);
            Assert.AreEqual(true, (lhs != rhs));
            rhs = new Complex(2d, 1d);
            Assert.AreEqual(true, (lhs != rhs));
        }

        /// <summary>
        /// A test for op_Inequality.
        /// </summary>
        [TestMethod]
        public void OpInequalityTest1()
        {
            const double lhs = 4.5;
            var rhs = new Complex(4.5, 0d);
            Assert.AreEqual(false, (lhs != rhs));
            rhs = new Complex(4.5, 6d);
            Assert.AreEqual(true, (lhs != rhs));
            rhs = new Complex(8d, 0d);
            Assert.AreEqual(true, (lhs != rhs));
            rhs = new Complex(7d, 6d);
            Assert.AreEqual(true, (lhs != rhs));
        }

        /// <summary>
        /// A test for op_Inequality.
        /// </summary>
        [TestMethod]
        public void OpInequalityTest2()
        {
            var lhs = new Complex(4.5d, 0d);
            double rhs = 4.5;
            Assert.AreEqual(false, (lhs != rhs));
            rhs = 5.4;
            Assert.AreEqual(true, (lhs != rhs));
        }
        #endregion

        #region op_MultiplyTest
        /// <summary>
        /// A test for op_Multiply.
        /// </summary>
        [TestMethod]
        public void OpMultiplyTest()
        {
            var lhs = new Complex(4d, 7d);
            const double rhs = 7.5;
            Complex target = (lhs * rhs);
            Assert.IsTrue(Math.Abs(30.000000000000000 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(52.500000000000000 - target.Imag) < 1e-15, "1.2");
        }

        /// <summary>
        /// A test for op_Multiply.
        /// </summary>
        [TestMethod]
        public void OpMultiplyTest1()
        {
            const double lhs = 7.5;
            var rhs = new Complex(4d, 7d);
            Complex target = (lhs * rhs);
            Assert.IsTrue(Math.Abs(30.000000000000000 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(52.500000000000000 - target.Imag) < 1e-15, "1.2");
        }

        /// <summary>
        /// A test for op_Multiply.
        /// </summary>
        [TestMethod]
        public void OpMultiplyTest2()
        {
            var lhs = new Complex(3d, 5d);
            var rhs = new Complex(4d, 7d);
            Complex target = (lhs * rhs);
            Assert.IsTrue(Math.Abs(-23.000000000000000 - target.Real) < 1e-15, "1.1");
            Assert.IsTrue(Math.Abs(41.000000000000000 - target.Imag) < 1e-15, "1.2");
        }
        #endregion

        #region op_SubtractionTest
        /// <summary>
        /// A test for op_Subtraction.
        /// </summary>
        [TestMethod]
        public void OpSubtractionTest()
        {
            const double lhs = 1d;
            var rhs = new Complex(2d, 5d);
            Complex target = lhs - rhs;
            Assert.AreEqual(-1d, target.Real);
            Assert.AreEqual(-5d, target.Imag);
        }

        /// <summary>
        /// A test for op_Subtraction.
        /// </summary>
        [TestMethod]
        public void OpSubtractionTest1()
        {
            const double rhs = 1d;
            var lhs = new Complex(5d, 7d);
            Complex target = lhs - rhs;
            Assert.AreEqual(4d, target.Real);
            Assert.AreEqual(7d, target.Imag);
        }

        /// <summary>
        /// A test for op_Subtraction.
        /// </summary>
        [TestMethod]
        public void OpSubtractionTest2()
        {
            var lhs = new Complex(2d, 3d);
            var rhs = new Complex(1d, 5d);
            Complex target = lhs - rhs;
            Assert.AreEqual(1d, target.Real);
            Assert.AreEqual(-2d, target.Imag);
        }
        #endregion

        #region op_UnaryNegationTest
        /// <summary>
        /// A test for op_UnaryNegation.
        /// </summary>
        [TestMethod]
        public void OpUnaryNegationTest()
        {
            var subtrahend = new Complex(2d, 3d);
            Complex target = -(subtrahend);
            Assert.AreEqual(-2d, target.Real);
            Assert.AreEqual(-3d, target.Imag);
            subtrahend = new Complex(-2d, -3d);
            target = -(subtrahend);
            Assert.AreEqual(2d, target.Real);
            Assert.AreEqual(3d, target.Imag);
        }
        #endregion

        #region op_UnaryPlusTest
        /// <summary>
        /// A test for op_UnaryPlus.
        /// </summary>
        [TestMethod]
        public void OpUnaryPlusTest()
        {
            var subtrahend = new Complex(2d, 3d);
            Complex target = +(subtrahend);
            Assert.AreEqual(2d, target.Real);
            Assert.AreEqual(3d, target.Imag);
            subtrahend = new Complex(-2d, -3d);
            target = +(subtrahend);
            Assert.AreEqual(-2d, target.Real);
            Assert.AreEqual(-3d, target.Imag);
        }
        #endregion
        #endregion

        #region ToStringTest
        /// <summary>
        /// A test for ToString.
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            var target = new Complex(0d, double.PositiveInfinity);
            Assert.AreEqual("Infinity", target.ToString());
            target = new Complex(0d, double.NegativeInfinity);
            Assert.AreEqual("Infinity", target.ToString());
            target = new Complex(0d, 1d);
            Assert.AreEqual("i", target.ToString());
            target = new Complex(0d, -1d);
            Assert.AreEqual("-i", target.ToString());
            target = new Complex(1d, 0d);
            Assert.AreEqual("1", target.ToString());
            target = new Complex(-1d, 0d);
            Assert.AreEqual("-1", target.ToString());
            target = new Complex(1d, 1d);
            Assert.AreEqual("1+i", target.ToString());
            target = new Complex(-1d, 1d);
            Assert.AreEqual("-1+i", target.ToString());
            target = new Complex(1d, -1d);
            Assert.AreEqual("1-i", target.ToString());
            target = new Complex(-1d, -1d);
            Assert.AreEqual("-1-i", target.ToString());
            target = new Complex(1d, 2d);
            Assert.AreEqual("1+2i", target.ToString());
            target = new Complex(-1d, 2d);
            Assert.AreEqual("-1+2i", target.ToString());
            target = new Complex(1d, -2d);
            Assert.AreEqual("1-2i", target.ToString());
            target = new Complex(-1d, -2d);
            Assert.AreEqual("-1-2i", target.ToString());
        }

        /// <summary>
        /// A test for ToString.
        /// </summary>
        [TestMethod]
        public void ToStringTest1()
        {
            var target = new Complex(0d, double.PositiveInfinity);
            Assert.AreEqual("Infinity", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(0d, double.NegativeInfinity);
            Assert.AreEqual("Infinity", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(0d, 1d);
            Assert.AreEqual("i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(0d, -1d);
            Assert.AreEqual("-i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, 0d);
            Assert.AreEqual("1", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, 0d);
            Assert.AreEqual("-1", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, 1d);
            Assert.AreEqual("1+i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, 1d);
            Assert.AreEqual("-1+i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, -1d);
            Assert.AreEqual("1-i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, -1d);
            Assert.AreEqual("-1-i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, 2d);
            Assert.AreEqual("1+2i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, 2d);
            Assert.AreEqual("-1+2i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, -2d);
            Assert.AreEqual("1-2i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, -2d);
            Assert.AreEqual("-1-2i", target.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }

        /// <summary>
        /// A test for ToString.
        /// </summary>
        [TestMethod]
        public void ToStringTest2()
        {
            var target = new Complex(0d, double.PositiveInfinity);
            Assert.AreEqual("Infinity", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(0d, double.NegativeInfinity);
            Assert.AreEqual("Infinity", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(0d, 1d);
            Assert.AreEqual("i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(0d, -1d);
            Assert.AreEqual("-i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, 0d);
            Assert.AreEqual("1.0e+00", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, 0d);
            Assert.AreEqual("-1.0e+00", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, 1d);
            Assert.AreEqual("1.0e+00+i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, 1d);
            Assert.AreEqual("-1.0e+00+i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, -1d);
            Assert.AreEqual("1.0e+00-i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, -1d);
            Assert.AreEqual("-1.0e+00-i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, 2d);
            Assert.AreEqual("1.0e+00+2.0e+00i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, 2d);
            Assert.AreEqual("-1.0e+00+2.0e+00i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(1d, -2d);
            Assert.AreEqual("1.0e+00-2.0e+00i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
            target = new Complex(-1d, -2d);
            Assert.AreEqual("-1.0e+00-2.0e+00i", target.ToString("0.0##e+00", CultureInfo.InvariantCulture.NumberFormat));
        }

        /// <summary>
        /// A test for ToString.
        /// </summary>
        [TestMethod]
        public void ToStringTest3()
        {
            var target = new Complex(0d, double.PositiveInfinity);
            Assert.AreEqual("Infinity", target.ToString("0.0##e+00"));
            target = new Complex(0d, double.NegativeInfinity);
            Assert.AreEqual("Infinity", target.ToString("0.0##e+00"));
            target = new Complex(0d, 1d);
            Assert.AreEqual("i", target.ToString("0.0##e+00"));
            target = new Complex(0d, -1d);
            Assert.AreEqual("-i", target.ToString("0.0##e+00"));
            target = new Complex(1d, 0d);
            Assert.AreEqual("1,0e+00", target.ToString("0.0##e+00"));
            target = new Complex(-1d, 0d);
            Assert.AreEqual("-1,0e+00", target.ToString("0.0##e+00"));
            target = new Complex(1d, 1d);
            Assert.AreEqual("1,0e+00+i", target.ToString("0.0##e+00"));
            target = new Complex(-1d, 1d);
            Assert.AreEqual("-1,0e+00+i", target.ToString("0.0##e+00"));
            target = new Complex(1d, -1d);
            Assert.AreEqual("1,0e+00-i", target.ToString("0.0##e+00"));
            target = new Complex(-1d, -1d);
            Assert.AreEqual("-1,0e+00-i", target.ToString("0.0##e+00"));
            target = new Complex(1d, 2d);
            Assert.AreEqual("1,0e+00+2,0e+00i", target.ToString("0.0##e+00"));
            target = new Complex(-1d, 2d);
            Assert.AreEqual("-1,0e+00+2,0e+00i", target.ToString("0.0##e+00"));
            target = new Complex(1d, -2d);
            Assert.AreEqual("1,0e+00-2,0e+00i", target.ToString("0.0##e+00"));
            target = new Complex(-1d, -2d);
            Assert.AreEqual("-1,0e+00-2,0e+00i", target.ToString("0.0##e+00"));
        }
        #endregion

        #region EqualsTest
        /// <summary>
        /// A test for Equals.
        /// </summary>
        [TestMethod]
        public void EqualsTest()
        {
            var lhs = new Complex(3d, 7d);
            var rhs = new Complex(3d, 7d);
            Assert.AreEqual(true, (lhs.Equals(rhs)));
            rhs = new Complex(3d, 6d);
            Assert.AreEqual(false, (lhs.Equals(rhs)));
            rhs = new Complex(4d, 7d);
            Assert.AreEqual(false, (lhs.Equals(rhs)));
            rhs = new Complex(2d, 1d);
            Assert.AreEqual(false, (lhs.Equals(rhs)));
        }

        /// <summary>
        /// A test for Equals.
        /// </summary>
        [TestMethod]
        public void EqualsTest1()
        {
            var lhs = new Complex(3d, 7d);
            var rhs = new Complex(3d, 7d);
            Assert.AreEqual(true, (lhs.Equals(rhs)));
            rhs = new Complex(3d, 6d);
            Assert.AreEqual(false, (lhs.Equals(rhs)));
            rhs = new Complex(4d, 7d);
            Assert.AreEqual(false, (lhs.Equals(rhs)));
            rhs = new Complex(2d, 1d);
            Assert.AreEqual(false, (lhs.Equals(rhs)));
        }
        #endregion

        #region CompareToTest
        /// <summary>
        /// A test for CompareTo.
        /// </summary>
        [TestMethod]
        public void CompareToTest()
        {
            Complex target = Complex.FromModulusArgument(0.456, 0.345);
            Complex other = Complex.FromModulusArgument(0.457, 0.345);
            Assert.IsTrue(target.CompareTo(other) < 0);
            other = Complex.FromModulusArgument(0.455, 0.345);
            Assert.IsTrue(target.CompareTo(other) > 0);
            other = Complex.FromModulusArgument(0.456, 0.346);
            Assert.IsTrue(target.CompareTo(other) < 0);
            other = Complex.FromModulusArgument(0.456, 0.344);
            Assert.IsTrue(target.CompareTo(other) > 0);
            other = Complex.FromModulusArgument(0.456, 0.345);
            Assert.IsTrue(target.CompareTo(other) == 0);
        }
        #endregion

        #region GetHashCodeTest
        /// <summary>
        /// A test for GetHashCode.
        /// </summary>
        [TestMethod]
        public void GetHashCodeTest()
        {
            var target = new Complex(0d, 0d);
            Assert.AreEqual(0d, target.GetHashCode());
            Assert.AreNotEqual(Complex.One.GetHashCode(), Complex.ImaginaryOne.GetHashCode(), "1");
            Assert.AreNotEqual(Complex.One.GetHashCode(), (-Complex.ImaginaryOne).GetHashCode(), "2");
            Assert.AreNotEqual((-Complex.One).GetHashCode(), Complex.ImaginaryOne.GetHashCode(), "3");
            Assert.AreNotEqual((-Complex.One).GetHashCode(), (-Complex.ImaginaryOne).GetHashCode(), "4");
        }
        #endregion

        #region SerializationTest
        private static void SerializeTo(Complex instance, string fileName)
        {
            var dcs = new DataContractSerializer(typeof(Complex), null, 65536, false, true, null);
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                dcs.WriteObject(fs, instance);
                fs.Close();
            }
        }

        private static Complex DeserializeFrom(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            var ser = new DataContractSerializer(typeof(Complex), null, 65536, false, true, null);
            var instance = (Complex)ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();
            return instance;
        }

        /// <summary>
        ///A test for the serialization
        ///</summary>
        [TestMethod]
        public void SerializationTest()
        {
            var source = new Complex(1.2, 1.3);
            const string fileName = "ComplexTest_1.xml";
            SerializeTo(source, fileName);
            Complex target = DeserializeFrom(fileName);
            Assert.AreEqual(1.2, target.Real);
            Assert.AreEqual(1.3, target.Imag);
            //FileInfo fi = new FileInfo(fileName);
            //fi.Delete();
        }
        #endregion
    }
}
