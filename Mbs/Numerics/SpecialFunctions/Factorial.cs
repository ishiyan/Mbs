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
        /// The maximum <c>n</c> such that <c>Factorial(n)</c> does not give an overflow.
        /// </summary>
        public const int FactorialMaxN = Gsl.FactorialMaxN;

        /// <summary>
        /// The maximum <c>n</c> such that <c>DoubleFactorial(n)</c> does not give an overflow.
        /// </summary>
        public const int DoubleFactorialMaxN = Gsl.DoubleFactorialMaxN;

        /// <summary>
        /// The factorial of an integer n is the product of all integers from 1 to n.
        /// <para>n! also has a combinatorial interpretation as the number of permutations of n objects. For example, a set of 3
        /// objects (abc) has 3! = 6 permutations: (abc), (bac), (cba), (acb), (cab), (bca).</para>
        /// <para>Because n! grows extremely quickly with increasing n, we return the result as a double, even though
        /// the value is always an integer (13! would overflow an int, 21! would overflow a long, 171! overflows even a double).</para>
        /// <para>In order to deal with factorials of larger numbers, you can use the <see cref="LnFactorial"/> method, which
        /// returns accurate values of ln(n!) even for values of n for which n! would overflow a double.</para>
        /// <para>The factorial is generalized to non-integer arguments by the gamma function.</para>
        /// </summary>
        /// <param name="n">The argument, which must be non-negative.</param>
        /// <returns>The value of the factorial.</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Factorial"/>
        public static double Factorial(int n)
        {
            if (n < 0)
                return double.NaN;
            if (n <= Gsl.FactorialMaxN)
                return Gsl.Factorials[n];
            return Math.Exp(LnGamma(++n));
        }

        /// <summary>
        /// The natural logarithm of the factorial of a positive integer, <c>ln(n!)</c>.
        /// <para>This function provides accurate values of <c>ln(n!)</c> even for values of n which would cause <c>n!</c> to overflow.</para>
        /// </summary>
        /// <param name="n">The argument, which must be non-negative.</param>
        /// <returns>The natural logarithm of the factorial <c>ln(n!)</c>.</returns>
        public static double LnFactorial(int n)
        {
            if (n < 0)
                return double.NaN;
            if (n < LogFactorials.Length)
                return LogFactorials[n];
            return LnGamma(++n);
        }

        /// <summary>
        /// The double factorial, <c>n!!</c>, of the given integer.
        /// <para />
        /// The double factorial of an integer is the product all integers of the same parity, up to and including the integer.
        /// <code>n!! = n(n-2)(n-4) ...</code>
        /// <para />
        /// Thus 5!! = 5 * 3 * 1 = 15 and 6!! = 6 * 4 * 2 = 48.
        /// </summary>
        /// <param name="n">The argument, which must be positive.</param>
        /// <returns>The value of the double factorial.</returns>
        /// <seealso href="http://mathworld.wolfram.com/DoubleFactorial.html"/>
        public static double DoubleFactorial(int n)
        {
            if (n < 0)
                return double.NaN;
            if (n < 32)
                return DoubleFactorialMultiply(n);
            return Math.Round(Math.Exp(LogDoubleFactorialGamma(n)));
        }

        /// <summary>
        /// The natural logarithm of the double factorial, n!!, of the given integer number.
        /// </summary>
        /// <param name="n">The argument, which must be positive.</param>
        /// <returns>The value of ln(n!!).</returns>
        /// <seealso href="http://mathworld.wolfram.com/DoubleFactorial.html"/>
        public static double LogDoubleFactorial(int n)
        {
            if (n < 0)
                return double.NaN;
            if (n < 32)
                return Math.Log(DoubleFactorialMultiply(n));
            return LogDoubleFactorialGamma(n);
        }

        /// <summary>
        /// The binomial coefficient Cⁿᵏ, the number of ways of picking <paramref name="k" />
        /// unordered outcomes from <paramref name="n" /> possibilities.
        /// </summary>
        /// <param name="n">The number of possibilities.</param>
        /// <param name="k">The number of outcomes to pick.</param>
        /// <returns>The number of ways of picking <paramref name="k" />
        /// unordered outcomes from <paramref name="n" /> possibilities.</returns>
        public static double BinomialCoefficient(int n, int k)
        {
            if (n < 0)
            {
                int n1 = -n + k - 1;
                if (n1 == n)
                    return 0d;
                return (k & 1) != 0 ? -BinomialCoefficient(n1, k) : BinomialCoefficient(n1, k);
            }

            if (k < 0 || k > n)
                return 0d;
            if (n < Gsl.FactorialMaxN)
                return Gsl.Factorials[n] / Gsl.Factorials[k] / Gsl.Factorials[n - k];
            return Math.Exp(LnFactorial(n) - LnFactorial(k) - LnFactorial(n - k));
        }

        private static readonly double[] LogFactorials =
        {
            0.0, 0.0, 0.69314718055994529, 1.791759469228055, 3.1780538303479458, 4.7874917427820458, 6.5792512120101012, 8.5251613610654147, 10.604602902745251, 12.801827480081469, 15.104412573075516, 17.502307845873887, 19.987214495661885, 22.552163853123425, 25.19122118273868, 27.89927138384089,
            30.671860106080672, 33.505073450136891, 36.395445208033053, 39.339884187199495, 42.335616460753485, 45.380138898476908, 48.471181351835227, 51.606675567764377, 54.784729398112319, 58.003605222980518, 61.261701761002, 64.557538627006338, 67.88974313718154, 71.257038967168015, 74.658236348830158, 78.0922235533153,
            81.557959456115043, 85.054467017581516, 88.580827542197682, 92.1361756036871, 95.7196945421432, 99.330612454787428, 102.96819861451381, 106.63176026064346, 110.32063971475739, 114.03421178146171, 117.77188139974507, 121.53308151543864, 125.3172711493569, 129.12393363912722, 132.95257503561632, 136.80272263732636,
            140.67392364823425, 144.5657439463449, 148.47776695177302, 152.40959258449735, 156.3608363030788, 160.3311282166309, 164.32011226319517, 168.32744544842765, 172.35279713916279, 176.39584840699735, 180.45629141754378, 184.53382886144948, 188.6281734236716, 192.7390472878449, 196.86618167289, 201.00931639928152,
            205.1681994826412, 209.34258675253685, 213.53224149456327, 217.73693411395422, 221.95644181913033, 226.1905483237276, 230.43904356577696, 234.70172344281826, 238.97838956183432, 243.26884900298271, 247.57291409618688, 251.89040220972319, 256.22113555000954, 260.56494097186322, 264.92164979855278, 269.29109765101981,
            273.67312428569369, 278.06757344036612, 282.4742926876304, 286.893133295427, 291.32395009427029, 295.76660135076065, 300.22094864701415, 304.68685676566872, 309.1641935801469, 313.65282994987905, 318.1526396202093, 322.66349912672615, 327.1852877037752, 331.71788719692847, 336.26118197919845, 340.815058870799,
            345.37940706226686, 349.95411804077025, 354.53908551944079, 359.1342053695754, 363.73937555556347
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long DoubleFactorialMultiply(int n)
        {
            long f = 1L;
            for (int k = n; k > 1; k -= 2)
                f *= k;
            return f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double LogDoubleFactorialGamma(int n)
        {
            if (n % 2 == 0)
            {
                // m = n/2, n!! = 2ᵐ Γ(m+1)
                int m = n / 2;
                return m * Constants.Ln2 + LnGamma(m + 1d);
            }
            else
            {
                // m = (n+1)/2, n!! = 2ᵐ Γ(m+1/2) / √̅π
                int m = (n + 1) / 2;
                return m * Constants.Ln2 + LnGamma(m + 0.5) - Math.Log(Constants.Pi) / 2d;
            }
        }
    }
}
