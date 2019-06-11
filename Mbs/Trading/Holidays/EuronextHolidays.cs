using System;

namespace Mbs.Trading.Holidays
{
    /// <summary>
    /// The EuroNext exchange holiday schedule.
    /// </summary>
    public static class EuronextHolidays
    {
        /// <summary>
        /// Checks if the specified date is a EuroNext holiday (non-trading day).
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if the specified date is a EuroNext holiday.</returns>
        /// <remarks>
        /// <para>Sources:</para>
        /// <para>EuroNext calendar of business days 2018.</para>
        /// <list type="table">
        /// <item><term>Monday 1 January 2018</term><description>New Year's Day</description></item>
        /// <item><term>Friday 30 March 2018</term><description>Good Friday</description></item>
        /// <item><term>Monday 2 April 2018</term><description>Easter Monday</description></item>
        /// <item><term>Tuesday 1 May 2018</term><description>Labour Day</description></item>
        /// <item><term>Tuesday 25 December 2018</term><description>Christmas Day</description></item>
        /// <item><term>Wednesday 26 December 2018</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>EuroNext calendar of business days 2017.</para>
        /// <list type="table">
        /// <item><term>Friday 14 April 2017</term><description>Good Friday</description></item>
        /// <item><term>Monday 17 April 2017</term><description>Easter Monday</description></item>
        /// <item><term>Monday 1 May 2017</term><description>Labour Day</description></item>
        /// <item><term>Monday 25 December 2017</term><description>Christmas Day</description></item>
        /// <item><term>Tuesday 26 December 2017</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>EuroNext calendar of business days 2016.</para>
        /// <list type="table">
        /// <item><term>Friday 1 January 2016</term><description>New Year's Day</description></item>
        /// <item><term>Friday 25 March 2016</term><description>Good Friday</description></item>
        /// <item><term>Monday 28 March 2016</term><description>Easter Monday</description></item>
        /// <item><term>Monday 26 December 2016</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>Euronext stock exchange holidays – 2015.</para>
        /// <list type="table">
        /// <item><term>January 1, Thursday</term><description>New Year's Day</description></item>
        /// <item><term>April 3, Friday</term><description>Good Friday</description></item>
        /// <item><term>April 6, Monday</term><description>Easter Monday</description></item>
        /// <item><term>May 1, Friday</term><description>Labour Day</description></item>
        /// <item><term>December 25, Friday</term><description>Christmas Day</description></item>
        /// </list>
        /// <para>Euronext’s 2014 holiday calendar for its European markets.</para>
        /// <list type="table">
        /// <item><term>Wednesday 1 January</term><description>New Year's Day</description></item>
        /// <item><term>Friday 18 April</term><description>Good Friday</description></item>
        /// <item><term>Monday 21 April</term><description>Easter Monday</description></item>
        /// <item><term>Thursday 1 May</term><description>Labour Day</description></item>
        /// <item><term>Thursday 25 December</term><description>Christmas Day</description></item>
        /// <item><term>Friday 26 December</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>Euronext 2013 holiday schedule.</para>
        /// <list type="table">
        /// <item><term>January 1</term><description>New Year's Day</description></item>
        /// <item><term>Friday March 29</term><description>Good Friday</description></item>
        /// <item><term>Monday April 1</term><description>Easter Monday</description></item>
        /// <item><term>May 1</term><description>Labour Day</description></item>
        /// <item><term>December 25</term><description>Christmas Day</description></item>
        /// <item><term>December 26</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>Euronext 2012 holiday schedule.</para>
        /// <list type="table">
        /// <item><term>January 1</term><description>New Year's Day</description></item>
        /// <item><term>Friday April 6</term><description>Good Friday</description></item>
        /// <item><term>Monday April 9</term><description>Easter Monday</description></item>
        /// <item><term>May 1</term><description>Labour Day</description></item>
        /// <item><term>December 25</term><description>Christmas Day</description></item>
        /// <item><term>December 26</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>NYSE EuroNext Announces its 2011 Holiday Calendar.</para>
        /// <list type="table">
        /// <item><term>Friday 22 April 2011</term><description>Good Friday</description></item>
        /// <item><term>Monday 25 April 2011</term><description>Easter Monday</description></item>
        /// <item><term>Monday 26 December 2011</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>NYSE EuroNext Announces its 2010 Holiday Calendar.</para>
        /// <list type="table">
        /// <item><term>Friday 1 January 2010</term><description>New Year's Day</description></item>
        /// <item><term>Friday 2 April 2010</term><description>Good Friday</description></item>
        /// <item><term>Monday 5 April 2010</term><description>Easter Monday</description></item>
        /// </list>
        /// <para>NYSE EuroNext Announces its 2009 Holiday Calendar.</para>
        /// <list type="table">
        /// <item><term>Thursday 1 January 2009</term><description>New Year's Day</description></item>
        /// <item><term>Friday 10 April 2009</term><description>Good Friday</description></item>
        /// <item><term>Monday 13 April 2009</term><description>Easter Monday</description></item>
        /// <item><term>Friday 1 May 2009 (*)</term><description>Labour Day</description></item>
        /// <item><term>Friday 25 December 2009</term><description>Christmas Day</description></item>
        /// </list>
        /// <para>NYSE EuroNext Announces its 2008 Holiday Calendar.</para>
        /// <list type="table">
        /// <item><term>Tuesday 1 January 2008</term><description>New Year's Day</description></item>
        /// <item><term>Friday 21 March 2008</term><description>Good Friday</description></item>
        /// <item><term>Monday 24 March 2008</term><description>Easter Monday</description></item>
        /// <item><term>Thursday 1 May 2008 (*)</term><description>Labour Day</description></item>
        /// <item><term>Thursday 25 December 2008</term><description>Christmas Day</description></item>
        /// <item><term>Friday 26 December 2008</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>2007 calendar for EuroNext markets.</para>
        /// <list type="table">
        /// <item><term>Monday 1 January 2007</term><description>New Year's Day</description></item>
        /// <item><term>Friday 6 April 2007</term><description>Good Friday</description></item>
        /// <item><term>Monday 9 April 2007</term><description>Easter Monday</description></item>
        /// <item><term>Tuesday 1 May 2007 (*)</term><description>Labour Day</description></item>
        /// <item><term>Tuesday 25 December 2007</term><description>Christmas Day</description></item>
        /// <item><term>Wednesday 26 December 2007</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>2006 calendar for EuroNext markets.</para>
        /// <list type="table">
        /// <item><term>Friday 14 April 2006</term><description>Good Friday</description></item>
        /// <item><term>Monday 17 April 2006</term><description>Easter Monday</description></item>
        /// <item><term>Monday 1 May 2006 (*)</term><description>Labour Day</description></item>
        /// <item><term>Monday 25 December 2006</term><description>Christmas Day</description></item>
        /// <item><term>Tuesday 26 December 2006</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>2005 calendar for EuroNext markets.</para>
        /// <list type="table">
        /// <item><term>Friday 25 March 2005</term><description>Good Friday</description></item>
        /// <item><term>Monday 28 March 2005</term><description>Easter Monday</description></item>
        /// <item><term>Monday 26 December 2005</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>2004 calendar for EuroNext markets.</para>
        /// <list type="table">
        /// <item><term>Thursday 1 January 2004</term><description>New Year's Day</description></item>
        /// <item><term>Friday 9 April 2004</term><description>Good Friday</description></item>
        /// <item><term>Monday 12 April 2004</term><description>Easter Monday</description></item>
        /// </list>
        /// <para>2003 calendar for EuroNext markets.</para>
        /// <list type="table">
        /// <item><term>Wednesday 1 January 2003</term><description>New Year's Day</description></item>
        /// <item><term>Friday 18 April 2003</term><description>Good Friday</description></item>
        /// <item><term>Monday 21 April 2003</term><description>Easter Monday</description></item>
        /// <item><term>Thursday 1 May 2003 (*)</term><description>Labour Day</description></item>
        /// <item><term>Thursday 25 December 2003</term><description>Christmas Day</description></item>
        /// <item><term>Friday 26 December 203</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>2002 calendar for EuroNext markets.</para>
        /// <list type="table">
        /// <item><term>Tuesday 1 January 2002</term><description>New Year's Day</description></item>
        /// <item><term>Friday 29 March 2002</term><description>Good Friday</description></item>
        /// <item><term>Monday 1st April 2002</term><description>Easter Monday</description></item>
        /// <item><term>Wednesday 1st May 2002 (*)</term><description>Labour Day</description></item>
        /// <item><term>Wednesday 25 December 2002</term><description>Christmas Day</description></item>
        /// <item><term>Thursday 26 December 2002</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>2001 calendar for EuroNext markets.</para>
        /// <list type="table">
        /// <item><term>Monday 1 January 2001</term><description>New Year's Day</description></item>
        /// <item><term>Friday 13 April 2001</term><description>Good Friday</description></item>
        /// <item><term>Monday 16 April 2001</term><description>Easter Monday</description></item>
        /// <item><term>Tuesday 1 May 2001 (*)</term><description>Labour Day</description></item>
        /// <item><term>Monday 4 June 2001</term><description>Whit Monday</description></item>
        /// <item><term>Tuesday 25 December 2001</term><description>Christmas Day</description></item>
        /// <item><term>Wednesday 26 December 2001</term><description>Boxing Day</description></item>
        /// <item><term>Monday 31 December 2001</term><description>Last trading day - change over to euro</description></item>
        /// </list>
        /// <para>2000 calendar for EuroNext markets.</para>
        /// <list type="table">
        /// <item><term>Friday 21 April 2000</term><description>Good Friday</description></item>
        /// <item><term>Monday 24 April 2000</term><description>Easter Monday</description></item>
        /// <item><term>Monday 1 May 2000 (*)</term><description>Labour Day</description></item>
        /// <item><term>Monday 12 June 2000</term><description>Whit Monday</description></item>
        /// <item><term>Monday 25 December 2000</term><description>Christmas Day</description></item>
        /// <item><term>Tuesday 26 December 2000</term><description>Boxing Day</description></item>
        /// </list>
        /// <para>1999 calendar for EuroNext markets.</para>
        /// <list type="table">
        /// <item><term>Friday 1 January 1999</term><description>New Year's Day</description></item>
        /// <item><term>Friday 2 April 1999</term><description>Good Friday</description></item>
        /// <item><term>Monday 5 April 1999</term><description>Easter Monday</description></item>
        /// <item><term>Monday 24 May 1999</term><description>Whit Monday</description></item>
        /// </list>
        /// <para>(*) Certain Euronext Liffe contracts will however be open, i.e. interest rate products, UK-based commodity contracts and those derivatives for which the underlying stocks are available for trading.</para>
        /// </remarks>
        public static bool IsEuronextHoliday(this DateTime dateTime)
        {
            DayOfWeek dow = dateTime.DayOfWeek;
            if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
                return true;

            int day = dateTime.Day;
            int month = dateTime.Month;
            switch (dateTime.Year)
            {
                case 2018:
                    switch (month)
                    {
                        case 1:
                            return day == 1; // Monday 1 January 2018
                        case 3:
                            return day == 30; // Friday 30 March 2018
                        case 4:
                            return day == 2; // Monday 2 April 2018
                        case 5:
                            return day == 1; // Tuesday 1 May 2018
                        case 12:
                            return day == 25 || day == 26; // Tuesday 25 December 2018, Wednesday 26 December 2018
                        default:
                            return false;
                    }

                case 2017:
                    switch (month)
                    {
                        case 4:
                            return day == 14 || day == 17; // Friday 14 April 2017, Monday 17 April 2017
                        case 5:
                            return day == 1; // Monday 1 May 2017
                        case 12:
                            return day == 25 || day == 26; // Monday 25 December 2017, Tuesday 26 December 2017
                        default:
                            return false;
                    }

                case 2016:
                    switch (month)
                    {
                        case 1:
                            return day == 1; // Friday January 1 2016
                        case 3:
                            return day == 25 || day == 28; // Friday 25 March 2016, Monday 28 March 2016
                        case 12:
                            return day == 26; // Monday 26 December 2016
                        default:
                            return false;
                    }

                case 2015:
                    switch (month)
                    {
                        case 1:
                        case 5:
                            return day == 1; // Thursday January 1 2015, Friday May 1 2015
                        case 4:
                            return day == 3 || day == 6; // Friday 3 April 2015, Monday 6 April 2015
                        case 12:
                            return day == 25; // Friday 25 December 2015
                        default:
                            return false;
                    }

                case 2014:
                    switch (month)
                    {
                        case 1:
                        case 5:
                            return day == 1; // Wednesday January 1 2014, Thursday May 1 2014
                        case 4:
                            return day == 18 || day == 21; // Friday 18 April 2014, Monday 21 April 2014
                        case 12:
                            return day == 25 || day == 26; // Thursday 25 December 2014, Friday 26 December 2014
                        default:
                            return false;
                    }

                case 2013:
                    switch (month)
                    {
                        case 1:
                        case 4:
                        case 5:
                            return day == 1; // January 1 2013, Monday April 1 2013, May 1 2013
                        case 3:
                            return day == 29; // Friday March 29 2013
                        case 12:
                            return day == 25 || day == 26; // 25 December 2013, 26 December 2013
                        default:
                            return false;
                    }

                case 2012:
                    switch (month)
                    {
                        case 1:
                        case 5:
                            return day == 1; // January 1 2012, May 1 2012
                        case 4:
                            return day == 6 || day == 9; // Friday 6 April 2012, Monday 9 April 2012
                        case 12:
                            return day == 25 || day == 26; // 25 December 2012, 26 December 2012
                        default:
                            return false;
                    }

                case 2011:
                    switch (month)
                    {
                        case 4:
                            return day == 22 || day == 25; // Friday 22 April 2011, Monday 25 April 2011
                        case 12:
                            return day == 26; // Monday 26 December 2011
                        default:
                            return false;
                    }

                case 2010:
                    switch (month)
                    {
                        case 1:
                            return day == 1; // Friday 1 January 2010
                        case 4:
                            return day == 2 || day == 5; // Friday 2 April 2010, Monday 5 April 2010
                        default:
                            return false;
                    }

                case 2009:
                    switch (month)
                    {
                        case 1:
                        case 5:
                            return day == 1; // Thursday 1 January 2009, Friday 1 May 2009
                        case 4:
                            return day == 10 || day == 13; // Friday 10 April 2009, Monday 13 April 2009
                        case 12:
                            return day == 25; // Friday 25 December 2009
                        default:
                            return false;
                    }

                case 2008:
                    switch (month)
                    {
                        case 1:
                        case 5:
                            return day == 1; // Tuesday 1 January 2008, Thursday 1 May 2008
                        case 3:
                            return day == 21 || day == 24; // Friday 21 March 2008, Monday 24 March 2008
                        case 12:
                            return day == 25 || day == 26; // Thursday 25 December 2008, Friday 26 December 2008
                        default:
                            return false;
                    }

                case 2007:
                    switch (month)
                    {
                        case 1:
                        case 5:
                            return day == 1; // Monday 1 January 2007, Tuesday 1 May 2007
                        case 4:
                            return day == 6 || day == 9; // Friday 6 April 2007, Monday 9 April 2007
                        case 12:
                            return day == 25 || day == 26; // Tuesday 25 December 2007, Wednesday 26 December 2007
                        default:
                            return false;
                    }

                case 2006:
                    switch (month)
                    {
                        case 5:
                            return day == 1; // Monday 1 May 2006
                        case 4:
                            return day == 14 || day == 17; // Friday 14 April 2006, Monday 17 April 2006
                        case 12:
                            return day == 25 || day == 26; // Monday 25 December 2006, Tuesday 26 December 2006
                        default:
                            return false;
                    }

                case 2005:
                    switch (month)
                    {
                        case 3:
                            return day == 25 || day == 28; // Friday 25 March 2005, Monday 28 March 2005
                        case 12:
                            return day == 26; // Monday 26 December 2005
                        default:
                            return false;
                    }

                case 2004:
                    switch (month)
                    {
                        case 1:
                            return day == 1; // Thursday 1 January 2004
                        case 4:
                            return day == 9 || day == 12; // Friday 9 April 2004, Monday 12 April 2004
                        default:
                            return false;
                    }

                case 2003:
                    switch (month)
                    {
                        case 1:
                        case 5:
                            return day == 1; // Wednesday 1 January 2003, Thursday 1 May 2003
                        case 4:
                            return day == 18 || day == 21; // Friday 18 April 2003, Monday 21 April 2003
                        case 12:
                            return day == 25 || day == 26; // Thursday 25 December 2003, Friday 26 December 2003
                        default:
                            return false;
                    }

                case 2002:
                    switch (month)
                    {
                        case 1:
                        case 4:
                        case 5:
                            return day == 1; // Tuesday 1 January 2002, Monday 1 April 2002, Wednesday 1 May 2002
                        case 3:
                            return day == 29; // Friday 29 March 2002
                        case 12:
                            return day == 25 || day == 26; // Wednesday 25 December 2002, Thursday 26 December 2002
                        default:
                            return false;
                    }

                case 2001:
                    switch (month)
                    {
                        case 1:
                            return day == 1; // Monday 1 January 2001
                        case 6:
                            return day == 4; // Monday 4 June 2001, Whit Monday
                        case 4:
                            return day == 13 || day == 16; // Friday 13 April 2001, Monday 16 April 2001
                        case 12:
                            return day == 25 || day == 26 || day == 31; // Tuesday 25 December 2001, Wednesday 26 December 2001, Monday 31 December 2001, change over to Euro
                        default:
                            return false;
                    }

                case 2000:
                    switch (month)
                    {
                        case 4:
                            return day == 21 || day == 24; // Friday 21 April 2000, Monday 24 April 2000
                        case 6:
                            return day == 12; // Monday 12 June 2000, Whit Monday
                        case 12:
                            return day == 25 || day == 26; // Monday 25 December 2000, Tuesday 26 December 2000
                        default:
                            return false;
                    }

                case 1999:
                    switch (month)
                    {
                        case 1:
                            return day == 1; // Friday 1 January 1999
                        case 4:
                            return day == 2 || day == 5; // Friday 2 April 1999, Monday 5 April 1999
                        case 5:
                            return day == 24; // Monday 24 May 1999, Whit Monday
                        default:
                            return false;
                    }
            }

            switch (month)
            {
                case 1:
                    if (day == 1) // New Year's Day
                        return true;
                    break;
                case 12:
                    if (day == 25 || day == 26) // Christmas Day, Boxing Day
                        return true;
                    break;
            }

            return dateTime.IsGoodFriday() || dateTime.IsEasterMonday();
        }

        /// <summary>
        /// Checks if the specified date is a EuroNext working (trading) day.
        /// </summary>
        /// <param name="dateTime">A date.</param>
        /// <returns>True if the specified date is a EuroNext workday.</returns>
        public static bool IsEuronextWorkday(this DateTime dateTime)
        {
            return !dateTime.IsEuronextHoliday();
        }
    }
}
