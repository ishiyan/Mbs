using System;
using Mbs.Numerics.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Mbs.UnitTests.Numerics.RandomGenerators
{
    [TestClass]
    public class ZigguratNormalRandomTests
    {
        private const int Seed = 123456890;
        private const int Count = 10000;
        private const double Mean = 12.3;
        private const double StandardDeviation = 1.23;

        private static ZigguratNormalRandom CreateGenerator(int seed = Seed)
        {
            return new ZigguratNormalRandom(new XorShiftUniformRandom(seed), Mean, StandardDeviation);
        }

        [TestMethod]
        public void ZigguratNormalRandom_NextDouble_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = CreateGenerator();
            double sum = 0;
            for (int i = 0; i < Count; ++i)
            {
                sum += gen.NextDouble();
            }

            double mean = sum / Count;
            Doubles.AreEqual(mean / Mean, 1, 1e-3,
                Invariant($"Mean value: actual={mean}, expected={Mean}"));
        }

        [TestMethod]
        public void ZigguratNormalRandom_NextDouble_GeneratedMany_StandardDeviationHasCorrectValue()
        {
            var gen = CreateGenerator();
            double sqrSum = 0;
            for (int i = 0; i < Count; ++i)
            {
                double x = gen.NextDouble() - Mean;
                sqrSum += x * x;
            }

            double standardDeviation = Math.Sqrt(sqrSum / (Count - 1));
            Doubles.AreEqual(standardDeviation / StandardDeviation, 1, 1e-2,
                Invariant($"Standard deviation value: actual={standardDeviation}, expected={StandardDeviation}"));
        }

        [TestMethod]
        public void ZigguratNormalRandom_NextDoubleStandard_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = CreateGenerator();
            double sum = 0;
            for (int i = 0; i < Count; ++i)
            {
                sum += gen.NextDoubleStandard();
            }

            double mean = sum / Count;
            Doubles.AreEqual(mean, 0, 1e-2,
                Invariant($"Mean value: actual={mean}, expected=0"));
        }

        [TestMethod]
        public void ZigguratNormalRandom_NextDoubleStandard_GeneratedMany_StandardDeviationHasCorrectValue()
        {
            var gen = CreateGenerator();
            double sqrSum = 0;
            for (int i = 0; i < Count; ++i)
            {
                double x = gen.NextDoubleStandard();
                sqrSum += x * x;
            }

            double standardDeviation = Math.Sqrt(sqrSum / (Count - 1));
            Doubles.AreEqual(standardDeviation, 1, 1e-2,
                Invariant($"Standard deviation value: actual={standardDeviation}, expected=1"));
        }

        [TestMethod]
        public void ZigguratNormalRandom_Seed_SameSeed_SameValue()
        {
            var gen1 = CreateGenerator();
            double expected = gen1.NextDouble();

            var gen2 = CreateGenerator();
            double actual = gen2.NextDouble();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ZigguratNormalRandom_Seed_DifferentSeed_DifferentValue()
        {
            var gen1 = CreateGenerator();
            double expected = gen1.NextDouble();

            var gen2 = CreateGenerator(Seed - 1);
            double actual = gen2.NextDouble();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void ZigguratNormalRandom_CanReset_ReturnsTrue()
        {
            var gen = CreateGenerator();

            Assert.IsTrue(gen.CanReset);
        }

        [TestMethod]
        public void ZigguratNormalRandom_Reset_ValueGeneratedThenReset_SameValueGenerated()
        {
            var gen = CreateGenerator();
            double expected = gen.NextDouble();

            gen.Reset();
            double actual = gen.NextDouble();

            Assert.AreEqual(expected, actual);
        }
    }
}
