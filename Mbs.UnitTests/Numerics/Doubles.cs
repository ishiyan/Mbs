using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Numerics
{
    /// <summary>
    /// Double-precision floating point comparison utilities.
    /// </summary>
    internal static class Doubles
    {
        public static void AreEqual(double expected, double actual, string message)
        {
            AreEqual(expected, actual, double.NaN, message);
        }

        public static void AreEqual(double expected, double actual, double tolerance, string message)
        {
            bool failed = false;

            if (double.IsNaN(actual))
            {
                if (double.IsNaN(expected))
                {
                    return;
                }

                failed = true;
            }
            else if (double.IsNaN(expected))
            {
                failed = true;
            }
            else if (double.IsNegativeInfinity(actual))
            {
                if (double.IsNegativeInfinity(expected))
                {
                    return;
                }

                failed = true;
            }
            else if (double.IsNegativeInfinity(expected))
            {
                failed = true;
            }
            else if (double.IsPositiveInfinity(actual))
            {
                if (double.IsPositiveInfinity(expected))
                {
                    return;
                }

                failed = true;
            }
            else if (double.IsPositiveInfinity(expected))
            {
                failed = true;
            }

            if (failed)
            {
                Assert.Fail($"Expected value <{expected}>, actual value <{actual}>.".ToString(CultureInfo.InvariantCulture));
            }

            if (double.IsNaN(tolerance))
            {
                // How many precise figures do we have after the decimal point?
                // We need to know the distance of the most significant digit from the decimal point, right?
                // The magnitude. We can get this with a Log10.
                // Then we need to divide 1 by 10 ^ precision to get a value around the precision we want.

                // Log10(100) = 2, so to get the manitude we add 1.
                int magnitude = 1 + (Math.Abs(expected) < double.Epsilon ? -1 : Convert.ToInt32(Math.Floor(Math.Log10(Math.Abs(expected)))));
                int precision = 15 - magnitude;
                tolerance = 1.0 / Math.Pow(10, precision);
            }

            Assert.AreEqual(expected, actual, tolerance, message);
        }

        public static double SafeRatio(double a, double b)
        {
            if (double.IsNaN(a) && double.IsNaN(b))
            {
                return 1d;
            }

            if (double.IsNegativeInfinity(a) && double.IsNegativeInfinity(b))
            {
                return 1d;
            }

            if (double.IsPositiveInfinity(a) && double.IsPositiveInfinity(b))
            {
                return 1d;
            }

            if (double.IsInfinity(a) && double.IsInfinity(b))
            {
                return 1d;
            }

            if (Math.Abs(b) < double.Epsilon)
            {
                return Math.Abs(a) < double.Epsilon ? 1d : double.MaxValue;
            }

            return Math.Abs(a / b);
        }
    }
}
