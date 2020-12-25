using System.Linq;
using Mbs.Numerics.RandomGenerators.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Mbs.UnitTests.Numerics.RandomGenerators.Other
{
    [TestClass]
    public class SystemUniformRandomTests
    {
        private const int Seed = 12345;
        private const int Count = 5000;

#pragma warning disable S2699 // Tests should include assertions
        [TestMethod]
        public void SystemUniformRandom_NextDouble_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new SystemUniformRandom(Seed);
            double expected = 0.5;

            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextDouble();
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-3,
                Invariant($"Mean value 1: actual={actual}, expected={expected}"));

            expected = 4;
            actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextDouble(8);
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-2,
                Invariant($"Mean value 2: actual={actual}, expected={expected}"));

            expected = 6;
            actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextDouble(4, 8);
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-3,
                Invariant($"Mean value 3: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void SystemUniformRandom_NextInt_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new SystemUniformRandom(Seed);

            double expected = int.MaxValue / 2d;
            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt();
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-3,
                Invariant($"Mean value 1: actual={actual}, expected={expected}"));

            expected = 400d;
            actual = 0d;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt(800);
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-2,
                Invariant($"Mean value 2: actual={actual}, expected={expected}"));

            expected = 600d;
            actual = 0d;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt(400, 800);
            }

            actual /= Count;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-2,
                Invariant($"Mean value 3: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void SystemUniformRandom_NextBoolean_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new SystemUniformRandom(Seed);

            const double expected = Count / 2d;
            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextBoolean() ? 1 : 0;
            }

            Doubles.AreEqual(
                1,
                expected / actual,
                1e-1,
                Invariant($"Mean value: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void SystemUniformRandom_NextBytes_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new SystemUniformRandom(Seed);

            const double expected = 128;
            double actual = 0;
            var array = new byte[32];

            for (int i = 0; i < Count; ++i)
            {
                gen.NextBytes(array);
                actual = array.Aggregate(actual, (current, b) => current + b);
            }

            actual /= Count * 32;
            Doubles.AreEqual(
                1,
                expected / actual,
                1e-2,
                Invariant($"Mean value: actual={actual}, expected={expected}"));
        }
#pragma warning restore S2699 // Tests should include assertions

        [TestMethod]
        public void SystemUniformRandom_Seed_SameSeed_SameValue()
        {
            var gen1 = new SystemUniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new SystemUniformRandom(Seed);
            int actual = gen2.NextInt();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SystemUniformRandom_Seed_DifferentSeed_DifferentValue()
        {
            var gen1 = new SystemUniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new SystemUniformRandom(Seed + 1);
            int actual = gen2.NextInt();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void SystemUniformRandom_CanReset_ReturnsTrue()
        {
            var gen = new SystemUniformRandom();

            Assert.IsTrue(gen.CanReset);
        }

        [TestMethod]
        public void SystemUniformRandom_Reset_ValueGeneratedThenReset_SameValueGenerated()
        {
            var gen = new SystemUniformRandom();
            int expected = gen.NextInt();

            gen.Reset();
            int actual = gen.NextInt();

            Assert.AreEqual(expected, actual);
        }
    }
}
