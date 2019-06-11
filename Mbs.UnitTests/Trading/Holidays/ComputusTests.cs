using System;
using System.Globalization;
using Mbs.Trading.Holidays;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Holidays
{
    [TestClass]
    public class ComputusTests
    {
        private static readonly DateTime[] WesternEasterSunday =
        {
            new DateTime(1583, 4, 10), new DateTime(1584, 4, 1), new DateTime(1585, 4, 21), new DateTime(1586, 4, 6), new DateTime(1587, 3, 29),
            new DateTime(1588, 4, 17), new DateTime(1589, 4, 2), new DateTime(1590, 4, 22), new DateTime(1591, 4, 14), new DateTime(1592, 3, 29),
            new DateTime(1593, 4, 18), new DateTime(1594, 4, 10), new DateTime(1595, 3, 26), new DateTime(1596, 4, 14), new DateTime(1597, 4, 6),
            new DateTime(1598, 3, 22), new DateTime(1599, 4, 11), new DateTime(1600, 4, 2), new DateTime(1601, 4, 22), new DateTime(1602, 4, 7),
            new DateTime(1603, 3, 30), new DateTime(1604, 4, 18), new DateTime(1605, 4, 10), new DateTime(1606, 3, 26), new DateTime(1607, 4, 15),
            new DateTime(1608, 4, 6), new DateTime(1609, 4, 19), new DateTime(1610, 4, 11), new DateTime(1611, 4, 3), new DateTime(1612, 4, 22),
            new DateTime(1613, 4, 7), new DateTime(1614, 3, 30), new DateTime(1615, 4, 19), new DateTime(1616, 4, 3), new DateTime(1617, 3, 26),
            new DateTime(1618, 4, 15), new DateTime(1619, 3, 31), new DateTime(1620, 4, 19), new DateTime(1621, 4, 11), new DateTime(1622, 3, 27),
            new DateTime(1623, 4, 16), new DateTime(1624, 4, 7), new DateTime(1625, 3, 30), new DateTime(1626, 4, 12), new DateTime(1627, 4, 4),
            new DateTime(1628, 4, 23), new DateTime(1629, 4, 15), new DateTime(1630, 3, 31), new DateTime(1631, 4, 20), new DateTime(1632, 4, 11),
            new DateTime(1633, 3, 27), new DateTime(1634, 4, 16), new DateTime(1635, 4, 8), new DateTime(1636, 3, 23), new DateTime(1637, 4, 12),
            new DateTime(1638, 4, 4), new DateTime(1639, 4, 24), new DateTime(1640, 4, 8), new DateTime(1641, 3, 31), new DateTime(1642, 4, 20),
            new DateTime(1643, 4, 5), new DateTime(1644, 3, 27), new DateTime(1645, 4, 16), new DateTime(1646, 4, 1), new DateTime(1647, 4, 21),
            new DateTime(1648, 4, 12), new DateTime(1649, 4, 4), new DateTime(1650, 4, 17), new DateTime(1651, 4, 9), new DateTime(1652, 3, 31),
            new DateTime(1653, 4, 13), new DateTime(1654, 4, 5), new DateTime(1655, 3, 28), new DateTime(1656, 4, 16), new DateTime(1657, 4, 1),
            new DateTime(1658, 4, 21), new DateTime(1659, 4, 13), new DateTime(1660, 3, 28), new DateTime(1661, 4, 17), new DateTime(1662, 4, 9),
            new DateTime(1663, 3, 25), new DateTime(1664, 4, 13), new DateTime(1665, 4, 5), new DateTime(1666, 4, 25), new DateTime(1667, 4, 10),
            new DateTime(1668, 4, 1), new DateTime(1669, 4, 21), new DateTime(1670, 4, 6), new DateTime(1671, 3, 29), new DateTime(1672, 4, 17),
            new DateTime(1673, 4, 2), new DateTime(1674, 3, 25), new DateTime(1675, 4, 14), new DateTime(1676, 4, 5), new DateTime(1677, 4, 18),
            new DateTime(1678, 4, 10), new DateTime(1679, 4, 2), new DateTime(1680, 4, 21), new DateTime(1681, 4, 6), new DateTime(1682, 3, 29),
            new DateTime(1683, 4, 18), new DateTime(1684, 4, 2), new DateTime(1685, 4, 22), new DateTime(1686, 4, 14), new DateTime(1687, 3, 30),
            new DateTime(1688, 4, 18), new DateTime(1689, 4, 10), new DateTime(1690, 3, 26), new DateTime(1691, 4, 15), new DateTime(1692, 4, 6),
            new DateTime(1693, 3, 22), new DateTime(1694, 4, 11), new DateTime(1695, 4, 3), new DateTime(1696, 4, 22), new DateTime(1697, 4, 7),
            new DateTime(1698, 3, 30), new DateTime(1699, 4, 19), new DateTime(1700, 4, 11), new DateTime(1701, 3, 27), new DateTime(1702, 4, 16),
            new DateTime(1703, 4, 8), new DateTime(1704, 3, 23), new DateTime(1705, 4, 12), new DateTime(1706, 4, 4), new DateTime(1707, 4, 24),
            new DateTime(1708, 4, 8), new DateTime(1709, 3, 31), new DateTime(1710, 4, 20), new DateTime(1711, 4, 5), new DateTime(1712, 3, 27),
            new DateTime(1713, 4, 16), new DateTime(1714, 4, 1), new DateTime(1715, 4, 21), new DateTime(1716, 4, 12), new DateTime(1717, 3, 28),
            new DateTime(1718, 4, 17), new DateTime(1719, 4, 9), new DateTime(1720, 3, 31), new DateTime(1721, 4, 13), new DateTime(1722, 4, 5),
            new DateTime(1723, 3, 28), new DateTime(1724, 4, 16), new DateTime(1725, 4, 1), new DateTime(1726, 4, 21), new DateTime(1727, 4, 13),
            new DateTime(1728, 3, 28), new DateTime(1729, 4, 17), new DateTime(1730, 4, 9), new DateTime(1731, 3, 25), new DateTime(1732, 4, 13),
            new DateTime(1733, 4, 5), new DateTime(1734, 4, 25), new DateTime(1735, 4, 10), new DateTime(1736, 4, 1), new DateTime(1737, 4, 21),
            new DateTime(1738, 4, 6), new DateTime(1739, 3, 29), new DateTime(1740, 4, 17), new DateTime(1741, 4, 2), new DateTime(1742, 3, 25),
            new DateTime(1743, 4, 14), new DateTime(1744, 4, 5), new DateTime(1745, 4, 18), new DateTime(1746, 4, 10), new DateTime(1747, 4, 2),
            new DateTime(1748, 4, 14), new DateTime(1749, 4, 6), new DateTime(1750, 3, 29), new DateTime(1751, 4, 11), new DateTime(1752, 4, 2),
            new DateTime(1753, 4, 22), new DateTime(1754, 4, 14), new DateTime(1755, 3, 30), new DateTime(1756, 4, 18), new DateTime(1757, 4, 10),
            new DateTime(1758, 3, 26), new DateTime(1759, 4, 15), new DateTime(1760, 4, 6), new DateTime(1761, 3, 22), new DateTime(1762, 4, 11),
            new DateTime(1763, 4, 3), new DateTime(1764, 4, 22), new DateTime(1765, 4, 7), new DateTime(1766, 3, 30), new DateTime(1767, 4, 19),
            new DateTime(1768, 4, 3), new DateTime(1769, 3, 26), new DateTime(1770, 4, 15), new DateTime(1771, 3, 31), new DateTime(1772, 4, 19),
            new DateTime(1773, 4, 11), new DateTime(1774, 4, 3), new DateTime(1775, 4, 16), new DateTime(1776, 4, 7), new DateTime(1777, 3, 30),
            new DateTime(1778, 4, 19), new DateTime(1779, 4, 4), new DateTime(1780, 3, 26), new DateTime(1781, 4, 15), new DateTime(1782, 3, 31),
            new DateTime(1783, 4, 20), new DateTime(1784, 4, 11), new DateTime(1785, 3, 27), new DateTime(1786, 4, 16), new DateTime(1787, 4, 8),
            new DateTime(1788, 3, 23), new DateTime(1789, 4, 12), new DateTime(1790, 4, 4), new DateTime(1791, 4, 24), new DateTime(1792, 4, 8),
            new DateTime(1793, 3, 31), new DateTime(1794, 4, 20), new DateTime(1795, 4, 5), new DateTime(1796, 3, 27), new DateTime(1797, 4, 16),
            new DateTime(1798, 4, 8), new DateTime(1799, 3, 24), new DateTime(1800, 4, 13), new DateTime(1801, 4, 5), new DateTime(1802, 4, 18),
            new DateTime(1803, 4, 10), new DateTime(1804, 4, 1), new DateTime(1805, 4, 14), new DateTime(1806, 4, 6), new DateTime(1807, 3, 29),
            new DateTime(1808, 4, 17), new DateTime(1809, 4, 2), new DateTime(1810, 4, 22), new DateTime(1811, 4, 14), new DateTime(1812, 3, 29),
            new DateTime(1813, 4, 18), new DateTime(1814, 4, 10), new DateTime(1815, 3, 26), new DateTime(1816, 4, 14), new DateTime(1817, 4, 6),
            new DateTime(1818, 3, 22), new DateTime(1819, 4, 11), new DateTime(1820, 4, 2), new DateTime(1821, 4, 22), new DateTime(1822, 4, 7),
            new DateTime(1823, 3, 30), new DateTime(1824, 4, 18), new DateTime(1825, 4, 3), new DateTime(1826, 3, 26), new DateTime(1827, 4, 15),
            new DateTime(1828, 4, 6), new DateTime(1829, 4, 19), new DateTime(1830, 4, 11), new DateTime(1831, 4, 3), new DateTime(1832, 4, 22),
            new DateTime(1833, 4, 7), new DateTime(1834, 3, 30), new DateTime(1835, 4, 19), new DateTime(1836, 4, 3), new DateTime(1837, 3, 26),
            new DateTime(1838, 4, 15), new DateTime(1839, 3, 31), new DateTime(1840, 4, 19), new DateTime(1841, 4, 11), new DateTime(1842, 3, 27),
            new DateTime(1843, 4, 16), new DateTime(1844, 4, 7), new DateTime(1845, 3, 23), new DateTime(1846, 4, 12), new DateTime(1847, 4, 4),
            new DateTime(1848, 4, 23), new DateTime(1849, 4, 8), new DateTime(1850, 3, 31), new DateTime(1851, 4, 20), new DateTime(1852, 4, 11),
            new DateTime(1853, 3, 27), new DateTime(1854, 4, 16), new DateTime(1855, 4, 8), new DateTime(1856, 3, 23), new DateTime(1857, 4, 12),
            new DateTime(1858, 4, 4), new DateTime(1859, 4, 24), new DateTime(1860, 4, 8), new DateTime(1861, 3, 31), new DateTime(1862, 4, 20),
            new DateTime(1863, 4, 5), new DateTime(1864, 3, 27), new DateTime(1865, 4, 16), new DateTime(1866, 4, 1), new DateTime(1867, 4, 21),
            new DateTime(1868, 4, 12), new DateTime(1869, 3, 28), new DateTime(1870, 4, 17), new DateTime(1871, 4, 9), new DateTime(1872, 3, 31),
            new DateTime(1873, 4, 13), new DateTime(1874, 4, 5), new DateTime(1875, 3, 28), new DateTime(1876, 4, 16), new DateTime(1877, 4, 1),
            new DateTime(1878, 4, 21), new DateTime(1879, 4, 13), new DateTime(1880, 3, 28), new DateTime(1881, 4, 17), new DateTime(1882, 4, 9),
            new DateTime(1883, 3, 25), new DateTime(1884, 4, 13), new DateTime(1885, 4, 5), new DateTime(1886, 4, 25), new DateTime(1887, 4, 10),
            new DateTime(1888, 4, 1), new DateTime(1889, 4, 21), new DateTime(1890, 4, 6), new DateTime(1891, 3, 29), new DateTime(1892, 4, 17),
            new DateTime(1893, 4, 2), new DateTime(1894, 3, 25), new DateTime(1895, 4, 14), new DateTime(1896, 4, 5), new DateTime(1897, 4, 18),
            new DateTime(1898, 4, 10), new DateTime(1899, 4, 2), new DateTime(1900, 4, 15), new DateTime(1901, 4, 7), new DateTime(1902, 3, 30),
            new DateTime(1903, 4, 12), new DateTime(1904, 4, 3), new DateTime(1905, 4, 23), new DateTime(1906, 4, 15), new DateTime(1907, 3, 31),
            new DateTime(1908, 4, 19), new DateTime(1909, 4, 11), new DateTime(1910, 3, 27), new DateTime(1911, 4, 16), new DateTime(1912, 4, 7),
            new DateTime(1913, 3, 23), new DateTime(1914, 4, 12), new DateTime(1915, 4, 4), new DateTime(1916, 4, 23), new DateTime(1917, 4, 8),
            new DateTime(1918, 3, 31), new DateTime(1919, 4, 20), new DateTime(1920, 4, 4), new DateTime(1921, 3, 27), new DateTime(1922, 4, 16),
            new DateTime(1923, 4, 1), new DateTime(1924, 4, 20), new DateTime(1925, 4, 12), new DateTime(1926, 4, 4), new DateTime(1927, 4, 17),
            new DateTime(1928, 4, 8), new DateTime(1929, 3, 31), new DateTime(1930, 4, 20), new DateTime(1931, 4, 5), new DateTime(1932, 3, 27),
            new DateTime(1933, 4, 16), new DateTime(1934, 4, 1), new DateTime(1935, 4, 21), new DateTime(1936, 4, 12), new DateTime(1937, 3, 28),
            new DateTime(1938, 4, 17), new DateTime(1939, 4, 9), new DateTime(1940, 3, 24), new DateTime(1941, 4, 13), new DateTime(1942, 4, 5),
            new DateTime(1943, 4, 25), new DateTime(1944, 4, 9), new DateTime(1945, 4, 1), new DateTime(1946, 4, 21), new DateTime(1947, 4, 6),
            new DateTime(1948, 3, 28), new DateTime(1949, 4, 17), new DateTime(1950, 4, 9), new DateTime(1951, 3, 25), new DateTime(1952, 4, 13),
            new DateTime(1953, 4, 5), new DateTime(1954, 4, 18), new DateTime(1955, 4, 10), new DateTime(1956, 4, 1), new DateTime(1957, 4, 21),
            new DateTime(1958, 4, 6), new DateTime(1959, 3, 29), new DateTime(1960, 4, 17), new DateTime(1961, 4, 2), new DateTime(1962, 4, 22),
            new DateTime(1963, 4, 14), new DateTime(1964, 3, 29), new DateTime(1965, 4, 18), new DateTime(1966, 4, 10), new DateTime(1967, 3, 26),
            new DateTime(1968, 4, 14), new DateTime(1969, 4, 6), new DateTime(1970, 3, 29), new DateTime(1971, 4, 11), new DateTime(1972, 4, 2),
            new DateTime(1973, 4, 22), new DateTime(1974, 4, 14), new DateTime(1975, 3, 30), new DateTime(1976, 4, 18), new DateTime(1977, 4, 10),
            new DateTime(1978, 3, 26), new DateTime(1979, 4, 15), new DateTime(1980, 4, 6), new DateTime(1981, 4, 19), new DateTime(1982, 4, 11),
            new DateTime(1983, 4, 3), new DateTime(1984, 4, 22), new DateTime(1985, 4, 7), new DateTime(1986, 3, 30), new DateTime(1987, 4, 19),
            new DateTime(1988, 4, 3), new DateTime(1989, 3, 26), new DateTime(1990, 4, 15), new DateTime(1991, 3, 31), new DateTime(1992, 4, 19),
            new DateTime(1993, 4, 11), new DateTime(1994, 4, 3), new DateTime(1995, 4, 16), new DateTime(1996, 4, 7), new DateTime(1997, 3, 30),
            new DateTime(1998, 4, 12), new DateTime(1999, 4, 4), new DateTime(2000, 4, 23), new DateTime(2001, 4, 15), new DateTime(2002, 3, 31),
            new DateTime(2003, 4, 20), new DateTime(2004, 4, 11), new DateTime(2005, 3, 27), new DateTime(2006, 4, 16), new DateTime(2007, 4, 8),
            new DateTime(2008, 3, 23), new DateTime(2009, 4, 12), new DateTime(2010, 4, 4), new DateTime(2011, 4, 24), new DateTime(2012, 4, 8),
            new DateTime(2013, 3, 31), new DateTime(2014, 4, 20), new DateTime(2015, 4, 5), new DateTime(2016, 3, 27), new DateTime(2017, 4, 16),
            new DateTime(2018, 4, 1), new DateTime(2019, 4, 21), new DateTime(2020, 4, 12), new DateTime(2021, 4, 4), new DateTime(2022, 4, 17),
            new DateTime(2023, 4, 9), new DateTime(2024, 3, 31), new DateTime(2025, 4, 20), new DateTime(2026, 4, 5), new DateTime(2027, 3, 28),
            new DateTime(2028, 4, 16), new DateTime(2029, 4, 1), new DateTime(2030, 4, 21), new DateTime(2031, 4, 13), new DateTime(2032, 3, 28),
            new DateTime(2033, 4, 17), new DateTime(2034, 4, 9), new DateTime(2035, 3, 25), new DateTime(2036, 4, 13), new DateTime(2037, 4, 5),
            new DateTime(2038, 4, 25), new DateTime(2039, 4, 10), new DateTime(2040, 4, 1), new DateTime(2041, 4, 21), new DateTime(2042, 4, 6),
            new DateTime(2043, 3, 29), new DateTime(2044, 4, 17), new DateTime(2045, 4, 9), new DateTime(2046, 3, 25), new DateTime(2047, 4, 14),
            new DateTime(2048, 4, 5), new DateTime(2049, 4, 18), new DateTime(2050, 4, 10), new DateTime(2051, 4, 2), new DateTime(2052, 4, 21),
            new DateTime(2053, 4, 6), new DateTime(2054, 3, 29), new DateTime(2055, 4, 18), new DateTime(2056, 4, 2), new DateTime(2057, 4, 22),
            new DateTime(2058, 4, 14), new DateTime(2059, 3, 30), new DateTime(2060, 4, 18), new DateTime(2061, 4, 10), new DateTime(2062, 3, 26),
            new DateTime(2063, 4, 15), new DateTime(2064, 4, 6), new DateTime(2065, 3, 29), new DateTime(2066, 4, 11), new DateTime(2067, 4, 3),
            new DateTime(2068, 4, 22), new DateTime(2069, 4, 14), new DateTime(2070, 3, 30), new DateTime(2071, 4, 19), new DateTime(2072, 4, 10),
            new DateTime(2073, 3, 26), new DateTime(2074, 4, 15), new DateTime(2075, 4, 7), new DateTime(2076, 4, 19), new DateTime(2077, 4, 11),
            new DateTime(2078, 4, 3), new DateTime(2079, 4, 23), new DateTime(2080, 4, 7), new DateTime(2081, 3, 30), new DateTime(2082, 4, 19),
            new DateTime(2083, 4, 4), new DateTime(2084, 3, 26), new DateTime(2085, 4, 15), new DateTime(2086, 3, 31), new DateTime(2087, 4, 20),
            new DateTime(2088, 4, 11), new DateTime(2089, 4, 3), new DateTime(2090, 4, 16), new DateTime(2091, 4, 8), new DateTime(2092, 3, 30),
            new DateTime(2093, 4, 12), new DateTime(2094, 4, 4), new DateTime(2095, 4, 24), new DateTime(2096, 4, 15), new DateTime(2097, 3, 31),
            new DateTime(2098, 4, 20), new DateTime(2099, 4, 12), new DateTime(2100, 3, 28), new DateTime(2101, 4, 17), new DateTime(2102, 4, 9),
            new DateTime(2103, 3, 25), new DateTime(2104, 4, 13), new DateTime(2105, 4, 5), new DateTime(2106, 4, 18), new DateTime(2107, 4, 10),
            new DateTime(2108, 4, 1), new DateTime(2109, 4, 21), new DateTime(2110, 4, 6), new DateTime(2111, 3, 29), new DateTime(2112, 4, 17),
            new DateTime(2113, 4, 2), new DateTime(2114, 4, 22), new DateTime(2115, 4, 14), new DateTime(2116, 3, 29), new DateTime(2117, 4, 18),
            new DateTime(2118, 4, 10), new DateTime(2119, 3, 26), new DateTime(2120, 4, 14), new DateTime(2121, 4, 6), new DateTime(2122, 3, 29),
            new DateTime(2123, 4, 11), new DateTime(2124, 4, 2), new DateTime(2125, 4, 22), new DateTime(2126, 4, 14), new DateTime(2127, 3, 30),
            new DateTime(2128, 4, 18), new DateTime(2129, 4, 10), new DateTime(2130, 3, 26), new DateTime(2131, 4, 15), new DateTime(2132, 4, 6),
            new DateTime(2133, 4, 19), new DateTime(2134, 4, 11), new DateTime(2135, 4, 3), new DateTime(2136, 4, 22), new DateTime(2137, 4, 7),
            new DateTime(2138, 3, 30), new DateTime(2139, 4, 19), new DateTime(2140, 4, 3), new DateTime(2141, 3, 26), new DateTime(2142, 4, 15),
            new DateTime(2143, 3, 31), new DateTime(2144, 4, 19), new DateTime(2145, 4, 11), new DateTime(2146, 4, 3), new DateTime(2147, 4, 16),
            new DateTime(2148, 4, 7), new DateTime(2149, 3, 30), new DateTime(2150, 4, 12), new DateTime(2151, 4, 4), new DateTime(2152, 4, 23),
            new DateTime(2153, 4, 15), new DateTime(2154, 3, 31), new DateTime(2155, 4, 20), new DateTime(2156, 4, 11), new DateTime(2157, 3, 27),
            new DateTime(2158, 4, 16), new DateTime(2159, 4, 8), new DateTime(2160, 3, 23), new DateTime(2161, 4, 12), new DateTime(2162, 4, 4),
            new DateTime(2163, 4, 24), new DateTime(2164, 4, 8), new DateTime(2165, 3, 31), new DateTime(2166, 4, 20), new DateTime(2167, 4, 5),
            new DateTime(2168, 3, 27), new DateTime(2169, 4, 16), new DateTime(2170, 4, 1), new DateTime(2171, 4, 21), new DateTime(2172, 4, 12),
            new DateTime(2173, 4, 4), new DateTime(2174, 4, 17), new DateTime(2175, 4, 9), new DateTime(2176, 3, 31), new DateTime(2177, 4, 20),
            new DateTime(2178, 4, 5), new DateTime(2179, 3, 28), new DateTime(2180, 4, 16), new DateTime(2181, 4, 1), new DateTime(2182, 4, 21),
            new DateTime(2183, 4, 13), new DateTime(2184, 3, 28), new DateTime(2185, 4, 17), new DateTime(2186, 4, 9), new DateTime(2187, 3, 25),
            new DateTime(2188, 4, 13), new DateTime(2189, 4, 5), new DateTime(2190, 4, 25), new DateTime(2191, 4, 10), new DateTime(2192, 4, 1),
            new DateTime(2193, 4, 21), new DateTime(2194, 4, 6), new DateTime(2195, 3, 29), new DateTime(2196, 4, 17), new DateTime(2197, 4, 9),
            new DateTime(2198, 3, 25), new DateTime(2199, 4, 14), new DateTime(2200, 4, 6), new DateTime(2201, 4, 19), new DateTime(2202, 4, 11),
            new DateTime(2203, 4, 3), new DateTime(2204, 4, 22), new DateTime(2205, 4, 7), new DateTime(2206, 3, 30), new DateTime(2207, 4, 19),
            new DateTime(2208, 4, 3), new DateTime(2209, 3, 26), new DateTime(2210, 4, 15), new DateTime(2211, 3, 31), new DateTime(2212, 4, 19),
            new DateTime(2213, 4, 11), new DateTime(2214, 3, 27), new DateTime(2215, 4, 16), new DateTime(2216, 4, 7), new DateTime(2217, 3, 30),
            new DateTime(2218, 4, 12), new DateTime(2219, 4, 4), new DateTime(2220, 4, 23), new DateTime(2221, 4, 15), new DateTime(2222, 3, 31),
            new DateTime(2223, 4, 20), new DateTime(2224, 4, 11), new DateTime(2225, 3, 27), new DateTime(2226, 4, 16), new DateTime(2227, 4, 8),
            new DateTime(2228, 3, 23), new DateTime(2229, 4, 12), new DateTime(2230, 4, 4), new DateTime(2231, 4, 24), new DateTime(2232, 4, 8),
            new DateTime(2233, 3, 31), new DateTime(2234, 4, 20), new DateTime(2235, 4, 5), new DateTime(2236, 3, 27), new DateTime(2237, 4, 16),
            new DateTime(2238, 4, 1), new DateTime(2239, 4, 21), new DateTime(2240, 4, 12), new DateTime(2241, 4, 4), new DateTime(2242, 4, 17),
            new DateTime(2243, 4, 9), new DateTime(2244, 3, 31), new DateTime(2245, 4, 13), new DateTime(2246, 4, 5), new DateTime(2247, 3, 28),
            new DateTime(2248, 4, 16), new DateTime(2249, 4, 1), new DateTime(2250, 4, 21), new DateTime(2251, 4, 13), new DateTime(2252, 3, 28),
            new DateTime(2253, 4, 17), new DateTime(2254, 4, 9), new DateTime(2255, 3, 25), new DateTime(2256, 4, 13), new DateTime(2257, 4, 5),
            new DateTime(2258, 4, 25), new DateTime(2259, 4, 10), new DateTime(2260, 4, 1), new DateTime(2261, 4, 21), new DateTime(2262, 4, 6),
            new DateTime(2263, 3, 29), new DateTime(2264, 4, 17), new DateTime(2265, 4, 2), new DateTime(2266, 3, 25), new DateTime(2267, 4, 14),
            new DateTime(2268, 4, 5), new DateTime(2269, 4, 18), new DateTime(2270, 4, 10), new DateTime(2271, 4, 2), new DateTime(2272, 4, 21),
            new DateTime(2273, 4, 6), new DateTime(2274, 3, 29), new DateTime(2275, 4, 18), new DateTime(2276, 4, 2), new DateTime(2277, 4, 22),
            new DateTime(2278, 4, 14), new DateTime(2279, 3, 30), new DateTime(2280, 4, 18), new DateTime(2281, 4, 10), new DateTime(2282, 3, 26),
            new DateTime(2283, 4, 15), new DateTime(2284, 4, 6), new DateTime(2285, 3, 22), new DateTime(2286, 4, 11), new DateTime(2287, 4, 3),
            new DateTime(2288, 4, 22), new DateTime(2289, 4, 7), new DateTime(2290, 3, 30), new DateTime(2291, 4, 19), new DateTime(2292, 4, 10),
            new DateTime(2293, 3, 26), new DateTime(2294, 4, 15), new DateTime(2295, 4, 7), new DateTime(2296, 4, 19), new DateTime(2297, 4, 11),
            new DateTime(2298, 4, 3), new DateTime(2299, 4, 16), new DateTime(2300, 4, 8), new DateTime(2301, 3, 31), new DateTime(2302, 4, 20),
            new DateTime(2303, 4, 5), new DateTime(2304, 3, 27), new DateTime(2305, 4, 16), new DateTime(2306, 4, 1), new DateTime(2307, 4, 21),
            new DateTime(2308, 4, 12), new DateTime(2309, 3, 28), new DateTime(2310, 4, 17), new DateTime(2311, 4, 9), new DateTime(2312, 3, 31),
            new DateTime(2313, 4, 13), new DateTime(2314, 4, 5), new DateTime(2315, 3, 28), new DateTime(2316, 4, 16), new DateTime(2317, 4, 1),
            new DateTime(2318, 4, 21), new DateTime(2319, 4, 6), new DateTime(2320, 3, 28), new DateTime(2321, 4, 17), new DateTime(2322, 4, 9),
            new DateTime(2323, 3, 25), new DateTime(2324, 4, 13), new DateTime(2325, 4, 5), new DateTime(2326, 4, 25), new DateTime(2327, 4, 10),
            new DateTime(2328, 4, 1), new DateTime(2329, 4, 21), new DateTime(2330, 4, 6), new DateTime(2331, 3, 29), new DateTime(2332, 4, 17),
            new DateTime(2333, 4, 2), new DateTime(2334, 3, 25), new DateTime(2335, 4, 14), new DateTime(2336, 4, 5), new DateTime(2337, 4, 18),
            new DateTime(2338, 4, 10), new DateTime(2339, 3, 26), new DateTime(2340, 4, 14), new DateTime(2341, 4, 6), new DateTime(2342, 3, 29),
            new DateTime(2343, 4, 11), new DateTime(2344, 4, 2), new DateTime(2345, 4, 22), new DateTime(2346, 4, 14), new DateTime(2347, 3, 30),
            new DateTime(2348, 4, 18), new DateTime(2349, 4, 10), new DateTime(2350, 3, 26), new DateTime(2351, 4, 15), new DateTime(2352, 4, 6),
            new DateTime(2353, 3, 22), new DateTime(2354, 4, 11), new DateTime(2355, 4, 3), new DateTime(2356, 4, 22), new DateTime(2357, 4, 7),
            new DateTime(2358, 3, 30), new DateTime(2359, 4, 19), new DateTime(2360, 4, 3), new DateTime(2361, 3, 26), new DateTime(2362, 4, 15),
            new DateTime(2363, 3, 31), new DateTime(2364, 4, 19), new DateTime(2365, 4, 11), new DateTime(2366, 4, 3), new DateTime(2367, 4, 16),
            new DateTime(2368, 4, 7), new DateTime(2369, 3, 30), new DateTime(2370, 4, 19), new DateTime(2371, 4, 4), new DateTime(2372, 3, 26),
            new DateTime(2373, 4, 15), new DateTime(2374, 3, 31), new DateTime(2375, 4, 20), new DateTime(2376, 4, 11), new DateTime(2377, 3, 27),
            new DateTime(2378, 4, 16), new DateTime(2379, 4, 8), new DateTime(2380, 3, 23), new DateTime(2381, 4, 12), new DateTime(2382, 4, 4),
            new DateTime(2383, 4, 24), new DateTime(2384, 4, 8), new DateTime(2385, 3, 31), new DateTime(2386, 4, 20), new DateTime(2387, 4, 5),
            new DateTime(2388, 3, 27), new DateTime(2389, 4, 16), new DateTime(2390, 4, 8), new DateTime(2391, 3, 24), new DateTime(2392, 4, 12),
            new DateTime(2393, 4, 4), new DateTime(2394, 4, 17), new DateTime(2395, 4, 9), new DateTime(2396, 3, 31), new DateTime(2397, 4, 20),
            new DateTime(2398, 4, 5), new DateTime(2399, 3, 28), new DateTime(2400, 4, 16), new DateTime(2401, 4, 1), new DateTime(2402, 4, 21),
            new DateTime(2403, 4, 13), new DateTime(2404, 3, 28), new DateTime(2405, 4, 17), new DateTime(2406, 4, 9), new DateTime(2407, 3, 25),
            new DateTime(2408, 4, 13), new DateTime(2409, 4, 5), new DateTime(2410, 4, 25), new DateTime(2411, 4, 10), new DateTime(2412, 4, 1),
            new DateTime(2413, 4, 21), new DateTime(2414, 4, 6), new DateTime(2415, 3, 29), new DateTime(2416, 4, 17), new DateTime(2417, 4, 2),
            new DateTime(2418, 3, 25), new DateTime(2419, 4, 14), new DateTime(2420, 4, 5), new DateTime(2421, 4, 18), new DateTime(2422, 4, 10),
            new DateTime(2423, 4, 2), new DateTime(2424, 4, 21), new DateTime(2425, 4, 6), new DateTime(2426, 3, 29), new DateTime(2427, 4, 18),
            new DateTime(2428, 4, 2), new DateTime(2429, 4, 22), new DateTime(2430, 4, 14), new DateTime(2431, 3, 30), new DateTime(2432, 4, 18),
            new DateTime(2433, 4, 10), new DateTime(2434, 3, 26), new DateTime(2435, 4, 15), new DateTime(2436, 4, 6), new DateTime(2437, 3, 22),
            new DateTime(2438, 4, 11), new DateTime(2439, 4, 3), new DateTime(2440, 4, 22), new DateTime(2441, 4, 7), new DateTime(2442, 3, 30),
            new DateTime(2443, 4, 19), new DateTime(2444, 4, 10), new DateTime(2445, 3, 26), new DateTime(2446, 4, 15), new DateTime(2447, 4, 7),
            new DateTime(2448, 4, 19), new DateTime(2449, 4, 11), new DateTime(2450, 4, 3), new DateTime(2451, 4, 16), new DateTime(2452, 4, 7),
            new DateTime(2453, 3, 30), new DateTime(2454, 4, 19), new DateTime(2455, 4, 4), new DateTime(2456, 3, 26), new DateTime(2457, 4, 15),
            new DateTime(2458, 3, 31), new DateTime(2459, 4, 20), new DateTime(2460, 4, 11), new DateTime(2461, 3, 27), new DateTime(2462, 4, 16),
            new DateTime(2463, 4, 8), new DateTime(2464, 3, 30), new DateTime(2465, 4, 12), new DateTime(2466, 4, 4), new DateTime(2467, 4, 24),
            new DateTime(2468, 4, 15), new DateTime(2469, 3, 31), new DateTime(2470, 4, 20), new DateTime(2471, 4, 5), new DateTime(2472, 3, 27),
            new DateTime(2473, 4, 16), new DateTime(2474, 4, 8), new DateTime(2475, 3, 24), new DateTime(2476, 4, 12), new DateTime(2477, 4, 4),
            new DateTime(2478, 4, 24), new DateTime(2479, 4, 9), new DateTime(2480, 3, 31), new DateTime(2481, 4, 20), new DateTime(2482, 4, 5),
            new DateTime(2483, 3, 28), new DateTime(2484, 4, 16), new DateTime(2485, 4, 1), new DateTime(2486, 4, 21), new DateTime(2487, 4, 13),
            new DateTime(2488, 4, 4), new DateTime(2489, 4, 17), new DateTime(2490, 4, 9), new DateTime(2491, 3, 25), new DateTime(2492, 4, 13),
            new DateTime(2493, 4, 5), new DateTime(2494, 3, 28), new DateTime(2495, 4, 10), new DateTime(2496, 4, 1), new DateTime(2497, 4, 21),
            new DateTime(2498, 4, 13), new DateTime(2499, 3, 29), new DateTime(2500, 4, 18),
            new DateTime(4051, 4, 9), new DateTime(4052, 3, 31), new DateTime(4053, 4, 20), new DateTime(4054, 4, 12), new DateTime(4055, 3, 28),
            new DateTime(4056, 4, 16), new DateTime(4057, 4, 8), new DateTime(4058, 3, 24), new DateTime(4059, 4, 13), new DateTime(4060, 4, 4),
            new DateTime(4061, 4, 24), new DateTime(4062, 4, 9), new DateTime(4063, 4, 1), new DateTime(4064, 4, 20), new DateTime(4065, 4, 5),
            new DateTime(4066, 3, 28), new DateTime(4067, 4, 17), new DateTime(4068, 4, 1), new DateTime(4069, 4, 21), new DateTime(4070, 4, 13),
            new DateTime(4071, 3, 29), new DateTime(4072, 4, 17), new DateTime(4073, 4, 9), new DateTime(4074, 4, 1), new DateTime(4075, 4, 14),
            new DateTime(4076, 4, 5), new DateTime(4077, 3, 28), new DateTime(4078, 4, 17), new DateTime(4079, 4, 2), new DateTime(4080, 4, 21),
            new DateTime(4081, 4, 13), new DateTime(4082, 3, 29), new DateTime(4083, 4, 18), new DateTime(4084, 4, 9), new DateTime(4085, 3, 25),
            new DateTime(4086, 4, 14), new DateTime(4087, 4, 6), new DateTime(4088, 4, 25), new DateTime(4089, 4, 10), new DateTime(4090, 4, 2),
            new DateTime(4091, 4, 22), new DateTime(4092, 4, 6), new DateTime(4093, 3, 29), new DateTime(4094, 4, 18), new DateTime(4095, 4, 3),
            new DateTime(4096, 3, 25), new DateTime(4097, 4, 14), new DateTime(4098, 4, 6), new DateTime(4099, 4, 19)
        };

        private static readonly DateTime[] OrthodoxEasterSunday =
        {
            new DateTime(1583, 4, 10), new DateTime(1584, 4, 29), new DateTime(1585, 4, 21), new DateTime(1586, 4, 13), new DateTime(1587, 4, 26),
            new DateTime(1588, 4, 17), new DateTime(1589, 4, 9), new DateTime(1590, 4, 29), new DateTime(1591, 4, 14), new DateTime(1592, 4, 5),
            new DateTime(1593, 4, 25), new DateTime(1594, 4, 10), new DateTime(1595, 4, 30), new DateTime(1596, 4, 21), new DateTime(1597, 4, 6),
            new DateTime(1598, 4, 26), new DateTime(1599, 4, 18), new DateTime(1600, 4, 2), new DateTime(1601, 4, 22), new DateTime(1602, 4, 14),
            new DateTime(1603, 5, 4), new DateTime(1604, 4, 18), new DateTime(1605, 4, 10), new DateTime(1606, 4, 30), new DateTime(1607, 4, 15),
            new DateTime(1608, 4, 6), new DateTime(1609, 4, 26), new DateTime(1610, 4, 18), new DateTime(1611, 4, 3), new DateTime(1612, 4, 22),
            new DateTime(1613, 4, 14), new DateTime(1614, 5, 4), new DateTime(1615, 4, 19), new DateTime(1616, 4, 10), new DateTime(1617, 4, 30),
            new DateTime(1618, 4, 15), new DateTime(1619, 4, 7), new DateTime(1620, 4, 26), new DateTime(1621, 4, 11), new DateTime(1622, 5, 1),
            new DateTime(1623, 4, 23), new DateTime(1624, 4, 7), new DateTime(1625, 4, 27), new DateTime(1626, 4, 19), new DateTime(1627, 4, 4),
            new DateTime(1628, 4, 23), new DateTime(1629, 4, 15), new DateTime(1630, 4, 7), new DateTime(1631, 4, 20), new DateTime(1632, 4, 11),
            new DateTime(1633, 5, 1), new DateTime(1634, 4, 16), new DateTime(1635, 4, 8), new DateTime(1636, 4, 27), new DateTime(1637, 4, 19),
            new DateTime(1638, 4, 4), new DateTime(1639, 4, 24), new DateTime(1640, 4, 15), new DateTime(1641, 5, 5), new DateTime(1642, 4, 20),
            new DateTime(1643, 4, 12), new DateTime(1644, 5, 1), new DateTime(1645, 4, 16), new DateTime(1646, 4, 8), new DateTime(1647, 4, 28),
            new DateTime(1648, 4, 12), new DateTime(1649, 4, 4), new DateTime(1650, 4, 24), new DateTime(1651, 4, 9), new DateTime(1652, 4, 28),
            new DateTime(1653, 4, 20), new DateTime(1654, 4, 5), new DateTime(1655, 4, 25), new DateTime(1656, 4, 16), new DateTime(1657, 4, 8),
            new DateTime(1658, 4, 21), new DateTime(1659, 4, 13), new DateTime(1660, 5, 2), new DateTime(1661, 4, 24), new DateTime(1662, 4, 9),
            new DateTime(1663, 4, 29), new DateTime(1664, 4, 20), new DateTime(1665, 4, 5), new DateTime(1666, 4, 25), new DateTime(1667, 4, 17),
            new DateTime(1668, 4, 1), new DateTime(1669, 4, 21), new DateTime(1670, 4, 13), new DateTime(1671, 5, 3), new DateTime(1672, 4, 17),
            new DateTime(1673, 4, 9), new DateTime(1674, 4, 29), new DateTime(1675, 4, 14), new DateTime(1676, 4, 5), new DateTime(1677, 4, 25),
            new DateTime(1678, 4, 10), new DateTime(1679, 4, 30), new DateTime(1680, 4, 21), new DateTime(1681, 4, 13), new DateTime(1682, 4, 26),
            new DateTime(1683, 4, 18), new DateTime(1684, 4, 9), new DateTime(1685, 4, 29), new DateTime(1686, 4, 14), new DateTime(1687, 4, 6),
            new DateTime(1688, 4, 25), new DateTime(1689, 4, 10), new DateTime(1690, 4, 30), new DateTime(1691, 4, 22), new DateTime(1692, 4, 6),
            new DateTime(1693, 4, 26), new DateTime(1694, 4, 18), new DateTime(1695, 4, 3), new DateTime(1696, 4, 22), new DateTime(1697, 4, 14),
            new DateTime(1698, 5, 4), new DateTime(1699, 4, 19), new DateTime(1700, 4, 11), new DateTime(1701, 5, 1), new DateTime(1702, 4, 16),
            new DateTime(1703, 4, 8), new DateTime(1704, 4, 27), new DateTime(1705, 4, 19), new DateTime(1706, 4, 4), new DateTime(1707, 4, 24),
            new DateTime(1708, 4, 15), new DateTime(1709, 5, 5), new DateTime(1710, 4, 20), new DateTime(1711, 4, 12), new DateTime(1712, 5, 1),
            new DateTime(1713, 4, 16), new DateTime(1714, 4, 8), new DateTime(1715, 4, 28), new DateTime(1716, 4, 12), new DateTime(1717, 5, 2),
            new DateTime(1718, 4, 24), new DateTime(1719, 4, 9), new DateTime(1720, 4, 28), new DateTime(1721, 4, 20), new DateTime(1722, 4, 5),
            new DateTime(1723, 4, 25), new DateTime(1724, 4, 16), new DateTime(1725, 4, 8), new DateTime(1726, 4, 21), new DateTime(1727, 4, 13),
            new DateTime(1728, 5, 2), new DateTime(1729, 4, 17), new DateTime(1730, 4, 9), new DateTime(1731, 4, 29), new DateTime(1732, 4, 20),
            new DateTime(1733, 4, 5), new DateTime(1734, 4, 25), new DateTime(1735, 4, 17), new DateTime(1736, 5, 6), new DateTime(1737, 4, 21),
            new DateTime(1738, 4, 13), new DateTime(1739, 5, 3), new DateTime(1740, 4, 17), new DateTime(1741, 4, 9), new DateTime(1742, 4, 29),
            new DateTime(1743, 4, 14), new DateTime(1744, 4, 5), new DateTime(1745, 4, 25), new DateTime(1746, 4, 10), new DateTime(1747, 4, 30),
            new DateTime(1748, 4, 21), new DateTime(1749, 4, 6), new DateTime(1750, 4, 26), new DateTime(1751, 4, 18), new DateTime(1752, 4, 9),
            new DateTime(1753, 4, 22), new DateTime(1754, 4, 14), new DateTime(1755, 5, 4), new DateTime(1756, 4, 25), new DateTime(1757, 4, 10),
            new DateTime(1758, 4, 30), new DateTime(1759, 4, 22), new DateTime(1760, 4, 6), new DateTime(1761, 4, 26), new DateTime(1762, 4, 18),
            new DateTime(1763, 4, 3), new DateTime(1764, 4, 22), new DateTime(1765, 4, 14), new DateTime(1766, 5, 4), new DateTime(1767, 4, 19),
            new DateTime(1768, 4, 10), new DateTime(1769, 4, 30), new DateTime(1770, 4, 15), new DateTime(1771, 4, 7), new DateTime(1772, 4, 26),
            new DateTime(1773, 4, 11), new DateTime(1774, 5, 1), new DateTime(1775, 4, 23), new DateTime(1776, 4, 14), new DateTime(1777, 4, 27),
            new DateTime(1778, 4, 19), new DateTime(1779, 4, 11), new DateTime(1780, 4, 30), new DateTime(1781, 4, 15), new DateTime(1782, 4, 7),
            new DateTime(1783, 4, 27), new DateTime(1784, 4, 11), new DateTime(1785, 5, 1), new DateTime(1786, 4, 23), new DateTime(1787, 4, 8),
            new DateTime(1788, 4, 27), new DateTime(1789, 4, 19), new DateTime(1790, 4, 4), new DateTime(1791, 4, 24), new DateTime(1792, 4, 15),
            new DateTime(1793, 5, 5), new DateTime(1794, 4, 20), new DateTime(1795, 4, 12), new DateTime(1796, 5, 1), new DateTime(1797, 4, 16),
            new DateTime(1798, 4, 8), new DateTime(1799, 4, 28), new DateTime(1800, 4, 20), new DateTime(1801, 4, 5), new DateTime(1802, 4, 25),
            new DateTime(1803, 4, 17), new DateTime(1804, 5, 6), new DateTime(1805, 4, 21), new DateTime(1806, 4, 13), new DateTime(1807, 4, 26),
            new DateTime(1808, 4, 17), new DateTime(1809, 4, 9), new DateTime(1810, 4, 29), new DateTime(1811, 4, 14), new DateTime(1812, 5, 3),
            new DateTime(1813, 4, 25), new DateTime(1814, 4, 10), new DateTime(1815, 4, 30), new DateTime(1816, 4, 21), new DateTime(1817, 4, 6),
            new DateTime(1818, 4, 26), new DateTime(1819, 4, 18), new DateTime(1820, 4, 9), new DateTime(1821, 4, 22), new DateTime(1822, 4, 14),
            new DateTime(1823, 5, 4), new DateTime(1824, 4, 18), new DateTime(1825, 4, 10), new DateTime(1826, 4, 30), new DateTime(1827, 4, 15),
            new DateTime(1828, 4, 6), new DateTime(1829, 4, 26), new DateTime(1830, 4, 18), new DateTime(1831, 5, 1), new DateTime(1832, 4, 22),
            new DateTime(1833, 4, 14), new DateTime(1834, 5, 4), new DateTime(1835, 4, 19), new DateTime(1836, 4, 10), new DateTime(1837, 4, 30),
            new DateTime(1838, 4, 15), new DateTime(1839, 4, 7), new DateTime(1840, 4, 26), new DateTime(1841, 4, 11), new DateTime(1842, 5, 1),
            new DateTime(1843, 4, 23), new DateTime(1844, 4, 7), new DateTime(1845, 4, 27), new DateTime(1846, 4, 19), new DateTime(1847, 4, 4),
            new DateTime(1848, 4, 23), new DateTime(1849, 4, 15), new DateTime(1850, 5, 5), new DateTime(1851, 4, 20), new DateTime(1852, 4, 11),
            new DateTime(1853, 5, 1), new DateTime(1854, 4, 23), new DateTime(1855, 4, 8), new DateTime(1856, 4, 27), new DateTime(1857, 4, 19),
            new DateTime(1858, 4, 4), new DateTime(1859, 4, 24), new DateTime(1860, 4, 15), new DateTime(1861, 5, 5), new DateTime(1862, 4, 20),
            new DateTime(1863, 4, 12), new DateTime(1864, 5, 1), new DateTime(1865, 4, 16), new DateTime(1866, 4, 8), new DateTime(1867, 4, 28),
            new DateTime(1868, 4, 12), new DateTime(1869, 5, 2), new DateTime(1870, 4, 24), new DateTime(1871, 4, 9), new DateTime(1872, 4, 28),
            new DateTime(1873, 4, 20), new DateTime(1874, 4, 12), new DateTime(1875, 4, 25), new DateTime(1876, 4, 16), new DateTime(1877, 4, 8),
            new DateTime(1878, 4, 28), new DateTime(1879, 4, 13), new DateTime(1880, 5, 2), new DateTime(1881, 4, 24), new DateTime(1882, 4, 9),
            new DateTime(1883, 4, 29), new DateTime(1884, 4, 20), new DateTime(1885, 4, 5), new DateTime(1886, 4, 25), new DateTime(1887, 4, 17),
            new DateTime(1888, 5, 6), new DateTime(1889, 4, 21), new DateTime(1890, 4, 13), new DateTime(1891, 5, 3), new DateTime(1892, 4, 17),
            new DateTime(1893, 4, 9), new DateTime(1894, 4, 29), new DateTime(1895, 4, 14), new DateTime(1896, 4, 5), new DateTime(1897, 4, 25),
            new DateTime(1898, 4, 17), new DateTime(1899, 4, 30), new DateTime(1900, 4, 22), new DateTime(1901, 4, 14), new DateTime(1902, 4, 27),
            new DateTime(1903, 4, 19), new DateTime(1904, 4, 10), new DateTime(1905, 4, 30), new DateTime(1906, 4, 15), new DateTime(1907, 5, 5),
            new DateTime(1908, 4, 26), new DateTime(1909, 4, 11), new DateTime(1910, 5, 1), new DateTime(1911, 4, 23), new DateTime(1912, 4, 7),
            new DateTime(1913, 4, 27), new DateTime(1914, 4, 19), new DateTime(1915, 4, 4), new DateTime(1916, 4, 23), new DateTime(1917, 4, 15),
            new DateTime(1918, 5, 5), new DateTime(1919, 4, 20), new DateTime(1920, 4, 11), new DateTime(1921, 5, 1), new DateTime(1922, 4, 16),
            new DateTime(1923, 4, 8), new DateTime(1924, 4, 27), new DateTime(1925, 4, 19), new DateTime(1926, 5, 2), new DateTime(1927, 4, 24),
            new DateTime(1928, 4, 15), new DateTime(1929, 5, 5), new DateTime(1930, 4, 20), new DateTime(1931, 4, 12), new DateTime(1932, 5, 1),
            new DateTime(1933, 4, 16), new DateTime(1934, 4, 8), new DateTime(1935, 4, 28), new DateTime(1936, 4, 12), new DateTime(1937, 5, 2),
            new DateTime(1938, 4, 24), new DateTime(1939, 4, 9), new DateTime(1940, 4, 28), new DateTime(1941, 4, 20), new DateTime(1942, 4, 5),
            new DateTime(1943, 4, 25), new DateTime(1944, 4, 16), new DateTime(1945, 5, 6), new DateTime(1946, 4, 21), new DateTime(1947, 4, 13),
            new DateTime(1948, 5, 2), new DateTime(1949, 4, 24), new DateTime(1950, 4, 9), new DateTime(1951, 4, 29), new DateTime(1952, 4, 20),
            new DateTime(1953, 4, 5), new DateTime(1954, 4, 25), new DateTime(1955, 4, 17), new DateTime(1956, 5, 6), new DateTime(1957, 4, 21),
            new DateTime(1958, 4, 13), new DateTime(1959, 5, 3), new DateTime(1960, 4, 17), new DateTime(1961, 4, 9), new DateTime(1962, 4, 29),
            new DateTime(1963, 4, 14), new DateTime(1964, 5, 3), new DateTime(1965, 4, 25), new DateTime(1966, 4, 10), new DateTime(1967, 4, 30),
            new DateTime(1968, 4, 21), new DateTime(1969, 4, 13), new DateTime(1970, 4, 26), new DateTime(1971, 4, 18), new DateTime(1972, 4, 9),
            new DateTime(1973, 4, 29), new DateTime(1974, 4, 14), new DateTime(1975, 5, 4), new DateTime(1976, 4, 25), new DateTime(1977, 4, 10),
            new DateTime(1978, 4, 30), new DateTime(1979, 4, 22), new DateTime(1980, 4, 6), new DateTime(1981, 4, 26), new DateTime(1982, 4, 18),
            new DateTime(1983, 5, 8), new DateTime(1984, 4, 22), new DateTime(1985, 4, 14), new DateTime(1986, 5, 4), new DateTime(1987, 4, 19),
            new DateTime(1988, 4, 10), new DateTime(1989, 4, 30), new DateTime(1990, 4, 15), new DateTime(1991, 4, 7), new DateTime(1992, 4, 26),
            new DateTime(1993, 4, 18), new DateTime(1994, 5, 1), new DateTime(1995, 4, 23), new DateTime(1996, 4, 14), new DateTime(1997, 4, 27),
            new DateTime(1998, 4, 19), new DateTime(1999, 4, 11), new DateTime(2000, 4, 30), new DateTime(2001, 4, 15), new DateTime(2002, 5, 5),
            new DateTime(2003, 4, 27), new DateTime(2004, 4, 11), new DateTime(2005, 5, 1), new DateTime(2006, 4, 23), new DateTime(2007, 4, 8),
            new DateTime(2008, 4, 27), new DateTime(2009, 4, 19), new DateTime(2010, 4, 4), new DateTime(2011, 4, 24), new DateTime(2012, 4, 15),
            new DateTime(2013, 5, 5), new DateTime(2014, 4, 20), new DateTime(2015, 4, 12), new DateTime(2016, 5, 1), new DateTime(2017, 4, 16),
            new DateTime(2018, 4, 8), new DateTime(2019, 4, 28), new DateTime(2020, 4, 19), new DateTime(2021, 5, 2), new DateTime(2022, 4, 24),
            new DateTime(2023, 4, 16), new DateTime(2024, 5, 5), new DateTime(2025, 4, 20), new DateTime(2026, 4, 12), new DateTime(2027, 5, 2),
            new DateTime(2028, 4, 16), new DateTime(2029, 4, 8), new DateTime(2030, 4, 28), new DateTime(2031, 4, 13), new DateTime(2032, 5, 2),
            new DateTime(2033, 4, 24), new DateTime(2034, 4, 9), new DateTime(2035, 4, 29), new DateTime(2036, 4, 20), new DateTime(2037, 4, 5),
            new DateTime(2038, 4, 25), new DateTime(2039, 4, 17), new DateTime(2040, 5, 6), new DateTime(2041, 4, 21), new DateTime(2042, 4, 13),
            new DateTime(2043, 5, 3), new DateTime(2044, 4, 24), new DateTime(2045, 4, 9), new DateTime(2046, 4, 29), new DateTime(2047, 4, 21),
            new DateTime(2048, 4, 5), new DateTime(2049, 4, 25), new DateTime(2050, 4, 17), new DateTime(2051, 5, 7), new DateTime(2052, 4, 21),
            new DateTime(2053, 4, 13), new DateTime(2054, 5, 3), new DateTime(2055, 4, 18), new DateTime(2056, 4, 9), new DateTime(2057, 4, 29),
            new DateTime(2058, 4, 14), new DateTime(2059, 5, 4), new DateTime(2060, 4, 25), new DateTime(2061, 4, 10), new DateTime(2062, 4, 30),
            new DateTime(2063, 4, 22), new DateTime(2064, 4, 13), new DateTime(2065, 4, 26), new DateTime(2066, 4, 18), new DateTime(2067, 4, 10),
            new DateTime(2068, 4, 29), new DateTime(2069, 4, 14), new DateTime(2070, 5, 4), new DateTime(2071, 4, 19), new DateTime(2072, 4, 10),
            new DateTime(2073, 4, 30), new DateTime(2074, 4, 22), new DateTime(2075, 4, 7), new DateTime(2076, 4, 26), new DateTime(2077, 4, 18),
            new DateTime(2078, 5, 8), new DateTime(2079, 4, 23), new DateTime(2080, 4, 14), new DateTime(2081, 5, 4), new DateTime(2082, 4, 19),
            new DateTime(2083, 4, 11), new DateTime(2084, 4, 30), new DateTime(2085, 4, 15), new DateTime(2086, 4, 7), new DateTime(2087, 4, 27),
            new DateTime(2088, 4, 18), new DateTime(2089, 5, 1), new DateTime(2090, 4, 23), new DateTime(2091, 4, 8), new DateTime(2092, 4, 27),
            new DateTime(2093, 4, 19), new DateTime(2094, 4, 11), new DateTime(2095, 4, 24), new DateTime(2096, 4, 15), new DateTime(2097, 5, 5),
            new DateTime(2098, 4, 27), new DateTime(2099, 4, 12), new DateTime(2100, 5, 2), new DateTime(2101, 4, 24), new DateTime(2102, 4, 9),
            new DateTime(2103, 4, 29), new DateTime(2104, 4, 20), new DateTime(2105, 4, 5), new DateTime(2106, 4, 25), new DateTime(2107, 4, 17),
            new DateTime(2108, 5, 6), new DateTime(2109, 4, 21), new DateTime(2110, 4, 13), new DateTime(2111, 5, 3), new DateTime(2112, 4, 17),
            new DateTime(2113, 4, 9), new DateTime(2114, 4, 29), new DateTime(2115, 4, 14), new DateTime(2116, 5, 3), new DateTime(2117, 4, 25),
            new DateTime(2118, 4, 17), new DateTime(2119, 4, 30), new DateTime(2120, 4, 21), new DateTime(2121, 4, 13), new DateTime(2122, 5, 3),
            new DateTime(2123, 4, 18), new DateTime(2124, 4, 9), new DateTime(2125, 4, 29), new DateTime(2126, 4, 14), new DateTime(2127, 5, 4),
            new DateTime(2128, 4, 25), new DateTime(2129, 4, 10), new DateTime(2130, 4, 30), new DateTime(2131, 4, 22), new DateTime(2132, 4, 6),
            new DateTime(2133, 4, 26), new DateTime(2134, 4, 18), new DateTime(2135, 5, 8), new DateTime(2136, 4, 22), new DateTime(2137, 4, 14),
            new DateTime(2138, 5, 4), new DateTime(2139, 4, 19), new DateTime(2140, 4, 10), new DateTime(2141, 4, 30), new DateTime(2142, 4, 22),
            new DateTime(2143, 4, 7), new DateTime(2144, 4, 26), new DateTime(2145, 4, 18), new DateTime(2146, 5, 8), new DateTime(2147, 4, 23),
            new DateTime(2148, 4, 14), new DateTime(2149, 5, 4), new DateTime(2150, 4, 19), new DateTime(2151, 4, 11), new DateTime(2152, 4, 30),
            new DateTime(2153, 4, 15), new DateTime(2154, 5, 5), new DateTime(2155, 4, 27), new DateTime(2156, 4, 11), new DateTime(2157, 5, 1),
            new DateTime(2158, 4, 23), new DateTime(2159, 4, 8), new DateTime(2160, 4, 27), new DateTime(2161, 4, 19), new DateTime(2162, 4, 11),
            new DateTime(2163, 4, 24), new DateTime(2164, 4, 15), new DateTime(2165, 5, 5), new DateTime(2166, 4, 20), new DateTime(2167, 4, 12),
            new DateTime(2168, 5, 1), new DateTime(2169, 4, 23), new DateTime(2170, 4, 8), new DateTime(2171, 4, 28), new DateTime(2172, 4, 19),
            new DateTime(2173, 5, 9), new DateTime(2174, 4, 24), new DateTime(2175, 4, 16), new DateTime(2176, 5, 5), new DateTime(2177, 4, 20),
            new DateTime(2178, 4, 12), new DateTime(2179, 5, 2), new DateTime(2180, 4, 16), new DateTime(2181, 4, 8), new DateTime(2182, 4, 28),
            new DateTime(2183, 4, 13), new DateTime(2184, 5, 2), new DateTime(2185, 4, 24), new DateTime(2186, 4, 9), new DateTime(2187, 4, 29),
            new DateTime(2188, 4, 20), new DateTime(2189, 4, 12), new DateTime(2190, 4, 25), new DateTime(2191, 4, 17), new DateTime(2192, 5, 6),
            new DateTime(2193, 4, 28), new DateTime(2194, 4, 13), new DateTime(2195, 5, 3), new DateTime(2196, 4, 24), new DateTime(2197, 4, 9),
            new DateTime(2198, 4, 29), new DateTime(2199, 4, 21), new DateTime(2200, 4, 6), new DateTime(2201, 4, 26), new DateTime(2202, 4, 18),
            new DateTime(2203, 5, 8), new DateTime(2204, 4, 22), new DateTime(2205, 4, 14), new DateTime(2206, 5, 4), new DateTime(2207, 4, 19),
            new DateTime(2208, 4, 10), new DateTime(2209, 4, 30), new DateTime(2210, 4, 15), new DateTime(2211, 5, 5), new DateTime(2212, 4, 26),
            new DateTime(2213, 4, 18), new DateTime(2214, 5, 1), new DateTime(2215, 4, 23), new DateTime(2216, 4, 14), new DateTime(2217, 5, 4),
            new DateTime(2218, 4, 19), new DateTime(2219, 4, 11), new DateTime(2220, 4, 30), new DateTime(2221, 4, 15), new DateTime(2222, 5, 5),
            new DateTime(2223, 4, 27), new DateTime(2224, 4, 11), new DateTime(2225, 5, 1), new DateTime(2226, 4, 23), new DateTime(2227, 4, 8),
            new DateTime(2228, 4, 27), new DateTime(2229, 4, 19), new DateTime(2230, 5, 9), new DateTime(2231, 4, 24), new DateTime(2232, 4, 15),
            new DateTime(2233, 5, 5), new DateTime(2234, 4, 20), new DateTime(2235, 4, 12), new DateTime(2236, 5, 1), new DateTime(2237, 4, 23),
            new DateTime(2238, 4, 8), new DateTime(2239, 4, 28), new DateTime(2240, 4, 19), new DateTime(2241, 5, 9), new DateTime(2242, 4, 24),
            new DateTime(2243, 4, 16), new DateTime(2244, 5, 5), new DateTime(2245, 4, 20), new DateTime(2246, 4, 12), new DateTime(2247, 5, 2),
            new DateTime(2248, 4, 16), new DateTime(2249, 5, 6), new DateTime(2250, 4, 28), new DateTime(2251, 4, 13), new DateTime(2252, 5, 2),
            new DateTime(2253, 4, 24), new DateTime(2254, 4, 9), new DateTime(2255, 4, 29), new DateTime(2256, 4, 20), new DateTime(2257, 4, 12),
            new DateTime(2258, 4, 25), new DateTime(2259, 4, 17), new DateTime(2260, 5, 6), new DateTime(2261, 4, 21), new DateTime(2262, 4, 13),
            new DateTime(2263, 5, 3), new DateTime(2264, 4, 24), new DateTime(2265, 4, 9), new DateTime(2266, 4, 29), new DateTime(2267, 4, 21),
            new DateTime(2268, 5, 10), new DateTime(2269, 4, 25), new DateTime(2270, 4, 17), new DateTime(2271, 5, 7), new DateTime(2272, 4, 21),
            new DateTime(2273, 4, 13), new DateTime(2274, 5, 3), new DateTime(2275, 4, 18), new DateTime(2276, 4, 9), new DateTime(2277, 4, 29),
            new DateTime(2278, 4, 14), new DateTime(2279, 5, 4), new DateTime(2280, 4, 25), new DateTime(2281, 4, 10), new DateTime(2282, 4, 30),
            new DateTime(2283, 4, 22), new DateTime(2284, 4, 13), new DateTime(2285, 4, 26), new DateTime(2286, 4, 18), new DateTime(2287, 5, 8),
            new DateTime(2288, 4, 29), new DateTime(2289, 4, 14), new DateTime(2290, 5, 4), new DateTime(2291, 4, 26), new DateTime(2292, 4, 10),
            new DateTime(2293, 4, 30), new DateTime(2294, 4, 22), new DateTime(2295, 4, 7), new DateTime(2296, 4, 26), new DateTime(2297, 4, 18),
            new DateTime(2298, 5, 8), new DateTime(2299, 4, 23), new DateTime(2300, 4, 15), new DateTime(2301, 5, 5), new DateTime(2302, 4, 20),
            new DateTime(2303, 4, 12), new DateTime(2304, 5, 1), new DateTime(2305, 4, 16), new DateTime(2306, 5, 6), new DateTime(2307, 4, 28),
            new DateTime(2308, 4, 19), new DateTime(2309, 5, 2), new DateTime(2310, 4, 24), new DateTime(2311, 4, 16), new DateTime(2312, 5, 5),
            new DateTime(2313, 4, 20), new DateTime(2314, 4, 12), new DateTime(2315, 5, 2), new DateTime(2316, 4, 16), new DateTime(2317, 5, 6),
            new DateTime(2318, 4, 28), new DateTime(2319, 4, 13), new DateTime(2320, 5, 2), new DateTime(2321, 4, 24), new DateTime(2322, 4, 9),
            new DateTime(2323, 4, 29), new DateTime(2324, 4, 20), new DateTime(2325, 5, 10), new DateTime(2326, 4, 25), new DateTime(2327, 4, 17),
            new DateTime(2328, 5, 6), new DateTime(2329, 4, 21), new DateTime(2330, 4, 13), new DateTime(2331, 5, 3), new DateTime(2332, 4, 24),
            new DateTime(2333, 4, 9), new DateTime(2334, 4, 29), new DateTime(2335, 4, 21), new DateTime(2336, 5, 10), new DateTime(2337, 4, 25),
            new DateTime(2338, 4, 17), new DateTime(2339, 4, 30), new DateTime(2340, 4, 21), new DateTime(2341, 4, 13), new DateTime(2342, 5, 3),
            new DateTime(2343, 4, 18), new DateTime(2344, 5, 7), new DateTime(2345, 4, 29), new DateTime(2346, 4, 14), new DateTime(2347, 5, 4),
            new DateTime(2348, 4, 25), new DateTime(2349, 4, 10), new DateTime(2350, 4, 30), new DateTime(2351, 4, 22), new DateTime(2352, 4, 13),
            new DateTime(2353, 4, 26), new DateTime(2354, 4, 18), new DateTime(2355, 5, 8), new DateTime(2356, 4, 22), new DateTime(2357, 4, 14),
            new DateTime(2358, 5, 4), new DateTime(2359, 4, 19), new DateTime(2360, 4, 10), new DateTime(2361, 4, 30), new DateTime(2362, 4, 22),
            new DateTime(2363, 5, 5), new DateTime(2364, 4, 26), new DateTime(2365, 4, 18), new DateTime(2366, 5, 8), new DateTime(2367, 4, 23),
            new DateTime(2368, 4, 14), new DateTime(2369, 5, 4), new DateTime(2370, 4, 19), new DateTime(2371, 4, 11), new DateTime(2372, 4, 30),
            new DateTime(2373, 4, 15), new DateTime(2374, 5, 5), new DateTime(2375, 4, 27), new DateTime(2376, 4, 11), new DateTime(2377, 5, 1),
            new DateTime(2378, 4, 23), new DateTime(2379, 4, 8), new DateTime(2380, 4, 27), new DateTime(2381, 4, 19), new DateTime(2382, 5, 9),
            new DateTime(2383, 4, 24), new DateTime(2384, 4, 15), new DateTime(2385, 5, 5), new DateTime(2386, 4, 27), new DateTime(2387, 4, 12),
            new DateTime(2388, 5, 1), new DateTime(2389, 4, 23), new DateTime(2390, 4, 8), new DateTime(2391, 4, 28), new DateTime(2392, 4, 19),
            new DateTime(2393, 5, 9), new DateTime(2394, 4, 24), new DateTime(2395, 4, 16), new DateTime(2396, 5, 5), new DateTime(2397, 4, 20),
            new DateTime(2398, 4, 12), new DateTime(2399, 5, 2), new DateTime(2400, 4, 16), new DateTime(2401, 5, 6), new DateTime(2402, 4, 28),
            new DateTime(2403, 4, 13), new DateTime(2404, 5, 2), new DateTime(2405, 4, 24), new DateTime(2406, 4, 16), new DateTime(2407, 4, 29),
            new DateTime(2408, 4, 20), new DateTime(2409, 4, 12), new DateTime(2410, 5, 2), new DateTime(2411, 4, 17), new DateTime(2412, 5, 6),
            new DateTime(2413, 4, 28), new DateTime(2414, 4, 13), new DateTime(2415, 5, 3), new DateTime(2416, 4, 24), new DateTime(2417, 4, 9),
            new DateTime(2418, 4, 29), new DateTime(2419, 4, 21), new DateTime(2420, 5, 10), new DateTime(2421, 4, 25), new DateTime(2422, 4, 17),
            new DateTime(2423, 5, 7), new DateTime(2424, 4, 21), new DateTime(2425, 4, 13), new DateTime(2426, 5, 3), new DateTime(2427, 4, 18),
            new DateTime(2428, 4, 9), new DateTime(2429, 4, 29), new DateTime(2430, 4, 21), new DateTime(2431, 5, 4), new DateTime(2432, 4, 25),
            new DateTime(2433, 4, 17), new DateTime(2434, 4, 30), new DateTime(2435, 4, 22), new DateTime(2436, 4, 13), new DateTime(2437, 5, 3),
            new DateTime(2438, 4, 18), new DateTime(2439, 5, 8), new DateTime(2440, 4, 29), new DateTime(2441, 4, 14), new DateTime(2442, 5, 4),
            new DateTime(2443, 4, 26), new DateTime(2444, 4, 10), new DateTime(2445, 4, 30), new DateTime(2446, 4, 22), new DateTime(2447, 4, 7),
            new DateTime(2448, 4, 26), new DateTime(2449, 4, 18), new DateTime(2450, 5, 8), new DateTime(2451, 4, 23), new DateTime(2452, 4, 14),
            new DateTime(2453, 5, 4), new DateTime(2454, 4, 19), new DateTime(2455, 4, 11), new DateTime(2456, 4, 30), new DateTime(2457, 4, 22),
            new DateTime(2458, 5, 5), new DateTime(2459, 4, 27), new DateTime(2460, 4, 18), new DateTime(2461, 5, 8), new DateTime(2462, 4, 23),
            new DateTime(2463, 4, 15), new DateTime(2464, 5, 4), new DateTime(2465, 4, 19), new DateTime(2466, 4, 11), new DateTime(2467, 5, 1),
            new DateTime(2468, 4, 15), new DateTime(2469, 5, 5), new DateTime(2470, 4, 27), new DateTime(2471, 4, 12), new DateTime(2472, 5, 1),
            new DateTime(2473, 4, 23), new DateTime(2474, 4, 8), new DateTime(2475, 4, 28), new DateTime(2476, 4, 19), new DateTime(2477, 5, 9),
            new DateTime(2478, 4, 24), new DateTime(2479, 4, 16), new DateTime(2480, 5, 5), new DateTime(2481, 4, 27), new DateTime(2482, 4, 12),
            new DateTime(2483, 5, 2), new DateTime(2484, 4, 23), new DateTime(2485, 4, 8), new DateTime(2486, 4, 28), new DateTime(2487, 4, 20),
            new DateTime(2488, 5, 9), new DateTime(2489, 4, 24), new DateTime(2490, 4, 16), new DateTime(2491, 5, 6), new DateTime(2492, 4, 20),
            new DateTime(2493, 4, 12), new DateTime(2494, 5, 2), new DateTime(2495, 4, 17), new DateTime(2496, 5, 6), new DateTime(2497, 4, 28),
            new DateTime(2498, 4, 13), new DateTime(2499, 5, 3), new DateTime(2500, 4, 25)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Shrove_Tuesday</c>.
        /// </summary>
        private static readonly DateTime[] ShroveTuesday =
        {
            new DateTime(2016, 2, 9), new DateTime(2017, 2, 28), new DateTime(2018, 2, 13), new DateTime(2019, 3, 5), new DateTime(2020, 2, 25),
            new DateTime(2021, 2, 16), new DateTime(2022, 3, 1), new DateTime(2023, 2, 21), new DateTime(2024, 2, 13), new DateTime(2025, 3, 4),
            new DateTime(2026, 2, 17), new DateTime(2027, 2, 9), new DateTime(2028, 2, 29), new DateTime(2029, 2, 13), new DateTime(2030, 3, 5),
            new DateTime(2031, 2, 25), new DateTime(2032, 2, 10), new DateTime(2033, 3, 1), new DateTime(2034, 2, 21), new DateTime(2035, 2, 6),
            new DateTime(2036, 2, 26), new DateTime(2037, 2, 17), new DateTime(2038, 3, 9), new DateTime(2039, 2, 22), new DateTime(2040, 2, 14),
            new DateTime(2041, 3, 5), new DateTime(2042, 2, 18), new DateTime(2043, 2, 10), new DateTime(2044, 3, 1), new DateTime(2045, 2, 21),
            new DateTime(2046, 2, 6), new DateTime(2047, 2, 26), new DateTime(2048, 2, 18), new DateTime(2049, 3, 2), new DateTime(2050, 2, 22)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Ash_Wednesday</c>.
        /// </summary>
        private static readonly DateTime[] AshWednesday =
        {
            new DateTime(2016, 2, 10), new DateTime(2017, 3, 1), new DateTime(2018, 2, 14), new DateTime(2019, 3, 6), new DateTime(2020, 2, 26),
            new DateTime(2021, 2, 17), new DateTime(2022, 3, 2), new DateTime(2023, 2, 22), new DateTime(2024, 2, 14), new DateTime(2025, 3, 5),
            new DateTime(2026, 2, 18)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Palm_Sunday</c>.
        /// </summary>
        private static readonly DateTime[] PalmSunday =
        {
            new DateTime(2009, 4, 5), new DateTime(2010, 3, 28), new DateTime(2011, 4, 17), new DateTime(2012, 4, 1), new DateTime(2013, 3, 24),
            new DateTime(2014, 4, 13), new DateTime(2015, 3, 29), new DateTime(2016, 3, 20), new DateTime(2017, 4, 9), new DateTime(2018, 3, 25),
            new DateTime(2019, 4, 14), new DateTime(2020, 4, 5), new DateTime(2021, 3, 28), new DateTime(2022, 4, 10), new DateTime(2023, 4, 2)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Maundy_Thursday</c>.
        /// </summary>
        private static readonly DateTime[] MaundyThursday =
        {
            new DateTime(2015, 4, 2), new DateTime(2016, 3, 24), new DateTime(2017, 4, 13), new DateTime(2018, 3, 29)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Good_Friday</c>.
        /// </summary>
        private static readonly DateTime[] GoodFriday =
        {
            new DateTime(2015, 4, 3), new DateTime(2016, 3, 25), new DateTime(2017, 4, 14)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Feast_of_the_Ascension</c>.
        /// </summary>
        private static readonly DateTime[] AscensionThursday =
        {
            new DateTime(2000, 6, 1), new DateTime(2001, 5, 24), new DateTime(2002, 5, 9), new DateTime(2003, 5, 29), new DateTime(2004, 5, 20),
            new DateTime(2005, 5, 5), new DateTime(2006, 5, 25), new DateTime(2007, 5, 17), new DateTime(2008, 5, 1), new DateTime(2009, 5, 21),
            new DateTime(2010, 5, 13), new DateTime(2011, 6, 2), new DateTime(2012, 5, 17), new DateTime(2013, 5, 9), new DateTime(2014, 5, 29),
            new DateTime(2015, 5, 14), new DateTime(2016, 5, 5), new DateTime(2017, 5, 25), new DateTime(2018, 5, 10), new DateTime(2019, 5, 30),
            new DateTime(2020, 5, 21)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Pentecost</c>.
        /// </summary>
        private static readonly DateTime[] WhitSunday =
        {
            new DateTime(2015, 5, 24), new DateTime(2016, 5, 15), new DateTime(2017, 6, 4)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Whit_Monday</c>.
        /// </summary>
        private static readonly DateTime[] WhitMonday =
        {
            new DateTime(2000, 6, 12), new DateTime(2001, 6, 4), new DateTime(2002, 5, 20),
            new DateTime(2003, 6, 9), new DateTime(2004, 5, 31), new DateTime(2005, 5, 16),
            new DateTime(2006, 6, 5), new DateTime(2007, 5, 28), new DateTime(2008, 5, 12),
            new DateTime(2009, 6, 1), new DateTime(2010, 5, 24), new DateTime(2011, 6, 13),
            new DateTime(2012, 5, 28), new DateTime(2013, 5, 20), new DateTime(2014, 6, 9),
            new DateTime(2015, 5, 25), new DateTime(2016, 5, 16), new DateTime(2017, 6, 5),
            new DateTime(2018, 5, 21), new DateTime(2019, 6, 10), new DateTime(2020, 6, 1)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Trinity_Sunday</c>.
        /// </summary>
        private static readonly DateTime[] TrinitySunday =
        {
            new DateTime(2014, 6, 15), new DateTime(2015, 5, 31), new DateTime(2016, 5, 22), new DateTime(2017, 6, 11), new DateTime(2018, 5, 27),
            new DateTime(2019, 6, 16), new DateTime(2020, 6, 7)
        };

        /// <summary>
        /// Taken from <c>https://en.wikipedia.org/wiki/Corpus_Christi_(feast)</c>.
        /// </summary>
        private static readonly DateTime[] CorpusChristi =
        {
            new DateTime(2013, 5, 30), new DateTime(2014, 6, 19), new DateTime(2015, 6, 4), new DateTime(2016, 5, 26), new DateTime(2017, 6, 15),
            new DateTime(2018, 5, 31), new DateTime(2019, 6, 20), new DateTime(2020, 6, 11), new DateTime(2021, 6, 3), new DateTime(2022, 6, 16)
        };

        private static readonly DateTime[] ChristmasDay =
        {
            new DateTime(1959, 12, 25), new DateTime(1963, 12, 25), new DateTime(1990, 12, 25)
        };

        private static readonly DateTime[] BoxingDay =
        {
            new DateTime(1959, 12, 26), new DateTime(1963, 12, 26), new DateTime(1990, 12, 26)
        };

        private static readonly DateTime[] NewYearDay =
        {
            new DateTime(1959, 1, 1), new DateTime(1963, 1, 1), new DateTime(1990, 1, 1)
        };

        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void Computus_EasterSundayFromYear_ExplicitlyKnownDays_Recognized()
        {
            foreach (var date in WesternEasterSunday)
            {
                Assert.IsTrue(date == date.EasterSundayFromYear(), date.ToLongDateString() + " (extension)");
                Assert.IsTrue(date == Computus.EasterSundayFromYear(date.Year), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_OrthodoxEasterSundayFromYear_ExplicitlyKnownDays_Recognized()
        {
            foreach (var date in OrthodoxEasterSunday)
            {
                Assert.IsTrue(date == date.OrthodoxEasterSundayFromYear(), date.ToLongDateString() + " (extension)");
                Assert.IsTrue(date == Computus.OrthodoxEasterSundayFromYear(date.Year), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_EasterSundayFromYear_CalculatedDays_AreSundays()
        {
            for (int i = 1583; i <= 2600; ++i)
            {
                Assert.AreEqual(DayOfWeek.Sunday, Computus.EasterSundayFromYear(i).DayOfWeek, i.ToString(CultureInfo.InvariantCulture));
            }
        }

        [TestMethod]
        public void Computus_OrthodoxEasterSundayFromYear_CalculatedDays_AreSundays()
        {
            for (int i = 1583; i <= 2500; ++i)
            {
                Assert.AreEqual(DayOfWeek.Sunday, Computus.OrthodoxEasterSundayFromYear(i).DayOfWeek, i.ToString(CultureInfo.InvariantCulture));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Computus_EasterSundayFromYear_DateYearBefore1583_Exception()
        {
            new DateTime(1582, 1, 1).EasterSundayFromYear();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Computus_EasterSundayFromYear_YearBefore1583_Exception()
        {
            Computus.EasterSundayFromYear(1582);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Computus_OrthodoxEasterSundayFromYear_DateYearBefore1583_Exception()
        {
            new DateTime(1582, 1, 1).OrthodoxEasterSundayFromYear();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Computus_OrthodoxEasterSundayFromYear_YearBefore1583_Exception()
        {
            Computus.OrthodoxEasterSundayFromYear(1582);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Computus_OrthodoxEasterSundayFromYear_DateYearAfter2500_Exception()
        {
            new DateTime(2501, 1, 1).OrthodoxEasterSundayFromYear();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Computus_OrthodoxEasterSundayFromYear_YearAfter2500_Exception()
        {
            Computus.OrthodoxEasterSundayFromYear(2501);
        }

        [TestMethod]
        public void Computus_EasterSundayDayOfYear_ExplicitlyKnownDays_CorrectDayOfYear()
        {
            foreach (var date in WesternEasterSunday)
            {
                Assert.AreEqual(date.DayOfYear, Computus.EasterSundayDayOfYear(date.Year), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_OrthodoxEasterSundayDayOfYear_ExplicitlyKnownDays_CorrectDayOfYear()
        {
            foreach (var date in OrthodoxEasterSunday)
            {
                Assert.AreEqual(date.DayOfYear, Computus.OrthodoxEasterSundayDayOfYear(date.Year), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsShroveTuesday_ShroveTuesdayDays_Recognized()
        {
            foreach (var date in ShroveTuesday)
            {
                Assert.IsTrue(date.IsShroveTuesday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsShroveTuesday_NotShroveTuesdayDays_NotRecognized()
        {
            foreach (var date in ShroveTuesday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsShroveTuesday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsShroveTuesday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsAshWednesday_AshWednesdayDays_Recognized()
        {
            foreach (var date in AshWednesday)
            {
                Assert.IsTrue(date.IsAshWednesday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsAshWednesday_NotAshWednesdayDays_NotRecognized()
        {
            foreach (var date in AshWednesday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsAshWednesday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsAshWednesday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsPalmSunday_PalmSundayDays_Recognized()
        {
            foreach (var date in PalmSunday)
            {
                Assert.IsTrue(date.IsPalmSunday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsPalmSunday_NotPalmSundayDays_NotRecognized()
        {
            foreach (var date in PalmSunday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsPalmSunday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsPalmSunday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsMaundyThursday_MaundyThursdayDays_Recognized()
        {
            foreach (var date in MaundyThursday)
            {
                Assert.IsTrue(date.IsMaundyThursday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsMaundyThursday_NotMaundyThursdayDays_NotRecognized()
        {
            foreach (var date in MaundyThursday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsMaundyThursday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsMaundyThursday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsGoodFriday_GoodFridayDays_Recognized()
        {
            foreach (var date in GoodFriday)
            {
                Assert.IsTrue(date.IsGoodFriday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsGoodFriday_NotGoodFridayDays_NotRecognized()
        {
            foreach (var date in GoodFriday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsGoodFriday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsGoodFriday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsEasterSunday_ExplicitlyKnownDays__Recognized()
        {
            foreach (var date in WesternEasterSunday)
            {
                Assert.IsTrue(date.IsEasterSunday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsEasterSunday_NotEasterSundayDays_NotRecognized()
        {
            foreach (var date in WesternEasterSunday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsEasterSunday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsEasterSunday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsOrthodoxEasterSunday_ExplicitlyKnownDays__Recognized()
        {
            foreach (var date in OrthodoxEasterSunday)
            {
                Assert.IsTrue(date.IsOrthodoxEasterSunday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsOrthodoxEasterSunday_NotEasterSundayDays_NotRecognized()
        {
            foreach (var date in OrthodoxEasterSunday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsOrthodoxEasterSunday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsOrthodoxEasterSunday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsEasterMonday_ExplicitlyKnownDays__Recognized()
        {
            foreach (var date in WesternEasterSunday)
            {
                var d = date.AddDays(1);
                Assert.IsTrue(d.IsEasterMonday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsEasterMonday_NotEasterMondayDays_NotRecognized()
        {
            foreach (var date in WesternEasterSunday)
            {
                Assert.IsFalse(date.IsEasterMonday(), date.ToLongDateString());
                var d = date.AddDays(2);
                Assert.IsFalse(d.IsEasterMonday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsOrthodoxEasterMonday_ExplicitlyKnownDays__Recognized()
        {
            foreach (var date in OrthodoxEasterSunday)
            {
                var d = date.AddDays(1);
                Assert.IsTrue(d.IsOrthodoxEasterMonday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsOrthodoxEasterMonday_NotEasterMondayDays_NotRecognized()
        {
            foreach (var date in OrthodoxEasterSunday)
            {
                Assert.IsFalse(date.IsOrthodoxEasterMonday(), date.ToLongDateString());
                var d = date.AddDays(2);
                Assert.IsFalse(d.IsOrthodoxEasterMonday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsAscensionThursday_AscensionThursdayDays_Recognized()
        {
            foreach (var date in AscensionThursday)
            {
                Assert.IsTrue(date.IsAscensionThursday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsAscensionThursday_NotAscensionThursdayDays_NotRecognized()
        {
            foreach (var date in AscensionThursday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsAscensionThursday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsAscensionThursday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsWhitSunday_WhitSundayDays_Recognized()
        {
            foreach (var date in WhitSunday)
            {
                Assert.IsTrue(date.IsWhitSunday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsWhitSunday_NotWhitSundayDays_NotRecognized()
        {
            foreach (var date in WhitSunday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsWhitSunday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsWhitSunday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsWhitMonday_WhitMondayDays_Recognized()
        {
            foreach (var date in WhitMonday)
            {
                Assert.IsTrue(date.IsWhitMonday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsWhitMonday_NotWhitMondayDays_NotRecognized()
        {
            foreach (var date in WhitMonday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsWhitMonday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsWhitMonday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsTrinitySunday_TrinitySundayDays_Recognized()
        {
            foreach (var date in TrinitySunday)
            {
                Assert.IsTrue(date.IsTrinitySunday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsTrinitySunday_NotTrinitySundayDays_NotRecognized()
        {
            foreach (var date in TrinitySunday)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsTrinitySunday(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsTrinitySunday(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsCorpusChristi_CorpusChristiDays_Recognized()
        {
            foreach (var date in CorpusChristi)
            {
                Assert.IsTrue(date.IsCorpusChristi(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsCorpusChristi_NotCorpusChristiDays_NotRecognized()
        {
            foreach (var date in CorpusChristi)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsCorpusChristi(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsCorpusChristi(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsChristmasDay_ChristmasDays_Recognized()
        {
            foreach (var date in ChristmasDay)
            {
                Assert.IsTrue(date.IsChristmasDay(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsChristmasDay_NotChristmasDays_NotRecognized()
        {
            foreach (var date in ChristmasDay)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsChristmasDay(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsChristmasDay(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsBoxingDay_BoxingDays_Recognized()
        {
            foreach (var date in BoxingDay)
            {
                Assert.IsTrue(date.IsBoxingDay(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsBoxingDay_NotBoxingDays_NotRecognized()
        {
            foreach (var date in BoxingDay)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsBoxingDay(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsBoxingDay(), d.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsNewYearDay_NewYearDays_Recognized()
        {
            foreach (var date in NewYearDay)
            {
                Assert.IsTrue(date.IsNewYearDay(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void Computus_IsNewYearDay_NotNewYearDays_NotRecognized()
        {
            foreach (var date in NewYearDay)
            {
                var d = date.AddDays(-1);
                Assert.IsFalse(d.IsNewYearDay(), d.ToLongDateString());
                d = date.AddDays(1);
                Assert.IsFalse(d.IsNewYearDay(), d.ToLongDateString());
            }
        }
    }
}
