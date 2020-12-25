using System;
using Mbs.Trading.Holidays;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Holidays
{
    [TestClass]
    public class IcelandicHolidayTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_ExplicitlyKnownDays_AreAlwaysHolidays()
        {
            Assert.IsTrue(new DateTime(2016, 1, 1).IsIcelandicHoliday(), "2016 January 01, Fri [New Year's Day]");
            Assert.IsTrue(new DateTime(2016, 3, 24).IsIcelandicHoliday(), "2016 March 24, Thu [Holy Thursday]");
            Assert.IsTrue(new DateTime(2016, 3, 25).IsIcelandicHoliday(), "2016 March 25, Fri [Good Friday]");
            Assert.IsTrue(new DateTime(2016, 3, 28).IsIcelandicHoliday(), "2016 March 28, Mon [Easter Monday]");
            Assert.IsTrue(new DateTime(2016, 5, 5).IsIcelandicHoliday(), "2016 May 5, Thu [Ascension]");
            Assert.IsTrue(new DateTime(2016, 5, 16).IsIcelandicHoliday(), "2016 May 16, Mon [Whit Monday]");
            Assert.IsTrue(new DateTime(2016, 6, 17).IsIcelandicHoliday(), "2016 June 17, Fri [Independence Day]");
            Assert.IsTrue(new DateTime(2016, 8, 1).IsIcelandicHoliday(), "2016 August 1, Mon [Commerce Day]");
            Assert.IsTrue(new DateTime(2016, 12, 26).IsIcelandicHoliday(), "2016 December 26, Mon [Boxing Day]");

            Assert.IsTrue(new DateTime(2017, 1, 1).IsIcelandicHoliday(), "2017 January 01, Sun [New Year's Day]");
            Assert.IsTrue(new DateTime(2017, 4, 13).IsIcelandicHoliday(), "2017 April 13, Thu [Holy Thursday]");
            Assert.IsTrue(new DateTime(2017, 4, 14).IsIcelandicHoliday(), "2017 April 14, Fri [Good Friday]");
            Assert.IsTrue(new DateTime(2017, 4, 17).IsIcelandicHoliday(), "2017 April 17, Mon [Easter Monday]");
            Assert.IsTrue(new DateTime(2017, 4, 20).IsIcelandicHoliday(), "2017 April 20, Thu [Ascension]");
            Assert.IsTrue(new DateTime(2017, 5, 1).IsIcelandicHoliday(), "2017 May 1, Mon [Labour Day]");
            Assert.IsTrue(new DateTime(2017, 5, 25).IsIcelandicHoliday(), "2017 May 25, Thu []");
            Assert.IsTrue(new DateTime(2017, 6, 5).IsIcelandicHoliday(), "2017 June 5, Mon []");
            Assert.IsTrue(new DateTime(2017, 8, 7).IsIcelandicHoliday(), "2017 August 7, Mon [Commerce Day]");
            Assert.IsTrue(new DateTime(2017, 12, 25).IsIcelandicHoliday(), "2017 December 25, Mon [Christmas]");
            Assert.IsTrue(new DateTime(2017, 12, 26).IsIcelandicHoliday(), "2017 December 26, Tue [Boxing Day]");

            Assert.IsTrue(new DateTime(2018, 1, 1).IsIcelandicHoliday(), "2018 January 01, Mon [New Year's Day]");
            Assert.IsTrue(new DateTime(2018, 3, 29).IsIcelandicHoliday(), "2018 March 29, Thu [Holy Thursday]");
            Assert.IsTrue(new DateTime(2018, 3, 30).IsIcelandicHoliday(), "2018 March 30, Fri [Good Friday]");
            Assert.IsTrue(new DateTime(2018, 4, 2).IsIcelandicHoliday(), "2018 April 2, Mon [Easter Monday]");
            Assert.IsTrue(new DateTime(2018, 4, 19).IsIcelandicHoliday(), "2018 April 19, Thu [Ascension]");
            Assert.IsTrue(new DateTime(2018, 5, 1).IsIcelandicHoliday(), "2018 May 1, Tue [Labour Day]");
            Assert.IsTrue(new DateTime(2018, 5, 10).IsIcelandicHoliday(), "2018 May 10, Thu []");
            Assert.IsTrue(new DateTime(2018, 5, 21).IsIcelandicHoliday(), "2018 May 21, Mon []");
            Assert.IsTrue(new DateTime(2018, 8, 6).IsIcelandicHoliday(), "2018 August 6, Mon [Commerce Day]");
            Assert.IsTrue(new DateTime(2018, 12, 25).IsIcelandicHoliday(), "2018 December 25 [Christmas]");
            Assert.IsTrue(new DateTime(2018, 12, 26).IsIcelandicHoliday(), "2018 December 26 [Boxing Day]");
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_MaundyThursday_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(-3).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_GoodFriday_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(-2).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_EasterMonday_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(1).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_LabourDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 5, 1).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_AscensionDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(39).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_IndependenceDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 6, 17).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_FirstDayOfSummer_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                // First Day of Summer (third or fourth Thursday in April).
                for (int day = 19; day <= 25; ++day)
                {
                    DateTime date = new DateTime(i, 4, day).Date;
                    if (date.DayOfWeek == DayOfWeek.Thursday)
                    {
                        Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_CommerceDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                // Commerce Day (first Monday in August).
                for (int day = 1; day <= 7; ++day)
                {
                    DateTime date = new DateTime(i, 8, day).Date;
                    if (date.DayOfWeek == DayOfWeek.Monday)
                    {
                        Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_WhitMonday_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(50).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_ChristmasDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 25).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_BoxingDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 26).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_NewYearsDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());

                for (int day = 2; day <= 3; ++day)
                {
                    date = new DateTime(i, 1, day).Date;
                    if (date.DayOfWeek == DayOfWeek.Monday)
                    {
                        Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicHoliday_ObservedHoliday_IsHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 26).Date;
                Assert.IsTrue(date.IsIcelandicHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void IcelandicHoliday_IsIcelandicWorkday_GivenADay_ReturnsTheInverseOfIcelandicHoliday()
        {
            DateTime firstDate = new DateTime(1999, 1, 1);
            for (int i = 0; i < 365 * 3; ++i)
            {
                DateTime date = firstDate.AddDays(i);
                Assert.AreNotEqual(date.IsIcelandicHoliday(), date.IsIcelandicWorkday(), date.ToLongDateString());
            }
        }
    }
}
