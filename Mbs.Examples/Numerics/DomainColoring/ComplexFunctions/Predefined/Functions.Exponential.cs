using System.Collections.Generic;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of exponential complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetExponential()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = "e^{z}", TexKey = "Exponential|Ez1", Function = Complex.Exp,
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = "e^{z^2}", TexKey = "Exponential|Ez2", Function = z => Complex.Exp(Z2(z)),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = "e^{z^3}", TexKey = "Exponential|Ez3", Function = z => Complex.Exp(Z2(z) * z),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = "e^{z^4}", TexKey = "Exponential|Ez4", Function = z => Complex.Exp(Z2(Z2(z))),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = "e^{z^5}", TexKey = "Exponential|Ez5", Function = z => Complex.Exp(Z2(Z2(z)) * z),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = "e^{1/z}", TexKey = "Exponential|E1Z1", Function = z => Complex.Exp(1 / z),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = "e^{1/z^2}", TexKey = "Exponential|E1Z2", Function = z => Complex.Exp(1 / Z2(z)),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = "e^{1/z^3}", TexKey = "Exponential|E1Z3", Function = z => Complex.Exp(1 / (Z2(z) * z)),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = "e^{1/z^4}", TexKey = "Exponential|E1Z4", Function = z => Complex.Exp(1 / Z2(Z2(z))),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = "e^{1/z^5}", TexKey = "Exponential|E1Z5", Function = z => Complex.Exp(1 / (Z2(Z2(z)) * z)),
                    ReMin = -C2Pi3, ReMax = C2Pi3, ImMin = -C2Pi3, ImMax = C2Pi3,
                },
                new ComplexFunction
                {
                    Label = "e^{1/z^6}", TexKey = "Exponential|E1Z6", Function = z => Complex.Exp(1 / Z2(Z3(z))),
                    ReMin = -C2Pi3, ReMax = C2Pi3, ImMin = -C2Pi3, ImMax = C2Pi3,
                },
                new ComplexFunction
                {
                    Label = "e^{1/z^7}", TexKey = "Exponential|E1Z7", Function = z => Complex.Exp(1 / (Z2(Z3(z)) * z)),
                    ReMin = -C2Pi3, ReMax = C2Pi3, ImMin = -C2Pi3, ImMax = C2Pi3,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"e^{\frac{1}{z+1}}", TexKey = "Exponential|E1Z1P1", Function = z => Complex.Exp(1 / (z + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"e^{\frac{1}{z^2+1}}", TexKey = "Exponential|E1Z2P1", Function = z => Complex.Exp(1 / (Z2(z) + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"e^{\frac{1}{z^3+1}}", TexKey = "Exponential|E1Z3P1", Function = z => Complex.Exp(1 / (Z2(z) * z + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"e^{\frac{1}{z^4+1}}", TexKey = "Exponential|E1Z4P1", Function = z => Complex.Exp(1 / (Z2(Z2(z)) + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"e^{\frac{1}{z^5+1}}", TexKey = "Exponential|E1Z5P1", Function = z => Complex.Exp(1 / (Z2(Z2(z)) * z + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"e^{\frac{1}{z^6+1}}", TexKey = "Exponential|E1Z6P1", Function = z => Complex.Exp(1 / (Z2(Z3(z)) + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"e^{\frac{1}{z^7+1}}", TexKey = "Exponential|E1Z7P1", Function = z => Complex.Exp(1 / (Z2(Z3(z)) * z + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"e^{\frac{1}{z^8+1}}", TexKey = "Exponential|E1Z8P1", Function = z => Complex.Exp(1 / (Z2(Z2(Z2(z))) + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"e^{\frac{1}{z^9+1}}", TexKey = "Exponential|E1Z9P1", Function = z => Complex.Exp(1 / (Z3(Z3(z)) + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
            };
        }
    }
}
