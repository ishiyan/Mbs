﻿using System.Linq;
using Mbs.Numerics.RandomGenerators.Well;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Mbs.UnitTests.Numerics.RandomGenerators.Well
{
    [TestClass]
    public class Well19937CUniformRandomTests
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
            2128528153, 327121884, 935445371, -83026433, -1041143083, 2084595880, -1073535198, -1678863790, -523636021, -1514837782, -736786810, 1527711112, -1051227939, 978703380, 410322163, 1727815703,
            -648426354, 636056441, 1954420292, 17754810, -958628705, -1091307602, 1793078738, -1680336346, 1792171272, 941973796, -2066152330, -1248758068, -1061211586, 262020189, 1276960217, -233886784,
            1767509252, -1811939255, -406116097, -742435920, -1349799525, 240329556, -332161345, 1488943143, -332244280, 2093328957, 674753300, -1930135556, 257111467, 63793650, -1964335223, 1315849133,
            -797349146, 1372022250, -1451892049, -1325138957, -870401239, -1294317369, 91490879, 386205044, -704074702, -1230679067, 1513674392, -262996240, 1196007314, 1398903796, 803719762, -1750926831,
            -1268814180, 1233515404, 1498313934, -970591257, 611113671, -261632474, 1834097325, 1709440492, -150396854, 2120561003, -62645660, 479080234, 1535125050, 1823378695, -1129289329, -1095198399,
            2092564733, 78836308, -692015409, 1647147229, -1847922219, 1838279320, -848333841, -1375151778, 920238861, 1512628290, -749439404, 288851918, -427218675, 679640964, 425700808, -2077624511,
            -1929434455, -647176419, 650437190, -1926749131, -1564744729, 734494454, 108193743, 246246679, 810042628, 1952337771, 1089253730, -1874275331, 1428419392, -492969232, 1945270770, -201265602,
            -755490251, -624426214, -699605715, -113446478, 809091299, -1521531511, 1136505389, -523660964, 132928433, 1926559713, -1485314325, -508322506, 46307756, -1627479740, -589386406, -1855555892,
            584299545, 1272841066, -597242658, 925134545, 1102566453, -753335037, -9523218, -1778632375, 568963646, 764338254, 1259944540, -2000124642, 1307414525, -151384482, 807294400, 1993749511,
            -15503094, -709471492, 2104830082, 1387684315, -1929056119, 224254668, -733550950, -889466978, -1987783335, -437144026, 995905753, -1021386158, -1096313388, -1014152835, -1303258241, 1201884788,
            -1845042397, 1421462511, 980805867, 2143771251, 481226968, 1790544569, 328448328, 1995857639, -66668269, -1411421267, -222586606, 866950765, -308713926, -1048350893, 993222402, -1139265642,
            -871837948, 1145571913, 381928580, 35386691, 1640961123, -1192981020, 775971009, 594246635, 1603197812, -575106766, 2023682000, -1636301903, -718093720, -1666421635, -2146115988, 320593570,
            287355418, 454400027, 1112753817, 1751196267, 782077910, -1478447368, -1007557264, -862315517, -2035355952, 2123515250, -557641502, -1789932035, 879640129, 44167603, 791148984, 1382939723,
            -2135684233, 1825489580, 937345485, -1839983359, -1536880111, -1472578359, 1548052748, -1471535862, -14508727, 1509621398, -2134967452, -787485401, 815341660, -327905128, 1028096737, 866906991,
            -1585990806, 859229080, 234806270, 998518056, -1897890815, -900923587, 1179856752, 1529572451, 620486106, 1119836556, 1661285564, 2097404633, -1437490790, 265306115, -984880135, 1326751968,
            1280043536, 680210701, 155786166, 1550973250, -325781949, -597789777, -1939780, 1345275487, 1930450001, 941449704, 669301309, 693651713, -990721514, 582968326, 976132553, -1892942099,
            -1065070157, -711990993, -688974833, -1026091683, 1115346827, -1305730749, -1733626381, -364566696, -21761572, -37152746, -262011730, 1302722752, -1806313409, -767072509, 764112137, 1671157377,
            1837645038, -1021606421, -1781898911, -232127459, -310742675, -1818095744, -1128320656, -705565953, -354445532, -523172807, -433877202, 131904485, -64292316, 381829280, 229820263, 1797992622,
            1359665678, 978481451, -885267130, -1415988446, -356533788, -961419072, 1938703090, 708344111, 679299953, 744615129, 1328811158, 1257588574, 569216282, -753296151, -1519838713, 2016884452,
            1062684606, 1561736790, 2028643511, -1353001615, 886376832, 1466953172, 1664783899, 1290079981, -57483993, -1176112430, 1634916316, 1976304475, 1374136869, -648738039, 1058175869, -909000745,
            -1526439218, 726626991, 2066596202, 64980943, -26166577, -885900005, -1821546816, -1103727665, 730606315, -1324948459, -696956940, -1300869403, 1171578314, 797249074, -1600611618, 1928247682,
            307164165, -1482476232, -1886179640, 1306433392, 1945271359, -1272113751, -1285984081, -2057145549, 795047465, 1262569087, -1239828121, 1426641636, -786371495, 2120199316, 1273690652, 74457589,
            -1033394229, 338952565, 46122958, 1225741533, 2115308090, 678200841, -1618264885, -101162569, -1628976330, -1232839500, 468709044, 1876019116, 92723122, 233398255, -554960844, 38494196,
            -406437278, 2083528643, -1106878615, -340722557, -2123964932, 223183343, 108918116, -1014629054, -901344544, -838896840, -1908460517, -1763508731, -926890833, 1703791049, -667755577, 1694418389,
            791641263, 1095689677, 1119202039, -1419111438, -2012259010, 188017439, -1775110395, -1971099661, -1688113734, 131472813, -776304959, 1511388884, 2080864872, -1733824651, 1992147495, 1119828320,
            1065336924, -1357606762, 462963503, 1180719494, -202678962, -892646595, 605869323, 1144255663, 878462678, -1051371303, 872374876, 631322271, -172600544, -1552071375, -1939570033, 151973117,
            1640861022, 310682640, 34192866, 2057773671, -2004476027, -1879238973, 582736114, 900581664, -427390545, -1232348528, -535115984, 1321853054, 69386780, -1729375922, 1418473715, 1022091451,
            496799289, -80757405, -1903543310, -1128846846, 1703964, 1984450945, 856753858, -812919184, 775486323, -1376056193, 638628840, 314243536, 1030626207, 644050997, 73923896, 362270613,
            236584904, 1463240891, -223614432, 435371594, -751940030, -124274553, -1991092884, 1579624267, 1249632649, 157589625, -345229739, -366245207, -1399995986, 1651729983, 1965074340, -1108970305,
            1163690769, 1732013523, -1461252895, 669755552, -476503675, -264578685, -32813949, 288222188, -25734262, 106040916, 1654395626, -365148479, 2014455846, -2040447994, 1351639280, -919975757,
            -1970412139, -47306532, 222377665, -363434917, -1091717516, 2090685531, -1221091649, -1729649190, -1239406708, 1064945398, -105437479, -419675255, 74701669, -12862899, -498269844, 1566898997,
            -1872838355, 1596887574, 485902962, 469225597, -881763553, 1307841032, -1642872487, 1388543045, 379792876, 1095683384, 840780732, 1934378038, 1851278350, -1359389423, 130868458, -313448799,
            -663624816, 1031714153, -608443411, -205137499, -1849464427, 1973593637, 1068741808, -1420655961, 1188762305, 954044841, -995454462, -1818101092, -1937201943, -324541290, -1520603933, 572873173,
            -554764496, 1051557081, -1245136076, -985349536, 329320398, 1787901464, -37803304, -1759310177, -1463492617, -1861729663, 1251768782, 256937091, -779036948, -2049893864, 1256022877, 1228075657,
            -1550195255, -611319853, 1190797155, 2047604112, -576077160, -1532843331, -1324899394, -159729560, -622525946, -1080302767, -236033484, 1895243903, -410123689, -1944154157, -681781021, 1208453003,
            579595878, 1303914051, -145607082, -131567277, -1917288455, 894217359, -175688726, -1585480723, 663691440, -1140068263, -641711178, 1596080008, 629197693, 976422358, -1570451095, 525923776,
            895046136, -504151767, 1602553020, -1233054923, -1798474837, -1488857895, 1055782627, 261863143, 1879276655, 488240679, 1910982611, -1919441259, 370435945, 1265230086, -1293284428, -1503576227,
            2076963035, -1379628250, 1157098875, 1984461153, -1947837397, 1705880124, 1453607404, -1233649748, 1479943773, -863878721, -862415630, -736723275, 940306358, -1596000684, -1174889953, -615723892,
            -885006597, -1796723178, 1844159055, -188942309, 2107251811, -1675486996, -1009475178, -859263556, -431866963, -9593673, -1878920923, -104853791, -1535224994, -69315537, 586690130, -1292234796,
            1378749456, -301873019, -319297563, 1677205851, 292450579, -1289441171, 1788113680, 1907606333, 1464711611, -1372023606, -1978832445, -1772259768, 1949124464, 1818322887, -1138036603, 1249727628,
            -1474866449, -1868013169, -1384567593, 717007936, 954189997, -1900561040, 738470389, -158973180, 1732860784, 1936031206, -1133354740, -1173166665, 1432976712, 852636081, 1732064691, -1831788120,
            1273933579, 455403217, 1988395890, 106493468, 506092152, -610530423, 1698053512, 1311747476, 1969503012, -1887461759, 1613543073, 903200334, -737865837, 325656800, -1234001200, 1492148864,
            2009861533, -368262605, 1091338541, 2076108119, -961392337, 1835877112, 316250307, -853333391, -2125443777, 815363504, -798707803, -158146540, 690786114, -530775684, 1203556940, 1611485582,
            -1661412270, -53184506, 2126287444, -232222229, 1559486057, 283532250, 1202760418, 932144172, 1082594656, -570104011, 413509167, -995027177, -996477516, -540544, -745537167, -712135469,
            -996294983, -592787198, 1889840948, 1314628747, -394266926, -682316577, 456447239, 1728806063, -396279614, -43387643, 1915717013, -861574144, -1078710588, -561401249, 1111464540, 63643984,
            -1693870413, -968369980, -1053148188, 708799038, 1883537988, 373371671, -156410415, -1596483236, -1846890431, 888692915, -1025632583, -1666477591, -343066267, -2059058792, 641501628, -1744347292,
            1648632991, 1743540146, 2020952406, 164014499, 990508262, 1706408228, -1236471842, -347116260, 1843634523, 827255665, 300519853, -1265974830, -547247177, -583064554, -1995437077, 689210107,
            -93151393, 835365056, 1706367315, -1605902756, 200954895, 431093688, -277573364, -928486713, -552221973, 145432789, 1128919795, 1675095586, 1930359882, 1215849501, -1447770583, 657776490,
            1885869860, -1629237204, -868897479, -1258169760, 1828140195, -883850439, 463933909, -347361158, 1478116648, 801176896, -1501915899, 1017335748, -1654508882, 123994786, 1588785290, 791166651,
            -1523108535, 340411166, -496474762, -1189711141, -7392628, 2045171250, -1245366209, 834787230, -1346883181, 2034209454, 737043362, 898803323, 1983089087, -1845404320, 9585188, -1180608323,
            1665100606, 1949474222, -211115008, 1151308295, -2132174259, 913126312, -2085061672, 1419864120, -1134542954, -53833957, -246913211, 468382370, -1759479323, 1136686211, 1307012488, -2036299559,
            -1346099736, 314743106, -1683101865, -947151948, -234529696, -2103334293, -279256894, -1484257, -1053953017, 1801205399, 941594454, -874119215, -672865187, 762284205, -1494975451, 486607927,
            -898264389, -1711861093, -212572760, 2106484281, -1610786470, 1352525590, -837779586, 1568282001, -593019125, -1146260782, -1595879979, -640781858, 1107692311, 1547132709, -1928385535, -2057772805,
            634887038, 329772618, 942136006, -864405576, 501883884, 1537141484, -1180626836, 1123055420, 1090885851, 421662750, 2033111605, 1710917425, -1118058244, 74321279, 257328195, -1199940697,
            208625996, -442341447, 808119183, 1166827075, 1177417517, -1856155370, -1464837036, -60624923, -1306220638, -91104698, -1434621430, 548899241, 37351476, 1478278431, -1255061434, 248470035,
            -104642597, -1865169521, 1418373655, -1660810523, -2129015436, 154612798, 276575732, 1930338442, 179503250, -929294855, -39452027, -1377657544, 1442322193, 1137511318, -432158653, -984801987,
            743099148, -1118893528, -904123623, -1273146363, -1884800406, -803169061, 1254123158, -484252077, 317646844, 404246525, -1230293916, 1121445742, -19657507, 652967153, -1055406692, -468950719,
            -1493532921, -1447624258, -1369679689, -1517000228, -145853307, 1518006526, 1591195514, -1475557146, -909722097, 2103182976, -406830579, -2124025254, -1804819507, -1357512858, 567321869, 409048156,
            567805180, 1749009386, 1762759722, -1770324077, 1271140844, 468219092, 955792405, 1911965665, 1876314424, -718200715, -1278883927, 1392281730, -120519585, 851473793, 245054754, -33369039,
            -284877584, -479534880, -212346563, -122017521, -1461429983, 1331007370, 1788621721, 1739036536, 1446350953, -1985448033, 685528610, -1386434659, 1368233993, 2021786790, 1596478397, -1716635278,
            -2011083017, 171876097, -311529197, 687812052, 377000657, -1055547517, -1499047842, -1818434951, -120863666, 33888043, -1387509273, -541540700, 1162597745, -1331415338, 1931708792, -850270000,
            663845594, 1536495943, -322924971, -1380272203, 261190298, -204874428, -2104974031, 883819928, 155808204, -1454446035, 1323388464, -1696505728, 1549800285, 1018150463, -1327715703, -1582480640,
            1013659809, -1820360082, 1666498787, 1406120540, -196541482, 1248470531, -1250433281, 836375878, 177646854, -1927020253, 2145878321, 689712096, -596605921, 348283199, 1916993096, 481356808,
            -339687826, 1219340319, 718895887, -2007521340, -1859185806, 2042164737, -58146784, 742449142, 1930754708, 780832111, 715056441, -1393886151, -8150527, -599607443, -537300865, -1212516084,
        };
#pragma warning disable S2699 // Tests should include assertions

        /// <summary>
        /// Apache Java test. Taken from http://svn.apache.org/repos/asf/commons/proper/math.
        /// </summary>
        [TestMethod]
        public void Well19937CUniformRandom_NextUInt_ApacheJavaTest()
        {
            var init = new int[624];
            for (int i = 0; i < init.Length; ++i)
            {
                init[i] = BaseInt[i % BaseInt.Length] + i;
            }

            const int length = 100;
            var gen = new Well19937CUniformRandom(init);
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
        public void Well19937CUniformRandom_NextUInt_BoostReferenceImplementationTest()
        {
            var init = new int[624];
            for (int i = 0; i < init.Length; ++i)
            {
                init[i] = 1;
            }

            var gen = new Well19937CUniformRandom(init);
            int iterations = 1000000000;
            uint actual = 0;
            while (iterations-- > 0)
            {
                actual = gen.NextUInt();
            }

            const uint expected = 0x243eaed5U;
            Assert.AreEqual(
                expected,
                actual,
                Invariant($"Boost reference implementation uint: actual={actual} expected={expected}"));
        }

        [TestMethod]
        public void Well19937CUniformRandom_NextDouble_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new Well19937CUniformRandom(Seed);
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
        public void Well19937CUniformRandom_NextInt_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new Well19937CUniformRandom(Seed);

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
        public void Well19937CUniformRandom_NextBoolean_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new Well19937CUniformRandom(Seed);

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
        public void Well19937CUniformRandom_NextBytes_GeneratedMany_MeanHasCorrectValue()
        {
            var gen = new Well19937CUniformRandom(Seed);

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
        public void Well19937CUniformRandom_Seed_SameSeed_SameValue()
        {
            var gen1 = new Well19937CUniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new Well19937CUniformRandom(Seed);
            int actual = gen2.NextInt();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Well19937CUniformRandom_Seed_DifferentSeed_DifferentValue()
        {
            var gen1 = new Well19937CUniformRandom(Seed);
            int expected = gen1.NextInt();

            var gen2 = new Well19937CUniformRandom(Seed + 1);
            int actual = gen2.NextInt();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void Well19937CUniformRandom_CanReset_ReturnsTrue()
        {
            var gen = new Well19937CUniformRandom();

            Assert.IsTrue(gen.CanReset);
        }

        [TestMethod]
        public void Well19937CUniformRandom_Reset_ValueGeneratedThenReset_SameValueGenerated()
        {
            var gen = new Well19937CUniformRandom();
            int expected = gen.NextInt();

            gen.Reset();
            int actual = gen.NextInt();

            Assert.AreEqual(expected, actual);
        }
    }
}
