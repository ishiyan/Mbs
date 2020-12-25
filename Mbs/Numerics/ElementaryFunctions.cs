using System;

namespace Mbs.Numerics
{
    /// <summary>
    /// Methods for evaluating various elementary functions that are not available in the
    /// standard <see cref="System.Math" /> class or are more accurate for specific values.
    /// </summary>
    public static class ElementaryFunctions
    {
        private static readonly double[] Log1PxExpansion =
        {
            2.1664791066439526, -0.28565398551049742, 0.015177672556905537, -0.0020021590494141545, 0.00019211375164056698,
            -2.5532588861055426E-05, 2.9004512660400622E-06, -3.8873813517057341E-07, 4.7743678729400456E-08, -6.4501969776090321E-09,
            8.2751976628812384E-10, -1.126049937649205E-10, 1.4844576692270934E-11, -2.0328515972462118E-12, 2.7291231220549217E-13,
            -3.7581977830387938E-14, 5.1107345870861672E-15, -7.0722150011433277E-16, 9.7089758328248469E-17, -1.3492637457521938E-17,
            1.8657327910677295E-18,
        };

        /// <summary>
        /// Determines if the given integer is odd.
        /// </summary>
        /// <param name="n">The integer.</param>
        /// <returns>A boolean indicating if the given integer is odd.</returns>
        public static bool IsOdd(int n)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return Math.IEEERemainder(n, 1) != 0;
        }

        /// <summary>
        /// Determines if the given integer is even.
        /// </summary>
        /// <param name="n">The integer.</param>
        /// <returns>A boolean indicating if the given integer is even.</returns>
        public static bool IsEven(int n)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return Math.IEEERemainder(n, 1) == 0.0;
        }

        /// <summary>
        /// Checks if a given integer number is power of two.
        /// </summary>
        /// <param name="n">An integer number to check.</param>
        /// <returns>A boolean indicating if the given integer is power of two.</returns>
        public static bool IsPowerOfTwo(int n)
        {
            return (n & (n - 1)) == 0 && n != 0;
        }

        /// <summary>
        /// Evaluates the exponential function minus 1.
        /// <para />
        /// When <c>x</c> is close to zero, the exponential function is close to one. Subtracting
        /// one from this result can cause significant round-off error. This function resolves this
        /// problem by using a direct approximations for arguments close to 0.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The exponential function minus 1.</returns>
        public static double ExpMinus1(double x)
        {
            if (double.IsNaN(x))
            {
                return x;
            }

            if (x < Constants.LogDoubleMin)
            {
                return -1d;
            }

            const double cut = 0.002;
            if (x < -cut)
            {
                return Math.Exp(x) - 1d;
            }

            if (x < cut)
            {
                return x * (1d + 0.5 * x * (1d + (x / 3d) * (1d + 0.25 * x * (1d + 0.2 * x))));
            }

            return x < Constants.LogDoubleMax
                ? Math.Exp(x) - 1d
                : double.PositiveInfinity;
        }

        /// <summary>
        /// Evaluates the logarithm of 1 plus the argument.
        /// <para />
        /// For small values of <c>x</c>, the logarithm of <c>1 + x</c> is close to zero.
        /// When the logarithm is evaluated directly, this can cause significant round-off error.
        /// This function resolves this problem by using a direct approximations for <c>x</c> close to 1.
        /// </summary>
        /// <param name="x">A real number greater than -1.</param>
        /// <returns>The logarithm of 1 plus x.</returns>
        public static double Log1PlusX(double x)
        {
            if (double.IsNaN(x))
            {
                return x;
            }

            if (x <= -1d)
            {
                return double.NaN;
            }

            double num = x * x;
            if (num < Constants.CubeRootDoubleEpsilon)
            {
                return x * (1d + x * (-0.5 + x * (0.33333333333333331 + x * (-0.25 + x * (0.2 +
                    x * (-0.16666666666666666 + x * (0.14285714285714285 + x * (0.125 +
                    x * (0.11111111111111111 + x * (0.1 + x))))))))));
            }

            return Math.Abs(x) > 0.5
                ? Math.Log(1d + x)
                : x * SpecialFunctions.EvaluateChebyshevSeries((8d * x + 1.0) / (2d * (x + 2d)), Log1PxExpansion);
        }

        /// <summary>
        /// A number raised to an integer power.
        /// <para />
        /// Raising a number to an integer power is orders of magnitude faster than raising a number to a real power.
        /// If you know the exponent is an integer, use this method instead of the <see cref="Math.Pow(double,double)" />.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <param name="n">An integer exponent.</param>
        /// <returns>The real number raised to the specified exponent.</returns>
        public static double Pow(double x, int n)
        {
            if (double.IsNaN(x))
            {
                return x;
            }

            if (n == 0)
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                return x != 0 ? 1d : double.NaN;
            }

            if (n == 1)
            {
                return x;
            }

            bool flag = false;
            if (n < 0)
            {
                flag = true;
                n = -n;
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (x == 0)
            {
                return flag ? double.PositiveInfinity : 0d;
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (x == 1)
            {
                return x;
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (x == -1)
            {
                return (n & 1) != 0 ? -1d : 1d;
            }

            double num = x;
            double num2 = 1;
            do
            {
                if ((n & 1) != 0)
                {
                    num2 *= num;
                }

                num *= num;
                n >>= 1;
            }
            while (n > 0);

            return flag ? 1d / num2 : num2;
        }

        /// <summary>
        /// The <paramref name="n" />th Fibonacci number, which is defined by the initial values:
        /// <para />
        /// F₀=0 and F₁=1, and the recurrence relation Fᵢ₊₁=Fᵢ+Fᵢ₋₁.
        /// </summary>
        /// <param name="n">The index of the number in the Fibonacci sequence.</param>
        /// <returns>A long integer that equals the <paramref name="n" />th Fibonacci number.</returns>
        public static long Fibonacci(int n)
        {
            if (n < 0)
            {
                return (n & 1) != 0 ? Fibonacci(-n) : -Fibonacci(-n);
            }

            if (n == 0)
            {
                return 0L;
            }

            if (n <= 2)
            {
                return 1L;
            }

            if (n > 92)
            {
                throw new OverflowException();
            }

            long f1 = 1L;
            long f2 = 2L;
            while (--n > 2)
            {
                long f3 = f1 + f2;
                f1 = f2;
                f2 = f3;
            }

            return f2;
        }
    }
}
