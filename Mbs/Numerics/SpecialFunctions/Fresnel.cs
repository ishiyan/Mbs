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
        /// The Fresnel cosine integral <c>C(x)</c> is a transcendental function named after Augustin-Jean Fresnel that is used in optics.
        /// <para>It arises in the description of near field Fresnel diffraction phenomena, and is defined through the following integral representation:</para>
        /// <para><c>C(x)=∫ͯ˳cos(t²)dt</c>.</para>
        /// </summary>
        /// <param name="x">The argument.</param>
        /// <returns>The value of <c>C(x)</c>.</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Fresnel_integral"/>
        public static double FresnelC(double x)
        {
            if (x < 0d)
                return -FresnelC(-x);
            if (x < 2d)
                return FresnelSeriesC(x);
            if (x > 64d)
                return FresnelAsymptotic(x).Real;
            return FresnelContinuedFraction(x).Real;
        }

        /// <summary>
        /// The Fresnel sine integral <c>S(x)</c> is a transcendental function named after Augustin-Jean Fresnel that is used in optics.
        /// <para>It arises in the description of near field Fresnel diffraction phenomena, and is defined through the following integral representation:</para>
        /// <para><c>S(x)=∫ͯ˳sin(t²)dt</c>.</para>
        /// </summary>
        /// <param name="x">The argument.</param>
        /// <returns>The value of <c>S(x)</c>.</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Fresnel_integral"/>
        public static double FresnelS(double x)
        {
            if (x < 0d)
                return -FresnelS(-x);
            if (x < 2d)
                return FresnelSeriesS(x);
            if (x > 64d)
                return FresnelAsymptotic(x).Imag;
            return FresnelContinuedFraction(x).Imag;
        }

        /// <summary>
        /// The Fresnel integrals, <c>S(x)</c> and <c>C(x)</c>, are two transcendental functions named after Augustin-Jean Fresnel that are used in optics.
        /// <para>They arise in the description of near field Fresnel diffraction phenomena, and are defined through the following integral representations:</para>
        /// <para><c>S(x)=∫ͯ˳sin(t²)dt, C(x)=∫ͯ˳cos(t²)dt</c>.</para>
        /// <para>The simultaneous parametric plot of <c>S(x)</c> against <c>C(x)</c> in the complex plane as x ranges from -∞ to +∞ is the Euler spiral, also known as the Cornu spiral or clothoid.</para>
        /// </summary>
        /// <param name="x">The argument.</param>
        /// <returns>The complex value <c>C(x)+S(x)i</c>.</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Fresnel_integral"/>
        /// <seealso href="http://mathworld.wolfram.com/CornuSpiral.html"/>
        public static Complex Fresnel(double x)
        {
            if (x < 0d)
                return -Fresnel(-x);
            if (x < 2d)
                return new Complex(FresnelSeriesC(x), FresnelSeriesS(x));
            if (x > 64d)
                return FresnelAsymptotic(x);
            return FresnelContinuedFraction(x);
        }

        // The use of the asymptotic series doesn't appear to be strictly necessary; the
        // continued fraction converges quickly to the right result in just a few terms
        // for arbitrarily large x; aha! this is because the continued fraction is just the
        // asymptotic series in that limit, down to the evaluation of sin(pi x²/2).
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double FresnelSeriesC(double x)
        {
            // Series requires ~10 terms at x~1, ~20 terms at x~2.
            // Compute the zero-order value.
            double df = x;

            // Pre-compute the factor the series is in.
            double x4 = x * x * Constants.PiOver2;
            x4 *= x4;

            // Add corrections as needed.
            double f = df;
            for (int n = 1; n < IterationLimit; ++n)
            {
                double fPrev = f;
                df = -df * x4 / (2 * n) / (2 * n - 1);
                f += df / (4 * n + 1);
                if (Math.Abs(f - fPrev) < double.Epsilon)
                    return f;
            }

            throw new NonConvergenceException(IterationLimit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double FresnelSeriesS(double x)
        {
            // Compute the zero-order value.
            double df = x * x * x * Constants.PiOver2;

            // Pre-compute the factor the series is in.
            double x4 = x * x * Constants.PiOver2;
            x4 *= x4;

            // Add corrections as needed.
            double f = df / 3d;
            for (int n = 1; n < IterationLimit; ++n)
            {
                double fPrev = f;
                df = -df * x4 / (2 * n + 1) / (2 * n);
                f += df / (4 * n + 3);
                if (Math.Abs(f - fPrev) < double.Epsilon)
                    return f;
            }

            throw new NonConvergenceException(IterationLimit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex FresnelAsymptotic(double x)
        {
            // Series requires ~3 terms at x~50, ~6 terms at x~10, ~10 terms at x~7, fails to converge to double precision much below that.
            double x2 = x * x * Constants.Pi;
            double x4 = x2 * x2;
            double f = 1d;
            double g = 1d;
            double d = 1d;
            int n = 1;
            while (true)
            {
                double fPrev = f;
                d = -d * (4 * n - 1) / x4;
                f += d;
                double gPrev = g;
                d *= (4 * n + 1);
                g += d;
                if (Math.Abs(f - fPrev) < double.Epsilon && Math.Abs(g - gPrev) < double.Epsilon)
                    break;
                ++n;
                if (n > IterationLimit)
                    throw new NonConvergenceException(IterationLimit);
            }

            double px = Constants.Pi * x;
            f /= px;
            g = g / x2 / px;
            double xx = x * x / 4d;
            double sin = Sin(0d, xx);
            double cos = Cos(0d, xx);
            double c = 0.5 + f * sin - g * cos;
            double s = 0.5 - f * cos - g * sin;
            return new Complex(c, s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex FresnelContinuedFraction(double x)
        {
            // Series requires ~35 terms at x~2, ~12 terms at x~4, ~6 terms at x~8, ~3 terms for x~12 and above.
            double px2 = Constants.Pi * x * x;

            // Complex z = Constants.SqrtPi * x / 2d * (1d - Complex.ImaginaryOne);
            // Investigate this carefully: it appears that (1) imaginary part of f doesn't change and
            // (2) real part of f merely increases at constant rate until "right" number is reached.
            Complex a = 1d;                // a₁
            var b = new Complex(1d, -px2); // b₁
            Complex d = 1d / b;            // D₁ = b₀ / b₁
            Complex df = a / b;            // Df₁ = f₁ - f₀
            Complex f = 0d + df;           // f₁ = f₀ + Df₁ = b₀ + Df₁
            for (int k = 1; ; ++k)
            {
                Complex fPrev = f;
                a = -(2 * k - 1) * (2 * k);
                b += 4.0;
                d = 1d / (b + a * d);
                df = (b * d - 1d) * df;
                f += df;
                if (f == fPrev)
                    break;
                if (k > IterationLimit)
                    throw new NonConvergenceException(IterationLimit);
            }

            double xx = x * x / 4d;
            var j = new Complex(1d, -1d);
            var e = new Complex(Cos(0d, xx), Sin(0d, xx));
            Complex erfc = j * e * f * x;
            Complex erf = 1d - erfc;
            return erf * new Complex(1d, 1d) / 2;
        }
    }
}
