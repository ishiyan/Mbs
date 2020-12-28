using System;
using Mbs.Numerics;

namespace DomainColoring.ColorMaps
{
    internal sealed class ColorMap
    {
        public string Label { get; set; }

        public Func<Complex, int> Function { get; set; }
    }
}
