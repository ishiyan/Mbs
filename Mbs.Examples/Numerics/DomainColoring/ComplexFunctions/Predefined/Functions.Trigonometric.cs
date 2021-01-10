using System.Collections.Generic;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of trigonometric complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetTrigonometric()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = @"sin\ z", TexKey = "Trigonometric|SinZ1", Function = Complex.Sin,
                },
                new ComplexFunction
                {
                    Label = @"cos\ z", TexKey = "Trigonometric|CosZ1", Function = Complex.Cos,
                },
                new ComplexFunction
                {
                    Label = @"tan\ z", TexKey = "Trigonometric|TanZ1", Function = Complex.Tan,
                },
                new ComplexFunction
                {
                    Label = @"cot\ z", TexKey = "Trigonometric|CotZ1", Function = Complex.Cot,
                },
                new ComplexFunction
                {
                    Label = @"sec\ z", TexKey = "Trigonometric|SecZ1", Function = Complex.Sec,
                },
                new ComplexFunction
                {
                    Label = @"csc\ z", TexKey = "Trigonometric|CscZ1", Function = Complex.Csc,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z}", TexKey = "Trigonometric|Sin1Z1", Function = z => Complex.Sin(1 / z),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = @"cos\ \frac{1}{z}", TexKey = "Trigonometric|Cos1Z1", Function = z => Complex.Cos(1 / z),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"tan\ \frac{1}{z}", TexKey = "Trigonometric|Tan1Z1", Function = z => Complex.Tan(1 / z),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"cot\ \frac{1}{z}", TexKey = "Trigonometric|Cot1Z1", Function = z => Complex.Cot(1 / z),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"sec\ \frac{1}{z}", TexKey = "Trigonometric|Sec1Z1", Function = z => Complex.Sec(1 / z),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },
                new ComplexFunction
                {
                    Label = @"csc\ \frac{1}{z}", TexKey = "Trigonometric|Csc1Z1", Function = z => Complex.Csc(1 / z),
                    ReMin = -C1Pi2, ReMax = C1Pi2, ImMin = -C1Pi2, ImMax = C1Pi2,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sin\ z", TexKey = "Trigonometric|SinZ1", Function = Complex.Sin,
                },
                new ComplexFunction
                {
                    Label = @"sin\ z^2", TexKey = "Trigonometric|SinZ2", Function = z => Complex.Sin(z * z),
                },
                new ComplexFunction
                {
                    Label = @"sin\ z^3", TexKey = "Trigonometric|SinZ3", Function = z => Complex.Sin(z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"sin\ z^4", TexKey = "Trigonometric|SinZ4", Function = z => Complex.Sin(z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"sin\ z^5", TexKey = "Trigonometric|SinZ5", Function = z => Complex.Sin(z * z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"sin\ z^6", TexKey = "Trigonometric|SinZ6", Function = z => Complex.Sin(z * z * z * z * z * z),
                },
                new ComplexFunction
                {
                    Label = @"sin\ z^7", TexKey = "Trigonometric|SinZ7", Function = z => Complex.Sin(z * z * z * z * z * z * z),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z}", TexKey = "Trigonometric|Sin1Z1", Function = z => Complex.Sin(1 / z),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^2}", TexKey = "Trigonometric|Sin1Z2", Function = z => Complex.Sin(1 / (z * z)),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^3}", TexKey = "Trigonometric|Sin1Z3", Function = z => Complex.Sin(1 / (z * z * z)),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^4}", TexKey = "Trigonometric|Sin1Z4", Function = z => Complex.Sin(1 / (z * z * z * z)),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^5}", TexKey = "Trigonometric|Sin1Z5", Function = z => Complex.Sin(1 / (z * z * z * z * z)),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^6}", TexKey = "Trigonometric|Sin1Z6", Function = z => Complex.Sin(1 / (z * z * z * z * z * z)),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^7}", TexKey = "Trigonometric|Sin1Z7", Function = z => Complex.Sin(1 / (z * z * z * z * z * z * z)),
                    ReMin = -C1Pi3, ReMax = C1Pi3, ImMin = -C1Pi3, ImMax = C1Pi3,
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z+1}", TexKey = "Trigonometric|Sin1Z1P1", Function = z => Complex.Sin(1 / (z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Sin1Z2P1", Function = z => Complex.Sin(1 / (z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^3+1}", TexKey = "Trigonometric|Sin1Z3P1", Function = z => Complex.Sin(1 / (z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^4+1}", TexKey = "Trigonometric|Sin1Z4P1", Function = z => Complex.Sin(1 / (z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^5+1}", TexKey = "Trigonometric|Sin1Z5P1", Function = z => Complex.Sin(1 / (z * z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^6+1}", TexKey = "Trigonometric|Sin1Z6P1", Function = z => Complex.Sin(1 / (z * z * z * z * z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^7+1}", TexKey = "Trigonometric|Sin1Z7P1", Function = z => Complex.Sin(1 / (z * z * z * z * z * z * z + 1)),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sin\ z", TexKey = "Trigonometric|SinZ1", Function = Complex.Sin,
                },
                new ComplexFunction
                {
                    Label = @"sin^2\ z", TexKey = "Trigonometric|Sin2Z1", Function = z => Z2(Complex.Sin(z)),
                },
                new ComplexFunction
                {
                    Label = @"sin^3\ z", TexKey = "Trigonometric|Sin3Z1", Function = z => Z3(Complex.Sin(z)),
                },
                new ComplexFunction
                {
                    Label = @"sin^4\ z", TexKey = "Trigonometric|Sin4Z1", Function = z => Z2(Z2(Complex.Sin(z))),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z}", TexKey = "Trigonometric|Sin1Z1", Function = z => Complex.Sin(1 / z),
                },
                new ComplexFunction
                {
                    Label = @"sin^2\ \frac{1}{z}", TexKey = "Trigonometric|Sin21Z1", Function = z => Z2(Complex.Sin(1 / z)),
                },
                new ComplexFunction
                {
                    Label = @"sin^3\ \frac{1}{z}", TexKey = "Trigonometric|Sin31Z1", Function = z => Z3(Complex.Sin(1 / z)),
                },
                new ComplexFunction
                {
                    Label = @"sin^4\ \frac{1}{z}", TexKey = "Trigonometric|Sin41Z1", Function = z => Z2(Z2(Complex.Sin(1 / z))),
                },

                // ----------------------------------------------------------------------------
                Separator,
                new ComplexFunction
                {
                    Label = @"sin\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Sin1Z2P1", Function = z => Complex.Sin(1 / (z * z + 1)),
                },
                new ComplexFunction
                {
                    Label = @"sin^2\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Sin21Z2P1", Function = z => Z2(Complex.Sin(1 / (z * z + 1))),
                },
                new ComplexFunction
                {
                    Label = @"sin^3\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Sin31Z2P1", Function = z => Z3(Complex.Sin(1 / (z * z + 1))),
                },
                new ComplexFunction
                {
                    Label = @"sin^4\ \frac{1}{z^2+1}", TexKey = "Trigonometric|Sin41Z2P1", Function = z => Z2(Z2(Complex.Sin(1 / (z * z + 1)))),
                },

                // ----------------------------------------------------------------------------
                Separator,
            };
        }
    }
}
