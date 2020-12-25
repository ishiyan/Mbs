using System;
using Mbs.Trading.Markets;
#pragma warning disable SA1108 // Block statements should not contain embedded comments

namespace Mbs.Trading.Holidays
{
    /// <summary>
    /// Generic Swiss exchange holiday schedule.
    /// </summary>
    public static class SwissHolidays
    {
        /// <summary>
        /// Checks if the specified date is a Swiss holiday (non-trading day).
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Xbrn"/>, <see cref="ExchangeMic.Aixe"/>, <see cref="ExchangeMic.Xqmh"/>, <see cref="ExchangeMic.Xswb"/>,
        /// <see cref="ExchangeMic.Xswx"/>, <see cref="ExchangeMic.Xvtx"/>.
        /// <para/>
        /// Not valid for <see cref="ExchangeMic.Xeup"/>.
        /// <para/>
        /// Verified using <c>http://www.marketholidays.com/HolidaysByCenter.aspx</c>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <remarks>
        /// <list type="table">
        /// <item><term>New Year's Day</term><description>January 1st</description></item>
        /// <item><term>Berchtoldstag</term><description>January 2nd</description></item>
        /// <item><term>Good Friday</term><description></description></item>
        /// <item><term>Easter Monday</term><description></description></item>
        /// <item><term>Ascension Day</term><description></description></item>
        /// <item><term>Whit (Pentecost) Monday</term><description></description></item>
        /// <item><term>Labour Day</term><description>May 1st</description></item>
        /// <item><term>National Day</term><description>August 1st</description></item>
        /// <item><term>Christmas Eve</term><description>December 24th</description></item>
        /// <item><term>Christmas Day</term><description>December 25th</description></item>
        /// <item><term>St. Stephen's Day (Boxing Day)</term><description>December 26th</description></item>
        /// <item><term>New Year's Eve</term><description>December 31st</description></item>
        /// </list>
        /// </remarks>
        /// <returns>True if the specified date is a Swiss non-trading day, false if not.</returns>
        public static bool IsSwissHoliday(this DateTime dateTime)
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
                dayOfYear == easterSunday + 26 || // General Prayer Day, 26 days after Easter.
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
                    // New Year's Day, Berchtoldstag.
                    if (day == 1 || day == 2)
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
                case 8:
                    // National Day.
                    if (day == 1)
                    {
                        return true;
                    }

                    break;
                case 12:
                    // Christmas Eve, Christmas Day, St. Stephen's Day, New Year's Eve.
                    if (day == 24 || day == 25 || day == 26 || day == 31)
                    {
                        return true;
                    }

                    break;
            }

            // Added manually.
            if (year == 1994 && ((month == 4 && day == 18) || (month == 9 && day == 12) || (month == 12 && day == 30)))
            {
                return true;
            }

            if (year == 1995 && month == 12 && day == 29)
            {
                return true;
            }

            if (year == 1996 && month == 4 && day == 15)
            {
                return true;
            }

            if (year == 1999 && month == 11 && day == 12)
            {
                return true;
            }

            if (year == 2000 && month == 1 && day == 3)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the specified date is a Swiss trading day.
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Xbrn"/>, <see cref="ExchangeMic.Aixe"/>, <see cref="ExchangeMic.Xqmh"/>, <see cref="ExchangeMic.Xswb"/>,
        /// <see cref="ExchangeMic.Xswx"/>, <see cref="ExchangeMic.Xvtx"/>.
        /// <para/>
        /// Not valid for <see cref="ExchangeMic.Xeup"/>.
        /// <para/>
        /// Verified using <c>http://www.marketholidays.com/HolidaysByCenter.aspx</c>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if the specified date is a Swiss trading day, false if not.</returns>
        public static bool IsSwissWorkday(this DateTime dateTime)
        {
            return !dateTime.IsSwissHoliday();
        }
    }
}
