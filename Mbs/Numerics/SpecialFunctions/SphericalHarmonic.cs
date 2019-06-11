using System;

namespace Mbs.Numerics
{
    /// <summary>
    /// Contains assorted special functions.
    /// </summary>
    public static partial class SpecialFunctions
    {
        /// <summary>
        /// Represented in a system of spherical coordinates, Laplace's spherical harmonics <c>Yˡᵐ(θ,φ)</c> are a specific set of spherical harmonics that forms an orthogonal system.
        /// <para><c>Yˡᵐ = √[(2l+1)/4π (l-m)!/(l+m)!] Pˡᵐ</c>; in terms of the renormalized <c>Peˡᵐ</c>, this is <c>Yˡᵐ = √[(2l+1)/4π] Peˡᵐ</c>.</para>
        /// </summary>
        /// <param name="l">The order, which must be non-negative.</param>
        /// <param name="m">The sub-order, which must lie between -l and l inclusive.</param>
        /// <param name="theta">The azimuthal angle <c>θ</c>. This angle is usually expressed as between -π/2 and +π/2, with positive values representing the upper hemisphere and negative values representing the lower hemisphere.</param>
        /// <param name="phi">The cylindrical angle <c>φ</c>. This angle is usually expressed as between 0 and 2π, measured counter-clockwise (as seen from above) from the positive x-axis. It is also possible to use negative values to represent clockwise movement.</param>
        /// <returns>The value of <c>Yˡᵐ(θ,φ)</c>.</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Spherical_harmonics" />
        public static Complex SphericalHarmonic(int l, int m, double theta, double phi)
        {
            if (l < 0)
                return Complex.NaN;
            if (m > l || m < -l)
                return Complex.NaN;
            if (m < 0)
            {
                Complex y = SphericalHarmonic(l, -m, theta, phi);
                if ((m % 2) != 0)
                    y = -y;
                return y.Conjugate;
            }

            double lp = Math.Sqrt((2 * l + 1) / (4d * Constants.Pi)) * LegendrePe(l, m, Math.Cos(theta));
            double mp = m * phi;
            return new Complex(lp * Cos(mp, 0d), lp * Sin(mp, 0d));
        }
    }
}
