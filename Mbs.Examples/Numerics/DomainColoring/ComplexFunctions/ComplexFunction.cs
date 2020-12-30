using System;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions
{
    internal sealed class ComplexFunction
    {
        public string Label { get; set; }

        public Func<Complex, Complex> Function { get; set; }

        public double ReMin { get; set; } = -Constants.Pi;

        public double ReMax { get; set; } = Constants.Pi;

        public double ImMin { get; set; } = -Constants.Pi;

        public double ImMax { get; set; } = Constants.Pi;

        public double CurrentReMin { get; set; }

        public double CurrentReMax { get; set; }

        public double CurrentImMin { get; set; }

        public double CurrentImMax { get; set; }

        public void Reset()
        {
            CurrentReMin = ReMin;
            CurrentReMax = ReMax;
            CurrentImMin = ImMin;
            CurrentImMax = ImMax;
        }
    }
}
