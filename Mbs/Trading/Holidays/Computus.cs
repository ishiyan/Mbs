using System;

namespace Mbs.Trading.Holidays
{
    /// <summary>
    /// The computus (Latin for computation) is the calculation of the date of Easter
    /// (the first Sunday after the first ecclesiastical full moon falling on or after 21 March).
    /// <para/>
    /// The name has been used for this procedure since the early Middle Ages, as it was one of the most important computations of the age.
    /// <para/>
    /// The Roman Catholic Church since 1583 has been using 21 March under the Gregorian calendar to calculate the date of Easter,
    /// while the Eastern Orthodox continued and continue to use 20 March under the Julian Calendar.
    /// </summary>
    public static class Computus
    {
        private static void ValidateEasterYearLowerBound(int year)
        {
            if (EasterSundayPerYearFirstYear > year)
                throw new ArgumentException("Algorithm is invalid before 1583 AD.", nameof(year));
        }

        private static void ValidateEasterYearUpperBound(int year)
        {
            if (EasterSundayPerYearLastYear < year)
                throw new ArgumentException("Algorithm is invalid after 2500 AD.", nameof(year));
        }

        /// <summary>
        /// Computes the date (greater than 1583 AD) that Western Easter falls on.
        /// </summary>
        /// <param name="dateTime">A date containing the year, should be greater than 1583 AD.</param>
        /// <returns>The date (greater than 1583 AD) that Western Easter falls on.</returns>
        public static DateTime EasterSundayFromYear(this DateTime dateTime)
        {
            return EasterSundayFromYear(dateTime.Year);
        }

        /// <summary>
        /// Computes the date (greater than 1583 AD) that Western Easter falls on.
        /// </summary>
        /// <param name="year">A year, should be greater than 1583 AD.</param>
        /// <returns>The date (greater than 1583 AD) that Western Easter falls on.</returns>
        public static DateTime EasterSundayFromYear(int year)
        {
            ValidateEasterYearLowerBound(year);
            if (EasterSundayPerYearLastYear < year)
                return EasterSundayFromYearKnuth(year);
            int i = year - EasterSundayPerYearFirstYear;
            return new DateTime(year, WesternEasterSundayMonthPerYear[i], WesternEasterSundayDayPerYear[i]);
        }

        /// <summary>
        /// Computes the date (greater than 1583 AD) that Orthodox Easter falls on.
        /// </summary>
        /// <param name="dateTime">A date containing the year, should be greater than 1583 AD.</param>
        /// <returns>The date (greater than 1583 AD) that Orthodox Easter falls on.</returns>
        public static DateTime OrthodoxEasterSundayFromYear(this DateTime dateTime)
        {
            return OrthodoxEasterSundayFromYear(dateTime.Year);
        }

        /// <summary>
        /// Computes the date (greater than 1583 AD) that Orthodox Easter falls on.
        /// </summary>
        /// <param name="year">A year, should be greater than 1583 AD.</param>
        /// <returns>The date (greater than 1583 AD) that Orthodox Easter falls on.</returns>
        public static DateTime OrthodoxEasterSundayFromYear(int year)
        {
            ValidateEasterYearLowerBound(year);
            ValidateEasterYearUpperBound(year);
            int i = year - EasterSundayPerYearFirstYear;
            return new DateTime(year, OrthodoxEasterSundayMonthPerYear[i], OrthodoxEasterSundayDayPerYear[i]);
        }

        /// <summary>
        /// Computes the day of year Western Easter falls on.
        /// </summary>
        /// <param name="year">A year, should be greater than 1583 AD.</param>
        /// <returns>The day of year Western Easter falls on.</returns>
        public static int EasterSundayDayOfYear(int year)
        {
            ValidateEasterYearLowerBound(year);
            if (EasterSundayPerYearLastYear < year)
                return EasterSundayFromYearKnuth(year).DayOfYear;
            return WesternEasterSundayDayOfYearPerYear[year - EasterSundayPerYearFirstYear];
        }

        /// <summary>
        /// Compute the day of year Orthodox Easter falls on.
        /// </summary>
        /// <param name="year">A year, should be greater than 1583 AD.</param>
        /// <returns>The day of year Orthodox Easter falls on.</returns>
        public static int OrthodoxEasterSundayDayOfYear(int year)
        {
            ValidateEasterYearLowerBound(year);
            ValidateEasterYearUpperBound(year);
            return OrthodoxEasterSundayDayOfYearPerYear[year - EasterSundayPerYearFirstYear];
        }

        /// <summary>
        /// Computes the date (greater than 1583 AD) that Western Easter falls on. Reference: Knuth, volume 1, page 155.
        /// </summary>
        /// <param name="year">A year, should be greater than 1583 AD.</param>
        /// <returns>The date (greater than 1583 AD) that Western Easter falls on.</returns>
        private static DateTime EasterSundayFromYearKnuth(int year)
        {
            // ReSharper disable IdentifierTypo
            // ReSharper disable CommentTypo
            int golden = year % 19 + 1; // E1: metonic cycle
            int century = year / 100 + 1; // E2: e.g. 1984 was in 20th century
            int x = 3 * century / 4 - 12; // E3: leap year correction
            int z = (8 * century + 5) / 25 - 5; // E3: sync with moon's orbit
            int d = 5 * year / 4 - x - 10;
            int epact = (11 * golden + 20 + z - x) % 30; // E5: epact
            if ((25 == epact && 11 < golden) || 24 == epact)
                epact++;
            int n = 44 - epact;
            n += 30 * (n < 21 ? 1 : 0); // E6:
            n += 7 - (d + n) % 7;
            if (31 < n) // E7:
                return new DateTime(year, 4, n - 31); // April

            // ReSharper restore IdentifierTypo
            // ReSharper restore CommentTypo
            return new DateTime(year, 3, n); // March
        }

        private const int EasterSundayPerYearFirstYear = 1583;
        private const int EasterSundayPerYearLastYear = 2500;

        private static readonly int[] WesternEasterSundayMonthPerYear =
        {
            4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 3, 4, 4, 3,
            4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4,
            3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4,
            4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3,
            4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 3, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 3,
            4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4,
            3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4,
            4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3,
            4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4,
            4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4,
            4, 4, 4, 3, 4, 4, 3, 4, 3, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4,
            4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3,
            4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 3, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4,
            3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4,
            3, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4,
            4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3,
            4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4,
            3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4,
            4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3,
            4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 3, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4,
            4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4,
            4, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 3, 4, 4, 3, 4,
            4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 4, 4, 4, 3, 4, 4, 3, 4, 4, 4, 4, 3, 4
        };

        private static readonly int[] WesternEasterSundayDayPerYear =
        {
            10, 1, 21, 6, 29, 17, 2, 22, 14, 29, 18, 10, 26, 14, 6, 22, 11, 2, 22, 7, 30, 18, 10, 26, 15, 6, 19, 11, 3, 22, 7, 30, 19, 3, 26, 15, 31, 19, 11, 27,
            16, 7, 30, 12, 4, 23, 15, 31, 20, 11, 27, 16, 8, 23, 12, 4, 24, 8, 31, 20, 5, 27, 16, 1, 21, 12, 4, 17, 9, 31, 13, 5, 28, 16, 1, 21, 13, 28, 17, 9,
            25, 13, 5, 25, 10, 1, 21, 6, 29, 17, 2, 25, 14, 5, 18, 10, 2, 21, 6, 29, 18, 2, 22, 14, 30, 18, 10, 26, 15, 6, 22, 11, 3, 22, 7, 30, 19, 11, 27, 16,
            8, 23, 12, 4, 24, 8, 31, 20, 5, 27, 16, 1, 21, 12, 28, 17, 9, 31, 13, 5, 28, 16, 1, 21, 13, 28, 17, 9, 25, 13, 5, 25, 10, 1, 21, 6, 29, 17, 2, 25,
            14, 5, 18, 10, 2, 14, 6, 29, 11, 2, 22, 14, 30, 18, 10, 26, 15, 6, 22, 11, 3, 22, 7, 30, 19, 3, 26, 15, 31, 19, 11, 3, 16, 7, 30, 19, 4, 26, 15, 31,
            20, 11, 27, 16, 8, 23, 12, 4, 24, 8, 31, 20, 5, 27, 16, 8, 24, 13, 5, 18, 10, 1, 14, 6, 29, 17, 2, 22, 14, 29, 18, 10, 26, 14, 6, 22, 11, 2, 22, 7,
            30, 18, 3, 26, 15, 6, 19, 11, 3, 22, 7, 30, 19, 3, 26, 15, 31, 19, 11, 27, 16, 7, 23, 12, 4, 23, 8, 31, 20, 11, 27, 16, 8, 23, 12, 4, 24, 8, 31, 20,
            5, 27, 16, 1, 21, 12, 28, 17, 9, 31, 13, 5, 28, 16, 1, 21, 13, 28, 17, 9, 25, 13, 5, 25, 10, 1, 21, 6, 29, 17, 2, 25, 14, 5, 18, 10, 2, 15, 7, 30,
            12, 3, 23, 15, 31, 19, 11, 27, 16, 7, 23, 12, 4, 23, 8, 31, 20, 4, 27, 16, 1, 20, 12, 4, 17, 8, 31, 20, 5, 27, 16, 1, 21, 12, 28, 17, 9, 24, 13, 5,
            25, 9, 1, 21, 6, 28, 17, 9, 25, 13, 5, 18, 10, 1, 21, 6, 29, 17, 2, 22, 14, 29, 18, 10, 26, 14, 6, 29, 11, 2, 22, 14, 30, 18, 10, 26, 15, 6, 19, 11,
            3, 22, 7, 30, 19, 3, 26, 15, 31, 19, 11, 3, 16, 7, 30, 12, 4, 23, 15, 31, 20, 11, 27, 16, 8, 23, 12, 4, 24, 8, 31, 20, 5, 27, 16, 1, 21, 12, 4, 17,
            9, 31, 20, 5, 28, 16, 1, 21, 13, 28, 17, 9, 25, 13, 5, 25, 10, 1, 21, 6, 29, 17, 9, 25, 14, 5, 18, 10, 2, 21, 6, 29, 18, 2, 22, 14, 30, 18, 10, 26,
            15, 6, 29, 11, 3, 22, 14, 30, 19, 10, 26, 15, 7, 19, 11, 3, 23, 7, 30, 19, 4, 26, 15, 31, 20, 11, 3, 16, 8, 30, 12, 4, 24, 15, 31, 20, 12, 28, 17, 9,
            25, 13, 5, 18, 10, 1, 21, 6, 29, 17, 2, 22, 14, 29, 18, 10, 26, 14, 6, 29, 11, 2, 22, 14, 30, 18, 10, 26, 15, 6, 19, 11, 3, 22, 7, 30, 19, 3, 26, 15,
            31, 19, 11, 3, 16, 7, 30, 12, 4, 23, 15, 31, 20, 11, 27, 16, 8, 23, 12, 4, 24, 8, 31, 20, 5, 27, 16, 1, 21, 12, 4, 17, 9, 31, 20, 5, 28, 16, 1, 21,
            13, 28, 17, 9, 25, 13, 5, 25, 10, 1, 21, 6, 29, 17, 9, 25, 14, 6, 19, 11, 3, 22, 7, 30, 19, 3, 26, 15, 31, 19, 11, 27, 16, 7, 30, 12, 4, 23, 15, 31,
            20, 11, 27, 16, 8, 23, 12, 4, 24, 8, 31, 20, 5, 27, 16, 1, 21, 12, 4, 17, 9, 31, 13, 5, 28, 16, 1, 21, 13, 28, 17, 9, 25, 13, 5, 25, 10, 1, 21, 6,
            29, 17, 2, 25, 14, 5, 18, 10, 2, 21, 6, 29, 18, 2, 22, 14, 30, 18, 10, 26, 15, 6, 22, 11, 3, 22, 7, 30, 19, 10, 26, 15, 7, 19, 11, 3, 16, 8, 31, 20,
            5, 27, 16, 1, 21, 12, 28, 17, 9, 31, 13, 5, 28, 16, 1, 21, 6, 28, 17, 9, 25, 13, 5, 25, 10, 1, 21, 6, 29, 17, 2, 25, 14, 5, 18, 10, 26, 14, 6, 29,
            11, 2, 22, 14, 30, 18, 10, 26, 15, 6, 22, 11, 3, 22, 7, 30, 19, 3, 26, 15, 31, 19, 11, 3, 16, 7, 30, 19, 4, 26, 15, 31, 20, 11, 27, 16, 8, 23, 12, 4,
            24, 8, 31, 20, 5, 27, 16, 8, 24, 12, 4, 17, 9, 31, 20, 5, 28, 16, 1, 21, 13, 28, 17, 9, 25, 13, 5, 25, 10, 1, 21, 6, 29, 17, 2, 25, 14, 5, 18, 10,
            2, 21, 6, 29, 18, 2, 22, 14, 30, 18, 10, 26, 15, 6, 22, 11, 3, 22, 7, 30, 19, 10, 26, 15, 7, 19, 11, 3, 16, 7, 30, 19, 4, 26, 15, 31, 20, 11, 27, 16,
            8, 30, 12, 4, 24, 15, 31, 20, 5, 27, 16, 8, 24, 12, 4, 24, 9, 31, 20, 5, 28, 16, 1, 21, 13, 4, 17, 9, 25, 13, 5, 28, 10, 1, 21, 13, 29, 18
        };

        private static readonly int[] WesternEasterSundayDayOfYearPerYear =
        {
            100, 92, 111, 96, 88, 108, 92, 112, 104, 89, 108, 100, 85, 105, 96, 81, 101, 93, 112, 97, 89, 109, 100, 85, 105, 97, 109, 101, 93, 113, 97, 89, 109, 94, 85, 105, 90, 110, 101, 86,
            106, 98, 89, 102, 94, 114, 105, 90, 110, 102, 86, 106, 98, 83, 102, 94, 114, 99, 90, 110, 95, 87, 106, 91, 111, 103, 94, 107, 99, 91, 103, 95, 87, 107, 91, 111, 103, 88, 107, 99,
            84, 104, 95, 115, 100, 92, 111, 96, 88, 108, 92, 84, 104, 96, 108, 100, 92, 112, 96, 88, 108, 93, 112, 104, 89, 109, 100, 85, 105, 97, 81, 101, 93, 113, 97, 89, 109, 101, 86, 106,
            98, 83, 102, 94, 114, 99, 90, 110, 95, 87, 106, 91, 111, 103, 87, 107, 99, 91, 103, 95, 87, 107, 91, 111, 103, 88, 107, 99, 84, 104, 95, 115, 100, 92, 111, 96, 88, 108, 92, 84,
            104, 96, 108, 100, 92, 105, 96, 88, 101, 93, 112, 104, 89, 109, 100, 85, 105, 97, 81, 101, 93, 113, 97, 89, 109, 94, 85, 105, 90, 110, 101, 93, 106, 98, 89, 109, 94, 86, 105, 90,
            110, 102, 86, 106, 98, 83, 102, 94, 114, 99, 90, 110, 95, 87, 106, 98, 83, 103, 95, 108, 100, 92, 104, 96, 88, 108, 92, 112, 104, 89, 108, 100, 85, 105, 96, 81, 101, 93, 112, 97,
            89, 109, 93, 85, 105, 97, 109, 101, 93, 113, 97, 89, 109, 94, 85, 105, 90, 110, 101, 86, 106, 98, 82, 102, 94, 114, 98, 90, 110, 102, 86, 106, 98, 83, 102, 94, 114, 99, 90, 110,
            95, 87, 106, 91, 111, 103, 87, 107, 99, 91, 103, 95, 87, 107, 91, 111, 103, 88, 107, 99, 84, 104, 95, 115, 100, 92, 111, 96, 88, 108, 92, 84, 104, 96, 108, 100, 92, 105, 97, 89,
            102, 94, 113, 105, 90, 110, 101, 86, 106, 98, 82, 102, 94, 114, 98, 90, 110, 95, 86, 106, 91, 111, 102, 94, 107, 99, 90, 110, 95, 87, 106, 91, 111, 103, 87, 107, 99, 84, 103, 95,
            115, 100, 91, 111, 96, 88, 107, 99, 84, 104, 95, 108, 100, 92, 111, 96, 88, 108, 92, 112, 104, 89, 108, 100, 85, 105, 96, 88, 101, 93, 112, 104, 89, 109, 100, 85, 105, 97, 109, 101,
            93, 113, 97, 89, 109, 94, 85, 105, 90, 110, 101, 93, 106, 98, 89, 102, 94, 114, 105, 90, 110, 102, 86, 106, 98, 83, 102, 94, 114, 99, 90, 110, 95, 87, 106, 91, 111, 103, 94, 107,
            99, 91, 110, 95, 87, 107, 91, 111, 103, 88, 107, 99, 84, 104, 95, 115, 100, 92, 111, 96, 88, 108, 99, 84, 104, 96, 108, 100, 92, 112, 96, 88, 108, 93, 112, 104, 89, 109, 100, 85,
            105, 97, 88, 101, 93, 113, 104, 89, 109, 101, 85, 105, 97, 110, 101, 93, 113, 98, 89, 109, 94, 86, 105, 90, 110, 102, 93, 106, 98, 90, 102, 94, 114, 106, 90, 110, 102, 87, 107, 99,
            84, 104, 95, 108, 100, 92, 111, 96, 88, 108, 92, 112, 104, 89, 108, 100, 85, 105, 96, 88, 101, 93, 112, 104, 89, 109, 100, 85, 105, 97, 109, 101, 93, 113, 97, 89, 109, 94, 85, 105,
            90, 110, 101, 93, 106, 98, 89, 102, 94, 114, 105, 90, 110, 102, 86, 106, 98, 83, 102, 94, 114, 99, 90, 110, 95, 87, 106, 91, 111, 103, 94, 107, 99, 91, 110, 95, 87, 107, 91, 111,
            103, 88, 107, 99, 84, 104, 95, 115, 100, 92, 111, 96, 88, 108, 99, 84, 104, 96, 109, 101, 93, 113, 97, 89, 109, 94, 85, 105, 90, 110, 101, 86, 106, 98, 89, 102, 94, 114, 105, 90,
            110, 102, 86, 106, 98, 83, 102, 94, 114, 99, 90, 110, 95, 87, 106, 91, 111, 103, 94, 107, 99, 91, 103, 95, 87, 107, 91, 111, 103, 88, 107, 99, 84, 104, 95, 115, 100, 92, 111, 96,
            88, 108, 92, 84, 104, 96, 108, 100, 92, 112, 96, 88, 108, 93, 112, 104, 89, 109, 100, 85, 105, 97, 81, 101, 93, 113, 97, 89, 109, 101, 85, 105, 97, 110, 101, 93, 106, 98, 90, 110,
            95, 87, 106, 91, 111, 103, 87, 107, 99, 91, 103, 95, 87, 107, 91, 111, 96, 88, 107, 99, 84, 104, 95, 115, 100, 92, 111, 96, 88, 108, 92, 84, 104, 96, 108, 100, 85, 105, 96, 88,
            101, 93, 112, 104, 89, 109, 100, 85, 105, 97, 81, 101, 93, 113, 97, 89, 109, 94, 85, 105, 90, 110, 101, 93, 106, 98, 89, 109, 94, 86, 105, 90, 110, 102, 86, 106, 98, 83, 102, 94,
            114, 99, 90, 110, 95, 87, 106, 98, 83, 103, 94, 107, 99, 91, 110, 95, 87, 107, 91, 111, 103, 88, 107, 99, 84, 104, 95, 115, 100, 92, 111, 96, 88, 108, 92, 84, 104, 96, 108, 100,
            92, 112, 96, 88, 108, 93, 112, 104, 89, 109, 100, 85, 105, 97, 81, 101, 93, 113, 97, 89, 109, 101, 85, 105, 97, 110, 101, 93, 106, 98, 89, 109, 94, 86, 105, 90, 110, 102, 86, 106,
            98, 90, 102, 94, 114, 106, 90, 110, 95, 87, 106, 98, 83, 103, 94, 114, 99, 91, 110, 95, 87, 107, 91, 111, 103, 95, 107, 99, 84, 104, 95, 87, 100, 92, 111, 103, 88, 108
        };

        private static readonly int[] OrthodoxEasterSundayMonthPerYear =
        {
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5,
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4,
            4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4,
            4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4,
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4,
            4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
            5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4,
            4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
            4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4,
            4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4,
            5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4,
            4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4,
            4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4,
            4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4,
            4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 4, 4,
            4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5,
            4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4,
            5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4,
            4, 5, 4, 5, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 5,
            4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5,
            4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 5, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4,
            5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 5, 4, 5, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4,
            4, 5, 4, 4, 5, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 4, 4, 4, 5, 4, 4, 5, 4, 4, 5, 4, 5, 4, 4, 5, 4
        };

        private static readonly int[] OrthodoxEasterSundayDayPerYear =
        {
            10, 29, 21, 13, 26, 17, 9, 29, 14, 5, 25, 10, 30, 21, 6, 26, 18, 2, 22, 14, 4, 18, 10, 30, 15, 6, 26, 18, 3, 22, 14, 4, 19, 10, 30, 15, 7, 26, 11, 1,
            23, 7, 27, 19, 4, 23, 15, 7, 20, 11, 1, 16, 8, 27, 19, 4, 24, 15, 5, 20, 12, 1, 16, 8, 28, 12, 4, 24, 9, 28, 20, 5, 25, 16, 8, 21, 13, 2, 24, 9,
            29, 20, 5, 25, 17, 1, 21, 13, 3, 17, 9, 29, 14, 5, 25, 10, 30, 21, 13, 26, 18, 9, 29, 14, 6, 25, 10, 30, 22, 6, 26, 18, 3, 22, 14, 4, 19, 11, 1, 16,
            8, 27, 19, 4, 24, 15, 5, 20, 12, 1, 16, 8, 28, 12, 2, 24, 9, 28, 20, 5, 25, 16, 8, 21, 13, 2, 17, 9, 29, 20, 5, 25, 17, 6, 21, 13, 3, 17, 9, 29,
            14, 5, 25, 10, 30, 21, 6, 26, 18, 9, 22, 14, 4, 25, 10, 30, 22, 6, 26, 18, 3, 22, 14, 4, 19, 10, 30, 15, 7, 26, 11, 1, 23, 14, 27, 19, 11, 30, 15, 7,
            27, 11, 1, 23, 8, 27, 19, 4, 24, 15, 5, 20, 12, 1, 16, 8, 28, 20, 5, 25, 17, 6, 21, 13, 26, 17, 9, 29, 14, 3, 25, 10, 30, 21, 6, 26, 18, 9, 22, 14,
            4, 18, 10, 30, 15, 6, 26, 18, 1, 22, 14, 4, 19, 10, 30, 15, 7, 26, 11, 1, 23, 7, 27, 19, 4, 23, 15, 5, 20, 11, 1, 23, 8, 27, 19, 4, 24, 15, 5, 20,
            12, 1, 16, 8, 28, 12, 2, 24, 9, 28, 20, 12, 25, 16, 8, 28, 13, 2, 24, 9, 29, 20, 5, 25, 17, 6, 21, 13, 3, 17, 9, 29, 14, 5, 25, 17, 30, 22, 14, 27,
            19, 10, 30, 15, 5, 26, 11, 1, 23, 7, 27, 19, 4, 23, 15, 5, 20, 11, 1, 16, 8, 27, 19, 2, 24, 15, 5, 20, 12, 1, 16, 8, 28, 12, 2, 24, 9, 28, 20, 5,
            25, 16, 6, 21, 13, 2, 24, 9, 29, 20, 5, 25, 17, 6, 21, 13, 3, 17, 9, 29, 14, 3, 25, 10, 30, 21, 13, 26, 18, 9, 29, 14, 4, 25, 10, 30, 22, 6, 26, 18,
            8, 22, 14, 4, 19, 10, 30, 15, 7, 26, 18, 1, 23, 14, 27, 19, 11, 30, 15, 5, 27, 11, 1, 23, 8, 27, 19, 4, 24, 15, 5, 20, 12, 1, 16, 8, 28, 19, 2, 24,
            16, 5, 20, 12, 2, 16, 8, 28, 13, 2, 24, 9, 29, 20, 5, 25, 17, 6, 21, 13, 3, 24, 9, 29, 21, 5, 25, 17, 7, 21, 13, 3, 18, 9, 29, 14, 4, 25, 10, 30,
            22, 13, 26, 18, 10, 29, 14, 4, 19, 10, 30, 22, 7, 26, 18, 8, 23, 14, 4, 19, 11, 30, 15, 7, 27, 18, 1, 23, 8, 27, 19, 11, 24, 15, 5, 27, 12, 2, 24, 9,
            29, 20, 5, 25, 17, 6, 21, 13, 3, 17, 9, 29, 14, 3, 25, 17, 30, 21, 13, 3, 18, 9, 29, 14, 4, 25, 10, 30, 22, 6, 26, 18, 8, 22, 14, 4, 19, 10, 30, 22,
            7, 26, 18, 8, 23, 14, 4, 19, 11, 30, 15, 5, 27, 11, 1, 23, 8, 27, 19, 11, 24, 15, 5, 20, 12, 1, 23, 8, 28, 19, 9, 24, 16, 5, 20, 12, 2, 16, 8, 28,
            13, 2, 24, 9, 29, 20, 12, 25, 17, 6, 28, 13, 3, 24, 9, 29, 21, 6, 26, 18, 8, 22, 14, 4, 19, 10, 30, 15, 5, 26, 18, 1, 23, 14, 4, 19, 11, 30, 15, 5,
            27, 11, 1, 23, 8, 27, 19, 9, 24, 15, 5, 20, 12, 1, 23, 8, 28, 19, 9, 24, 16, 5, 20, 12, 2, 16, 6, 28, 13, 2, 24, 9, 29, 20, 12, 25, 17, 6, 21, 13,
            3, 24, 9, 29, 21, 10, 25, 17, 7, 21, 13, 3, 18, 9, 29, 14, 4, 25, 10, 30, 22, 13, 26, 18, 8, 29, 14, 4, 26, 10, 30, 22, 7, 26, 18, 8, 23, 15, 5, 20,
            12, 1, 16, 6, 28, 19, 2, 24, 16, 5, 20, 12, 2, 16, 6, 28, 13, 2, 24, 9, 29, 20, 10, 25, 17, 6, 21, 13, 3, 24, 9, 29, 21, 10, 25, 17, 30, 21, 13, 3,
            18, 7, 29, 14, 4, 25, 10, 30, 22, 13, 26, 18, 8, 22, 14, 4, 19, 10, 30, 22, 5, 26, 18, 8, 23, 14, 4, 19, 11, 30, 15, 5, 27, 11, 1, 23, 8, 27, 19, 9,
            24, 15, 5, 27, 12, 1, 23, 8, 28, 19, 9, 24, 16, 5, 20, 12, 2, 16, 6, 28, 13, 2, 24, 16, 29, 20, 12, 2, 17, 6, 28, 13, 3, 24, 9, 29, 21, 10, 25, 17,
            7, 21, 13, 3, 18, 9, 29, 21, 4, 25, 17, 30, 22, 13, 3, 18, 8, 29, 14, 4, 26, 10, 30, 22, 7, 26, 18, 8, 23, 14, 4, 19, 11, 30, 22, 5, 27, 18, 8, 23,
            15, 4, 19, 11, 1, 15, 5, 27, 12, 1, 23, 8, 28, 19, 9, 24, 16, 5, 27, 12, 2, 23, 8, 28, 20, 9, 24, 16, 6, 20, 12, 2, 17, 6, 28, 13, 3, 25
        };

        private static readonly int[] OrthodoxEasterSundayDayOfYearPerYear =
        {
            100, 120, 111, 103, 116, 108, 99, 119, 104, 96, 115, 100, 120, 112, 96, 116, 108, 93, 112, 104, 124, 109, 100, 120, 105, 97, 116, 108, 93, 113, 104, 124, 109, 101, 120, 105, 97, 117, 101, 121,
            113, 98, 117, 109, 94, 114, 105, 97, 110, 102, 121, 106, 98, 118, 109, 94, 114, 106, 125, 110, 102, 122, 106, 98, 118, 103, 94, 114, 99, 119, 110, 95, 115, 107, 98, 111, 103, 123, 114, 99,
            119, 111, 95, 115, 107, 92, 111, 103, 123, 108, 99, 119, 104, 96, 115, 100, 120, 112, 103, 116, 108, 100, 119, 104, 96, 116, 100, 120, 112, 97, 116, 108, 93, 113, 104, 124, 109, 101, 121, 106,
            98, 118, 109, 94, 114, 106, 125, 110, 102, 122, 106, 98, 118, 103, 122, 114, 99, 119, 110, 95, 115, 107, 98, 111, 103, 123, 107, 99, 119, 111, 95, 115, 107, 127, 111, 103, 123, 108, 99, 119,
            104, 96, 115, 100, 120, 112, 96, 116, 108, 100, 112, 104, 124, 116, 100, 120, 112, 97, 116, 108, 93, 113, 104, 124, 109, 101, 120, 105, 97, 117, 101, 121, 113, 105, 117, 109, 101, 121, 105, 97,
            117, 102, 121, 113, 98, 118, 109, 94, 114, 106, 125, 110, 102, 122, 106, 98, 118, 110, 95, 115, 107, 127, 111, 103, 116, 108, 99, 119, 104, 124, 115, 100, 120, 112, 96, 116, 108, 100, 112, 104,
            124, 109, 100, 120, 105, 97, 116, 108, 121, 113, 104, 124, 109, 101, 120, 105, 97, 117, 101, 121, 113, 98, 117, 109, 94, 114, 105, 125, 110, 102, 121, 113, 98, 118, 109, 94, 114, 106, 125, 110,
            102, 122, 106, 98, 118, 103, 122, 114, 99, 119, 110, 102, 115, 107, 98, 118, 103, 123, 114, 99, 119, 111, 95, 115, 107, 127, 111, 103, 123, 108, 99, 119, 104, 96, 115, 107, 120, 112, 104, 117,
            109, 101, 120, 105, 125, 117, 101, 121, 113, 98, 117, 109, 94, 114, 105, 125, 110, 102, 121, 106, 98, 118, 109, 122, 114, 106, 125, 110, 102, 122, 106, 98, 118, 103, 122, 114, 99, 119, 110, 95,
            115, 107, 126, 111, 103, 123, 114, 99, 119, 111, 95, 115, 107, 127, 111, 103, 123, 108, 99, 119, 104, 124, 115, 100, 120, 112, 103, 116, 108, 100, 119, 104, 124, 116, 100, 120, 112, 97, 116, 108,
            128, 113, 104, 124, 109, 101, 120, 105, 97, 117, 108, 121, 113, 105, 117, 109, 101, 121, 105, 125, 117, 102, 121, 113, 98, 118, 109, 94, 114, 106, 125, 110, 102, 122, 106, 98, 118, 110, 122, 114,
            106, 126, 110, 102, 122, 107, 98, 118, 103, 123, 114, 99, 119, 111, 95, 115, 107, 127, 111, 103, 123, 115, 99, 119, 111, 96, 115, 107, 127, 112, 103, 123, 108, 100, 119, 104, 124, 116, 100, 120,
            112, 104, 116, 108, 100, 120, 104, 124, 109, 101, 120, 112, 97, 117, 108, 128, 113, 105, 124, 109, 101, 121, 105, 97, 117, 109, 121, 113, 98, 118, 109, 101, 114, 106, 125, 117, 102, 122, 114, 99,
            119, 111, 95, 115, 107, 127, 111, 103, 123, 108, 99, 119, 104, 124, 115, 107, 120, 112, 103, 123, 108, 100, 119, 104, 124, 116, 100, 120, 112, 97, 116, 108, 128, 113, 104, 124, 109, 101, 120, 112,
            97, 117, 108, 128, 113, 105, 124, 109, 101, 121, 105, 125, 117, 102, 121, 113, 98, 118, 109, 101, 114, 106, 125, 110, 102, 122, 113, 98, 118, 110, 129, 114, 106, 126, 110, 102, 122, 107, 98, 118,
            103, 123, 114, 99, 119, 111, 102, 115, 107, 127, 118, 103, 123, 115, 99, 119, 111, 96, 116, 108, 128, 113, 104, 124, 109, 101, 120, 105, 125, 117, 108, 121, 113, 105, 124, 109, 101, 121, 105, 125,
            117, 102, 121, 113, 98, 118, 109, 129, 114, 106, 125, 110, 102, 122, 113, 98, 118, 110, 129, 114, 106, 126, 110, 102, 122, 107, 126, 118, 103, 123, 114, 99, 119, 111, 102, 115, 107, 127, 111, 103,
            123, 115, 99, 119, 111, 131, 115, 107, 127, 112, 103, 123, 108, 100, 119, 104, 124, 116, 100, 120, 112, 104, 116, 108, 128, 120, 104, 124, 116, 101, 120, 112, 97, 117, 108, 128, 113, 105, 125, 110,
            102, 122, 106, 126, 118, 110, 122, 114, 106, 126, 110, 102, 122, 107, 126, 118, 103, 123, 114, 99, 119, 111, 130, 115, 107, 127, 111, 103, 123, 115, 99, 119, 111, 131, 115, 107, 120, 112, 103, 123,
            108, 128, 119, 104, 124, 116, 100, 120, 112, 104, 116, 108, 128, 113, 104, 124, 109, 101, 120, 112, 125, 117, 108, 128, 113, 105, 124, 109, 101, 121, 105, 125, 117, 102, 121, 113, 98, 118, 109, 129,
            114, 106, 125, 117, 102, 122, 113, 98, 118, 110, 129, 114, 106, 126, 110, 102, 122, 107, 126, 118, 103, 123, 114, 106, 119, 111, 102, 122, 107, 127, 118, 103, 123, 115, 99, 119, 111, 131, 115, 107,
            127, 112, 103, 123, 108, 100, 119, 111, 124, 116, 107, 120, 112, 104, 123, 108, 128, 120, 104, 124, 116, 101, 120, 112, 97, 117, 108, 128, 113, 105, 124, 109, 101, 121, 112, 125, 117, 109, 128, 113,
            105, 125, 109, 101, 121, 106, 125, 117, 102, 122, 113, 98, 118, 110, 129, 114, 106, 126, 117, 102, 122, 114, 98, 118, 110, 130, 114, 106, 126, 111, 102, 122, 107, 127, 118, 103, 123, 115
        };

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Shrove Tuesday, aka Mardi Gras (47 days before Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Shrove Tuesday, false if not.</returns>
        public static bool IsShroveTuesday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) - 47 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Ash Wednesday, start of Lent (46 days before Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Ash Wednesday, false if not.</returns>
        public static bool IsAshWednesday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) - 46 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Palm Sunday (7 days before Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Palm Sunday, false if not.</returns>
        public static bool IsPalmSunday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) - 7 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Maundy Thursday (3 days before Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Maundy Thursday, false if not.</returns>
        public static bool IsMaundyThursday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) - 3 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Good Friday (Karfreitag, 2 days before Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Good Friday, false if not.</returns>
        public static bool IsGoodFriday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) - 2 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Western Easter Sunday.
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Western Easter Sunday, false if not.</returns>
        public static bool IsEasterSunday(this DateTime dateTime)
        {
            return EasterSundayFromYear(dateTime) == dateTime;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Orthodox Easter Sunday.
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Orthodox Easter Sunday, false if not.</returns>
        public static bool IsOrthodoxEasterSunday(this DateTime dateTime)
        {
            return OrthodoxEasterSundayFromYear(dateTime) == dateTime;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Western Easter Monday (Ostermontag, 1 day after Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Western Easter Monday, false if not.</returns>
        public static bool IsEasterMonday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) + 1 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Orthodox Easter Monday (1 day after Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Orthodox Easter Monday, false if not.</returns>
        public static bool IsOrthodoxEasterMonday(this DateTime dateTime)
        {
            return OrthodoxEasterSundayDayOfYear(dateTime.Year) + 1 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Ascension Thursday (Christi Himmelfahrt, 39 days after Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Ascension Thursday, false if not.</returns>
        public static bool IsAscensionThursday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) + 39 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Pentecost, Whit Sunday (49 days after Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Whit Sunday, false if not.</returns>
        public static bool IsWhitSunday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) + 49 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Whit Monday (Pfingstmontag, 50 days after Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Whit Monday, false if not.</returns>
        public static bool IsWhitMonday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) + 50 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Trinity Sunday (7 days after Whit Sunday or 56 days after Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Trinity Sunday, false if not.</returns>
        public static bool IsTrinitySunday(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) + 56 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date (greater than 1583 AD) is the Corpus Christi (60 days after Easter).
        /// </summary>
        /// <param name="dateTime">A date. The year should be greater than 1583 AD.</param>
        /// <returns>True if a date is the Corpus Christi, false if not.</returns>
        public static bool IsCorpusChristi(this DateTime dateTime)
        {
            return EasterSundayDayOfYear(dateTime.Year) + 60 == dateTime.DayOfYear;
        }

        /// <summary>
        /// Checks if a date is the Christmas Day (25 December).
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if a date is the Christmas Day, false if not.</returns>
        public static bool IsChristmasDay(this DateTime dateTime)
        {
            return 12 == dateTime.Month && 25 == dateTime.Day;
        }

        /// <summary>
        /// Checks if a date is the Boxing Day (26 December).
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if a date is the Boxing Day, false if not.</returns>
        public static bool IsBoxingDay(this DateTime dateTime)
        {
            return 12 == dateTime.Month && 26 == dateTime.Day;
        }

        /// <summary>
        /// Checks if a date is the New Year Day (1 January).
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if a date is the New Year Day, false if not.</returns>
        public static bool IsNewYearDay(this DateTime dateTime)
        {
            return 1 == dateTime.Month && 1 == dateTime.Day;
        }
    }
}
