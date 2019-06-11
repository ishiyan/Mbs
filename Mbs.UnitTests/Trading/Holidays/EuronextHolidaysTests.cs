using System;
using Mbs.Trading.Holidays;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Holidays
{
    [TestClass]
    public class EuronextHolidaysTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void EuronextHoliday_IsEuronextHoliday_ExplicitlyKnownDays_AreAlwaysHolidays()
        {
            Assert.IsTrue(new DateTime(2018, 1, 1).IsEuronextHoliday(), "Monday 1 January 2018 [New Year's Day]");
            Assert.IsTrue(new DateTime(2018, 3, 30).IsEuronextHoliday(), "Friday 30 March 2018 [Good Friday]");
            Assert.IsTrue(new DateTime(2018, 4, 2).IsEuronextHoliday(), "Monday 2 April 2018 [Easter Monday]");
            Assert.IsTrue(new DateTime(2018, 5, 1).IsEuronextHoliday(), "Tuesday 1 May 2018 [Labour Day]");
            Assert.IsTrue(new DateTime(2018, 12, 25).IsEuronextHoliday(), "Tuesday 25 December 2018 [Christmas Day]");
            Assert.IsTrue(new DateTime(2018, 12, 26).IsEuronextHoliday(), "Wednesday 26 December 2018 [Boxing Day]");

            Assert.IsTrue(new DateTime(2017, 4, 14).IsEuronextHoliday(), "Friday 14 April 2017 [Good Friday]");
            Assert.IsTrue(new DateTime(2017, 4, 17).IsEuronextHoliday(), "Monday 17 April 2017 [Easter Monday]");
            Assert.IsTrue(new DateTime(2017, 5, 1).IsEuronextHoliday(), "Monday 1 May 2017 [Labour Day]");
            Assert.IsTrue(new DateTime(2017, 12, 25).IsEuronextHoliday(), "Monday 25 December 2017 [Christmas Day]");
            Assert.IsTrue(new DateTime(2017, 12, 26).IsEuronextHoliday(), "Tuesday 26 December 2017 [Boxing Day]");

            Assert.IsTrue(new DateTime(2016, 1, 1).IsEuronextHoliday(), "Friday 1 January 2016 [New Year's Day]");
            Assert.IsTrue(new DateTime(2016, 3, 25).IsEuronextHoliday(), "Friday 25 March 2016 [Good Friday]");
            Assert.IsTrue(new DateTime(2016, 3, 28).IsEuronextHoliday(), "Monday 28 March 2016 [Easter Monday]");
            Assert.IsTrue(new DateTime(2016, 12, 26).IsEuronextHoliday(), "Monday 26 December 2016 [Boxing Day]");

            Assert.IsTrue(new DateTime(2015, 1, 1).IsEuronextHoliday(), "Thursday 1 January 2015 [New Year's Day]");
            Assert.IsTrue(new DateTime(2015, 4, 3).IsEuronextHoliday(), "Friday 3 April 2015 [Good Friday]");
            Assert.IsTrue(new DateTime(2015, 4, 6).IsEuronextHoliday(), "Monday 6 April 2015 [Easter Monday]");
            Assert.IsTrue(new DateTime(2015, 5, 1).IsEuronextHoliday(), "Friday 1 May 2015 [Labour Day]");
            Assert.IsTrue(new DateTime(2015, 12, 25).IsEuronextHoliday(), "Friday 25 December 2015 [Christmas Day]");

            Assert.IsTrue(new DateTime(2014, 1, 1).IsEuronextHoliday(), "Wednesday 1 January 2014 [New Year's Day]");
            Assert.IsTrue(new DateTime(2014, 4, 18).IsEuronextHoliday(), "Friday 18 April 2014 [Good Friday]");
            Assert.IsTrue(new DateTime(2014, 4, 21).IsEuronextHoliday(), "Monday 21 April 2014 [Easter Monday]");
            Assert.IsTrue(new DateTime(2014, 5, 1).IsEuronextHoliday(), "Thursday 1 May 2014 [Labour Day]");
            Assert.IsTrue(new DateTime(2014, 12, 25).IsEuronextHoliday(), "Thursday 25 December 2014 [Christmas Day]");
            Assert.IsTrue(new DateTime(2014, 12, 26).IsEuronextHoliday(), "Friday 26 December 2014 [Boxing Day]");

            Assert.IsTrue(new DateTime(2013, 1, 1).IsEuronextHoliday(), "1 January 2013 [New Year's Day]");
            Assert.IsTrue(new DateTime(2013, 3, 29).IsEuronextHoliday(), "Friday 29 March 2013 [Good Friday]");
            Assert.IsTrue(new DateTime(2013, 4, 1).IsEuronextHoliday(), "Monday 1 April 2013 [Easter Monday]");
            Assert.IsTrue(new DateTime(2013, 5, 1).IsEuronextHoliday(), "1 May 2013 [Labour Day]");
            Assert.IsTrue(new DateTime(2013, 12, 25).IsEuronextHoliday(), "25 December 2013 [Christmas Day]");
            Assert.IsTrue(new DateTime(2013, 12, 26).IsEuronextHoliday(), "26 December 2013 [Boxing Day]");

            Assert.IsTrue(new DateTime(2012, 1, 1).IsEuronextHoliday(), "1 January 2012 [New Year's Day]");
            Assert.IsTrue(new DateTime(2012, 4, 6).IsEuronextHoliday(), "Friday 6 April 2012 [Good Friday]");
            Assert.IsTrue(new DateTime(2012, 4, 9).IsEuronextHoliday(), "Monday 9 April 2012 [Easter Monday]");
            Assert.IsTrue(new DateTime(2012, 5, 1).IsEuronextHoliday(), "1 May 2012 [Labour Day]");
            Assert.IsTrue(new DateTime(2012, 12, 25).IsEuronextHoliday(), "25 December 2012 [Christmas Day]");
            Assert.IsTrue(new DateTime(2012, 12, 26).IsEuronextHoliday(), "26 December 2012 [Boxing Day]");

            Assert.IsTrue(new DateTime(2011, 4, 22).IsEuronextHoliday(), "Friday 22 April 2011 [Good Friday]");
            Assert.IsTrue(new DateTime(2011, 4, 25).IsEuronextHoliday(), "Monday 25 April 2011 [Easter Monday]");
            Assert.IsTrue(new DateTime(2011, 12, 26).IsEuronextHoliday(), "Monday 26 December 2011 [Boxing Day]");

            Assert.IsTrue(new DateTime(2010, 1, 1).IsEuronextHoliday(), "Friday 1 January 2010 [New Year's Day]");
            Assert.IsTrue(new DateTime(2010, 4, 2).IsEuronextHoliday(), "Friday 2 April 2010 [Good Friday]");
            Assert.IsTrue(new DateTime(2010, 4, 5).IsEuronextHoliday(), "Monday 5 April 2010 [Easter Monday]");

            Assert.IsTrue(new DateTime(2009, 1, 1).IsEuronextHoliday(), "Thursday 1 January 2009 [New Year's Day]");
            Assert.IsTrue(new DateTime(2009, 4, 10).IsEuronextHoliday(), "Friday 10 April 2009 [Good Friday]");
            Assert.IsTrue(new DateTime(2009, 4, 13).IsEuronextHoliday(), "Monday 13 April 2009 [Easter Monday]");
            Assert.IsTrue(new DateTime(2009, 5, 1).IsEuronextHoliday(), "Friday 1 May 2009 [Labour Day]");
            Assert.IsTrue(new DateTime(2009, 12, 25).IsEuronextHoliday(), "Friday 25 December 2009 [Christmas Day]");

            Assert.IsTrue(new DateTime(2008, 1, 1).IsEuronextHoliday(), "Tuesday 1 January 2008 [New Year's Day]");
            Assert.IsTrue(new DateTime(2008, 3, 21).IsEuronextHoliday(), "Friday 21 March 2008 [Good Friday]");
            Assert.IsTrue(new DateTime(2008, 3, 24).IsEuronextHoliday(), "Monday 24 March 2008 [Easter Monday]");
            Assert.IsTrue(new DateTime(2008, 5, 1).IsEuronextHoliday(), "Thursday 1 May 2008 [Labour Day]");
            Assert.IsTrue(new DateTime(2008, 12, 25).IsEuronextHoliday(), "Thursday 25 December 2008 [Christmas Day]");
            Assert.IsTrue(new DateTime(2008, 12, 26).IsEuronextHoliday(), "Friday 26 December 2008 [Boxing Day]");

            Assert.IsTrue(new DateTime(2007, 1, 1).IsEuronextHoliday(), "Monday 1 January 2007 [New Year's Day]");
            Assert.IsTrue(new DateTime(2007, 4, 6).IsEuronextHoliday(), "Friday 6 April 2007 [Good Friday]");
            Assert.IsTrue(new DateTime(2007, 4, 9).IsEuronextHoliday(), "Monday 9 April 2007 [Easter Monday]");
            Assert.IsTrue(new DateTime(2007, 5, 1).IsEuronextHoliday(), "Tuesday 1 May 2007 [Labour Day]");
            Assert.IsTrue(new DateTime(2007, 12, 25).IsEuronextHoliday(), "Tuesday 25 December 2007 [Christmas Day]");
            Assert.IsTrue(new DateTime(2007, 12, 26).IsEuronextHoliday(), "Wednesday 26 December 2007 [Boxing Day]");

            Assert.IsTrue(new DateTime(2006, 4, 14).IsEuronextHoliday(), "Friday 14 April 2006 [Good Friday]");
            Assert.IsTrue(new DateTime(2006, 4, 17).IsEuronextHoliday(), "Monday 17 April 2006 [Easter Monday]");
            Assert.IsTrue(new DateTime(2006, 5, 1).IsEuronextHoliday(), "Monday 1 May 2006 [Labour Day]");
            Assert.IsTrue(new DateTime(2006, 12, 25).IsEuronextHoliday(), "Monday 25 December 2006 [Christmas Day]");
            Assert.IsTrue(new DateTime(2006, 12, 26).IsEuronextHoliday(), "Tuesday 26 December 2006 [Boxing Day]");

            Assert.IsTrue(new DateTime(2005, 3, 25).IsEuronextHoliday(), "Friday 25 March 2005 [Good Friday]");
            Assert.IsTrue(new DateTime(2005, 3, 28).IsEuronextHoliday(), "Monday 28 March 2005 [Easter Monday]");
            Assert.IsTrue(new DateTime(2005, 12, 26).IsEuronextHoliday(), "Monday 26 December 2005 [Boxing Day]");

            Assert.IsTrue(new DateTime(2004, 1, 1).IsEuronextHoliday(), "Thursday 1 January 2004 [New Year's Day]");
            Assert.IsTrue(new DateTime(2004, 4, 9).IsEuronextHoliday(), "Friday 9 April 2004 [Good Friday]");
            Assert.IsTrue(new DateTime(2004, 4, 12).IsEuronextHoliday(), "Monday 12 April 2004 [Easter Monday]");

            Assert.IsTrue(new DateTime(2003, 1, 1).IsEuronextHoliday(), "Wednesday 1 January 2003 [New Year's Day]");
            Assert.IsTrue(new DateTime(2003, 4, 18).IsEuronextHoliday(), "Friday 18 April 2003 [Good Friday]");
            Assert.IsTrue(new DateTime(2003, 4, 21).IsEuronextHoliday(), "Monday 21 April 2003 [Easter Monday]");
            Assert.IsTrue(new DateTime(2003, 5, 1).IsEuronextHoliday(), "Thursday 1 May 2003 [Labour Day]");
            Assert.IsTrue(new DateTime(2003, 12, 25).IsEuronextHoliday(), "Thursday 25 December 2003 [Christmas Day]");
            Assert.IsTrue(new DateTime(2003, 12, 26).IsEuronextHoliday(), "Friday 26 December 2003 [Boxing Day]");

            Assert.IsTrue(new DateTime(2002, 1, 1).IsEuronextHoliday(), "Tuesday 1 January 2002 [New Year's Day]");
            Assert.IsTrue(new DateTime(2002, 3, 29).IsEuronextHoliday(), "Friday 29 March 2002 [Good Friday]");
            Assert.IsTrue(new DateTime(2002, 4, 1).IsEuronextHoliday(), "Monday 1 April 2002 [Easter Monday]");
            Assert.IsTrue(new DateTime(2002, 5, 1).IsEuronextHoliday(), "Wednesday 1 May 2002 [Labour Day]");
            Assert.IsTrue(new DateTime(2002, 12, 25).IsEuronextHoliday(), "Wednesday 25 December 2002 [Christmas Day]");
            Assert.IsTrue(new DateTime(2002, 12, 26).IsEuronextHoliday(), "Thursday 26 December 2002 [Boxing Day]");

            Assert.IsTrue(new DateTime(2001, 1, 1).IsEuronextHoliday(), "Monday 1 January 2001 [New Year's Day]");
            Assert.IsTrue(new DateTime(2001, 4, 13).IsEuronextHoliday(), "Friday 13 April 2001 [Good Friday]");
            Assert.IsTrue(new DateTime(2001, 4, 16).IsEuronextHoliday(), "Monday 16 April 2001 [Easter Monday]");
            Assert.IsTrue(new DateTime(2001, 6, 4).IsEuronextHoliday(), "Monday 4 June 2001 [Whit Monday]");
            Assert.IsTrue(new DateTime(2001, 12, 25).IsEuronextHoliday(), "Tuesday 25 December 2001 [Christmas Day]");
            Assert.IsTrue(new DateTime(2001, 12, 26).IsEuronextHoliday(), "Wednesday 26 December 2001 [Boxing Day]");
            Assert.IsTrue(new DateTime(2001, 12, 31).IsEuronextHoliday(), "Monday 31 December 2001 [Change over to euro]");

            Assert.IsTrue(new DateTime(2000, 4, 21).IsEuronextHoliday(), "Friday 21 April 2000 [Good Friday]");
            Assert.IsTrue(new DateTime(2000, 4, 24).IsEuronextHoliday(), "Monday 24 April 2000 [Easter Monday]");
            Assert.IsTrue(new DateTime(2000, 6, 12).IsEuronextHoliday(), "Monday 12 June 2000 [Whit Monday]");
            Assert.IsTrue(new DateTime(2000, 12, 25).IsEuronextHoliday(), "Monday 25 December 2000 [Christmas Day]");
            Assert.IsTrue(new DateTime(2000, 12, 26).IsEuronextHoliday(), "Tuesday 26 December 2000 [Boxing Day]");

            Assert.IsTrue(new DateTime(1999, 1, 1).IsEuronextHoliday(), "Friday 1 January 1999 [New Year's Day]");
            Assert.IsTrue(new DateTime(1999, 4, 2).IsEuronextHoliday(), "Friday 2 April 1999 [Good Friday]");
            Assert.IsTrue(new DateTime(1999, 4, 5).IsEuronextHoliday(), "Monday 5 April 1999 [Easter Monday]");
            Assert.IsTrue(new DateTime(1999, 5, 24).IsEuronextHoliday(), "Monday 24 May 1999 [Whit Monday]");
        }

        [TestMethod]
        public void EuronextHoliday_IsEuronextHoliday_NewYearDay_IsAlwaysHoliday()
        {
            for (int i = 1980; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1);
                Assert.IsTrue(date.IsEuronextHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void EuronextHoliday_IsEuronextHoliday_GoodFriday_IsAlwaysHoliday()
        {
            for (int i = 1980; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(-2).Date;
                Assert.IsTrue(date.IsEuronextHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void EuronextHoliday_IsEuronextHoliday_EasterMonday_IsAlwaysHoliday()
        {
            for (int i = 1980; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(1).Date;
                Assert.IsTrue(date.IsEuronextHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void EuronextHoliday_IsEuronextHoliday_ChristmasDay_IsAlwaysHoliday()
        {
            for (int i = 1980; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 25);
                Assert.IsTrue(date.IsEuronextHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void EuronextHoliday_IsEuronextHoliday_BoxingDay_IsAlwaysHoliday()
        {
            for (int i = 1980; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 26);
                Assert.IsTrue(date.IsEuronextHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void EuronextHoliday_IsEuronextWorkday_GivenADay_ReturnsTheInverseOfEuronextHoliday()
        {
            DateTime firstDate = new DateTime(1999, 1, 1);
            for (int i = 0; i < 365 * 3; ++i)
            {
                DateTime date = firstDate.AddDays(i);
                Assert.AreNotEqual(date.IsEuronextHoliday(), date.IsEuronextWorkday(), date.ToLongDateString());
            }
        }
    }
}
