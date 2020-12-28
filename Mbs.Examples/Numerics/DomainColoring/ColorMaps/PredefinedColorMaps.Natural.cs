using System;
using System.Runtime.CompilerServices;
using Mbs.Numerics;

namespace DomainColoring.ColorMaps
{
    /// <summary>
    /// Implements natural color mapping.
    /// </summary>
    internal static partial class PredefinedColorMaps
    {
        /// <summary>
        /// 10 × 2⁻⁵², the default relative accuracy.
        /// </summary>
        private static readonly double DefaultRelativeAccuracy = 10 * PositiveEpsilonOf(1d);

        /// <summary>
        /// Transform complex numbers to HSV values. Get H(hue) value by finding the angle of z.
        /// Get V value by tanh(|z|), use tanh to ensure V in [0,1].
        /// V is set to 1, but for special cases, 0 for infinities, 0.5 for NaNs.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Natural(Complex z)
        {
            if (z.IsInfinity || z.IsNaN)
            {
                return 0;
            }

            // Extract a phase 0 ≤ θ < 2π.
            double t = z.Argument;
            while (t < 0d)
            {
                t += Constants.TwoPi;
            }

            while (t >= Constants.TwoPi)
            {
                t -= Constants.TwoPi;
            }

            // The hue is determined by the phase.
            double h = t / Constants.TwoPi;

            // Extract a magnitude m ≥ 0.
            double m = Complex.Abs(z);
            double s, v;
            if (Math.Abs(m) < DefaultRelativeAccuracy)
            {
                s = 0d;
                v = 0d;
            }
            else if (double.IsInfinity(m) || AlmostEqual(m, double.MaxValue))
            {
                s = 1d;
                v = 1d;
            }
            else
            {
                // Map the magnitude logarithmically into the repeating interval 0 < r < 1.
                // This is essentially where we are between contour lines.
                double r0 = 0d;
                double r1 = 0.1;
                while (m > r1)
                {
                    r0 = r1;
                    r1 *= Constants.GoldenRatio;
                }

                double r = (m - r0) / (r1 - r0);

                // This puts contour lines at 0, 1, φ, φ², φ³, …

                // Determine saturation and value based on r.
                // p is a distances from a contour line.
                double p = r < 0.5 ? 2d * r : 2d * (1d - r);

                // Let p go very fast to zero; this keeps the contour lines from getting thick.
                r = p * p * p;
                p = 1d - r * r * r;
                s = 0.5 + 0.5 * p;
                v = Math.Tanh(m);
            }

            return HsvToBrga32(h, s, v);
        }

        /// <summary>
        /// Evaluates the positive epsilon (ε), the minimum distance to the nearest bigger distinguishable floating
        /// point number near the argument value. The negative epsilon is equal to ½ times this positive epsilon.
        /// </summary>
        /// <param name="value">The value to calculate the positive epsilon from.</param>
        /// <returns>The positive epsilon value (positive double or NaN).</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double PositiveEpsilonOf(this double value)
        {
            return 2 * NegativeEpsilonOf(value);
        }

        /// <summary>
        /// Evaluates the negative epsilon (ε), the minimum distance to the nearest smaller distinguishable floating
        /// point number near the argument value. The positive epsilon is equal to 2 times this negative epsilon.
        /// </summary>
        /// <param name="value">The value to evaluate the negative epsilon from.</param>
        /// <returns>The negative epsilon value (positive double or NaN).</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double NegativeEpsilonOf(this double value)
        {
            if (double.IsInfinity(value) || double.IsNaN(value))
            {
                return double.NaN;
            }

            long signed64 = BitConverter.DoubleToInt64Bits(value);
            if (signed64 == 0)
            {
                return BitConverter.Int64BitsToDouble(++signed64) - value;
            }

            if (signed64-- < 0)
            {
                return BitConverter.Int64BitsToDouble(signed64) - value;
            }

            return value - BitConverter.Int64BitsToDouble(signed64);
        }

        /// <summary>
        /// Checks whether two given floating point numbers are almost equal.
        /// </summary>
        /// <param name="first">The first floating point number.</param>
        /// <param name="second">The second floating point number.</param>
        /// <returns>Whether two given floating point numbers are almost equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AlmostEqual(double first, double second)
        {
            return AlmostEqualNorm(first, second, first - second, DefaultRelativeAccuracy);
        }

        /// <summary>
        /// Checks whether two floating point numbers are almost equal.
        /// </summary>
        /// <param name="first">The first floating point number.</param>
        /// <param name="second">The second floating point number.</param>
        /// <param name="difference">The difference of the two numbers according to the Norm.</param>
        /// <param name="relativeAccuracy">The relative accuracy required for being almost equal.</param>
        /// <returns>Whether two given floating point numbers are almost equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AlmostEqualNorm(double first, double second, double difference, double relativeAccuracy)
        {
            if ((first == 0 && Math.Abs(second) < relativeAccuracy) || (second == 0 && Math.Abs(first) < relativeAccuracy))
            {
                return true;
            }

            return Math.Abs(difference) < relativeAccuracy * Math.Max(Math.Abs(first), Math.Abs(second));
        }
    }
}
