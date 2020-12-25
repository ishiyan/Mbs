using System;
using Mbs.Trading.Markets;
#pragma warning disable SA1108 // Block statements should not contain embedded comments

namespace Mbs.Trading.Holidays
{
    /// <summary>
    /// Generic Norwegian exchange holiday schedule.
    /// </summary>
    public static class NorwegianHolidays
    {
        /// <summary>
        /// Checks if the specified date is a Norwegian holiday (non-trading day).
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Xosl"/>, <see cref="ExchangeMic.Xoas"/>, <see cref="ExchangeMic.Notc"/>, <see cref="ExchangeMic.Norx"/>.
        /// <para/>
        /// Not valid for <see cref="ExchangeMic.Xima"/>, <see cref="ExchangeMic.Fish"/>.
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
        /// <item><term>Labour Day</term><description>May 1st</description></item>
        /// <item><term>Constitution (National Independence) Day</term><description>May 17th</description></item>
        /// <item><term>Ascension Day</term><description></description></item>
        /// <item><term>Whit (Pentecost) Monday</term><description></description></item>
        /// <item><term>Christmas Eve</term><description>December 24th</description></item>
        /// <item><term>Christmas Day</term><description>December 25th</description></item>
        /// <item><term>Boxing Day</term><description>December 26th</description></item>
        /// <item><term>New Year's Eve</term><description>December 31st</description></item>
        /// </list>
        /// </remarks>
        /// <returns>True if the specified date is a Norwegian non-trading day, false if not.</returns>
        public static bool IsNorwegianHoliday(this DateTime dateTime)
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
                    // New Year's Day.
                    if (day == 1)
                    {
                        return true;
                    }

                    break;
                case 5:
                    // Labour Day, Constitution (National Independence) Day.
                    if (day == 1 || day == 17)
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

            return false;
        }

        /// <summary>
        /// Checks if the specified date is a Norwegian trading day.
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Xosl"/>, <see cref="ExchangeMic.Xoas"/>, <see cref="ExchangeMic.Notc"/>, <see cref="ExchangeMic.Norx"/>.
        /// <para/>
        /// Not valid for <see cref="ExchangeMic.Xima"/>, <see cref="ExchangeMic.Fish"/>.
        /// <para/>
        /// Verified using <c>http://www.marketholidays.com/HolidaysByCenter.aspx</c>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if the specified date is a Norwegian trading day, false if not.</returns>
        public static bool IsNorwegianWorkday(this DateTime dateTime)
        {
            return !dateTime.IsNorwegianHoliday();
        }
    }
}
