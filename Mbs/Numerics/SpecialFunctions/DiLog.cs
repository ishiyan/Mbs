using System;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Mbs.Numerics
{
    /// <summary>
    /// Contains assorted special functions.
    /// </summary>
    public static partial class SpecialFunctions
    {
        /// <summary>
        /// Bernoulli numbers; these are coefficients in the log expansion for the di-log log series.
        /// </summary>
        private static readonly double[] Dc =
        {
            1.0,
            1.0 / 6.0,
            -1.0 / 30.0,
            1.0 / 42.0,
            -1.0 / 30.0,
            5.0 / 66.0,
            -691.0 / 2730.0,
            7.0 / 6.0,
            -3617.0 / 510.0,
            43867.0 / 798.0,
            -174611.0 / 330.0,
            854513.0 / 138.0,
            -236364091.0 / 2730.0,
            -236364091.0 / 2730.0,
            8553103.0 / 6.0,
            -23749461029.0 / 870.0,
            8615841276005.0 / 14322.0,
        };

        /// <summary>
        /// The di-logarithm function, Li₂(x), also called Spence's function.
        /// The function gets is name from the similarity of this series to the expansion of ln(1-x), the
        /// difference being that the integer in the denominator is raised to the second power.
        /// <para />
        /// Li₂(x) is real for -∞; &lt; x ≤ 1; for values outside this range, use the complex version.
        /// </summary>
        /// <param name="x">The argument, which must be less than or equal to unity.</param>
        /// <returns>The value Li₂(x).</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Dilogarithm" />
        public static double DiLog(double x)
        {
            if (x > 1d)
            {
                return double.NaN;
            }

            if (x > 0.7)
            {
                // Use series near 1.
                return DiLogSeries1(1d - x);
            }

            if (x > -0.7)
            {
                // Series near 0 (defining power series).
                return DiLogSeries0(x);
            }

            if (x >= -1d)
            {
                // Use Li(-x) = 1/2 Li(x²) - Li(-x) to map to [0,1].
                return DiLog(x * x) / 2d - DiLog(-x);
            }

            // Use formula for Li(1/x) to map to [-1,0].
            double ln = Math.Log(-x);
            return -Constants.Pi * Constants.Pi / 6d - ln * ln / 2d - DiLog(1d / x);
        }

        /// <summary>
        /// Computes the complex di-logarithm function, also called Spence's function.
        /// </summary>
        /// <param name="z">The complex argument.</param>
        /// <returns>The value Li<sub>2</sub>(z).</returns>
        /// <seealso href="http://mathworld.wolfram.com/Dilogarithm.html" />
        public static Complex DiLog(Complex z)
        {
            Complex f;
            double a0 = Complex.Abs(z);
            if (a0 > 1d)
            {
                // Outside the unit disk, reflect into the unit disk.
                Complex ln = Complex.Log(-z);
                f = -Constants.Pi * Constants.Pi / 6d - ln * ln / 2d - DiLog(1d / z);
            }
            else
            {
                // Inside the unit disk.
                if (a0 < 0.75)
                {
                    // Close to 0, use the expansion about zero.
                    f = DiLogSeries0(z);
                }
                else
                {
                    // We are in the annulus near the edge of the unit disk.
                    if (z.Real < 0d)
                    {
                        // Reflect negative into positive half-disk.
                        // This avoids problems with the log expansion near -1.
                        f = DiLog(z * z) / 2d - DiLog(-z);
                    }
                    else
                    {
                        // Figure out whether we are close to 1.
                        Complex e = 1d - z;
                        if (Complex.Abs(e) < 0.5)
                        {
                            // Close to 1, use the expansion about 1.
                            f = DiLogSeries1(e);
                        }
                        else
                        {
                            // Otherwise, use the log expansion, which is good
                            // near the unit circle but not too close to 1 or -1.
                            f = DiLogLogSeries(z);
                        }
                    }
                }
            }

            if (z.Real > 1d && Math.Sign(f.Imag) != Math.Sign(z.Imag))
            {
                f = f.Conjugate;
            }

            return f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double DiLogSeries0(double x)
        {
            // Series definition of DiLog Li₂(x) = ∑˚˚ᵢ₌₁(xⁱ/i²).
            // This converges reliably to full accuracy within a few tens of iterations below x~1/2; by x~1 it no longer converges.
            double xx = x;
            double f = xx;
            for (int k = 2; k < IterationLimit; ++k)
            {
                double fOld = f;
                xx *= x;
                f += xx / (k * k);
                if (Math.Abs(f - fOld) < double.Epsilon)
                {
                    return f;
                }
            }

            return double.NaN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex DiLogSeries0(Complex z)
        {
            Complex zz = z;
            Complex f = zz;
            for (int k = 2; k < IterationLimit; ++k)
            {
                Complex fOld = f;
                zz *= z;
                f += zz / (k * k);
                if (f == fOld)
                {
                    return f;
                }
            }

            return Complex.NaN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double DiLogSeries1(double e)
        {
            double f = Constants.Pi * Constants.Pi / 6d;
            if (Math.Abs(e) < double.Epsilon)
            {
                return f;
            }

            double l = Math.Log(e);
            double ek = 1d;
            for (int k = 1; k < IterationLimit; ++k)
            {
                double fOld = f;
                ek *= e;
                double df = ek * (l - 1d / k) / k;
                f += df;
                if (Math.Abs(f - fOld) < double.Epsilon)
                {
                    return f;
                }
            }

            return double.NaN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex DiLogSeries1(Complex e)
        {
            Complex f = Constants.Pi * Constants.Pi / 6d;
            if (e == 0d)
            {
                return f;
            }

            Complex l = Complex.Log(e);
            Complex ek = 1d;
            for (int k = 1; k < IterationLimit; ++k)
            {
                Complex fOld = f;
                ek *= e;
                Complex df = ek * (l - 1d / k) / k;
                f += df;
                if (f == fOld)
                {
                    return f;
                }
            }

            return Complex.NaN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex DiLogLogSeries(Complex z)
        {
            Complex ln = Complex.Log(z);
            Complex ln2 = ln * ln;
            Complex f = Constants.Pi * Constants.Pi / 6d + ln * (1d - Complex.Log(-ln)) - ln2 / 4d;
            Complex p = ln;
            for (int k = 1; k < Dc.Length; ++k)
            {
                Complex fOld = f;
                p *= ln2 / (2 * k + 1) / (2 * k);
                f += (-Dc[k] / (2 * k)) * p;
                if (f == fOld)
                {
                    return f;
                }
            }

            return Complex.NaN;
        }
    }
}
