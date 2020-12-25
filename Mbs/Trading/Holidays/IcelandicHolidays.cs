using System;
using Mbs.Trading.Markets;
#pragma warning disable SA1108 // Block statements should not contain embedded comments

namespace Mbs.Trading.Holidays
{
    /// <summary>
    /// Generic Icelandic exchange holiday schedule.
    /// </summary>
    public static class IcelandicHolidays
    {
        /// <summary>
        /// Checks if the specified date is an Icelandic holiday (non-trading day).
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Xcse"/>, <see cref="ExchangeMic.Fndk"/>, <see cref="ExchangeMic.Dktc"/>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <remarks>
        /// <list type="table">
        /// <item><term>New Year's Day</term><description>January 1st, might be moved to Monday</description></item>
        /// <item><term>Maundy Thursday (Holy Thursday)</term><description></description></item>
        /// <item><term>Good Friday</term><description></description></item>
        /// <item><term>Easter Monday</term><description></description></item>
        /// <item><term>First Day of Summer</term><description>Third or fourth Thursday in April</description></item>
        /// <item><term>Labor Day</term><description>May 1st</description></item>
        /// <item><term>Ascension Day</term><description></description></item>
        /// <item><term>Whit (Pentecost) Monday</term><description></description></item>
        /// <item><term>Independence Day</term><description>June 17th</description></item>
        /// <item><term>Commerce Day</term><description>First Monday in August</description></item>
        /// <item><term>Christmas Eve</term><description>December 25th</description></item>
        /// <item><term>Boxing Day (St. Stephen’s Day)</term><description>December 26th</description></item>
        /// </list>
        /// </remarks>
        /// <returns>True if the specified date is an Icelandic non-trading day, false if not.</returns>
        public static bool IsIcelandicHoliday(this DateTime dateTime)
        {
            DayOfWeek dow = dateTime.DayOfWeek;
            if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
            {
                return true;
            }

            int year = dateTime.Year;
            int dayOfYear = dateTime.DayOfYear;
            int easterSunday = Computus.EasterSundayDayOfYear(year);
            if (dayOfYear == easterSunday - 3 || // Maundy Thursday (Holy Thursday), 3 days before Easter.
                dayOfYear == easterSunday - 2 || // Good Friday.
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
                    // New Year's Day (possibly moved to Monday).
                    if (day == 1 || ((day == 2 || day == 3) && dow == DayOfWeek.Monday))
                    {
                        return true;
                    }

                    break;
                case 4:
                    // First Day of Summer (third or fourth Thursday in April).
                    if (day >= 19 && day <= 25 && dow == DayOfWeek.Thursday)
                    {
                        return true;
                    }

                    break;
                case 5:
                    // Labor Day.
                    if (day == 1)
                    {
                        return true;
                    }

                    break;
                case 6:
                    // Independence Day.
                    if (day == 17)
                    {
                        return true;
                    }

                    break;
                case 8:
                    // Commerce Day (first Monday in August).
                    if (day <= 7 && dow == DayOfWeek.Monday)
                    {
                        return true;
                    }

                    break;
                case 12:
                    // Christmas Day, Boxing Day.
                    if (day == 25 || day == 26)
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }

        /// <summary>
        /// Checks if the specified date is an Icelandic trading day.
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Xcse"/>, <see cref="ExchangeMic.Fndk"/>, <see cref="ExchangeMic.Dktc"/>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if the specified date is an Icelandic trading day, false if not.</returns>
        public static bool IsIcelandicWorkday(this DateTime dateTime)
        {
            return !dateTime.IsIcelandicHoliday();
        }
    }
}
