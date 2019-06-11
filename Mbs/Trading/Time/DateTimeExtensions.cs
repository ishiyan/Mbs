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
            switch (granularity)
            {
                case TimeGranularity.Aperiodic:
                    throw new ArgumentException("Cannot add aperiodic time granularities", nameof(granularity));

                case TimeGranularity.Second1:
                    return dateTime.AddSeconds(count);

                case TimeGranularity.Second2:
                    return dateTime.AddSeconds(count * 2);

                case TimeGranularity.Second3:
                    return dateTime.AddSeconds(count * 3);

                case TimeGranularity.Second4:
                    return dateTime.AddSeconds(count * 4);

                case TimeGranularity.Second5:
                    return dateTime.AddSeconds(count * 5);

                case TimeGranularity.Second6:
                    return dateTime.AddSeconds(count * 6);

                case TimeGranularity.Second10:
                    return dateTime.AddSeconds(count * 10);

                case TimeGranularity.Second12:
                    return dateTime.AddSeconds(count * 12);

                case TimeGranularity.Second15:
                    return dateTime.AddSeconds(count * 15);

                case TimeGranularity.Second20:
                    return dateTime.AddSeconds(count * 20);

                case TimeGranularity.Second30:
                    return dateTime.AddSeconds(count * 30);

                case TimeGranularity.Minute1:
                    return dateTime.AddMinutes(count);

                case TimeGranularity.Minute2:
                    return dateTime.AddMinutes(count * 2);

                case TimeGranularity.Minute3:
                    return dateTime.AddMinutes(count * 3);

                case TimeGranularity.Minute4:
                    return dateTime.AddMinutes(count * 4);

                case TimeGranularity.Minute5:
                    return dateTime.AddMinutes(count * 5);

                case TimeGranularity.Minute6:
                    return dateTime.AddMinutes(count * 6);

                case TimeGranularity.Minute10:
                    return dateTime.AddMinutes(count * 10);

                case TimeGranularity.Minute12:
                    return dateTime.AddMinutes(count * 12);

                case TimeGranularity.Minute15:
                    return dateTime.AddMinutes(count * 15);

                case TimeGranularity.Minute20:
                    return dateTime.AddMinutes(count * 20);

                case TimeGranularity.Minute30:
                    return dateTime.AddMinutes(count * 30);

                case TimeGranularity.Hour1:
                    return dateTime.AddHours(count);

                case TimeGranularity.Hour2:
                    return dateTime.AddHours(count * 2);

                case TimeGranularity.Hour3:
                    return dateTime.AddHours(count * 3);

                case TimeGranularity.Hour4:
                    return dateTime.AddHours(count * 4);

                case TimeGranularity.Hour6:
                    return dateTime.AddHours(count * 6);

                case TimeGranularity.Hour8:
                    return dateTime.AddHours(count * 8);

                case TimeGranularity.Hour12:
                    return dateTime.AddHours(count * 12);

                case TimeGranularity.Day1:
                    return dateTime.AddDays(count);

                case TimeGranularity.Week1:
                    return dateTime.AddDays(count * 7);

                case TimeGranularity.Week2:
                    return dateTime.AddDays(count * 14);

                case TimeGranularity.Week3:
                    return dateTime.AddDays(count * 21);

                case TimeGranularity.Month1:
                    return dateTime.AddMonths(count);

                case TimeGranularity.Month2:
                    return dateTime.AddMonths(count * 2);

                case TimeGranularity.Month3:
                    return dateTime.AddMonths(count * 3);

                case TimeGranularity.Month4:
                    return dateTime.AddMonths(count * 4);

                case TimeGranularity.Month6:
                    return dateTime.AddMonths(count * 6);

                case TimeGranularity.Year1:
                    return dateTime.AddYears(count);

                default:
                    throw new ArgumentException("Unknown time granularity", nameof(granularity));
            }
        }

        /// <summary>
        /// Adds a time granularity unit to the new instance of <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance.</param>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>A new <see cref="DateTime"/> instance.</returns>
        public static DateTime Add(this DateTime dateTime, TimeGranularity granularity)
        {
            switch (granularity)
            {
                case TimeGranularity.Aperiodic:
                    throw new ArgumentException("Cannot add aperiodic time granularities", nameof(granularity));

                case TimeGranularity.Second1:
                    return dateTime.AddSeconds(1);

                case TimeGranularity.Second2:
                    return dateTime.AddSeconds(2);

                case TimeGranularity.Second3:
                    return dateTime.AddSeconds(3);

                case TimeGranularity.Second4:
                    return dateTime.AddSeconds(4);

                case TimeGranularity.Second5:
                    return dateTime.AddSeconds(5);

                case TimeGranularity.Second6:
                    return dateTime.AddSeconds(6);

                case TimeGranularity.Second10:
                    return dateTime.AddSeconds(10);

                case TimeGranularity.Second12:
                    return dateTime.AddSeconds(12);

                case TimeGranularity.Second15:
                    return dateTime.AddSeconds(15);

                case TimeGranularity.Second20:
                    return dateTime.AddSeconds(20);

                case TimeGranularity.Second30:
                    return dateTime.AddSeconds(30);

                case TimeGranularity.Minute1:
                    return dateTime.AddMinutes(1);

                case TimeGranularity.Minute2:
                    return dateTime.AddMinutes(2);

                case TimeGranularity.Minute3:
                    return dateTime.AddMinutes(3);

                case TimeGranularity.Minute4:
                    return dateTime.AddMinutes(4);

                case TimeGranularity.Minute5:
                    return dateTime.AddMinutes(5);

                case TimeGranularity.Minute6:
                    return dateTime.AddMinutes(6);

                case TimeGranularity.Minute10:
                    return dateTime.AddMinutes(10);

                case TimeGranularity.Minute12:
                    return dateTime.AddMinutes(12);

                case TimeGranularity.Minute15:
                    return dateTime.AddMinutes(15);

                case TimeGranularity.Minute20:
                    return dateTime.AddMinutes(20);

                case TimeGranularity.Minute30:
                    return dateTime.AddMinutes(30);

                case TimeGranularity.Hour1:
                    return dateTime.AddHours(1);

                case TimeGranularity.Hour2:
                    return dateTime.AddHours(2);

                case TimeGranularity.Hour3:
                    return dateTime.AddHours(3);

                case TimeGranularity.Hour4:
                    return dateTime.AddHours(4);

                case TimeGranularity.Hour6:
                    return dateTime.AddHours(6);

                case TimeGranularity.Hour8:
                    return dateTime.AddHours(8);

                case TimeGranularity.Hour12:
                    return dateTime.AddHours(12);

                case TimeGranularity.Day1:
                    return dateTime.AddDays(1);

                case TimeGranularity.Week1:
                    return dateTime.AddDays(7);

                case TimeGranularity.Week2:
                    return dateTime.AddDays(14);

                case TimeGranularity.Week3:
                    return dateTime.AddDays(21);

                case TimeGranularity.Month1:
                    return dateTime.AddMonths(1);

                case TimeGranularity.Month2:
                    return dateTime.AddMonths(2);

                case TimeGranularity.Month3:
                    return dateTime.AddMonths(3);

                case TimeGranularity.Month4:
                    return dateTime.AddMonths(4);

                case TimeGranularity.Month6:
                    return dateTime.AddMonths(6);

                case TimeGranularity.Year1:
                    return dateTime.AddYears(1);

                default:
                    throw new ArgumentException("Unknown time granularity", nameof(granularity));
            }
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
            int daysAbsolutValue = sign * days;
            for (int i = 0; i < daysAbsolutValue; ++i)
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
            int daysAbsolutValue = sign * days;
            for (int i = 0; i < daysAbsolutValue; ++i)
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
            return dateTime.ToString(0 == dateTime.Millisecond ? "yyyyMMdd:HHmmss" : "yyyyMMdd:HHmmss.fff", InvariantInfo);
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
            return dateTime.ToString(0 == dateTime.Millisecond ? "HHmmss" : "HHmmss.fff", InvariantInfo);
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
            if (length == 0)
                return DateTime.MinValue;
            if (length == 8)
                return DateTime.ParseExact(text, "yyyyMMdd", InvariantInfo);
            if (length == 6)
                return DateTime.ParseExact(text, "HHmmss", InvariantInfo);
            return DateTime.ParseExact(text, "yyyyMMdd:HHmmss", InvariantInfo);
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
