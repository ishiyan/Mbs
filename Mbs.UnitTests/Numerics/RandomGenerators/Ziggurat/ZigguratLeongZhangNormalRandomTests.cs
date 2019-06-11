using Mbs.Numerics.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Mbs.UnitTests.Numerics.RandomGenerators
{
    [TestClass]
    public class ZigguratLeongZhangNormalRandomTests
    {
        private const int Seed = 123456890;
        private const int ExpectedCount = 10;

        private static readonly double[] Expected =
        {
            -0.0348489455891, 0.1133640240572, 0.4399593126786,  0.8364859775316, -1.3212429099975,
            -0.3842937024311, 1.5063555141008, -0.5565839502005, 0.5643957962826, -2.1546832849813
        };

        /// <summary>
        /// Taken from http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
        /// <para />
        /// SFMT-src-1.3.3.zip, SFMT.86243.out.txt.
        /// </summary>
        [TestMethod]
        public void ZigguratLeongZhangNormalRandom_NextDouble_ReferenceImplementationTest()
        {
            var gen = new ZigguratLeongZhangNormalRandom(Seed);
            for (int i = 0; i < ExpectedCount; ++i)
            {
                double expected = Expected[i];
                double actual = gen.NextDouble();
                Doubles.AreEqual(expected, actual, 1e-13,
                    Invariant($"Reference implementation: index={i}, actual={actual} expected={expected}"));
            }
        }

        /// <summary>
        /// Taken from http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
        /// <para />
        /// SFMT-src-1.3.3.zip, SFMT.86243.out.txt.
        /// </summary>
        [TestMethod]
        public void ZigguratLeongZhangNormalRandom_NextDoubleStandard_ReferenceImplementationTest()
        {
            var gen = new ZigguratLeongZhangNormalRandom(Seed);
            for (int i = 0; i < ExpectedCount; ++i)
            {
                double expected = Expected[i];
                double actual = gen.NextDouble();
                Doubles.AreEqual(expected, actual, 1e-13,
                    Invariant($"Reference implementation: index={i}, actual={actual} expected={expected}"));
            }
        }

        [TestMethod]
        public void ZigguratLeongZhangNormalRandom_Seed_SameSeed_SameValue()
        {
            var gen1 = new ZigguratLeongZhangNormalRandom(Seed);
            double expected = gen1.NextDouble();

            var gen2 = new ZigguratLeongZhangNormalRandom(Seed);
            double actual = gen2.NextDouble();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ZigguratLeongZhangNormalRandom_Seed_DifferentSeed_DifferentValue()
        {
            var gen1 = new ZigguratLeongZhangNormalRandom(Seed);
            double expected = gen1.NextDouble();

            var gen2 = new ZigguratLeongZhangNormalRandom(Seed + 1);
            double actual = gen2.NextDouble();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void ZigguratLeongZhangNormalRandom_CanReset_ReturnsTrue()
        {
            var gen = new ZigguratLeongZhangNormalRandom(Seed);

            Assert.IsTrue(gen.CanReset);
        }

        [TestMethod]
        public void ZigguratLeongZhangNormalRandom_Reset_ValueGeneratedThenReset_SameValueGenerated()
        {
            var gen = new ZigguratLeongZhangNormalRandom(Seed);
            double expected = gen.NextDouble();

            gen.Reset();
            double actual = gen.NextDouble();

            Assert.AreEqual(expected, actual);
        }
    }
}
