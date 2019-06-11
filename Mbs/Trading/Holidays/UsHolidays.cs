using System;
using Mbs.Trading.Markets;

namespace Mbs.Trading.Holidays
{
    /// <summary>
    /// Generic US exchange holiday schedule.
    /// </summary>
    public static class UsHolidays
    {
        /// <summary>
        /// Checks if the specified date is an US holiday (non-trading day).
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Edga"/>, <see cref="ExchangeMic.Edgx"/>, <see cref="ExchangeMic.Bato"/>, <see cref="ExchangeMic.Bats"/>, <see cref="ExchangeMic.Baty"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Bids"/>, <see cref="ExchangeMic.Xbox"/>, <see cref="ExchangeMic.C2Ox"/>, <see cref="ExchangeMic.Xcbo"/>, <see cref="ExchangeMic.Xcbf"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Fcbt"/>, <see cref="ExchangeMic.Xchi"/>, <see cref="ExchangeMic.Fcme"/>, <see cref="ExchangeMic.Xcme"/>, <see cref="ExchangeMic.Xcec"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Edgo"/>, <see cref="ExchangeMic.Eris"/>, <see cref="ExchangeMic.Ifus"/>, <see cref="ExchangeMic.Imfx"/>, <see cref="ExchangeMic.Iexg"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xisx"/>, <see cref="ExchangeMic.Gmni"/>, <see cref="ExchangeMic.Mcry"/>, <see cref="ExchangeMic.Xkbt"/>, <see cref="ExchangeMic.Xmio"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xmge"/>, <see cref="ExchangeMic.Gree"/>, <see cref="ExchangeMic.Pipe"/>, <see cref="ExchangeMic.Otcq"/>, <see cref="ExchangeMic.Psgm"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Pinx"/>, <see cref="ExchangeMic.Xpho"/>, <see cref="ExchangeMic.Ootc"/>, <see cref="ExchangeMic.Xotc"/>, <see cref="ExchangeMic.Xoch"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xnyl"/>, <see cref="ExchangeMic.Nodx"/>, <see cref="ExchangeMic.Xnym"/>, <see cref="ExchangeMic.Xcis"/>, <see cref="ExchangeMic.Xndq"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xpsx"/>, <see cref="ExchangeMic.Xphl"/>, <see cref="ExchangeMic.Xbxo"/>, <see cref="ExchangeMic.Xbos"/>, <see cref="ExchangeMic.Xnms"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xngs"/>, <see cref="ExchangeMic.Xncm"/>, <see cref="ExchangeMic.Xnas"/>, <see cref="ExchangeMic.Amxo"/>, <see cref="ExchangeMic.Xase"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Arco"/>, <see cref="ExchangeMic.Arcx"/>, <see cref="ExchangeMic.Xnys"/>.
        /// <para/>
        /// Verified using <c>https://www.marketholidays.com/HolidaysByCategory.aspx</c>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <remarks>
        /// <para>Public holidays (see: http://www.opm.gov/fedhol/)</para>
        /// <list type="table">
        /// <item><term>New Year's Day</term><description>January 1st (possibly moved to Monday if actually on Sunday, or to Friday if on Saturday)</description></item>
        /// <item><term>Martin Luther King's birthday</term><description>Third Monday in January</description></item>
        /// <item><term>Presidents' Day (a.k.a. Washington's birthday)</term><description>Third Monday in February</description></item>
        /// <item><term>Good Friday</term><description></description></item>
        /// <item><term>Memorial Day</term><description>Last Monday in May</description></item>
        /// <item><term>Independence Day</term><description>July 4th (moved to Monday if Sunday or Friday if Saturday)</description></item>
        /// <item><term>Labor Day</term><description>First Monday in September</description></item>
        /// <item><term>-DROPPED- Columbus Day</term><description>Second Monday in October</description></item>
        /// <item><term>-DROPPED- Veterans' Day</term><description>November 11th (moved to Monday if Sunday or Friday if Saturday)</description></item>
        /// <item><term>Thanksgiving Day</term><description>Fourth Thursday in November</description></item>
        /// <item><term>Presidential election day</term><description>First Tuesday in November of election years (until 1980)</description></item>
        /// <item><term>Christmas</term><description>December 25th (moved to Monday if Sunday or Friday if Saturday)</description></item>
        /// <item><term>Special historic closings</term><description>http://www.moneymentor.com/Reference%20Material/nyse_historical_closings.htm, http://www.ltadvisors.net/Info/research/closings.pdf</description></item>
        /// </list>
        /// </remarks>
        /// <returns>True if the specified date is an US non-trading day, false if not.</returns>
        public static bool IsUsHoliday(this DateTime dateTime)
        {
            DayOfWeek dow = dateTime.DayOfWeek;
            if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
                return true;

            int day = dateTime.Day;
            int month = dateTime.Month;
            int year = dateTime.Year;
            switch (month)
            {
                case 1:
                    // New Year's Day (possibly moved to Monday if on Sunday).
                    if (day == 1 || (day == 2 && dow == DayOfWeek.Monday))
                        return true;

                    // Martin Luther King's birthday (third Monday in January).
                    if (year >= 1998 && (day >= 15 && day <= 21) && dow == DayOfWeek.Monday)
                        return true;
                    break;

                case 2:
                    // Washington's birthday (third Monday in February).
                    if ((day >= 15 && day <= 21) && dow == DayOfWeek.Monday)
                        return true;
                    break;

                case 3:
                case 4:
                    // Good Friday.
                    if (dateTime.IsGoodFriday())
                        return true;
                    break;

                case 5:
                    // Memorial Day (last Monday in May).
                    if (day >= 25 && dow == DayOfWeek.Monday)
                        return true;
                    break;

                case 7:
                    // Independence Day (Monday if Sunday or Friday if Saturday).
                    if (day == 4 || (day == 5 && dow == DayOfWeek.Monday) || (day == 3 && dow == DayOfWeek.Friday))
                        return true;
                    break;

                case 9:
                    // Labor Day (first Monday in September).
                    if (day <= 7 && dow == DayOfWeek.Monday)
                        return true;
                    break;

                // case 10:
                //     // Columbus Day (second Monday in October)
                //     if ((day >= 8 && day <= 14) && dow == DayOfWeek.Monday)
                //         return true;
                //     break;
                case 11:
                    // // Veteran's Day (Monday if Sunday or Friday if Saturday)
                    // if (day == 11 || (day == 12 && dow == DayOfWeek.Monday) ||
                    //     (day == 10 && dow == DayOfWeek.Friday))
                    //     return true;

                    // Thanksgiving Day (fourth Thursday in November).
                    if ((day >= 22 && day <= 28) && dow == DayOfWeek.Thursday)
                        return true;

                    // Presidential election days.
                    if ((year <= 1968 || (year <= 1980 && year % 4 == 0)) && day <= 7 && dow == DayOfWeek.Tuesday)
                        return true;
                    break;

                case 12:
                    // Christmas (Monday if Sunday or Friday if Saturday).
                    if (day == 25 || (day == 26 && dow == DayOfWeek.Monday) || (day == 24 && dow == DayOfWeek.Friday))
                        return true;
                    break;
            }

            // Hurricane Sandy.
            if (year == 2012 && month == 10 && (day == 29 || day == 30))
                return true;

            // President Ford's funeral
            if (year == 2007 && month == 1 && day == 2)
                return true;

            // President Reagan's funeral
            if (year == 2004 && month == 6 && day == 11)
                return true;

            // Terrorism September 11-14, 2001
            if (year == 2001 && month == 9 && (day >= 11 && day <= 14))
                return true;

            // President Nixon's funeral
            if (year == 1994 && month == 4 && day == 27)
                return true;

            // Hurricane Gloria
            if (year == 1985 && month == 9 && day == 27)
                return true;

            // 1977 Blackout
            if (year == 1977 && month == 7 && day == 14)
                return true;

            // Funeral of former President Lyndon B. Johnson.
            if (year == 1973 && month == 1 && day == 25)
                return true;

            // Funeral of former President Harry S. Truman
            if (year == 1972 && month == 12 && day == 28)
                return true;

            if (year == 1969 &&

                // National Day of Participation for the lunar exploration.
                ((month == 7 && day == 21) ||

                // Funeral of former President Eisenhower.
                (month == 3 && day == 31) ||

                // Closed all day - heavy snow.
                (month == 2 && day == 10)))
                return true;

            if (year == 1968 &&

                // Day after Independence Day.
                ((month == 7 && day == 5) ||

                // June 12-Dec. 31, 1968. Four day week (closed on Wednesdays) - Paperwork Crisis.
                (dateTime.DayOfYear >= 163 && dow == DayOfWeek.Wednesday) ||

                // Day of mourning for Martin Luther King Jr.
                (month == 4 && day == 9)))
                return true;

            // Funeral of President Kennedy.
            if (year == 1963 && month == 11 && day == 25)
                return true;

            // Day before Decoration Day.
            if (year == 1961 && month == 5 && day == 26)
                return true;

            // Day after Christmas.
            if (year == 1958 && month == 12 && day == 26)
                return true;

            // Christmas Eve.
            if ((year == 1954 || year == 1956 || year == 1965) && month == 12 && day == 24)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if the specified date is an US trading day.
        /// <para/>
        /// Valid for the following ISO 10383 Market Identifier Codes:
        /// <para/>
        /// <see cref="ExchangeMic.Edga"/>, <see cref="ExchangeMic.Edgx"/>, <see cref="ExchangeMic.Bato"/>, <see cref="ExchangeMic.Bats"/>, <see cref="ExchangeMic.Baty"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Bids"/>, <see cref="ExchangeMic.Xbox"/>, <see cref="ExchangeMic.C2Ox"/>, <see cref="ExchangeMic.Xcbo"/>, <see cref="ExchangeMic.Xcbf"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Fcbt"/>, <see cref="ExchangeMic.Xchi"/>, <see cref="ExchangeMic.Fcme"/>, <see cref="ExchangeMic.Xcme"/>, <see cref="ExchangeMic.Xcec"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Edgo"/>, <see cref="ExchangeMic.Eris"/>, <see cref="ExchangeMic.Ifus"/>, <see cref="ExchangeMic.Imfx"/>, <see cref="ExchangeMic.Iexg"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xisx"/>, <see cref="ExchangeMic.Gmni"/>, <see cref="ExchangeMic.Mcry"/>, <see cref="ExchangeMic.Xkbt"/>, <see cref="ExchangeMic.Xmio"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xmge"/>, <see cref="ExchangeMic.Gree"/>, <see cref="ExchangeMic.Pipe"/>, <see cref="ExchangeMic.Otcq"/>, <see cref="ExchangeMic.Psgm"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Pinx"/>, <see cref="ExchangeMic.Xpho"/>, <see cref="ExchangeMic.Ootc"/>, <see cref="ExchangeMic.Xotc"/>, <see cref="ExchangeMic.Xoch"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xnyl"/>, <see cref="ExchangeMic.Nodx"/>, <see cref="ExchangeMic.Xnym"/>, <see cref="ExchangeMic.Xcis"/>, <see cref="ExchangeMic.Xndq"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xpsx"/>, <see cref="ExchangeMic.Xphl"/>, <see cref="ExchangeMic.Xbxo"/>, <see cref="ExchangeMic.Xbos"/>, <see cref="ExchangeMic.Xnms"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Xngs"/>, <see cref="ExchangeMic.Xncm"/>, <see cref="ExchangeMic.Xnas"/>, <see cref="ExchangeMic.Amxo"/>, <see cref="ExchangeMic.Xase"/>,
        /// <para/>
        /// <see cref="ExchangeMic.Arco"/>, <see cref="ExchangeMic.Arcx"/>, <see cref="ExchangeMic.Xnys"/>.
        /// <para/>
        /// Verified using <c>https://www.marketholidays.com/HolidaysByCategory.aspx</c>.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if the specified date is an US trading day, false if not.</returns>
        public static bool IsUsWorkday(this DateTime dateTime)
        {
            return !dateTime.IsUsHoliday();
        }
    }
}
