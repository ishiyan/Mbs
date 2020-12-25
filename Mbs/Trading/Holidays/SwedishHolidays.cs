using System;
using Mbs.Trading.Markets;
#pragma warning disable SA1108 // Block statements should not contain embedded comments

namespace Mbs.Trading.Holidays
{
    /// <summary>
    /// Generic Swedish exchange holiday schedule.
    /// </summary>
    public static class SwedishHolidays
    {
        /// <summary>
        /// Checks if the specified date is a Swedish holiday (non-trading day).
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Xsto"/>, <see cref="ExchangeMic.Nmtf"/>, <see cref="ExchangeMic.Xngm"/>, <see cref="ExchangeMic.Xndx"/>, <see cref="ExchangeMic.Fnse"/>, <see cref="ExchangeMic.Xsat"/>.
        /// <para/>
        /// Verified using <c>http://www.marketholidays.com/HolidaysByCenter.aspx</c>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <remarks>
        /// <list type="table">
        /// <item><term>New Year's Day</term><description>January 1st</description></item>
        /// <item><term>Epiphany Day (Twelfth Night)</term><description>January 6th</description></item>
        /// <item><term>Good Friday</term><description></description></item>
        /// <item><term>Easter Monday</term><description></description></item>
        /// <item><term>General Prayer Day</term><description>25 days after Easter Monday</description></item>
        /// <item><term>Ascension Day</term><description></description></item>
        /// <item><term>Whit (Pentecost) Monday</term><description></description></item>
        /// <item><term>Labour Day</term><description>May 1st</description></item>
        /// <item><term>National Day</term><description>June 6th. It has been debated whether or not this day should be declared as a holiday. As of 2002 the Stockholmborsen is open that day.</description></item>
        /// <item><term>Midsummer Eve</term><description>Friday between June 19-25</description></item>
        /// <item><term>Christmas Eve</term><description>December 24th</description></item>
        /// <item><term>Christmas Day</term><description>December 25th</description></item>
        /// <item><term>Boxing Day</term><description>December 26th</description></item>
        /// <item><term>New Year's Eve</term><description>December 31st</description></item>
        /// </list>
        /// </remarks>
        /// <returns>True if the specified date is a Swedish non-trading day, false if not.</returns>
        public static bool IsSwedishHoliday(this DateTime dateTime)
        {
            DayOfWeek dow = dateTime.DayOfWeek;
            if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
            {
                return true;
            }

            int year = dateTime.Year;
            int dayOfYear = dateTime.DayOfYear;
            int easterSunday = Computus.EasterSundayDayOfYear(year);
            if (dayOfYear == easterSunday - 2 || // Good Friday.
                dayOfYear == easterSunday + 1 || // Easter Monday.
                dayOfYear == easterSunday + 39 || // Ascension Day, 39 days after Easter.
                dayOfYear == easterSunday + 50) // Whit (Pentecost) Monday, 50 days after Easter.
            {
                return true;
            }

            int day = dateTime.Day;
            int month = dateTime.Month;
            switch (month)
            {
                case 1:
                    // New Year's Day, Epiphany.
                    if (day == 1 || day == 6)
                    {
                        return true;
                    }

                    break;
                case 5:
                    // Labour Day.
                    if (day == 1)
                    {
                        return true;
                    }

                    break;
                case 6:
                    // National Day.
                    if (day == 6 && year > 2002)
                    {
                        return true;
                    }

                    // Midsummer Eve (Friday between June 19-25).
                    if (day >= 19 && day <= 25 && dow == DayOfWeek.Friday)
                    {
                        return true;
                    }

                    break;
                case 12:
                    // Christmas Eve, Christmas Day, Boxing Day, New Year's Eve.
                    if (day == 24 || day == 25 || day == 26 || day == 31)
                    {
                        return true;
                    }

                    break;
            }

            // Added manually.
            if (year == 1995 && month == 4 && day == 13)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the specified date is a Swedish trading day.
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Xsto"/>, <see cref="ExchangeMic.Nmtf"/>, <see cref="ExchangeMic.Xngm"/>, <see cref="ExchangeMic.Xndx"/>, <see cref="ExchangeMic.Fnse"/>, <see cref="ExchangeMic.Xsat"/>.
        /// <para/>
        /// Verified using <c>http://www.marketholidays.com/HolidaysByCenter.aspx</c>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if the specified date is a Swedish trading day, false if not.</returns>
        public static bool IsSwedishWorkday(this DateTime dateTime)
        {
            return !dateTime.IsSwedishHoliday();
        }
    }
}
