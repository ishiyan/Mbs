using System.Collections.Generic;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of inverse trigonometric complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetTrigonometricInverse()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = @"arcsin\ z", TexKey = "Trigonometric|AsinZ1", Function = Complex.Asin,
                },
                new ComplexFunction
                {
                    Label = @"arccos\ z", TexKey = "Trigonometric|AcosZ1", Function = Complex.Acos,
                },
                new ComplexFunction
                {
                    Label = @"arctan\ z", TexKey = "Trigonometric|AtanZ1", Function = Complex.Atan,
                },
                new ComplexFunction
                {
                    Label = @"arccot\ z", TexKey = "Trigonometric|AcotZ1", Function = Complex.Acot,
                },
                new ComplexFunction
                {
                    Label = @"arcsec\ z", TexKey = "Trigonometric|AsecZ1", Function = Complex.Asec,
                },
                new ComplexFunction
                {
                    Label = @"arccsc\ z", TexKey = "Trigonometric|AcscZ1", Function = Complex.Acsc,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z}", TexKey = "Trigonometric|Asin1Z1", Function = z => Complex.Asin(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arccos\ \frac{1}{z}", TexKey = "Trigonometric|Acos1Z1", Function = z => Complex.Acos(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arctan\ \frac{1}{z}", TexKey = "Trigonometric|Atan1Z1", Function = z => Complex.Atan(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arccot\ \frac{1}{z}", TexKey = "Trigonometric|Acot1Z1", Function = z => Complex.Acot(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arcsec\ \frac{1}{z}", TexKey = "Trigonometric|Asec1Z1", Function = z => Complex.Asec(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arccsc\ \frac{1}{z}", TexKey = "Trigonometric|Acsc1Z1", Function = z => Complex.Acsc(1 / z),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsin\ z", TexKey = "Trigonometric|AsinZ1", Function = Complex.Asin,
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ z^2", TexKey = "Trigonometric|AsinZ2", Function = z => Complex.Asin(z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ z^3", TexKey = "Trigonometric|AsinZ3", Function = z => Complex.Asin(z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ z^4", TexKey = "Trigonometric|AsinZ4", Function = z => Complex.Asin(z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ z^5", TexKey = "Trigonometric|AsinZ5", Function = z => Complex.Asin(z * z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ z^6", TexKey = "Trigonometric|AsinZ6", Function = z => Complex.Asin(z * z * z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ z^7", TexKey = "Trigonometric|AsinZ7", Function = z => Complex.Asin(z * z * z * z * z * z * z),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z}", TexKey = "Trigonometric|Asin1Z1", Function = z => Complex.Asin(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^2}", TexKey = "Trigonometric|Asin1Z2", Function = z => Complex.Asin(1 / (z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^3}", TexKey = "Trigonometric|Asin1Z3", Function = z => Complex.Asin(1 / (z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^4}", TexKey = "Trigonometric|Asin1Z4", Function = z => Complex.Asin(1 / (z * z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^5}", TexKey = "Trigonometric|Asin1Z5", Function = z => Complex.Asin(1 / (z * z * z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^6}", TexKey = "Trigonometric|Asin1Z6", Function = z => Complex.Asin(1 / (z * z * z * z * z * z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^7}", TexKey = "Trigonometric|Asin1Z7", Function = z => Complex.Asin(1 / (z * z * z * z * z * z * z)),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z+1}", TexKey = "Trigonometric|Asin1Z1P1", Function = z => Complex.Asin(1 / (z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Asin1Z2P1", Function = z => Complex.Asin(1 / (z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^3+1}", TexKey = "Trigonometric|Asin1Z3P1", Function = z => Complex.Asin(1 / (z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^4+1}", TexKey = "Trigonometric|Asin1Z4P1", Function = z => Complex.Asin(1 / (z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^5+1}", TexKey = "Trigonometric|Asin1Z5P1", Function = z => Complex.Asin(1 / (z * z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^6+1}", TexKey = "Trigonometric|Asin1Z6P1", Function = z => Complex.Asin(1 / (z * z * z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^7+1}", TexKey = "Trigonometric|Asin1Z7P1", Function = z => Complex.Asin(1 / (z * z * z * z * z * z * z + 1)),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsin\ z", TexKey = "Trigonometric|AsinZ1", Function = Complex.Asin,
                },
                new ComplexFunction
                {
                    Label = @"arcsin^2\ z", TexKey = "Trigonometric|Asin2Z1", Function = z => Z2(Complex.Asin(z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin^3\ z", TexKey = "Trigonometric|Asin3Z1", Function = z => Z3(Complex.Asin(z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin^4\ z", TexKey = "Trigonometric|Asin4Z1", Function = z => Z2(Z2(Complex.Asin(z))),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z}", TexKey = "Trigonometric|Asin1Z1", Function = z => Complex.Asin(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"arcsin^2\ \frac{1}{z}", TexKey = "Trigonometric|Asin21Z1", Function = z => Z2(Complex.Asin(1 / z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin^3\ \frac{1}{z}", TexKey = "Trigonometric|Asin31Z1", Function = z => Z3(Complex.Asin(1 / z)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin^4\ \frac{1}{z}", TexKey = "Trigonometric|Asin41Z1", Function = z => Z2(Z2(Complex.Asin(1 / z))),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"arcsin\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Asin1Z2P1", Function = z => Complex.Asin(1 / (z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"arcsin^2\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Asin21Z2P1", Function = z => Z2(Complex.Asin(1 / (z * z + 1))),
                },
                new ComplexFunction
                {
                    Label = @"arcsin^3\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Asin31Z2P1", Function = z => Z3(Complex.Asin(1 / (z * z + 1))),
                },
                new ComplexFunction
                {
                    Label = @"arcsin^4\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Asin41Z2P1", Function = z => Z2(Z2(Complex.Asin(1 / (z * z + 1)))),
                },

                // ----------------------------------------------------------------------------
                Separator,
            };
        }
    }
}
