using System.Collections.Generic;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of hyperbolic complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetHyperbolic()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = @"sinh\ z", TexKey = "Hyperbolic|SinhZ1", Function = Complex.Sinh,
                },
                new ComplexFunction
                {
                    Label = @"cosh\ z", TexKey = "Hyperbolic|CoshZ1", Function = Complex.Cosh,
                },
                new ComplexFunction
                {
                    Label = @"tanh\ z", TexKey = "Hyperbolic|TanhZ1", Function = Complex.Tanh,
                },
                new ComplexFunction
                {
                    Label = @"coth\ z", TexKey = "Hyperbolic|CothZ1", Function = Complex.Coth,
                },
                new ComplexFunction
                {
                    Label = @"sech\ z", TexKey = "Hyperbolic|SechZ1", Function = Complex.Sech,
                },
                new ComplexFunction
                {
                    Label = @"csch\ z", TexKey = "Hyperbolic|CschZ1", Function = Complex.Csch,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z}", TexKey = "Hyperbolic|Sinh1Z1", Function = z => Complex.Sinh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"cosh\ \frac{1}{z}", TexKey = "Hyperbolic|Cosh1Z1", Function = z => Complex.Cosh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"tanh\ \frac{1}{z}", TexKey = "Hyperbolic|Tanh1Z1", Function = z => Complex.Tanh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"coth\ \frac{1}{z}", TexKey = "Hyperbolic|Coth1Z1", Function = z => Complex.Coth(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"sech\ \frac{1}{z}", TexKey = "Hyperbolic|Sech1Z1", Function = z => Complex.Sech(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"csch\ \frac{1}{z}", TexKey = "Hyperbolic|Csch1Z1", Function = z => Complex.Csch(1 / z),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sinh\ z", TexKey = "Hyperbolic|SinZ1", Function = Complex.Sinh,
                },
                new ComplexFunction
                {
                    Label = @"sinh\ z^2", TexKey = "Hyperbolic|SinZ2", Function = z => Complex.Sinh(z * z),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ z^3", TexKey = "Hyperbolic|SinZ3", Function = z => Complex.Sinh(z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ z^4", TexKey = "Hyperbolic|SinZ4", Function = z => Complex.Sinh(z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ z^5", TexKey = "Hyperbolic|SinZ5", Function = z => Complex.Sinh(z * z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ z^6", TexKey = "Hyperbolic|SinZ6", Function = z => Complex.Sinh(z * z * z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ z^7", TexKey = "Hyperbolic|SinZ7", Function = z => Complex.Sinh(z * z * z * z * z * z * z),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z}", TexKey = "Hyperbolic|Sinh1Z1", Function = z => Complex.Sinh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^2}", TexKey = "Hyperbolic|Sinh1Z2", Function = z => Complex.Sinh(1 / (z * z)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^3}", TexKey = "Hyperbolic|Sinh1Z3", Function = z => Complex.Sinh(1 / (z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^4}", TexKey = "Hyperbolic|Sinh1Z4", Function = z => Complex.Sinh(1 / (z * z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^5}", TexKey = "Hyperbolic|Sinh1Z5", Function = z => Complex.Sinh(1 / (z * z * z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^6}", TexKey = "Hyperbolic|Sinh1Z6", Function = z => Complex.Sinh(1 / (z * z * z * z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^7}", TexKey = "Hyperbolic|Sinh1Z7", Function = z => Complex.Sinh(1 / (z * z * z * z * z * z * z)),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z+1}", TexKey = "Hyperbolic|Sinh1Z1P1", Function = z => Complex.Sinh(1 / (z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Sinh1Z2P1", Function = z => Complex.Sinh(1 / (z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^3+1}", TexKey = "Hyperbolic|Sinh1Z3P1", Function = z => Complex.Sinh(1 / (z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^4+1}", TexKey = "Hyperbolic|Sinh1Z4P1", Function = z => Complex.Sinh(1 / (z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^5+1}", TexKey = "Hyperbolic|Sinh1Z5P1", Function = z => Complex.Sinh(1 / (z * z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^6+1}", TexKey = "Hyperbolic|Sinh1Z6P1", Function = z => Complex.Sinh(1 / (z * z * z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^7+1}", TexKey = "Hyperbolic|Sinh1Z7P1", Function = z => Complex.Sinh(1 / (z * z * z * z * z * z * z + 1)),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sinh\ z", TexKey = "Hyperbolic|SinhZ1", Function = Complex.Sinh,
                },
                new ComplexFunction
                {
                    Label = @"sinh^2\ z", TexKey = "Hyperbolic|Sinh2Z1", Function = z => Z2(Complex.Sinh(z)),
                },
                new ComplexFunction
                {
                    Label = @"sinh^3\ z", TexKey = "Hyperbolic|Sinh3Z1", Function = z => Z3(Complex.Sinh(z)),
                },
                new ComplexFunction
                {
                    Label = @"sinh^4\ z", TexKey = "Hyperbolic|Sinh4Z1", Function = z => Z2(Z2(Complex.Sinh(z))),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z}", TexKey = "Hyperbolic|Sinh1Z1", Function = z => Complex.Sinh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"sinh^2\ \frac{1}{z}", TexKey = "Hyperbolic|Sinh21Z1", Function = z => Z2(Complex.Sinh(1 / z)),
                },
                new ComplexFunction
                {
                    Label = @"sinh^3\ \frac{1}{z}", TexKey = "Hyperbolic|Sinh31Z1", Function = z => Z3(Complex.Sinh(1 / z)),
                },
                new ComplexFunction
                {
                    Label = @"sinh^4\ \frac{1}{z}", TexKey = "Hyperbolic|Sinh41Z1", Function = z => Z2(Z2(Complex.Sinh(1 / z))),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sinh\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Sinh1Z2P1", Function = z => Complex.Sinh(1 / (z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sinh^2\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Sinh21Z2P1", Function = z => Z2(Complex.Sinh(1 / (z * z + 1))),
                },
                new ComplexFunction
                {
                    Label = @"sinh^3\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Sinh31Z2P1", Function = z => Z3(Complex.Sinh(1 / (z * z + 1))),
                },
                new ComplexFunction
                {
                    Label = @"sinh^4\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Sinh41Z2P1", Function = z => Z2(Z2(Complex.Sinh(1 / (z * z + 1)))),
                },

                // ----------------------------------------------------------------------------
                Separator,
            };
        }
    }
}
