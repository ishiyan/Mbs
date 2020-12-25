using System;
using System.Globalization;
using Mbs.Trading.Holidays;

namespace Mbs.Trading.Time
{
    /// <summary>
    /// <see cref="DateTime"/> extensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTimeFormatInfo InvariantInfo = DateTimeFormatInfo.InvariantInfo;

        /// <summary>
        /// Adds time granularity units to the new instance of <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <param name="granularity">The time granularity.</param>
        /// <param name="count">The number of units.</param>
        /// <returns>A new <see cref="DateTime"/> instance.</returns>
        public static DateTime Add(this DateTime dateTime, TimeGranularity granularity, int count)
        {
            return granularity switch
            {
                TimeGranularity.Aperiodic => throw new ArgumentException("Cannot add aperiodic time granularity", nameof(granularity)),
                TimeGranularity.Second1 => dateTime.AddSeconds(count),
                TimeGranularity.Second2 => dateTime.AddSeconds(count * 2),
                TimeGranularity.Second3 => dateTime.AddSeconds(count * 3),
                TimeGranularity.Second4 => dateTime.AddSeconds(count * 4),
                TimeGranularity.Second5 => dateTime.AddSeconds(count * 5),
                TimeGranularity.Second6 => dateTime.AddSeconds(count * 6),
                TimeGranularity.Second10 => dateTime.AddSeconds(count * 10),
                TimeGranularity.Second12 => dateTime.AddSeconds(count * 12),
                TimeGranularity.Second15 => dateTime.AddSeconds(count * 15),
                TimeGranularity.Second20 => dateTime.AddSeconds(count * 20),
                TimeGranularity.Second30 => dateTime.AddSeconds(count * 30),
                TimeGranularity.Minute1 => dateTime.AddMinutes(count),
                TimeGranularity.Minute2 => dateTime.AddMinutes(count * 2),
                TimeGranularity.Minute3 => dateTime.AddMinutes(count * 3),
                TimeGranularity.Minute4 => dateTime.AddMinutes(count * 4),
                TimeGranularity.Minute5 => dateTime.AddMinutes(count * 5),
                TimeGranularity.Minute6 => dateTime.AddMinutes(count * 6),
                TimeGranularity.Minute10 => dateTime.AddMinutes(count * 10),
                TimeGranularity.Minute12 => dateTime.AddMinutes(count * 12),
                TimeGranularity.Minute15 => dateTime.AddMinutes(count * 15),
                TimeGranularity.Minute20 => dateTime.AddMinutes(count * 20),
                TimeGranularity.Minute30 => dateTime.AddMinutes(count * 30),
                TimeGranularity.Hour1 => dateTime.AddHours(count),
                TimeGranularity.Hour2 => dateTime.AddHours(count * 2),
                TimeGranularity.Hour3 => dateTime.AddHours(count * 3),
                TimeGranularity.Hour4 => dateTime.AddHours(count * 4),
                TimeGranularity.Hour6 => dateTime.AddHours(count * 6),
                TimeGranularity.Hour8 => dateTime.AddHours(count * 8),
                TimeGranularity.Hour12 => dateTime.AddHours(count * 12),
                TimeGranularity.Day1 => dateTime.AddDays(count),
                TimeGranularity.Week1 => dateTime.AddDays(count * 7),
                TimeGranularity.Week2 => dateTime.AddDays(count * 14),
                TimeGranularity.Week3 => dateTime.AddDays(count * 21),
                TimeGranularity.Month1 => dateTime.AddMonths(count),
                TimeGranularity.Month2 => dateTime.AddMonths(count * 2),
                TimeGranularity.Month3 => dateTime.AddMonths(count * 3),
                TimeGranularity.Month4 => dateTime.AddMonths(count * 4),
                TimeGranularity.Month6 => dateTime.AddMonths(count * 6),
                TimeGranularity.Year1 => dateTime.AddYears(count),
                _ => throw new ArgumentException("Unknown time granularity", nameof(granularity))
            };
        }

        /// <summary>
        /// Adds a time granularity unit to the new instance of <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>A new <see cref="DateTime"/> instance.</returns>
        public static DateTime Add(this DateTime dateTime, TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Aperiodic => throw new ArgumentException("Cannot add aperiodic time granularity", nameof(granularity)),
                TimeGranularity.Second1 => dateTime.AddSeconds(1),
                TimeGranularity.Second2 => dateTime.AddSeconds(2),
                TimeGranularity.Second3 => dateTime.AddSeconds(3),
                TimeGranularity.Second4 => dateTime.AddSeconds(4),
                TimeGranularity.Second5 => dateTime.AddSeconds(5),
                TimeGranularity.Second6 => dateTime.AddSeconds(6),
                TimeGranularity.Second10 => dateTime.AddSeconds(10),
                TimeGranularity.Second12 => dateTime.AddSeconds(12),
                TimeGranularity.Second15 => dateTime.AddSeconds(15),
                TimeGranularity.Second20 => dateTime.AddSeconds(20),
                TimeGranularity.Second30 => dateTime.AddSeconds(30),
                TimeGranularity.Minute1 => dateTime.AddMinutes(1),
                TimeGranularity.Minute2 => dateTime.AddMinutes(2),
                TimeGranularity.Minute3 => dateTime.AddMinutes(3),
                TimeGranularity.Minute4 => dateTime.AddMinutes(4),
                TimeGranularity.Minute5 => dateTime.AddMinutes(5),
                TimeGranularity.Minute6 => dateTime.AddMinutes(6),
                TimeGranularity.Minute10 => dateTime.AddMinutes(10),
                TimeGranularity.Minute12 => dateTime.AddMinutes(12),
                TimeGranularity.Minute15 => dateTime.AddMinutes(15),
                TimeGranularity.Minute20 => dateTime.AddMinutes(20),
                TimeGranularity.Minute30 => dateTime.AddMinutes(30),
                TimeGranularity.Hour1 => dateTime.AddHours(1),
                TimeGranularity.Hour2 => dateTime.AddHours(2),
                TimeGranularity.Hour3 => dateTime.AddHours(3),
                TimeGranularity.Hour4 => dateTime.AddHours(4),
                TimeGranularity.Hour6 => dateTime.AddHours(6),
                TimeGranularity.Hour8 => dateTime.AddHours(8),
                TimeGranularity.Hour12 => dateTime.AddHours(12),
                TimeGranularity.Day1 => dateTime.AddDays(1),
                TimeGranularity.Week1 => dateTime.AddDays(7),
                TimeGranularity.Week2 => dateTime.AddDays(14),
                TimeGranularity.Week3 => dateTime.AddDays(21),
                TimeGranularity.Month1 => dateTime.AddMonths(1),
                TimeGranularity.Month2 => dateTime.AddMonths(2),
                TimeGranularity.Month3 => dateTime.AddMonths(3),
                TimeGranularity.Month4 => dateTime.AddMonths(4),
                TimeGranularity.Month6 => dateTime.AddMonths(6),
                TimeGranularity.Year1 => dateTime.AddYears(1),
                _ => throw new ArgumentException("Unknown time granularity", nameof(granularity))
            };
        }

        /// <summary>
        /// Adds business days (weekends excluded, holidays included) to the new instance of <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <param name="days">The number of business days.</param>
        /// <returns>A new <see cref="DateTime"/> instance.</returns>
        public static DateTime AddBusinessDays(this DateTime dateTime, int days)
        {
            int sign = Math.Sign(days);
            int daysAbsoluteValue = sign * days;
            for (int i = 0; i < daysAbsoluteValue; ++i)
            {
                DayOfWeek dayOfWeek;
                do
                {
                    dateTime = dateTime.AddDays(sign);
                    dayOfWeek = dateTime.DayOfWeek;
                }
                while (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday);
            }

            return dateTime;
        }

        /// <summary>
        /// Adds Euronext trading days (holidays and weekends excluded) to the new instance of <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <param name="days">The number of Euronext trading days.</param>
        /// <returns>A new <see cref="DateTime"/> instance.</returns>
        public static DateTime AddEuronextTradingDays(this DateTime dateTime, int days)
        {
            int sign = Math.Sign(days);
            int daysAbsoluteValue = sign * days;
            for (int i = 0; i < daysAbsoluteValue; ++i)
            {
                do
                {
                    dateTime = dateTime.AddDays(sign);
                }
                while (dateTime.IsEuronextHoliday());
            }

            return dateTime;
        }

        /// <summary>
        /// Returns a compact string in the invariant culture that represents this date and time.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <returns>A compact string in the invariant culture that represents this date and time.</returns>
        public static string ToCompactString(this DateTime dateTime)
        {
            return dateTime.ToString(dateTime.Millisecond == 0 ? "yyyyMMdd:HHmmss" : "yyyyMMdd:HHmmss.fff", InvariantInfo);
        }

        /// <summary>
        /// Returns a compact string in the invariant culture that represents this date.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <returns>A compact string in the invariant culture that represents this date.</returns>
        public static string ToCompactDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd", InvariantInfo);
        }

        /// <summary>
        /// Returns a compact string in the invariant culture that represents this time.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <returns>A compact time string in the invariant culture that represents this time.</returns>
        public static string ToCompactTimeString(this DateTime dateTime)
        {
            return dateTime.ToString(dateTime.Millisecond == 0 ? "HHmmss" : "HHmmss.fff", InvariantInfo);
        }

        /// <summary>
        /// Returns a string in the invariant culture that represents this date and time using a given format specification.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <param name="format">The format specification.</param>
        /// <returns>A string in the invariant culture that.</returns>
        public static string ToInvariantString(this DateTime dateTime, string format)
        {
            return dateTime.ToString(format, InvariantInfo);
        }

        /// <summary>
        /// Creates a <see cref="DateTime"/> instance from a string using a given invariant format specification.
        /// The string must match the specified format exactly.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <param name="format">The format specification.</param>
        /// <returns>A <see cref="DateTime"/> instance.</returns>
        public static DateTime ParseDateTimeExact(this string text, string format)
        {
            return DateTime.ParseExact(text.Trim(), format, InvariantInfo);
        }

        /// <summary>
        /// Creates a <see cref="DateTime"/> instance from a string using a compact invariant format (yyyyMMdd, HHmmss, yyyyMMdd:HHmmss).
        /// </summary>
        /// <param name="text">The string.</param>
        /// <returns>A <see cref="DateTime"/> instance.</returns>
        public static DateTime ParseDateTimeCompact(this string text)
        {
            text = text.Trim();
            int length = text.Length;
            return length switch
            {
                0 => DateTime.MinValue,
                8 => DateTime.ParseExact(text, "yyyyMMdd", InvariantInfo),
                6 => DateTime.ParseExact(text, "HHmmss", InvariantInfo),
                _ => DateTime.ParseExact(text, "yyyyMMdd:HHmmss", InvariantInfo)
            };
        }

        /// <summary>
        /// Checks if the number of ticks is zero.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <returns>True if the number of ticks is zero.</returns>
        public static bool IsZero(this DateTime dateTime)
        {
            return dateTime.Ticks == 0L;
        }

        /// <summary>
        /// Checks if the number of ticks is not zero.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <returns>True if the number of ticks is not zero.</returns>
        public static bool IsNotZero(this DateTime dateTime)
        {
            return dateTime.Ticks != 0L;
        }
    }
}
