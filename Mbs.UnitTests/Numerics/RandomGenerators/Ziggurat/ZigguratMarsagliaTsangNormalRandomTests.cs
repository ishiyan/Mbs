using System;
using Mbs.Numerics.RandomGenerators.Ziggurat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Mbs.UnitTests.Numerics.RandomGenerators.Ziggurat
{
    [TestClass]
    public class ZigguratMarsagliaTsangNormalRandomTests
    {
        private const int Seed = 123456890;
        private const int ExpectedCount = 10;
        private const int Count = 10000;
        private const double Mean = 12.3;
        private const double StandardDeviation = 1.23;

        private static readonly double[] Expected =
        {
            0.0188738179215, -0.6381595677902, -2.4014380546905, 0.4090516442993, 0.3330368951602,
            0.6023299279460, 0.6449064649155, -0.2741292982499, 0.6804153809252, 0.7170403875774,
        };
#pragma warning disable S2699 // Tests should include assertions

        /// <summary>
        /// Taken from http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
        /// <para />
        /// SFMT-src-1.3.3.zip, SFMT.86243.out.txt.
        /// </summary>
        [TestMethod]
        public void ZigguratMarsagliaTsangNormalRandom_NextDouble_ReferenceImplementationTest()
        {
            var gen = new ZigguratMarsagliaTsangNormalRandom(Seed);
            for (int i = 0; i < ExpectedCount; ++i)
            {
                double expected = Expected[i];
                double actual = gen.NextDouble();
                Doubles.AreEqual(
                    expected,
                    actual,
                    1e-13,
                    Invariant($"Reference implementation: index={i}, actual={actual} expected={expected}"));
            }
        }

        /// <summary>
        /// Taken from http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
        /// <para />
        /// SFMT-src-1.3.3.zip, SFMT.86243.out.txt.
        /// </summary>
        [TestMethod]
        public void ZigguratMarsagliaTsangNormalRandom_NextDoubleStandard_ReferenceImplementationTest()
        {
            var gen = new ZigguratMarsagliaTsangNormalRandom(Seed);
            for (int i = 0; i < ExpectedCount; ++i)
            {
                double expected = Expected[i];
                double actual = gen.NextDoubleStandard();
                Doubles.AreEqual(
                    expected,
                    actual,
                    1e-13,
                    Invariant($"Reference implementation: index={i}, actual={actual} expected={expected}"));
            }
        }

        [TestMethod]
        public void ZigguratMarsagliaTsangNormalRandom_NextDouble_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new ZigguratMarsagliaTsangNormalRandom(Seed, Mean, StandardDeviation);
            double sum = 0;
            for (int i = 0; i < Count; ++i)
            {
                sum += gen.NextDouble();
            }

            double mean = sum / Count;
            Doubles.AreEqual(
                1,
                mean / Mean,
                1e-3,
                Invariant($"Mean value: actual={mean}, expected={Mean}"));
        }

        [TestMethod]
        public void ZigguratMarsagliaTsangNormalRandom_NextDouble_GeneratedMany_StandardDeviationHasCorrectValue()
        {
            var gen = new ZigguratMarsagliaTsangNormalRandom(Seed, Mean, StandardDeviation);
            double sqrSum = 0;
            for (int i = 0; i < Count; ++i)
            {
                double x = gen.NextDouble() - Mean;
                sqrSum += x * x;
            }

            double standardDeviation = Math.Sqrt(sqrSum / (Count - 1));
            Doubles.AreEqual(
                1,
                standardDeviation / StandardDeviation,
                1e-1,
                Invariant($"Standard deviation value: actual={standardDeviation}, expected={StandardDeviation}"));
        }
#pragma warning restore S2699 // Tests should include assertions

        [TestMethod]
        public void ZigguratMarsagliaTsangNormalRandom_Seed_SameSeed_SameValue()
        {
            var gen1 = new ZigguratMarsagliaTsangNormalRandom(Seed);
            double expected = gen1.NextDouble();

            var gen2 = new ZigguratMarsagliaTsangNormalRandom(Seed);
            double actual = gen2.NextDouble();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ZigguratMarsagliaTsangNormalRandom_Seed_DifferentSeed_DifferentValue()
        {
            var gen1 = new ZigguratMarsagliaTsangNormalRandom(Seed);
            double expected = gen1.NextDouble();

            var gen2 = new ZigguratMarsagliaTsangNormalRandom(Seed + 1);
            double actual = gen2.NextDouble();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void ZigguratMarsagliaTsangNormalRandom_CanReset_ReturnsTrue()
        {
            var gen = new ZigguratMarsagliaTsangNormalRandom(Seed);

            Assert.IsTrue(gen.CanReset);
        }

        [TestMethod]
        public void ZigguratMarsagliaTsangNormalRandom_Reset_ValueGeneratedThenReset_SameValueGenerated()
        {
            var gen = new ZigguratMarsagliaTsangNormalRandom(Seed);
            double expected = gen.NextDouble();

            gen.Reset();
            double actual = gen.NextDouble();

            Assert.AreEqual(expected, actual);
        }
    }
}
