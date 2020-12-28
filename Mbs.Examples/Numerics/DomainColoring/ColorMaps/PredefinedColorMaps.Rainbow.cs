using System.Runtime;
using System.Runtime.CompilerServices;
using Mbs.Numerics;

namespace DomainColoring.ColorMaps
{
    /// <summary>
    /// Implements rainbow color mapping.
    /// </summary>
    internal static partial class PredefinedColorMaps
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Rainbow(Complex z)
        {
            if (z.IsInfinity || z.IsNaN)
            {
                return 0;
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

            // Map the magnitude logarithmically into the repeating interval 0 < r < 1.
            // This is essentially where we are between contour lines.
            double r0 = 0d;
            double r1 = 0.1;
            while (m > r1)
            {
                r0 = r1;
                r1 *= Constants.GoldenRatio;
            }

            double r = (m - r0) / (r1 - r0);

            // This puts contour lines at 0, 1, e, e², e³, …

            // Determine saturation and value based on r.
            // p is a complimentary distance from a contour line.
            double p = r < 0.5 ? 2 * r : 2 * (1 - r);
            p = 1 - p;

            // Only let p go very fast to zero; this keep the contour lines from getting thick.
            p *= p;
            p *= p;

            // Adjust saturation (and value = saturation) from p.
            double s = 1 - 0.3 * p * p * p;

            return HsvToBrga32(h, s, s);
        }
    }
}
