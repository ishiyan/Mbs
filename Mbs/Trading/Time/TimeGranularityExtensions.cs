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
            switch (granularity)
            {
                case TimeGranularity.Aperiodic:
                    throw new ArgumentException("Aperiodic time granularities do not have a time span", nameof(granularity));

                case TimeGranularity.Second1:
                    return TimeSpans[0];

                case TimeGranularity.Second2:
                    return TimeSpans[1];

                case TimeGranularity.Second3:
                    return TimeSpans[2];

                case TimeGranularity.Second4:
                    return TimeSpans[3];

                case TimeGranularity.Second5:
                    return TimeSpans[4];

                case TimeGranularity.Second6:
                    return TimeSpans[5];

                case TimeGranularity.Second10:
                    return TimeSpans[6];

                case TimeGranularity.Second12:
                    return TimeSpans[7];

                case TimeGranularity.Second15:
                    return TimeSpans[8];

                case TimeGranularity.Second20:
                    return TimeSpans[9];

                case TimeGranularity.Second30:
                    return TimeSpans[10];

                case TimeGranularity.Minute1:
                    return TimeSpans[11];

                case TimeGranularity.Minute2:
                    return TimeSpans[12];

                case TimeGranularity.Minute3:
                    return TimeSpans[13];

                case TimeGranularity.Minute4:
                    return TimeSpans[14];

                case TimeGranularity.Minute5:
                    return TimeSpans[15];

                case TimeGranularity.Minute6:
                    return TimeSpans[16];

                case TimeGranularity.Minute10:
                    return TimeSpans[17];

                case TimeGranularity.Minute12:
                    return TimeSpans[18];

                case TimeGranularity.Minute15:
                    return TimeSpans[19];

                case TimeGranularity.Minute20:
                    return TimeSpans[20];

                case TimeGranularity.Minute30:
                    return TimeSpans[21];

                case TimeGranularity.Hour1:
                    return TimeSpans[22];

                case TimeGranularity.Hour2:
                    return TimeSpans[23];

                case TimeGranularity.Hour3:
                    return TimeSpans[24];

                case TimeGranularity.Hour4:
                    return TimeSpans[25];

                case TimeGranularity.Hour6:
                    return TimeSpans[26];

                case TimeGranularity.Hour8:
                    return TimeSpans[27];

                case TimeGranularity.Hour12:
                    return TimeSpans[28];

                case TimeGranularity.Day1:
                    return TimeSpans[29];

                case TimeGranularity.Week1:
                    return TimeSpans[30];

                case TimeGranularity.Week2:
                    return TimeSpans[31];

                case TimeGranularity.Week3:
                    return TimeSpans[32];

                case TimeGranularity.Month1:
                    return TimeSpans[33];

                case TimeGranularity.Month2:
                    return TimeSpans[34];

                case TimeGranularity.Month3:
                    return TimeSpans[35];

                case TimeGranularity.Month4:
                    return TimeSpans[36];

                case TimeGranularity.Month6:
                    return TimeSpans[37];

                case TimeGranularity.Year1:
                    return TimeSpans[38];

                default:
                    throw new ArgumentException("Unknown time granularity", nameof(granularity));
            }
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
            switch (granularity)
            {
                case TimeGranularity.Second1:
                case TimeGranularity.Second2:
                case TimeGranularity.Second3:
                case TimeGranularity.Second4:
                case TimeGranularity.Second5:
                case TimeGranularity.Second6:
                case TimeGranularity.Second10:
                case TimeGranularity.Second12:
                case TimeGranularity.Second15:
                case TimeGranularity.Second20:
                case TimeGranularity.Second30:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Indicates whether this granularity is in minutes.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is in minutes.</returns>
        public static bool IsMinute(this TimeGranularity granularity)
        {
            switch (granularity)
            {
                case TimeGranularity.Minute1:
                case TimeGranularity.Minute2:
                case TimeGranularity.Minute3:
                case TimeGranularity.Minute4:
                case TimeGranularity.Minute5:
                case TimeGranularity.Minute6:
                case TimeGranularity.Minute10:
                case TimeGranularity.Minute12:
                case TimeGranularity.Minute15:
                case TimeGranularity.Minute20:
                case TimeGranularity.Minute30:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Indicates whether this granularity is in hours.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is in hours.</returns>
        public static bool IsHour(this TimeGranularity granularity)
        {
            switch (granularity)
            {
                case TimeGranularity.Hour1:
                case TimeGranularity.Hour2:
                case TimeGranularity.Hour3:
                case TimeGranularity.Hour4:
                case TimeGranularity.Hour6:
                case TimeGranularity.Hour8:
                case TimeGranularity.Hour12:
                    return true;
                default:
                    return false;
            }
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
            switch (granularity)
            {
                case TimeGranularity.Week1:
                case TimeGranularity.Week2:
                case TimeGranularity.Week3:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Indicates whether this granularity is in months.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is in months.</returns>
        public static bool IsMonth(this TimeGranularity granularity)
        {
            switch (granularity)
            {
                case TimeGranularity.Month1:
                case TimeGranularity.Month2:
                case TimeGranularity.Month3:
                case TimeGranularity.Month4:
                case TimeGranularity.Month6:
                    return true;
                default:
                    return false;
            }
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
            switch (granularity)
            {
                case TimeGranularity.Day1:
                case TimeGranularity.Week1:
                case TimeGranularity.Week2:
                case TimeGranularity.Week3:
                case TimeGranularity.Month1:
                case TimeGranularity.Month2:
                case TimeGranularity.Month3:
                case TimeGranularity.Month4:
                case TimeGranularity.Month6:
                case TimeGranularity.Year1:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Indicates whether this granularity is less than a day.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns a value indicating whether this granularity is less than a day.</returns>
        public static bool IsIntraday(this TimeGranularity granularity)
        {
            switch (granularity)
            {
                case TimeGranularity.Second1:
                case TimeGranularity.Second2:
                case TimeGranularity.Second3:
                case TimeGranularity.Second4:
                case TimeGranularity.Second5:
                case TimeGranularity.Second6:
                case TimeGranularity.Second10:
                case TimeGranularity.Second12:
                case TimeGranularity.Second15:
                case TimeGranularity.Second20:
                case TimeGranularity.Second30:
                case TimeGranularity.Minute1:
                case TimeGranularity.Minute2:
                case TimeGranularity.Minute3:
                case TimeGranularity.Minute4:
                case TimeGranularity.Minute5:
                case TimeGranularity.Minute6:
                case TimeGranularity.Minute10:
                case TimeGranularity.Minute12:
                case TimeGranularity.Minute15:
                case TimeGranularity.Minute20:
                case TimeGranularity.Minute30:
                case TimeGranularity.Hour1:
                case TimeGranularity.Hour2:
                case TimeGranularity.Hour3:
                case TimeGranularity.Hour4:
                case TimeGranularity.Hour6:
                case TimeGranularity.Hour8:
                case TimeGranularity.Hour12:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// The number of time units in the granularity.
        /// </summary>
        /// <param name="granularity">The time granularity.</param>
        /// <returns>Returns the number of time units in the granularity.</returns>
        public static int NumberOfUnits(this TimeGranularity granularity)
        {
            switch (granularity)
            {
                case TimeGranularity.Second1:
                case TimeGranularity.Minute1:
                case TimeGranularity.Hour1:
                case TimeGranularity.Day1:
                case TimeGranularity.Week1:
                case TimeGranularity.Month1:
                case TimeGranularity.Year1:
                    return 1;

                case TimeGranularity.Second2:
                case TimeGranularity.Minute2:
                case TimeGranularity.Hour2:
                case TimeGranularity.Week2:
                case TimeGranularity.Month2:
                    return 2;

                case TimeGranularity.Second3:
                case TimeGranularity.Minute3:
                case TimeGranularity.Hour3:
                case TimeGranularity.Week3:
                case TimeGranularity.Month3:
                    return 3;

                case TimeGranularity.Second4:
                case TimeGranularity.Minute4:
                case TimeGranularity.Hour4:
                case TimeGranularity.Month4:
                    return 4;

                case TimeGranularity.Second5:
                case TimeGranularity.Minute5:
                    return 5;

                case TimeGranularity.Second6:
                case TimeGranularity.Minute6:
                case TimeGranularity.Hour6:
                case TimeGranularity.Month6:
                    return 6;

                case TimeGranularity.Hour8:
                    return 8;

                case TimeGranularity.Second10:
                case TimeGranularity.Minute10:
                    return 10;

                case TimeGranularity.Second12:
                case TimeGranularity.Minute12:
                case TimeGranularity.Hour12:
                    return 12;

                case TimeGranularity.Second15:
                case TimeGranularity.Minute15:
                    return 15;

                case TimeGranularity.Second20:
                case TimeGranularity.Minute20:
                    return 20;

                case TimeGranularity.Second30:
                case TimeGranularity.Minute30:
                    return 30;
            }

            return 1;
        }
    }
}
