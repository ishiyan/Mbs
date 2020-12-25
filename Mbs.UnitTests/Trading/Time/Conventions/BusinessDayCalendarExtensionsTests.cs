using System;
using System.Collections.Generic;
using Mbs.Trading.Markets;
using Mbs.Trading.Time.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Time.Conventions
{
    [TestClass]
    public class BusinessDayCalendarExtensionsTests
    {
        private static readonly DateTime Saturday = new DateTime(2020, 12, 19);
        private static readonly DateTime Sunday = new DateTime(2020, 12, 20);
        private static readonly DateTime Wednesday = new DateTime(2020, 12, 16);
        private static readonly DateTime CommonPrayerDay = new DateTime(1994, 4, 29);
        private static readonly DateTime ConstitutionDay = new DateTime(1993, 5, 17);
        private static readonly DateTime MidsummerEve = new DateTime(1993, 6, 25);
        private static readonly DateTime Berchtoldstag = new DateTime(1995, 1, 2);
        private static readonly DateTime IndependenceDay = new DateTime(1989, 7, 4);
        private static readonly DateTime EasterMonday = new DateTime(2008, 3, 24);
        private static readonly DateTime LabourDay = new DateTime(2020, 5, 1);

        private static readonly ExchangeMic[] MicsEuronext =
        {
            ExchangeMic.Xbru, ExchangeMic.Alxb, ExchangeMic.Enxb, ExchangeMic.Mlxb, ExchangeMic.Tnlb, ExchangeMic.Vpxb,
            ExchangeMic.Xbrd, ExchangeMic.Xpar, ExchangeMic.Alxp, ExchangeMic.Xmat, ExchangeMic.Xmli, ExchangeMic.Xmon,
            ExchangeMic.Xlis, ExchangeMic.Alxl, ExchangeMic.Enxl, ExchangeMic.Mfox, ExchangeMic.Wqxl, ExchangeMic.Xams,
            ExchangeMic.Tnla, ExchangeMic.Xeuc, ExchangeMic.Xeue, ExchangeMic.Xeui, ExchangeMic.Xldn,
        };

        private static readonly ExchangeMic[] MicsUs =
        {
            ExchangeMic.Edga, ExchangeMic.Edgx, ExchangeMic.Bato, ExchangeMic.Bats, ExchangeMic.Baty, ExchangeMic.Bids,
            ExchangeMic.Xbox, ExchangeMic.C2Ox, ExchangeMic.Xcbo, ExchangeMic.Xcbf, ExchangeMic.Fcbt, ExchangeMic.Xcbt,
            ExchangeMic.Xchi, ExchangeMic.Fcme, ExchangeMic.Xcme, ExchangeMic.Xcec, ExchangeMic.Edgo, ExchangeMic.Eris,
            ExchangeMic.Ifus, ExchangeMic.Imfx, ExchangeMic.Iexg, ExchangeMic.Xisx, ExchangeMic.Gmni, ExchangeMic.Mcry,
            ExchangeMic.Xkbt, ExchangeMic.Xmio, ExchangeMic.Xmge, ExchangeMic.Gree, ExchangeMic.Pipe, ExchangeMic.Otcq,
            ExchangeMic.Psgm, ExchangeMic.Pinx, ExchangeMic.Xpho, ExchangeMic.Ootc, ExchangeMic.Xotc, ExchangeMic.Xoch,
            ExchangeMic.Xnyl, ExchangeMic.Nodx, ExchangeMic.Xnym, ExchangeMic.Xcis, ExchangeMic.Xndq, ExchangeMic.Xpsx,
            ExchangeMic.Xphl, ExchangeMic.Xbxo, ExchangeMic.Xbos, ExchangeMic.Xnms, ExchangeMic.Xngs, ExchangeMic.Xncm,
            ExchangeMic.Xnas, ExchangeMic.Amxo, ExchangeMic.Xase, ExchangeMic.Arco, ExchangeMic.Arcx, ExchangeMic.Xnys,
        };

        private static readonly ExchangeMic[] MicsSwiss =
        {
            ExchangeMic.Xbrn, ExchangeMic.Aixe, ExchangeMic.Xqmh, ExchangeMic.Xswb, ExchangeMic.Xswx, ExchangeMic.Xvtx,
        };

        private static readonly ExchangeMic[] MicsSwedish =
        {
            ExchangeMic.Xsto, ExchangeMic.Nmtf, ExchangeMic.Xngm, ExchangeMic.Xndx, ExchangeMic.Fnse, ExchangeMic.Xsat,
        };

        private static readonly ExchangeMic[] MicsNorwegian =
        {
            ExchangeMic.Xosl, ExchangeMic.Xoas, ExchangeMic.Notc, ExchangeMic.Norx,
        };

        private static readonly ExchangeMic[] MicsDanish =
        {
            ExchangeMic.Xcse, ExchangeMic.Dktc, ExchangeMic.Fndk,
        };

        private static readonly ExchangeMic[] MicsNotSpecified =
        {
            ExchangeMic.Xxxx,
        };

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_Mics_IsCorrect()
        {
            foreach (var mic in CombineAllMics())
            {
                Assert.IsFalse(Wednesday.IsBusinessHoliday(mic), mic.ToString());
                Assert.IsTrue(Sunday.IsBusinessHoliday(mic), mic.ToString());
            }
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_MicsEuronext_IsCorrect()
        {
            foreach (var mic in MicsEuronext)
            {
                Assert.IsTrue(EasterMonday.IsBusinessHoliday(mic), mic.ToString());
            }
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_MicsUnitedStates_IsCorrect()
        {
            foreach (var mic in MicsUs)
            {
                Assert.IsTrue(IndependenceDay.IsBusinessHoliday(mic), mic.ToString());
            }
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_MicsSwiss_IsCorrect()
        {
            foreach (var mic in MicsSwiss)
            {
                Assert.IsTrue(Berchtoldstag.IsBusinessHoliday(mic), mic.ToString());
            }
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_MicsSwedish_IsCorrect()
        {
            foreach (var mic in MicsSwedish)
            {
                Assert.IsTrue(MidsummerEve.IsBusinessHoliday(mic), mic.ToString());
            }
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_MicsNorwegian_IsCorrect()
        {
            foreach (var mic in MicsNorwegian)
            {
                Assert.IsTrue(ConstitutionDay.IsBusinessHoliday(mic), mic.ToString());
            }
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_MicsDanish_IsCorrect()
        {
            foreach (var mic in MicsDanish)
            {
                Assert.IsTrue(CommonPrayerDay.IsBusinessHoliday(mic), mic.ToString());
            }
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_NoHolidaysCalendar_IsCorrect()
        {
            DateTime dateOrigin = new DateTime(1980, 1, 1);
            for (int i = 0; i < 365 * 40; ++i)
            {
                DateTime date = dateOrigin.AddDays(i).Date;
                Assert.IsFalse(date.IsBusinessHoliday(BusinessDayCalendar.NoHolidays), date.ToLongDateString());
            }
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_WeekendsOnlyCalendar_IsCorrect()
        {
            Assert.IsFalse(Wednesday.IsBusinessHoliday(BusinessDayCalendar.WeekendsOnly));
            Assert.IsTrue(Sunday.IsBusinessHoliday(BusinessDayCalendar.WeekendsOnly));
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_TargetCalendar_IsCorrect()
        {
            Assert.IsFalse(Wednesday.IsBusinessHoliday(BusinessDayCalendar.Target));
            Assert.IsTrue(LabourDay.IsBusinessHoliday(BusinessDayCalendar.Target));
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_EuronextCalendar_IsCorrect()
        {
            Assert.IsFalse(Wednesday.IsBusinessHoliday(BusinessDayCalendar.Euronext));
            Assert.IsTrue(EasterMonday.IsBusinessHoliday(BusinessDayCalendar.Euronext));
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_UnitedStatesCalendar_IsCorrect()
        {
            Assert.IsFalse(Wednesday.IsBusinessHoliday(BusinessDayCalendar.UnitedStates));
            Assert.IsTrue(IndependenceDay.IsBusinessHoliday(BusinessDayCalendar.UnitedStates));
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_SwissCalendar_IsCorrect()
        {
            Assert.IsFalse(Wednesday.IsBusinessHoliday(BusinessDayCalendar.Switzerland));
            Assert.IsTrue(Berchtoldstag.IsBusinessHoliday(BusinessDayCalendar.Switzerland));
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_SwedishCalendar_IsCorrect()
        {
            Assert.IsFalse(Wednesday.IsBusinessHoliday(BusinessDayCalendar.Sweden));
            Assert.IsTrue(MidsummerEve.IsBusinessHoliday(BusinessDayCalendar.Sweden));
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_NorwegianCalendar_IsCorrect()
        {
            Assert.IsFalse(Wednesday.IsBusinessHoliday(BusinessDayCalendar.Norway));
            Assert.IsTrue(ConstitutionDay.IsBusinessHoliday(BusinessDayCalendar.Norway));
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessHoliday_DanishCalendar_IsCorrect()
        {
            Assert.IsFalse(Wednesday.IsBusinessHoliday(BusinessDayCalendar.Denmark));
            Assert.IsTrue(CommonPrayerDay.IsBusinessHoliday(BusinessDayCalendar.Denmark));
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessDay_Calendar_IsCorrect()
        {
            Assert.IsTrue(Wednesday.IsBusinessDay(BusinessDayCalendar.Target));
            Assert.IsFalse(LabourDay.IsBusinessDay(BusinessDayCalendar.Target));
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsBusinessDay_Mic_IsCorrect()
        {
            foreach (var mic in CombineAllMics())
            {
                Assert.IsTrue(Wednesday.IsBusinessDay(mic), mic.ToString());
                Assert.IsFalse(Sunday.IsBusinessDay(mic), mic.ToString());
            }
        }

        [TestMethod]
        public void BusinessDayCalendarExtensions_IsWeekend_IsCorrect()
        {
            Assert.IsFalse(Wednesday.IsWeekend());
            Assert.IsTrue(Saturday.IsWeekend());
            Assert.IsTrue(Sunday.IsWeekend());
        }

        private static List<ExchangeMic> CombineAllMics()
        {
            var mics = new List<ExchangeMic>(MicsEuronext);
            mics.AddRange(MicsDanish);
            mics.AddRange(MicsNorwegian);
            mics.AddRange(MicsNotSpecified);
            mics.AddRange(MicsSwedish);
            mics.AddRange(MicsSwiss);
            mics.AddRange(MicsUs);

            return mics;
        }
    }
}
