using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Historical.Providers.Csv;
using Mbs.Trading.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Historical.Providers.Csv
{
    [TestClass]
    public class CsvHistoricalDataScalarTests
    {
        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_Ts_ValidLines_CorrectValues()
        {
            List<Scalar> list = await EnumerateScalarAsync(new[]
            {
                "2019/01/02 09:00:27.0000000;18.29",
                "2019/01/02 09:00:32.0000000;18.202",
            });

            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 9, 0, 27), list[0].Time);
            Assert.AreEqual(18.29, list[0].Value);

            Assert.AreEqual(new DateTime(2019, 1, 2, 9, 0, 32), list[1].Time);
            Assert.AreEqual(18.202, list[1].Value);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_ExtraField_CorrectValues()
        {
            List<Scalar> list = await EnumerateScalarAsync(new[]
            {
                "2019/01/02 09:00:27.0000000;18.29;abc",
            });

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 9, 0, 27), list[0].Time);
            Assert.AreEqual(18.29, list[0].Value);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_Separator_LineParsed()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    "2019/01/02 09:00:27.0000000|18.29",
                },
                separator: "|");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 9, 0, 27), list[0].Time);
            Assert.AreEqual(18.29, list[0].Value);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_DateTimeFormat_LineParsed()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    "20190102;18.29",
                },
                dateTimeFormat: "yyyyMMdd");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Value);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_Comment_LineSkipped()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    "# abc",
                    "2019/01/02 09:00:27.0000000;18.29",
                    "#",
                },
                comment: "#");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 9, 0, 27), list[0].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_EmptyLine_LineSkipped()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    string.Empty,
                    "2019/01/02 09:00:27.0000000;18.29",
                    string.Empty,
                },
                comment: "#");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 9, 0, 27), list[0].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_BeforeStartDate_LineSkipped()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    "2019/01/02 09:00:27.0000000;18.29",
                    "2019/01/03 09:00:27.0000000;18.29",
                    "2019/01/04 09:00:27.0000000;18.29",
                    "2019/01/07 09:00:27.0000000;18.29",
                    "2019/01/08 09:00:27.0000000;18.29",
                    "2019/01/09 09:00:27.0000000;18.29",
                },
                startTicks: new DateTime(2019, 1, 7).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 7, 9, 0, 27), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 8, 9, 0, 27), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 9, 9, 0, 27), list[2].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_AfterEndDate_Completed()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    "2019/01/02 09:00:27.0000000;18.29",
                    "2019/01/03 09:00:27.0000000;18.29",
                    "2019/01/04 09:00:27.0000000;18.29",
                    "2019/01/07 09:00:27.0000000;18.29",
                    "2019/01/08 09:00:27.0000000;18.29",
                    "2019/01/09 09:00:27.0000000;18.29",
                },
                endTicks: new DateTime(2019, 1, 7).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 9, 0, 27), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 3, 9, 0, 27), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 4, 9, 0, 27), list[2].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_BetweenStartAndEndDate_CorrectRangeParsed()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    "2019/01/02 09:00:27.0000000;18.29",
                    "2019/01/03 09:00:27.0000000;18.29",
                    "2019/01/04 09:00:27.0000000;18.29",
                    "2019/01/07 09:00:27.0000000;18.29",
                    "2019/01/08 09:00:27.0000000;18.29",
                    "2019/01/09 09:00:27.0000000;18.29",
                },
                startTicks: new DateTime(2019, 1, 3).Ticks,
                endTicks: new DateTime(2019, 1, 8).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 3, 9, 0, 27), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 4, 9, 0, 27), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 7, 9, 0, 27), list[2].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_BeforeStartTime_LineSkipped()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    "2019/01/02 00:01:28.0000000;18.29",
                    "2019/01/02 00:02:29.0000000;18.29",
                    "2019/01/02 00:03:30.0000000;18.29",
                    "2019/01/02 00:04:31.0000000;18.29",
                    "2019/01/02 00:05:32.0000000;18.29",
                    "2019/01/02 00:06:33.0000000;18.29",
                },
                startTicks: new DateTime(2019, 1, 2, 0, 4, 15).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 4, 31), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 5, 32), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 6, 33), list[2].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_AfterEndTime_Completed()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    "2019/01/02 00:01:28.0000000;18.29",
                    "2019/01/02 00:02:29.0000000;18.29",
                    "2019/01/02 00:03:30.0000000;18.29",
                    "2019/01/02 00:04:31.0000000;18.29",
                    "2019/01/02 00:05:32.0000000;18.29",
                    "2019/01/02 00:06:33.0000000;18.29",
                },
                endTicks: new DateTime(2019, 1, 2, 0, 4, 15).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 1, 28), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 2, 29), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 3, 30), list[2].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_BetweenStartAndEndTime_CorrectRangeParsed()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    "2019/01/02 00:01:28.0000000;18.29",
                    "2019/01/02 00:02:29.0000000;18.29",
                    "2019/01/02 00:03:30.0000000;18.29",
                    "2019/01/02 00:04:31.0000000;18.29",
                    "2019/01/02 00:05:32.0000000;18.29",
                    "2019/01/02 00:06:33.0000000;18.29",
                },
                startTicks: new DateTime(2019, 1, 2, 0, 2, 30).Ticks,
                endTicks: new DateTime(2019, 1, 2, 0, 5, 40).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 3, 30), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 4, 31), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 5, 32), list[2].Time);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = true)]
        public async Task CsvHistoricalData_EnumerateScalarAsync_InvalidDateTime_Exception()
        {
            List<Scalar> list = await EnumerateScalarAsync(new[]
            {
                "2019/01/02;18.29;161",
            });

            Assert.IsNotNull(list);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = true)]
        public async Task CsvHistoricalData_EnumerateScalarAsync_InvalidValue_Exception()
        {
            List<Scalar> list = await EnumerateScalarAsync(new[]
            {
                "2019/01/02 00:00:00.0000000;a18.29",
            });

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_LongLine_Parsed()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    CreateComment(5119),
                    "2019/01/02 00:00:00.0000000;18.29",
                },
                comment: "#");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateScalarAsync_VeryLongLine_Parsed()
        {
            List<Scalar> list = await EnumerateScalarAsync(
                new[]
                {
                    CreateComment(5120),
                    "2019/01/02 00:00:00.0000000;18.29",
                },
                comment: "#");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
        }

        private static async Task<List<Scalar>> EnumerateScalarAsync(
            string[] lines,
            CsvColumnSet columnSet = CsvColumnSet.Ts,
            string dateTimeFormat = CsvInfo.DefaultDateTimeFormat,
            string separator = CsvInfo.DefaultSeparatorCharacter,
            string comment = CsvInfo.DefaultCommentCharacter,
            long startTicks = 0L,
            long endTicks = 3155378975999999999L)
        {
            var list = new List<Scalar>();

            var path = Path.GetTempFileName();
            File.WriteAllLines(path, lines);

            var csvInfo = new CsvInfo(path, columnSet, dateTimeFormat, TimeGranularity.Aperiodic, separator, comment);
            var csvRequest = new CsvRequest
            {
                StartDateTime = new DateTime(startTicks),
                EndDateTime = new DateTime(endTicks),
            };

            try
            {
                await foreach (var s in
                    CsvHistoricalData.EnumerateScalarAsync(csvInfo, csvRequest, CancellationToken.None))
                {
                    list.Add(s);
                }

                return list;
            }
            finally
            {
                File.Delete(path);
            }
        }

        private static string CreateComment(int length)
        {
            var sb = new StringBuilder(length);
            sb.Append('#');
            while (--length > 1)
            {
                sb.Append('a');
            }

            return sb.ToString();
        }
    }
}
