﻿using System;

// ReSharper disable once CheckNamespace
namespace Mbs.Numerics
{
    /// <summary>
    /// Contains assorted special functions.
    /// </summary>
    public static partial class SpecialFunctions
    {
        private static readonly int[] Primes =
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53,
            59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131,
            137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223,
        };

        /// <summary>
        /// Coefficients for Maclaurin summation in HurwitzZeta(), B₂ᵢ/(2i)!.
        /// </summary>
        private static readonly double[] HurwitzZetaCoefficients =
        {
            1.00000000000000000000000000000,     0.083333333333333333333333333333,    -0.00138888888888888888888888888889,
            0.000033068783068783068783068783069, -8.2671957671957671957671957672e-07, 2.0876756987868098979210090321e-08,
            -5.2841901386874931848476822022e-10, 1.3382536530684678832826980975e-11,  -3.3896802963225828668301953912e-13,
            8.5860620562778445641359054504e-15,  -2.1748686985580618730415164239e-16, 5.5090028283602295152026526089e-18,
            -1.3954464685812523340707686264e-19, 3.5347070396294674716932299778e-21,  -8.9535174270375468504026113181e-23,
        };

        /// <summary>
        /// An array of 2π¹⁰ⁿ.
        /// </summary>
        private static readonly double[] TwoPiPow =
        {
            1d,                        9.589560061550901348e+007, 9.195966217409212684e+015,
            8.818527036583869903e+023, 8.456579467173150313e+031, 8.109487671573504384e+039,
            7.776641909496069036e+047, 7.457457466828644277e+055, 7.151373628461452286e+063,
            6.857852693272229709e+071, 6.576379029540265771e+079, 6.306458169130020789e+087,
            6.047615938853066678e+095, 5.799397627482402614e+103, 5.561367186955830005e+111,
            5.333106466365131227e+119, 5.114214477385391780e+127, 4.904306689854036836e+135,
        };

        /// <summary>
        /// Chebyshev fit for (sₓ-1)ζ(sₓ), sₓ= (x+1)/2, x⋲[-1,1].
        /// </summary>
        private static readonly double[] ZetaXlt1 =
        {
            1.48018677156931561235192914649,     0.25012062539889426471999938167,     0.00991137502135360774243761467,
            -0.00012084759656676410329833091,    -4.7585866367662556504652535281e-06, 2.2229946694466391855561441361e-07,
            -2.2237496498030257121309056582e-09, -1.0173226513229028319420799028e-10, 4.3756643450424558284466248449e-12,
            -6.2229632593100551465504090814e-14, -6.6116201003272207115277520305e-16, 4.9477279533373912324518463830e-17,
            -1.0429819093456189719660003522e-18, 6.9925216166580021051464412040e-21,
        };

        /// <summary>
        /// Chebyshev fit for (sₓ-1)ζ(sₓ), sₓ= (19x+21)/2, x⋲[-1,1].
        /// </summary>
        private static readonly double[] ZetaXgt1 =
        {
            19.3918515726724119415911269006,    9.1525329692510756181581271500,      0.2427897658867379985365270155,
            -0.1339000688262027338316641329,    0.0577827064065028595578410202,      -0.0187625983754002298566409700,
            0.0039403014258320354840823803,     -0.0000581508273158127963598882,     -0.0003756148907214820704594549,
            0.0001892530548109214349092999,     -0.0000549032199695513496115090,     8.7086484008939038610413331863e-6,
            6.4609477924811889068410083425e-7,  -9.6749773915059089205835337136e-7,  3.6585400766767257736982342461e-7,
            -8.4592516427275164351876072573e-8, 9.9956786144497936572288988883e-9,   1.4260036420951118112457144842e-9,
            -1.1761968823382879195380320948e-9, 3.7114575899785204664648987295e-10,  -7.4756855194210961661210215325e-11,
            7.8536934209183700456512982968e-12, 9.9827182259685539619810406271e-13,  -7.5276687030192221587850302453e-13,
            2.1955026393964279988917878654e-13, -4.1934859852834647427576319246e-14, 4.6341149635933550715779074274e-15,
            2.3742488509048340106830309402e-16, -2.7276516388124786119323824391e-16, 7.8473570134636044722154797225e-17,
        };

        /// <summary>
        /// The Riemann zeta function, ζ(s), can be defined as the sum of the <c>s</c>-th inverse power of the natural numbers.
        /// </summary>
        /// <param name="s">The argument.</param>
        /// <returns>The value ζ(s).</returns>
        public static double RiemannZeta(double s)
        {
            if (Math.Abs(s - 1d) < double.Epsilon)
            {
                return double.NaN;
            }

            if (s >= 0d)
            {
                // Here s ≥ 0 and s≠1.
                if (s < 1d)
                {
                    return EvaluateChebyshevSeries(2d * s - 1d, ZetaXlt1, 13, -1d, 1d) / (s - 1d);
                }

                if (s <= 20d)
                {
                    return EvaluateChebyshevSeries((2d * s - 21d) / 19d, ZetaXgt1, 29, -1d, 1d) / (s - 1d);
                }

                return 1d / ((1d - Math.Pow(2d, -s)) * (1d - Math.Pow(3d, -s)) * (1d - Math.Pow(5d, -s)) * (1d - Math.Pow(7d, -s)));
            }

            // Reflection formula, [Abramowitz+Stegun, 23.2.5].
            double q = (s > -19d) ?
                EvaluateChebyshevSeries((-19d - 2d * s) / 19d, ZetaXgt1, 29, -1d, 1d) / -s :
                1d / ((1d - Math.Pow(2d, -(1d - s))) * (1d - Math.Pow(3d, -(1d - s))) * (1d - Math.Pow(5d, -(1d - s))) * (1d - Math.Pow(7d, -(1d - s))));
            double sinTerm = Math.Abs(Math.IEEERemainder(s, 2d)) < double.Epsilon ?
                0d :
                Math.Sin(0.5 * Constants.Pi * Math.IEEERemainder(s, 4d)) / Constants.Pi;
            if (Math.Abs(sinTerm) < double.Epsilon)
            {
                return 0d;
            }

            var n = (int)Math.Floor(-s / 10d);
            double p = s + 10d * n;
            p = Math.Pow(Constants.TwoPi, p);
            if (n < 18)
            {
                p /= TwoPiPow[n];
            }
            else
            {
                p /= ElementaryFunctions.Pow(Constants.TwoPi, 10 * n);
            }

            return p * sinTerm * q * Gamma(1d - s);
        }

        /// <summary>
        /// The Hurwitz zeta function ζ(s,a) is a generalization of the Riemann zeta function ζ(s) that is also known as the generalized zeta function.
        /// <para />
        /// It is classically defined as:
        /// <para />
        /// ζ(s,a) = ∑˚˚ᵢ₌₀ 1/(i+a)ˢ for ℝ[s]>1.
        /// </summary>
        /// <param name="s">The argument.</param>
        /// <param name="a">The parameter, must be positive.</param>
        /// <returns>The value of ζ(s,a).</returns>
        public static double HurwitzZeta(double s, double a)
        {
            if (s <= 1d || a <= 0d)
            {
                return double.NaN;
            }

            const double maxBits = 54d;
            if ((s > maxBits && a < 1d) || (s > 0.5 * maxBits && a < 0.25))
            {
                return Math.Pow(a, -s);
            }

            if (s > 0.5 * maxBits && a < 1d)
            {
                return Math.Pow(a, -s) * (1d + Math.Pow(a / (1d + a), s) + Math.Pow(a / (2d + a), s));
            }

            // Euler-Maclaurin summation formula.
            // [Moshier, p. 400, with several typo corrections]
            const int jMax = 12;
            const int kMax = 10;
            double ka = kMax + a;
            double ka2 = ka * ka;
            double pMax = Math.Pow(ka, -s);
            double pcp = pMax / ka;
            double ans = pMax * (ka / (s - 1d) + 0.5);
            double scp = s;
            for (int k = 0; k < kMax; ++k)
            {
                ans += Math.Pow(k + a, -s);
            }

            for (int j = 0; j <= jMax; ++j)
            {
                double delta = HurwitzZetaCoefficients[j + 1] * scp * pcp;
                ans += delta;
                if (Math.Abs(delta / ans) < 0.5 * Constants.DoubleEpsilon)
                {
                    break;
                }

                delta = s + 2 * j + 1;
                scp *= delta * (delta + 1);
                pcp /= ka2;
            }

            return ans;
        }

        /// <summary>
        /// The Dirichlet eta function, η(s), is the sum of the <c>s</c>-th inverse power of the natural numbers,
        /// with alternating signs. It can be related to the Riemann zeta function, ζ(s).
        /// </summary>
        /// <param name="s">The argument, which must be non-negative.</param>
        /// <returns>The value of η(s).</returns>
        public static double DirichletEta(double s)
        {
            if (s > 100d)
            {
                return 1d;
            }

            if (Math.Abs(s - 1d) < 10d * Constants.FifthRootDoubleEpsilon)
            {
                double q = s - 1d;
                const double c = Constants.Ln2 * (Constants.EulerGamma - 0.5 * Constants.Ln2);
                return Constants.Ln2 + q * (c + q * (-0.0326862962794492996 + q * (0.0015689917054155150 + q * 0.00074987242112047532)));
            }

            return RiemannZeta(s) * Math.Exp((1d - s) * Constants.Ln2);
        }

        /// <summary>
        /// The Riemann-Euler zeta function of complex argument.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The calculated value.</returns>
        public static Complex RiemannEuler(Complex z)
        {
            int len = Primes.Length;
            Complex f = 1d;
            for (int k = 0; k < len; ++k)
            {
                Complex fOld = f;
                Complex fk = 1d - Complex.Pow(Primes[k], -z);
                f *= fk;
                if (f == fOld)
                {
                    return 1d / f;
                }
            }

            return double.NaN; // Or throw a non convergence exception.
        }
    }
}
