using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics
{
    /// <summary>
    /// Contains assorted special functions.
    /// </summary>
    public static partial class SpecialFunctions
    {
        /// <summary>
        /// The Dawson integral (also called the Dawson function) is
        /// <para><c>D₊(x)=e⁻ˣˆ²∫ͯ˳eᵗˆ²dt</c>.</para>
        /// <para>It is related to the error function for purely imaginary arguments:</para>
        /// <para><c>D₊(x) = ½√̅πe⁻ˣˆ²erfi(x) = -½i√̅πe⁻ˣˆ²erf(ix)</c>,</para>
        /// <para>where <c>erfi</c> is the imaginary error function, <c>erfi(x) = −i erf(ix)</c>.</para>
        /// </summary>
        /// <param name="x">The argument.</param>
        /// <returns>The value of <c>D₊(x)</c>.</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Dawson_function"/>
        public static double Dawson(double x)
        {
            if (x < 0d)
            {
                // Dawson is an odd function.
                return -Dawson(-x);
            }

            if (x < 1d)
            {
                // Use the series expansion near the origin.
                return DawsonSeries(x);
            }

            if (x > 10d)
            {
                // Use the asymptotic expansion for large values.
                return DawsonAsymptotic(x);
            }

            // Use the Rybicki algorithm in between.
            return DawsonRybicki(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double DawsonSeries(double x)
        {
            // A series expansion for the Dawson integral near the origin.
            // Requires ~30 terms at x~2, ~20 terms at x~1, ~10 terms at x~0.5.</para>
            double xx = -2d * x * x;
            double df = x;
            double f = df;
            for (int k = 1; k < IterationLimit; ++k)
            {
                double fPrev = f;
                df *= xx / (2 * k + 1);
                f += df;
                if (Math.Abs(f - fPrev) < double.Epsilon)
                    return f;
            }

            return double.NaN; // throw new NonConvergenceException(IterationLimit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double DawsonAsymptotic(double x)
        {
            // An asymptotic expansion for the Dawson integral from large values.
            // Requires ~5 terms at x~50, ~15 terms at x~10, fails to converge below x~7.</para>
            double xx = 2d * x * x;
            double df = 0.5 / x;
            double f = df;
            for (int k = 0; k < IterationLimit; ++k)
            {
                double fPrev = f;
                df *= (2 * k + 1) / xx;
                f += df;
                if (Math.Abs(f - fPrev) < double.Epsilon)
                    return f;
            }

            return double.NaN; // throw new NonConvergenceException(IterationLimit);
        }

        private const double DawsonRybickiH = 0.25;
        private const int DawsonRybickiCoefficientsLen = 16;
        private static readonly double[] DawsonRybickiCoefficients = ComputeDawsonRybickiCoefficients(0.25, DawsonRybickiCoefficientsLen);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double[] ComputeDawsonRybickiCoefficients(double h, int n)
        {
            // Pre-computes e⁻⁽ʰᵐ⁾ˆ² for Rybicki algorithm.
            var coefficients = new double[n];
            for (int k = 0; k < n; ++k)
            {
                int m = 2 * k + 1;
                double z = h * m;
                coefficients[k] = Math.Exp(-z * z);
            }

            return coefficients;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double DawsonRybicki(double x)
        {
            // An algorithm by Rybicki, described in Numerical Recipies.
            // <para>It expresses Dawson's integral as a sum over exponentially supressed terms with a suppression factor h.</para>
            // <para>It converges in the limit h->0 and the error for non-zero h goes like exp(-(π/2h)²); since the error
            // goes down exponentially with h, even quite moderate value of h give good results: we use h~0.25 to get full precision
            // the series is infinite, but since the terms die off exponentially you don't need many to get full precision: we need ~12 x 2</para>
            int n0 = 2 * ((int)Math.Round(x / DawsonRybickiH / 2d));
            double x0 = n0 * DawsonRybickiH;
            double y = x - x0;
            double f = 0d;
            double b = Math.Exp(2d * DawsonRybickiH * y);
            double bb = b * b;
            for (int k = 0; k < DawsonRybickiCoefficientsLen; ++k)
            {
                double fPrev = f;
                int m = 2 * k + 1;
                double df = DawsonRybickiCoefficients[k] * (b / (n0 + m) + 1d / b / (n0 - m));
                f += df;
                if (Math.Abs(f - fPrev) < double.Epsilon)
                    return Math.Exp(-y * y) / Constants.SqrtPi * f;
                b *= bb;
            }

            return double.NaN; // throw new NonConvergenceException(IterationLimit);
        }
    }
}
