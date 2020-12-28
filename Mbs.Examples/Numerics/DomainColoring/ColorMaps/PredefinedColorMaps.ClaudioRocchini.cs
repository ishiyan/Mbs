using System.Runtime;
using System.Runtime.CompilerServices;
using Mbs.Numerics;

namespace DomainColoring.ColorMaps
{
    /// <summary>
    /// Implements Claudio Rocchini color mapping.
    /// </summary>
    internal static partial class PredefinedColorMaps
    {
        /// <summary>
        /// Claudio Rocchini used this color mapping in his image at
        /// http://en.wikipedia.org/wiki/File:Color_complex_plot.jpg
        /// for the Wikipedia article on "Complex Analysis".
        /// <para/>
        /// Determining the hue: get the phase from the ComplexMath.Arg
        /// function and express it as a fraction of 2π.
        /// <para/>
        /// Determining the saturation: we need our algorithm to fade to black or white near
        /// the desired contour line values, but keep those regions small enough that the
        /// figure does not become "washed out" with large regions devoid of color.
        /// Notice that the variables p and q measure the distance from and to the nearest
        /// contour lines. A naive approach would be to map those quantities linearly into s
        /// and v, but that produces large regions that are nearly white or nearly black instead
        /// of sharply defined contour lines. (Try it for yourself!) Rocchini's trick for avoiding
        /// that problem is to use the variables p1 and q1 in place of p and q. p1 and q1 are
        /// produced from p and q via a simple function <c>1 - (1-x)³</c> that preserves ordering
        /// (that is, as the input rises from 0 to 1, the output also rises from 0 to 1), but stays
        /// closer to one over a larger ranger (the output is already ~0.9 by the time the input
        /// reaches ~0.5). This ensures that our contour lines are thin.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ClaudioRocchini(Complex z)
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
                r1 *= Constants.E;
            }

            double r = (m - r0) / (r1 - r0);

            // This puts contour lines at 0, 1, e, e², e³, …

            // Determine saturation and value based on r.
            // p and q are complementary distances from a contour line.
            double p = r < 0.5 ? 2d * r : 2d * (1d - r);
            double q = 1d - p;

            // Only let p and q go to zero very close to zero; otherwise they should stay nearly 1.
            // This keep the contour lines from getting thick.
            double p1 = 1d - q * q * q;
            double q1 = 1d - p * p * p;

            // Adjust saturation and v value from p1 and q1.
            double s = 0.4 + 0.6 * p1;
            double v = 0.6 + 0.4 * q1;

            return HsvToBrga32(h, s, v);
        }
    }
}
