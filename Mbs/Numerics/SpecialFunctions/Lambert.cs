namespace Mbs.Numerics
{
    using System;

    /// <summary>
    /// Contains assorted special functions.
    /// </summary>
    public static partial class SpecialFunctions
    {
        /// <summary>
        /// Computes the Lambert W function, which is the solution of the transcendental equation
        /// <para />
        /// W(x)∙℮ᵂ⁽ˣ⁾ = x.
        /// <para />
        /// The function appears in a number of contexts, including the solution of differential equations and the enumeration of trees.
        /// </summary>
        /// <param name="x">The argument, which must be greater than or equal to -1/e.</param>
        /// <returns>The value of the W(x).</returns>
        /// <seealso href="http://en.wikipedia.org/wiki/Lambert_W_function" />
        /// <seealso href="http://www.apmaths.uwo.ca/~djeffrey/Offprints/W-adv-cm.pdf"/>
        public static double LambertW(double x)
        {
            if (x < -Constants.InvE)
                return double.NaN; // throw new ArgumentOutOfRangeException("x");

            double w;
            if (x < 0d)
            {
                // this was commented out...
                double p = Constants.Sqrt2 * Math.Sqrt(1d + Constants.E * x);
                w = ((0.50000000254110366 + p * (2.077082690400045 + p * (1.4275043714348843 + p * 0.18169293085614638))) / (1d + p * (1.3257391022094323 + p * (0.43856420102046717 + p * 0.0265500411085822)))) - 1.5;
            }
            else if (x < 0.3)
            {
                w = (0.99999998958768466 + x * (2.0367969407614619 + x * (0.40989120652325517 - x * 0.055180135629034427))) / (1d + x * (3.036793688437895 + x * 1.9468487052703809));
            }
            else if (x < 2.3)
            {
                w = (6.91370020636232e-05 + x * (0.99891196507386126 + x * (1.6699417741103892 + x * (0.45463995685440306 + x * 0.0062805649158445319)))) / (1d + x * (2.661987382297955 + x * (1.6538505709215219 + x * 0.20277327219972122)));
            }
            else if (x < 10.3)
            {
                w = (0.01976952864697059 + x * (0.91255159181269541 + x * (0.45556098974402293 + x * (0.030507319010741406 + x * 7.9583704602728445e-05)))) / (1d + x * (1.2250936530860326 + x * (0.26576871286495957 + x * 0.0094435537295920469)));
            }
            else if (x < 25000.0)
            {
                double p = (x - 13000d) / 12000d;
                w = (7.463270151357106 + p * (11.012438386596848 + p * 3.8836808096898814)) / (1d + p * (1.3667593539934748 + p * (0.42189887531405906 - p * 0.0085760410974559369)));
            }
            else
            {
                double p1 = Math.Log(x);
                double q1 = 1d / p1;
                double p2 = Math.Log(p1);
                w = p1 - p2 + p2 * q1 * (1.0 + q1 * (0.5 * (p2 - 2.0) + q1 * (6d + p2 * (2d * p2 - 9.0))));
            }

            double f;
            do
            {
                double e = Math.Exp(w);
                f = (w * e - x) / ((w + 1d) * e);
                w -= f;
            }
            while (Math.Abs(f) > 1e-10);
            return w;
        }
    }
}
