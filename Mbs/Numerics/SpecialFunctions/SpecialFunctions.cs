using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics
{
    /// <summary>
    /// Contains assorted special functions.
    /// </summary>
    public static partial class SpecialFunctions
    {
        private const int IterationLimit = 250000;
        private static readonly double Dmax = Convert.ToDouble(decimal.MaxValue);

        /// <summary>
        /// Double dedicates 52 bits to the magnitude of the mantissa, so 2⁻⁵² is the smallest fraction difference
        /// it can detect; in order to avoid any funny effects at the margin, we only try for 2⁻⁵⁰.
        /// </summary>
        public static readonly double Accuracy = Math.Pow(2.0, -50);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Sin(double x, double y)
        {
            // Sin that is accurate for large arguments.
            return Math.Sin(Reduce(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Cos(double x, double y)
        {
            // Cos that is accurate for large arguments.
            return Math.Cos(Reduce(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Reduce(double x, double y)
        {
            // Reduces an argument to its corresponding argument in <c>[-2π, 2π]</c>.
            double t = x + Constants.TwoPi * y;
            if (Math.Abs(t) < 64d || Math.Abs(t) > Dmax)
            {
                // If the argument is small we don't need the high accurary reduction.
                // If the argument is too big, we can't do the high accuracy reduction
                // because it would overflow a decimal vairable.
                return t;
            }

            // Otherwise, convert to decimal, subtract a multiple of 2π, and return.
            const decimal decimalTwoPi = 2m * 3.1415926535897932384626433832795028841971693993751m;

            // Reduce x by factors of 2π
            decimal dx = Convert.ToDecimal(x);
            decimal dn = decimal.Truncate(dx / decimalTwoPi);
            dx -= dn * decimalTwoPi;

            // Reduce y by factors of 1.
            decimal dy = Convert.ToDecimal(y);
            decimal dm = decimal.Truncate(dy / 1m);
            dy -= dm * 1m;

            // Form the argument.
            decimal dt = dx + dy * decimalTwoPi;
            return Convert.ToDouble(dt);
        }

        /// <summary>
        /// Evaluates the Chebyshev series for a real argument.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <param name="coefficients">Chebyshev series coefficients.</param>
        /// <returns>The value of the Chebyshev series for the specified argument.</returns>
        internal static double EvaluateChebyshevSeries(double x, double[] coefficients)
        {
            int degree = coefficients.Length - 1;
            if (degree == 0)
                return 0.5 * coefficients[0];
            double xx = x + x;
            double c = coefficients[degree];
            double q = 0d;
            for (int i = degree - 1; i > 0; --i)
            {
                double temp = c;
                c = xx * c - q + coefficients[i];
                q = temp;
            }

            return x * c - q + 0.5 * coefficients[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double EvaluateChebyshevSeries(double x, double[] coefficients, int order, double a, double b)
        {
            double d = 0d, dd = 0d;
            double y = (2d * x - a - b) / (b - a);
            double y2 = 2 * y;
            for (int j = order; j >= 1; --j)
            {
                double temp = d;
                d = y2 * d - dd + coefficients[j];
                dd = temp;
            }

            return y * d - dd + 0.5 * coefficients[0];
        }

        /// <summary>
        /// The Taylor coefficient of the specified degree for a given argument.
        /// </summary>
        /// <param name="degree">The degree of the coefficient.</param>
        /// <param name="x">A real number.</param>
        /// <returns>The Taylor coefficient of the specified degree.</returns>
        public static double TaylorCoefficient(int degree, double x)
        {
            if (x < 0.0 || degree < 0)
                return double.NaN;
            if (degree == 0)
                return 1d;
            if (degree == 1)
                return x;
            if (degree < 170)
                return ElementaryFunctions.Pow(x, degree) / Factorial(degree);
            double q = Math.Exp(degree * Math.Log(Math.Abs(x)) - LnFactorial(degree));
            return x < 0d && degree % 2 != 0 ? -q : q;
        }
    }
}
