using System;
using Mbs.Trading.Holidays;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Holidays
{
    [TestClass]
    public class SwedishHolidayTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_ExplicitlyKnownDays_AreAlwaysHolidays()
        {
             Assert.IsTrue(new DateTime(1993, 1, 1).IsSwedishHoliday(), "1993 January 01, Fri [New Years Day]");
             Assert.IsTrue(new DateTime(1993, 1, 6).IsSwedishHoliday(), "1993 January 06, Wed [Epiphany Day]");
             Assert.IsTrue(new DateTime(1993, 4, 9).IsSwedishHoliday(), "1993 April 09, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1993, 4, 12).IsSwedishHoliday(), "1993 April 12, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(1993, 5, 20).IsSwedishHoliday(), "1993 May 20, Thu [Ascension]");
             Assert.IsTrue(new DateTime(1993, 5, 31).IsSwedishHoliday(), "1993 May 31, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(1993, 6, 25).IsSwedishHoliday(), "1993 June 25, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(1993, 12, 24).IsSwedishHoliday(), "1993 December 24, Fri [Christmas Eve]");
             Assert.IsTrue(new DateTime(1993, 12, 31).IsSwedishHoliday(), "1993 December 31, Fri [New Years Eve]");

             Assert.IsTrue(new DateTime(1994, 1, 6).IsSwedishHoliday(), "1994 January 06, Thu [Epiphany Day]");
             Assert.IsTrue(new DateTime(1994, 4, 1).IsSwedishHoliday(), "1994 April 01, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1994, 4, 4).IsSwedishHoliday(), "1994 April 04, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(1994, 5, 12).IsSwedishHoliday(), "1994 May 12, Thu [Ascension]");
             Assert.IsTrue(new DateTime(1994, 5, 23).IsSwedishHoliday(), "1994 May 23, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(1994, 6, 24).IsSwedishHoliday(), "1994 June 24, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(1994, 12, 26).IsSwedishHoliday(), "1994 December 26, Mon [Boxing Day]");

             Assert.IsTrue(new DateTime(1995, 1, 6).IsSwedishHoliday(), "1995 January 06, Fri [Epiphany Day]");
             Assert.IsTrue(new DateTime(1995, 4, 13).IsSwedishHoliday(), "1995 April 13, Thu [Added Manually]");
             Assert.IsTrue(new DateTime(1995, 4, 14).IsSwedishHoliday(), "1995 April 14, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1995, 4, 17).IsSwedishHoliday(), "1995 April 17, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(1995, 5, 1).IsSwedishHoliday(), "1995 May 01, Mon [Labour Day]");
             Assert.IsTrue(new DateTime(1995, 5, 25).IsSwedishHoliday(), "1995 May 25, Thu [Ascension]");
             Assert.IsTrue(new DateTime(1995, 6, 5).IsSwedishHoliday(), "1995 June 05, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(1995, 6, 23).IsSwedishHoliday(), "1995 June 23, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(1995, 12, 25).IsSwedishHoliday(), "1995 December 25, Mon [Christmas Day]");
             Assert.IsTrue(new DateTime(1995, 12, 26).IsSwedishHoliday(), "1995 December 26, Tue [Boxing Day]");

             Assert.IsTrue(new DateTime(1996, 1, 1).IsSwedishHoliday(), "1996 January 01, Mon [New Years Day]");
             Assert.IsTrue(new DateTime(1996, 4, 5).IsSwedishHoliday(), "1996 April 05, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1996, 4, 8).IsSwedishHoliday(), "1996 April 08, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(1996, 5, 1).IsSwedishHoliday(), "1996 May 01, Wed [Labour Day]");
             Assert.IsTrue(new DateTime(1996, 5, 16).IsSwedishHoliday(), "1996 May 16, Thu [Ascension]");
             Assert.IsTrue(new DateTime(1996, 5, 27).IsSwedishHoliday(), "1996 May 27, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(1996, 6, 21).IsSwedishHoliday(), "1996 June 21, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(1996, 12, 24).IsSwedishHoliday(), "1996 December 24, Tue [Christmas Eve]");
             Assert.IsTrue(new DateTime(1996, 12, 25).IsSwedishHoliday(), "1996 December 25, Wed [Christmas Day]");
             Assert.IsTrue(new DateTime(1996, 12, 26).IsSwedishHoliday(), "1996 December 26, Thu [Boxing Day]");
             Assert.IsTrue(new DateTime(1996, 12, 31).IsSwedishHoliday(), "1996 December 31, Tue [New Years Eve]");

             Assert.IsTrue(new DateTime(1997, 1, 1).IsSwedishHoliday(), "1997 January 01, Wed [New Years Day]");
             Assert.IsTrue(new DateTime(1997, 1, 6).IsSwedishHoliday(), "1997 January 06, Mon [Epiphany Day]");
             Assert.IsTrue(new DateTime(1997, 3, 28).IsSwedishHoliday(), "1997 March 28, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1997, 3, 31).IsSwedishHoliday(), "1997 March 31, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(1997, 5, 1).IsSwedishHoliday(), "1997 May 01, Thu [Labour Day]");
             Assert.IsTrue(new DateTime(1997, 5, 8).IsSwedishHoliday(), "1997 May 08, Thu [Ascension]");
             Assert.IsTrue(new DateTime(1997, 5, 19).IsSwedishHoliday(), "1997 May 19, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(1997, 6, 20).IsSwedishHoliday(), "1997 June 20, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(1997, 12, 24).IsSwedishHoliday(), "1997 December 24, Wed [Christmas Eve]");
             Assert.IsTrue(new DateTime(1997, 12, 25).IsSwedishHoliday(), "1997 December 25, Thu [Christmas Day]");
             Assert.IsTrue(new DateTime(1997, 12, 26).IsSwedishHoliday(), "1997 December 26, Fri [Boxing Day]");
             Assert.IsTrue(new DateTime(1997, 12, 31).IsSwedishHoliday(), "1997 December 31, Wed [New Years Eve]");

             Assert.IsTrue(new DateTime(1998, 1, 1).IsSwedishHoliday(), "1998 January 01, Thu [New Years Day]");
             Assert.IsTrue(new DateTime(1998, 1, 6).IsSwedishHoliday(), "1998 January 06, Tue [Epiphany Day]");
             Assert.IsTrue(new DateTime(1998, 4, 10).IsSwedishHoliday(), "1998 April 10, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1998, 4, 13).IsSwedishHoliday(), "1998 April 13, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(1998, 5, 1).IsSwedishHoliday(), "1998 May 01, Fri [Labour Day]");
             Assert.IsTrue(new DateTime(1998, 5, 21).IsSwedishHoliday(), "1998 May 21, Thu [Ascension]");
             Assert.IsTrue(new DateTime(1998, 6, 1).IsSwedishHoliday(), "1998 June 01, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(1998, 6, 19).IsSwedishHoliday(), "1998 June 19, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(1998, 12, 24).IsSwedishHoliday(), "1998 December 24, Thu [Christmas Eve]");
             Assert.IsTrue(new DateTime(1998, 12, 25).IsSwedishHoliday(), "1998 December 25, Fri [Christmas Day]");
             Assert.IsTrue(new DateTime(1998, 12, 31).IsSwedishHoliday(), "1998 December 31, Thu [New Years Eve]");

             Assert.IsTrue(new DateTime(1999, 1, 1).IsSwedishHoliday(), "1999 January 01, Fri [New Years Day]");
             Assert.IsTrue(new DateTime(1999, 1, 6).IsSwedishHoliday(), "1999 January 06, Wed [Epiphany Day]");
             Assert.IsTrue(new DateTime(1999, 4, 2).IsSwedishHoliday(), "1999 April 02, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1999, 4, 5).IsSwedishHoliday(), "1999 April 05, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(1999, 5, 13).IsSwedishHoliday(), "1999 May 13, Thu [Ascension]");
             Assert.IsTrue(new DateTime(1999, 5, 24).IsSwedishHoliday(), "1999 May 24, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(1999, 6, 25).IsSwedishHoliday(), "1999 June 25, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(1999, 12, 24).IsSwedishHoliday(), "1999 December 24, Fri [Christmas Eve]");
             Assert.IsTrue(new DateTime(1999, 12, 31).IsSwedishHoliday(), "1999 December 31, Fri [New Years Eve]");

             Assert.IsTrue(new DateTime(2000, 1, 6).IsSwedishHoliday(), "2000 January 06, Thu [Epiphany Day]");
             Assert.IsTrue(new DateTime(2000, 4, 21).IsSwedishHoliday(), "2000 April 21, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2000, 4, 24).IsSwedishHoliday(), "2000 April 24, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2000, 5, 1).IsSwedishHoliday(), "2000 May 01, Mon [Labour Day]");
             Assert.IsTrue(new DateTime(2000, 6, 1).IsSwedishHoliday(), "2000 June 01, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2000, 6, 12).IsSwedishHoliday(), "2000 June 12, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(2000, 6, 23).IsSwedishHoliday(), "2000 June 23, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2000, 12, 25).IsSwedishHoliday(), "2000 December 25, Mon [Christmas Day]");
             Assert.IsTrue(new DateTime(2000, 12, 26).IsSwedishHoliday(), "2000 December 26, Tue [New Years Eve]");

             Assert.IsTrue(new DateTime(2001, 1, 1).IsSwedishHoliday(), "2001 January 01, Mon [New Years Day]");
             Assert.IsTrue(new DateTime(2001, 4, 13).IsSwedishHoliday(), "2001 April 13, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2001, 4, 16).IsSwedishHoliday(), "2001 April 16, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2001, 5, 1).IsSwedishHoliday(), "2001 May 01, Tue [Labour Day]");
             Assert.IsTrue(new DateTime(2001, 5, 24).IsSwedishHoliday(), "2001 May 24, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2001, 6, 4).IsSwedishHoliday(), "2001 June 04, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(2001, 6, 22).IsSwedishHoliday(), "2001 June 22, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2001, 12, 24).IsSwedishHoliday(), "2001 December 24, Mon [Christmas Eve]");
             Assert.IsTrue(new DateTime(2001, 12, 25).IsSwedishHoliday(), "2001 December 25, Tue [Christmas Day]");
             Assert.IsTrue(new DateTime(2001, 12, 26).IsSwedishHoliday(), "2001 December 26, Wed [Boxing Day]");
             Assert.IsTrue(new DateTime(2001, 12, 31).IsSwedishHoliday(), "2001 December 31, Mon [New Years Eve]");

             Assert.IsTrue(new DateTime(2002, 1, 1).IsSwedishHoliday(), "2002 January 01, Tue [New Years Day]");
             Assert.IsTrue(new DateTime(2002, 3, 29).IsSwedishHoliday(), "2002 March 29, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2002, 4, 1).IsSwedishHoliday(), "2002 April 01, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2002, 5, 1).IsSwedishHoliday(), "2002 May 01, Wed [Labour Day]");
             Assert.IsTrue(new DateTime(2002, 5, 9).IsSwedishHoliday(), "2002 May 09, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2002, 5, 20).IsSwedishHoliday(), "2002 May 20, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(2002, 6, 21).IsSwedishHoliday(), "2002 June 21, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2002, 12, 24).IsSwedishHoliday(), "2002 December 24, Tue [Christmas Eve]");
             Assert.IsTrue(new DateTime(2002, 12, 25).IsSwedishHoliday(), "2002 December 25, Wed [Christmas Day]");
             Assert.IsTrue(new DateTime(2002, 12, 26).IsSwedishHoliday(), "2002 December 26, Thu [Boxing Day]");
             Assert.IsTrue(new DateTime(2002, 12, 31).IsSwedishHoliday(), "2002 December 31, Tue [New Years Eve]");

             Assert.IsTrue(new DateTime(2003, 1, 1).IsSwedishHoliday(), "2003 January 01, Wed [New Years Day]");
             Assert.IsTrue(new DateTime(2003, 1, 6).IsSwedishHoliday(), "2003 January 06, Mon [Epiphany Day]");
             Assert.IsTrue(new DateTime(2003, 4, 18).IsSwedishHoliday(), "2003 April 18, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2003, 4, 21).IsSwedishHoliday(), "2003 April 21, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2003, 5, 1).IsSwedishHoliday(), "2003 May 01, Thu [Labour Day]");
             Assert.IsTrue(new DateTime(2003, 5, 29).IsSwedishHoliday(), "2003 May 29, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2003, 6, 9).IsSwedishHoliday(), "2003 June 09, Mon [National Day]");
             Assert.IsTrue(new DateTime(2003, 6, 20).IsSwedishHoliday(), "2003 June 20, Fri [Midsummer Day]");
             Assert.IsTrue(new DateTime(2003, 12, 24).IsSwedishHoliday(), "2003 December 24, Wed [Christmas Eve]");
             Assert.IsTrue(new DateTime(2003, 12, 25).IsSwedishHoliday(), "2003 December 25, Thu [Christmas Day]");
             Assert.IsTrue(new DateTime(2003, 12, 26).IsSwedishHoliday(), "2003 December 26, Fri [Boxing Day]");
             Assert.IsTrue(new DateTime(2003, 12, 31).IsSwedishHoliday(), "2003 December 31, Wed [New Years Eve]");

             Assert.IsTrue(new DateTime(2004, 1, 1).IsSwedishHoliday(), "2004 January 01, Thu [New Years Day]");
             Assert.IsTrue(new DateTime(2004, 1, 6).IsSwedishHoliday(), "2004 January 06, Tue [Epiphany Day]");
             Assert.IsTrue(new DateTime(2004, 4, 9).IsSwedishHoliday(), "2004 April 09, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2004, 4, 12).IsSwedishHoliday(), "2004 April 12, Mon [Easter Holiday]");
             Assert.IsTrue(new DateTime(2004, 5, 20).IsSwedishHoliday(), "2004 May 20, Thu [Ascension Day]");
             Assert.IsTrue(new DateTime(2004, 5, 31).IsSwedishHoliday(), "2004 May 31, Mon [Whit Monday]");
             Assert.IsTrue(new DateTime(2004, 6, 25).IsSwedishHoliday(), "2004 June 25, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2004, 12, 24).IsSwedishHoliday(), "2004 December 24, Fri [Christmas Eve]");
             Assert.IsTrue(new DateTime(2004, 12, 31).IsSwedishHoliday(), "2004 December 31, Fri [New Years Eve]");

             Assert.IsTrue(new DateTime(2005, 1, 6).IsSwedishHoliday(), "2005 January 06, Thu [Epiphany Day]");
             Assert.IsTrue(new DateTime(2005, 3, 25).IsSwedishHoliday(), "2005 March 25, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2005, 3, 28).IsSwedishHoliday(), "2005 March 28, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2005, 5, 5).IsSwedishHoliday(), "2005 May 05, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2005, 6, 6).IsSwedishHoliday(), "2005 June 06, Mon [National Day]");
             Assert.IsTrue(new DateTime(2005, 6, 24).IsSwedishHoliday(), "2005 June 24, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2005, 12, 26).IsSwedishHoliday(), "2005 December 26, Mon [Boxing Day]");

             Assert.IsTrue(new DateTime(2006, 1, 6).IsSwedishHoliday(), "2006 January 06, Fri [Epiphany Day]");
             Assert.IsTrue(new DateTime(2006, 4, 14).IsSwedishHoliday(), "2006 April 14, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2006, 4, 17).IsSwedishHoliday(), "2006 April 17, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2006, 5, 1).IsSwedishHoliday(), "2006 May 01, Mon [Labour Day]");
             Assert.IsTrue(new DateTime(2006, 5, 25).IsSwedishHoliday(), "2006 May 25, Thu [Ascension Day]");
             Assert.IsTrue(new DateTime(2006, 6, 6).IsSwedishHoliday(), "2006 June 06, Tue [National Day]");
             Assert.IsTrue(new DateTime(2006, 6, 23).IsSwedishHoliday(), "2006 June 23, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2006, 12, 25).IsSwedishHoliday(), "2006 December 25, Mon [Christmas Day]");
             Assert.IsTrue(new DateTime(2006, 12, 26).IsSwedishHoliday(), "2006 December 26, Tue [Boxing Day]");

             Assert.IsTrue(new DateTime(2007, 1, 1).IsSwedishHoliday(), "2007 January 01, Mon [New Years Day]");
             Assert.IsTrue(new DateTime(2007, 4, 6).IsSwedishHoliday(), "2007 April 06, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2007, 4, 9).IsSwedishHoliday(), "2007 April 09, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2007, 5, 1).IsSwedishHoliday(), "2007 May 01, Tue [Labour Day]");
             Assert.IsTrue(new DateTime(2007, 5, 17).IsSwedishHoliday(), "2007 May 17, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2007, 6, 6).IsSwedishHoliday(), "2007 June 06, Wed [National Day]");
             Assert.IsTrue(new DateTime(2007, 6, 22).IsSwedishHoliday(), "2007 June 22, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2007, 12, 24).IsSwedishHoliday(), "2007 December 24, Mon [Christmas Eve]");
             Assert.IsTrue(new DateTime(2007, 12, 25).IsSwedishHoliday(), "2007 December 25, Tue [Christmas]");
             Assert.IsTrue(new DateTime(2007, 12, 26).IsSwedishHoliday(), "2007 December 26, Wed [Boxing Day]");
             Assert.IsTrue(new DateTime(2007, 12, 31).IsSwedishHoliday(), "2007 December 31, Mon [New Years Eve]");

             Assert.IsTrue(new DateTime(2008, 1, 1).IsSwedishHoliday(), "2008 January 01, Tue [New Years Day]");
             Assert.IsTrue(new DateTime(2008, 3, 21).IsSwedishHoliday(), "2008 March 21, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2008, 3, 24).IsSwedishHoliday(), "2008 March 24, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2008, 5, 1).IsSwedishHoliday(), "2008 May 01, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2008, 6, 6).IsSwedishHoliday(), "2008 June 06, Fri [National Day]");
             Assert.IsTrue(new DateTime(2008, 6, 20).IsSwedishHoliday(), "2008 June 20, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2008, 12, 24).IsSwedishHoliday(), "2008 December 24, Wed [Christmas Eve]");
             Assert.IsTrue(new DateTime(2008, 12, 25).IsSwedishHoliday(), "2008 December 25, Thu [Christmas]");
             Assert.IsTrue(new DateTime(2008, 12, 26).IsSwedishHoliday(), "2008 December 26, Fri [Boxing Day]");
             Assert.IsTrue(new DateTime(2008, 12, 31).IsSwedishHoliday(), "2008 December 31, Wed [New Years Eve]");

             Assert.IsTrue(new DateTime(2009, 1, 1).IsSwedishHoliday(), "2009 January 01, Thu [New Years Day]");
             Assert.IsTrue(new DateTime(2009, 1, 6).IsSwedishHoliday(), "2009 January 06, Tue [Epiphany Day]");
             Assert.IsTrue(new DateTime(2009, 4, 10).IsSwedishHoliday(), "2009 April 10, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2009, 4, 13).IsSwedishHoliday(), "2009 April 13, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2009, 5, 1).IsSwedishHoliday(), "2009 May 01, Fri [Labour Day]");
             Assert.IsTrue(new DateTime(2009, 5, 21).IsSwedishHoliday(), "2009 May 21, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2009, 6, 19).IsSwedishHoliday(), "2009 June 19, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2009, 12, 24).IsSwedishHoliday(), "2009 December 24, Thu [Christmas Eve]");
             Assert.IsTrue(new DateTime(2009, 12, 25).IsSwedishHoliday(), "2009 December 25, Fri [Christmas]");
             Assert.IsTrue(new DateTime(2009, 12, 31).IsSwedishHoliday(), "2009 December 31, Thu [New Years Eve]");

             Assert.IsTrue(new DateTime(2010, 1, 1).IsSwedishHoliday(), "2010 January 01, Fri [New Years Day]");
             Assert.IsTrue(new DateTime(2010, 1, 6).IsSwedishHoliday(), "2010 January 06, Wed [Epiphany Day]");
             Assert.IsTrue(new DateTime(2010, 4, 2).IsSwedishHoliday(), "2010 April 02, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2010, 4, 5).IsSwedishHoliday(), "2010 April 05, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2010, 5, 13).IsSwedishHoliday(), "2010 May 13, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2010, 6, 25).IsSwedishHoliday(), "2010 June 25, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2010, 12, 24).IsSwedishHoliday(), "2010 December 24, Fri [Christmas]");
             Assert.IsTrue(new DateTime(2010, 12, 31).IsSwedishHoliday(), "2010 December 31, Fri [New Years Eve]");

             Assert.IsTrue(new DateTime(2011, 1, 6).IsSwedishHoliday(), "2011 January 06, Thu [Epiphany Day]");
             Assert.IsTrue(new DateTime(2011, 4, 22).IsSwedishHoliday(), "2011 April 22, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2011, 4, 25).IsSwedishHoliday(), "2011 April 25, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2011, 6, 2).IsSwedishHoliday(), "2011 June 02, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2011, 6, 6).IsSwedishHoliday(), "2011 June 06, Mon [National Day]");
             Assert.IsTrue(new DateTime(2011, 6, 24).IsSwedishHoliday(), "2011 June 24, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2011, 12, 26).IsSwedishHoliday(), "2011 December 26, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2012, 1, 6).IsSwedishHoliday(), "2012 January 06, Fri [Epiphany Day]");
             Assert.IsTrue(new DateTime(2012, 4, 6).IsSwedishHoliday(), "2012 April 06, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2012, 4, 9).IsSwedishHoliday(), "2012 April 09, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2012, 5, 1).IsSwedishHoliday(), "2012 May 01, Tue [Labour Day]");
             Assert.IsTrue(new DateTime(2012, 5, 17).IsSwedishHoliday(), "2012 May 17, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2012, 6, 6).IsSwedishHoliday(), "2012 June 06, Wed [National Day]");
             Assert.IsTrue(new DateTime(2012, 6, 22).IsSwedishHoliday(), "2012 June 22, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2012, 12, 24).IsSwedishHoliday(), "2012 December 24, Mon [Christmas Eve]");
             Assert.IsTrue(new DateTime(2012, 12, 25).IsSwedishHoliday(), "2012 December 25, Tue [Christmas]");
             Assert.IsTrue(new DateTime(2012, 12, 26).IsSwedishHoliday(), "2012 December 26, Wed [Boxing Day]");
             Assert.IsTrue(new DateTime(2012, 12, 31).IsSwedishHoliday(), "2012 December 31, Mon [New Years Eve]");

             Assert.IsTrue(new DateTime(2013, 1, 1).IsSwedishHoliday(), "2013 January 01, Tue [New Years Day]");
             Assert.IsTrue(new DateTime(2013, 3, 29).IsSwedishHoliday(), "2013 March 29, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2013, 4, 1).IsSwedishHoliday(), "2013 April 01, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2013, 5, 1).IsSwedishHoliday(), "2013 May 01, Wed [Labour Day]");
             Assert.IsTrue(new DateTime(2013, 5, 9).IsSwedishHoliday(), "2013 May 09, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2013, 6, 6).IsSwedishHoliday(), "2013 June 06, Thu [National Day]");
             Assert.IsTrue(new DateTime(2013, 6, 21).IsSwedishHoliday(), "2013 June 21, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2013, 12, 24).IsSwedishHoliday(), "2013 December 24, Tue [Christmas Eve]");
             Assert.IsTrue(new DateTime(2013, 12, 25).IsSwedishHoliday(), "2013 December 25, Wed [Christmas]");
             Assert.IsTrue(new DateTime(2013, 12, 26).IsSwedishHoliday(), "2013 December 26, Thu [Boxing Day]");
             Assert.IsTrue(new DateTime(2013, 12, 31).IsSwedishHoliday(), "2013 December 31, Tue [New Years Eve]");

             Assert.IsTrue(new DateTime(2014, 1, 1).IsSwedishHoliday(), "2014 January 01, Wed [New Years Day]");
             Assert.IsTrue(new DateTime(2014, 1, 6).IsSwedishHoliday(), "2014 January 06, Mon [Epiphany Day]");
             Assert.IsTrue(new DateTime(2014, 4, 18).IsSwedishHoliday(), "2014 April 18, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2014, 4, 21).IsSwedishHoliday(), "2014 April 21, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2014, 5, 1).IsSwedishHoliday(), "2014 May 01, Thu [Labour Day]");
             Assert.IsTrue(new DateTime(2014, 5, 29).IsSwedishHoliday(), "2014 May 29, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2014, 6, 6).IsSwedishHoliday(), "2014 June 06, Fri [National Day]");
             Assert.IsTrue(new DateTime(2014, 6, 20).IsSwedishHoliday(), "2014 June 20, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2014, 12, 24).IsSwedishHoliday(), "2014 December 24, Wed [Christmas Eve]");
             Assert.IsTrue(new DateTime(2014, 12, 25).IsSwedishHoliday(), "2014 December 25, Thu [Christmas]");
             Assert.IsTrue(new DateTime(2014, 12, 26).IsSwedishHoliday(), "2014 December 26, Fri [Boxing Day]");
             Assert.IsTrue(new DateTime(2014, 12, 31).IsSwedishHoliday(), "2014 December 31, Wed [New Years Eve]");

             Assert.IsTrue(new DateTime(2015, 1, 1).IsSwedishHoliday(), "2015 January 01, Thu [New Years Day]");
             Assert.IsTrue(new DateTime(2015, 1, 6).IsSwedishHoliday(), "2015 January 06, Tue [Epiphany Day]");
             Assert.IsTrue(new DateTime(2015, 4, 3).IsSwedishHoliday(), "2015 April 03, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2015, 4, 6).IsSwedishHoliday(), "2015 April 06, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2015, 5, 1).IsSwedishHoliday(), "2015 May 01, Fri [Labour Day]");
             Assert.IsTrue(new DateTime(2015, 5, 14).IsSwedishHoliday(), "2015 May 14, Thu [Ascension]");
             Assert.IsTrue(new DateTime(2015, 6, 19).IsSwedishHoliday(), "2015 June 19, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2015, 12, 24).IsSwedishHoliday(), "2015 December 24, Thu [Christmas Eve]");
             Assert.IsTrue(new DateTime(2015, 12, 25).IsSwedishHoliday(), "2015 December 25, Fri [Christmas]");
             Assert.IsTrue(new DateTime(2015, 12, 31).IsSwedishHoliday(), "2015 December 31, Thu [New Years Eve]");

             Assert.IsTrue(new DateTime(2016, 1, 1).IsSwedishHoliday(), "2016 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2016, 1, 6).IsSwedishHoliday(), "2016 January 06, Wed [Epiphany Day]");
             Assert.IsTrue(new DateTime(2016, 3, 25).IsSwedishHoliday(), "2016 March 25, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2016, 3, 28).IsSwedishHoliday(), "2016 March 28, Mon [Easter Monday]");
             Assert.IsTrue(new DateTime(2016, 5, 5).IsSwedishHoliday(), "2016 May 05, Thu [Ascension Day]");
             Assert.IsTrue(new DateTime(2016, 6, 6).IsSwedishHoliday(), "2016 June 06, Mon [National Day]");
             Assert.IsTrue(new DateTime(2016, 6, 24).IsSwedishHoliday(), "2016 June 24, Fri [Midsummer Eve]");
             Assert.IsTrue(new DateTime(2016, 12, 26).IsSwedishHoliday(), "2016 December 26, Mon [Boxing Day]");
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_EpiphanyDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 6).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_GoodFriday_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(-2).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_EasterMonday_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(1).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_LabourDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 5, 1).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_AscensionDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(39).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_NationalDay_NotHolidayBefore2003()
        {
            for (int i = 1900; i < 2003; ++i)
            {
                if (i == 1927 || i == 1938 || i == 1949 || i == 1960)
                {
                    // Coincides with Whit Monday.
                    continue;
                }

                DateTime date = new DateTime(i, 6, 6).Date;
                DayOfWeek dow = date.DayOfWeek;
                if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
                {
                    continue;
                }

                Assert.IsFalse(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_NationalDay_IsAlwaysHolidayAfter2003()
        {
            for (int i = 2003; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 6, 6).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_WhitMonday_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(50).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_MidsummerEve_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2003; ++i)
            {
                for (int j = 19; j <= 25; ++j)
                {
                    DateTime date = new DateTime(i, 6, j).Date;
                    if (date.DayOfWeek == DayOfWeek.Friday)
                    {
                        Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_ChristmasEve_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 24).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_ChristmasDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 25).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_BoxingDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 26).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_NewYearsEve_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 31).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishHoliday_NewYearsDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).Date;
                Assert.IsTrue(date.IsSwedishHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void SwedishHoliday_IsSwedishWorkday_GivenADay_ReturnsTheInverseOfSwedishHoliday()
        {
            DateTime firstDate = new DateTime(1999, 1, 1);
            for (int i = 0; i < 365 * 3; ++i)
            {
                DateTime date = firstDate.AddDays(i);
                Assert.AreNotEqual(date.IsSwedishHoliday(), date.IsSwedishWorkday(), date.ToLongDateString());
            }
        }
    }
}
