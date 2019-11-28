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
        /// The Gauss error function, erf(x) = 2/√̅π∙ₒ∫ˣexp(-t²)dt.
        /// </summary>
        /// <param name="x">An argument.</param>
        /// <returns>The value of erf.</returns>
        /// <remarks>
        /// <code>
        /// ====================================================
        /// Copyright (C) 1993 Sun Microsystems, Inc. All rights reserved.
        ///
        /// Developed at SunPro, a Sun Microsystems, Inc. business.
        /// Permission to use, copy, modify, and distribute this
        /// software is freely granted, provided that this notice
        /// is preserved.
        /// ====================================================
        ///
        /// double erf(double x)
        /// double erfc(double x)
        ///                           x
        ///                    2      |\
        ///     erf(x)  =  ---------  | exp(-t*t)dt
        ///                 sqrt(pi) \|
        ///                           0
        ///     erfc(x) =  1-erf(x)
        /// Recall that
        ///     erf(-x) = -erf(x)
        ///     erfc(-x) = 2 - erfc(x)
        ///
        /// Method:
        /// 1. For |x| in [0, 0.84375]
        ///    erf(x)  = x + x*R(x^2)
        ///    erfc(x) = 1 - erf(x)           if x in [-.84375,0.25]
        ///            = 0.5 + ((0.5-x)-x*R)  if x in [0.25,0.84375]
        ///    where R = P/Q where P is an odd poly of degree 8 and
        ///    Q is an odd poly of degree 10.
        ///
        ///          |R - (erf(x)-x)/x| ≤ 2^(-57.90)
        ///
        ///    Remark. The formula is derived by noting
        ///    erf(x) = (2/sqrt(pi))*(x - x^3/3 + x^5/10 - x^7/42 + ....)
        ///    and that 2/sqrt(pi) = 1.128379167095512573896158903121545171688
        ///    is close to one. The interval is chosen because the fix
        ///    point of erf(x) is near 0.6174 (i.e., erf(x)=x when x is
        ///    near 0.6174), and by some experiment, 0.84375 is chosen to
        ///    guarantee the error is less than one ulp for erf.
        ///
        /// 2. For |x| in [0.84375,1.25], let z = |x| - 1, and
        ///    c = 0.84506291151 rounded to single (24 bits)
        ///         erf(x)  = sign(x) * (c  + P1(z)/Q1(z))
        ///         erfc(x) = (1-c)  - P1(s)/Q1(z) if x > 0
        ///                   1+(c+P1(z)/Q1(z))    if x &lt; 0
        ///         |P1/Q1 - (erf(|x|)-c)| ≤ 2^(-59.06)
        ///    Remark: here we use the taylor series expansion at x=1.
        ///         erf(1+z) = erf(1) + s*Poly(z)
        ///                  = 0.845.. + P1(z)/Q1(z)
        ///    That is, we use rational approximation to approximate
        ///         erf(1+z) - (c = (single)0.84506291151)
        ///    Recall that |P1/Q1| &lt; 0.078 for x in [0.84375,1.25]
        ///    where
        ///         P1(z) = degree 6 poly in z
        ///         Q1(z) = degree 6 poly in z
        ///
        /// 3. For x in [1.25, 1/0.35(~2.857143)],
        ///         erfc(x) = (1/x)*exp(-x*x-0.5625+R1/S1)
        ///         erf(x)  = 1 - erfc(x)
        ///    where
        ///         R1(z) = degree 7 poly in z, (z=1/x^2)
        ///         S1(z) = degree 8 poly in z
        ///
        /// 4. For x in [1/0.35,28]
        ///         erfc(x) = (1/x)*exp(-x*x-0.5625+R2/S2) if x > 0
        ///                 = 2.0 - (1/x)*exp(-x*x-0.5625+R2/S2) if -6&lt;x&lt;0
        ///                 = 2.0 - tiny            (if x &lt;= -6)
        ///         erf(x)  = sign(x)*(1.0 - erfc(x)) if x &lt; 6, else
        ///         erf(x)  = sign(x)*(1.0 - tiny)
        ///    where
        ///         R2(z) = degree 6 poly in z, (z=1/x^2)
        ///         S2(z) = degree 7 poly in z
        ///    Note1:
        ///       To compute exp(-x*x-0.5625+R/S), let s be a single
        ///       precision number and z := x; then
        ///            -x*x = -z*z + (z-x)*(z+x)
        ///            exp(-x*x-0.5626+R/S) =
        ///                    exp(-z*z-0.5625)*exp((z-x)*(z+x)+R/S);
        ///    Note2:
        ///       Here 4 and 5 make use of the asymptotic series
        ///                      exp(-x*x)
        ///            erfc(x) ~ ---------- * ( 1 + Poly(1/x^2) )
        ///                      x*sqrt(pi)
        ///       We use rational approximation to approximate
        ///            g(z)=f(1/x^2) = log(erfc(x)*x) - x*x + 0.5625
        ///       Here is the error bound for R1/S1 and R2/S2
        ///            |R1/S1 - f(x)|  &lt; 2^(-62.57)
        ///            |R2/S2 - f(x)|  &lt; 2^(-61.52)
        ///
        /// 5. For inf > x >= 28
        ///       erf(x)  = sign(x) *(1 - tiny)  (raise inexact)
        ///       erfc(x) = tiny*tiny (raise underflow) if x > 0
        ///               = 2 - tiny if x&lt;0
        ///
        /// 7. Special case:
        ///       erf(0)  = 0, erf(inf)  = 1, erf(-inf) = -1,
        ///       erfc(0) = 1, erfc(inf) = 0, erfc(-inf) = 2,
        ///       erfc/erf(NaN) is NaN
        /// </code>
        /// </remarks>
        public static double Erf(double x)
        {
            double ax = Math.Abs(x);
            if (ax < 0.84375)
            {
                // Rational approximation for |x| < 0.84375.
                if (ax < 3.7252902984e-9)
                {
                    const double efx = 1.28379167095512586316e-01;
                    const double efx8 = 1.02703333676410069053e+00;
                    if (ax < double.Epsilon * 16) // tiny
                        return 0.125 * (8.0 * x + efx8 * x); // Avoid underflow.
                    return x + efx * x;
                }

                const double p0 = 1.28379167095512558561e-01;
                const double p1 = -3.25042107247001499370e-01;
                const double p2 = -2.84817495755985104766e-02;
                const double p3 = -5.77027029648944159157e-03;
                const double p4 = -2.37630166566501626084e-05;
                const double q1 = 3.97917223959155352819e-01;
                const double q2 = 6.50222499887672944485e-02;
                const double q3 = 5.08130628187576562776e-03;
                const double q4 = 1.32494738004321644526e-04;
                const double q5 = -3.96022827877536812320e-06;
                double z = x * x;
                double r = p0 + z * (p1 + z * (p2 + z * (p3 + z * p4)));
                double s = 1d + z * (q1 + z * (q2 + z * (q3 + z * (q4 + z * q5))));
                return x + x * (r / s);
            }

            if (ax < 1.25)
            {
                // Rational approximation for 0.84375 ≤ |x| < 1.25.
                const double p0 = -2.36211856075265944077e-03;
                const double p1 = 4.14856118683748331666e-01;
                const double p2 = -3.72207876035701323847e-01;
                const double p3 = 3.18346619901161753674e-01;
                const double p4 = -1.10894694282396677476e-01;
                const double p5 = 3.54783043256182359371e-02;
                const double p6 = -2.16637559486879084300e-03;
                const double q1 = 1.06420880400844228286e-01;
                const double q2 = 5.40397917702171048937e-01;
                const double q3 = 7.18286544141962662868e-02;
                const double q4 = 1.26171219808761642112e-01;
                const double q5 = 1.36370839120290507362e-02;
                const double q6 = 1.19844998467991074170e-02;
                double z = ax - 1d;
                double p = p0 + z * (p1 + z * (p2 + z * (p3 + z * (p4 + z * (p5 + z * p6)))));
                double q = 1d + z * (q1 + z * (q2 + z * (q3 + z * (q4 + z * (q5 + z * q6)))));
                const double erx = 8.45062911510467529297e-01;
                return x >= 0 ? erx + p / q : -erx - p / q;
            }

            if (ax >= 6d)
            {
                // Approximation for 6.0 ≤ |x|.
                const double tiny = 1e-300;
                return x >= 0 ? 1d - tiny : tiny - 1d;
            }

            {
                // Rational approximation for 1.25 ≤ |x| < 6.0.
                double r, s, z = 1d / (ax * ax);
                if (ax < 1d / 0.35)
                {
                    // Rational approximation for 1.25 ≤ |x| < (1/0.35).
                    const double r0 = -9.86494403484714822705e-03;
                    const double r1 = -6.93858572707181764372e-01;
                    const double r2 = -1.05586262253232909814e+01;
                    const double r3 = -6.23753324503260060396e+01;
                    const double r4 = -1.62396669462573470355e+02;
                    const double r5 = -1.84605092906711035994e+02;
                    const double r6 = -8.12874355063065934246e+01;
                    const double r7 = -9.81432934416914548592e+00;
                    const double s1 = 1.96512716674392571292e+01;
                    const double s2 = 1.37657754143519042600e+02;
                    const double s3 = 4.34565877475229228821e+02;
                    const double s4 = 6.45387271733267880336e+02;
                    const double s5 = 4.29008140027567833386e+02;
                    const double s6 = 1.08635005541779435134e+02;
                    const double s7 = 6.57024977031928170135e+00;
                    const double s8 = -6.04244152148580987438e-02;
                    r = r0 + z * (r1 + z * (r2 + z * (r3 + z * (r4 + z * (r5 + z * (r6 + z * r7))))));
                    s = 1d + z * (s1 + z * (s2 + z * (s3 + z * (s4 + z * (s5 + z * (s6 + z * (s7 + z * s8)))))));
                }
                else
                {
                    // Rational approximation for (1/0.35) ≤ |x| < 6.0.
                    const double r0 = -9.86494292470009928597e-03;
                    const double r1 = -7.99283237680523006574e-01;
                    const double r2 = -1.77579549177547519889e+01;
                    const double r3 = -1.60636384855821916062e+02;
                    const double r4 = -6.37566443368389627722e+02;
                    const double r5 = -1.02509513161107724954e+03;
                    const double r6 = -4.83519191608651397019e+02;
                    const double s1 = 3.03380607434824582924e+01;
                    const double s2 = 3.25792512996573918826e+02;
                    const double s3 = 1.53672958608443695994e+03;
                    const double s4 = 3.19985821950859553908e+03;
                    const double s5 = 2.55305040643316442583e+03;
                    const double s6 = 4.74528541206955367215e+02;
                    const double s7 = -2.24409524465858183362e+01;
                    r = r0 + z * (r1 + z * (r2 + z * (r3 + z * (r4 + z * (r5 + z * r6)))));
                    s = 1d + z * (s1 + z * (s2 + z * (s3 + z * (s4 + z * (s5 + z * (s6 + z * s7))))));
                }

                var f = (float)ax;
                z = f;
                z = Math.Exp(-z * z - 0.5625) * Math.Exp((z - ax) * (z + ax) + r / s) / ax;
                return x >= 0 ? 1d - z : z - 1d;
            }
        }

        /// <summary>
        /// The complex error function.
        /// <para />
        /// This function is the analytic continuation of the error function to the complex plane.
        /// <para />
        /// The complex error function is entire: it has no poles, cuts, or discontinuities anywhere in the complex plane.
        /// <para />
        /// For pure imaginary arguments, erf(z) reduces to the Dawson integral (<see cref="Dawson"/>).
        /// <para />
        /// Away from the origin near the real axis, the real part of erf(z) quickly approaches ±1.
        /// To accurately determine the small difference erf(z) - 1 in this region, use the <see cref="Faddeeva"/> function.
        /// Away from the origin near the imaginary axis, the magnitude of erf(z) increases very quickly.
        /// Although erf(z) may overflow in this region, you can still accurately determine the value of the product erf(z) exp(z²) using the <see cref="Faddeeva"/> function.
        /// </summary>
        /// <param name="z">The complex argument.</param>
        /// <returns>The value of erf(z).</returns>
        public static Complex Erf(Complex z)
        {
            double r = Complex.Abs(z);
            if (r < 4d)
            {
                // Near the origin, use the series.
                return ErfSeries(z);
            }

            // Otherwise, just compute from Faddeeva.
            if (z.Real < 0d)
            {
                // Since Fadddeeva blows up for negative z.Real, use erf(z) = -erf(-z).
                return Complex.Exp(-z * z) * Faddeeva(-Complex.ImaginaryOne * z) - 1d;
            }

            return 1d - Complex.Exp(-z * z) * Faddeeva(Complex.ImaginaryOne * z);
        }

        /// <summary>
        /// The complementary Gauss error function, erfc(x) = 1 - erf(x).
        /// </summary>
        /// <param name="x">An argument.</param>
        /// <returns>The value of erfc.</returns>
        public static double Erfc(double x)
        {
            const double tiny = 1e-300;
            double ax = Math.Abs(x);
            if (ax < 0.84375)
            {
                // Rational approximation for |x| < 0.84375.
                if (ax < 13.8777878e-18)
                    return 1d - x;
                const double p0 = 1.28379167095512558561e-01;
                const double p1 = -3.25042107247001499370e-01;
                const double p2 = -2.84817495755985104766e-02;
                const double p3 = -5.77027029648944159157e-03;
                const double p4 = -2.37630166566501626084e-05;
                const double q1 = 3.97917223959155352819e-01;
                const double q2 = 6.50222499887672944485e-02;
                const double q3 = 5.08130628187576562776e-03;
                const double q4 = 1.32494738004321644526e-04;
                const double q5 = -3.96022827877536812320e-06;
                double z = x * x;
                double r = p0 + z * (p1 + z * (p2 + z * (p3 + z * p4)));
                double s = 1d + z * (q1 + z * (q2 + z * (q3 + z * (q4 + z * q5))));
                if (ax < 0.25)
                {
                    // |x| < 1/4.
                    return 1d - (x + x * r / s);
                }

                r = x * r / s;
                r += x - 0.5;
                return 0.5 - r;
            }

            if (ax < 1.25)
            {
                // 0.84375 ≤ |x| < 1.25
                const double p0 = -2.36211856075265944077e-03;
                const double p1 = 4.14856118683748331666e-01;
                const double p2 = -3.72207876035701323847e-01;
                const double p3 = 3.18346619901161753674e-01;
                const double p4 = -1.10894694282396677476e-01;
                const double p5 = 3.54783043256182359371e-02;
                const double p6 = -2.16637559486879084300e-03;
                const double q1 = 1.06420880400844228286e-01;
                const double q2 = 5.40397917702171048937e-01;
                const double q3 = 7.18286544141962662868e-02;
                const double q4 = 1.26171219808761642112e-01;
                const double q5 = 1.36370839120290507362e-02;
                const double q6 = 1.19844998467991074170e-02;
                double z = ax - 1d;
                double p = p0 + z * (p1 + z * (p2 + z * (p3 + z * (p4 + z * (p5 + z * p6)))));
                double q = 1d + z * (q1 + z * (q2 + z * (q3 + z * (q4 + z * (q5 + z * q6)))));
                const double erx = 8.45062911510467529297e-01;
                if (x >= 0d)
                {
                    z = 1d - erx;
                    return z - p / q;
                }

                z = erx + p / q;
                return 1d + z;
            }

            if (ax < 28d)
            {
                // |x|<28
                double r, s, z = 1d / (x * x);
                if (ax < 2.857142857)
                {
                    // |x| < 1/.35 ~ 2.857143
                    const double r0 = -9.86494403484714822705e-03;
                    const double r1 = -6.93858572707181764372e-01;
                    const double r2 = -1.05586262253232909814e+01;
                    const double r3 = -6.23753324503260060396e+01;
                    const double r4 = -1.62396669462573470355e+02;
                    const double r5 = -1.84605092906711035994e+02;
                    const double r6 = -8.12874355063065934246e+01;
                    const double r7 = -9.81432934416914548592e+00;
                    const double s1 = 1.96512716674392571292e+01;
                    const double s2 = 1.37657754143519042600e+02;
                    const double s3 = 4.34565877475229228821e+02;
                    const double s4 = 6.45387271733267880336e+02;
                    const double s5 = 4.29008140027567833386e+02;
                    const double s6 = 1.08635005541779435134e+02;
                    const double s7 = 6.57024977031928170135e+00;
                    const double s8 = -6.04244152148580987438e-02;
                    r = r0 + z * (r1 + z * (r2 + z * (r3 + z * (r4 + z * (r5 + z * (r6 + z * r7))))));
                    s = 1d + z * (s1 + z * (s2 + z * (s3 + z * (s4 + z * (s5 + z * (s6 + z * (s7 + z * s8)))))));
                }
                else
                {
                    // |x| >= 1/.35 ~ 2.857143
                    if (x < -6.0)
                        return 2d - tiny; /* x < -6 */
                    const double r0 = -9.86494292470009928597e-03;
                    const double r1 = -7.99283237680523006574e-01;
                    const double r2 = -1.77579549177547519889e+01;
                    const double r3 = -1.60636384855821916062e+02;
                    const double r4 = -6.37566443368389627722e+02;
                    const double r5 = -1.02509513161107724954e+03;
                    const double r6 = -4.83519191608651397019e+02;
                    const double s1 = 3.03380607434824582924e+01;
                    const double s2 = 3.25792512996573918826e+02;
                    const double s3 = 1.53672958608443695994e+03;
                    const double s4 = 3.19985821950859553908e+03;
                    const double s5 = 2.55305040643316442583e+03;
                    const double s6 = 4.74528541206955367215e+02;
                    const double s7 = -2.24409524465858183362e+01;
                    r = r0 + z * (r1 + z * (r2 + z * (r3 + z * (r4 + z * (r5 + z * r6)))));
                    s = 1d + z * (s1 + z * (s2 + z * (s3 + z * (s4 + z * (s5 + z * (s6 + z * s7))))));
                }

                var f = (float)ax;
                z = f;
                z = Math.Exp(-z * z - 0.5625) * Math.Exp((z - ax) * (z + ax) + r / s) / ax;
                return x > 0d ? z : 2d - z;
            }

            return x > 0d ? tiny * tiny : 2d - tiny;
        }

        /// <summary>
        /// The error function (also called the Gauss error function or probability integral):
        /// <para><c>erf(x) = 2∕√̅π ∫̽˳ e⁻ᵗˆ²dt</c>.</para>
        /// <para>The area under a bell curve within <c>>±z</c> standard deviations of the mean is given by <c>erf(z/√̅2)</c>.</para>
        /// <para>For large values of <c>x</c>, <c>erf(x)≈1</c> to within floating-point accuracy. To obtain accurate values of
        /// <c>erf(x)=1-erfc(x)</c> in this range, use the <see cref="Erfc"/> function.</para>
        /// </summary>
        /// <param name="x">The argument.</param>
        /// <returns>The value of <c>erf(x)</c>.</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Error_function" />
        /// <seealso href="http://mathworld.wolfram.com/Erf.html" />
        public static double Erf2(double x)
        {
            return x < 0 ? -RegularizedGammaP(0.5, x * x) : RegularizedGammaP(0.5, x * x);
        }

        /// <summary>
        /// Complementary error function:
        /// <para><c>erfc(x) = 1-erf(x) = 1 - 2∕√̅π ∫̽˳ e⁻ᵗˆ²dt = 2∕√̅π ∫̥˚˚ e⁻ᵗˆ²dt</c>.</para>
        /// <para>The complementary error function can be used to express the area in the tails of a Bell curve beyond a given distance from its center.</para>
        /// <para>For small values of <c>x</c>, <c>erfc(x)≈1</c> to within floating-point accuracy. To obtain accurate values of
        /// <c>erfc(x)=1-erf(x)</c> in this range, use the <see cref="Erf2"/> function.</para>
        /// </summary>
        /// <param name="x">The argument.</param>
        /// <returns>The value of <c>erfc(x)</c>.</returns>
        public static double Erfc2(double x)
        {
            return x < 0 ? 1d + RegularizedGammaP(0.5, x * x) : RegularizedGammaQ(0.5, x * x);
        }

        /// <summary>
        /// The inverse error function.
        /// </summary>
        /// <param name="y">The error function value <c>erf(x)</c>, which must lie inside [-1,1].</param>
        /// <returns>The corresponding argument x.</returns>
        public static double InverseErf(double y)
        {
            if (y > 1d || y < -1d)
                return double.NaN;

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (y == 1d)
                return double.PositiveInfinity;

            // if (y < 0d)
            //     return -InverseErf(-y);
            return ErfOrErfc(1d - y, y);
        }

        /// <summary>
        /// The inverse complementary error function.
        /// </summary>
        /// <param name="y">The value of <c>erfc(x)</c>, which must lie between 0 and 1.</param>
        /// <returns>The corresponding argument x.</returns>
        public static double InverseErfc(double y)
        {
            if (y < 0 || y > 2d)
                return double.NaN;
            return ErfOrErfc(y, 1d - y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double ApproximateInverseErf(double y)
        {
            double x = Constants.SqrtPi * y / 2d;
            double xx = x * x;
            // ReSharper disable once IdentifierTypo
            double xxxx = xx * xx;
            double s = 1d + xx / 3d + 7d / 30d * xxxx + 127d / 630d * xxxx * xx;
            return x * s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double ApproximateInverseErfc(double y)
        {
            double yy = y * y;
            double log = Math.Log(Constants.TwoOverPi / yy);
            double s = log - Math.Log(log);
            return Math.Sqrt(s / 2d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double ErfOrErfc(double x, double q)
        {
            q = 0.5 * q;
            double d;
            if (Math.Abs(q) <= 0.425)
            {
                d = 0.180625 - (q * q);
                return 0.70710678118654757 * (q * (((((((2509.0809287301227 * d + 33430.575583588128) * d + 67265.7709270087) * d + 45921.95393154987) * d + 13731.693765509461) * d + 1971.5909503065514) * d + 133.14166789178438) * d + 3.3871328727963665) / (((((((5226.4952788528544 * d + 28729.085735721943) * d + 39307.895800092709) * d + 21213.794301586597) * d + 5394.1960214247511) * d + 687.18700749205789) * d + 42.313330701600911) * d + 1d));
            }

            d = 0.5 * x;
            if (x > 1d)
                d = 1d - d;
            d = Math.Sqrt(-Math.Log(d));
            double num;
            if (d <= 5d)
            {
                d -= 1.6;
                num = (((((((0.00077454501427834139 * d + 0.022723844989269184) * d + 0.24178072517745061) * d + 1.2704582524523684) * d + 3.6478483247632045) * d + 5.769497221460691) * d + 4.6303378461565456) * d + 1.4234371107496835) / (((((((1.0507500716444169E-09 * d + 0.00054759380849953455) * d + 0.015198666563616457) * d + 0.14810397642748008) * d + 0.6897673349851) * d + 1.6763848301838038) * d + 2.053191626637759) * d + 1.0);
            }
            else
            {
                d -= 5d;
                num = (((((((2.0103343992922882E-07 * d + 2.7115555687434876E-05) * d + 0.0012426609473880784) * d + 0.026532189526576124) * d + 0.29656057182850487) * d + 1.7848265399172913) * d + 5.4637849111641144) * d + 6.6579046435011033) / (((((((2.0442631033899397E-15 * d + 1.421511758316446E-07) * d + 1.8463183175100548E-05) * d + 0.00078686913114561329) * d + 0.014875361290850615) * d + 0.13692988092273581) * d + 0.599832206555888) * d + 1.0);
            }

            return Constants.HalfSqrt2 * (q > 0d ? num : -num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex ErfSeries(Complex z)
        {
            // The power series for the error function
            // erf(z) = 2/√̅π ∑˚˚ᵢ₌₀(-1)ⁱz²ⁱ⁺¹/(2i+1)i!
            // requires about 15 terms at |z| ~ 1, 30 terms at |z| ~ 2, 45 terms at |z| ~ 3.
            Complex zp = Constants.TwoOverSqrtPi * z;
            Complex zz = -z * z;
            Complex f = zp;
            for (int k = 1; k < IterationLimit; ++k)
            {
                Complex fOld = f;
                zp *= zz / k;
                f += zp / (2 * k + 1);
                if (f == fOld)
                    return f;
            }

            return Complex.NaN;
        }
    }
}
