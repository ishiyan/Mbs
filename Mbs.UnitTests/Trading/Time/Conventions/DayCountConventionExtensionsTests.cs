using System;
using Mbs.Trading.Time.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Time.Conventions
{
    [TestClass]
    public class DayCountConventionExtensionsTests
    {
        /// <summary>
        /// A test for YearFractionTo ActualActual-Isma/Isda/Afb.
        /// Taken from http://www.isda.org/c_and_a/pdf/mktc1198.pdf,
        /// "EMU AND MARKET CONVENTIONS: RECENT DEVELOPMENTS".
        /// </summary>
        [TestMethod]
        public void DayCountConventionExtensions_YearFractionTo_Mktc1198Test_CorrectResults()
        {
            const double epsilon = 1.0e-15;

            // First example, page 3 of 14.
            var start = new DateTime(2003, 11, 1);
            var end = new DateTime(2004, 5, 1);

            // 61/365 + 121/366
            Assert.AreEqual(
                0.49772438056740774010030690919979,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsda),
                epsilon,
                "First example, page 3, ISDA");

            // 182/(182*2)
            Assert.AreEqual(
                0.500000000000,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsma),
                epsilon,
                "First example, page 3, ISMA");

            // 182/366
            Assert.AreEqual(
                0.49726775956284153005464480874317,
                start.YearFractionTo(end, DayCountConvention.ActualActualAfb),
                epsilon,
                "First example, page 3, AFB");

            // Short first calculation period (first period), pages 4-5 of 14.
            start = new DateTime(1999, 2, 1);
            end = new DateTime(1999, 7, 1);

            // 150/365
            Assert.AreEqual(
                0.4109589041095890410958904109589,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsda),
                epsilon,
                "Short first calculation period (first period), pages 4-5, ISDA");

            // 150/(365*1)
            var referenceStart = new DateTime(1999, 7, 1);
            var referenceEnd = new DateTime(1999, 7, 1);
            Assert.AreEqual(
                0.4109589041095890410958904109589,
                start.YearFractionTo(end, referenceStart, referenceEnd, DayCountConvention.ActualActualIsma),
                epsilon,
                "Short first calculation period (first period), pages 4-5, ISMA");

            // 150/365
            Assert.AreEqual(
                0.4109589041095890410958904109589,
                start.YearFractionTo(end, DayCountConvention.ActualActualAfb),
                epsilon,
                "Short first calculation period (first period), pages 4-5, AFB");

            // Short first calculation period (second period), pages 4-5 of 14.
            start = new DateTime(1999, 7, 1);
            end = new DateTime(2000, 7, 1);

            // (184/365) + (182/366)
            Assert.AreEqual(
                1.0013773486039374204656037128528,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsda),
                epsilon,
                "Short first calculation period (second period), pages 4-5, ISDA");

            // 366/(366*1)
            Assert.AreEqual(
                1.000000000000,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsma),
                epsilon,
                "Short first calculation period (second period), pages 4-5, ISMA");

            // 366/(366*1)
            Assert.AreEqual(
                1.000000000000,
                start.YearFractionTo(end, DayCountConvention.ActualActualAfb),
                epsilon,
                "Short first calculation period (second period), pages 4-5, AFB");

            // Long first calculation period (first period), page 6 of 14.
            start = new DateTime(2002, 8, 15);
            end = new DateTime(2003, 7, 15);

            // 334/365
            Assert.AreEqual(
                0.91506849315068493150684931506849,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsda),
                epsilon,
                "Long first calculation period (first period), page 6, ISDA");

            // 181/(181*2) + 153/(184*2)
            referenceStart = new DateTime(2003, 1, 15);
            referenceEnd = new DateTime(2003, 7, 15);
            Assert.AreEqual(
                0.91576086956521739130434782608696,
                start.YearFractionTo(end, referenceStart, referenceEnd, DayCountConvention.ActualActualIsma),
                epsilon,
                "Long first calculation period (first period), page 6, ISMA");

            // 334/365
            Assert.AreEqual(
                0.91506849315068493150684931506849,
                start.YearFractionTo(end, DayCountConvention.ActualActualAfb),
                epsilon,
                "Long first calculation period (first period), page 6, AFB");

            // Long first calculation period (second period), page 6 of 14.
            // Warning: the ISDA case is in disagreement with mktc1198.pdf.
            start = new DateTime(2003, 7, 15);
            end = new DateTime(2004, 1, 15);

            // 184/365
            Assert.AreEqual(
                0.50410958904109589041095890410959,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsda),
                1.0e-3,
                "Long first calculation period (second period), page 6, ISDA");

            // 184/(184*2)
            Assert.AreEqual(
                0.500000000000,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsma),
                epsilon,
                "Long first calculation period (second period), page 6, ISMA");

            // 184/365
            Assert.AreEqual(
                0.50410958904109589041095890410959,
                start.YearFractionTo(end, DayCountConvention.ActualActualAfb),
                1.0e-3,
                "Long first calculation period (second period), page 6, AFB");

            // Short final calculation period (penultimate period), page 7 of 14.
            start = new DateTime(1999, 7, 30);
            end = new DateTime(2000, 1, 30);

            // (155/365) + (29/366)
            Assert.AreEqual(
                0.50389250692417097088105397110562,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsda),
                epsilon,
                "Short final calculation period (penultimate period), page 7, ISDA");

            // 184/(184*2)
            Assert.AreEqual(
                0.500000000000,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsma),
                epsilon,
                "Short final calculation period (penultimate period), page 7, ISMA");

            // 184/365
            Assert.AreEqual(
                0.50410958904109589041095890410959,
                start.YearFractionTo(end, DayCountConvention.ActualActualAfb),
                epsilon,
                "Short final calculation period (penultimate period), page 7, AFB");

            // Short final calculation period (final period), page 8 of 14.
            start = new DateTime(2000, 1, 30);
            end = new DateTime(2000, 6, 30);

            // 152/366
            Assert.AreEqual(
                0.41530054644808743169398907103825,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsda),
                epsilon,
                "Short final calculation period (final period), page 8, ISDA");

            // 152/(182*2)
            referenceEnd = new DateTime(2000, 6, 30);
            Assert.AreEqual(
                0.41758241758241758241758241758242,
                start.YearFractionTo(end, start, referenceEnd, DayCountConvention.ActualActualIsma),
                1.0e-3,
                "Short final calculation period (final period), page 8, ISMA");

            // 152/366
            Assert.AreEqual(
                0.41530054644808743169398907103825,
                start.YearFractionTo(end, DayCountConvention.ActualActualAfb),
                1.0e-10,
                "Short final calculation period (final period), page 8, AFB");

            // Long final calculation period, page 8 of 14.
            start = new DateTime(1999, 11, 30);
            end = new DateTime(2000, 4, 30);

            // (32/365) + (120/366)
            Assert.AreEqual(
                0.41554008533572872220974623849091,
                start.YearFractionTo(end, DayCountConvention.ActualActualIsda),
                epsilon,
                "Long final calculation period, page 8, ISDA");

            // 91/(91*4) + 61/(92*4)
            referenceEnd = new DateTime(2000, 6, 30);
            Assert.AreEqual(
                0.41576086956521739130434782608696,
                start.YearFractionTo(end, start, referenceEnd, DayCountConvention.ActualActualIsma),
                1.0e-3,
                "Long final calculation period, page 8, ISMA");

            // 152/366
            Assert.AreEqual(
                0.41530054644808743169398907103825,
                start.YearFractionTo(end, DayCountConvention.ActualActualAfb),
                epsilon,
                "Long final calculation period, page 8, AFB");
        }

        /// <summary>
        /// A sanity test for YearFractionTo. Taken from http://jfin.org/wp/.
        /// </summary>
        [TestMethod]
        public void DayCountConventionExtensions_YearFractionTo_SanityTest_CorrectResults()
        {
            var start = new DateTime(2011, 1, 1);
            var endQuarter = new DateTime(2011, 4, 1);
            var endSemi = new DateTime(2011, 7, 1);
            var endAnnual = new DateTime(2012, 1, 1);
            const double epsilon1 = 0.01;
            const double epsilon2 = 0.02;
            const double epsilon4 = 0.04;

            // Actual360.
            Assert.AreEqual(0.25, start.YearFractionTo(endQuarter, DayCountConvention.Actual360), epsilon1);
            Assert.AreEqual(0.50, start.YearFractionTo(endSemi, DayCountConvention.Actual360), epsilon2);
            Assert.AreEqual(1.00, start.YearFractionTo(endAnnual, DayCountConvention.Actual360), epsilon4);

            // Actual365Fixed.
            Assert.AreEqual(0.25, start.YearFractionTo(endQuarter, DayCountConvention.Actual365Fixed), epsilon1);
            Assert.AreEqual(0.50, start.YearFractionTo(endSemi, DayCountConvention.Actual365Fixed), epsilon2);
            Assert.AreEqual(1.00, start.YearFractionTo(endAnnual, DayCountConvention.Actual365Fixed), epsilon4);

            // ActualActualAfb.
            Assert.AreEqual(0.25, start.YearFractionTo(endQuarter, DayCountConvention.ActualActualAfb), epsilon1);
            Assert.AreEqual(0.50, start.YearFractionTo(endSemi, DayCountConvention.ActualActualAfb), epsilon2);
            Assert.AreEqual(1.00, start.YearFractionTo(endAnnual, DayCountConvention.ActualActualAfb), epsilon4);

            // ActualActualIsda.
            Assert.AreEqual(0.25, start.YearFractionTo(endQuarter, DayCountConvention.ActualActualIsda), epsilon1);
            Assert.AreEqual(0.50, start.YearFractionTo(endSemi, DayCountConvention.ActualActualIsda), epsilon2);
            Assert.AreEqual(1.00, start.YearFractionTo(endAnnual, DayCountConvention.ActualActualIsda), epsilon4);

            // ActualActualIsma.
            Assert.AreEqual(0.25, start.YearFractionTo(endQuarter, DayCountConvention.ActualActualIsma), epsilon1);
            Assert.AreEqual(0.50, start.YearFractionTo(endSemi, DayCountConvention.ActualActualIsma), epsilon2);
            Assert.AreEqual(1.00, start.YearFractionTo(endAnnual, DayCountConvention.ActualActualIsma), epsilon4);

            // Thirty360European.
            Assert.AreEqual(0.25, start.YearFractionTo(endQuarter, DayCountConvention.Thirty360European), epsilon1);
            Assert.AreEqual(0.50, start.YearFractionTo(endSemi, DayCountConvention.Thirty360European), epsilon2);
            Assert.AreEqual(1.00, start.YearFractionTo(endAnnual, DayCountConvention.Thirty360European), epsilon4);

            // Thirty360American.
            Assert.AreEqual(0.25, start.YearFractionTo(endQuarter, DayCountConvention.Thirty360American), epsilon1);
            Assert.AreEqual(0.50, start.YearFractionTo(endSemi, DayCountConvention.Thirty360American), epsilon2);
            Assert.AreEqual(1.00, start.YearFractionTo(endAnnual, DayCountConvention.Thirty360American), epsilon4);

            // Thirty360German.
            Assert.AreEqual(0.25, start.YearFractionTo(endQuarter, DayCountConvention.Thirty360German), epsilon1);
            Assert.AreEqual(0.50, start.YearFractionTo(endSemi, DayCountConvention.Thirty360German), epsilon2);
            Assert.AreEqual(1.00, start.YearFractionTo(endAnnual, DayCountConvention.Thirty360German), epsilon4);

            // Actual365L.
            Assert.AreEqual(0.25, start.YearFractionTo(endQuarter, DayCountConvention.Actual365L), epsilon1);
            Assert.AreEqual(0.50, start.YearFractionTo(endSemi, DayCountConvention.Actual365L), epsilon2);
            Assert.AreEqual(1.00, start.YearFractionTo(endAnnual, DayCountConvention.Actual365L), epsilon4);
        }

        /// <summary>
        /// A Maple (package: Finance) test for YearFractionTo.
        /// Taken from http://www.maplesoft.com/support/help/Maple/view.aspx?path=Finance/DayCounters.
        /// </summary>
        [TestMethod]
        public void DayCountConventionExtensions_YearFractionTo_MapleTest_CorrectResults()
        {
            const double epsilon = 1.0e-9;

            Assert.AreEqual(
                0.4958904110,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 7, 1), DayCountConvention.ActualActualIsda),
                epsilon);
            Assert.AreEqual(
                0.3005464481,
                new DateTime(2008, 1, 1).YearFractionTo(new DateTime(2008, 4, 20), DayCountConvention.ActualActualIsda),
                epsilon);
            Assert.AreEqual(
                0.6994535519,
                new DateTime(2008, 4, 20).YearFractionTo(new DateTime(2009, 1, 1), DayCountConvention.ActualActualIsda),
                epsilon);

            Assert.AreEqual(
                0.5000000000,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 7, 1), DayCountConvention.ActualActualIsma),
                epsilon);
            Assert.AreEqual(
                0.3333333333,
                new DateTime(2008, 1, 1).YearFractionTo(new DateTime(2008, 4, 20), DayCountConvention.ActualActualIsma),
                epsilon);
            Assert.AreEqual(
                0.2500000000,
                new DateTime(2008, 1, 1).YearFractionTo(new DateTime(2008, 4, 1), DayCountConvention.ActualActualIsma),
                epsilon);

            Assert.AreEqual(
                0.4958904110,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 7, 1), DayCountConvention.ActualActualAfb),
                epsilon);
            Assert.AreEqual(
                0.3005464481,
                new DateTime(2008, 1, 1).YearFractionTo(new DateTime(2008, 4, 20), DayCountConvention.ActualActualAfb),
                epsilon);
            Assert.AreEqual(
                0.7013698630,
                new DateTime(2008, 4, 20).YearFractionTo(new DateTime(2009, 1, 1), DayCountConvention.ActualActualAfb),
                epsilon);

            Assert.AreEqual(
                0.8444444444,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 11, 1), DayCountConvention.Actual360),
                epsilon);
            Assert.AreEqual(
                1.0194444444,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2007, 1, 3), DayCountConvention.Actual360),
                epsilon);
            Assert.AreEqual(
                1.0138888889,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2007, 1, 1), DayCountConvention.Actual360),
                epsilon);

            Assert.AreEqual(
                1.002739726,
                new DateTime(2008, 1, 1).YearFractionTo(new DateTime(2009, 1, 1), DayCountConvention.Actual365Fixed),
                epsilon);

            Assert.AreEqual(
                0.833333333,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 11, 1), DayCountConvention.Thirty360American),
                epsilon);
            Assert.AreEqual(
                0.833333333,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 10, 31), DayCountConvention.Thirty360American),
                epsilon);
            Assert.AreEqual(
                0.830555555,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 10, 30), DayCountConvention.Thirty360American),
                epsilon);
            Assert.AreEqual(
                0.752777777,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 10, 2), DayCountConvention.Thirty360American),
                epsilon);

            Assert.AreEqual(
                0.833333333,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 11, 1), DayCountConvention.Thirty360European),
                epsilon);
            Assert.AreEqual(
                0.830555555,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 10, 31), DayCountConvention.Thirty360European),
                epsilon);
            Assert.AreEqual(
                0.830555555,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 10, 30), DayCountConvention.Thirty360European),
                epsilon);
            Assert.AreEqual(
                0.158333333,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 2, 28), DayCountConvention.Thirty360European),
                epsilon);
            Assert.AreEqual(
                0.166666666,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 3, 1), DayCountConvention.Thirty360European),
                epsilon);

            Assert.AreEqual(
                0.155555555,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 2, 27), DayCountConvention.Thirty360German),
                epsilon);
            Assert.AreEqual(
                0.163888888,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 2, 28), DayCountConvention.Thirty360German),
                epsilon);
            Assert.AreEqual(
                0.166666666,
                new DateTime(2006, 1, 1).YearFractionTo(new DateTime(2006, 3, 1), DayCountConvention.Thirty360German),
                epsilon);
        }

        /// <summary>
        /// A Maple (package: Finance) test for DayCountTo.
        /// Taken from http://www.maplesoft.com/support/help/Maple/view.aspx?path=Finance/DayCounters.
        /// </summary>
        [TestMethod]
        public void DayCountConventionExtensions_DayCountTo_MapleTest_CorrectResults()
        {
            Assert.AreEqual(
                181,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 7, 1), DayCountConvention.ActualActualIsda));
            Assert.AreEqual(
                110,
                new DateTime(2008, 1, 1).DayCountTo(new DateTime(2008, 4, 20), DayCountConvention.ActualActualIsda));
            Assert.AreEqual(
                256,
                new DateTime(2008, 4, 20).DayCountTo(new DateTime(2009, 1, 1), DayCountConvention.ActualActualIsda));

            Assert.AreEqual(
                181,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 7, 1), DayCountConvention.ActualActualIsma));
            Assert.AreEqual(
                110,
                new DateTime(2008, 1, 1).DayCountTo(new DateTime(2008, 4, 20), DayCountConvention.ActualActualIsma));
            Assert.AreEqual(
                91,
                new DateTime(2008, 1, 1).DayCountTo(new DateTime(2008, 4, 1), DayCountConvention.ActualActualIsma));

            Assert.AreEqual(
                181,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 7, 1), DayCountConvention.ActualActualAfb));
            Assert.AreEqual(
                110,
                new DateTime(2008, 1, 1).DayCountTo(new DateTime(2008, 4, 20), DayCountConvention.ActualActualAfb));
            Assert.AreEqual(
                256,
                new DateTime(2008, 4, 20).DayCountTo(new DateTime(2009, 1, 1), DayCountConvention.ActualActualAfb));

            Assert.AreEqual(
                367,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2007, 1, 3), DayCountConvention.Actual360));
            Assert.AreEqual(
                365,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2007, 1, 1), DayCountConvention.Actual360));

            Assert.AreEqual(
                366,
                new DateTime(2008, 1, 1).DayCountTo(new DateTime(2009, 1, 1), DayCountConvention.Actual365Fixed));

            Assert.AreEqual(
                300,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 11, 1), DayCountConvention.Thirty360American));
            Assert.AreEqual(
                300,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 10, 31), DayCountConvention.Thirty360American));
            Assert.AreEqual(
                299,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 10, 30), DayCountConvention.Thirty360American));
            Assert.AreEqual(
                271,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 10, 2), DayCountConvention.Thirty360American));

            Assert.AreEqual(
                300,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 11, 1), DayCountConvention.Thirty360European));
            Assert.AreEqual(
                299,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 10, 31), DayCountConvention.Thirty360European));
            Assert.AreEqual(
                299,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 10, 30), DayCountConvention.Thirty360European));
            Assert.AreEqual(
                57,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 2, 28), DayCountConvention.Thirty360European));
            Assert.AreEqual(
                60,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 3, 1), DayCountConvention.Thirty360European));

            Assert.AreEqual(
                56,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 2, 27), DayCountConvention.Thirty360German));
            Assert.AreEqual(
                59,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 2, 28), DayCountConvention.Thirty360German));
            Assert.AreEqual(
                60,
                new DateTime(2006, 1, 1).DayCountTo(new DateTime(2006, 3, 1), DayCountConvention.Thirty360German));
        }

        [TestMethod]
        public void DayCountConventionExtensions_DayFractionTo_Intraday_CorrectResults()
        {
            var start = new DateTime(2006, 1, 1, 6, 0, 0);
            var end = new DateTime(2006, 1, 1, 12, 0, 0);

            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.Thirty360American), "06-12, Thirty360American");
            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.Thirty360German), "06-12, Thirty360German");
            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.Thirty360European), "06-12, Thirty360European");
            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.ActualActualIsma), "06-12, ActualActualIsma");
            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.ActualActualAfb), "06-12, ActualActualAfb");
            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.ActualActualIsda), "06-12, ActualActualIsda");
            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.Actual365Fixed), "06-12, Actual365Fixed");
            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.Actual360), "06-12, Actual360");
            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.Actual365L), "06-12, Actual365L");
            Assert.AreEqual(0.25, start.DayFractionTo(end, DayCountConvention.Theoretical), "06-12, Theoretical");

            start = new DateTime(2006, 1, 1, 0, 0, 0);

            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.Thirty360American), "00-12, Thirty360American");
            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.Thirty360German), "00-12, Thirty360German");
            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.Thirty360European), "00-12, Thirty360European");
            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.ActualActualIsma), "00-12, ActualActualIsma");
            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.ActualActualAfb), "00-12, ActualActualAfb");
            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.ActualActualIsda), "00-12, ActualActualIsda");
            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.Actual365Fixed), "00-12, Actual365Fixed");
            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.Actual360), "00-12, Actual360");
            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.Actual365L), "00-12, Actual365L");
            Assert.AreEqual(0.5, start.DayFractionTo(end, DayCountConvention.Theoretical), "00-12, Theoretical");
        }

        [TestMethod]
        public void DayCountConventionExtensions_DayFractionTo_MoreThanDay_CorrectResults()
        {
            var start = new DateTime(2006, 1, 1, 6, 0, 0);
            var end = new DateTime(2006, 1, 2, 12, 0, 0);

            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.Thirty360American), "1.25, Thirty360American");
            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.Thirty360German), "1.25, Thirty360German");
            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.Thirty360European), "1.25, Thirty360European");
            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.ActualActualIsma), "1.25, ActualActualIsma");
            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.ActualActualAfb), "1.25, ActualActualAfb");
            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.ActualActualIsda), "1.25, ActualActualIsda");
            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.Actual365Fixed), "1.25, Actual365Fixed");
            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.Actual360), "1.25, Actual360");
            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.Actual365L), "1.25, Actual365L");
            Assert.AreEqual(1.25, start.DayFractionTo(end, DayCountConvention.Theoretical), "1.25, Theoretical");

            start = new DateTime(2006, 1, 1, 0, 0, 0);

            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.Thirty360American), "1.5, Thirty360American");
            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.Thirty360German), "1.5, Thirty360German");
            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.Thirty360European), "1.5, Thirty360European");
            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.ActualActualIsma), "1.5, ActualActualIsma");
            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.ActualActualAfb), "1.5, ActualActualAfb");
            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.ActualActualIsda), "1.5, ActualActualIsda");
            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.Actual365Fixed), "1.5, Actual365Fixed");
            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.Actual360), "1.5, Actual360");
            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.Actual365L), "1.5, Actual365L");
            Assert.AreEqual(1.5, start.DayFractionTo(end, DayCountConvention.Theoretical), "1.5, Theoretical");
        }
    }
}
