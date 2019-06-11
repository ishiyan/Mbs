using System;
using Mbs.Trading.Markets;

namespace Mbs.Trading.Holidays
{
    /// <summary>
    /// Generic Danish exchange holiday schedule.
    /// </summary>
    public static class DanishHolidays
    {
        /// <summary>
        /// Checks if the specified date is a Danish holiday (non-trading day).
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Dktc"/>, <see cref="ExchangeMic.Xcse"/>, <see cref="ExchangeMic.Fndk"/>.
        /// <para/>
        /// Verified using <c>http://www.marketholidays.com/HolidaysByCenter.aspx</c>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <remarks>
        /// <list type="table">
        /// <item><term>New Year's Day</term><description>January 1st</description></item>
        /// <item><term>Maundy Thursday (Holy Thursday)</term><description></description></item>
        /// <item><term>Good Friday</term><description></description></item>
        /// <item><term>Easter Monday</term><description></description></item>
        /// <item><term>General Prayer Day</term><description>25 days after Easter Monday</description></item>
        /// <item><term>Ascension Day</term><description></description></item>
        /// <item><term>Whit (Pentecost) Monday</term><description></description></item>
        /// <item><term>Constitution Day</term><description>June 5th</description></item>
        /// <item><term>Christmas Eve</term><description>December 24th</description></item>
        /// <item><term>Christmas Day</term><description>December 25th</description></item>
        /// <item><term>Boxing Day</term><description>December 26th</description></item>
        /// <item><term>New Year's Eve</term><description>December 31st</description></item>
        /// </list>
        /// </remarks>
        /// <returns>True if the specified date is a Danish non-trading day, false if not.</returns>
        public static bool IsDanishHoliday(this DateTime dateTime)
        {
            DayOfWeek dow = dateTime.DayOfWeek;
            if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
                return true;

            int year = dateTime.Year;
            int dayOfYear = dateTime.DayOfYear;
            int easterSunday = Computus.EasterSundayDayOfYear(year);
            if (dayOfYear == easterSunday - 3 || // Maundy Thursday (Holy Thursday), 3 days before Easter.
                dayOfYear == easterSunday - 2 || // Good Friday.
                dayOfYear == easterSunday + 1 || // Easter Monday.
                dayOfYear == easterSunday + 26 || // General Prayer Day, 26 days after Easter.
                dayOfYear == easterSunday + 39 || // Ascension Day, 39 days after Easter.
                (dayOfYear == easterSunday + 40 && 2008 < year) || // Day after Ascension Day, 40 days after Easter.
                dayOfYear == easterSunday + 50) // Whit (Pentecost) Monday, 50 days after Easter.
                return true;

            int day = dateTime.Day;
            int month = dateTime.Month;
            switch (month)
            {
                case 1:
                    // New Year's Day.
                    if (day == 1)
                        return true;
                    break;
                case 6:
                    // Constitution Day.
                    if (day == 5)
                        return true;
                    break;
                case 12:
                    // Christmas Eve, Christmas Day, Boxing Day, New Year's Eve.
                    if (day == 24 || day == 25 || day == 26 || day == 31)
                        return true;
                    break;
            }

            if (year == 1998 && month == 2 && day == 16)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if the specified date is a Danish trading day.
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Dktc"/>, <see cref="ExchangeMic.Xcse"/>, <see cref="ExchangeMic.Fndk"/>.
        /// <para/>
        /// Verified using <c>http://www.marketholidays.com/HolidaysByCenter.aspx</c>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if the specified date is a Danish trading day, false if not.</returns>
        public static bool IsDanishWorkday(this DateTime dateTime)
        {
            return !dateTime.IsDanishHoliday();
        }
    }
}
