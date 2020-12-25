using System.Linq;
using Mbs.Numerics.RandomGenerators.Well;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Mbs.UnitTests.Numerics.RandomGenerators.Well
{
    [TestClass]
    public class Well1024AUniformRandomTests
    {
        private const int Seed = 12345;
        private const int Count = 5000;

        private static readonly int[] BaseInt =
        {
            740849862, 1202665156, -199039369, -259008301, -291878969, -1164428990, -1565918811, 491009864,
            -1883086670, 1383450241, 1244617256, 689006653, -1576746370, -1307940314, 1421489086, 1742094000,
            -595495729, 1047766204, 1875773301, -1637793284, 1379017098, 262792705, 191880010, -251000180,
            -1753047622, -972355720, 90626881, 1644693418, 1503365577, 439653419, 1806361562, 1268823869,
        };

        private static readonly int[] ExpectedInt =
        {
            -1478749726, -1645579484, -2075363835, -2063444174, -1834148336, -1769045872, -40711346, 1717441026,
            2130656771, 783441285, 570433609, 1560023451, 653233971, 1368672434, -72036215, 1071111800,
            933776492, 26114960, 49888778, 1808107515, 1092989296, 754848337, 1336994364, -1987450448,
            -691190146, -1803870839, 1110716866, 1173269113, -391000050, 2014216908, 180756301, -382891013,
            -1908154585, 1580737629, 1080267957, -125532248, 2094530239, 2132964485, -438596348, -760299445,
            1058181869, 2050816800, -1534429037, -62552782, 824524142, -818590371, -1857695907, -684762866,
            -156556543, -902759995, -880795194, -1387351132, -1263017515, 448006597, 201038266, 1929826313,
            -455367306, 672963027, 2000073013, -1546842042, 446341090, 1001696686, -779919012, -347722602,
            -1342821677, 1639571150, -835315755, 1505585376, 367004975, -2035864404, -1786623553, 1249724913,
            182435312, 1444514513, 1815333708, 1333772382, 299664001, -284691169, 2034403374, 1423310887,
            -1319051884, 1557286441, -445198266, -251809030, 1602786123, 944036382, -1020529634, 258344235,
            685254367, 1838964943, -156674528, -979736602, -538312836, 234643178, 211152102, -635498640,
            -1036733933, -1347589147, -565609042, -1358714165, 508618483, -786364693, 2071450261, 1206956772,
            -678931458, 167690617, 144698821, 1719720781, 1575869280, -1343221123, -1766469944, 284991647,
            -717305514, 892653651, -1368347075, -615701972, -730369849, 1360396003, -1869287623, 1778269052,
            -586061545, -699517114, 61530249, -1860611767, -519660852, 1841085925, 1555610093, -399979337,
            -790345742, 422355947, 2007965433, 2044952550, -1712164595, -102915702, -693865324, -1894042487,
            -1285020072, -215883074, 95833252, 1625818040, -1055951680, 513067085, 1825246558, -553461652,
            -1923361799, -1869480206, 567232636, -1751727150, -1832301399, -108136455, -1312244126, 14006795,
            850221366, -382389732, -1741556188, -1317464467, 1948314870, 753994471, 1028235947, 342494132,
            -1862256693, 723808794, -234257642, 1609928369, -802733456, 1315831915, 1436072885, 1224767136,
            2144557791, -1839965886, 224821018, -1461697757, -1080386760, 1638573498, -1188173812, -325181523,
            -1750676219, -1780415850, 698793362, -908352052, 299746482, -161660934, 1938166833, 800297005,
            56640033, -1214932666, -1248124842, 1822796868, 1777615881, -718517774, 1908159957, 1733053281,
            1851844331, 1283519375, -1771494956, 2060179999, 1666129209, 1919453531, -498145770, 697567008,
            1855487148, -1587163491, 565216434, -1477877933, -925662919, -806492585, -1201439047, -1424534232,
            1788616523, 69414717, 655893636, -1175978556, 24787512, -861550001, 439525754, -190433174,
            -383811606, -508589783, 1441608687, 608181366, 1539467064, 925903122, 697209654, 1878283393,
            -1967567432, -1659677763, -249658183, 847096354, 397741956, -125334541, -1286840731, 1016461908,
            -997968592, 1795331475, 1856856501, -1716726445, -582181331, -887091847, 426964855, -609219941,
            -1456232632, -483467616, 1069260754, 972242064, -1406786247, 1954194029, 52627891, 1212755081,
            2117436668, 281073392, 741537353, -483063506, 1850906286, -244876135, -270818140, 1817568823,
        };

        /// <summary>
        /// Apache Java test. Taken from http://svn.apache.org/repos/asf/commons/proper/math.
        /// </summary>
        [TestMethod]
        public void Well1024AUniformRandom_NextUInt_ApacheJavaTest()
        {
            const int length = 100;
            var gen = new Well1024AUniformRandom(BaseInt);
            for (int i = 0; i < length; ++i)
            {
                var expected = (uint)ExpectedInt[i];
                uint actual = gen.NextUInt();
                Assert.AreEqual(
                    expected,
                    actual,
                    Invariant($"Apache Java int: index={i}, actual={actual} expected={expected}"));
            }
        }

        /// <summary>
        /// Taken from https://bitbucket.org/sergiu/random/src/.
        /// Verifies the value generated by the reference implementation after 10⁹ iterations.
        /// The generator is seeded using an array filled with 1s.
        /// </summary>
        // [Ignore("Takes about 1 minute to execute.")]
        [TestMethod]
        public void Well1024AUniformRandom_NextUInt_BoostReferenceImplementationTest()
        {
            var init = new int[1024];
            for (int i = 0; i < init.Length; ++i)
            {
                init[i] = 1;
            }

            var gen = new Well1024AUniformRandom(init);
            int iterations = 1000000000;
            uint actual = 0;
            while (iterations-- > 0)
            {
                actual = gen.NextUInt();
            }

            const uint expected = 0xd07f528cU;
            Assert.AreEqual(
                expected,
                actual,
                Invariant($"Boost reference implementation uint: actual={actual} expected={expected}"));
        }

#pragma warning disable S2699 // Tests should include assertions
        [TestMethod]
        public void Well1024AUniformRandom_NextDouble_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new Well1024AUniformRandom(Seed);
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
                1e-1,
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
                1e-2,
                Invariant($"Mean value 3: actual={actual}, expected={expected}"));
        }

        [TestMethod]
        public void Well1024AUniformRandom_NextInt_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new Well1024AUniformRandom(Seed);

            double expected = int.MaxValue / 2d;
            double actual = 0;
            for (int i = 0; i < Count; ++i)
            {
                actual += gen.NextInt();
            }

            actual /= Count;
            Doubles.AreEqual(
                expected / actual,
                1,
                1e-1,
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
        public void Well1024AUniformRandom_NextBoolean_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new Well1024AUniformRandom(Seed);

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
        public void Well1024AUniformRandom_NextBytes_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new Well1024AUniformRandom(Seed);

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
        public void Well1024AUniformRandom_Seed_SameSeed_SameValue()
        {
            var gen1 = new Well1024AUniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new Well1024AUniformRandom(Seed);
            int actual = gen2.NextInt();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Well1024AUniformRandom_Seed_DifferentSeed_DifferentValue()
        {
            var gen1 = new Well1024AUniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new Well1024AUniformRandom(Seed + 1);
            int actual = gen2.NextInt();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void Well1024AUniformRandom_CanReset_ReturnsTrue()
        {
            var gen = new Well1024AUniformRandom();

            Assert.IsTrue(gen.CanReset);
        }

        [TestMethod]
        public void Well1024AUniformRandom_Reset_ValueGeneratedThenReset_SameValueGenerated()
        {
            var gen = new Well1024AUniformRandom();
            int expected = gen.NextInt();

            gen.Reset();
            int actual = gen.NextInt();

            Assert.AreEqual(expected, actual);
        }
    }
}
