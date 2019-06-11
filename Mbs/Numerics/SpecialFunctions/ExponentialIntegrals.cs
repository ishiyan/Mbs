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
        /// The cosine integral function, Ci.
        /// </summary>
        /// <param name="x">A real number greater than or equal to zero.</param>
        /// <returns>The value of the cosine integral.</returns>
        public static double CosineIntegral(double x)
        {
            if (x < 0d)
                return double.NaN;
            if (Math.Abs(x) < double.Epsilon)
                return double.NegativeInfinity;
            if (x <= Constants.SqrtDoubleEpsilon)
                return Constants.EulerGamma + Math.Log(x);
            if (x <= 4d)
                return Math.Log(x) - 0.5 + EvaluateChebyshevSeries((x * x - 8d) * 0.125, CiExpansion);
            AsymptoticFgSeries(x, out var p, out var q);
            return p * Math.Sin(x) - q * Math.Cos(x);
        }

        /// <summary>
        /// The sine integral function, Si.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the cosine integral.</returns>
        public static double SineIntegral(double x)
        {
            double abs = Math.Abs(x);
            if (abs < Constants.SqrtDoubleEpsilon)
                return x;
            if (abs <= 4d)
                return x * (0.75 + EvaluateChebyshevSeries((x * x - 8d) * 0.125, SiExpansion));
            AsymptoticFgSeries(abs, out var p, out var q);
            abs = Constants.PiOver2 - p * Math.Cos(abs) - q * Math.Sin(abs);
            return x < 0d ? -abs : abs;
        }

        /// <summary>
        /// The exponential integral function.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the exponential integral.</returns>
        public static double ExponentialIntegral(double x)
        {
            return Math.Abs(x) < double.Epsilon ? double.NaN : -InternalE1(-x);
        }

        private static readonly double Big = 1.44115188075855872 * ElementaryFunctions.Pow(10d, 17);

        /// <summary>
        /// The exponential integral function.
        /// </summary>
        /// <param name="x">A real number, must be non-negative.</param>
        /// <param name="n">The order, must be non-negative.</param>
        /// <returns>The value of the exponential integral.</returns>
        public static double ExponentialIntegral(double x, int n)
        {
            if (n < 0 | x < 0d | x > 170d | Math.Abs(x) < double.Epsilon & n < 2)
                return double.NaN;

            if (Math.Abs(x) < double.Epsilon)
                return 1d / (n - 1);

            if (n == 0)
                return Math.Exp(-x) / x;

            double yk, xk, pk, t;
            if (n > 5000)
            {
                xk = x + n;
                yk = 1 / (xk * xk);
                t = n;
                pk = yk * t * (6d * x * x - 8d * t * x + t * t);
                pk = yk * (pk + t * (t - 2d * x));
                pk = yk * (pk + t);
                return (pk + 1) * Math.Exp(-x) / xk;
            }

            double result;
            if (x <= 1d)
            {
                double psi = -Constants.EulerGamma - Math.Log(x);
                for (int i = 1; i <= n - 1; ++i)
                {
                    psi += 1d / i;
                }

                xk = 0d;
                yk = 1d;
                pk = 1d - n;
                double z = -x;
                result = n == 1 ? 0d : (1d / pk);
                do
                {
                    yk *= z / ++xk;
                    if (Math.Abs(++pk) > double.Epsilon)
                        result += yk / pk;
                    t = Math.Abs(result) > double.Epsilon ? Math.Abs(yk / result) : 1d;
                }
                while (t >= Constants.DoubleEpsilon);
                t = 1d;
                for (int i = 1; i <= n - 1; ++i)
                    t *= z / i;
                return psi * t - result;
            }

            int k = 1;
            double pkm2 = 1d;
            double qkm2 = x;
            double pkm1 = 1d;
            double qkm1 = x + n;
            result = pkm1 / qkm1;
            do
            {
                ++k;
                if (k % 2 == 1)
                {
                    yk = 1d;
                    xk = n + (k - 1) / 2d;
                }
                else
                {
                    yk = x;
                    xk = k / 2d;
                }

                pk = pkm1 * yk + pkm2 * xk;
                double qk = qkm1 * yk + qkm2 * xk;
                if (Math.Abs(qk) > double.Epsilon)
                {
                    double r = pk / qk;
                    t = Math.Abs((result - r) / r);
                    result = r;
                }
                else
                {
                    t = 1d;
                }

                pkm2 = pkm1;
                pkm1 = pk;
                qkm2 = qkm1;
                qkm1 = qk;
                if (Math.Abs(pk) > Big)
                {
                    pkm2 /= Big;
                    pkm1 /= Big;
                    qkm2 /= Big;
                    qkm1 /= Big;
                }
            }
            while (t >= Constants.DoubleEpsilon);
            return result * Math.Exp(-x);
        }

        /// <summary>
        /// The entire complex exponential integral can be defined by an integral or an equivalent series.
        /// <para />
        /// Both Ei(z) and E₁(x) and be obtained from Ein(z).
        /// <para />
        /// Unlike either Ei(z) or E₁(z), Ein(z) is entire, that is, it has no poles or cuts anywhere in the complex plane.
        /// </summary>
        /// <param name="z">The complex argument.</param>
        /// <returns>The value of Ein(z).</returns>
        public static Complex ExponentialIntegral(Complex z)
        {
            if (z.Real < -40d)
            {
                // For sufficiently negative z, use the asymptotic expansion for Ei.
                return Constants.EulerGamma + Complex.Log(-z) - IntegralEiAsymptoticSeries(-z);
            }

            // Ideally, we would like to use the series within some simple radius of the origin and the continued fraction
            // outside it. That works okay for the right half-plane, but for z.Re < 0 we have to contend with the fact that the
            // continued fraction fails on or near the negative real axis. That makes us use the series in an oddly shaped
            // region that extends much further from the origin than we would like into the left half-plane. It would be
            // great if we could find an alternative approach in that region.
            return IsEinSeriesPrefered(z) ? EinSeries(z) : Constants.EulerGamma + Complex.Log(z) + IntegeralE1ContinuedFraction(z);
        }

        /// <summary>
        /// The logarithmic integral function.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the logarithmic integral.</returns>
        public static double LogarithmicIntegral(double x)
        {
            return Math.Abs(x) < double.Epsilon ? double.NaN : -InternalE1(-Math.Log(x));
        }

        /// <summary>
        /// The exponential integral function E1(x).
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the exponential integral function E1(x).</returns>
        public static double E1(double x)
        {
            return Math.Abs(x) < double.Epsilon ? double.NaN : InternalE1(x);
        }

        private static readonly double[] FExpansion1 =
        {
            -0.11910819690513635, -0.024782314499623623, 0.0011910281453357821, -9.27027714388562E-05,
            9.3373141568271E-06,  -1.1058287820557E-06,  1.46477207146E-07,     -2.10694496288E-08,
            3.2293492367E-09,     -5.206529618E-10,      8.74878885E-11,        -1.52176187E-11,
            2.7257192E-12,        -5.007053E-13,         9.40241E-14,           -1.80014E-14,
            3.5063E-15,           -6.935E-16,            1.391E-16,             -2.82E-17
        };

        private static readonly double[] FExpansion2 =
        {
            -0.034840925389701322, -0.01668422056779597, 0.00067529012412377381, -5.35066622544701E-05, 6.2693421779007E-06, -9.526638801991E-07, 1.745629224251E-07, -3.68795403065E-08, 8.7202677705E-09, -2.2601970392E-09, 6.324624977E-10, -1.888911889E-10, 5.96774674E-11, -1.98044313E-11, 6.8641396E-12, -2.473102E-12,
            9.22636E-13, -3.552364E-13, 1.407606E-13, -5.72623E-14, 2.38654E-14, -1.01714E-14, 4.4259E-15, -1.9634E-15, 8.868E-16, -4.074E-16, 1.901E-16, -9E-17, 4.32E-17
        };

        private static readonly double[] GExpansion1 =
        {
            -0.3040578798253496, -0.05668909845971206, 0.0039046158173275643, -0.00037460759592022609, 4.35431556559844E-05, -5.7417294453025E-06, 8.282552104503E-07, -1.278245892595E-07, 2.07978352949E-08, -3.5313205922E-09, 6.210824236E-10, -1.125215474E-10, 2.09088918E-11, -3.9715832E-12, 7.690431E-13, -1.514697E-13,
            3.02892E-14, -6.14E-15, 1.2601E-15, -2.615E-16, 5.48E-17
        };

        private static readonly double[] GExpansion2 =
        {
            -0.096732936753243223, -0.045207790795745988, 0.0028190005352706523, -0.000289916774075916, 4.07444664601121E-05, -7.1056382192354E-06, 1.4534723163019E-06, -3.364116512503E-07, 8.59774367886E-08, -2.38437656302E-08, 7.083190634E-09, -2.2318068154E-09, 7.401087359E-10, -2.567171162E-10, 9.26707021E-11, -3.46693311E-11,
            1.33950573E-11, -5.3290754E-12, 2.1775312E-12, -9.118621E-13, 3.905864E-13, -1.708459E-13, 7.62015E-14, -3.46151E-14, 1.59996E-14, -7.5213E-15, 3.597E-15, -1.753E-15, 8.738E-16, -4.487E-16, 2.397E-16, -1.347E-16,
            8.01E-17, -5.01E-17
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AsymptoticFgSeries(double x, out double f, out double g)
        {
            double xx = x * x;
            if (x <= 7.0710678118654755)
            {
                g = (1d / xx - 0.04125) / 0.02125;
                f = (1d + EvaluateChebyshevSeries(g, FExpansion1)) / x;
                g = (1d + EvaluateChebyshevSeries(g, GExpansion1)) / xx;
            }
            else if (x * 1.4901161193847656E-08 <= 1d)
            {
                g = 100d / xx - 1d;
                f = (1d + EvaluateChebyshevSeries(g, FExpansion2)) / x;
                g = (1d + EvaluateChebyshevSeries(g, GExpansion2)) / xx;
            }
            else
            {
                f = (x * double.Epsilon < 1d) ? (1d / x) : 0d;
                g = (x * Constants.SqrtDoubleMin < 1d) ? (1d / xx) : 0d;
            }
        }

        private static readonly double[] CiExpansion =
        {
            -0.34004281856055363, -1.0330216640117746, 0.19388222659917082, -0.019182604360198658, 0.0011078925258478497, -4.1572345582472091E-05, 1.09278524300229E-06, -2.123285954183E-08, 3.1733482164E-10, -3.76141548E-12, 3.622653E-14, -2.8912E-16, 1.94E-18
        };

        private static readonly double[] SiExpansion =
        {
            -0.1315646598184842, -0.27765785269736021, 0.035441405486665918, -0.002563163144793398, 0.0001162365390497009, -3.5904327241606E-06, 8.02342123706E-08, -1.3562997693E-09, 1.79440722E-11, -1.908387E-13, 1.667E-15, -1.22E-17
        };

        private static readonly double[] AsymptoticE1Expansion1 =
        {
            0.12150323971606579, -0.065088778513550147, 0.00489765135745967, -0.000649237843027216, 9.3840434587471E-05, 4.20236380882E-07, -8.113374735904E-06, 2.804247688663E-06, 5.6487164441E-08, -3.4480917445E-07, 5.8209273578E-08, 3.8711426349E-08, -1.2453235014E-08, -5.118504888E-09, 2.148771527E-09, 8.68459898E-10,
            -3.43650105E-10, -1.79796603E-10, 4.744206E-11, 4.0423282E-11, -3.543928E-12, -8.853444E-12, -9.60151E-13, 1.692921E-12, 6.0799E-13, -2.24338E-13, -2.00327E-13, -6.246E-15, 4.5571E-14, 1.6383E-14, -5.561E-15, -6.074E-15,
            -8.62E-16, 1.223E-15, 7.16E-16, -2.4E-17, -2.01E-16, -8.2E-17, 1.7E-17
        };

        private static readonly double[] AsymptoticE1Expansion2 =
        {
            0.58241749513472674, -0.15834885090578274, -0.0067642755903231412, 0.005125843950185725, 0.000435232492169391, -0.000143613366305483, -4.1801320556301E-05, -2.71339575864E-06, 1.151381913647E-06, 4.20650022012E-07, 6.6581901391E-08, 6.62143777E-10, -2.84410487E-09, -9.40724197E-10, -1.77476602E-10, -1.5830222E-11,
            2.905732E-12, 1.769356E-12, 4.92735E-13, 9.3709E-14, 1.0707E-14, -5.37E-16, -7.16E-16, -2.44E-16, -5.8E-17
        };

        private static readonly double[] AsymptoticE1Expansion3 =
        {
            -0.6057732466406035, -0.1125352434836609, 0.013432266247902778, -0.0019268451873811451, 0.000309118337720603, -5.3564132129618E-05, 9.827812880247E-06, -1.885368984916E-06, 3.74943193568E-07, -7.682345587E-08, 1.6143270567E-08, -3.466802211E-09, 7.58754209E-10, -1.68864333E-10, 3.8145706E-11, -8.733026E-12,
            2.023672E-12, -4.74132E-13, 1.12211E-13, -2.6804E-14, 6.457E-15, -1.568E-15, 3.83E-16, -9.4E-17, 2.3E-17
        };

        private static readonly double[] AsymptoticE1Expansion4 =
        {
            -0.18929180007530169, -0.086481178552598709, 0.00722410154374659, -0.00080975594575573, 0.00010999134432661, -1.717332998937E-05, 2.98562751447E-06, -5.6596491457E-07, 1.1526808397E-07, -2.49503044E-08, 5.6923242E-09, -1.35995766E-09, 3.3846628E-10, -8.737853E-11, 2.331588E-11, -6.41148E-12,
            1.81224E-12, -5.2538E-13, 1.5592E-13, -4.729E-14, 1.463E-14, -4.61E-15, 1.48E-15, -4.8E-16, 1.6E-16, -5E-17
        };

        private static readonly double[] E1Expansion1 =
        {
            -16.113461655571495, 7.79407277874268, -1.955405818863142, 0.37337293866277943, -0.056925031910929021, 0.0072110777696600915, -0.00078104901449841595, 7.3880933562621675E-05, -6.2028618758082E-06, 4.6816002303176E-07, -3.209288853329E-08, 2.01519974874E-09, -1.1673686816E-10, 6.27627066E-12, -3.1481541E-13, 1.479904E-14,
            -6.5457E-16, 2.733E-17, -1.08E-18
        };

        private static readonly double[] E1Expansion2 =
        {
            -0.037390214792202794, 0.042723986062209576, -0.13031820798497004, 0.014419124024698891, -0.0013461707805106802, 0.0001073102925306378, -7.42999951611943E-06, 4.5377325690753E-07, -2.47641721139E-08, 1.22076581374E-09, -5.48514148E-11, 2.26362142E-12, -8.635897E-14, 3.06291E-15, -1.0148E-16, 3.15E-18
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double InternalE1(double x)
        {
            double d = Constants.LogDoubleMin;
            double num2 = d - Math.Log(d);
            if (x < -num2)
                return double.NegativeInfinity;
            if (x <= -10d)
                return Math.Exp(-x) / x * (1d + EvaluateChebyshevSeries(20d / x + 1d, AsymptoticE1Expansion1));
            if (x <= -4d)
                return Math.Exp(-x) / x * (1d + EvaluateChebyshevSeries((40d / x + 7d) / 3d, AsymptoticE1Expansion2));
            if (x <= -1d)
                return -Math.Log(Math.Abs(x)) + EvaluateChebyshevSeries((2d * x + 5d) / 3d, E1Expansion1);
            if (x <= 1d)
                return -Math.Log(Math.Abs(x)) - 0.6875 + x + EvaluateChebyshevSeries(x, E1Expansion2);
            if (x <= 4d)
                return Math.Exp(-x) / x * (1d + EvaluateChebyshevSeries((8d / x - 5d) / 3d, AsymptoticE1Expansion3));
            if (x > num2)
                return 0d;
            return Math.Exp(-x) / x * (1d + EvaluateChebyshevSeries(8d / x - 1d, AsymptoticE1Expansion4));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex EinSeries(Complex z)
        {
            // We use it for |z| < 4 on the positive real axis, moving to |z| < 40 on the negative real axis.
            Complex zp = z;
            Complex f = zp;
            for (int k = 2; k < IterationLimit; ++k)
            {
                Complex fOld = f;
                zp *= -z / k;
                f += zp / k;
                if (f == fOld)
                    return f;
            }

            return Complex.NaN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex IntegeralE1ContinuedFraction(Complex z)
        {
            // This continued fraction is valid for |z| >> 1, except close to the negative real axis.
            int a = 1;           // a_1
            Complex b = z + 1d;  // b_1
            Complex d = 1d / b;  // D_1 = 1 / b_1 (denominator of a_1 / b_1)
            Complex df = a / b;  // Df_1 = f_1 - f_0 = a_1 / b_1
            Complex f = 0d + df; // f_1 = f_0 + Df_1 = b_0 + a_1 / b_1 (here b_0 = 0)
            for (int k = 1; k < IterationLimit; ++k)
            {
                Complex fOld = f;
                a = -k * k;
                b += 2d;
                d = 1d / (b + a * d);
                df = (b * d - 1d) * df;
                f += df;
                if (f == fOld)
                    return Complex.Exp(-z) * f;
            }

            return Complex.NaN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex IntegralEiAsymptoticSeries(Complex z)
        {
            Complex dy = 1d;
            Complex y = dy;
            for (int k = 1; k < IterationLimit; ++k)
            {
                Complex yOld = y;
                dy = dy * k / z;
                y = yOld + dy;
                if (y == yOld)
                    return Complex.Exp(z) / z * y;
            }

            return Complex.NaN;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsEinSeriesPrefered(Complex z)
        {
            // This defines the region where, empirically and approximately, the number of terms required by the Ein series is
            // less than the number of terms required by the E1 continued fraction. We use piecewise linear and quadratic terms
            // to define it. Basically, it says stay away from the negative real axis, particularly for small negative real parts.
            if (z.Real > 4d)
                return false;
            if (z.Real > 0d)
                return Math.Abs(z.Imag) < 6d * (1d - Math.Pow(2, z.Real / 4d));
            if (z.Real > -20d)
                return Math.Abs(z.Imag) < 7d - Math.Abs(z.Real + 10d) / 10d;
            if (z.Real > -50d)
                return Math.Abs(z.Imag) < 10d + z.Real / 5d;
            return false;
        }
    }
}
