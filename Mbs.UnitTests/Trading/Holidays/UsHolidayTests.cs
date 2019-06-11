using System;
using Mbs.Trading.Holidays;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Holidays
{
    [TestClass]
    public class UsHolidayTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void UsHoliday_IsUsHoliday_ExplicitlyKnownDays_AreAlwaysHolidays()
        {
             Assert.IsTrue(new DateTime(1989, 1, 2).IsUsHoliday(), "1989 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(1989, 2, 20).IsUsHoliday(), "1989 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1989, 3, 24).IsUsHoliday(), "1989 March 24, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1989, 5, 29).IsUsHoliday(), "1989 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1989, 7, 4).IsUsHoliday(), "1989 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(1989, 9, 4).IsUsHoliday(), "1989 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1989, 11, 23).IsUsHoliday(), "1989 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1989, 12, 25).IsUsHoliday(), "1989 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(1990, 1, 1).IsUsHoliday(), "1990 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(1990, 2, 19).IsUsHoliday(), "1990 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1990, 4, 13).IsUsHoliday(), "1990 April 13, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1990, 5, 28).IsUsHoliday(), "1990 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1990, 7, 4).IsUsHoliday(), "1990 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(1990, 9, 3).IsUsHoliday(), "1990 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1990, 11, 22).IsUsHoliday(), "1990 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1990, 12, 25).IsUsHoliday(), "1990 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(1991, 1, 1).IsUsHoliday(), "1991 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(1991, 2, 18).IsUsHoliday(), "1991 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1991, 3, 29).IsUsHoliday(), "1991 March 29, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1991, 5, 27).IsUsHoliday(), "1991 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1991, 7, 4).IsUsHoliday(), "1991 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(1991, 9, 2).IsUsHoliday(), "1991 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1991, 11, 28).IsUsHoliday(), "1991 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1991, 12, 25).IsUsHoliday(), "1991 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(1992, 1, 1).IsUsHoliday(), "1992 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(1992, 2, 17).IsUsHoliday(), "1992 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1992, 4, 17).IsUsHoliday(), "1992 April 17, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1992, 5, 25).IsUsHoliday(), "1992 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1992, 7, 3).IsUsHoliday(), "1992 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(1992, 9, 7).IsUsHoliday(), "1992 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1992, 11, 26).IsUsHoliday(), "1992 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1992, 12, 25).IsUsHoliday(), "1992 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(1993, 1, 1).IsUsHoliday(), "1993 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(1993, 2, 15).IsUsHoliday(), "1993 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1993, 4, 9).IsUsHoliday(), "1993 April 09, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1993, 5, 31).IsUsHoliday(), "1993 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1993, 7, 5).IsUsHoliday(), "1993 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(1993, 9, 6).IsUsHoliday(), "1993 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1993, 11, 25).IsUsHoliday(), "1993 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1993, 12, 24).IsUsHoliday(), "1993 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(1994, 2, 21).IsUsHoliday(), "1994 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1994, 4, 1).IsUsHoliday(), "1994 April 01, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1994, 4, 27).IsUsHoliday(), "1994 April 27, Wed [Funeral of President Richard M. Nixon]");
             Assert.IsTrue(new DateTime(1994, 5, 30).IsUsHoliday(), "1994 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1994, 7, 4).IsUsHoliday(), "1994 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(1994, 9, 5).IsUsHoliday(), "1994 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1994, 11, 24).IsUsHoliday(), "1994 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1994, 12, 26).IsUsHoliday(), "1994 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(1995, 1, 2).IsUsHoliday(), "1995 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(1995, 2, 20).IsUsHoliday(), "1995 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1995, 4, 14).IsUsHoliday(), "1995 April 14, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1995, 5, 29).IsUsHoliday(), "1995 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1995, 7, 4).IsUsHoliday(), "1995 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(1995, 9, 4).IsUsHoliday(), "1995 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1995, 11, 23).IsUsHoliday(), "1995 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1995, 12, 25).IsUsHoliday(), "1995 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(1996, 1, 1).IsUsHoliday(), "1996 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(1996, 2, 19).IsUsHoliday(), "1996 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1996, 4, 5).IsUsHoliday(), "1996 April 05, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1996, 5, 27).IsUsHoliday(), "1996 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1996, 7, 4).IsUsHoliday(), "1996 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(1996, 9, 2).IsUsHoliday(), "1996 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1996, 11, 28).IsUsHoliday(), "1996 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1996, 12, 25).IsUsHoliday(), "1996 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(1997, 1, 1).IsUsHoliday(), "1997 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(1997, 2, 17).IsUsHoliday(), "1997 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1997, 3, 28).IsUsHoliday(), "1997 March 28, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1997, 5, 26).IsUsHoliday(), "1997 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1997, 7, 4).IsUsHoliday(), "1997 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(1997, 9, 1).IsUsHoliday(), "1997 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1997, 11, 27).IsUsHoliday(), "1997 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1997, 12, 25).IsUsHoliday(), "1997 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(1998, 1, 1).IsUsHoliday(), "1998 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(1998, 1, 19).IsUsHoliday(), "1998 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(1998, 2, 16).IsUsHoliday(), "1998 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1998, 4, 10).IsUsHoliday(), "1998 April 10, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1998, 5, 25).IsUsHoliday(), "1998 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1998, 7, 3).IsUsHoliday(), "1998 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(1998, 9, 7).IsUsHoliday(), "1998 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1998, 11, 26).IsUsHoliday(), "1998 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1998, 12, 25).IsUsHoliday(), "1998 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(1999, 1, 1).IsUsHoliday(), "1999 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(1999, 1, 18).IsUsHoliday(), "1999 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(1999, 2, 15).IsUsHoliday(), "1999 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(1999, 4, 2).IsUsHoliday(), "1999 April 02, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(1999, 5, 31).IsUsHoliday(), "1999 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(1999, 7, 5).IsUsHoliday(), "1999 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(1999, 9, 6).IsUsHoliday(), "1999 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(1999, 11, 25).IsUsHoliday(), "1999 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(1999, 12, 24).IsUsHoliday(), "1999 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2000, 1, 17).IsUsHoliday(), "2000 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2000, 2, 21).IsUsHoliday(), "2000 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2000, 4, 21).IsUsHoliday(), "2000 April 21, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2000, 5, 29).IsUsHoliday(), "2000 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2000, 7, 4).IsUsHoliday(), "2000 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2000, 9, 4).IsUsHoliday(), "2000 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2000, 11, 23).IsUsHoliday(), "2000 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2000, 12, 25).IsUsHoliday(), "2000 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2001, 1, 1).IsUsHoliday(), "2001 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2001, 1, 15).IsUsHoliday(), "2001 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2001, 2, 19).IsUsHoliday(), "2001 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2001, 4, 13).IsUsHoliday(), "2001 April 13, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2001, 5, 28).IsUsHoliday(), "2001 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2001, 7, 4).IsUsHoliday(), "2001 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2001, 9, 3).IsUsHoliday(), "2001 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2001, 9, 11).IsUsHoliday(), "2001 September 11, Tue [Terrorism]");
             Assert.IsTrue(new DateTime(2001, 9, 12).IsUsHoliday(), "2001 September 12, Wed [Terrorism]");
             Assert.IsTrue(new DateTime(2001, 9, 13).IsUsHoliday(), "2001 September 13, Thu [Terrorism]");
             Assert.IsTrue(new DateTime(2001, 9, 14).IsUsHoliday(), "2001 September 14, Fri [Terrorism]");
             Assert.IsTrue(new DateTime(2001, 11, 22).IsUsHoliday(), "2001 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2001, 12, 25).IsUsHoliday(), "2001 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2002, 1, 1).IsUsHoliday(), "2002 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2002, 1, 21).IsUsHoliday(), "2002 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2002, 2, 18).IsUsHoliday(), "2002 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2002, 3, 29).IsUsHoliday(), "2002 March 29, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2002, 5, 27).IsUsHoliday(), "2002 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2002, 7, 4).IsUsHoliday(), "2002 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2002, 9, 2).IsUsHoliday(), "2002 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2002, 11, 28).IsUsHoliday(), "2002 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2002, 12, 25).IsUsHoliday(), "2002 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2003, 1, 1).IsUsHoliday(), "2003 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2003, 1, 20).IsUsHoliday(), "2003 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2003, 2, 17).IsUsHoliday(), "2003 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2003, 4, 18).IsUsHoliday(), "2003 April 18, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2003, 5, 26).IsUsHoliday(), "2003 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2003, 7, 4).IsUsHoliday(), "2003 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2003, 9, 1).IsUsHoliday(), "2003 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2003, 11, 27).IsUsHoliday(), "2003 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2003, 12, 25).IsUsHoliday(), "2003 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2004, 1, 1).IsUsHoliday(), "2004 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2004, 1, 19).IsUsHoliday(), "2004 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2004, 2, 16).IsUsHoliday(), "2004 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2004, 4, 9).IsUsHoliday(), "2004 April 09, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2004, 5, 31).IsUsHoliday(), "2004 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2004, 6, 11).IsUsHoliday(), "2004 June 11, Fri [National Day of Mourning for President Ronald W. Reagan]");
             Assert.IsTrue(new DateTime(2004, 7, 5).IsUsHoliday(), "2004 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2004, 9, 6).IsUsHoliday(), "2004 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2004, 11, 25).IsUsHoliday(), "2004 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2004, 12, 24).IsUsHoliday(), "2004 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2005, 1, 17).IsUsHoliday(), "2005 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2005, 2, 21).IsUsHoliday(), "2005 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2005, 3, 25).IsUsHoliday(), "2005 March 25, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2005, 5, 30).IsUsHoliday(), "2005 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2005, 7, 4).IsUsHoliday(), "2005 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2005, 9, 5).IsUsHoliday(), "2005 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2005, 11, 24).IsUsHoliday(), "2005 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2005, 12, 26).IsUsHoliday(), "2005 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2006, 1, 2).IsUsHoliday(), "2006 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2006, 1, 16).IsUsHoliday(), "2006 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2006, 2, 20).IsUsHoliday(), "2006 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2006, 4, 14).IsUsHoliday(), "2006 April 14, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2006, 5, 29).IsUsHoliday(), "2006 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2006, 7, 4).IsUsHoliday(), "2006 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2006, 9, 4).IsUsHoliday(), "2006 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2006, 11, 23).IsUsHoliday(), "2006 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2006, 12, 25).IsUsHoliday(), "2006 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2007, 1, 1).IsUsHoliday(), "2007 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2007, 1, 2).IsUsHoliday(), "2007 January 02, Tue [National Day of Mourning for President Gerald R. Ford]");
             Assert.IsTrue(new DateTime(2007, 1, 15).IsUsHoliday(), "2007 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2007, 2, 19).IsUsHoliday(), "2007 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2007, 4, 6).IsUsHoliday(), "2007 April 06, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2007, 5, 28).IsUsHoliday(), "2007 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2007, 7, 4).IsUsHoliday(), "2007 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2007, 9, 3).IsUsHoliday(), "2007 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2007, 11, 22).IsUsHoliday(), "2007 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2007, 12, 25).IsUsHoliday(), "2007 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2008, 1, 1).IsUsHoliday(), "2008 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2008, 1, 21).IsUsHoliday(), "2008 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2008, 2, 18).IsUsHoliday(), "2008 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2008, 3, 21).IsUsHoliday(), "2008 March 21, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2008, 5, 26).IsUsHoliday(), "2008 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2008, 7, 4).IsUsHoliday(), "2008 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2008, 9, 1).IsUsHoliday(), "2008 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2008, 11, 27).IsUsHoliday(), "2008 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2008, 12, 25).IsUsHoliday(), "2008 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2009, 1, 1).IsUsHoliday(), "2009 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2009, 1, 19).IsUsHoliday(), "2009 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2009, 2, 16).IsUsHoliday(), "2009 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2009, 4, 10).IsUsHoliday(), "2009 April 10, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2009, 5, 25).IsUsHoliday(), "2009 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2009, 7, 3).IsUsHoliday(), "2009 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(2009, 9, 7).IsUsHoliday(), "2009 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2009, 11, 26).IsUsHoliday(), "2009 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2009, 12, 25).IsUsHoliday(), "2009 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(2010, 1, 1).IsUsHoliday(), "2010 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2010, 1, 18).IsUsHoliday(), "2010 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2010, 2, 15).IsUsHoliday(), "2010 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2010, 4, 2).IsUsHoliday(), "2010 April 02, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2010, 5, 31).IsUsHoliday(), "2010 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2010, 7, 5).IsUsHoliday(), "2010 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2010, 9, 6).IsUsHoliday(), "2010 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2010, 11, 25).IsUsHoliday(), "2010 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2010, 12, 24).IsUsHoliday(), "2010 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2011, 1, 17).IsUsHoliday(), "2011 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2011, 2, 21).IsUsHoliday(), "2011 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2011, 4, 22).IsUsHoliday(), "2011 April 22, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2011, 5, 30).IsUsHoliday(), "2011 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2011, 7, 4).IsUsHoliday(), "2011 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2011, 9, 5).IsUsHoliday(), "2011 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2011, 11, 24).IsUsHoliday(), "2011 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2011, 12, 26).IsUsHoliday(), "2011 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2012, 1, 2).IsUsHoliday(), "2012 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2012, 1, 16).IsUsHoliday(), "2012 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2012, 2, 20).IsUsHoliday(), "2012 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2012, 4, 6).IsUsHoliday(), "2012 April 06, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2012, 5, 28).IsUsHoliday(), "2012 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2012, 7, 4).IsUsHoliday(), "2012 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2012, 9, 3).IsUsHoliday(), "2012 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2012, 10, 29).IsUsHoliday(), "2012 October 29, Mon [Hurricane Sandy]");
             Assert.IsTrue(new DateTime(2012, 10, 30).IsUsHoliday(), "2012 October 30, Tue [Hurricane Sandy]");
             Assert.IsTrue(new DateTime(2012, 11, 22).IsUsHoliday(), "2012 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2012, 12, 25).IsUsHoliday(), "2012 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2013, 1, 1).IsUsHoliday(), "2013 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2013, 1, 21).IsUsHoliday(), "2013 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2013, 2, 18).IsUsHoliday(), "2013 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2013, 3, 29).IsUsHoliday(), "2013 March 29, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2013, 5, 27).IsUsHoliday(), "2013 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2013, 7, 4).IsUsHoliday(), "2013 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2013, 9, 2).IsUsHoliday(), "2013 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2013, 11, 28).IsUsHoliday(), "2013 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2013, 12, 25).IsUsHoliday(), "2013 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2014, 1, 1).IsUsHoliday(), "2014 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2014, 1, 20).IsUsHoliday(), "2014 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2014, 2, 17).IsUsHoliday(), "2014 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2014, 4, 18).IsUsHoliday(), "2014 April 18, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2014, 5, 26).IsUsHoliday(), "2014 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2014, 7, 4).IsUsHoliday(), "2014 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2014, 9, 1).IsUsHoliday(), "2014 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2014, 11, 27).IsUsHoliday(), "2014 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2014, 12, 25).IsUsHoliday(), "2014 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2015, 1, 1).IsUsHoliday(), "2015 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2015, 1, 19).IsUsHoliday(), "2015 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2015, 2, 16).IsUsHoliday(), "2015 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2015, 4, 3).IsUsHoliday(), "2015 April 03, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2015, 5, 25).IsUsHoliday(), "2015 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2015, 7, 3).IsUsHoliday(), "2015 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(2015, 9, 7).IsUsHoliday(), "2015 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2015, 11, 26).IsUsHoliday(), "2015 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2015, 12, 25).IsUsHoliday(), "2015 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(2016, 1, 1).IsUsHoliday(), "2016 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2016, 1, 18).IsUsHoliday(), "2016 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2016, 2, 15).IsUsHoliday(), "2016 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2016, 3, 25).IsUsHoliday(), "2016 March 25, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2016, 5, 30).IsUsHoliday(), "2016 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2016, 7, 4).IsUsHoliday(), "2016 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2016, 9, 5).IsUsHoliday(), "2016 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2016, 11, 24).IsUsHoliday(), "2016 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2016, 12, 26).IsUsHoliday(), "2016 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2017, 1, 2).IsUsHoliday(), "2017 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2017, 1, 16).IsUsHoliday(), "2017 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2017, 2, 20).IsUsHoliday(), "2017 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2017, 4, 14).IsUsHoliday(), "2017 April 14, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2017, 5, 29).IsUsHoliday(), "2017 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2017, 7, 4).IsUsHoliday(), "2017 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2017, 9, 4).IsUsHoliday(), "2017 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2017, 11, 23).IsUsHoliday(), "2017 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2017, 12, 25).IsUsHoliday(), "2017 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2018, 1, 1).IsUsHoliday(), "2018 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2018, 1, 15).IsUsHoliday(), "2018 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2018, 2, 19).IsUsHoliday(), "2018 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2018, 3, 30).IsUsHoliday(), "2018 March 30, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2018, 5, 28).IsUsHoliday(), "2018 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2018, 7, 4).IsUsHoliday(), "2018 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2018, 9, 3).IsUsHoliday(), "2018 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2018, 11, 22).IsUsHoliday(), "2018 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2018, 12, 25).IsUsHoliday(), "2018 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2019, 1, 1).IsUsHoliday(), "2019 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2019, 1, 21).IsUsHoliday(), "2019 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2019, 2, 18).IsUsHoliday(), "2019 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2019, 4, 19).IsUsHoliday(), "2019 April 19, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2019, 5, 27).IsUsHoliday(), "2019 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2019, 7, 4).IsUsHoliday(), "2019 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2019, 9, 2).IsUsHoliday(), "2019 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2019, 11, 28).IsUsHoliday(), "2019 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2019, 12, 25).IsUsHoliday(), "2019 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2020, 1, 1).IsUsHoliday(), "2020 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2020, 1, 20).IsUsHoliday(), "2020 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2020, 2, 17).IsUsHoliday(), "2020 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2020, 4, 10).IsUsHoliday(), "2020 April 10, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2020, 5, 25).IsUsHoliday(), "2020 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2020, 7, 3).IsUsHoliday(), "2020 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(2020, 9, 7).IsUsHoliday(), "2020 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2020, 11, 26).IsUsHoliday(), "2020 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2020, 12, 25).IsUsHoliday(), "2020 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(2021, 1, 1).IsUsHoliday(), "2021 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2021, 1, 18).IsUsHoliday(), "2021 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2021, 2, 15).IsUsHoliday(), "2021 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2021, 4, 2).IsUsHoliday(), "2021 April 02, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2021, 5, 31).IsUsHoliday(), "2021 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2021, 7, 5).IsUsHoliday(), "2021 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2021, 9, 6).IsUsHoliday(), "2021 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2021, 11, 25).IsUsHoliday(), "2021 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2021, 12, 24).IsUsHoliday(), "2021 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2022, 1, 17).IsUsHoliday(), "2022 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2022, 2, 21).IsUsHoliday(), "2022 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2022, 4, 15).IsUsHoliday(), "2022 April 15, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2022, 5, 30).IsUsHoliday(), "2022 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2022, 7, 4).IsUsHoliday(), "2022 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2022, 9, 5).IsUsHoliday(), "2022 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2022, 11, 24).IsUsHoliday(), "2022 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2022, 12, 26).IsUsHoliday(), "2022 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2023, 1, 2).IsUsHoliday(), "2023 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2023, 1, 16).IsUsHoliday(), "2023 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2023, 2, 20).IsUsHoliday(), "2023 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2023, 4, 7).IsUsHoliday(), "2023 April 07, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2023, 5, 29).IsUsHoliday(), "2023 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2023, 7, 4).IsUsHoliday(), "2023 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2023, 9, 4).IsUsHoliday(), "2023 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2023, 11, 23).IsUsHoliday(), "2023 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2023, 12, 25).IsUsHoliday(), "2023 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2024, 1, 1).IsUsHoliday(), "2024 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2024, 1, 15).IsUsHoliday(), "2024 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2024, 2, 19).IsUsHoliday(), "2024 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2024, 3, 29).IsUsHoliday(), "2024 March 29, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2024, 5, 27).IsUsHoliday(), "2024 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2024, 7, 4).IsUsHoliday(), "2024 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2024, 9, 2).IsUsHoliday(), "2024 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2024, 11, 28).IsUsHoliday(), "2024 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2024, 12, 25).IsUsHoliday(), "2024 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2025, 1, 1).IsUsHoliday(), "2025 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2025, 1, 20).IsUsHoliday(), "2025 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2025, 2, 17).IsUsHoliday(), "2025 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2025, 4, 18).IsUsHoliday(), "2025 April 18, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2025, 5, 26).IsUsHoliday(), "2025 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2025, 7, 4).IsUsHoliday(), "2025 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2025, 9, 1).IsUsHoliday(), "2025 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2025, 11, 27).IsUsHoliday(), "2025 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2025, 12, 25).IsUsHoliday(), "2025 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2026, 1, 1).IsUsHoliday(), "2026 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2026, 1, 19).IsUsHoliday(), "2026 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2026, 2, 16).IsUsHoliday(), "2026 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2026, 4, 3).IsUsHoliday(), "2026 April 03, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2026, 5, 25).IsUsHoliday(), "2026 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2026, 7, 3).IsUsHoliday(), "2026 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(2026, 9, 7).IsUsHoliday(), "2026 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2026, 11, 26).IsUsHoliday(), "2026 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2026, 12, 25).IsUsHoliday(), "2026 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(2027, 1, 1).IsUsHoliday(), "2027 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2027, 1, 18).IsUsHoliday(), "2027 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2027, 2, 15).IsUsHoliday(), "2027 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2027, 3, 26).IsUsHoliday(), "2027 March 26, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2027, 5, 31).IsUsHoliday(), "2027 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2027, 7, 5).IsUsHoliday(), "2027 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2027, 9, 6).IsUsHoliday(), "2027 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2027, 11, 25).IsUsHoliday(), "2027 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2027, 12, 24).IsUsHoliday(), "2027 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2028, 1, 17).IsUsHoliday(), "2028 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2028, 2, 21).IsUsHoliday(), "2028 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2028, 4, 14).IsUsHoliday(), "2028 April 14, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2028, 5, 29).IsUsHoliday(), "2028 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2028, 7, 4).IsUsHoliday(), "2028 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2028, 9, 4).IsUsHoliday(), "2028 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2028, 11, 23).IsUsHoliday(), "2028 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2028, 12, 25).IsUsHoliday(), "2028 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2029, 1, 1).IsUsHoliday(), "2029 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2029, 1, 15).IsUsHoliday(), "2029 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2029, 2, 19).IsUsHoliday(), "2029 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2029, 3, 30).IsUsHoliday(), "2029 March 30, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2029, 5, 28).IsUsHoliday(), "2029 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2029, 7, 4).IsUsHoliday(), "2029 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2029, 9, 3).IsUsHoliday(), "2029 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2029, 11, 22).IsUsHoliday(), "2029 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2029, 12, 25).IsUsHoliday(), "2029 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2030, 1, 1).IsUsHoliday(), "2030 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2030, 1, 21).IsUsHoliday(), "2030 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2030, 2, 18).IsUsHoliday(), "2030 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2030, 4, 19).IsUsHoliday(), "2030 April 19, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2030, 5, 27).IsUsHoliday(), "2030 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2030, 7, 4).IsUsHoliday(), "2030 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2030, 9, 2).IsUsHoliday(), "2030 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2030, 11, 28).IsUsHoliday(), "2030 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2030, 12, 25).IsUsHoliday(), "2030 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2031, 1, 1).IsUsHoliday(), "2031 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2031, 1, 20).IsUsHoliday(), "2031 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2031, 2, 17).IsUsHoliday(), "2031 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2031, 4, 11).IsUsHoliday(), "2031 April 11, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2031, 5, 26).IsUsHoliday(), "2031 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2031, 7, 4).IsUsHoliday(), "2031 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2031, 9, 1).IsUsHoliday(), "2031 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2031, 11, 27).IsUsHoliday(), "2031 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2031, 12, 25).IsUsHoliday(), "2031 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2032, 1, 1).IsUsHoliday(), "2032 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2032, 1, 19).IsUsHoliday(), "2032 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2032, 2, 16).IsUsHoliday(), "2032 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2032, 3, 26).IsUsHoliday(), "2032 March 26, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2032, 5, 31).IsUsHoliday(), "2032 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2032, 7, 5).IsUsHoliday(), "2032 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2032, 9, 6).IsUsHoliday(), "2032 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2032, 11, 25).IsUsHoliday(), "2032 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2032, 12, 24).IsUsHoliday(), "2032 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2033, 1, 17).IsUsHoliday(), "2033 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2033, 2, 21).IsUsHoliday(), "2033 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2033, 4, 15).IsUsHoliday(), "2033 April 15, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2033, 5, 30).IsUsHoliday(), "2033 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2033, 7, 4).IsUsHoliday(), "2033 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2033, 9, 5).IsUsHoliday(), "2033 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2033, 11, 24).IsUsHoliday(), "2033 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2033, 12, 26).IsUsHoliday(), "2033 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2034, 1, 2).IsUsHoliday(), "2034 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2034, 1, 16).IsUsHoliday(), "2034 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2034, 2, 20).IsUsHoliday(), "2034 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2034, 4, 7).IsUsHoliday(), "2034 April 07, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2034, 5, 29).IsUsHoliday(), "2034 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2034, 7, 4).IsUsHoliday(), "2034 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2034, 9, 4).IsUsHoliday(), "2034 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2034, 11, 23).IsUsHoliday(), "2034 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2034, 12, 25).IsUsHoliday(), "2034 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2035, 1, 1).IsUsHoliday(), "2035 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2035, 1, 15).IsUsHoliday(), "2035 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2035, 2, 19).IsUsHoliday(), "2035 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2035, 3, 23).IsUsHoliday(), "2035 March 23, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2035, 5, 28).IsUsHoliday(), "2035 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2035, 7, 4).IsUsHoliday(), "2035 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2035, 9, 3).IsUsHoliday(), "2035 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2035, 11, 22).IsUsHoliday(), "2035 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2035, 12, 25).IsUsHoliday(), "2035 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2036, 1, 1).IsUsHoliday(), "2036 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2036, 1, 21).IsUsHoliday(), "2036 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2036, 2, 18).IsUsHoliday(), "2036 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2036, 4, 11).IsUsHoliday(), "2036 April 11, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2036, 5, 26).IsUsHoliday(), "2036 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2036, 7, 4).IsUsHoliday(), "2036 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2036, 9, 1).IsUsHoliday(), "2036 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2036, 11, 27).IsUsHoliday(), "2036 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2036, 12, 25).IsUsHoliday(), "2036 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2037, 1, 1).IsUsHoliday(), "2037 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2037, 1, 19).IsUsHoliday(), "2037 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2037, 2, 16).IsUsHoliday(), "2037 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2037, 4, 3).IsUsHoliday(), "2037 April 03, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2037, 5, 25).IsUsHoliday(), "2037 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2037, 7, 3).IsUsHoliday(), "2037 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(2037, 9, 7).IsUsHoliday(), "2037 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2037, 11, 26).IsUsHoliday(), "2037 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2037, 12, 25).IsUsHoliday(), "2037 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(2038, 1, 1).IsUsHoliday(), "2038 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2038, 1, 18).IsUsHoliday(), "2038 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2038, 2, 15).IsUsHoliday(), "2038 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2038, 4, 23).IsUsHoliday(), "2038 April 23, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2038, 5, 31).IsUsHoliday(), "2038 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2038, 7, 5).IsUsHoliday(), "2038 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2038, 9, 6).IsUsHoliday(), "2038 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2038, 11, 25).IsUsHoliday(), "2038 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2038, 12, 24).IsUsHoliday(), "2038 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2039, 1, 17).IsUsHoliday(), "2039 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2039, 2, 21).IsUsHoliday(), "2039 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2039, 4, 8).IsUsHoliday(), "2039 April 08, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2039, 5, 30).IsUsHoliday(), "2039 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2039, 7, 4).IsUsHoliday(), "2039 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2039, 9, 5).IsUsHoliday(), "2039 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2039, 11, 24).IsUsHoliday(), "2039 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2039, 12, 26).IsUsHoliday(), "2039 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2040, 1, 2).IsUsHoliday(), "2040 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2040, 1, 16).IsUsHoliday(), "2040 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2040, 2, 20).IsUsHoliday(), "2040 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2040, 3, 30).IsUsHoliday(), "2040 March 30, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2040, 5, 28).IsUsHoliday(), "2040 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2040, 7, 4).IsUsHoliday(), "2040 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2040, 9, 3).IsUsHoliday(), "2040 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2040, 11, 22).IsUsHoliday(), "2040 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2040, 12, 25).IsUsHoliday(), "2040 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2041, 1, 1).IsUsHoliday(), "2041 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2041, 1, 21).IsUsHoliday(), "2041 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2041, 2, 18).IsUsHoliday(), "2041 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2041, 4, 19).IsUsHoliday(), "2041 April 19, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2041, 5, 27).IsUsHoliday(), "2041 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2041, 7, 4).IsUsHoliday(), "2041 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2041, 9, 2).IsUsHoliday(), "2041 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2041, 11, 28).IsUsHoliday(), "2041 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2041, 12, 25).IsUsHoliday(), "2041 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2042, 1, 1).IsUsHoliday(), "2042 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2042, 1, 20).IsUsHoliday(), "2042 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2042, 2, 17).IsUsHoliday(), "2042 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2042, 4, 4).IsUsHoliday(), "2042 April 04, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2042, 5, 26).IsUsHoliday(), "2042 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2042, 7, 4).IsUsHoliday(), "2042 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2042, 9, 1).IsUsHoliday(), "2042 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2042, 11, 27).IsUsHoliday(), "2042 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2042, 12, 25).IsUsHoliday(), "2042 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2043, 1, 1).IsUsHoliday(), "2043 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2043, 1, 19).IsUsHoliday(), "2043 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2043, 2, 16).IsUsHoliday(), "2043 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2043, 3, 27).IsUsHoliday(), "2043 March 27, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2043, 5, 25).IsUsHoliday(), "2043 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2043, 7, 3).IsUsHoliday(), "2043 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(2043, 9, 7).IsUsHoliday(), "2043 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2043, 11, 26).IsUsHoliday(), "2043 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2043, 12, 25).IsUsHoliday(), "2043 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(2044, 1, 1).IsUsHoliday(), "2044 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2044, 1, 18).IsUsHoliday(), "2044 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2044, 2, 15).IsUsHoliday(), "2044 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2044, 4, 15).IsUsHoliday(), "2044 April 15, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2044, 5, 30).IsUsHoliday(), "2044 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2044, 7, 4).IsUsHoliday(), "2044 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2044, 9, 5).IsUsHoliday(), "2044 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2044, 11, 24).IsUsHoliday(), "2044 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2044, 12, 26).IsUsHoliday(), "2044 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2045, 1, 2).IsUsHoliday(), "2045 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2045, 1, 16).IsUsHoliday(), "2045 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2045, 2, 20).IsUsHoliday(), "2045 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2045, 4, 7).IsUsHoliday(), "2045 April 07, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2045, 5, 29).IsUsHoliday(), "2045 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2045, 7, 4).IsUsHoliday(), "2045 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2045, 9, 4).IsUsHoliday(), "2045 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2045, 11, 23).IsUsHoliday(), "2045 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2045, 12, 25).IsUsHoliday(), "2045 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2046, 1, 1).IsUsHoliday(), "2046 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2046, 1, 15).IsUsHoliday(), "2046 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2046, 2, 19).IsUsHoliday(), "2046 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2046, 3, 23).IsUsHoliday(), "2046 March 23, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2046, 5, 28).IsUsHoliday(), "2046 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2046, 7, 4).IsUsHoliday(), "2046 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2046, 9, 3).IsUsHoliday(), "2046 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2046, 11, 22).IsUsHoliday(), "2046 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2046, 12, 25).IsUsHoliday(), "2046 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2047, 1, 1).IsUsHoliday(), "2047 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2047, 1, 21).IsUsHoliday(), "2047 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2047, 2, 18).IsUsHoliday(), "2047 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2047, 4, 12).IsUsHoliday(), "2047 April 12, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2047, 5, 27).IsUsHoliday(), "2047 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2047, 7, 4).IsUsHoliday(), "2047 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2047, 9, 2).IsUsHoliday(), "2047 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2047, 11, 28).IsUsHoliday(), "2047 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2047, 12, 25).IsUsHoliday(), "2047 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2048, 1, 1).IsUsHoliday(), "2048 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2048, 1, 20).IsUsHoliday(), "2048 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2048, 2, 17).IsUsHoliday(), "2048 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2048, 4, 3).IsUsHoliday(), "2048 April 03, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2048, 5, 25).IsUsHoliday(), "2048 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2048, 7, 3).IsUsHoliday(), "2048 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(2048, 9, 7).IsUsHoliday(), "2048 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2048, 11, 26).IsUsHoliday(), "2048 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2048, 12, 25).IsUsHoliday(), "2048 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(2049, 1, 1).IsUsHoliday(), "2049 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2049, 1, 18).IsUsHoliday(), "2049 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2049, 2, 15).IsUsHoliday(), "2049 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2049, 4, 16).IsUsHoliday(), "2049 April 16, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2049, 5, 31).IsUsHoliday(), "2049 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2049, 7, 5).IsUsHoliday(), "2049 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2049, 9, 6).IsUsHoliday(), "2049 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2049, 11, 25).IsUsHoliday(), "2049 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2049, 12, 24).IsUsHoliday(), "2049 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2050, 1, 17).IsUsHoliday(), "2050 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2050, 2, 21).IsUsHoliday(), "2050 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2050, 4, 8).IsUsHoliday(), "2050 April 08, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2050, 5, 30).IsUsHoliday(), "2050 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2050, 7, 4).IsUsHoliday(), "2050 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2050, 9, 5).IsUsHoliday(), "2050 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2050, 11, 24).IsUsHoliday(), "2050 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2050, 12, 26).IsUsHoliday(), "2050 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2051, 1, 2).IsUsHoliday(), "2051 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2051, 1, 16).IsUsHoliday(), "2051 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2051, 2, 20).IsUsHoliday(), "2051 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2051, 3, 31).IsUsHoliday(), "2051 March 31, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2051, 5, 29).IsUsHoliday(), "2051 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2051, 7, 4).IsUsHoliday(), "2051 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2051, 9, 4).IsUsHoliday(), "2051 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2051, 11, 23).IsUsHoliday(), "2051 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2051, 12, 25).IsUsHoliday(), "2051 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2052, 1, 1).IsUsHoliday(), "2052 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2052, 1, 15).IsUsHoliday(), "2052 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2052, 2, 19).IsUsHoliday(), "2052 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2052, 4, 19).IsUsHoliday(), "2052 April 19, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2052, 5, 27).IsUsHoliday(), "2052 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2052, 7, 4).IsUsHoliday(), "2052 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2052, 9, 2).IsUsHoliday(), "2052 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2052, 11, 28).IsUsHoliday(), "2052 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2052, 12, 25).IsUsHoliday(), "2052 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2053, 1, 1).IsUsHoliday(), "2053 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2053, 1, 20).IsUsHoliday(), "2053 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2053, 2, 17).IsUsHoliday(), "2053 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2053, 4, 4).IsUsHoliday(), "2053 April 04, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2053, 5, 26).IsUsHoliday(), "2053 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2053, 7, 4).IsUsHoliday(), "2053 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2053, 9, 1).IsUsHoliday(), "2053 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2053, 11, 27).IsUsHoliday(), "2053 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2053, 12, 25).IsUsHoliday(), "2053 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2054, 1, 1).IsUsHoliday(), "2054 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2054, 1, 19).IsUsHoliday(), "2054 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2054, 2, 16).IsUsHoliday(), "2054 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2054, 3, 27).IsUsHoliday(), "2054 March 27, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2054, 5, 25).IsUsHoliday(), "2054 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2054, 7, 3).IsUsHoliday(), "2054 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(2054, 9, 7).IsUsHoliday(), "2054 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2054, 11, 26).IsUsHoliday(), "2054 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2054, 12, 25).IsUsHoliday(), "2054 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(2055, 1, 1).IsUsHoliday(), "2055 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2055, 1, 18).IsUsHoliday(), "2055 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2055, 2, 15).IsUsHoliday(), "2055 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2055, 4, 16).IsUsHoliday(), "2055 April 16, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2055, 5, 31).IsUsHoliday(), "2055 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2055, 7, 5).IsUsHoliday(), "2055 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2055, 9, 6).IsUsHoliday(), "2055 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2055, 11, 25).IsUsHoliday(), "2055 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2055, 12, 24).IsUsHoliday(), "2055 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2056, 1, 17).IsUsHoliday(), "2056 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2056, 2, 21).IsUsHoliday(), "2056 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2056, 3, 31).IsUsHoliday(), "2056 March 31, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2056, 5, 29).IsUsHoliday(), "2056 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2056, 7, 4).IsUsHoliday(), "2056 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2056, 9, 4).IsUsHoliday(), "2056 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2056, 11, 23).IsUsHoliday(), "2056 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2056, 12, 25).IsUsHoliday(), "2056 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2057, 1, 1).IsUsHoliday(), "2057 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2057, 1, 15).IsUsHoliday(), "2057 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2057, 2, 19).IsUsHoliday(), "2057 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2057, 4, 20).IsUsHoliday(), "2057 April 20, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2057, 5, 28).IsUsHoliday(), "2057 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2057, 7, 4).IsUsHoliday(), "2057 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2057, 9, 3).IsUsHoliday(), "2057 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2057, 11, 22).IsUsHoliday(), "2057 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2057, 12, 25).IsUsHoliday(), "2057 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2058, 1, 1).IsUsHoliday(), "2058 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2058, 1, 21).IsUsHoliday(), "2058 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2058, 2, 18).IsUsHoliday(), "2058 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2058, 4, 12).IsUsHoliday(), "2058 April 12, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2058, 5, 27).IsUsHoliday(), "2058 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2058, 7, 4).IsUsHoliday(), "2058 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2058, 9, 2).IsUsHoliday(), "2058 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2058, 11, 28).IsUsHoliday(), "2058 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2058, 12, 25).IsUsHoliday(), "2058 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2059, 1, 1).IsUsHoliday(), "2059 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2059, 1, 20).IsUsHoliday(), "2059 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2059, 2, 17).IsUsHoliday(), "2059 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2059, 3, 28).IsUsHoliday(), "2059 March 28, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2059, 5, 26).IsUsHoliday(), "2059 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2059, 7, 4).IsUsHoliday(), "2059 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2059, 9, 1).IsUsHoliday(), "2059 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2059, 11, 27).IsUsHoliday(), "2059 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2059, 12, 25).IsUsHoliday(), "2059 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2060, 1, 1).IsUsHoliday(), "2060 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2060, 1, 19).IsUsHoliday(), "2060 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2060, 2, 16).IsUsHoliday(), "2060 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2060, 4, 16).IsUsHoliday(), "2060 April 16, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2060, 5, 31).IsUsHoliday(), "2060 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2060, 7, 5).IsUsHoliday(), "2060 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2060, 9, 6).IsUsHoliday(), "2060 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2060, 11, 25).IsUsHoliday(), "2060 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2060, 12, 24).IsUsHoliday(), "2060 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2061, 1, 17).IsUsHoliday(), "2061 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2061, 2, 21).IsUsHoliday(), "2061 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2061, 4, 8).IsUsHoliday(), "2061 April 08, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2061, 5, 30).IsUsHoliday(), "2061 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2061, 7, 4).IsUsHoliday(), "2061 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2061, 9, 5).IsUsHoliday(), "2061 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2061, 11, 24).IsUsHoliday(), "2061 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2061, 12, 26).IsUsHoliday(), "2061 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2062, 1, 2).IsUsHoliday(), "2062 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2062, 1, 16).IsUsHoliday(), "2062 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2062, 2, 20).IsUsHoliday(), "2062 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2062, 3, 24).IsUsHoliday(), "2062 March 24, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2062, 5, 29).IsUsHoliday(), "2062 May 29, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2062, 7, 4).IsUsHoliday(), "2062 July 04, Tue [Independence Day]");
             Assert.IsTrue(new DateTime(2062, 9, 4).IsUsHoliday(), "2062 September 04, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2062, 11, 23).IsUsHoliday(), "2062 November 23, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2062, 12, 25).IsUsHoliday(), "2062 December 25, Mon [Christmas]");

             Assert.IsTrue(new DateTime(2063, 1, 1).IsUsHoliday(), "2063 January 01, Mon [New Year's Day]");
             Assert.IsTrue(new DateTime(2063, 1, 15).IsUsHoliday(), "2063 January 15, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2063, 2, 19).IsUsHoliday(), "2063 February 19, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2063, 4, 13).IsUsHoliday(), "2063 April 13, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2063, 5, 28).IsUsHoliday(), "2063 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2063, 7, 4).IsUsHoliday(), "2063 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2063, 9, 3).IsUsHoliday(), "2063 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2063, 11, 22).IsUsHoliday(), "2063 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2063, 12, 25).IsUsHoliday(), "2063 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2064, 1, 1).IsUsHoliday(), "2064 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2064, 1, 21).IsUsHoliday(), "2064 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2064, 2, 18).IsUsHoliday(), "2064 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2064, 4, 4).IsUsHoliday(), "2064 April 04, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2064, 5, 26).IsUsHoliday(), "2064 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2064, 7, 4).IsUsHoliday(), "2064 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2064, 9, 1).IsUsHoliday(), "2064 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2064, 11, 27).IsUsHoliday(), "2064 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2064, 12, 25).IsUsHoliday(), "2064 December 25, Thu [Christmas]");

             Assert.IsTrue(new DateTime(2065, 1, 1).IsUsHoliday(), "2065 January 01, Thu [New Year's Day]");
             Assert.IsTrue(new DateTime(2065, 1, 19).IsUsHoliday(), "2065 January 19, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2065, 2, 16).IsUsHoliday(), "2065 February 16, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2065, 3, 27).IsUsHoliday(), "2065 March 27, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2065, 5, 25).IsUsHoliday(), "2065 May 25, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2065, 7, 3).IsUsHoliday(), "2065 July 03, Fri [Friday before Independence Day]");
             Assert.IsTrue(new DateTime(2065, 9, 7).IsUsHoliday(), "2065 September 07, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2065, 11, 26).IsUsHoliday(), "2065 November 26, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2065, 12, 25).IsUsHoliday(), "2065 December 25, Fri [Christmas]");

             Assert.IsTrue(new DateTime(2066, 1, 1).IsUsHoliday(), "2066 January 01, Fri [New Year's Day]");
             Assert.IsTrue(new DateTime(2066, 1, 18).IsUsHoliday(), "2066 January 18, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2066, 2, 15).IsUsHoliday(), "2066 February 15, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2066, 4, 9).IsUsHoliday(), "2066 April 09, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2066, 5, 31).IsUsHoliday(), "2066 May 31, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2066, 7, 5).IsUsHoliday(), "2066 July 05, Mon [Monday after Independence Day]");
             Assert.IsTrue(new DateTime(2066, 9, 6).IsUsHoliday(), "2066 September 06, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2066, 11, 25).IsUsHoliday(), "2066 November 25, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2066, 12, 24).IsUsHoliday(), "2066 December 24, Fri [Friday before Christmas]");

             Assert.IsTrue(new DateTime(2067, 1, 17).IsUsHoliday(), "2067 January 17, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2067, 2, 21).IsUsHoliday(), "2067 February 21, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2067, 4, 1).IsUsHoliday(), "2067 April 01, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2067, 5, 30).IsUsHoliday(), "2067 May 30, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2067, 7, 4).IsUsHoliday(), "2067 July 04, Mon [Independence Day]");
             Assert.IsTrue(new DateTime(2067, 9, 5).IsUsHoliday(), "2067 September 05, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2067, 11, 24).IsUsHoliday(), "2067 November 24, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2067, 12, 26).IsUsHoliday(), "2067 December 26, Mon [Monday after Christmas]");

             Assert.IsTrue(new DateTime(2068, 1, 2).IsUsHoliday(), "2068 January 02, Mon [Monday after New Year's Day]");
             Assert.IsTrue(new DateTime(2068, 1, 16).IsUsHoliday(), "2068 January 16, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2068, 2, 20).IsUsHoliday(), "2068 February 20, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2068, 4, 20).IsUsHoliday(), "2068 April 20, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2068, 5, 28).IsUsHoliday(), "2068 May 28, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2068, 7, 4).IsUsHoliday(), "2068 July 04, Wed [Independence Day]");
             Assert.IsTrue(new DateTime(2068, 9, 3).IsUsHoliday(), "2068 September 03, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2068, 11, 22).IsUsHoliday(), "2068 November 22, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2068, 12, 25).IsUsHoliday(), "2068 December 25, Tue [Christmas]");

             Assert.IsTrue(new DateTime(2069, 1, 1).IsUsHoliday(), "2069 January 01, Tue [New Year's Day]");
             Assert.IsTrue(new DateTime(2069, 1, 21).IsUsHoliday(), "2069 January 21, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2069, 2, 18).IsUsHoliday(), "2069 February 18, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2069, 4, 12).IsUsHoliday(), "2069 April 12, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2069, 5, 27).IsUsHoliday(), "2069 May 27, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2069, 7, 4).IsUsHoliday(), "2069 July 04, Thu [Independence Day]");
             Assert.IsTrue(new DateTime(2069, 9, 2).IsUsHoliday(), "2069 September 02, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2069, 11, 28).IsUsHoliday(), "2069 November 28, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2069, 12, 25).IsUsHoliday(), "2069 December 25, Wed [Christmas]");

             Assert.IsTrue(new DateTime(2070, 1, 1).IsUsHoliday(), "2070 January 01, Wed [New Year's Day]");
             Assert.IsTrue(new DateTime(2070, 1, 20).IsUsHoliday(), "2070 January 20, Mon [Martin Luther King]");
             Assert.IsTrue(new DateTime(2070, 2, 17).IsUsHoliday(), "2070 February 17, Mon [Washington's Birthday]");
             Assert.IsTrue(new DateTime(2070, 3, 28).IsUsHoliday(), "2070 March 28, Fri [Good Friday]");
             Assert.IsTrue(new DateTime(2070, 5, 26).IsUsHoliday(), "2070 May 26, Mon [Memorial Day]");
             Assert.IsTrue(new DateTime(2070, 7, 4).IsUsHoliday(), "2070 July 04, Fri [Independence Day]");
             Assert.IsTrue(new DateTime(2070, 9, 1).IsUsHoliday(), "2070 September 01, Mon [Labor Day]");
             Assert.IsTrue(new DateTime(2070, 11, 27).IsUsHoliday(), "2070 November 27, Thu [Thanksgiving]");
             Assert.IsTrue(new DateTime(2070, 12, 25).IsUsHoliday(), "2070 December 25, Thu [Christmas]");
        }

        [TestMethod]
        public void UsHoliday_IsUsHoliday_GoodFriday_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(-2).Date;
                Assert.IsTrue(date.IsUsHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void UsHoliday_IsUsWorkday_GivenADay_ReturnsTheInverseOfUsHoliday()
        {
            DateTime firstDate = new DateTime(1999, 1, 1);
            for (int i = 0; i < 365 * 3; ++i)
            {
                DateTime date = firstDate.AddDays(i);
                Assert.AreNotEqual(date.IsUsHoliday(), date.IsUsWorkday(), date.ToLongDateString());
            }
        }
    }
}
