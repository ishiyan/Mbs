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
        /// The complex error function, also known as the Faddeeva function, is defined as:
        /// <para><c>w(x) = e⁻ˣˆ²erfc(-ix) = 2e⁻ˣˆ²∕√π ∫̥˚˚ e⁻ᵗˆ²dt</c>.</para>
        /// <para>For purely imaginary values, it can be reduced to the error function. For purely real values, it can be reduced to Dawson's integral.</para>
        /// </summary>
        /// <param name="z">The complex argument.</param>
        /// <returns>The complex value of <c>w(z)</c>.</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Faddeeva" />
        public static Complex Faddeeva(Complex z)
        {
            // Use reflection formulae to ensure that we are in the first quadrant.
            if (z.Imag < 0d)
                return 2d * Complex.Exp(-z * z) - Faddeeva(-z);
            if (z.Real < 0d)
                return Faddeeva(-z.Conjugate).Conjugate;
            double r = Complex.Abs(z);
            if (r < 2d)
                return Complex.Exp(-z * z) * (1d - ErfSeries(-Complex.ImaginaryOne * z));
            if (z.Imag < 0.1 && z.Real < 30d)
            {
                // This is a special, awkward region along the real axis, Re w(x) ~ e⁻ˣˆ²; the Weideman
                // algorthm doesen't compute this small number well and the Laplace continued fraction
                // misses it entirely; therefore very close to the real axis we will use an analytic result
                // on the real axis and Taylor expand to where we need to go.
                // Unfortunately the Taylor expansion converges poorly for large x, so we drop this
                // work-arround near x~30, when this real part becomes too small to represent as a double anyway.
                double x = z.Real, y = z.Imag;
                return FaddeevaTaylor(new Complex(x, 0d), Math.Exp(-x * x) + 2d * Dawson(x) / Constants.SqrtPi * Complex.ImaginaryOne, new Complex(0d, y));
            }

            if (r > 7d)
            {
                // Use Laplace continued fraction for large z.
                return FaddeevaContinuedFraction(z);
            }

            // Use Weideman algorithm for intermediate region.
            return FaddeevaWeideman(z);
        }

        private static readonly double FaddeevaWeidemanL = Math.Sqrt(40d / Constants.Sqrt2);

        private static readonly double[] FaddeevaWeidemanCoefficients =
        {
            3.0005271472811341147438, // 0
            2.899624509389705247492,
            2.616054152761860368947,
            2.20151379487831192991,
            1.725383084817977807050,
            1.256381567576513235243, // 5
            0.847217457659381821530,
            0.52665289882770863869581,
            0.2998943799615006297951,
            0.1550426380247949427170,
            0.0718236177907433682806, // 10
            0.0292029164712418670902,
            0.01004818624278342412539,
            0.002705405633073791311865,
            0.000439807015986966782752,
            -0.0000393936314548956872961, // 15
            -0.0000559130926424831822323,
            -0.00001800744714475095715480,
            -1.066013898494714388844e-6,
            1.483566113220077986810e-6,
            5.91213695189949384568e-7, // 20
            1.419864239993567456648e-8,
            -6.35177348504429108355e-8,
            -1.83156167830404631847e-8,
            3.24974651804369739084e-9,
            3.01778054000907084962e-9, // 25
            2.10860063470665179035e-10,
            -3.56323398659765326830e-10,
            -9.05512445092829268740e-11,
            3.47272670930455000726e-11,
            1.771449521401119186147e-11, // 30
            -2.72760231582004518397e-12,
            -2.90768834218286692054e-12,
            1.203145821938798755342e-13,
            4.53296667826067277389e-13,
            1.372562058671550042872e-14, // 35
            -7.07408626028685552231e-14,
            -5.40931028288214223366e-15,
            1.135768719899924165040e-14,
            1.128073562364402060469e-15,
            -1.89969494739492699566e-15 // 40
        };

#pragma warning disable SA1203 // Constants must appear before fields
        private const int FaddeevaWeidemanCoefficientsLen = 41;
#pragma warning restore SA1203

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex FaddeevaWeideman(Complex z)
        {
            // There is a zero near (1.9915,-1.3548) -- check it out.
            Complex zq = Complex.ImaginaryOne * z;
            Complex zn = FaddeevaWeidemanL + zq;
            Complex zd = FaddeevaWeidemanL - zq;
            zq = zn / zd;
            Complex f = FaddeevaWeidemanCoefficients[FaddeevaWeidemanCoefficientsLen - 1];
            for (int k = FaddeevaWeidemanCoefficientsLen - 2; k > 0; --k)
                f = f * zq + FaddeevaWeidemanCoefficients[k];
            Complex zp = zn * zd;
            return 2d / zp * f * zq + Constants.OneOverSqrtPi / zd;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex FaddeevaContinuedFraction(Complex z)
        {
            // Requires about 10 terms at |z|~10, 15 terms at |z|~7; for smaller z, still
            // converges off the real axis, but fails to converge on the real axis for z~6 and below.
            Complex a = 1d;      // a₁
            Complex b = z;       // b₁
            Complex d = 1d / b;  // D₁ = b₀/b₁
            Complex df = a / b;  // Df₁ = f₁ - f₀
            Complex f = 0d + df; // f₁ = f₀ + Df₁ = b₀ + Df₁
            for (int k = 1; k < IterationLimit; ++k)
            {
                Complex fOld = f;
                a = -k / 2d;
                d = 1d / (b + a * d);
                df = (b * d - 1d) * df;
                f += df;
                if (f == fOld)
                    return Complex.ImaginaryOne / Constants.SqrtPi * f;
            }

            return Complex.NaN; // throw new NonConvergenceException(IterationLimit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex FaddeevaTaylor(Complex z0, Complex w0, Complex dz)
        {
            // Given a Faddeeva value w0 at a point z0, this routine computes the Fadeeva value a distance dz away
            // using a Taylor expansion. we use this near the real axis, where the Weideman algorithm doesn't give
            // the e⁻ˣˆ² real part of w(x) very accurately, and the Laplace continued fraction misses it entirely.

            // First order Taylor expansion.
            Complex wpPrev = w0;
            Complex wp = 2d * (Complex.ImaginaryOne / Constants.SqrtPi - z0 * w0);
            Complex zz = dz;
            Complex w = w0 + wp * dz;

            // Higher orders.
            for (int k = 2; k < IterationLimit; ++k)
            {
                // Remmeber the current value.
                Complex wPrev = w;

                // Compute the next derivative.
                Complex wpNext = -2d * (z0 * wp + (k - 1) * wpPrev);
                wpPrev = wp;
                wp = wpNext;

                // Use it to generate the next term in the Taylor expansion.
                zz = zz * dz / k;
                w = wPrev + wp * zz;

                // Test whether we have converged.
                if (w == wPrev)
                    return w;
            }

            return Complex.NaN; // throw new NonConvergenceException(IterationLimit);
        }
    }
}
