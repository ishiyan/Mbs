using System.Collections.Generic;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of assorted complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetAssorted()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = @"\frac{(z^{2}-1)(z+2-i)^{2}}{(z^{2}+2-2i)}", TexKey = "Assorted|Claudio1",
                    Function = z =>
                    {
                        Complex zs = z + 2 - Complex.ImaginaryOne;
                        return (z * z - 1) * zs * zs / (z * z + 2 - 2 * Complex.ImaginaryOne);
                    },
                },
                new ComplexFunction
                {
                    // Claudio Rocchini's example function from http://en.wikipedia.org/wiki/File:Color_complex_plot.jpg
                    Label = @"\frac{(z^{2}-1)(z-2-i)^{2}}{(z^{2}+2+2i)}", TexKey = "Assorted|Claudio2",
                    Function = z =>
                    {
                        Complex zs = z - 2 - Complex.ImaginaryOne;
                        return (z * z - 1) * zs * zs / (z * z + 2 + 2 * Complex.ImaginaryOne);
                    },
                },
            };
        }
    }
}
