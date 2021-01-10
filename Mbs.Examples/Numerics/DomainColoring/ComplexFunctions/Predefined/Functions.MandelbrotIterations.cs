using System.Collections.Generic;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of Mandelbrot iterations.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetMandelbrotIterations()
        {
            // https://en.wikipedia.org/wiki/Mandelbrot_set
            return new[]
            {
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 0", TexKey = "Mandelbrot|000", Function = z => MandelbrotIterations(z, 0),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 1", TexKey = "Mandelbrot|001", Function = z => MandelbrotIterations(z, 1),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 2", TexKey = "Mandelbrot|002", Function = z => MandelbrotIterations(z, 2),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 3", TexKey = "Mandelbrot|003", Function = z => MandelbrotIterations(z, 3),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 4", TexKey = "Mandelbrot|004", Function = z => MandelbrotIterations(z, 4),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 5", TexKey = "Mandelbrot|005", Function = z => MandelbrotIterations(z, 5),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 6", TexKey = "Mandelbrot|006", Function = z => MandelbrotIterations(z, 6),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 7", TexKey = "Mandelbrot|007", Function = z => MandelbrotIterations(z, 7),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 8", TexKey = "Mandelbrot|008", Function = z => MandelbrotIterations(z, 8),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 9", TexKey = "Mandelbrot|009", Function = z => MandelbrotIterations(z, 9),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 10", TexKey = "Mandelbrot|010", Function = z => MandelbrotIterations(z, 10),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 20", TexKey = "Mandelbrot|020", Function = z => MandelbrotIterations(z, 20),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 30", TexKey = "Mandelbrot|030", Function = z => MandelbrotIterations(z, 30),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 40", TexKey = "Mandelbrot|040", Function = z => MandelbrotIterations(z, 40),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 50", TexKey = "Mandelbrot|050", Function = z => MandelbrotIterations(z, 50),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 60", TexKey = "Mandelbrot|060", Function = z => MandelbrotIterations(z, 60),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 70", TexKey = "Mandelbrot|070", Function = z => MandelbrotIterations(z, 70),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 80", TexKey = "Mandelbrot|080", Function = z => MandelbrotIterations(z, 80),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 90", TexKey = "Mandelbrot|090", Function = z => MandelbrotIterations(z, 90),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 100", TexKey = "Mandelbrot|100", Function = z => MandelbrotIterations(z, 100),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 200", TexKey = "Mandelbrot|200", Function = z => MandelbrotIterations(z, 200),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
                new ComplexFunction
                {
                    Label = "z → z² + c, iterations: 300", TexKey = "Mandelbrot|300", Function = z => MandelbrotIterations(z, 300),
                    ReMin = -2, ReMax = 1, ImMin = -1, ImMax = 1,
                },
            };
        }

        private static Complex MandelbrotIterations(Complex c, int iterations)
        {
            var z = Complex.Zero;
            do
            {
                z = z * z + c;
            }
            while (iterations-- >= 0);

            return z;
        }
    }
}
