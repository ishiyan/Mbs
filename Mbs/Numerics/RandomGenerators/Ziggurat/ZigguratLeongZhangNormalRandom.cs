using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.RandomGenerators.Ziggurat
{
    /// <summary>
    /// This implementation is derived from the file 'rnorrexp.c' which accompanies this article:
    /// <para/>
    /// <para/>George Marsaglia and Wai Wan Tsang,
    /// The Ziggurat Method for Generating Random Variables,
    /// Journal of Statistical Software, Vol 5, Iss 8, Oct 2000,
    /// http://www.jstatsoft.org/v05/i08.
    /// <para/>
    /// as well as the comment article:
    /// <para/>
    /// Philip H W Leong, Ganglie Zhang, Dong-U Lee, Wayne Luk, and John Villasenor,
    /// A Comment on the Implementation of the Ziggurat method,
    /// Journal of Statistical Software, Vol 12, Iss 7, Feb 2005,
    /// http://www.jstatsoft.org/v12/i07.
    /// <para/>
    /// The original code by Marsaglia and Tsang
    /// with modifications of Leong, Zhang et al (use of KISS instead of SHR3 as the internal uniform generator)
    /// and with removed exponential generator.
    /// </summary>
    public class ZigguratLeongZhangNormalRandom : INormalRandomGenerator
    {
        private readonly Func<double> sampleFn;
        private readonly uint[] kn = new uint[128];
        private readonly double[] wn = new double[128];
        private readonly double[] fn = new double[128];
        private readonly uint seed;
        private uint iz;
        private uint jsr = 123456789;
        private uint z = 362436069;
        private uint w = 521288629;
        private uint jcong = 380116160;
        private int hz;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZigguratLeongZhangNormalRandom"/> class.
        /// </summary>
        /// <param name="mean">The mean value of the normal distribution.</param>
        /// <param name="stdDev">The standard deviation of the normal distribution.</param>
        /// <param name="seed">A seed value.</param>
        public ZigguratLeongZhangNormalRandom(uint seed = 123456789, double mean = 0, double stdDev = 1)
        {
            this.seed = seed;

            // We predetermine which of these four function variants to use at construction time,
            // thus avoiding the two condition tests on each invocation of Sample().
            if (Math.Abs(mean) <= double.Epsilon)
            {
                if (Math.Abs(stdDev - 1) <= double.Epsilon)
                {
                    sampleFn = Rnor;
                }
                else
                {
                    sampleFn = () => Rnor() * stdDev;
                }
            }
            else
            {
                if (Math.Abs(stdDev - 1) <= double.Epsilon)
                {
                    sampleFn = () => mean + Rnor();
                }
                else
                {
                    sampleFn = () => mean + Rnor() * stdDev;
                }
            }

            Reset();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ZigguratLeongZhangNormalRandom"/> can be reset,
        /// so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public bool CanReset => true;

        /// <summary>
        /// Resets the <see cref="ZigguratLeongZhangNormalRandom"/>,
        /// so that it produces the same random number sequence again.
        /// </summary>
        public void Reset()
        {
            jsr = 123456789;
            if (jsr != seed)
            {
                jsr ^= seed;
            }

            z = 362436069;
            w = 521288629;
            jcong = 380116160;

            // Setup tables for RNOR.
            const double m1 = 2147483648.0;
            double dn = 3.442619855899;
            double vn = 9.91256303526217e-3;
            double tn = dn;

            double exp = Math.Exp(-dn * dn / 2);
            double q = vn / exp;
            kn[0] = (uint)(dn / q * m1);
            kn[1] = 0;

            wn[0] = q / m1;
            wn[127] = dn / m1;

            fn[0] = 1.0;
            fn[127] = exp;

            for (int i = 126; i >= 1; --i)
            {
                dn = Math.Sqrt(-2 * Math.Log(vn / dn + exp));
                kn[i + 1] = (uint)(dn / tn * m1);
                tn = dn;
                exp = Math.Exp(-dn * dn / 2);
                fn[i] = exp;
                wn[i] = dn / m1;
            }
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
            return mean + NextDoubleStandard() * stdDev;
        }

        /// <summary>
        /// Take a sample from the standard gaussian distribution, i.e. with mean of 0 and standard deviation of 1.
        /// </summary>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        public double NextDoubleStandard()
        {
            return Rnor();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void NewZ()
        {
            z = 36969 * (z & 65535) + (z >> 16);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void NewW()
        {
            w = 18000 * (w & 65535) + (w >> 16);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private uint Mwc()
        {
            NewZ();
            NewW();
            return (z << 16) + w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private uint Shr3()
        {
            uint jz = jsr;
            jsr ^= jsr << 13;
            jsr ^= jsr >> 17;
            jsr ^= jsr << 5;
            return jz + jsr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private uint Cong()
        {
            jcong = 69069 * jcong + 1234567;
            return jcong;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private uint Kiss()
        {
            return (Mwc() ^ Cong()) + Shr3();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double Rnor()
        {
            hz = (int)Kiss();
            iz = (uint)(hz & 127);
            return Math.Abs(hz) < kn[iz] ? hz * wn[iz] : FixN();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double Uni()
        {
            return 0.5 + (int)Kiss() * 0.2328306e-9;
        }

        /// <summary>
        /// Generates variates from the residue when rejection in RNOR occurs.
        /// </summary>
        /// <returns>The generated variate.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double FixN()
        {
            // The start of the right tail.
            const double r = 3.442620;
            const double oneOverR = 1 / r;
            while (true)
            {
                double x = hz * wn[iz];
                if (iz == 0)
                {
                    double y;
                    do
                    {
                        y = -Math.Log(Uni());
                        x = y * oneOverR;
                    }
                    while (y + y < x * x);
                    y = r + x;
                    return hz > 0 ? y : -y;
                }

                // iz > 0, handle the wedges of other strips.
                if (fn[iz] + Uni() * (fn[iz - 1] - fn[iz]) < Math.Exp(-0.5 * x * x))
                {
                    return x;
                }

                // Initiate, try to exit the loop.
                hz = (int)Shr3();
                iz = (uint)(hz & 127);
                if (Math.Abs(hz) < kn[iz])
                {
                    return hz * wn[iz];
                }
            }
        }
    }
}
