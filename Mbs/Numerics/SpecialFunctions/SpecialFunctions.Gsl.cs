using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics
{
    // zReSharper disable MemberHidesStaticFromOuterClass
    // zReSharper disable CompareOfFloatsByEqualityOperator

    /// <summary>
    /// Contains assorted special functions.
    /// </summary>
    public static partial class SpecialFunctions
    {
        /// <summary>
        /// Ported from the Gnu Scientific Library.
        /// </summary>
        public static partial class Gsl
        {
            /// <summary>
            /// The Taylor coefficient of the specified degree for a given argument.
            /// </summary>
            /// <param name="degree">The degree of the coefficient.</param>
            /// <param name="x">A real number.</param>
            /// <returns>The Taylor coefficient of the specified degree.</returns>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double TaylorCoefficient(int degree, double x)
            {
                if (x < 0d || degree < 0)
                    return double.NaN;
                if (degree == 0)
                    return 1d;
                if (degree == 1)
                    return x;
                if (Math.Abs(x) < double.Epsilon)
                    return 0d;
                const double log2Pi = Constants.LnPi + Constants.Ln2;
                double lnTest = degree * (Math.Log(x) + 1d) + 1d - (degree + 0.5) * Math.Log(degree + 1d) + 0.5 * log2Pi;
                if (lnTest < Constants.LogDoubleMin + 1d)
                    return 0d;
                if (lnTest > Constants.LogDoubleMax - 1d)
                    return double.PositiveInfinity;
                double product = 1d;
                for (int i = 1; i <= degree; i++)
                    product *= x / i;
                return product;
            }

            /// <summary>
            /// Force the angle to lie in the range (-π, π].
            /// </summary>
            /// <param name="theta">The angle.</param>
            /// <returns>The restricted angle.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double RestrictAngle(double theta)
            {
                double atheta = Math.Abs(theta);
                if (atheta > 0.0625 / Constants.DoubleEpsilon)
                    return double.NaN;

                // Synthetic extended precision constants.
                const double p1 = 4 * 7.8539812564849853515625e-01;
                const double p2 = 4 * 3.7748947079307981766760e-08;
                const double p3 = 4 * 2.6951514290790594840552e-15;
                const double twoPi = 2 * (p1 + p2 + p3);

                double y = (theta >= 0d ? 1 : -1) * 2 * Math.Floor(atheta / twoPi);
                double r = theta - y * p1 - y * p2 - y * p3;
                if (r > Constants.Pi)
                    r = r - 2 * p1 - 2 * p2 - 2 * p3; // r - twoPi
                else if (r < -Constants.Pi)
                    r = r + 2 * p1 + 2 * p2 + 2 * p3; // r + twoPi
                return r;
            }

            /// <summary>
            /// Calculates tan(z) of a complex number.
            /// </summary>
            /// <param name="zr">A real part of the complex number.</param>
            /// <param name="zi">An imaginary part of the complex number.</param>
            /// <param name="imag">The imaginary part of the result.</param>
            /// <returns>The real part of the result.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double ComplexTan(double zr, double zi, out double imag)
            {
                if (Math.Abs(zi) < 1d)
                {
                    double sinzr = Math.Sin(zr);
                    double sinhzi = Math.Sinh(zi);
                    double denominator = sinzr * sinzr + sinhzi * sinhzi;
                    imag = 0.5 * Math.Sinh(2 * zi) / denominator;
                    return 0.5 * Math.Sin(2 * zr) / denominator;
                }
                else
                {
                    double u = Math.Exp(-zi);
                    double s = 2 * u / (1 - u * u);
                    s *= s;
                    double coszr = Math.Cos(zr);
                    double denominator = 1 + coszr * coszr * s;
                    imag = 1 / Math.Tanh(zi) / denominator;
                    return 0.5 * Math.Sin(2 * zr) * s / denominator;
                }
            }

            /// <summary>
            /// Calculates cot(z) of a complex number.
            /// </summary>
            /// <param name="zr">A real part of the complex number.</param>
            /// <param name="zi">An imaginary part of the complex number.</param>
            /// <param name="imag">The imaginary part of the result.</param>
            /// <returns>The real part of the result.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double ComplexCot(double zr, double zi, out double imag)
            {
                double tanr = ComplexTan(zr, zi, out var tani);

                // cot = inverse(tan)
                double ss = 1 / (tanr * tanr + tani * tani);
                imag = -zi * ss;
                return zr * ss;
            }

            /// <summary>
            /// Calculates a natural logarithm of a complex number.
            /// </summary>
            /// <param name="zr">A real part of the complex number.</param>
            /// <param name="zi">An imaginary part of the complex number.</param>
            /// <param name="imag">The imaginary part of the result.</param>
            /// <returns>The real part of the result.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double ComplexLog(double zr, double zi, out double imag)
            {
                if (Math.Abs(zr) > double.Epsilon || Math.Abs(zi) > double.Epsilon)
                {
                    imag = Math.Atan2(zi, zr);
                    double ax = Math.Abs(zr);
                    double ay = Math.Abs(zi);
                    double min = Math.Min(ax, ay);
                    double max = Math.Max(ax, ay);
                    double real = min / max;
                    return Math.Log(max) + 0.5 * Math.Log(1d + real * real);
                }

                imag = double.NaN;
                return double.NaN;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double SinhSeries(double x)
            {
                // Sinh(x) series. Double-precision for |x| < 1.
                const double c0 = 1d / 6;
                const double c1 = 1d / 120;
                const double c2 = 1d / 5040;
                const double c3 = 1d / 362880;
                const double c4 = 1d / 39916800;
                const double c5 = 1d / 6227020800;
                const double c6 = 1d / 1307674368000;
                const double c7 = 1d / 355687428096000;
                double y = x * x;
                return x * (1.0 + y * (c0 + y * (c1 + y * (c2 + y * (c3 + y * (c4 + y * (c5 + y * (c6 + y * c7))))))));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double CoshM1Series(double x)
            {
                // Cosh(x)-1 series. Double-precision for |x| < 1.
                const double c0 = 0.5;
                const double c1 = 1d / 24;
                const double c2 = 1d / 720;
                const double c3 = 1d / 40320;
                const double c4 = 1d / 3628800;
                const double c5 = 1d / 479001600;
                const double c6 = 1d / 87178291200;
                const double c7 = 1d / 20922789888000;
                const double c8 = 1d / 6402373705728000;
                double y = x * x;
                return y * (c0 + y * (c1 + y * (c2 + y * (c3 + y * (c4 + y * (c5 + y * (c6 + y * (c7 + y * c8))))))));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double ComplexSin(double zr, double zi, out double imag)
            {
                double real = Math.Abs(zi);
                if (real < 1d)
                {
                    double sh = SinhSeries(zi);
                    double chm1 = CoshM1Series(zi);
                    real = Math.Sin(zr) * (chm1 + 1d);
                    imag = Math.Cos(zr) * sh;
                }
                else if (real < Constants.LogDoubleMax)
                {
                    double ex = Math.Exp(zi);
                    real = 1d / ex;
                    double ch = 0.5 * (ex + real);
                    double sh = 0.5 * (ex - real);
                    real = Math.Sin(zr) * ch;
                    imag = Math.Cos(zr) * sh;
                }
                else
                {
                    real = double.PositiveInfinity;
                    imag = double.PositiveInfinity;
                }

                return real;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double ComplexLogSin(double zr, double zi, out double imag)
            {
                double real;
                if (zi > 60d)
                {
                    real = -Constants.Ln2 + zi;
                    imag = Constants.PiOver2 - zr;
                }
                else if (zi < -60d)
                {
                    real = -Constants.Ln2 - zi;
                    imag = -Constants.PiOver2 + zr;
                }
                else
                {
                    double sinReal = ComplexSin(zr, zi, out var sinImag);
                    real = ComplexLog(sinReal, sinImag, out imag);
                }

                imag = RestrictAngle(imag);
                return real;
            }
        }
    }
}
