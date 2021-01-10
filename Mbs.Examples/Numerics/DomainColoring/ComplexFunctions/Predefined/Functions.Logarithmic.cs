using System.Collections.Generic;
using Mbs.Numerics;

// ReSharper disable StringLiteralTypo
namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of exponential complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetLogarithmic()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = @"\ln{z}", TexKey = "Logarithmic|LnZ1", Function = Complex.Log,
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln{z^2}", TexKey = "Logarithmic|LnZ2", Function = z => Complex.Log(Z2(z)),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln{z^3}", TexKey = "Logarithmic|LnZ3", Function = z => Complex.Log(Z2(z) * z),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln{z^4}", TexKey = "Logarithmic|LnZ4", Function = z => Complex.Log(Z2(Z2(z))),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln{z^5}", TexKey = "Logarithmic|LnZ5", Function = z => Complex.Log(Z2(Z2(z)) * z),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln{z^6}", TexKey = "Logarithmic|LnZ6", Function = z => Complex.Log(Z2(Z3(z))),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln{z^7}", TexKey = "Logarithmic|LnZ7", Function = z => Complex.Log(Z2(Z3(z)) * z),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln{z^8}", TexKey = "Logarithmic|LnZ8", Function = z => Complex.Log(Z2(Z2(Z2(z)))),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln{z^9}", TexKey = "Logarithmic|LnZ9", Function = z => Complex.Log(Z3(Z3(z))),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"\ln\left(z+1\right)", TexKey = "Logarithmic|LnZ1P1", Function = z => Complex.Log(z + 1),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln\left(z^2+1\right)", TexKey = "Logarithmic|LnZ2P1", Function = z => Complex.Log(Z2(z) + 1),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln\left(z^3+1\right)", TexKey = "Logarithmic|LnZ3P1", Function = z => Complex.Log(Z2(z) * z + 1),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln\left(z^4+1\right)", TexKey = "Logarithmic|LnZ4P1", Function = z => Complex.Log(Z2(Z2(z)) + 1),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\left(z^5+1\right)", TexKey = "Logarithmic|LnZ5P1", Function = z => Complex.Log(Z2(Z2(z)) * z + 1),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\left(z^6+1\right)", TexKey = "Logarithmic|LnZ6P1", Function = z => Complex.Log(Z2(Z3(z)) + 1),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\left(z^7+1\right)", TexKey = "Logarithmic|LnZ7P1", Function = z => Complex.Log(Z2(Z3(z)) * z + 1),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\left(z^8+1\right)", TexKey = "Logarithmic|LnZ8P1", Function = z => Complex.Log(Z2(Z2(Z2(z))) + 1),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\left(z^9+1\right)", TexKey = "Logarithmic|LnZ9P1", Function = z => Complex.Log(Z3(Z3(z)) + 1),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z}", TexKey = "Logarithmic|Ln1Z1", Function = z => Complex.Log(1 / z),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^2}", TexKey = "Logarithmic|Ln1Z2", Function = z => Complex.Log(1 / Z2(z)),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^3}", TexKey = "Logarithmic|Ln1Z3", Function = z => Complex.Log(1 / (Z2(z) * z)),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^4}", TexKey = "Logarithmic|Ln1Z4", Function = z => Complex.Log(1 / Z2(Z2(z))),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^5}", TexKey = "Logarithmic|Ln1Z5", Function = z => Complex.Log(1 / (Z2(Z2(z)) * z)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^6}", TexKey = "Logarithmic|Ln1Z6", Function = z => Complex.Log(1 / Z2(Z3(z))),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^7}", TexKey = "Logarithmic|Ln1Z7", Function = z => Complex.Log(1 / (Z2(Z3(z)) * z)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^8}", TexKey = "Logarithmic|Ln1Z8", Function = z => Complex.Log(1 / Z2(Z2(Z2(z)))),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^9}", TexKey = "Logarithmic|Ln1Z9", Function = z => Complex.Log(1 / Z3(Z3(z))),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z+1}", TexKey = "Logarithmic|Ln1Z1P1", Function = z => Complex.Log(1 / (z + 1)),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^2+1}", TexKey = "Logarithmic|Ln1Z2P1", Function = z => Complex.Log(1 / (Z2(z) + 1)),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^3+1}", TexKey = "Logarithmic|Ln1Z3P1", Function = z => Complex.Log(1 / (Z2(z) * z + 1)),
                    ReMin = -C3Pi4, ReMax = C3Pi4, ImMin = -C3Pi4, ImMax = C3Pi4,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^4+1}", TexKey = "Logarithmic|Ln1Z4P1", Function = z => Complex.Log(1 / (Z2(Z2(z)) + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^5+1}", TexKey = "Logarithmic|Ln1Z5P1", Function = z => Complex.Log(1 / (Z2(Z2(z)) * z + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^6+1}", TexKey = "Logarithmic|Ln1Z6P1", Function = z => Complex.Log(1 / (Z2(Z3(z)) + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^7+1}", TexKey = "Logarithmic|Ln1Z7P1", Function = z => Complex.Log(1 / (Z2(Z3(z)) * z + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^8+1}", TexKey = "Logarithmic|Ln1Z8P1", Function = z => Complex.Log(1 / (Z2(Z2(Z2(z))) + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"\ln\frac{1}{z^9+1}", TexKey = "Logarithmic|Ln1Z9P1", Function = z => Complex.Log(1 / (Z3(Z3(z)) + 1)),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
            };
        }
    }
}
