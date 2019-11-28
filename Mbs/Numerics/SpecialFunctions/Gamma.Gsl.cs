using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics
{
    // zReSharper disable CompareOfFloatsByEqualityOperator
    // zReSharper disable MemberHidesStaticFromOuterClass

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
            /// The natural logarithm of the gamma function,
            /// <para><c>lnΓ(x) = ln∫̥˚˚ tˣ⁻¹e⁻ᵗdt</c>.</para>,
            /// subject to <c>x</c> not being a negative integer or zero.
            /// For <c>0>x</c> the real part of <c>lnΓ(x)</c> is returned,
            /// which is equivalent to <c>ln|Γ(x)|</c>.
            /// The function is computed using the real Lanczos method.
            /// <para>Because <c>Γ(x)</c> grows rapidly for increasing positive <c>x</c>, it is often necessary to
            /// work with its logarithm in order to avoid overflow. This function returns accurate
            /// values of <c>lnΓ(x)</c> even for values of x which would cause <c>Γ(x)</c> to overflow.</para>
            /// </summary>
            /// <param name="x">The argument, which must be positive.</param>
            /// <returns>The natural logarithm of the gamma function <c>lnΓ(x)</c>.</returns>
            /// <seealso href="http://mathworld.wolfram.com/LogGammaFunction.html" />
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double LnGamma(double x)
            {
                double value = x - 1;
                double abs = Math.Abs(value);
                if (abs < 0.01)
                    return LnGamma1Pade(value);
                value -= 1;
                abs = Math.Abs(value);
                if (abs < 0.01)
                    return LnGamma2Pade(value);
                if (x >= 0.5)
                    return LnGammaLanczos(x);
                if (Math.Abs(x) < double.Epsilon)
                    return double.NaN;
                if (Math.Abs(x) < 0.02)
                    return LnGammaSgn0(x);
                if (x > -0.5 / (Constants.DoubleEpsilon * Constants.Pi))
                {
                    // Try to extract a fractional part from x.
                    double z = 1d - x;
                    value = Math.Sin(Constants.Pi * z);
                    abs = Math.Abs(value);
                    if (Math.Abs(value) < double.Epsilon)
                        return double.NaN;
                    if (abs < Constants.Pi * 0.015)
                    {
                        // x is near a negative integer, -n.
                        if (x < int.MinValue + 2d)
                            return 0;
                        int n = -(int)(x - 0.5);

                        return LnGammaSgnSing(n, x + n, out _);
                    }

                    value = LnGammaLanczos(z);
                    return Constants.LnPi - (Math.Log(abs) + value);
                }

                // |x| was too large to extract any fractional part.
                return double.NaN;
            }

            /// <summary>
            /// The natural logarithm of the complex gamma function, <c>lnΓ(x)</c>.
            /// </summary>
            /// <param name="z">The complex argument, which must have a non-negative real part.</param>
            /// <returns>The complex value <c>lnΓ(x)</c>.</returns>
            /// <seealso href="http://mathworld.wolfram.com/LogGammaFunction.html" />
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static Complex LnGamma(Complex z)
            {
                double zr = z.Real, zi = z.Imag;
                if (zr <= 0.5)
                {
                    // Transform to right half plane using reflection;
                    // in fact we do a little better by stopping at 1/2.
                    double x = 1d - zr, y = -zi;
                    double lczr = ComplexLnGammaLanczos(x, y, out var lczi);
                    double lnsinr = ComplexLogSin(Constants.Pi * zr, Constants.Pi * zi, out double lnsini);
                    zr = Constants.LnPi - lnsinr - lczr;
                    zi = -lnsini - lczi;
                    zi = RestrictAngle(zi);
                }
                else
                {
                    // Otherwise plain vanilla Lanczos.
                    zr = ComplexLnGammaLanczos(zr, zi, out double imag);
                    zi = imag;
                }

                return new Complex(zr, zi);
            }

            /// <summary>
            /// Computes the sign of the gamma function and the natural logarithm of its magnitude,
            /// subject to <c>x</c> not being a negative integer or zero.
            /// The function is computed using the real Lanczos method.
            /// The value of the gamma function can be reconstructed using the relation
            /// <c>Γ(x) = sign * exp(result)</c>.
            /// <para>Because <c>Γ(x)</c> grows rapidly for increasing positive <c>x</c>, it is often necessary to
            /// work with its logarithm in order to avoid overflow. This function returns accurate
            /// values of <c>lnΓ(x)</c> even for values of x which would cause <c>Γ(x)</c> to overflow.</para>
            /// </summary>
            /// <param name="x">The argument, which must be positive.</param>
            /// <param name="sign">The sign of the gamma function.</param>
            /// <returns>The natural logarithm of the gamma function <c>lnΓ(x)</c>.</returns>
            /// <seealso href="http://mathworld.wolfram.com/LogGammaFunction.html" />
            public static double LnGammaSign(double x, out double sign)
            {
                if (Math.Abs(x - 1d) < 0.01)
                {
                    sign = 1.0;
                    return LnGamma1Pade(x - 1d);
                }

                if (Math.Abs(x - 2d) < 0.01)
                {
                    sign = 1d;
                    return LnGamma2Pade(x - 2d);
                }

                if (x >= 0.5)
                {
                    sign = 1d;
                    return LnGammaLanczos(x);
                }

                if (Math.Abs(x) < double.Epsilon)
                {
                    sign = 0d;
                    return double.NaN;
                }

                if (Math.Abs(x) < 0.02)
                {
                    sign = Math.Sign(x);
                    return LnGammaSgn0(x);
                }

                if (x > -0.5 / (Constants.DoubleEpsilon * Constants.Pi))
                {
                    // Try to extract a fractional part from x.
                    double z = 1d - x;
                    double s = Math.Sin(Constants.Pi * x);
                    double absS = Math.Abs(s);
                    if (Math.Abs(s) < double.Epsilon)
                    {
                        sign = 0d;
                        return double.NaN;
                    }

                    if (absS < Constants.Pi * 0.015)
                    {
                        // x is near a negative integer, -n.
                        if (x < int.MinValue + 2d)
                        {
                            sign = 0d;
                            return 0d;
                        }

                        int n = -(int)(x - 0.5);
                        double eps = x + n;
                        return LnGammaSgnSing(n, eps, out sign);
                    }

                    {
                        sign = s > 0d ? 1d : -1d;
                        return Constants.LnPi - (Math.Log(absS) + LnGammaLanczos(z));
                    }
                }

                // The |x| was too large to extract any fractional part.
                sign = 0d;
                return 0d;
            }

            /// <summary>
            /// A double value indicating the largest argument for the gamma function that will not result in an overflow.
            /// </summary>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public const double MaxGammaArgument = 171.6243769563027257;

            /// <summary>
            /// The gamma function,
            /// <para><c>Γ(x) = ∫̥˚˚ tˣ⁻¹e⁻ᵗdt</c>.</para>,
            /// subject to <c>x</c> not being a negative integer or zero.
            /// The function is computed using the real Lanczos method.
            /// <para>The gamma function is a generalization of the factorial to arbitrary real values.</para>
            /// <para>For positive integer arguments, this integral evaluates to <c>Γ(n+1) = n!</c>, but it can also be evaluated for non-integer z.</para>
            /// <para>Because <c>Γ(x)</c> grows beyond the largest value that can be represented by a <see cref="double" /> at quite
            /// moderate values of <c>x</c>, it may be useful to work with the <c>ln Γ(x)</c>.</para>
            /// </summary>
            /// <param name="x">The argument.</param>
            /// <returns>The value of <c>Γ(x)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Gamma_function" />
            /// <seealso href="http://mathworld.wolfram.com/GammaFunction.html" />
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double Gamma(double x)
            {
                if (x < 0.5)
                {
                    var rintx = (int)Math.Floor(x + 0.5);
                    double fx = x - rintx;
                    double signGamma = ElementaryFunctions.IsEven(rintx) ? 1d : -1d;
                    double sinTerm = signGamma * Math.Sin(Constants.Pi * fx) / Constants.Pi;
                    if (Math.Abs(sinTerm) < double.Epsilon)
                        return double.NaN;
                    if (x > -169d)
                    {
                        double gValue = GammaXgtHalf(1d - x);
                        if (Math.Abs(sinTerm) * gValue * Constants.DoubleMin < 1d)
                            return 1d / (sinTerm * gValue);
                        return 0d;
                    }

                    // It is hard to control it here. We can only exponentiate the
                    // logarithm and eat the loss of precision.
                    double value = LnGammaSign(x, out double sign);
                    return value * sign;
                }

                return GammaXgtHalf(x);
            }

            /// <summary>
            /// The complex gamma function, <c>Γ(z)</c>.
            /// </summary>
            /// <param name="z">The complex argument.</param>
            /// <returns>The complex value of <c>Γ(z)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Gamma_function" />
            /// <seealso href="http://mathworld.wolfram.com/GammaFunction.html" />
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static Complex Gamma(Complex z)
            {
                return z;
            }

            /// <summary>
            /// Computes the reciprocal of the gamma function, 1/Gamma(x)
            /// using the real Lanczos method.
            /// </summary>
            /// <param name="x">The positive real argument.</param>
            /// <returns>The calculated value.</returns>
            public static double GammaInverted(double x)
            {
                if (x <= 0d && Math.Abs(x - Math.Floor(x)) < double.Epsilon)
                {
                    // Negative integer.
                    return 0d;
                }

                if (x < 0.5)
                {
                    double value = LnGammaSign(x, out double sign);
                    if (double.IsNaN(value))
                        return 0d;
                    return Math.Exp(value) * sign;
                }

                {
                    double value = GammaXgtHalf(x);
                    if (double.IsPositiveInfinity(value))
                        return 0d;
                    return 1d / value;
                }
            }

            /// <summary>
            /// The regulated gamma function (gamma star) for positive <c>x</c>.
            /// <para><c>Gamma*(x) = Gamma(x)/(sqrt{2/pi} x^(x-1/2) exp(-x))</c></para>
            /// <para><c>Gamma*(x) = (1 + (1/12x) + ...) for x->infinity</c></para>
            /// </summary>
            /// <param name="x">The positive real argument.</param>
            /// <returns>The calculated value.</returns>
            public static double GammaStar(double x)
            {
                if (x <= 0d)
                    return double.NaN;
                if (x < 0.5)
                {
                    const double c = 0.5 * (Constants.Ln2 + Constants.LnPi);
                    double value = LnGamma(x);
                    double lx = Math.Log(x);
                    value -= (x - 0.5) * lx + x - c;
                    return Math.Exp(value);
                }

                if (x < 2d)
                {
                    double t = 4d / 3d * (x - 0.5) - 1d;
                    return GStarAChebyshevSeries.Evaluate(t);
                }

                if (x < 10d)
                {
                    double t = 0.25 * (x - 2d) - 1d;
                    double value = GStarBChebyshevSeries.Evaluate(t);
                    return value / (x * x) + 1d + 1d / (12d * x);
                }

                if (x < 1d / Constants.FourthRootDoubleEpsilon)
                    return GammaStarSeries(x);
                if (x < 1d / Constants.DoubleEpsilon)
                {
                    // Use Stirling formula for Gamma(x).
                    x = 1d / x;
                    return 1d + x / 12d * (1d + x / 24d * (1d - x * (139d / 180d + 571d / 8640d * x)));
                }

                return 1;
            }

            /// <summary>
            /// The psi function, <c>ψ(x)</c>, also called the digamma function, is the logarithmic derivative of the gamma function:
            /// <para><c>ψ(x) = d/dx ln Γ(x) = Γʹ(x)/Γ(x)</c>.</para>
            /// </summary>
            /// <param name="x">The real argument, x ≠ {0.0, -1.0, -2.0, ...}.</param>
            /// <returns>The value of <c>ψ(x)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Digamma_function" />
            /// <seealso href="http://mathworld.wolfram.com/DigammaFunction.html" />
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double Digamma(double x)
            {
                if (Math.Abs(x) < double.Epsilon || Math.Abs(x + 1d) < double.Epsilon || Math.Abs(x + 2d) < double.Epsilon)
                    return double.NaN;
                double y = Math.Abs(x);
                if (y >= 2d)
                {
                    double t = 8d / (y * y) - 1d;
                    double value = ApsiChebyshevSeries.Evaluate(t);
                    if (x < 0d)
                    {
                        double c = Constants.Pi * x;
                        double s = Math.Sin(c);
                        if (Math.Abs(s) < 2 * Constants.SqrtDoubleMin)
                            return double.NaN;
                        c = Math.Cos(c);
                        return Math.Log(y) - 0.5 / x + value - Constants.Pi * c / s;
                    }

                    value += Math.Log(y) - 0.5 / x;
                    return value;
                }

                // -2 < x < 2
                if (x < -1d)
                {
                    // x = -2 + v
                    double v = x + 2d;
                    double value = PsiChebyshevSeries.Evaluate(2d * v - 1d);
                    double t1 = 1d / x;
                    double t2 = 1d / (x + 1d);
                    double t3 = 1d / v;
                    return value - (t1 + t2 + t3);
                }

                if (x < 0d)
                {
                    // x = -1 + v
                    double v = x + 1d;
                    double value = PsiChebyshevSeries.Evaluate(2d * v - 1d);
                    double t1 = 1d / x;
                    double t2 = 1d / v;
                    return value - (t1 + t2);
                }

                if (x < 1d)
                {
                    // x = v
                    double value = PsiChebyshevSeries.Evaluate(2d * x - 1d);
                    return value - 1d / x;
                }

                {
                    // x = 1 + v
                    double v = x - 1d;
                    return PsiChebyshevSeries.Evaluate(2d * v - 1d);
                }
            }

            /// <summary>
            /// Evaluates the digamma function for a positive integer argument.
            /// <para />
            /// The psi function, <c>ψ(x)</c>, also called the digamma function, is the logarithmic derivative of the gamma function:
            /// <para />
            /// ψ(x) = d/dx ln Γ(x) = Γʹ(x)/Γ(x).
            /// </summary>
            /// <param name="n">An integer argument, n > 0.</param>
            /// <returns>The value of <c>ψ(x)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Digamma_function" />
            /// <seealso href="http://mathworld.wolfram.com/DigammaFunction.html" />
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double Digamma(int n)
            {
                if (n <= 0)
                    return double.NaN;
                double value;
                if (n <= PsiTableNMax)
                {
                    value = PsiTable[n];
                }
                else
                {
                    // Abramowitz+Stegun 6.3.18.
                    const double c2 = -1d / 12;
                    const double c3 = 1d / 120;
                    const double c4 = -1d / 252;
                    const double c5 = 1d / 240;
                    double ni2 = (1d / n) * (1d / n);
                    double ser = ni2 * (c2 + ni2 * (c3 + ni2 * (c4 + ni2 * c5)));
                    value = Math.Log(n);
                    value += -0.5 / n + ser;
                }

                return value;
            }

            /// <summary>
            /// The complex psi function, <c>ψ(z)</c>, also called the digamma function, is the logarithmic derivative of the gamma function:
            /// <para><c>ψ(z) = d/dz ln Γ(z) = Γʹ(z)/Γ(z)</c>.</para>
            /// </summary>
            /// <param name="z">The complex argument.</param>
            /// <returns>The value of <c>ψ(z)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Digamma_function" />
            /// <seealso href="http://mathworld.wolfram.com/DigammaFunction.html" />
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static Complex Digamma(Complex z)
            {
                double zr = z.Real, zi = z.Imag;
                double real, imag;
                if (zr >= 0d)
                {
                    real = ComplexPsiRightHalfPlane(zr, zi, out imag);
                }
                else
                {
                    // Reflection formula [Abramowitz+Stegun, 6.3.7].
                    real = ComplexPsiRightHalfPlane(1d - zr, -zi, out imag);
                    double cotr = ComplexCot(zr * Constants.Pi, zi * Constants.Pi, out var coti);
                    if (!double.IsNaN(cotr) && !double.IsInfinity(cotr) &&
                        !double.IsNaN(coti) && !double.IsInfinity(coti))
                    {
                        real -= cotr * Constants.Pi;
                        imag -= coti * Constants.Pi;
                    }
                }

                return new Complex(real, imag);
            }

            /// <summary>
            /// The trigamma function, <c>ψ₁(x)</c> (the second of the polygamma functions),
            /// is the second logarithmic derivative of the gamma function:
            /// <para><c>ψ₁(x) = d²/dx² ln Γ(x)</c>.</para>
            /// It follows from this definition that
            /// <para><c>ψ₁(x) = d/dx ψ(x)</c></para>
            /// where <c>ψ(x)</c> is the digamma function.
            /// </summary>
            /// <param name="x">The real argument, x ≠ {0.0, -1.0, -2.0, ...}.</param>
            /// <returns>The value of <c>ψ₁(x)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Trigamma_function" />
            /// <seealso href="http://mathworld.wolfram.com/TrigammaFunction.html" />
            public static double Trigamma(double x)
            {
                if (Math.Abs(x) < double.Epsilon || Math.Abs(x + 1d) < double.Epsilon || Math.Abs(x + 2d) < double.Epsilon)
                    return double.NaN;
                if (x > 0d)
                    return PsiNxg0(1, x);
                if (x > -5d)
                {
                    // Abramowitz + Stegun 6.4.6.
                    var mm = (int)-Math.Floor(x);
                    double fx = x + mm;
                    if (Math.Abs(fx) < double.Epsilon)
                        return double.NaN;
                    double sum = 0d;
                    for (int m = 0; m < mm; ++m)
                    {
                        double xm = x + m;
                        sum += 1.0 / (xm * xm);
                    }

                    sum += PsiNxg0(1, fx);
                    return sum;
                }

                // Abramowitz + Stegun 6.4.7.
                double sinPiX = Math.Sin(Constants.Pi * x);
                double d = Constants.PiSquared / (sinPiX * sinPiX);
                return d - PsiNxg0(1, 1d - x);
            }

            /// <summary>
            /// Evaluates the trigamma function for a positive integer argument.
            /// <para><c>ψ₁(x) = d²/dx² ln Γ(x)</c>.</para>
            /// It follows from this definition that
            /// <para><c>ψ₁(x) = d/dx ψ(x)</c></para>
            /// where <c>ψ(x)</c> is the digamma function.
            /// </summary>
            /// <param name="n">An integer argument, n > 0.</param>
            /// <returns>The value of <c>ψ₁(x)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Trigamma_function" />
            /// <seealso href="http://mathworld.wolfram.com/TrigammaFunction.html" />
            public static double Trigamma(int n)
            {
                if (n <= 0)
                    return double.NaN;
                double value;
                if (n <= Psi1TableNMax)
                {
                    value = Psi1Table[n];
                }
                else
                {
                    // Abramowitz+Stegun 6.4.12. Double-precision for n > 100.
                    const double c0 = -1d / 30;
                    const double c1 = 1d / 42;
                    const double c2 = -1d / 30;
                    double ni2 = 1d / n;
                    ni2 *= ni2;
                    double ser = ni2 * ni2 * (c0 + ni2 * (c1 + c2 * ni2));
                    value = (1d + 0.5 / n + 1d / (6d * n * n) + ser) / n;
                }

                return value;
            }

            /// <summary>
            /// The polygamma function, <c>ψ⁽ⁿ⁾(x)</c>, which gives higher logarithmic derivatives of the gamma function:
            /// <para><c>ψ⁽ⁿ⁾(x) = dⁿ/dxⁿ ψ(x) = dⁿ/dxⁿ lnΓ(x)</c></para>
            /// </summary>
            /// <param name="n">The order, which must be non-negative.</param>
            /// <param name="x">The argument, which must be positive.</param>
            /// <returns>The value of <c>ψ⁽ⁿ⁾(x)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Polygamma_function"/>
            /// <seealso href="http://mathworld.wolfram.com/PolygammaFunction.html"/>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double Polygamma(int n, double x)
            {
                if (n == 0)
                    return Digamma(x);
                if (n == 1)
                    return Trigamma(x);
                if (n < 0 || x <= 0d)
                    return double.NaN;
                double value = HurwitzZeta(n + 1d, x) * LnFactorial(n);
                if (ElementaryFunctions.IsEven(n))
                    value = -value;
                return value;
            }

            /// <summary>
            /// The normalized (regularized) lower (left) incomplete gamma function,
            /// <para><c>P(a,x) = γ(a,x)/Γ(x), γ(a,x) = ∫̽˳ tᵃ⁻¹e⁻ᵗdt</c>.</para>
            /// <para>The lower incomplete gamma function is obtained by carrying out the gamma function integration from zero to some
            /// finite value <c>x</c>, instead of to infinity. The function is normalized by dividing by the complete integral, so the
            /// function ranges from 0 to 1 as <c>x</c> ranges from 0 to infinity.</para>
            /// <para>This function changes rapidly from 0 to 1 around the point <c>x=a</c>. For large values of <c>x</c>, this function becomes 1 within floating point precision. To determine its deviation from 1
            /// in this region, use the complementary function <c>Q(a,x) = 1 - P(a,x)</c> (<see cref="RegularizedGammaQ"/>).</para>
            /// <para>For <c>a=ν/2</c> and <c>x=χ²/2</c>, this function is the CDF of the <c>χ²</c> distribution with <c>ν</c> degrees of freedom.</para>
            /// </summary>
            /// <param name="a">The shape parameter, which must be positive.</param>
            /// <param name="x">The argument, which must be non-negative.</param>
            /// <returns>The value of <c>γ(a,x)/Γ(x)</c>.</returns>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double RegularizedGammaP(double a, double x)
            {
                return x;
            }

            /// <summary>
            /// The normalized (regularized) upper (right) incomplete gamma function,
            /// <para><c>Q(a,x) = Γ(a,x)/Γ(x), Γ(a,x) = ∫̊ₓ̊ tᵃ⁻¹e⁻ᵗdt</c>.</para>
            /// <para>The upper incomplete gamma function is obtained by carrying out the gamma function integration from a finite value <c>x</c>
            /// to infinity. The function is normalized by dividing by the complete integral, so the
            /// function ranges from 1 to 0 as <c>x</c> ranges from 0 to infinity.</para>
            /// <para>This function changes rapidly from 1 to 0 around the point <c>x=a</c>.</para>
            /// <para>This function is the complement of the lower incomplete gamma function <c>P(a,x) = 1 - Q(a,x)</c> (<see cref="RegularizedGammaP"/>).</para>
            /// </summary>
            /// <param name="a">The shape parameter, which must be positive.</param>
            /// <param name="x">The argument, which must be non-negative.</param>
            /// <returns>The value of <c>Γ(a,x)/Γ(x)</c>.</returns>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double RegularizedGammaQ(double a, double x)
            {
                return x;
            }

            /// <summary>
            /// The full (non-normalized / non-regularized) lower incomplete Gamma function,
            /// <para><c>γ(a,x) = ∫̽˳ tᵃ⁻¹e⁻ᵗdt</c>.</para>
            /// <para>The lower incomplete gamma function is obtained by carrying out the gamma function integration from zero to some
            /// finite value <c>x</c>, instead of to infinity. Like the gamma function itself, this function gets large very quickly. For most
            /// purposes, you will prefer to use the regularized incomplete gamma functions <c>Q(a,x) = Γ(a,x)/Γ(x)</c> (<see cref="RegularizedGammaQ"/>)
            /// and <c>P(a,x) = γ(a,x)/Γ(x)</c> (<see cref="RegularizedGammaP"/>).</para>
            /// </summary>
            /// <param name="a">The shape parameter, which must be positive.</param>
            /// <param name="x">The argument, which must be non-negative.</param>
            /// <returns>The value of <c>γ(a,x)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Incomplete_Gamma_function"/>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double IncompleteGammaLower(double a, double x)
            {
                return RegularizedGammaP(a, x) * Gamma(a);
            }

            /// <summary>
            /// The full (non-normalized / non-regularized) upper incomplete Gamma function,
            /// <para><c>Γ(a,x) = ∫̊ₓ̊ tᵃ⁻¹e⁻ᵗdt</c>.</para>
            /// <para>The upper incomplete gamma function is obtained by carrying out the gamma function integration from finite value <c>x</c>
            /// to infinity. Like the gamma function itself, this function gets large very quickly. For most
            /// purposes, you will prefer to use the regularized incomplete gamma functions <c>Q(a,x) = Γ(a,x)/Γ(x)</c> (<see cref="RegularizedGammaQ"/>)
            /// and <c>P(a,x) = γ(a,x)/Γ(x)</c> (<see cref="RegularizedGammaP"/>).</para>
            /// </summary>
            /// <param name="a">The shape parameter, which must be positive.</param>
            /// <param name="x">The argument, which must be non-negative.</param>
            /// <returns>The value of <c>Γ(a,x)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Incomplete_Gamma_function"/>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double IncompleteGammaUpper(double a, double x)
            {
                return x;
            }

            /// <summary>
            /// Evaluates the incomplete Gamma function between two arguments.
            /// </summary>
            /// <param name="a">The shape parameter, which must be positive.</param>
            /// <param name="x1">The first argument, which must be non-negative.</param>
            /// <param name="x2">The second argument, which must be non-negative.</param>
            /// <returns>The value of <c>γ(a,x1) - γ(a,x2)</c>.</returns>
            /// <seealso href="http://en.wikipedia.org/wiki/Incomplete_Gamma_function"/>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double IncompleteGamma(double a, double x1, double x2)
            {
                return Gamma(a) * (RegularizedGammaQ(a, x1) - RegularizedGammaQ(a, x2));
            }

            /// <summary>
            /// The inverse of the regularized Gamma function P(a, x).
            /// </summary>
            /// <param name="a">The parameter of the Gamma function.</param>
            /// <param name="p">The value of the regularized Gamma function.</param>
            /// <returns>The value <c>x</c> for which <c>P(a,x)</c> equals <c>p</c>.</returns>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double InverseRegularizedGammaP(double a, double p)
            {
                return InverseRegularizedGamma(a, p, 1d - p);
            }

            /// <summary>
            /// The inverse of the regularized Gamma function Q(a, x).
            /// </summary>
            /// <param name="a">The parameter of the Gamma function.</param>
            /// <param name="q">The value of the regularized Gamma function.</param>
            /// <returns>The value <c>x</c> for which <c>Q(a,x)</c> equals <c>p</c>.</returns>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static double InverseRegularizedGammaQ(double a, double q)
            {
                return InverseRegularizedGamma(a, 1d - q, q);
            }

            private const double LogRootTwoPi = 0.9189385332046727418;

            /// <summary>
            /// Lanczos method: coefficients for gamma=7, kmax=8.
            /// </summary>
            private static readonly double[] Lanczos7Coefficients =
            {
                0.99999999999980993227684700473478,
                676.520368121885098567009190444019,
                -1259.13921672240287047156078755283,
                771.3234287776530788486528258894,
                -176.61502916214059906584551354,
                12.507343278686904814458936853,
                -0.13857109526572011689554707,
                9.984369578019570859563e-6,
                1.50563273514931155834e-7
            };

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double LnGammaLanczos(double x)
            {
                // Lanczos method for real x > 0; gamma=7, truncated at 1/(z+8).
                // [J. SIAM Numer. Anal, Ser. B, 1 (1964) 86]
                x -= 1d; // Lanczos writes z! instead of Gamma(z).

                double ag = Lanczos7Coefficients[0];
                for (int k = 1; k < 9; ++k)
                    ag += Lanczos7Coefficients[k] / (x + k);

                // (x+0.5)*Math.Log(x+7.5) - (x+7.5) + LogRootTwoPi + Math.Log(Ag(x))
                double term1 = (x + 0.5) * Math.Log((x + 7.5) / Constants.E);
                double term2 = LogRootTwoPi + Math.Log(ag);
                return term1 + (term2 - 7d);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double LnGammaSgn0(double eps)
            {
                // Gives double-precision for |eps| < 0.02.
                // Calculate series for g(eps) = Gamma(eps) eps - 1/(1+eps) - eps/2.
                const double c1 = -0.07721566490153286061;
                const double c2 = -0.01094400467202744461;
                const double c3 = 0.09252092391911371098;
                const double c4 = -0.01827191316559981266;
                const double c5 = 0.01800493109685479790;
                const double c6 = -0.00685088537872380685;
                const double c7 = 0.00399823955756846603;
                const double c8 = -0.00189430621687107802;
                const double c9 = 0.00097473237804513221;
                const double c10 = -0.00048434392722255893;
                double g6 = c6 + eps * (c7 + eps * (c8 + eps * (c9 + eps * c10)));
                double g = eps * (c1 + eps * (c2 + eps * (c3 + eps * (c4 + eps * (c5 + eps * g6)))));

                // Calculate Gamma(eps) eps, a positive quantity.
                double gee = g + 1d / (1d + eps) + 0.5 * eps;

                // sgn = Math.Sign(eps);
                return Math.Log(gee / Math.Abs(eps));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double LnGammaSgnSing(int n, double eps, out double sgn)
            {
                // The argument is near a negative integer, x = -n + eps, assuming n >= 1.
                // Calculates sign as well as ln(|gamma(x)|).
                if (Math.Abs(eps) < double.Epsilon)
                {
                    sgn = 0;
                    return 0;
                }

                double value;
                if (n == 1)
                {
                    // Calculate series for g = eps gamma(-1+eps) + 1 + eps/2 (1+3eps)/(1-eps^2).
                    // Double-precision for |eps| < 0.02.
                    const double c0 = 0.07721566490153286061;
                    const double c1 = 0.08815966957356030521;
                    const double c2 = -0.00436125434555340577;
                    const double c3 = 0.01391065882004640689;
                    const double c4 = -0.00409427227680839100;
                    const double c5 = 0.00275661310191541584;
                    const double c6 = -0.00124162645565305019;
                    const double c7 = 0.00065267976121802783;
                    const double c8 = -0.00032205261682710437;
                    const double c9 = 0.00016229131039545456;
                    double g5 = c5 + eps * (c6 + eps * (c7 + eps * (c8 + eps * c9)));
                    double g = eps * (c0 + eps * (c1 + eps * (c2 + eps * (c3 + eps * (c4 + eps * g5)))));

                    // Calculate eps gamma(-1+eps), a negative quantity.
                    double gee = g - 1d - 0.5 * eps * (1d + 3d * eps) / (1d - eps * eps);

                    value = Math.Log(Math.Abs(gee) / Math.Abs(eps));
                    sgn = eps > 0d ? -1d : 1d;
                }
                else
                {
                    // Series for sin(pi(n+1-eps))/(pi eps) modulo the sign.
                    // Double-precision for |eps| < 0.02.
                    const double cs1 = -1.6449340668482264365;
                    const double cs2 = 0.8117424252833536436;
                    const double cs3 = -0.1907518241220842137;
                    const double cs4 = 0.0261478478176548005;
                    const double cs5 = -0.0023460810354558236;
                    double e2 = eps * eps;
                    double sinSeries = 1d + e2 * (cs1 + e2 * (cs2 + e2 * (cs3 + e2 * (cs4 + e2 * cs5))));

                    // Calculate series for ln(gamma(1+n-eps)).
                    // Double-precision for |eps| < 0.02.
                    double absEps = Math.Abs(eps);

                    double psi2Value = 0d, psi3Value = 0d, psi4Value = 0d, psi5Value = 0d, psi6Value = 0d;
                    double c0 = LnFactorial(n);
                    int n1 = n + 1;
                    double psi0Value = Digamma(n1);
                    double psi1Value = Trigamma(n1) / 2d;
                    if (absEps > 0.00001)
                        psi2Value = Polygamma(2, n1) / 6d;
                    if (absEps > 0.0002)
                        psi3Value = Polygamma(3, n1) / 24d;
                    if (absEps > 0.001)
                        psi4Value = Polygamma(4, n1) / 120d;
                    if (absEps > 0.005)
                        psi5Value = Polygamma(5, n1) / 720d;
                    if (absEps > 0.01)
                        psi6Value = Polygamma(6, n1) / 5040d;
                    double lngSeries = c0 - eps * (psi0Value - eps * (psi1Value - eps * (psi2Value - eps * (psi3Value
                        - eps * (psi4Value - eps * (psi5Value - eps * psi6Value))))));

                    // Calculate
                    // g = ln(|eps gamma(-N+eps)|)
                    //   = -ln(gamma(1+N-eps)) + ln(|eps Pi/System.Math.Sin(Pi(N+1+eps))|)
                    value = -lngSeries - Math.Log(sinSeries);
                    value -= Math.Log(Math.Abs(eps));
                    sgn = (ElementaryFunctions.IsOdd(n) ? -1d : 1d) * (eps > 0d ? 1d : -1d);
                }

                return value;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double LnGamma1Pade(double eps)
            {
                // Use (2,2) Pade for Log(Gamma(1+eps))/eps plus a correction series.
                const double k = 2.0816265188662692474880210318;
                const double n1 = -1.0017419282349508699871138440;
                const double n2 = 1.7364839209922879823280541733;
                const double d1 = 1.2433006018858751556055436011;
                const double d2 = 5.0456274100274010152489597514;

                const double c0 = 0.004785324257581753;
                const double c1 = -0.01192457083645441;
                const double c2 = 0.01931961413960498;
                const double c3 = -0.02594027398725020;
                const double c4 = 0.03141928755021455;

                double pade = k * ((eps + n1) * (eps + n2)) / ((eps + d1) * (eps + d2));

                double eps5 = eps * eps * eps * eps * eps;
                double correction = eps5 * (c0 + eps * (c1 + eps * (c2 + eps * (c3 + c4 * eps))));

                return eps * (pade + correction);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double LnGamma2Pade(double eps)
            {
                // Use (2,2) Pade for Log(Gamma(2+eps))/eps plus a correction series.
                const double k = 2.85337998765781918463568869;
                const double n1 = 1.000895834786669227164446568;
                const double n2 = 4.209376735287755081642901277;
                const double d1 = 2.618851904903217274682578255;
                const double d2 = 10.85766559900983515322922936;

                const double c0 = 0.0001139406357036744;
                const double c1 = -0.0001365435269792533;
                const double c2 = 0.0001067287169183665;
                const double c3 = -0.0000693271800931282;
                const double c4 = 0.0000407220927867950;

                double pade = k * ((eps + n1) * (eps + n2)) / ((eps + d1) * (eps + d2));

                double eps5 = eps * eps * eps * eps * eps;
                double correction = eps5 * (c0 + eps * (c1 + eps * (c2 + eps * (c3 + c4 * eps))));

                return eps * (pade + correction);
            }

            /// <summary>
            /// Chebyshev series.
            /// </summary>
            private static readonly double[] Gamma510Data =
            {
                -1.5285594096661578881275075214,
                4.8259152300595906319768555035,
                0.2277712320977614992970601978,
                -0.0138867665685617873604917300,
                0.0012704876495201082588139723,
                -0.0001393841240254993658962470,
                0.0000169709242992322702260663,
                -2.2108528820210580075775889168e-06,
                3.0196602854202309805163918716e-07,
                -4.2705675000079118380587357358e-08,
                6.2026423818051402794663551945e-09,
                -9.1993973208880910416311405656e-10,
                1.3875551258028145778301211638e-10,
                -2.1218861491906788718519522978e-11,
                3.2821736040381439555133562600e-12,
                -5.1260001009953791220611135264e-13,
                8.0713532554874636696982146610e-14,
                -1.2798522376569209083811628061e-14,
                2.0417711600852502310258808643e-15,
                -3.2745239502992355776882614137e-16,
                5.2759418422036579482120897453e-17,
                -8.5354147151695233960425725513e-18,
                1.3858639703888078291599886143e-18,
                -2.2574398807738626571560124396e-19
            };

            /// <summary>
            /// Chebyshev expansion for Math.Log(gamma(x)/gamma(8)), xϵ(5, 10), tϵ(-1, 1).
            /// </summary>
            private static readonly ChebyshevSeries Gamma510ChebyshevSeries = new ChebyshevSeries(-1, 1, 23, Gamma510Data);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double GammaXgtHalf(double x)
            {
                // Gamma(x) for x >= 1/2.
                if (Math.Abs(x - 0.5) < double.Epsilon)
                    return Constants.SqrtPi;
                if (x <= (FactorialMaxN + 1d) && Math.Abs(x - Math.Floor(x)) < double.Epsilon)
                {
                    var n = (int)Math.Floor(x);
                    return Factorials[n - 1];
                }

                if (Math.Abs(x - 1d) < 0.01)
                {
                    // Use series for Gamma[1+eps] - 1/(1+eps).
                    double eps = x - 1d;
                    const double c1 = 0.4227843350984671394;
                    const double c2 = -0.01094400467202744461;
                    const double c3 = 0.09252092391911371098;
                    const double c4 = -0.018271913165599812664;
                    const double c5 = 0.018004931096854797895;
                    const double c6 = -0.006850885378723806846;
                    const double c7 = 0.003998239557568466030;
                    return 1d / x + eps * (c1 + eps * (c2 + eps * (c3 + eps * (c4 + eps * (c5 + eps * (c6 + eps * c7))))));
                }

                if (Math.Abs(x - 2d) < 0.01)
                {
                    // Use series for Gamma[1 + eps].
                    double eps = x - 2d;
                    const double c1 = 0.4227843350984671394;
                    const double c2 = 0.4118403304264396948;
                    const double c3 = 0.08157691924708626638;
                    const double c4 = 0.07424901075351389832;
                    const double c5 = -0.00026698206874501476832;
                    const double c6 = 0.011154045718130991049;
                    const double c7 = -0.002852645821155340816;
                    const double c8 = 0.0021039333406973880085;
                    return 1d + eps * (c1 + eps * (c2 + eps * (c3 + eps * (c4 + eps * (c5 + eps * (c6 + eps * (c7 + eps * c8)))))));
                }

                if (x < 5d)
                {
                    // Exponentiating the logarithm is fine, as long as the exponential
                    // is not so large that it greatly amplifies the error.
                    return Math.Exp(LnGammaLanczos(x));
                }

                if (x < 10d)
                {
                    // This is a sticky area. The logarithm is too large
                    // and the gammastar series is not good.
                    const double gamma8 = 5040d;
                    double t = (2d * x - 15d) / 5d;
                    double value = Gamma510ChebyshevSeries.Evaluate(t);
                    return Math.Exp(value) * gamma8;
                }

                if (x < MaxGammaArgument)
                {
                    // We do not want to exponentiate the logarithm if x is large because
                    // of the inevitable inflation of the error. So we carefully use
                    // Math.Pow() and Math.Exp() with exact quantities.
                    double p = Math.Pow(x, 0.5 * x);
                    double e = Math.Exp(-x);
                    double q = (p * e) * p;
                    double pre = Constants.Sqrt2 * Constants.SqrtPi * q / Math.Sqrt(x);
                    return pre * GammaStarSeries(x);
                }

                return double.PositiveInfinity;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double GammaStarSeries(double x)
            {
                // Series for gammastar(x), double-precision for x > 10.0.
                // Use the Stirling series for the correction to Log(Gamma(x)), which is better
                // behaved and easier to compute than the regular Stirling series for Gamma(x).
                double y = 1d / (x * x);
                const double c0 = 1d / 12;
                const double c1 = -1d / 360;
                const double c2 = 1d / 1260;
                const double c3 = -1d / 1680;
                const double c4 = 1d / 1188;
                const double c5 = -691d / 360360;
                const double c6 = 1d / 156;
                const double c7 = -3617d / 122400;
                double ser = c0 + y * (c1 + y * (c2 + y * (c3 + y * (c4 + y * (c5 + y * (c6 + y * c7))))));
                return Math.Exp(ser / x);
            }

            /// <summary>
            /// Chebyshev series.
            /// </summary>
            private static readonly double[] GStarAData =
            {
                2.16786447866463034423060819465,
                -0.05533249018745584258035832802,
                0.01800392431460719960888319748,
                -0.00580919269468937714480019814,
                0.00186523689488400339978881560,
                -0.00059746524113955531852595159,
                0.00019125169907783353925426722,
                -0.00006124996546944685735909697,
                0.00001963889633130842586440945,
                -6.3067741254637180272515795142e-06,
                2.0288698405861392526872789863e-06,
                -6.5384896660838465981983750582e-07,
                2.1108698058908865476480734911e-07,
                -6.8260714912274941677892994580e-08,
                2.2108560875880560555583978510e-08,
                -7.1710331930255456643627187187e-09,
                2.3290892983985406754602564745e-09,
                -7.5740371598505586754890405359e-10,
                2.4658267222594334398525312084e-10,
                -8.0362243171659883803428749516e-11,
                2.6215616826341594653521346229e-11,
                -8.5596155025948750540420068109e-12,
                2.7970831499487963614315315444e-12,
                -9.1471771211886202805502562414e-13,
                2.9934720198063397094916415927e-13,
                -9.8026575909753445931073620469e-14,
                3.2116773667767153777571410671e-14,
                -1.0518035333878147029650507254e-14,
                3.4144405720185253938994854173e-15,
                -1.0115153943081187052322643819e-15
            };

            /// <summary>
            /// Chebyshev coefficients for <c>Gamma*(3/4(t+1)+1/2), tϵ(-1, 1)</c>.
            /// </summary>
            private static readonly ChebyshevSeries GStarAChebyshevSeries = new ChebyshevSeries(-1, 1, 29, GStarAData);

            /// <summary>
            /// Chebyshev series.
            /// </summary>
            private static readonly double[] GStarBData =
            {
                0.0057502277273114339831606096782,
                0.0004496689534965685038254147807,
                -0.0001672763153188717308905047405,
                0.0000615137014913154794776670946,
                -0.0000223726551711525016380862195,
                8.0507405356647954540694800545e-06,
                -2.8671077107583395569766746448e-06,
                1.0106727053742747568362254106e-06,
                -3.5265558477595061262310873482e-07,
                1.2179216046419401193247254591e-07,
                -4.1619640180795366971160162267e-08,
                1.4066283500795206892487241294e-08,
                -4.6982570380537099016106141654e-09,
                1.5491248664620612686423108936e-09,
                -5.0340936319394885789686867772e-10,
                1.6084448673736032249959475006e-10,
                -5.0349733196835456497619787559e-11,
                1.5357154939762136997591808461e-11,
                -4.5233809655775649997667176224e-12,
                1.2664429179254447281068538964e-12,
                -3.2648287937449326771785041692e-13,
                7.1528272726086133795579071407e-14,
                -9.4831735252566034505739531258e-15,
                -2.3124001991413207293120906691e-15,
                2.8406613277170391482590129474e-15,
                -1.7245370321618816421281770927e-15,
                8.6507923128671112154695006592e-16,
                -3.9506563665427555895391869919e-16,
                1.6779342132074761078792361165e-16,
                -6.0483153034414765129837716260e-17
            };

            /// <summary>
            /// Chebyshev coefficients for <c>x^2(Gamma*(x) - 1 - 1/(12x)), x = 4(t+1)+2, tϵ(-1, 1)</c>.
            /// </summary>
            private static readonly ChebyshevSeries GStarBChebyshevSeries = new ChebyshevSeries(-1, 1, 29, GStarBData);

            /// <summary>
            /// Chebyshev series.
            /// </summary>
            private static readonly double[] R1PyData =
            {
                1.59888328244976954803168395603,
                0.67905625353213463845115658455,
                -0.068485802980122530009506482524,
                -0.005788184183095866792008831182,
                0.008511258167108615980419855648,
                -0.004042656134699693434334556409,
                0.001352328406159402601778462956,
                -0.000311646563930660566674525382,
                0.000018507563785249135437219139,
                0.000028348705427529850296492146,
                -0.000019487536014574535567541960,
                8.0709788710834469408621587335e-06,
                -2.2983564321340518037060346561e-06,
                3.0506629599604749843855962658e-07,
                1.3042238632418364610774284846e-07,
                -1.2308657181048950589464690208e-07,
                5.7710855710682427240667414345e-08,
                -1.8275559342450963966092636354e-08,
                3.1020471300626589420759518930e-09,
                6.8989327480593812470039430640e-10,
                -8.7182290258923059852334818997e-10,
                4.4069147710243611798213548777e-10,
                -1.4727311099198535963467200277e-10,
                2.7589682523262644748825844248e-11,
                4.1871826756975856411554363568e-12,
                -6.5673460487260087541400767340e-12,
                3.4487900886723214020103638000e-12,
                -1.1807251417448690607973794078e-12,
                2.3798314343969589258709315574e-13,
                2.1663630410818831824259465821e-15
            };

            /// <summary>
            /// Chebyshev fit for the following.
            /// <c>f(y) = Re(Psi(1+Iy)) + EulerGamma - y^2/(1+y^2) - y^2/(2(4+y^2)), xϵ(1, 10)</c>
            /// <c> => </c>
            /// <c>y(x) = (9x + 11)/2, xϵ(-1, 1)</c>
            /// <c>x(y) = (2y - 11)/9</c>
            /// <c>g(x) := f(y(x))</c>
            /// </summary>
            private static readonly ChebyshevSeries R1PyChebyshevSeries = new ChebyshevSeries(-1, 1, 29, R1PyData);

            /// <summary>
            /// Chebyshev series.
            /// </summary>
            private static readonly double[] PsiData =
            {
                -.038057080835217922,
                .491415393029387130,
                -.056815747821244730,
                .008357821225914313,
                -.001333232857994342,
                .000220313287069308,
                -.000037040238178456,
                .000006283793654854,
                -.000001071263908506,
                .000000183128394654,
                -.000000031353509361,
                .000000005372808776,
                -.000000000921168141,
                .000000000157981265,
                -.000000000027098646,
                .000000000004648722,
                -.000000000000797527,
                .000000000000136827,
                -.000000000000023475,
                .000000000000004027,
                -.000000000000000691,
                .000000000000000118,
                -.000000000000000020
            };

            /// <summary>
            /// Chebyshev fits from SLATEC code for psi(x).
            /// Series for PSI on the interval  0.0 to 1.0 with
            /// weighted error 2.03e-17,
            /// log weighted error 16.69,
            /// significant figures required 16.39,
            /// decimal places required 17.37.
            /// </summary>
            private static readonly ChebyshevSeries PsiChebyshevSeries = new ChebyshevSeries(-1, 1, 22, PsiData);

            /// <summary>
            /// Chebyshev series.
            /// </summary>
            private static readonly double[] ApsiData =
            {
                -.0204749044678185,
                -.0101801271534859,
                .0000559718725387,
                -.0000012917176570,
                .0000000572858606,
                -.0000000038213539,
                .0000000003397434,
                -.0000000000374838,
                .0000000000048990,
                -.0000000000007344,
                .0000000000001233,
                -.0000000000000228,
                .0000000000000045,
                -.0000000000000009,
                .0000000000000002,
                -.0000000000000000
            };

            /// <summary>
            /// Chebyshev fits from SLATEC code for psi(x).
            /// Series for APSI on the interval  0.0 to  2.5e-01 with
            /// weighted error 5.54e-17,
            /// log weighted error 16.26,
            /// significant figures required 14.42,
            /// decimal places required 16.86.
            /// </summary>
            private static readonly ChebyshevSeries ApsiChebyshevSeries = new ChebyshevSeries(-1, 1, 15, ApsiData);

#pragma warning disable SA1203 // Constants must appear before fields
            private const int PsiTableNMax = 100;
#pragma warning restore SA1203
            private static readonly double[] PsiTable =
            {
                0.0,                             // psi(0)
                -Constants.EulerGamma,           // psi(1)
                0.42278433509846713939348790992, // ...
                0.92278433509846713939348790992,
                1.25611766843180047272682124325,
                1.50611766843180047272682124325,
                1.70611766843180047272682124325,
                1.87278433509846713939348790992,
                2.01564147795560999653634505277,
                2.14064147795560999653634505277,
                2.25175258906672110764745616389,
                2.35175258906672110764745616389,
                2.44266167997581201673836525479,
                2.52599501330914535007169858813,
                2.60291809023222227314862166505,
                2.67434666166079370172005023648,
                2.74101332832746036838671690315,
                2.80351332832746036838671690315,
                2.86233685773922507426906984432,
                2.91789241329478062982462539988,
                2.97052399224214905087725697883,
                3.02052399224214905087725697883,
                3.06814303986119666992487602645,
                3.11359758531574212447033057190,
                3.15707584618530734186163491973,
                3.1987425128519740085283015864,
                3.2387425128519740085283015864,
                3.2772040513135124700667631249,
                3.3142410883505495071038001619,
                3.3499553740648352213895144476,
                3.3844381326855248765619282407,
                3.4177714660188582098952615740,
                3.4500295305349872421533260902,
                3.4812795305349872421533260902,
                3.5115825608380175451836291205,
                3.5409943255438998981248055911,
                3.5695657541153284695533770196,
                3.5973435318931062473311547974,
                3.6243705589201332743581818244,
                3.6506863483938174848844976139,
                3.6763273740348431259101386396,
                3.7013273740348431259101386396,
                3.7257176179372821503003825420,
                3.7495271417468059598241920658,
                3.7727829557002943319172153216,
                3.7955102284275670591899425943,
                3.8177324506497892814121648166,
                3.8394715810845718901078169905,
                3.8607481768292527411716467777,
                3.8815815101625860745049801110,
                3.9019896734278921969539597029,
                3.9219896734278921969539597029,
                3.9415975165651470989147440166,
                3.9608282857959163296839747858,
                3.9796962103242182164764276160,
                3.9982147288427367349949461345,
                4.0163965470245549168131279527,
                4.0342536898816977739559850956,
                4.0517975495308205809735289552,
                4.0690389288411654085597358518,
                4.0859880813835382899156680552,
                4.1026547480502049565823347218,
                4.1190481906731557762544658694,
                4.1351772229312202923834981274,
                4.1510502388042361653993711433,
                4.1666752388042361653993711433,
                4.1820598541888515500147557587,
                4.1972113693403667015299072739,
                4.2121367424746950597388624977,
                4.2268426248276362362094507330,
                4.2413353784508246420065521823,
                4.2556210927365389277208378966,
                4.2697055997787924488475984600,
                4.2835944886676813377364873489,
                4.2972931188046676391063503626,
                4.3108066323181811526198638761,
                4.3241399656515144859531972094,
                4.3372978603883565912163551041,
                4.3502848733753695782293421171,
                4.3631053861958823987421626300,
                4.3757636140439836645649474401,
                4.3882636140439836645649474401,
                4.4006092930563293435772931191,
                4.4128044150075488557724150703,
                4.4248526077786331931218126607,
                4.4367573696833950978837174226,
                4.4485220755657480390601880108,
                4.4601499825424922251066996387,
                4.4716442354160554434975042364,
                4.4830078717796918071338678728,
                4.4942438268358715824147667492,
                4.5053549379469826935258778603,
                4.5163439489359936825368668713,
                4.5272135141533849868846929582,
                4.5379662023254279976373811303,
                4.5486045001977684231692960239,
                4.5591308159872421073798223397,
                4.5695474826539087740464890064,
                4.5798567610044242379640147796, // ...
                4.5900608426370772991885045755, // psi(1,99)
                4.6001618527380874001986055856 // psi(1,100)
            };

#pragma warning disable SA1203 // Constants must appear before fields
            private const int Psi1TableNMax = 100;
#pragma warning restore SA1203
            private static readonly double[] Psi1Table =
            {
                0.0,                             // psi(1,0)
                Constants.Pi * Constants.Pi / 6, // psi(1,1)
                0.644934066848226436472415,      // ...
                0.394934066848226436472415,
                0.2838229557371153253613041,
                0.2213229557371153253613041,
                0.1813229557371153253613041,
                0.1535451779593375475835263,
                0.1331370146940314251345467,
                0.1175120146940314251345467,
                0.1051663356816857461222010,
                0.0951663356816857461222010,
                0.0869018728717683907503002,
                0.0799574284273239463058557,
                0.0740402686640103368384001,
                0.0689382278476838062261552,
                0.0644937834032393617817108,
                0.0605875334032393617817108,
                0.0571273257907826143768665,
                0.0540409060376961946237801,
                0.0512708229352031198315363,
                0.0487708229352031198315363,
                0.0465032492390579951149830,
                0.0444371335365786562720078,
                0.0425467743683366902984728,
                0.0408106632572255791873617,
                0.0392106632572255791873617,
                0.0377313733163971768204978,
                0.0363596312039143235969038,
                0.0350841209998326909438426,
                0.0338950603577399442137594,
                0.0327839492466288331026483,
                0.0317433665203020901265817,
                0.03076680402030209012658168,
                0.02984853037475571730748159,
                0.02898347847164153045627052,
                0.02816715194102928555831133,
                0.02739554700275768062003973,
                0.02666508681283803124093089,
                0.02597256603721476254286995,
                0.02531510384129102815759710,
                0.02469010384129102815759710,
                0.02409521984367056414807896,
                0.02352832641963428296894063,
                0.02298749353699501850166102,
                0.02247096461137518379091722,
                0.02197713745088135663042339,
                0.02150454765882086513703965,
                0.02105185413233829383780923,
                0.02061782635456051606003145,
                0.02020133322669712580597065,
                0.01980133322669712580597065,
                0.01941686571420193164987683,
                0.01904704322899483105816086,
                0.01869104465298913508094477,
                0.01834810912486842177504628,
                0.01801753061247172756017024,
                0.01769865306145131939690494,
                0.01739086605006319997554452,
                0.01709360088954001329302371,
                0.01680632711763538818529605,
                0.01652854933985761040751827,
                0.01625980437882562975715546,
                0.01599965869724394401313881,
                0.01574770606433893015574400,
                0.01550356543933893015574400,
                0.01526687904880638577704578,
                0.01503731063741979257227076,
                0.01481454387422086185273411,
                0.01459828089844231513993134,
                0.01438824099085987447620523,
                0.01418415935820681325171544,
                0.01398578601958352422176106,
                0.01379288478501562298719316,
                0.01360523231738567365335942,
                0.01342261726990576130858221,
                0.01324483949212798353080444,
                0.01307170929822216635628920,
                0.01290304679189732236910755,
                0.01273868124291638877278934,
                0.01257845051066194236996928,
                0.01242220051066194236996928,
                0.01226978472038606978956995,
                0.01212106372098095378719041,
                0.01197590477193174490346273,
                0.01183418141592267460867815,
                0.01169577311142440471248438,
                0.01156056489076458859566448,
                0.01142844704164317229232189,
                0.01129931481023821361463594,
                0.01117306812421372175754719,
                0.01104961133409026496742374,
                0.01092885297157366069257770,
                0.01081070552355853781923177,
                0.01069508522063334415522437,
                0.01058191183901270133041676,
                0.01047110851491297833872701,
                0.01036260157046853389428257,
                0.01025632035036012704977199, // ...
                0.01015219706839427948625679, // psi(1,99)
                0.01005016666333357139524567 // psi(1,100)
            };

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double PsiNxg0(int n, double x)
            {
                if (n == 0)
                    return Digamma(x);

                // Abramowitz + Stegun 6.4.10
                double value = HurwitzZeta(n + 1d, x) * LnFactorial(n);
                if (ElementaryFunctions.IsEven(n))
                    value = -value;
                return value;
            }

            /// <summary>
            /// Coefficients for gamma=7, kmax=8 Lanczos method.
            /// </summary>
            private static readonly double[] Lanczos7C =
            {
                0.99999999999980993227684700473478,
                676.520368121885098567009190444019,
                -1259.13921672240287047156078755283,
                771.3234287776530788486528258894,
                -176.61502916214059906584551354,
                12.507343278686904814458936853,
                -0.13857109526572011689554707,
                9.984369578019570859563e-6,
                1.50563273514931155834e-7
            };

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double ComplexLnGammaLanczos(double zr, double zi, out double imag)
            {
                // Complex version of Lanczos method; becomes bad in the left half-plane.
                zr -= 1d; // Lanczos writes z! instead of Gamma(z).
                double agr = Lanczos7C[0];
                double agi = 0d;
                for (int i = 1; i <= 8; ++i)
                {
                    double r = zr + i;
                    double a = Lanczos7C[i] / (r * r + zi * zi);
                    agr += a * r;
                    agi -= a * zi;
                }

                double logzr = ComplexLog(zr + 7.5, zi, out double logzi);
                double logagr = ComplexLog(agr, agi, out double logagi);

                // (z+0.5)*log(z+7.5) - (z+7.5) + logRootTwoPi + log(Ag(z))
                const double logRootTwoPi = 0.9189385332046727418;
                imag = zi * logzr + (zr + 0.5) * logzi - zi + logagi;
                imag = RestrictAngle(agi);
                return (zr + 0.5) * logzr - zi * logzi - (zr + 7.5) + logRootTwoPi + logagr;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double ComplexPsiAsymptotic(double zr, double zi, out double imag)
            {
                // Psi(z) for large |z| in the right half-plane; [Abramowitz + Stegun, 6.3.18].
                // Coefficients in the asymptotic expansion for large z;
                // let w = z^(-2) and write the expression in the form
                // ln(z) - 1/(2z) - 1/12 w (1 + c1 w + c2 w + c3 w + ... )
                const double c1 = -0.1;
                const double c2 = 1d / 21;
                const double c3 = -0.05;

                // inverse(z)
                double ss = 1.0 / (zr * zr + zi * zi);
                double invr = zr * ss;
                double invi = -zi * ss;
                double tmpr = invr;
                double tmpi = invi;

                // w = inverse * inverse
                double wr = tmpr * tmpr - tmpi * tmpi;
                double wi = tmpr * tmpi + tmpi * tmpr;

                // Horner method evaluation of term in parentheses.
                // sum = w * c3/c2 + 1
                double sumr = wr * c3 / c2 + 1d;
                double sumi = wi * c3 / c2;

                // sum = (sum * c2/c1) * w + 1
                tmpr = sumr * c2 / c1;
                tmpi = sumi * c2 / c1;
                sumr = tmpr * wr - tmpi * wi + 1d;
                sumi = tmpr * wi + tmpi * wr;

                // sum = (sum * c1) * w + 1
                tmpr = sumr * c1;
                tmpi = sumi * c1;
                sumr = tmpr * wr - tmpi * wi + 1d;
                sumi = tmpr * wi + tmpi * wr;

                // Correction added to log(z).
                // cs = sum * w
                tmpr = sumr;
                tmpi = sumi;
                sumr = tmpr * wr - tmpi * wi;
                sumi = tmpr * wi + tmpi * wr;

                // cs = cs * (-1d/12d)
                sumr *= -1d / 12d;
                sumi *= -1d / 12d;

                // cs = cs - 0.5 * inverse
                sumr += -0.5 * invr;
                sumi += -0.5 * invi;

                // cs = cs + log(z)
                tmpr = ComplexLog(zr, zi, out tmpi);
                sumr += tmpr;
                sumi += tmpi;

                imag = sumi;
                return sumr;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static double ComplexPsiRightHalfPlane(double zr, double zi, out double imag)
            {
                // Psi(z) for complex z in the right half-plane.
                if (Math.Abs(zr) < double.Epsilon && Math.Abs(zi) < double.Epsilon)
                {
                    imag = 0d;
                    return 0d;
                }

                // Compute the number of recurrences to apply.
                int nRecurse = 0;
                if (zr < 20d && Math.Abs(zi) < 20d)
                {
                    double sp = Math.Sqrt(20d + zi);
                    double sn = Math.Sqrt(20d - zi);
                    double rhs = sp * sn - zr;
                    if (rhs > 0d)
                        nRecurse = (int)Math.Ceiling(rhs);
                }

                // Compute asymptotic at the large value z + nRecurse.
                double real = ComplexPsiAsymptotic(zr + nRecurse, zi, out imag);

                // Descend recursively, if necessary.
                if (nRecurse > 0)
                {
                    for (int i = nRecurse; i >= 1; --i)
                    {
                        double znReal = zr + i - 1;
                        double ss = 1d / (znReal * znReal + zi * zi);
                        double znInverseReal = ss * znReal;
                        double znInverseImag = -ss * zi;
                        real -= znInverseReal;
                        imag -= znInverseImag;
                    }
                }

                return real;
            }
        }
    }
}
