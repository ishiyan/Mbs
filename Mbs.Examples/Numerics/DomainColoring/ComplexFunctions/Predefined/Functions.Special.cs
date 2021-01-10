using System.Collections.Generic;
using Mbs.Numerics;

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of special complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetSpecial()
        {
            return new[]
            {
                new ComplexFunction
                {
                    // https://en.wikipedia.org/wiki/Gamma_function
                    Label = @"\Gamma\left(z\right)", TexKey = "Special|GammaZ1", Function = SpecialFunctions.Gamma,
                },
                new ComplexFunction
                {
                    Label = @"\ln|\Gamma\left(z\right)|", TexKey = "Special|LnGammaZ1", Function = SpecialFunctions.LnGamma,
                },
                new ComplexFunction
                {
                    // https://en.wikipedia.org/wiki/Digamma_function
                    // @"\psi\left(z\right)\equiv\dfrac{\operatorname\Gamma'\left(z\right)}{\Gamma\left(z\right)}"
                    Label = @"\psi\left(z\right)",
                    TexKey = "Special|DigammaZ1", Function = SpecialFunctions.Digamma,
                },
                new ComplexFunction
                {
                    // https://en.wikipedia.org/wiki/Faddeeva_function
                    Label = @"w\left(z\right)", TexKey = "Special|FaddeevaZ1", Function = SpecialFunctions.Faddeeva,
                },
                new ComplexFunction
                {
                    // https://en.wikipedia.org/wiki/Riemann_zeta_function
                    // https://www.wikiwand.com/en/Riemann_zeta_function
                    Label = @"\zeta\left(z\right)", TexKey = "Special|RiemannZetaZ1", Function = SpecialFunctions.RiemannEuler,
                    ReMin = -C10Pi, ReMax = C10Pi, ImMin = -C10Pi, ImMax = C10Pi,
                },
                new ComplexFunction
                {
                    // https://en.wikipedia.org/wiki/Butterworth_filter
                    Label = @"Butterworth filter, \omega=1, order=9", TexKey = "Special|Butterworth19-Z1", Function = z => ButterworthFilter(z, 1, 9),
                    ReMin = -0.9, ReMax = 0.03, ImMin = 0.6, ImMax = 1,
                },
            };
        }

        private static Complex ButterworthFilter(Complex z, double omega, int order)
        {
            int n2 = order * 2;
            z /= omega;
            Complex value = Complex.One;
            for (int k = 1; k < order; ++k)
            {
                Complex zk = Complex.ImaginaryOne * Complex.Exp((Complex.ImaginaryOne * (2 * k - 1)) / n2);
                value *= 1 / (z - zk);
            }

            return value;
        }
    }
}
