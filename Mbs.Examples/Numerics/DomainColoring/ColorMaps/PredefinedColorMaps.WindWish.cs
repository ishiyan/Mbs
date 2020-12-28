using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using Mbs.Numerics;

namespace DomainColoring.ColorMaps
{
    /// <summary>
    /// Implements Wind Wish color mapping.
    /// </summary>
    internal static partial class PredefinedColorMaps
    {
        /// <summary>
        /// Taken from
        /// http://www.windwish.net/2009/09/complex-function-visualization/.
        /// <para/>
        /// Transform complex numbers to HSV values. Get H(hue) value by finding the angle of z.
        /// Get S(saturation) value by tanh(|z|), use tanh to ensure S in [0,1].
        /// V is set to 1, but for special cases, 0 for infinities, 0.5 for NaNs.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int WindWish(Complex z)
        {
            double v = 1;
            if (z.IsInfinity)
            {
                v = 0d;
            }
            else if (z.IsNaN)
            {
                v = 0.5;
            }

            // Extract a phase 0 ≤ θ < 2π.
            double t = z.Argument;
            while (t < 0d)
            {
                t += Constants.TwoPi;
            }

            while (t >= Constants.TwoPi)
            {
                t -= Constants.TwoPi;
            }

            // The hue is determined by the phase.
            double h = t / Constants.TwoPi;

            // Extract a magnitude m ≥ 0.
            double m = Complex.Abs(z);

            // Determine saturation and value based on m.
            double s = Math.Tanh(m);

            return HsvToBrga32(h, s, v);
        }
    }
}
