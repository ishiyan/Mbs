using System;
using Mbs.Trading.Holidays;
using Mbs.Trading.Time.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Holidays
{
    [TestClass]
    public class TargetHolidayTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_GoodFriday_NotHolidayBefore2000()
        {
            for (int i = 1900; i < 2000; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(-2).Date;
                if (date.IsWeekend())
                {
                    continue;
                }

                Assert.IsFalse(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_GoodFriday_IsHolidayAfter1999()
        {
            for (int i = 2000; i < 2100; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(-2).Date;
                Assert.IsTrue(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_EasterMonday_NotHolidayBefore2000()
        {
            for (int i = 1900; i < 2000; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(1).Date;
                if (date.IsWeekend())
                {
                    continue;
                }

                Assert.IsFalse(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_EasterMonday_IsHolidayAfter1999()
        {
            for (int i = 2000; i < 2100; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).EasterSundayFromYear().AddDays(1).Date;
                Assert.IsTrue(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_LabourDay_NotHolidayBefore2000()
        {
            for (int i = 1900; i < 2000; ++i)
            {
                DateTime date = new DateTime(i, 5, 1).Date;
                if (date.IsWeekend())
                {
                    continue;
                }

                Assert.IsFalse(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_LabourDay_IsHolidayAfter1999()
        {
            for (int i = 2000; i < 2100; ++i)
            {
                DateTime date = new DateTime(i, 5, 1).Date;
                Assert.IsTrue(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_ChristmasDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 12, 25).Date;
                Assert.IsTrue(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_BoxingDay_NotHolidayBefore2000()
        {
            for (int i = 1900; i < 2000; ++i)
            {
                DateTime date = new DateTime(i, 12, 26).Date;
                if (date.IsWeekend())
                {
                    continue;
                }

                Assert.IsFalse(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_BoxingDay_IsHolidayAfter1999()
        {
            for (int i = 2000; i < 2100; ++i)
            {
                DateTime date = new DateTime(i, 12, 26).Date;
                Assert.IsTrue(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_NewYearsEve_IsHolidayFrom1998Till2001Only()
        {
            for (int i = 1998; i < 2002; ++i)
            {
                DateTime date = new DateTime(i, 12, 31).Date;
                Assert.IsTrue(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_NewYearsEve_NotHolidayBefore1998()
        {
            for (int i = 1900; i < 1998; ++i)
            {
                DateTime date = new DateTime(i, 12, 31).Date;
                if (date.IsWeekend())
                {
                    continue;
                }

                Assert.IsFalse(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_NewYearsEve_NotHolidayAfter2001()
        {
            for (int i = 2002; i < 2100; ++i)
            {
                DateTime date = new DateTime(i, 12, 31).Date;
                if (date.IsWeekend())
                {
                    continue;
                }

                Assert.IsFalse(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetHoliday_NewYearsDay_IsAlwaysHoliday()
        {
            for (int i = 1900; i < 2050; ++i)
            {
                DateTime date = new DateTime(i, 1, 1).Date;
                Assert.IsTrue(date.IsTargetHoliday(), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void TargetHoliday_IsTargetWorkday_GivenADay_ReturnsTheInverseOfTargetHoliday()
        {
            DateTime firstDate = new DateTime(1999, 1, 1);
            for (int i = 0; i < 365 * 3; ++i)
            {
                DateTime date = firstDate.AddDays(i);
                Assert.AreNotEqual(date.IsTargetHoliday(), date.IsTargetWorkday(), date.ToLongDateString());
            }
        }
    }
}
