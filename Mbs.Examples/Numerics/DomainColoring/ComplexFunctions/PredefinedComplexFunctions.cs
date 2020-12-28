using System.Collections.Generic;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions
{
    internal static class PredefinedComplexFunctions
    {
        public static IEnumerable<ComplexFunction> Get()
        {
            return new[]
            {
                new ComplexFunction
                {
                    // Claudio Rocchini's example function from http://en.wikipedia.org/wiki/File:Color_complex_plot.jpg
                    Label = "  (z²-1)(z-2-i)²/(z²+2+2i)  ",
                    Function = z =>
                    {
                        Complex zs = z - 2 - Complex.ImaginaryOne;
                        return (z * z - 1) * zs * zs / (z * z + 2 + 2 * Complex.ImaginaryOne);
                    },
                },
                new ComplexFunction
                {
                    Label = "  z  ", Function = z => z,
                },
                new ComplexFunction
                {
                    Label = "  z²-1  ", Function = z => (z - 1) * (z + 1),
                },
                new ComplexFunction
                {
                    Label = "  1/(z²-1)  ", Function = z => 1 / (z * z - 1),
                },
                new ComplexFunction
                {
                    Label = "  z³+1  ", Function = z => z * z * z + 1,
                },
                new ComplexFunction
                {
                    Label = "  1/(z³-1)  ", Function = z => 1 / (z * z * z - 1),
                },
                new ComplexFunction
                {
                    Label = "  1/z  ", Function = z => 1 / z,
                },
                new ComplexFunction
                {
                    Label = "  1/z²  ", Function = z => 1 / (z * z),
                },
                new ComplexFunction
                {
                    Label = "  1/z³  ", Function = z => 1 / (z * z * z),
                },
                new ComplexFunction
                {
                    Label = "  ²√z̅  ", Function = Complex.Sqrt,
                },
                new ComplexFunction
                {
                    Label = "  eᶻ  ", Function = Complex.Exp,
                },
                new ComplexFunction
                {
                    Label = "  ln(z)  ", Function = Complex.Log,
                },
                new ComplexFunction
                {
                    Label = "  Trigonometric functions  ",
                },
                new ComplexFunction
                {
                    Label = "  sin(z)  ", Function = Complex.Sin,
                },
                new ComplexFunction
                {
                    Label = "  cos(z)  ", Function = Complex.Cos,
                },
                new ComplexFunction
                {
                    Label = "  tan(z)  ", Function = Complex.Tan,
                },
                new ComplexFunction
                {
                    Label = "  cot(z)  ", Function = Complex.Cot,
                },
                new ComplexFunction
                {
                    Label = "  sec(z)  ", Function = Complex.Sec,
                },
                new ComplexFunction
                {
                    Label = "  cosec(z)  ", Function = Complex.Cosec,
                },
                new ComplexFunction
                {
                    Label = "  Trigonometric hyperbolic functions  ",
                },
                new ComplexFunction
                {
                    Label = "  sinh(z)  ", Function = Complex.Sinh,
                },
                new ComplexFunction
                {
                    Label = "  cosh(z)  ", Function = Complex.Cosh,
                },
                new ComplexFunction
                {
                    Label = "  tanh(z)  ", Function = Complex.Tanh,
                },
                new ComplexFunction
                {
                    Label = "  coth(z)  ", Function = Complex.Coth,
                },
                new ComplexFunction
                {
                    Label = "  sech(z)  ", Function = Complex.Sech,
                },
                new ComplexFunction
                {
                    Label = "  cosech(z)  ", Function = Complex.Cosech,
                },
                new ComplexFunction
                {
                    Label = "  Inverse trigonometric functions  ",
                },
                new ComplexFunction
                {
                    Label = "  asin(z)  ", Function = Complex.Asin,
                },
                new ComplexFunction
                {
                    Label = "  acos(z)  ", Function = Complex.Acos,
                },
                new ComplexFunction
                {
                    Label = "  atan(z)  ", Function = Complex.Atan,
                },
                new ComplexFunction
                {
                    Label = "  acot(z)  ", Function = Complex.Acot,
                },
                new ComplexFunction
                {
                    Label = "  asec(z)  ", Function = Complex.Asec,
                },
                new ComplexFunction
                {
                    Label = "  acosec(z)  ", Function = Complex.Acosec,
                },
                new ComplexFunction
                {
                    Label = "  Inverse trigonometric hyperbolic functions  ",
                },
                new ComplexFunction
                {
                    Label = "  asinh(z)  ", Function = Complex.Asinh,
                },
                new ComplexFunction
                {
                    Label = "  acosh(z)  ", Function = Complex.Acosh,
                },
                new ComplexFunction
                {
                    Label = "  atanh(z)  ", Function = Complex.Atanh,
                },
                new ComplexFunction
                {
                    Label = "  acoth(z)  ", Function = Complex.Acoth,
                },
                new ComplexFunction
                {
                    Label = "  asech(z)  ", Function = Complex.Asech,
                },
                new ComplexFunction
                {
                    Label = "  acosech(z)  ", Function = Complex.Acosech,
                },
                new ComplexFunction
                {
                    Label = "  Special functions  ",
                },
                new ComplexFunction
                {
                    Label = "  Γ(z)  ", Function = SpecialFunctions.Gamma,
                },
                new ComplexFunction
                {
                    Label = "  ln |Γ(z)|  ",
                    Function = z =>
                    {
                        if (z.Real < 0d)
                        {
                            z.Real = -z.Real;
                        }

                        return SpecialFunctions.LnGamma(z);
                    },
                },
                new ComplexFunction
                {
                    Label = "  ψ(z) (digamma)  ", Function = SpecialFunctions.Digamma,
                },
            };
        }
    }
}
