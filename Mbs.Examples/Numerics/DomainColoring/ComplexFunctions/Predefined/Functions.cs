using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A combined collections of complex functions.
    /// </summary>
    internal static partial class Functions
    {
        private const double C1Pi2 = Constants.PiOver2;
        private const double C1Pi3 = Constants.Pi / 3;
        private const double C2Pi3 = Constants.TwoPi / 3;

        // private const double C1Pi4 = Constants.PiOver4
        private const double C3Pi4 = Constants.PiOver4 * 3;
        private const double C2Pi = Constants.TwoPi;
        private const double C10Pi = Constants.Pi * 10;

        private static readonly ComplexFunction Separator = new ComplexFunction();

        public static IEnumerable<Category> GetCategories()
        {
            return new[]
            {
                new Category
                {
                    Label = "Assorted", Functions = GetAssorted(),
                },
                new Category
                {
                    Label = "Elementary", Functions = GetElementary(),
                },
                new Category
                {
                    Label = "Exponential", Functions = GetExponential(),
                },
                new Category
                {
                    Label = "Logarithmic", Functions = GetLogarithmic(),
                },
                new Category
                {
                    Label = "Trigonometric", Functions = GetTrigonometric(),
                },
                new Category
                {
                    Label = "Trigonometric inverse", Functions = GetTrigonometricInverse(),
                },
                new Category
                {
                    Label = "Hyperbolic", Functions = GetHyperbolic(),
                },
                new Category
                {
                    Label = "Hyperbolic inverse", Functions = GetHyperbolicInverse(),
                },
                new Category
                {
                    Label = "Special functions", Functions = GetSpecial(),
                },
                new Category
                {
                    Label = "Mandelbrot iterations", Functions = GetMandelbrotIterations(),
                },
                new Category
                {
                    Label = "Möbius transformation", Functions = GetMöbiusTransformations(),
                },
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex Z2(Complex z)
        {
            return z * z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex Z3(Complex z)
        {
            return z * z * z;
        }
    }
}
