using System.Collections.Generic;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of elementary rational complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetElementary()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = "z", TexKey = "Elementary|Z1", Function = z => z,
                },
                new ComplexFunction
                {
                    Label = "z^2", TexKey = "Elementary|Z2", Function = Z2,
                },
                new ComplexFunction
                {
                    Label = "z^3", TexKey = "Elementary|Z3", Function = Z3,
                },
                new ComplexFunction
                {
                    Label = "z^4", TexKey = "Elementary|Z4", Function = z => Z2(Z2(z)),
                },
                new ComplexFunction
                {
                    Label = "z^5", TexKey = "Elementary|Z5", Function = z => Z2(Z2(z)) * z,
                },
                new ComplexFunction
                {
                    Label = "z^6", TexKey = "Elementary|Z6", Function = z => Z2(Z3(z)),
                },
                new ComplexFunction
                {
                    Label = "z^7", TexKey = "Elementary|Z7", Function = z => Z2(Z3(z)) * z,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"\frac{1}{z}", TexKey = "Elementary|1Z1", Function = z => 1 / z,
                },
                new ComplexFunction
                {
                    Label = @"\frac{1}{z^2}", TexKey = "Elementary|1Z2", Function = z => 1 / Z2(z),
                },
                new ComplexFunction
                {
                    Label = @"\frac{1}{z^3}", TexKey = "Elementary|1Z3", Function = z => 1 / Z3(z),
                },
                new ComplexFunction
                {
                    Label = @"\frac{1}{z^4}", TexKey = "Elementary|1Z4", Function = z => 1 / Z2(Z2(z)),
                },
                new ComplexFunction
                {
                    Label = @"\frac{1}{z^5}", TexKey = "Elementary|1Z5", Function = z => 1 / (Z2(Z2(z)) * z),
                },
                new ComplexFunction
                {
                    Label = @"\frac{1}{z^6}", TexKey = "Elementary|1Z6", Function = z => 1 / Z2(Z3(z)),
                },
                new ComplexFunction
                {
                    Label = @"\frac{1}{z^7}", TexKey = "Elementary|1Z7", Function = z => 1 / (Z2(Z3(z)) * z),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = "z+1", TexKey = "Elementary|Z1P1", Function = z => z + 1,
                },
                new ComplexFunction
                {
                    Label = "z^2+1", TexKey = "Elementary|Z2P1", Function = z => Z2(z) + 1,
                },
                new ComplexFunction
                {
                    Label = "z^3+1", TexKey = "Elementary|Z3P1", Function = z => Z3(z) + 1,
                },
                new ComplexFunction
                {
                    Label = "z^4+1", TexKey = "Elementary|Z4P1", Function = z => Z2(Z2(z)) + 1,
                },
                new ComplexFunction
                {
                    Label = "z^5+1", TexKey = "Elementary|Z5P1", Function = z => Z2(Z2(z)) * z + 1,
                },
                new ComplexFunction
                {
                    Label = "z^6+1", TexKey = "Elementary|Z6P1", Function = z => Z2(Z3(z)) + 1,
                },
                new ComplexFunction
                {
                    Label = "z^7+1", TexKey = "Elementary|Z7P1", Function = z => Z2(Z3(z)) * z + 1,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = "\frac{1}{z+1}", TexKey = "Elementary|1Z1P1", Function = z => 1 / (z + 1),
                },
                new ComplexFunction
                {
                    Label = "\frac{1}{z^2+1}", TexKey = "Elementary|1Z2P1", Function = z => 1 / (Z2(z) + 1),
                },
                new ComplexFunction
                {
                    Label = "\frac{1}{z^3+1}", TexKey = "Elementary|1Z3P1", Function = z => 1 / (Z3(z) + 1),
                },
                new ComplexFunction
                {
                    Label = "\frac{1}{z^4+1}", TexKey = "Elementary|1Z4P1", Function = z => 1 / (Z2(Z2(z)) + 1),
                },
                new ComplexFunction
                {
                    Label = "\frac{1}{z^5+1}", TexKey = "Elementary|1Z5P1", Function = z => 1 / (Z2(Z2(z)) * z + 1),
                },
                new ComplexFunction
                {
                    Label = "\frac{1}{z^6+1}", TexKey = "Elementary|1Z6P1", Function = z => 1 / (Z2(Z3(z)) + 1),
                },
                new ComplexFunction
                {
                    Label = "\frac{1}{z^7+1}", TexKey = "Elementary|1Z7P1", Function = z => 1 / (Z2(Z3(z)) * z + 1),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"\sqrt{z}", TexKey = "Elementary|Sqrt2Z", Function = Complex.Sqrt,
                },
                new ComplexFunction
                {
                    Label = @"\sqrt[3]{z}", TexKey = "Elementary|Sqrt3Z", Function = z => Complex.Pow(z, 1d / 3),
                },
                new ComplexFunction
                {
                    Label = @"\sqrt[4]{z}", TexKey = "Elementary|Sqrt4Z", Function = z => Complex.Pow(z, 1d / 4),
                },
                new ComplexFunction
                {
                    Label = @"\sqrt[5]{z}", TexKey = "Elementary|Sqrt5Z", Function = z => Complex.Pow(z, 1d / 5),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"\sqrt{1/z}", TexKey = "Elementary|Sqrt21Z", Function = z => Complex.Sqrt(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"\sqrt[3]{1/z}", TexKey = "Elementary|Sqrt31Z", Function = z => Complex.Pow(1 / z, 1d / 3),
                },
                new ComplexFunction
                {
                    Label = @"\sqrt[4]{1/z}", TexKey = "Elementary|Sqrt41Z", Function = z => Complex.Pow(1 / z, 1d / 4),
                },
                new ComplexFunction
                {
                    Label = @"\sqrt[5]{1/z}", TexKey = "Elementary|Sqrt51Z", Function = z => Complex.Pow(1 / z, 1d / 5),
                },
            };
        }
    }
}
