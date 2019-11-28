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
        /// The value of a (physicists') Hermite polynomial, Hᵤ(x).
        /// <para />
        /// Hermite polynomials are orthogonal on the interval (-∞,+∞) with the weight e⁻ˣˆ².
        /// <para />
        /// They appear in the solution of the one-dimensional, quantum mechanical, harmonic oscillator.
        /// <para />
        /// Statisticians' Hermite polynomials (see <see cref="HermiteHe"/>) are related to physicists' Hermite
        /// polynomials via Hᵤ(x) = 2ᵘHᵤ(x √̅2). Statisticians' Hermite polynomials
        /// do not grow as quickly as physicists', and may therefore be preferable for large values of <paramref name="n"/>
        /// and <paramref name="x"/> which could overflow <see cref="double"/>.
        /// <para />
        /// Recurrence: Hᵤ₊₁ = 2x Hᵤ - 2u Hᵤ₋₁.
        /// </summary>
        /// <param name="n">The order, which must be non-negative.</param>
        /// <param name="x">The argument.</param>
        /// <returns>The value Hᵤ(x).</returns>
        /// <seealso href="http://mathworld.wolfram.com/HermitePolynomial.html" />
        public static double HermiteH(int n, double x)
        {
            if (n < 0)
                return double.NaN;
            if (n == 0)
                return 1d;
            double h0 = 1d;
            double h1 = 2d * x;
            for (int k = 1; k < n; ++k)
            {
                double h2 = 2d * (x * h1 - k * h0);
                h0 = h1;
                h1 = h2;
            }

            return h1;
        }

        /// <summary>
        /// The value of a (statisticians') Hermite polynomial.
        /// <para />
        /// Hermite polynomials are orthogonal on the interval (-∞,+∞) with a weight function
        /// equal to the standard normal probability distribution.
        /// <para />
        /// Their orthonormality relation makes them a useful basis for expressing perturbations
        /// around a normal distribution.
        /// <para />
        /// Physicists' Hermite polynomials (see <see cref="HermiteH"/>) are related to statisticians' Hermite
        /// polynomials via Hᵤ(x) = 2ᵘHᵤ(x √̅2).
        /// </summary>
        /// <param name="n">The order, which must be non-negative.</param>
        /// <param name="x">The argument.</param>
        /// <returns>The value Heᵤ(x).</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Hermite_polynomial" />
        public static double HermiteHe(int n, double x)
        {
            if (n < 0)
                return double.NaN;
            if (n == 0)
                return 1d;
            double h0 = 1d;
            double h1 = x;
            for (int k = 1; k < n; ++k)
            {
                double h2 = x * h1 - n * h0;
                h0 = h1;
                h1 = h2;
            }

            return h1;
        }

        /// <summary>
        /// Computes the value of a Laguerre polynomial, Lᵤ(x).
        /// <para />
        /// Laguerre functions are orthogonal on the interval [0,+∞) with the weight e⁻ˣ.
        /// <para />
        /// Recurrence: (u+1)Lᵤ₊₁ = (2u+1-x)Lᵤ - uLᵤ₋₁.
        /// </summary>
        /// <param name="n">The order, which must be non-negative.</param>
        /// <param name="x">The argument, which must be non-negative.</param>
        /// <returns>The value Lᵤ(x).</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Laguerre_polynomial" />
        /// <seealso href="http://mathworld.wolfram.com/LaguerrePolynomial.html" />
        public static double LaguerreL(int n, double x)
        {
            if (n < 0 || x < 0d)
                return double.NaN;
            if (n == 0)
                return 1d;
            double l0 = 1d;
            double l1 = 1d - x;
            for (int k = 1; k < n; ++k)
            {
                double l2 = ((2 * k + 1 - x) * l1 - k * l0) / (k + 1);
                l0 = l1;
                l1 = l2;
            }

            return l1;
        }

        /// <summary>
        /// The value of an associated Laguerre polynomial, Lᵤᵃ(x).
        /// <para />
        /// The associated Laguerre polynomials are orthonormal on the interval [0,+∞) with the weight xᵃe⁻ˣ.
        /// </summary>
        /// <param name="n">The order, which must be non-negative.</param>
        /// <param name="a">The associated order, which must be greater than -1.</param>
        /// <param name="x">The argument.</param>
        /// <returns>The value Lᵤᵃ(x).</returns>
        /// <seealso href="http://mathworld.wolfram.com/LaguerrePolynomial.html" />
        public static double LaguerreL(int n, double a, double x)
        {
            if (n < 0 || a <= -1 || x < 0d)
                return double.NaN;

            // Standard recurrence on n is claimed stable.
            double l0 = 0d; // L₋₁
            double l1 = 1d; // L₀
            for (int k = 0; k < n; ++k)
            {
                double l2 = ((2 * k + 1 + a - x) * l1 - (k + a) * l0) / (k + 1);
                l0 = l1;
                l1 = l2;
            }

            return l1;
        }

        /// <summary>
        /// The value of a Legendre polynomial, Pᵤ(x).
        /// <para />
        /// Legendre polynomials are orthogonal on the interval [-1,1]  with weight 1.
        /// <para />
        /// Recurrence: (u+1)Pᵤ₊₁ = (2u+1)xPᵤ - uPᵤ₋₁.
        /// </summary>
        /// <param name="l">The order, which must be non-negative.</param>
        /// <param name="x">The argument, which must lie on the closed interval between -1 and +1.</param>
        /// <returns>The value of Pᵤ(x).</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Legendre_polynomial"/>
        /// <seealso href="http://mathworld.wolfram.com/LegendrePolynomial.html"/>
        public static double LegendreP(int l, double x)
        {
            if (Math.Abs(x) > 1d)
                return double.NaN;
            if (l < 0)
                return LegendreP(-l - 1, x);
            if (l == 0)
                return 1d;
            double p0 = 1d;
            double p1 = x;
            for (int n = 1; n < l; ++n)
            {
                double p2 = ((2 * n + 1) * x * p1 - n * p0) / (n + 1);
                p0 = p1;
                p1 = p2;
            }

            return p1;
        }

        /// <summary>
        /// The value of an associated Legendre polynomial, Pᵤᵥ(x).
        /// <para />
        /// Associated Legendre polynomials appear in the definition of the <see cref="SphericalHarmonic"/> functions.
        /// <para />
        /// For values of l and m over about 150, values of this polynomial can exceed the capacity of double-wide floating point numbers.
        /// </summary>
        /// <param name="l">The order, which must be non-negative.</param>
        /// <param name="m">The associated order, which must lie between -l and l inclusive.</param>
        /// <param name="x">The argument, which must lie on the closed interval between -1 and +1.</param>
        /// <returns>The value of Pᵤᵥ(x).</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Associated_Legendre_polynomials"/>
        public static double LegendreP(int l, int m, double x)
        {
            if (l < 0 || Math.Abs(m) > l || Math.Abs(x) > 1d)
                return double.NaN;
            // For low enough orders, we can can get the factorial quickly from a table look-up and without danger of overflow.
            // For higher orders, we must move into log space to avoid overflow.
            var f = l < 10 ? Math.Sqrt(Factorial(l + m) / Factorial(l - m)) : Math.Exp((LnFactorial(l + m) - LnFactorial(l - m)) / 2d);

            if (m < 0)
            {
                m = -m;
                if (m % 2 != 0)
                    f = -f;
            }

            return f * LegendrePe(l, m, x);
        }

        /// <summary>
        /// The value of a Chebyshev polynomial, Tᵢ(x).
        /// <para />
        /// Chebyshev polynomials are orthogonal on the interval [-1,1] with the weight √̅1̅-̅x̅²̅ .
        /// <para />
        /// Recurrence: Tᵢ₊₁ = 2xTᵢ - Tᵢ₋₁.
        /// </summary>
        /// <param name="n">The order, which must be non-negative.</param>
        /// <param name="x">The argument, which must lie in the closed interval between -1 and +1.</param>
        /// <returns>The value of Tᵢ(x).</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Chebyshev_polynomials"/>
        /// <seealso href="http://mathworld.wolfram.com/ChebyshevPolynomialoftheFirstKind.html"/>
        public static double ChebyshevT(int n, double x)
        {
            if (Math.Abs(x) > 1d || n < 0)
                return double.NaN;
            if (n == 0)
                return 1d;

            // Very close to the endpoints, the recurrence looses accuracy for high n;
            // use a series expansion there instead.
            if (n > 10 && (n * n * (1d - Math.Abs(x)) < 1d))
                return ChebyshevTSeries1(n, x);
            double t0 = 1d;
            double t1 = x;
            for (int k = 1; k < n; ++k)
            {
                double t2 = 2 * x * t1 - t0;
                t0 = t1;
                t1 = t2;
            }

            return t1;
        }

        /// <summary>
        /// The value of a Zernike polynomial, Rnm(ρ).
        /// <para />
        /// Zernike polynomials are orthonormal on the interval [0,1] with the weight ρ.
        /// <para />
        /// They are often used in optics to characterize the imperfections in a lens.
        /// In this context, the amplitude of each is associated with a name given in the following table.
        /// <table>
        ///     <tr><th>n</th><th>m</th><th>name</th></tr>
        ///     <tr><td>1</td><td>1</td><td>tilt</td></tr>
        ///     <tr><td>2</td><td>0</td><td>de-focus</td></tr>
        ///     <tr><td>2</td><td>2</td><td>astigmatism</td></tr>
        ///     <tr><td>3</td><td>1</td><td>coma</td></tr>
        ///     <tr><td>3</td><td>3</td><td>trefoil</td></tr>
        /// </table>
        /// </summary>
        /// <param name="n">The order parameter, which must be non-negative.</param>
        /// <param name="m">The index parameter, which must lie between 0 and n.</param>
        /// <param name="rho">The argument, which must lie between 0 and 1.</param>
        /// <returns>The value of Rnm(ρ).</returns>
        public static double ZernikeR(int n, int m, double rho)
        {
            if (n < 0 || m < 0 || m > n || rho < 0d || rho > 1d)
                return double.NaN;

            // n and m have the same parity.
            if ((n - m) % 2 != 0)
                return 0d;

            // R00.
            if (n == 0)
                return 1d;

            // R^{m}_m.
            double r2 = Math.Pow(rho, m);
            if (n == m)
                return r2;

            // R^{m+1}_{m+1}.
            int k = m;
            double r1 = r2 * rho;
            while (true)
            {
                k += 2;

                // |*
                // | \
                // |  * recurrence involving two lesser m's
                // | /
                // |*
                // 2n R^{m+1}_{n-1} = (n+m) R^{m}_{n-2} + (n-m) R^{m}_{n}
                double r0 = ((2 * k) * rho * r1 - (k + m) * r2) / (k - m);
                if (k == n)
                    return r0;

                // |  *
                // | /
                // |* recurrence involving two greater m's
                // | \
                // |  *
                double rp = (2 * (k + 1) * rho * r0 - (k - m) * r1) / (k + m + 2);
                r2 = r0;
                r1 = rp;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double LegendrePe(int l, int m, double x)
        {
            // Re-normalized associated legendre polynomials Pe{l,m} = sqrt((l-m)!/(l+m)!) P{l,m},
            // unlike the un-re-normalized P{l,m}, do not get too big.
            // This is not quite the same renormalization used by NR; it omits a factor sqrt((2l+1)/4π),
            // by omitting this factor, we avoid some unnecessary factors and divisions by 4π.
            // The l-recursion (l-m) P{l,m} = x (2l-1) P{l-1,m} - (l+m-1) P{l-2,m} becomes
            // sqrt((l-m)(l+m)) P{l,m} = x (2l-1) P{l-1,m} - sqrt((l-1-m)(l-1+m)) P{l-2,m}
            // and is stable for increasing l.
            // The initial value P{m,m} = (-1)ᵐ (2m-1)!! (1-x²)^(m/2) becomes
            // Pe{m,m} = (-1)ᵐ (2m-1)!! sqrt( (1-x²)ᵐ / (2m)! ) = (-1)ᵐ sqrt( prod_{k=1}ᵐ (2k-1) (1-x²) / (2k) ).
            double xx = (1d + x) * (1d - x);

            // Determine P{m,m}.
            double p0 = 1d;
            for (int k = 1; k <= m; ++k)
                p0 *= (1d - 1d / (2 * k)) * xx;
            p0 = Math.Sqrt(p0);
            if (m % 2 != 0)
                p0 = -p0;
            if (l == m)
                return p0;

            // Determine P{m+1,m}.
            double s0 = Math.Sqrt(2 * m + 1);
            double p1 = x * s0 * p0;

            // Iterate up to P{l,m}.
            for (int k = m + 2; k <= l; ++k)
            {
                double s2 = Math.Sqrt((k - m) * (k + m));
                double p2 = (x * (2 * k - 1) * p1 - s0 * p0) / s2;

                // Prepare for next iteration.
                s0 = s2;
                p0 = p1;
                p1 = p2;
            }

            return p1;
        }

        private static double ChebyshevTSeries1(int n, double x)
        {
            // A series expansion for Chebyshev polynomials near x~1. Good in the n²(x-1)<<1 limit.
            // Handle negative case.
            if (x < 0d)
                return n % 2 == 0 ? ChebyshevTSeries1(n, -x) : -ChebyshevTSeries1(n, -x);
            double xm = x - 1d;

            double f = 1d;
            double df = 1d;
            for (int k = 1; k < IterationLimit; ++k)
            {
                double fOld = f;
                df = df * (n + k - 1) * (n - k + 1) * xm / k / (2 * k - 1);
                f += df;
                if (Math.Abs(f - fOld) < double.Epsilon)
                    return f;
            }

            return double.NaN;
        }
    }
}
