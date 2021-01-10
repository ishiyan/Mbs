using System.Collections.Generic;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of inverse hyperbolic complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetHyperbolicInverse()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = @"arcsinh\ z", TexKey = "Hyperbolic|AsinhZ1", Function = Complex.Asinh,
                },
                new ComplexFunction
                {
                    Label = @"arccosh\ z", TexKey = "Hyperbolic|AcoshZ1", Function = Complex.Acosh,
                },
                new ComplexFunction
                {
                    Label = @"arctanh\ z", TexKey = "Hyperbolic|AtanhZ1", Function = Complex.Atanh,
                },
                new ComplexFunction
                {
                    Label = @"arccoth\ z", TexKey = "Hyperbolic|AcothZ1", Function = Complex.Acoth,
                },
                new ComplexFunction
                {
                    Label = @"arcsech\ z", TexKey = "Hyperbolic|AsechZ1", Function = Complex.Asech,
                },
                new ComplexFunction
                {
                    Label = @"arccsch\ z", TexKey = "Hyperbolic|AcschZ1", Function = Complex.Acsch,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z}", TexKey = "Hyperbolic|Asinh1Z1", Function = z => Complex.Asinh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arccosh\ \frac{1}{z}", TexKey = "Hyperbolic|Acosh1Z1", Function = z => Complex.Acosh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arctanh\ \frac{1}{z}", TexKey = "Hyperbolic|Atanh1Z1", Function = z => Complex.Atanh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arccoth\ \frac{1}{z}", TexKey = "Hyperbolic|Acoth1Z1", Function = z => Complex.Acoth(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arcsech\ \frac{1}{z}", TexKey = "Hyperbolic|Asech1Z1", Function = z => Complex.Asech(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arccsch\ \frac{1}{z}", TexKey = "Hyperbolic|Acsch1Z1", Function = z => Complex.Acsch(1 / z),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsinh\ z", TexKey = "Hyperbolic|AsinZ1", Function = Complex.Asinh,
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ z^2", TexKey = "Hyperbolic|AsinZ2", Function = z => Complex.Asinh(z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ z^3", TexKey = "Hyperbolic|AsinZ3", Function = z => Complex.Asinh(z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ z^4", TexKey = "Hyperbolic|AsinZ4", Function = z => Complex.Asinh(z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ z^5", TexKey = "Hyperbolic|AsinZ5", Function = z => Complex.Asinh(z * z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ z^6", TexKey = "Hyperbolic|AsinZ6", Function = z => Complex.Asinh(z * z * z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ z^7", TexKey = "Hyperbolic|AsinZ7", Function = z => Complex.Asinh(z * z * z * z * z * z * z),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z}", TexKey = "Hyperbolic|Asinh1Z1", Function = z => Complex.Asinh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^2}", TexKey = "Hyperbolic|Asinh1Z2", Function = z => Complex.Asinh(1 / (z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^3}", TexKey = "Hyperbolic|Asinh1Z3", Function = z => Complex.Asinh(1 / (z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^4}", TexKey = "Hyperbolic|Asinh1Z4", Function = z => Complex.Asinh(1 / (z * z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^5}", TexKey = "Hyperbolic|Asinh1Z5", Function = z => Complex.Asinh(1 / (z * z * z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^6}", TexKey = "Hyperbolic|Asinh1Z6", Function = z => Complex.Asinh(1 / (z * z * z * z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^7}", TexKey = "Hyperbolic|Asinh1Z7", Function = z => Complex.Asinh(1 / (z * z * z * z * z * z * z)),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z+1}", TexKey = "Hyperbolic|Asinh1Z1P1", Function = z => Complex.Asinh(1 / (z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Asinh1Z2P1", Function = z => Complex.Asinh(1 / (z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^3+1}", TexKey = "Hyperbolic|Asinh1Z3P1", Function = z => Complex.Asinh(1 / (z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^4+1}", TexKey = "Hyperbolic|Asinh1Z4P1", Function = z => Complex.Asinh(1 / (z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^5+1}", TexKey = "Hyperbolic|Asinh1Z5P1", Function = z => Complex.Asinh(1 / (z * z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^6+1}", TexKey = "Hyperbolic|Asinh1Z6P1", Function = z => Complex.Asinh(1 / (z * z * z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^7+1}", TexKey = "Hyperbolic|Asinh1Z7P1", Function = z => Complex.Asinh(1 / (z * z * z * z * z * z * z + 1)),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsinh\ z", TexKey = "Hyperbolic|AsinhZ1", Function = Complex.Asinh,
                },
                new ComplexFunction
                {
                    Label = @"arcsinh^2\ z", TexKey = "Hyperbolic|Asinh2Z1", Function = z => Z2(Complex.Asinh(z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh^3\ z", TexKey = "Hyperbolic|Asinh3Z1", Function = z => Z3(Complex.Asinh(z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh^4\ z", TexKey = "Hyperbolic|Asinh4Z1", Function = z => Z2(Z2(Complex.Asinh(z))),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z}", TexKey = "Hyperbolic|Asinh1Z1", Function = z => Complex.Asinh(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh^2\ \frac{1}{z}", TexKey = "Hyperbolic|Asinh21Z1", Function = z => Z2(Complex.Asinh(1 / z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh^3\ \frac{1}{z}", TexKey = "Hyperbolic|Asinh31Z1", Function = z => Z3(Complex.Asinh(1 / z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh^4\ \frac{1}{z}", TexKey = "Hyperbolic|Asinh41Z1", Function = z => Z2(Z2(Complex.Asinh(1 / z))),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsinh\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Asinh1Z2P1", Function = z => Complex.Asinh(1 / (z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh^2\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Asinh21Z2P1", Function = z => Z2(Complex.Asinh(1 / (z * z + 1))),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh^3\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Asinh31Z2P1", Function = z => Z3(Complex.Asinh(1 / (z * z + 1))),
                },
                new ComplexFunction
                {
                    Label = @"arcsinh^4\ \frac{1}{z^2+1}", TexKey = "Hyperbolic|Asinh41Z2P1", Function = z => Z2(Z2(Complex.Asinh(1 / (z * z + 1)))),
                },

                // ----------------------------------------------------------------------------
                Separator,
            };
        }
    }
}
