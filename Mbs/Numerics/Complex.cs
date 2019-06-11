using System;
using System.Globalization;
using System.Runtime;

namespace Mbs.Numerics
{
    /// <summary>
    /// Represents a complex number.
    /// </summary>
    public struct Complex : IComparable<Complex>, IEquatable<Complex>
    {
        private double real;

        /// <summary>
        /// Gets or sets the real part of the complex number.
        /// </summary>
        public double Real
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries.")]
            get => real;
            set { real = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the complex number is real.
        /// </summary>
        public bool IsReal => Math.Abs(imag) < double.Epsilon;

        /// <summary>
        /// Gets a value indicating whether the complex number is real and not negative, that is ≥ 0.
        /// </summary>
        public bool IsRealNonNegative => Math.Abs(imag) < double.Epsilon && real >= 0d;

        private double imag;

        /// <summary>
        /// Gets or sets the imaginary part of the complex number.
        /// </summary>
        public double Imag
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries.")]
            get => imag;
            set { imag = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the complex number is imaginary.
        /// </summary>
        public bool IsImaginary => Math.Abs(real) < double.Epsilon;

        /// <summary>
        /// Gets or sets the modulus of the complex number.
        /// If this complex number is zero when the modulus is set, the complex number is assumed to be positive real with an argument of zero.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if an attempt is made to set a negative modulus.
        /// </exception>
        public double Modulus
        {
            get => Math.Sqrt(real * real + imag * imag);
            set
            {
                if (value < 0d)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Value must not be negative");
                if (double.IsInfinity(value))
                {
                    real = value;
                    imag = value;
                }
                else
                {
                    if (IsZero)
                    {
                        real = value;
                        imag = 0d;
                    }
                    else
                    {
                        double factor = value / Math.Sqrt(real * real + imag * imag);
                        real *= factor;
                        imag *= factor;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the squared modulus of the complex number.
        /// If the complex number is zero when the squared modulus is set,
        /// the complex number is assumed to be positive real with an argument of zero.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if an attempt is made to set a negative squared modulus.
        /// </exception>
        public double ModulusSquared
        {
            get => real * real + imag * imag;
            set
            {
                if (value < 0d)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Value must not be negative");
                if (double.IsInfinity(value))
                {
                    real = value;
                    imag = value;
                }
                else
                {
                    if (IsZero)
                    {
                        real = Math.Sqrt(value);
                        imag = 0d;
                    }
                    else
                    {
                        double factor = value / (real * real + imag * imag);
                        real *= factor;
                        imag *= factor;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the argument of the complex number.
        /// Argument always returns a value within an interval (-π, π].
        /// If the complex number is zero, the complex number is assumed to be positive real with an argument of zero.
        /// </summary>
        public double Argument
        {
            get
            {
                if (Math.Abs(imag) < double.Epsilon)
                    return real < 0d ? Constants.Pi : 0d;
                return Math.Atan2(imag, real);
            }

            set
            {
                double modulus = Modulus;
                real = Math.Cos(value) * modulus;
                imag = Math.Sin(value) * modulus;
            }
        }

        /// <summary>
        /// Gets the unity of the complex number (same as the argument, but on the unit circle, eᶦᶿ).
        /// </summary>
        public Complex Sign
        {
            get
            {
                if (double.IsPositiveInfinity(real))
                {
                    if (double.IsPositiveInfinity(imag))
                        return new Complex(Constants.HalfSqrt2, Constants.HalfSqrt2);
                    if (double.IsNegativeInfinity(imag))
                        return new Complex(Constants.HalfSqrt2, -Constants.HalfSqrt2);
                }
                else if (double.IsNegativeInfinity(real))
                {
                    if (double.IsPositiveInfinity(imag))
                        return new Complex(-Constants.HalfSqrt2, -Constants.HalfSqrt2);
                    if (double.IsNegativeInfinity(imag))
                        return new Complex(-Constants.HalfSqrt2, Constants.HalfSqrt2);
                }

                // Don't replace this with "Modulus"!
                double mod, ar = Math.Abs(real), ai = Math.Abs(imag);
                if (ar > ai)
                {
                    double r = imag / real;
                    mod = ar * Math.Sqrt(1 + r * r);
                }
                else if (Math.Abs(imag) > double.Epsilon)
                {
                    double r = real / imag;
                    mod = ai * Math.Sqrt(1 + r * r);
                }
                else
                {
                    return Zero;
                }

                return new Complex(real / mod, imag / mod);
            }
        }

        /// <summary>
        /// The zero complex number value.
        /// </summary>
        public static readonly Complex Zero = new Complex(0d, 0d);

        /// <summary>
        /// Gets a value indicating whether the complex number is zero.
        /// </summary>
        public bool IsZero => Math.Abs(real) < double.Epsilon && Math.Abs(imag) < double.Epsilon;

        /// <summary>
        /// The complex number one.
        /// </summary>
        public static readonly Complex One = new Complex(1d, 0d);

        /// <summary>
        /// Gets a value indicating whether the complex number is one.
        /// </summary>
        public bool IsOne => Math.Abs(real - 1d) < double.Epsilon && Math.Abs(imag) < double.Epsilon;

        /// <summary>
        /// The imaginary unit complex number.
        /// </summary>
        public static readonly Complex ImaginaryOne = new Complex(0d, 1d);

        /// <summary>
        /// Gets a value indicating whether the complex number is the imaginary unit.
        /// </summary>
        public bool IsImaginaryOne => Math.Abs(real) < double.Epsilon && Math.Abs(imag - 1d) < double.Epsilon;

        /// <summary>
        /// Represents a complex value that is not a number.
        /// </summary>
        public static readonly Complex NaN = new Complex(double.NaN, double.NaN);

        /// <summary>
        /// Gets a value indicating whether a complex number evaluates to a value that is not a number.
        /// </summary>
        public bool IsNaN => double.IsNaN(real) || double.IsNaN(imag);

        /// <summary>
        /// Represents the complex infinity value as a complex number of infinite real and imaginary part.
        /// </summary>
        public static readonly Complex Infinity = new Complex(double.PositiveInfinity, double.PositiveInfinity);

        /// <summary>
        /// Gets a value indicating whether this complex number evaluates to the complex infinity value or to a directed infinity value.
        /// </summary>
        public bool IsInfinity => double.IsInfinity(real) || double.IsInfinity(imag);

        /// <summary>
        /// Gets or sets the conjugate of the complex number.
        /// The semantic of <i>setting the conjugate</i> is such that <c>a.Conjugate = b</c> is equivalent to <c>a = b.Conjugate</c>, where <c>a</c> and <c>b</c> are the complex numbers.
        /// </summary>
        public Complex Conjugate
        {
            get => new Complex(real, -imag);
            set => this = value.Conjugate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> struct.
        /// </summary>
        /// <param name="real">The real part of the complex number.</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public Complex(double real)
        {
            this.real = real;
            imag = 0d;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> struct.
        /// </summary>
        /// <param name="real">The real part of the complex number.</param>
        /// <param name="imaginary">The imaginary part of the complex number.</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public Complex(double real, double imaginary)
        {
            this.real = real;
            imag = imaginary;
        }

        /// <summary>
        /// Constructs a new instance of the complex number from its real and imaginary parts.
        /// </summary>
        /// <param name="real">The real part of the complex number.</param>
        /// <param name="imaginary">The imaginary part of the complex number.</param>
        /// <returns>The created instance of the complex number.</returns>
        public static Complex FromRealImaginary(double real, double imaginary)
        {
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Constructs a new instance of the complex number from its modulus and argument.
        /// </summary>
        /// <param name="modulus">The non-negative modulus of the complex number.</param>
        /// <param name="argument">The argument of the complex number.</param>
        /// <returns>The created instance of the complex number.</returns>
        public static Complex FromModulusArgument(double modulus, double argument)
        {
            if (modulus < 0d)
                throw new ArgumentOutOfRangeException(nameof(modulus), modulus, "Value must not be negative.");
            return new Complex(modulus * Math.Cos(argument), modulus * Math.Sin(argument));
        }

        /// <summary>
        /// The unary addition opreator.
        /// </summary>
        /// <param name="summand">A complex summand.</param>
        /// <returns>The summand.</returns>
        public static Complex operator +(Complex summand) => summand;

        /// <summary>
        /// The unary addition opreator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="summand">A complex summand.</param>
        /// <returns>The summand.</returns>
        public static Complex Plus(Complex summand) => summand;

        /// <summary>
        /// The complex addition opreator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex summand.</param>
        /// <param name="rhs">A right-hand side complex summand.</param>
        /// <returns>The sum of the two specified complex numbers.</returns>
        public static Complex operator +(Complex lhs, Complex rhs) => new Complex(lhs.real + rhs.real, lhs.imag + rhs.imag);

        /// <summary>
        /// The complex addition opreator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex summand.</param>
        /// <param name="rhs">A right-hand side complex summand.</param>
        /// <returns>The sum of the two specified complex numbers.</returns>
        public static Complex Add(Complex lhs, Complex rhs) => lhs + rhs;

        /// <summary>
        /// The complex addition opreator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex summand.</param>
        /// <param name="rhs">A right-hand side real summand.</param>
        /// <returns>The sum of the two specified complex and real numbers.</returns>
        public static Complex operator +(Complex lhs, double rhs) => new Complex(lhs.real + rhs, lhs.imag);

        /// <summary>
        /// The complex addition opreator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex summand.</param>
        /// <param name="rhs">A right-hand side real summand.</param>
        /// <returns>The sum of the two specified complex and real numbers.</returns>
        public static Complex Add(Complex lhs, double rhs) => lhs + rhs;

        /// <summary>
        /// The complex addition operator.
        /// </summary>
        /// <param name="lhs">A left-hand side real summand.</param>
        /// <param name="rhs">A right-hand side complex summand.</param>
        /// <returns>The sum of the two specified real and complex numbers.</returns>
        public static Complex operator +(double lhs, Complex rhs) => new Complex(lhs + rhs.real, rhs.imag);

        /// <summary>
        /// The complex addition opreator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side real summand.</param>
        /// <param name="rhs">A right-hand side complex summand.</param>
        /// <returns>The sum of the two specified real and complex numbers.</returns>
        public static Complex Add(double lhs, Complex rhs) => lhs + rhs;

        /// <summary>
        /// The unary negation operator.
        /// </summary>
        /// <param name="subtrahend">A complex subtrahend.</param>
        /// <returns>The negated complex subtrahend.</returns>
        public static Complex operator -(Complex subtrahend) => new Complex(-subtrahend.real, -subtrahend.imag);

        /// <summary>
        /// The unary negation operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="subtrahend">A complex subtrahend.</param>
        /// <returns>The negated complex subtrahend.</returns>
        public static Complex Negate(Complex subtrahend) => -subtrahend;

        /// <summary>
        /// The complex subtraction operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex minuend.</param>
        /// <param name="rhs">A right-hand side complex subtrahend.</param>
        /// <returns>The difference of the two specified complex numbers.</returns>
        public static Complex operator -(Complex lhs, Complex rhs) => new Complex(lhs.real - rhs.real, lhs.imag - rhs.imag);

        /// <summary>
        /// The complex subtraction operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex minuend.</param>
        /// <param name="rhs">A right-hand side complex subtrahend.</param>
        /// <returns>The difference of the two specified complex numbers.</returns>
        public static Complex Subtract(Complex lhs, Complex rhs) => lhs - rhs;

        /// <summary>
        /// The complex subtraction operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex minuend.</param>
        /// <param name="rhs">A right-hand side real subtrahend.</param>
        /// <returns>The difference of the two specified complex and real numbers.</returns>
        public static Complex operator -(Complex lhs, double rhs) => new Complex(lhs.real - rhs, lhs.imag);

        /// <summary>
        /// The complex subtraction operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex minuend.</param>
        /// <param name="rhs">A right-hand side real subtrahend.</param>
        /// <returns>The difference of the two specified complex and real numbers.</returns>
        public static Complex Subtract(Complex lhs, double rhs) => lhs - rhs;

        /// <summary>
        /// The complex subtraction operator.
        /// </summary>
        /// <param name="lhs">A left-hand side real minuend.</param>
        /// <param name="rhs">A right-hand side complex subtrahend.</param>
        /// <returns>The difference of the two specified real and complex numbers.</returns>
        public static Complex operator -(double lhs, Complex rhs) => new Complex(lhs - rhs.real, -rhs.imag);

        /// <summary>
        /// The complex subtraction operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side real minuend.</param>
        /// <param name="rhs">A right-hand side complex subtrahend.</param>
        /// <returns>The difference of the two specified real and complex numbers.</returns>
        public static Complex Subtract(double lhs, Complex rhs) => lhs - rhs;

        /// <summary>
        /// The complex multiplication operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex multiplicand.</param>
        /// <param name="rhs">A right-hand side complex multiplier.</param>
        /// <returns>The product of the two specified complex numbers.</returns>
        public static Complex operator *(Complex lhs, Complex rhs) => new Complex(lhs.real * rhs.real - lhs.imag * rhs.imag, lhs.imag * rhs.real + lhs.real * rhs.imag);

        /// <summary>
        /// The complex multiplication operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex multiplicand.</param>
        /// <param name="rhs">A right-hand side complex multiplier.</param>
        /// <returns>The product of the two specified complex numbers.</returns>
        public static Complex Multiply(Complex lhs, Complex rhs) => lhs * rhs;

        /// <summary>
        /// The complex multiplication operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex multiplicand.</param>
        /// <param name="rhs">A right-hand side real multiplier.</param>
        /// <returns>The product of the two specified complex and real numbers.</returns>
        public static Complex operator *(Complex lhs, double rhs) => new Complex(rhs * lhs.real, rhs * lhs.imag);

        /// <summary>
        /// The complex multiplication operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex multiplicand.</param>
        /// <param name="rhs">A right-hand side real multiplier.</param>
        /// <returns>The product of the two specified complex and real numbers.</returns>
        public static Complex Multiply(Complex lhs, double rhs) => lhs * rhs;

        /// <summary>
        /// The complex multiplication operator.
        /// </summary>
        /// <param name="lhs">A left-hand side real multiplicand.</param>
        /// <param name="rhs">A right-hand side complex multiplier.</param>
        /// <returns>The product of the two specified real and complex numbers.</returns>
        public static Complex operator *(double lhs, Complex rhs) => new Complex(lhs * rhs.real, lhs * rhs.imag);

        /// <summary>
        /// The complex multiplication operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side real multiplicand.</param>
        /// <param name="rhs">A right-hand side complex multiplier.</param>
        /// <returns>The product of the two specified real and complex numbers.</returns>
        public static Complex Multiply(double lhs, Complex rhs) => lhs * rhs;

        /// <summary>
        /// Implicit conversion of a real double to a complex number.
        /// </summary>
        /// <param name="number">The double number to convert.</param>
        /// <returns>The complex number.</returns>
        public static implicit operator Complex(double number) => new Complex(number, 0d);

        /// <summary>
        /// Implicit conversion of a real double to a complex number.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="number">The double number to convert.</param>
        /// <returns>The complex number.</returns>
        public static Complex ToComplex(double number) => new Complex(number, 0d);

        /// <summary>
        /// The complex division operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex dividend.</param>
        /// <param name="rhs">A right-hand side complex divisor.</param>
        /// <returns>The division of the two specified complex numbers.</returns>
        public static Complex operator /(Complex lhs, Complex rhs)
        {
            if (rhs.IsZero)
                return Infinity;
            double z2Mod = rhs.ModulusSquared;
            return new Complex((lhs.real * rhs.real + lhs.imag * rhs.imag) / z2Mod, (lhs.imag * rhs.real - lhs.real * rhs.imag) / z2Mod);
        }

        /// <summary>
        /// The complex division operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex dividend.</param>
        /// <param name="rhs">A right-hand side complex divisor.</param>
        /// <returns>The division of the two specified complex numbers.</returns>
        public static Complex Divide(Complex lhs, Complex rhs) => lhs / rhs;

        /// <summary>
        /// The complex division operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex dividend.</param>
        /// <param name="rhs">A right-hand side real divisor.</param>
        /// <returns>The division of the two specified complex and real numbers.</returns>
        public static Complex operator /(Complex lhs, double rhs)
        {
            if (Math.Abs(rhs) < double.Epsilon)
                return Infinity;
            return new Complex(lhs.real / rhs, lhs.imag / rhs);
        }

        /// <summary>
        /// The complex division operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex dividend.</param>
        /// <param name="rhs">A right-hand side real divisor.</param>
        /// <returns>The division of the two specified complex and real numbers.</returns>
        public static Complex Divide(Complex lhs, double rhs) => lhs / rhs;

        /// <summary>
        /// The complex division operator.
        /// </summary>
        /// <param name="lhs">A left-hand side real dividend.</param>
        /// <param name="rhs">A right-hand side complex divisor.</param>
        /// <returns>The division of the two specified real and complex numbers.</returns>
        public static Complex operator /(double lhs, Complex rhs)
        {
            if (rhs.IsZero)
                return Infinity;
            double zmod = rhs.ModulusSquared;
            return new Complex(lhs * rhs.real / zmod, -lhs * rhs.imag / zmod);
        }

        /// <summary>
        /// The complex division operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side real dividend.</param>
        /// <param name="rhs">A right-hand side complex divisor.</param>
        /// <returns>The division of the two specified real and complex numbers.</returns>
        public static Complex Divide(double lhs, Complex rhs) => lhs / rhs;

        /// <summary>
        /// The equality operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex number.</param>
        /// <param name="rhs">A right-hand side complex number.</param>
        /// <returns>The equality of the two specified complex numbers.</returns>
        public static bool operator ==(Complex lhs, Complex rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// The equality operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex number.</param>
        /// <param name="rhs">A right-hand side complex number.</param>
        /// <returns>The equality of the two specified complex numbers.</returns>
        public static bool Equals(Complex lhs, Complex rhs) => lhs == rhs;

        /// <summary>
        /// The equality operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex number.</param>
        /// <param name="rhs">A right-hand side real number.</param>
        /// <returns>The equality of the two specified complex and real numbers.</returns>
        public static bool operator ==(Complex lhs, double rhs)
        {
            return /*!lhs.IsNaN && !double.IsNaN(rhs) &&*/ Math.Abs(lhs.imag) < double.Epsilon && Math.Abs(rhs - lhs.real) < double.Epsilon;
        }

        /// <summary>
        /// The equality operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex number.</param>
        /// <param name="rhs">A right-hand side real number.</param>
        /// <returns>The equality of the two specified complex and real numbers.</returns>
        public static bool Equals(Complex lhs, double rhs) => lhs == rhs;

        /// <summary>
        /// The equality operator.
        /// </summary>
        /// <param name="lhs">A left-hand side real number.</param>
        /// <param name="rhs">A right-hand side complex number.</param>
        /// <returns>The equality of the two specified real and complex numbers.</returns>
        public static bool operator ==(double lhs, Complex rhs)
        {
            return /*!rhs.IsNaN && !double.IsNaN(lhs) &&*/ Math.Abs(rhs.imag) < double.Epsilon && Math.Abs(lhs - rhs.real) < double.Epsilon;
        }

        /// <summary>
        /// The equality operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side real number.</param>
        /// <param name="rhs">A right-hand side complex number.</param>
        /// <returns>The equality of the two specified real and complex numbers.</returns>
        public static bool Equals(double lhs, Complex rhs) => lhs == rhs;

        /// <summary>
        /// The inequality operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex number.</param>
        /// <param name="rhs">A right-hand side complex number.</param>
        /// <returns>The inequality of the two specified complex numbers.</returns>
        public static bool operator !=(Complex lhs, Complex rhs)
        {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// The inequality operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex number.</param>
        /// <param name="rhs">A right-hand side complex number.</param>
        /// <returns>The inequality of the two specified complex numbers.</returns>
        public static bool NotEquals(Complex lhs, Complex rhs) => lhs != rhs;

        /// <summary>
        /// The inequality operator.
        /// </summary>
        /// <param name="lhs">A left-hand side complex number.</param>
        /// <param name="rhs">A right-hand side real number.</param>
        /// <returns>The inequality of the two specified complex and real numbers.</returns>
        public static bool operator !=(Complex lhs, double rhs)
        {
            return /*lhs.IsNaN || double.IsNaN(rhs) ||*/ Math.Abs(lhs.imag) > double.Epsilon || Math.Abs(rhs - lhs.real) > double.Epsilon;
        }

        /// <summary>
        /// The inequality operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side complex number.</param>
        /// <param name="rhs">A right-hand side real number.</param>
        /// <returns>The inequality of the two specified complex and real numbers.</returns>
        public static bool NotEquals(Complex lhs, double rhs) => lhs != rhs;

        /// <summary>
        /// The inequality operator.
        /// </summary>
        /// <param name="lhs">A left-hand side real number.</param>
        /// <param name="rhs">A right-hand side complex number.</param>
        /// <returns>The inequality of the two specified real and complex numbers.</returns>
        public static bool operator !=(double lhs, Complex rhs)
        {
            return /*rhs.IsNaN || double.IsNaN(lhs) ||*/ Math.Abs(rhs.imag) > double.Epsilon || Math.Abs(lhs - rhs.real) > double.Epsilon;
        }

        /// <summary>
        /// The inequality operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA2225: Operator overloads have named alternates.
        /// </remarks>
        /// <param name="lhs">A left-hand side real number.</param>
        /// <param name="rhs">A right-hand side complex number.</param>
        /// <returns>The inequality of the two specified real and complex numbers.</returns>
        public static bool NotEquals(double lhs, Complex rhs) => lhs != rhs;

        /// <summary>
        /// The less-then operator.
        /// </summary>
        /// <param name="lhs">A left-hand side.</param>
        /// <param name="rhs">A right-hand side.</param>
        /// <returns>The boolean specifying the less-then relationship.</returns>
        public static bool operator <(Complex lhs, Complex rhs) => lhs.CompareTo(rhs) < 0;

        /// <summary>
        /// The less-or-equal-then operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA1036: Override methods on comparable types.
        /// </remarks>
        /// <param name="lhs">A left-hand side.</param>
        /// <param name="rhs">A right-hand side.</param>
        /// <returns>The boolean specifying the less-or-equal-then relationship.</returns>
        public static bool operator <=(Complex lhs, Complex rhs) => lhs.CompareTo(rhs) <= 0;

        /// <summary>
        /// The greater-then operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA1036: Override methods on comparable types.
        /// </remarks>
        /// <param name="lhs">A left-hand side.</param>
        /// <param name="rhs">A right-hand side.</param>
        /// <returns>The boolean specifying the greater-then relationship.</returns>
        public static bool operator >(Complex lhs, Complex rhs) => lhs.CompareTo(rhs) > 0;

        /// <summary>
        /// The greater-or-equal-then operator.
        /// </summary>
        /// <remarks>
        /// To satisfy CA1036: Override methods on comparable types.
        /// </remarks>
        /// <param name="lhs">A left-hand side.</param>
        /// <param name="rhs">A right-hand side.</param>
        /// <returns>The boolean specifying the greater-or-equal-then relationship.</returns>
        public static bool operator >=(Complex lhs, Complex rhs) => lhs.CompareTo(rhs) >= 0;

        /// <summary>
        /// The absolute value of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The absolute value of the specified complex number.</returns>
        public static double Abs(Complex number)
        {
            return number.Abs();
        }

        /// <summary>
        /// The absolute value of this complex number.
        /// </summary>
        /// <returns>The absolute value.</returns>
        public double Abs()
        {
            return Math.Sqrt(imag * imag + real * real);
        }

        /// <summary>
        /// The inversion of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The inversion of a specified complex number.</returns>
        public static Complex Inv(Complex number)
        {
            return number.Inv();
        }

        /// <summary>
        /// The inversion of a complex number.
        /// </summary>
        /// <returns>The inversion of a specified complex number.</returns>
        public Complex Inv()
        {
            double mod = real * real + imag * imag;
            return new Complex(real / mod, -imag / mod);
        }

        /// <summary>
        /// The exponential of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The exponential of the specified complex number.</returns>
        public static Complex Exp(Complex number)
        {
            return number.Exp();
        }

        /// <summary>
        /// The exponential of this complex number.
        /// </summary>
        /// <returns>The exponential of the complex number.</returns>
        public Complex Exp()
        {
            double exp = Math.Exp(real);
            if (IsReal)
                return new Complex(exp, 0d);
            return new Complex(exp * Math.Cos(imag), exp * Math.Sin(imag));
        }

        /// <summary>
        /// The natural logarithm of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The natural logarithm of the specified complex number.</returns>
        public static Complex Log(Complex number)
        {
            return number.Log();
        }

        /// <summary>
        /// The natural logarithm of this complex number.
        /// </summary>
        /// <returns>The natural logarithm of the complex number.</returns>
        public Complex Log()
        {
            if (IsRealNonNegative)
                return new Complex(Math.Log(real), 0d);
            return new Complex(0.5d * Math.Log(ModulusSquared), Argument);
        }

        /// <summary>
        /// Raise a complex number to the given complex power.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <param name="power">A complex power.</param>
        /// <returns>The specified complex power of the specified complex number.</returns>
        public static Complex Pow(Complex number, Complex power)
        {
            return number.Pow(power);
        }

        /// <summary>
        /// Raise this complex number to the given complex power.
        /// </summary>
        /// <param name="power">A complex power.</param>
        /// <returns>The specified complex power of this complex number.</returns>
        public Complex Pow(Complex power)
        {
            if (IsZero)
            {
                if (power.IsZero)
                    return One;
                if (power.Real > 0)
                    return Zero;
                if (power.Real < 0)
                {
                    if (Math.Abs(power.Imag) < double.Epsilon)
                        return new Complex(double.PositiveInfinity, 0d);
                    return new Complex(double.PositiveInfinity, double.PositiveInfinity);
                }

                return NaN;
            }

            return (power * Log()).Exp();
        }

        /// <summary>
        /// Raise a double-precision floating number to the given complex power.
        /// </summary>
        /// <param name="number">A double-precision floating number.</param>
        /// <param name="power">A complex power.</param>
        /// <returns>The specified complex power of the specified double-precision floating number.</returns>
        public static Complex Pow(double number, Complex power)
        {
            return Exp(power * Math.Log(number));
        }

        /// <summary>
        /// Raise a complex number to the given double-precision floating power.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <param name="power">A double-precision floating power.</param>
        /// <returns>The specified double-precision floating power of the specified complex number.</returns>
        public static Complex Pow(Complex number, double power)
        {
            return Exp(power * Log(number));
        }

        /// <summary>
        /// Raise this complex number to the given double-precision floating power.
        /// </summary>
        /// <param name="power">A double-precision floating power.</param>
        /// <returns>The specified double-precision floating power of this complex number.</returns>
        public Complex Pow(double power)
        {
            return Exp(power * Log());
        }

        /// <summary>
        /// The square (power of 2) of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The square (power of 2) of the specified complex number.</returns>
        public static Complex Square(Complex number)
        {
            return number.Square();
        }

        /// <summary>
        /// The square (power of 2) of this complex number.
        /// </summary>
        /// <returns>The (power of 2) root of this complex number.</returns>
        public Complex Square()
        {
            if (IsReal)
                return new Complex(real * real, 0d);
            return new Complex(real * real - imag * imag, 2 * real * imag);
        }

        /// <summary>
        /// The complex square root of a double-precision floating-point number.
        /// </summary>
        /// <param name="number">A double-precision floating-point number.</param>
        /// <returns>The complex square root of a]the specified double-precision floating-point number.</returns>
        public static Complex Sqrt(double number)
        {
            if (number >= 0)
                return new Complex(Math.Sqrt(number));
            return new Complex(0, Math.Sqrt(-number));
        }

        /// <summary>
        /// The square root of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The square root of the specified complex number.</returns>
        public static Complex Sqrt(Complex number)
        {
            return number.Sqrt();
        }

        /// <summary>
        /// The square root of this complex number.
        /// </summary>
        /// <returns>The square root of the complex number.</returns>
        public Complex Sqrt()
        {
            if (IsRealNonNegative)
                return new Complex(Math.Sqrt(real), 0d);
            double mod = Modulus;
            if (imag > 0 || (Math.Abs(imag) < double.Epsilon && real < 0))
                return new Complex(Constants.HalfSqrt2 * Math.Sqrt(mod + real), Constants.HalfSqrt2 * Math.Sqrt(mod - real));
            return new Complex(Constants.HalfSqrt2 * Math.Sqrt(mod + real), -Constants.HalfSqrt2 * Math.Sqrt(mod - real));
        }

        /// <summary>
        /// The cosine of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The cosine of the specified complex number.</returns>
        public static Complex Cos(Complex number)
        {
            return number.Cos();
        }

        /// <summary>
        /// The cosine of this complex number.
        /// </summary>
        /// <returns>The cosine of the complex number.</returns>
        public Complex Cos()
        {
            if (IsReal)
                return new Complex(Math.Cos(real), 0d);
            return new Complex(Math.Cos(real) * Math.Cosh(imag), -Math.Sin(real) * Math.Sinh(imag));
        }

        /// <summary>
        /// The sine of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The sine of the specified complex number.</returns>
        public static Complex Sin(Complex number)
        {
            return number.Sin();
        }

        /// <summary>
        /// The sine of this complex number.
        /// </summary>
        /// <returns>The sine of the complex number.</returns>
        public Complex Sin()
        {
            if (IsReal)
                return new Complex(Math.Sin(real), 0d);
            return new Complex(Math.Sin(real) * Math.Cosh(imag), Math.Cos(real) * Math.Sinh(imag));
        }

        /// <summary>
        /// The tangent of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The tangent of the specified complex number.</returns>
        public static Complex Tan(Complex number)
        {
            return number.Tan();
        }

        /// <summary>
        /// The tangent of this complex number.
        /// </summary>
        /// <returns>The tangent of the complex number.</returns>
        public Complex Tan()
        {
            if (IsReal)
                return new Complex(Math.Tan(real), 0d);
            double cosr = Math.Cos(real);
            double sinhi = Math.Sinh(imag);
            double denom = cosr * cosr + sinhi * sinhi;
            return new Complex(Math.Sin(real) * cosr / denom, sinhi * Math.Cosh(imag) / denom);
        }

        /// <summary>
        /// The cotangent of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The cotangent of the specified complex number.</returns>
        public static Complex Cot(Complex number)
        {
            return number.Cot();
        }

        /// <summary>
        /// The cotangent of this complex number.
        /// </summary>
        /// <returns>The cotangent of the complex number.</returns>
        public Complex Cot()
        {
            if (IsReal)
                return new Complex(1 / Math.Tan(real), 0d);
            double sinr = Math.Sin(real);
            double sinhi = Math.Sinh(imag);
            double denom = sinr * sinr + sinhi * sinhi;
            return new Complex(sinr * Math.Cos(real) / denom, -sinhi * Math.Cosh(imag) / denom);
        }

        /// <summary>
        /// The secant of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The secant of the specified complex number.</returns>
        public static Complex Sec(Complex number)
        {
            return number.Sec();
        }

        /// <summary>
        /// The secant of this complex number.
        /// </summary>
        /// <returns>The secant of the complex number.</returns>
        public Complex Sec()
        {
            if (IsReal)
                return new Complex(1 / Math.Cos(real), 0d);
            double cosr = Math.Cos(real);
            double sinhi = Math.Sinh(imag);
            double denom = cosr * cosr + sinhi * sinhi;
            return new Complex(cosr * Math.Cosh(imag) / denom, Math.Sin(real) * sinhi / denom);
        }

        /// <summary>
        /// The cosecant of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The cosecant of the specified complex number.</returns>
        public static Complex Cosec(Complex number)
        {
            return number.Cosec();
        }

        /// <summary>
        /// The cosecant of this complex number.
        /// </summary>
        /// <returns>The cosecant of the complex number.</returns>
        public Complex Cosec()
        {
            if (IsReal)
                return new Complex(1 / Math.Sin(real), 0d);
            double sinr = Math.Sin(real);
            double sinhi = Math.Sinh(imag);
            double denom = sinr * sinr + sinhi * sinhi;
            return new Complex(sinr * Math.Cosh(imag) / denom, -Math.Cos(real) * sinhi / denom);
        }

        /// <summary>
        /// The arcus cosine of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The arcus cosine of the specified complex number.</returns>
        public static Complex Acos(Complex number)
        {
            return number.Acos();
        }

        /// <summary>
        /// The arcus cosine of this complex number.
        /// </summary>
        /// <returns>The arcus cosine of the complex number.</returns>
        public Complex Acos()
        {
            return -ImaginaryOne * (this + ImaginaryOne * (1 - Square()).Sqrt()).Log();
        }

        /// <summary>
        /// The arcus sine of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The arcus sine of the specified complex number.</returns>
        public static Complex Asin(Complex number)
        {
            return number.Asin();
        }

        /// <summary>
        /// The arcus sine of this complex number.
        /// </summary>
        /// <returns>The arcus sine of the complex number.</returns>
        public Complex Asin()
        {
            return -ImaginaryOne * ((1 - Square()).Sqrt() + ImaginaryOne * this).Log();
        }

        /// <summary>
        /// The arcus tangent of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The arcus tangent of the specified complex number.</returns>
        public static Complex Atan(Complex number)
        {
            return number.Atan();
        }

        /// <summary>
        /// The arcus tangent of this complex number.
        /// </summary>
        /// <returns>The arcus tangent of the complex number.</returns>
        public Complex Atan()
        {
            var iz = new Complex(-imag, real); // ImaginaryOne*this
            return new Complex(0d, 0.5) * ((1 - iz).Log() - (1 + iz).Log());
        }

        /// <summary>
        /// The arcus cotangent of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The arcus cotangent of the specified complex number.</returns>
        public static Complex Acot(Complex number)
        {
            return number.Acot();
        }

        /// <summary>
        /// The arcus cotangent of this complex number.
        /// </summary>
        /// <returns>The arcus cotangent of the complex number.</returns>
        public Complex Acot()
        {
            var iz = new Complex(-imag, real); // ImaginaryOne*this
            return new Complex(0d, 0.5) * ((1 + iz).Log() - (1 - iz).Log()) + Constants.PiOver2;
        }

        /// <summary>
        /// The arcus secant of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The arcus secant of the specified complex number.</returns>
        public static Complex Asec(Complex number)
        {
            return number.Asec();
        }

        /// <summary>
        /// The arcus secant of this complex number.
        /// </summary>
        /// <returns>The arcus secant of the complex number.</returns>
        public Complex Asec()
        {
            Complex inv = 1 / this;
            return -ImaginaryOne * (inv + ImaginaryOne * (1 - inv.Square()).Sqrt()).Log();
        }

        /// <summary>
        /// The arcus cosecant of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The arcus cosecant of the specified complex number.</returns>
        public static Complex Acosec(Complex number)
        {
            return number.Acosec();
        }

        /// <summary>
        /// The arcus cosecant of this complex number.
        /// </summary>
        /// <returns>The arcus cosecant of the complex number.</returns>
        public Complex Acosec()
        {
            Complex inv = 1 / this;
            return -ImaginaryOne * (ImaginaryOne * inv + (1 - inv.Square()).Sqrt()).Log();
        }

        /// <summary>
        /// The hyperbolic cosine of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The hyperbolic cosine of the specified complex number.</returns>
        public static Complex Cosh(Complex number)
        {
            return number.Cosh();
        }

        /// <summary>
        /// The hyperbolic cosine of this complex number.
        /// </summary>
        /// <returns>The hyperbolic cosine of the complex number.</returns>
        public Complex Cosh()
        {
            if (IsReal)
                return new Complex(Math.Cosh(real), 0d);
            return new Complex(Math.Cosh(real) * Math.Cos(imag), Math.Sinh(real) * Math.Sin(imag));
        }

        /// <summary>
        /// The hyperbolic sine of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The hyperbolic sine of the specified complex number.</returns>
        public static Complex Sinh(Complex number)
        {
            return number.Sinh();
        }

        /// <summary>
        /// The hyperbolic sine of this complex number.
        /// </summary>
        /// <returns>The hyperbolic sine of the complex number.</returns>
        public Complex Sinh()
        {
            if (IsReal)
                return new Complex(Math.Sinh(real), 0d);
            return new Complex(Math.Sinh(real) * Math.Cos(imag), Math.Cosh(real) * Math.Sin(imag));
        }

        /// <summary>
        /// The hyperbolic tangent of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The hyperbolic tangent of the specified complex number.</returns>
        public static Complex Tanh(Complex number)
        {
            return number.Tanh();
        }

        /// <summary>
        /// The hyperbolic tangent of this complex number.
        /// </summary>
        /// <returns>The hyperbolic tangent of the complex number.</returns>
        public Complex Tanh()
        {
            if (IsReal)
                return new Complex(Math.Tanh(real), 0d);
            double cosi = Math.Cos(imag);
            double sinhr = Math.Sinh(real);
            double denom = cosi * cosi + sinhr * sinhr;
            return new Complex(Math.Cosh(real) * sinhr / denom, cosi * Math.Sin(imag) / denom);
        }

        /// <summary>
        /// The hyperbolic cotangent of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The hyperbolic cotangent of the specified complex number.</returns>
        public static Complex Coth(Complex number)
        {
            return number.Coth();
        }

        /// <summary>
        /// The hyperbolic cotangent of this complex number.
        /// </summary>
        /// <returns>The hyperbolic cotangent of the complex number.</returns>
        public Complex Coth()
        {
            // return (Exp(2 * this) + 1) / (Exp(2 * this) - 1);
            if (IsReal)
                return new Complex(1 / Math.Tanh(real), 0d);
            double sini = Math.Sin(imag);
            double sinhr = Math.Sinh(real);
            double denom = sini * sini + sinhr * sinhr;
            return new Complex(sinhr * Math.Cosh(real) / denom, -sini * Math.Cos(imag) / denom);
        }

        /// <summary>
        /// The hyperbolic secant of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The hyperbolic secant of the specified complex number.</returns>
        public static Complex Sech(Complex number)
        {
            return number.Sech();
        }

        /// <summary>
        /// The hyperbolic secant of this complex number.
        /// </summary>
        /// <returns>The hyperbolic secant of the complex number.</returns>
        public Complex Sech()
        {
            if (IsReal)
                return new Complex(1 / Math.Cosh(real), 0d);
            Complex exp = Exp();
            return 2 * exp / (exp.Square() + 1);
        }

        /// <summary>
        /// The hyperbolic cosecant of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The hyperbolic  of the specified complex number.</returns>
        public static Complex Cosech(Complex number)
        {
            return number.Cosech();
        }

        /// <summary>
        /// The hyperbolic cosecant of this complex number.
        /// </summary>
        /// <returns>The hyperbolic cosecant of the complex number.</returns>
        public Complex Cosech()
        {
            if (IsReal)
                return new Complex(1 / Math.Sinh(real), 0d);
            Complex exp = Exp();
            return 2 * exp / (exp.Square() - 1);
        }

        /// <summary>
        /// The inverse hyperbolic cosine of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The inverse hyperbolic cosine of the specified complex number.</returns>
        public static Complex Acosh(Complex number)
        {
            return number.Acosh();
        }

        /// <summary>
        /// The inverse hyperbolic cosine of this complex number.
        /// </summary>
        /// <returns>The inverse hyperbolic cosine of the complex number.</returns>
        public Complex Acosh()
        {
            return (this + (this - 1).Sqrt() * (this + 1).Sqrt()).Log();
        }

        /// <summary>
        /// The inverse hyperbolic sine of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The inverse hyperbolic sine of the specified complex number.</returns>
        public static Complex Asinh(Complex number)
        {
            return number.Asinh();
        }

        /// <summary>
        /// The inverse hyperbolic sine of this complex number.
        /// </summary>
        /// <returns>The inverse hyperbolic sine of the complex number.</returns>
        public Complex Asinh()
        {
            return (this + (Square() + 1).Sqrt()).Log();
        }

        /// <summary>
        /// The inverse hyperbolic tangent of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The inverse hyperbolic tangent of the specified complex number.</returns>
        public static Complex Atanh(Complex number)
        {
            return number.Atanh();
        }

        /// <summary>
        /// The inverse hyperbolic tangent of this complex number.
        /// </summary>
        /// <returns>The inverse hyperbolic tangent of the complex number.</returns>
        public Complex Atanh()
        {
            return 0.5 * ((1 + this).Log() - (1 - this).Log());
        }

        /// <summary>
        /// The inverse hyperbolic cotangent of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The inverse hyperbolic cotangent of the specified complex number.</returns>
        public static Complex Acoth(Complex number)
        {
            return number.Acoth();
        }

        /// <summary>
        /// The inverse hyperbolic cotangent of this complex number.
        /// </summary>
        /// <returns>The inverse hyperbolic cotangent of the complex number.</returns>
        public Complex Acoth()
        {
            return 0.5 * ((this + 1).Log() - (this - 1).Log());
        }

        /// <summary>
        /// The inverse hyperbolic secant of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The inverse hyperbolic secant of the specified complex number.</returns>
        public static Complex Asech(Complex number)
        {
            return number.Asech();
        }

        /// <summary>
        /// The inverse hyperbolic secant of this complex number.
        /// </summary>
        /// <returns>The inverse hyperbolic secant of the complex number.</returns>
        public Complex Asech()
        {
            Complex inv = 1d / this;
            return (inv + (inv - 1d).Sqrt() * (inv + 1).Sqrt()).Log();
        }

        /// <summary>
        /// The inverse hyperbolic cosecant of a complex number.
        /// </summary>
        /// <param name="number">A complex number.</param>
        /// <returns>The inverse hyperbolic  of the specified complex number.</returns>
        public static Complex Acosech(Complex number)
        {
            return number.Acosech();
        }

        /// <summary>
        /// The inverse hyperbolic cosecant of this complex number.
        /// </summary>
        /// <returns>The inverse hyperbolic cosecant of the complex number.</returns>
        public Complex Acosech()
        {
            Complex inv = 1d / this;
            return (inv + (inv.Square() + 1d).Sqrt()).Log();
        }

        /// <summary>
        /// Returns the string that represents this object.
        /// </summary>
        /// <returns>Returns the string that represents the object.</returns>
        public override string ToString()
        {
            return ToString(NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Returns the string that represents this object.
        /// </summary>
        /// <param name="format">The format info.</param>
        /// <returns>Returns the string that represents the object.</returns>
        public string ToString(string format)
        {
            return ToString(format, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Returns the string that represents this object.
        /// </summary>
        /// <param name="format">The format info.</param>
        /// <param name="numberFormat">The number format info.</param>
        /// <returns>Returns the string that represents the object.</returns>
        public string ToString(string format, NumberFormatInfo numberFormat)
        {
            if (IsInfinity)
                return "Infinity";
            if (IsNaN)
                return numberFormat.NaNSymbol;
            if (IsReal)
                return real.ToString(format, numberFormat);

            // There's a difference between the negative sign and the subtraction operator!
            if (IsImaginary)
            {
                if (Math.Abs(imag - 1) < double.Epsilon)
                    return "i";
                if (Math.Abs(imag + 1) < double.Epsilon)
                    return string.Concat(numberFormat.NegativeSign, "i");
                if (imag < 0)
                    return string.Concat(numberFormat.NegativeSign, (-imag).ToString(format, numberFormat), "i");
                return string.Concat(imag.ToString(format, numberFormat), "i");
            }

            if (Math.Abs(imag - 1) < double.Epsilon)
                return string.Concat(real.ToString(format, numberFormat), "+i");
            if (Math.Abs(imag + 1) < double.Epsilon)
                return string.Concat(real.ToString(format, numberFormat), "-i");
            if (imag < 0)
                return string.Concat(real.ToString(format, numberFormat), "-", (-imag).ToString(format, numberFormat), "i");
            return string.Concat(real.ToString(format, numberFormat), "+", imag.ToString(format, numberFormat), "i");
        }

        /// <summary>
        /// Returns the string that represents this object.
        /// </summary>
        /// <param name="numberFormat">The number format info.</param>
        /// <returns>Returns the string that represents the object.</returns>
        public string ToString(NumberFormatInfo numberFormat)
        {
            if (IsInfinity)
                return "Infinity";
            if (IsNaN)
                return numberFormat.NaNSymbol;
            if (IsReal)
                return real.ToString(numberFormat);

            // There's a difference between the negative sign and the subtraction operator!
            if (IsImaginary)
            {
                if (Math.Abs(imag - 1) < double.Epsilon)
                    return "i";
                if (Math.Abs(imag + 1) < double.Epsilon)
                    return string.Concat(numberFormat.NegativeSign, "i");
                if (imag < 0)
                    return string.Concat(numberFormat.NegativeSign, (-imag).ToString(numberFormat), "i");
                return string.Concat(imag.ToString(numberFormat), "i");
            }

            if (Math.Abs(imag - 1) < double.Epsilon)
                return string.Concat(real.ToString(numberFormat), "+i");
            if (Math.Abs(imag + 1) < double.Epsilon)
                return string.Concat(real.ToString(numberFormat), "-i");
            if (imag < 0)
                return string.Concat(real.ToString(numberFormat), "-", (-imag).ToString(numberFormat), "i");
            return string.Concat(real.ToString(numberFormat), "+", imag.ToString(numberFormat), "i");
        }

        /// <summary>
        /// Determines whether the specified instances are considered equal.
        /// </summary>
        /// <param name="obj">The object to compare with this object.</param>
        /// <returns>True if objects are equal, false if not.</returns>
        public override bool Equals(object obj)
        {
            return obj is Complex complex && Equals(complex);
        }

        /// <summary>
        /// Determines whether the specified complex numbers are considered equal.
        /// </summary>
        /// <param name="other">The complex number to compare with this one.</param>
        /// <returns>True if complex numbers are equal, false if not.</returns>
        public bool Equals(Complex other)
        {
            return /*!IsNaN && !other.IsNaN &&*/ (Math.Abs(real - other.real) < double.Epsilon) && (Math.Abs(imag - other.imag) < double.Epsilon);
        }

        /// <summary>
        /// Compares this complex number with another complex number.
        /// The complex number's modulus takes precedence over the argument.
        /// </summary>
        /// <param name="other">The complex number to compare with.</param>
        /// <returns>Returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the specified value.</returns>
        public int CompareTo(Complex other)
        {
            int result = Modulus.CompareTo(other.Modulus);
            if (result != 0)
                return result;
            return Argument.CompareTo(other.Argument);
        }

        /// <summary>
        /// Gets the hashcode of the complex number.
        /// </summary>
        /// <returns>The hashcode.</returns>
        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return real.GetHashCode() ^ -imag.GetHashCode();
        }
    }
}
