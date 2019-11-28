using System;

namespace Mbs.Trading.Time
{
    /// <summary>
    /// Time granularity extensions.
    /// </summary>
    public static class TimeGranularityExtensions
    {
        private static readonly TimeSpan[] TimeSpans =
        {
            new TimeSpan(0, 0, 1), // Second1, 0
            new TimeSpan(0, 0, 2), // Second2, 1
            new TimeSpan(0, 0, 3), // Second3, 2
            new TimeSpan(0, 0, 4), // Second4, 3
            new TimeSpan(0, 0, 5), // Second5, 4
            new TimeSpan(0, 0, 6), // Second6, 5
            new TimeSpan(0, 0, 10), // Second10, 6
            new TimeSpan(0, 0, 12), // Second12, 7
            new TimeSpan(0, 0, 15), // Second15, 8
            new TimeSpan(0, 0, 20), // Second20, 9
            new TimeSpan(0, 0, 30), // Second30, 10
            new TimeSpan(0, 1, 0), // Minute1, 11
            new TimeSpan(0, 2, 0), // Minute2, 12
            new TimeSpan(0, 3, 0), // Minute3, 13
            new TimeSpan(0, 4, 0), // Minute4, 14
            new TimeSpan(0, 5, 0), // Minute5, 15
            new TimeSpan(0, 6, 0), // Minute6, 16
            new TimeSpan(0, 10, 0), // Minute10, 17
            new TimeSpan(0, 12, 0), // Minute12, 18
            new TimeSpan(0, 15, 0), // Minute15, 19
            new TimeSpan(0, 20, 0), // Minute20, 20
            new TimeSpan(0, 30, 0), // Minute30, 21
            new TimeSpan(1, 0, 0), // Hour1, 22
            new TimeSpan(2, 0, 0), // Hour2, 23
            new TimeSpan(3, 0, 0), // Hour3, 24
            new TimeSpan(4, 0, 0), // Hour4, 25
            new TimeSpan(6, 0, 0), // Hour6, 26
            new TimeSpan(8, 0, 0), // Hour8, 27
            new TimeSpan(12, 0, 0), // Hour12, 28
            new TimeSpan(1, 0, 0, 0), // Day1, 29
            new TimeSpan(7, 0, 0, 0), // Week1, 30
            new TimeSpan(14, 0, 0, 0), // Week2, 31
            new TimeSpan(21, 0, 0, 0), // Week3, 32
            new TimeSpan(30, 0, 0, 0), // Month1, 33
            new TimeSpan(60, 0, 0, 0), // Month2, 34
            new TimeSpan(90, 0, 0, 0), // Month3, 35
            new TimeSpan(120, 0, 0, 0), // Month4, 36
            new TimeSpan(160, 0, 0, 0), // Month6, 37
            new TimeSpan(365, 0, 0, 0) // Year1, 38
        };

        /// <summary>
        /// The equivalent <see cref="System.TimeSpan"/> of this time granularity.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns the equivalent time span of this time granularity.</returns>
        public static TimeSpan TimeSpan(this TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Aperiodic => throw new ArgumentException("Aperiodic time granularity does not have a time span", nameof(granularity)),
                TimeGranularity.Second1 => TimeSpans[0],
                TimeGranularity.Second2 => TimeSpans[1],
                TimeGranularity.Second3 => TimeSpans[2],
                TimeGranularity.Second4 => TimeSpans[3],
                TimeGranularity.Second5 => TimeSpans[4],
                TimeGranularity.Second6 => TimeSpans[5],
                TimeGranularity.Second10 => TimeSpans[6],
                TimeGranularity.Second12 => TimeSpans[7],
                TimeGranularity.Second15 => TimeSpans[8],
                TimeGranularity.Second20 => TimeSpans[9],
                TimeGranularity.Second30 => TimeSpans[10],
                TimeGranularity.Minute1 => TimeSpans[11],
                TimeGranularity.Minute2 => TimeSpans[12],
                TimeGranularity.Minute3 => TimeSpans[13],
                TimeGranularity.Minute4 => TimeSpans[14],
                TimeGranularity.Minute5 => TimeSpans[15],
                TimeGranularity.Minute6 => TimeSpans[16],
                TimeGranularity.Minute10 => TimeSpans[17],
                TimeGranularity.Minute12 => TimeSpans[18],
                TimeGranularity.Minute15 => TimeSpans[19],
                TimeGranularity.Minute20 => TimeSpans[20],
                TimeGranularity.Minute30 => TimeSpans[21],
                TimeGranularity.Hour1 => TimeSpans[22],
                TimeGranularity.Hour2 => TimeSpans[23],
                TimeGranularity.Hour3 => TimeSpans[24],
                TimeGranularity.Hour4 => TimeSpans[25],
                TimeGranularity.Hour6 => TimeSpans[26],
                TimeGranularity.Hour8 => TimeSpans[27],
                TimeGranularity.Hour12 => TimeSpans[28],
                TimeGranularity.Day1 => TimeSpans[29],
                TimeGranularity.Week1 => TimeSpans[30],
                TimeGranularity.Week2 => TimeSpans[31],
                TimeGranularity.Week3 => TimeSpans[32],
                TimeGranularity.Month1 => TimeSpans[33],
                TimeGranularity.Month2 => TimeSpans[34],
                TimeGranularity.Month3 => TimeSpans[35],
                TimeGranularity.Month4 => TimeSpans[36],
                TimeGranularity.Month6 => TimeSpans[37],
                TimeGranularity.Year1 => TimeSpans[38],
                _ => throw new ArgumentException("Unknown time granularity", nameof(granularity))
            };
        }

        /// <summary>
        /// The amount of seconds in this time granularity.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns the amount of seconds in this granularity.</returns>
        public static long TotalSeconds(this TimeGranularity granularity)
        {
            return (long)granularity.TimeSpan().TotalSeconds;
        }

        /// <summary>
        /// Indicates whether this granularity is aperiodic.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is aperiodic.</returns>
        public static bool IsAperiodic(this TimeGranularity granularity)
        {
            return granularity == TimeGranularity.Aperiodic;
        }

        /// <summary>
        /// Indicates whether this granularity is in seconds.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is in seconds.</returns>
        public static bool IsSecond(this TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Second1 => true,
                TimeGranularity.Second2 => true,
                TimeGranularity.Second3 => true,
                TimeGranularity.Second4 => true,
                TimeGranularity.Second5 => true,
                TimeGranularity.Second6 => true,
                TimeGranularity.Second10 => true,
                TimeGranularity.Second12 => true,
                TimeGranularity.Second15 => true,
                TimeGranularity.Second20 => true,
                TimeGranularity.Second30 => true,
                _ => false
            };
        }

        /// <summary>
        /// Indicates whether this granularity is in minutes.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is in minutes.</returns>
        public static bool IsMinute(this TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Minute1 => true,
                TimeGranularity.Minute2 => true,
                TimeGranularity.Minute3 => true,
                TimeGranularity.Minute4 => true,
                TimeGranularity.Minute5 => true,
                TimeGranularity.Minute6 => true,
                TimeGranularity.Minute10 => true,
                TimeGranularity.Minute12 => true,
                TimeGranularity.Minute15 => true,
                TimeGranularity.Minute20 => true,
                TimeGranularity.Minute30 => true,
                _ => false
            };
        }

        /// <summary>
        /// Indicates whether this granularity is in hours.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is in hours.</returns>
        public static bool IsHour(this TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Hour1 => true,
                TimeGranularity.Hour2 => true,
                TimeGranularity.Hour3 => true,
                TimeGranularity.Hour4 => true,
                TimeGranularity.Hour6 => true,
                TimeGranularity.Hour8 => true,
                TimeGranularity.Hour12 => true,
                _ => false
            };
        }

        /// <summary>
        /// Indicates whether this granularity is in days.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is days.</returns>
        public static bool IsDay(this TimeGranularity granularity)
        {
            return granularity == TimeGranularity.Day1;
        }

        /// <summary>
        /// Indicates whether this granularity is in weeks.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is in weeks.</returns>
        public static bool IsWeek(this TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Week1 => true,
                TimeGranularity.Week2 => true,
                TimeGranularity.Week3 => true,
                _ => false
            };
        }

        /// <summary>
        /// Indicates whether this granularity is in months.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is in months.</returns>
        public static bool IsMonth(this TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Month1 => true,
                TimeGranularity.Month2 => true,
                TimeGranularity.Month3 => true,
                TimeGranularity.Month4 => true,
                TimeGranularity.Month6 => true,
                _ => false
            };
        }

        /// <summary>
        /// Indicates whether this granularity is in years.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is years.</returns>
        public static bool IsYear(this TimeGranularity granularity)
        {
            return granularity == TimeGranularity.Year1;
        }

        /// <summary>
        /// Indicates whether this granularity is equal or longer than a day.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is equal or longer than a day.</returns>
        public static bool IsEndofday(this TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Day1 => true,
                TimeGranularity.Week1 => true,
                TimeGranularity.Week2 => true,
                TimeGranularity.Week3 => true,
                TimeGranularity.Month1 => true,
                TimeGranularity.Month2 => true,
                TimeGranularity.Month3 => true,
                TimeGranularity.Month4 => true,
                TimeGranularity.Month6 => true,
                TimeGranularity.Year1 => true,
                _ => false
            };
        }

        /// <summary>
        /// Indicates whether this granularity is less than a day.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is less than a day.</returns>
        public static bool IsIntraday(this TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Second1 => true,
                TimeGranularity.Second2 => true,
                TimeGranularity.Second3 => true,
                TimeGranularity.Second4 => true,
                TimeGranularity.Second5 => true,
                TimeGranularity.Second6 => true,
                TimeGranularity.Second10 => true,
                TimeGranularity.Second12 => true,
                TimeGranularity.Second15 => true,
                TimeGranularity.Second20 => true,
                TimeGranularity.Second30 => true,
                TimeGranularity.Minute1 => true,
                TimeGranularity.Minute2 => true,
                TimeGranularity.Minute3 => true,
                TimeGranularity.Minute4 => true,
                TimeGranularity.Minute5 => true,
                TimeGranularity.Minute6 => true,
                TimeGranularity.Minute10 => true,
                TimeGranularity.Minute12 => true,
                TimeGranularity.Minute15 => true,
                TimeGranularity.Minute20 => true,
                TimeGranularity.Minute30 => true,
                TimeGranularity.Hour1 => true,
                TimeGranularity.Hour2 => true,
                TimeGranularity.Hour3 => true,
                TimeGranularity.Hour4 => true,
                TimeGranularity.Hour6 => true,
                TimeGranularity.Hour8 => true,
                TimeGranularity.Hour12 => true,
                _ => false
            };
        }

        /// <summary>
        /// The number of time units in the granularity.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns the number of time units in the granularity.</returns>
        public static int NumberOfUnits(this TimeGranularity granularity)
        {
            return granularity switch
            {
                TimeGranularity.Second1 => 1,
                TimeGranularity.Minute1 => 1,
                TimeGranularity.Hour1 => 1,
                TimeGranularity.Day1 => 1,
                TimeGranularity.Week1 => 1,
                TimeGranularity.Month1 => 1,
                TimeGranularity.Year1 => 1,

                TimeGranularity.Second2 => 2,
                TimeGranularity.Minute2 => 2,
                TimeGranularity.Hour2 => 2,
                TimeGranularity.Week2 => 2,
                TimeGranularity.Month2 => 2,

                TimeGranularity.Second3 => 3,
                TimeGranularity.Minute3 => 3,
                TimeGranularity.Hour3 => 3,
                TimeGranularity.Week3 => 3,
                TimeGranularity.Month3 => 3,

                TimeGranularity.Second4 => 4,
                TimeGranularity.Minute4 => 4,
                TimeGranularity.Hour4 => 4,
                TimeGranularity.Month4 => 4,

                TimeGranularity.Second5 => 5,
                TimeGranularity.Minute5 => 5,

                TimeGranularity.Second6 => 6,
                TimeGranularity.Minute6 => 6,
                TimeGranularity.Hour6 => 6,
                TimeGranularity.Month6 => 6,

                TimeGranularity.Hour8 => 8,

                TimeGranularity.Second10 => 10,
                TimeGranularity.Minute10 => 10,

                TimeGranularity.Second12 => 12,
                TimeGranularity.Minute12 => 12,
                TimeGranularity.Hour12 => 12,

                TimeGranularity.Second15 => 15,
                TimeGranularity.Minute15 => 15,

                TimeGranularity.Second20 => 20,
                TimeGranularity.Minute20 => 20,

                TimeGranularity.Second30 => 30,
                TimeGranularity.Minute30 => 30,
                _ => 1
            };
        }
    }
}
