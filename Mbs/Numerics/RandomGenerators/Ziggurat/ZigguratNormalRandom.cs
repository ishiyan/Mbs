using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// A fast Gaussian distribution sampler for .Net by Colin Green, 11/09/2011
    /// <para/>
    /// An implementation of the Ziggurat algorithm for random sampling from a Gaussian distribution. See:
    /// <para/>
    /// (1) Wikipedia:Ziggurat algorithm, http://en.wikipedia.org/wiki/Ziggurat_algorithm
    /// <para/>
    /// (2) The Ziggurat Method for Generating Random Variables, George Marsaglia and Wai Wan Tsang, http://www.jstatsoft.org/v05/i08/paper
    /// <para/>
    /// (3) An Improved Ziggurat Method to Generate Normal Random Samples, Jurgen A Doornik, http://www.doornik.com/research/ziggurat.pdf.
    /// </summary>
    public class ZigguratNormalRandom : INormalRandomGenerator
    {
        /// <summary>
        /// Number of blocks.
        /// </summary>
        private const int BlockCount = 128;

        /// <summary>
        /// Right hand x coord of the base rectangle, thus also the left hand x coord of the tail (pre-determined/computed for 128 blocks).
        /// </summary>
        private const double BaseRectangleRightX = 3.442619855899;

        /// <summary>
        /// Area of each rectangle, pre-determined/computed for 128 blocks.
        /// </summary>
        private const double RectangleArea = 9.91256303526217e-3;

        /// <summary>
        /// Scale factor for converting a UInt with range [0,0xffffffff] to a double with range [0,1].
        /// </summary>
        private const double UIntToU = 1.0 / uint.MaxValue;

        private readonly IRandomGenerator rng;
        private readonly Func<double> sampleFn;

        /// <summary>
        /// The x coordinaate of the top-right position ox rectangle i.
        /// </summary>
        private readonly double[] xRight;

        /// <summary>
        /// The y coordinaate of the top-right position ox rectangle i.
        /// </summary>
        private readonly double[] yTop;

        /// <summary>
        /// The proportion of each segment that is entirely within the distribution, expressed
        /// as uint where a value of 0 indicates 0% and uint.MaxValue 100%. Expressing this as
        /// an integer allows some floating points operations to be replaced with integer ones.
        /// </summary>
        private readonly uint[] xComp;

        /// <summary>
        /// Rectangle area divided by the height of B0. This is *not* the same as xRight[i]
        /// because the area of B0 is RectangleArea minus the area of the distribution tail.
        /// </summary>
        private readonly double areaDividedByHeightB0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZigguratNormalRandom"/> class.
        /// </summary>
        /// <param name="rng">The uniform random generator.</param>
        /// <param name="mean">The mean value of the normal distribution.</param>
        /// <param name="stdDev">The standard deviation of the normal distribution.</param>
        public ZigguratNormalRandom(IRandomGenerator rng, double mean = 0, double stdDev = 1)
        {
            this.rng = rng;

            // We predetermine which of these four function variants to use at construction time,
            // thus avoiding the two condition tests on each invocation of Sample().
            if (Math.Abs(mean) <= double.Epsilon)
            {
                if (Math.Abs(stdDev - 1) <= double.Epsilon)
                {
                    sampleFn = NextDoubleStandard;
                }
                else
                {
                    sampleFn = () => NextDoubleStandard() * stdDev;
                }
            }
            else
            {
                if (Math.Abs(stdDev - 1) <= double.Epsilon)
                {
                    sampleFn = () => mean + NextDoubleStandard();
                }
                else
                {
                    sampleFn = () => mean + (NextDoubleStandard() * stdDev);
                }
            }

            // Initialise rectangle position data.
            // xRight[i] and yTop[i] describe the top-right position ox Box i.
            // We add one to the length of xRight so that we have an entry at xRight[BlockCount],
            // this avoids having to do a special case test when sampling from the top box.
            xRight = new double[BlockCount + 1];
            yTop = new double[BlockCount];

            // Determine top right position of the base rectangle/box (the rectangle with the Gaussian tale attached).
            // We call this Box 0 or B0 for short. xRight[0] also describes the right-hand edge of B1.
            xRight[0] = BaseRectangleRightX;
            yTop[0] = GaussianPdfDenorm(BaseRectangleRightX);

            // Useful precomputed values.
            areaDividedByHeightB0 = RectangleArea / yTop[0];
            xComp = new uint[BlockCount];

            Reset();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ZigguratNormalRandom"/> can be reset, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public bool CanReset => rng.CanReset;

        /// <summary>
        /// Resets the random number generator, so that it produces the same random number sequence again.
        /// </summary>
        public void Reset()
        {
            rng.Reset();

            // Determine top right position of the base rectangle/box (the rectangle with the Gaussian tale attached).
            // We call this Box 0 or B0 for short. xRight[0] also describes the right-hand edge of B1.
            xRight[0] = BaseRectangleRightX;
            yTop[0] = GaussianPdfDenorm(BaseRectangleRightX);

            // The next box (B1) has a right hand X edge the same as B0.
            // B1's height is the box area divided by its width, hence B1 has a smaller height
            // than B0 because B0's total area includes the attached distribution tail.
            xRight[1] = BaseRectangleRightX;
            yTop[1] = yTop[0] + RectangleArea / BaseRectangleRightX;

            // Calculate positions of all remaining rectangles.
            for (int i = 2; i < BlockCount; ++i)
            {
                xRight[i] = GaussianPdfDenormInv(yTop[i - 1]);
                yTop[i] = yTop[i - 1] + (RectangleArea / xRight[i]);
            }

            // For completeness we define the right-hand edge of a notional box 6 as being zero (a box with no area).
            xRight[BlockCount] = 0.0;

            // Special case for base box. xComp[0] stores the area of B0 as a proportion of BaseRectangleRightX
            // (recalling that all segments have area RectangleArea, but that the base segment is the combination of B0 and the distribution tail).
            // Thus xComp[0] is the probability that a sample point is within the box part of the segment.
            xComp[0] = (uint)(BaseRectangleRightX * yTop[0] / RectangleArea * uint.MaxValue);
            for (int i = 1; i < BlockCount - 1; ++i)
            {
                xComp[i] = (uint)(xRight[i + 1] / xRight[i] * uint.MaxValue);
            }

            // Shown for completeness.
            xComp[BlockCount - 1] = 0;

            // Sanity check. Test that the top edge of the topmost rectangle is at y = 1.
            // We expect there to be a tiny drift away from 1 due to the inexactness of floating point arithmetic.
            Debug.Assert(Math.Abs(1 - yTop[BlockCount - 1]) < 1e-10, "1 - yTop[127] >= 1e-10");
        }

        /// <summary>
        /// A double-precision floating point Gaussian random number.
        /// </summary>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        public double NextDouble()
        {
            return sampleFn();
        }

        /// <summary>
        /// A double-precision floating point Gaussian random number given the distribution mean and the standard deviation.
        /// </summary>
        /// <param name="mean">Distribution mean.</param>
        /// <param name="stdDev">Distribution standard deviation.</param>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        public double NextDouble(double mean, double stdDev)
        {
            return mean + (NextDoubleStandard() * stdDev);
        }

        /// <summary>
        /// Take a sample from the standard gaussian distribution, i.e. with mean of 0 and standard deviation of 1.
        /// </summary>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        public double NextDoubleStandard()
        {
            while (true)
            {
                // Select box at random.
                byte u = (byte)rng.NextUInt();
                int i = u & 0x7F;
                int sign = (u & 0x80) == 0 ? -1 : 1;

                // Generate uniform random value with range [0, 0xffffffff].
                uint u2 = rng.NextUInt();

                // Special case for the base segment.
                if (0 == i)
                {
                    if (u2 < xComp[0])
                    {
                        // Generated x is within R0.
                        return u2 * UIntToU * areaDividedByHeightB0 * sign;
                    }

                    // Generated x is in the tail of the distribution.
                    return SampleTail() * sign;
                }

                // All other segments.
                if (u2 < xComp[i])
                {
                    // Generated x is within the rectangle.
                    return u2 * UIntToU * xRight[i] * sign;
                }

                // Generated x is outside of the rectangle.
                // Generate a random y coordinate and test if our (x,y) is within the distribution curve.
                // This execution path is relatively slow/expensive (makes a call to Math.Exp()) but relatively rarely executed,
                // although more often than the 'tail' path (above).
                double x = u2 * UIntToU * xRight[i];
                if (yTop[i - 1] + (yTop[i] - yTop[i - 1]) * rng.NextDouble() < GaussianPdfDenorm(x))
                {
                    return x * sign;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double NextDoubleNonZero()
        {
            double nonZero;
            do
            {
                nonZero = rng.NextDouble();
            }
            while (Math.Abs(nonZero) < double.Epsilon);
            return nonZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double SampleTail()
        {
            // Sample from the distribution tail (defined as having x >= BaseRectangleRightX).
            double x, y;
            do
            {
                // We use NextDoubleNonZero() because Log(0) returns NaN and will also tend
                // to be a very slow execution path (when it occurs, which is rarely).
                x = -Math.Log(NextDoubleNonZero()) / BaseRectangleRightX;
                y = -Math.Log(NextDoubleNonZero());
            }
            while (y + y < x * x);
            return BaseRectangleRightX + x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double GaussianPdfDenorm(double x)
        {
            // Gaussian probability density function, denormalised, that is, y = e^-(x^2/2).
            return Math.Exp(-x * x / 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double GaussianPdfDenormInv(double y)
        {
            // Inverse function of GaussianPdfDenorm(x).
            // Operates over the y range (0,1], which happens to be the y range of the pdf, with the exception
            // that it does not include y=0, but we would never call with y = 0 so it doesn't matter. Remember
            // that a Gaussian effectively has a tail going off into x == infinity, hence asking what is x when
            // y = 0 is an invalid question in the context of this class.
            return Math.Sqrt(-2 * Math.Log(y));
        }
    }
}
