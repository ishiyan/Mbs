using System;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions
{
    internal sealed class ComplexFunction
    {
        public string Label { get; set; }

        public Func<Complex, Complex> Function { get; set; }

        public double ReMin { get; set; } = -3;

        public double ReMax { get; set; } = 3;

        public double ImMin { get; set; } = -3;

        public double ImMax { get; set; } = 3;
    }
}
