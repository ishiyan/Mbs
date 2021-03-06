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
    public class CsvHistoricalDataOhlcvTests
    {
        // CsvHistoricalData_EnumerateOhlcvAsync_Tohlcv_ValidLines_CorrectValues
        // CsvHistoricalData_EnumerateOhlcvAsync_Tohlc_ValidLines_CorrectValues
        // CsvHistoricalData_EnumerateOhlcvAsync_Tocv_ValidLines_CorrectValues
        // CsvHistoricalData_EnumerateOhlcvAsync_Toc_ValidLines_CorrectValues
        // CsvHistoricalData_EnumerateOhlcvAsync_ExtraField_CorrectValues
        // CsvHistoricalData_EnumerateOhlcvAsync_Separator_LineParsed
        // CsvHistoricalData_EnumerateOhlcvAsync_DateTimeFormat_LineParsed
        // CsvHistoricalData_EnumerateOhlcvAsync_Comment_LineSkipped
        // CsvHistoricalData_EnumerateOhlcvAsync_EmptyLine_LineSkipped

        // CsvHistoricalData_EnumerateOhlcvAsync_BeforeStartDate_LineSkipped
        // CsvHistoricalData_EnumerateOhlcvAsync_AfterEndDate_Completed
        // CsvHistoricalData_EnumerateOhlcvAsync_BetweenStartAndEndDate_CorrectRangeParsed
        // CsvHistoricalData_EnumerateOhlcvAsync_BeforeStartTime_LineSkipped
        // CsvHistoricalData_EnumerateOhlcvAsync_AfterEndTime_Completed
        // CsvHistoricalData_EnumerateOhlcvAsync_BetweenStartAndEndTime_CorrectRangeParsed

        // CsvHistoricalData_EnumerateOhlcvAsync_InvalidDateTime_Exception
        // CsvHistoricalData_EnumerateOhlcvAsync_InvalidOpen_Exception
        // CsvHistoricalData_EnumerateOhlcvAsync_InvalidClose_Exception
        // CsvHistoricalData_EnumerateOhlcvAsync_InvalidHigh_Exception
        // CsvHistoricalData_EnumerateOhlcvAsync_InvalidLow_Exception
        // CsvHistoricalData_EnumerateOhlcvAsync_InvalidVolume_Exception

        // CsvHistoricalData_EnumerateOhlcvAsync_LongLine_Parsed
        // CsvHistoricalData_EnumerateOhlcvAsync_VeryLongLine_Parsed
        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_Tohlcv_ValidLines_CorrectValues()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(new[]
            {
                "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;504066",
                "2019/01/03 00:00:00.0000000;18.12;18.23;17.32;17.504;1087725",
            });

            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Open);
            Assert.AreEqual(18.81, list[0].High);
            Assert.AreEqual(17.93, list[0].Low);
            Assert.AreEqual(18.706, list[0].Close);
            Assert.AreEqual(504066, list[0].Volume);

            Assert.AreEqual(new DateTime(2019, 1, 3), list[1].Time);
            Assert.AreEqual(18.12, list[1].Open);
            Assert.AreEqual(18.23, list[1].High);
            Assert.AreEqual(17.32, list[1].Low);
            Assert.AreEqual(17.504, list[1].Close);
            Assert.AreEqual(1087725, list[1].Volume);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_Tohlc_ValidLines_CorrectValues()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706",
                    "2019/01/03 00:00:00.0000000;18.12;18.23;17.32;17.504",
                },
                CsvColumnSet.Tohlc);

            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Open);
            Assert.AreEqual(18.81, list[0].High);
            Assert.AreEqual(17.93, list[0].Low);
            Assert.AreEqual(18.706, list[0].Close);
            Assert.AreEqual(0, list[0].Volume);

            Assert.AreEqual(new DateTime(2019, 1, 3), list[1].Time);
            Assert.AreEqual(18.12, list[1].Open);
            Assert.AreEqual(18.23, list[1].High);
            Assert.AreEqual(17.32, list[1].Low);
            Assert.AreEqual(17.504, list[1].Close);
            Assert.AreEqual(0, list[1].Volume);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_Tocv_ValidLines_CorrectValues()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:00:00.0000000;18.29;18.706;504066",
                    "2019/01/03 00:00:00.0000000;18.12;17.504;1087725",
                },
                CsvColumnSet.Tocv);

            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Open);
            Assert.AreEqual(18.706, list[0].High);
            Assert.AreEqual(18.29, list[0].Low);
            Assert.AreEqual(18.706, list[0].Close);
            Assert.AreEqual(504066, list[0].Volume);

            Assert.AreEqual(new DateTime(2019, 1, 3), list[1].Time);
            Assert.AreEqual(18.12, list[1].Open);
            Assert.AreEqual(18.12, list[1].High);
            Assert.AreEqual(17.504, list[1].Low);
            Assert.AreEqual(17.504, list[1].Close);
            Assert.AreEqual(1087725, list[1].Volume);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_Toc_ValidLines_CorrectValues()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:00:00.0000000;18.29;18.706",
                    "2019/01/03 00:00:00.0000000;18.12;17.504",
                },
                CsvColumnSet.Toc);

            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Open);
            Assert.AreEqual(18.706, list[0].High);
            Assert.AreEqual(18.29, list[0].Low);
            Assert.AreEqual(18.706, list[0].Close);
            Assert.AreEqual(0, list[0].Volume);

            Assert.AreEqual(new DateTime(2019, 1, 3), list[1].Time);
            Assert.AreEqual(18.12, list[1].Open);
            Assert.AreEqual(18.12, list[1].High);
            Assert.AreEqual(17.504, list[1].Low);
            Assert.AreEqual(17.504, list[1].Close);
            Assert.AreEqual(0, list[1].Volume);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_ExtraField_CorrectValues()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(new[]
            {
                "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;504066;abc",
            });

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Open);
            Assert.AreEqual(18.81, list[0].High);
            Assert.AreEqual(17.93, list[0].Low);
            Assert.AreEqual(18.706, list[0].Close);
            Assert.AreEqual(504066, list[0].Volume);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_Separator_LineParsed()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:00:00.0000000|18.29|18.81|17.93|18.706|504066",
                },
                separator: "|");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Open);
            Assert.AreEqual(18.81, list[0].High);
            Assert.AreEqual(17.93, list[0].Low);
            Assert.AreEqual(18.706, list[0].Close);
            Assert.AreEqual(504066, list[0].Volume);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_DateTimeFormat_LineParsed()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "20190102;18.29;18.81;17.93;18.706;504066",
                },
                dateTimeFormat: "yyyyMMdd");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Open);
            Assert.AreEqual(18.81, list[0].High);
            Assert.AreEqual(17.93, list[0].Low);
            Assert.AreEqual(18.706, list[0].Close);
            Assert.AreEqual(504066, list[0].Volume);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_Comment_LineSkipped()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "# abc",
                    "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;504066",
                    "#",
                },
                comment: "#");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Open);
            Assert.AreEqual(18.81, list[0].High);
            Assert.AreEqual(17.93, list[0].Low);
            Assert.AreEqual(18.706, list[0].Close);
            Assert.AreEqual(504066, list[0].Volume);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_EmptyLine_LineSkipped()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(new[]
            {
                string.Empty,
                "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;504066",
                string.Empty,
            });

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(18.29, list[0].Open);
            Assert.AreEqual(18.81, list[0].High);
            Assert.AreEqual(17.93, list[0].Low);
            Assert.AreEqual(18.706, list[0].Close);
            Assert.AreEqual(504066, list[0].Volume);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_BeforeStartDate_LineSkipped()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;504066",
                    "2019/01/03 00:00:00.0000000;18.12;18.23;17.32;17.504;1087725",
                    "2019/01/04 00:00:00.0000000;17.5;17.98;17.45;17.752;509279",
                    "2019/01/07 00:00:00.0000000;18.086;18.858;18.014;18.75;758588",
                    "2019/01/08 00:00:00.0000000;18.59;19.162;18.35;18.7;762024",
                    "2019/01/09 00:00:00.0000000;19;19.01;18.54;18.594;604749",
                },
                startTicks: new DateTime(2019, 1, 7).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 7), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 8), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 9), list[2].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_AfterEndDate_Completed()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;504066",
                    "2019/01/03 00:00:00.0000000;18.12;18.23;17.32;17.504;1087725",
                    "2019/01/04 00:00:00.0000000;17.5;17.98;17.45;17.752;509279",
                    "2019/01/07 00:00:00.0000000;18.086;18.858;18.014;18.75;758588",
                    "2019/01/08 00:00:00.0000000;18.59;19.162;18.35;18.7;762024",
                    "2019/01/09 00:00:00.0000000;19;19.01;18.54;18.594;604749",
                },
                endTicks: new DateTime(2019, 1, 7).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(4, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 3), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 4), list[2].Time);
            Assert.AreEqual(new DateTime(2019, 1, 7), list[3].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_BetweenStartAndEndDate_CorrectRangeParsed()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;504066",
                    "2019/01/03 00:00:00.0000000;18.12;18.23;17.32;17.504;1087725",
                    "2019/01/04 00:00:00.0000000;17.5;17.98;17.45;17.752;509279",
                    "2019/01/07 00:00:00.0000000;18.086;18.858;18.014;18.75;758588",
                    "2019/01/08 00:00:00.0000000;18.59;19.162;18.35;18.7;762024",
                    "2019/01/09 00:00:00.0000000;19;19.01;18.54;18.594;604749",
                },
                startTicks: new DateTime(2019, 1, 3).Ticks,
                endTicks: new DateTime(2019, 1, 8).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(4, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 3), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 4), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 7), list[2].Time);
            Assert.AreEqual(new DateTime(2019, 1, 8), list[3].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_BeforeStartTime_LineSkipped()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:01:28.0000000;18.29;18.81;17.93;18.706;504066",
                    "2019/01/02 00:02:29.0000000;18.12;18.23;17.32;17.504;1087725",
                    "2019/01/02 00:03:30.0000000;17.5;17.98;17.45;17.752;509279",
                    "2019/01/02 00:04:31.0000000;18.086;18.858;18.014;18.75;758588",
                    "2019/01/02 00:05:32.0000000;18.59;19.162;18.35;18.7;762024",
                    "2019/01/02 00:06:33.0000000;19;19.01;18.54;18.594;604749",
                },
                startTicks: new DateTime(2019, 1, 2, 0, 4, 15).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 4, 31), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 5, 32), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 6, 33), list[2].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_AfterEndTime_Completed()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:01:28.0000000;18.29;18.81;17.93;18.706;504066",
                    "2019/01/02 00:02:29.0000000;18.12;18.23;17.32;17.504;1087725",
                    "2019/01/02 00:03:30.0000000;17.5;17.98;17.45;17.752;509279",
                    "2019/01/02 00:04:31.0000000;18.086;18.858;18.014;18.75;758588",
                    "2019/01/02 00:05:32.0000000;18.59;19.162;18.35;18.7;762024",
                    "2019/01/02 00:06:33.0000000;19;19.01;18.54;18.594;604749",
                },
                endTicks: new DateTime(2019, 1, 2, 0, 4, 15).Ticks);

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 1, 28), list[0].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 2, 29), list[1].Time);
            Assert.AreEqual(new DateTime(2019, 1, 2, 0, 3, 30), list[2].Time);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_BetweenStartAndEndTime_CorrectRangeParsed()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    "2019/01/02 00:01:28.0000000;18.29;18.81;17.93;18.706;504066",
                    "2019/01/02 00:02:29.0000000;18.12;18.23;17.32;17.504;1087725",
                    "2019/01/02 00:03:30.0000000;17.5;17.98;17.45;17.752;509279",
                    "2019/01/02 00:04:31.0000000;18.086;18.858;18.014;18.75;758588",
                    "2019/01/02 00:05:32.0000000;18.59;19.162;18.35;18.7;762024",
                    "2019/01/02 00:06:33.0000000;19;19.01;18.54;18.594;604749",
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
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_InvalidDateTime_Exception()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(new[]
            {
                "2019/01/02;18.81;17.93;18.706;504066",
            });

            Assert.IsNotNull(list);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = true)]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_InvalidOpen_Exception()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(new[]
            {
                "2019/01/02 00:00:00.0000000; 18.29;18.81;17.93;18.706;504066",
            });

            Assert.IsNotNull(list);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = true)]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_InvalidHigh_Exception()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(new[]
            {
                "2019/01/02 00:00:00.0000000;18.29;18e.81;17.93;18.706;504066",
            });

            Assert.IsNotNull(list);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = true)]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_InvalidLow_Exception()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(new[]
            {
                "2019/01/02 00:00:00.0000000;18.29;18.81;17.g93;18.706;504066",
            });

            Assert.IsNotNull(list);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = true)]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_InvalidClose_Exception()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(new[]
            {
                "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706 ;504066",
            });

            Assert.IsNotNull(list);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = true)]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_InvalidVolume_Exception()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(new[]
            {
                "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;5e+04066",
            });

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_LongLine_Parsed()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    CreateComment(5119),
                    "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;504066",
                },
                comment: "#");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public async Task CsvHistoricalData_EnumerateOhlcvAsync_VeryLongLine_Parsed()
        {
            List<Ohlcv> list = await EnumerateOhlcvAsync(
                new[]
                {
                    CreateComment(5120),
                    "2019/01/02 00:00:00.0000000;18.29;18.81;17.93;18.706;504066",
                },
                comment: "#");

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
        }

        private static async Task<List<Ohlcv>> EnumerateOhlcvAsync(
            string[] lines,
            CsvColumnSet columnSet = CsvColumnSet.Tohlcv,
            string dateTimeFormat = CsvInfo.DefaultDateTimeFormat,
            TimeGranularity timeGranularity = CsvInfo.DefaultTimeGranularity, // not used ?
            string separator = CsvInfo.DefaultSeparatorCharacter,
            string comment = CsvInfo.DefaultCommentCharacter,
            long startTicks = 0L,
            long endTicks = 3155378975999999999L)
        {
            var list = new List<Ohlcv>();

            var path = Path.GetTempFileName();
            File.WriteAllLines(path, lines);

            var csvInfo = new CsvInfo(path, columnSet, dateTimeFormat, timeGranularity, separator, comment);
            var csvRequest = new CsvRequest
            {
                StartDateTime = new DateTime(startTicks),
                EndDateTime = new DateTime(endTicks),
            };

            try
            {
                await foreach (var ohlcv in
                    CsvHistoricalData.EnumerateOhlcvAsync(csvInfo, csvRequest, CancellationToken.None))
                {
                    list.Add(ohlcv);
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
