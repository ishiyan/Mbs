using System;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Holidays
{
    /// <summary>
    /// Generic Trans-european Automated Real-time Gross settlement Express Transfer system holiday schedule.
    /// </summary>
    public static class TargetHolidays
    {
        /// <summary>
        /// Checks if the specified date is a TARGET
        /// (Trans-european Automated Real-time Gross settlement Express Transfer system)
        /// holiday.
        /// <para/>
        /// See <c>http://www.ecb.int</c>.
        /// <para/>
        /// The holidays (apart from weekends) are:
        /// <para/>
        /// New Year's Day, Good Friday, Easter Monday, Labour Day, Christmas Day, Boxing Day.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <remarks>
        /// <list type="table">
        /// <item><term>New Year's Day</term><description>January 1st</description></item>
        /// <item><term>Good Friday</term><description></description></item>
        /// <item><term>Easter Monday</term><description></description></item>
        /// <item><term>Labour Day</term><description>May, 1st</description></item>
        /// <item><term>Christmas Day</term><description>December 25th</description></item>
        /// <item><term>Boxing Day</term><description>December 26th</description></item>
        /// </list>
        /// </remarks>
        /// <returns>True if the specified date is a TARGET holiday, false if not.</returns>
        public static bool IsTargetHoliday(this DateTime dateTime)
        {
            int day = dateTime.Day, doy = dateTime.DayOfYear, month = dateTime.Month;
            int year = dateTime.Year, easterMonday = Computus.EasterSundayDayOfYear(year) + 1;
            return dateTime.IsWeekend()
                || (day == 1 && month == 1) // New Year's Day.
                || (doy == easterMonday - 3 && year >= 2000) // Good Friday.
                || (doy == easterMonday && year >= 2000) // Easter Monday.
                || (day == 1 && month == 5 && year >= 2000) // Labour Day.
                || (day == 25 && month == 12) // Christmas.
                || (day == 26 && month == 12 && year >= 2000) // Day of Goodwill.
                || (day == 31 && month == 12 && (year == 1998 || year == 1999 || year == 2001)); // December 31st, 1998, 1999, and 2001 only.
        }

        /// <summary>
        /// Checks if the specified date is an TARGET
        /// (Trans-european Automated Real-time Gross settlement Express Transfer system)
        /// working day.
        /// <para/>
        /// See <c>http://www.ecb.int</c>.
        /// <para/>
        /// The holidays (apart from weekends) are:
        /// <para/>
        /// New Year's Day, Good Friday, Easter Monday, Labour Day, Christmas Day, Boxing Day.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if the specified date is a TARGET working day, false if not.</returns>
        public static bool IsTargetWorkday(this DateTime dateTime)
        {
            return !dateTime.IsTargetHoliday();
        }
    }
}
