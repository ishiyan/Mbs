using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Holidays;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data
{
    /// <summary>
    /// Range, aggregation, and conversion helpers.
    /// </summary>
    public static class AggregatingConverter
    {
        /// <summary>
        /// Converts a range of an ohlcv list to an enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <typeparam name="T">A temporal entity type.</typeparam>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<T> Range<T>(this List<T> list, DateTime startTime, DateTime endTime)
            where T : TemporalEntity
        {
            if (list == null || list.Count == 0)
            {
                yield break;
            }

            bool checkBeginTime = startTime.IsNotZero(), checkEndTime = endTime.IsNotZero();
            if (checkBeginTime || checkEndTime)
            {
                int index, firstIndex = 0, lastIndex = list.Count - 1;
                if (checkBeginTime)
                {
                    index = list.FindIndex(t => t.Time >= startTime);
                    if (index >= 0)
                    {
                        firstIndex = index;
                    }
                }

                if (checkEndTime)
                {
                    index = list.FindLastIndex(t => t.Time <= endTime);
                    if (index >= 0)
                    {
                        lastIndex = index;
                    }
                }

                for (index = firstIndex; index <= lastIndex; index++)
                {
                    yield return list[index];
                }
            }
            else
            {
                foreach (T t in list)
                {
                    yield return t;
                }
            }
        }

        /// <summary>
        /// Aggregates a range of a one-day ohlcv list and converts it to a physical multi-day enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="count">The number of physical days to aggregate.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Ohlcv> AggregatePhysicalDays(this List<Ohlcv> list, DateTime startTime, DateTime endTime, int count)
        {
            return AggregateDays(list, startTime, endTime, count, DayBinThreshold);
        }

        /// <summary>
        /// Aggregates a range of a one-day scalar list and converts it to a physical multi-day enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="count">The number of physical days to aggregate.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Scalar> AggregatePhysicalDays(this List<Scalar> list, DateTime startTime, DateTime endTime, int count)
        {
            return AggregateDays(list, startTime, endTime, count, DayBinThreshold);
        }

        /// <summary>
        /// Aggregates a range of a one-day ohlcv list and converts it to a business multi-day enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="count">The number of business days to aggregate.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Ohlcv> AggregateBusinessDays(this List<Ohlcv> list, DateTime startTime, DateTime endTime, int count)
        {
            return AggregateDays(list, startTime, endTime, count, BusinessDayBinThreshold);
        }

        /// <summary>
        /// Aggregates a range of a one-day scalar list and converts it to a business multi-day enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="count">The number of business days to aggregate.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Scalar> AggregateBusinessDays(this List<Scalar> list, DateTime startTime, DateTime endTime, int count)
        {
            return AggregateDays(list, startTime, endTime, count, BusinessDayBinThreshold);
        }

        /// <summary>
        /// Aggregates a range of a one-day ohlcv list and converts it to a Euronext trading multi-day enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="count">The number of Euronext trading days to aggregate.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Ohlcv> AggregateEuronextTradingDays(this List<Ohlcv> list, DateTime startTime, DateTime endTime, int count)
        {
            return AggregateDays(list, startTime, endTime, count, EuronextTradingDayBinThreshold);
        }

        /// <summary>
        /// Aggregates a range of a one-day scalar list and converts it to a Euronext trading multi-day enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="count">The number of Euronext trading days to aggregate.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Scalar> AggregateEuronextTradingDays(this List<Scalar> list, DateTime startTime, DateTime endTime, int count)
        {
            return AggregateDays(list, startTime, endTime, count, EuronextTradingDayBinThreshold);
        }

        /// <summary>
        /// Aggregates a range of a one-week ohlcv list and converts it to a multi-week enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="count">The number of weeks to aggregate.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Ohlcv> AggregateWeeks(this List<Ohlcv> list, DateTime startTime, DateTime endTime, int count)
        {
            var dateTime = new DateTime(0L);
            Ohlcv ohlcv = null;
            int index, firstIndex = 0, lastIndex = list.Count - 1;
            if (startTime.IsNotZero())
            {
                index = list.FindIndex(o => o.Time >= startTime);
                if (index >= 0)
                {
                    firstIndex = index;
                }
            }

            if (endTime.IsNotZero())
            {
                index = list.FindLastIndex(o => o.Time <= endTime);
                if (index >= 0)
                {
                    lastIndex = index;
                }
            }

            for (index = firstIndex; index <= lastIndex; ++index)
            {
                Ohlcv currentOhlcv = list[index];
                if (dateTime > currentOhlcv.Time)
                {
                    if (ohlcv != null)
                    {
                        ohlcv.Aggregate(currentOhlcv);
                    }
                    else
                    {
                        ohlcv = currentOhlcv.CloneAggregation();
                    }
                }
                else
                {
                    if (ohlcv != null)
                    {
                        yield return ohlcv;
                    }

                    ohlcv = currentOhlcv.CloneAggregation();
                    dateTime = currentOhlcv.WeekBinThreshold(count);
                }
            }

            if (ohlcv != null)
            {
                yield return ohlcv;
            }
        }

        /// <summary>
        /// Aggregates a range of a one-month ohlcv list and converts it to a multi-month enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="count">The number of month to aggregate.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Ohlcv> AggregateMonths(this List<Ohlcv> list, DateTime startTime, DateTime endTime, int count)
        {
            var dateTime = new DateTime(0L);
            Ohlcv ohlcv = null;
            int index, firstIndex = 0, lastIndex = list.Count - 1;
            if (startTime.IsNotZero())
            {
                index = list.FindIndex(o => o.Time >= startTime);
                if (index >= 0)
                {
                    firstIndex = index;
                }
            }

            if (endTime.IsNotZero())
            {
                index = list.FindLastIndex(o => o.Time <= endTime);
                if (index >= 0)
                {
                    lastIndex = index;
                }
            }

            for (index = firstIndex; index <= lastIndex; index++)
            {
                Ohlcv currentOhlcv = list[index];
                if (dateTime > currentOhlcv.Time)
                {
                    if (ohlcv != null)
                    {
                        ohlcv.Aggregate(currentOhlcv);
                    }
                    else
                    {
                        ohlcv = currentOhlcv.CloneAggregation();
                    }
                }
                else
                {
                    if (ohlcv != null)
                    {
                        yield return ohlcv;
                    }

                    ohlcv = currentOhlcv.CloneAggregation();
                    dateTime = currentOhlcv.MonthBinThreshold(count);
                }
            }

            if (ohlcv != null)
            {
                yield return ohlcv;
            }
        }

        /// <summary>
        /// Aggregates a range of a one-day ohlcv list and converts it to a multi-year enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="count">The number of month to aggregate.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Ohlcv> AggregateYears(this List<Ohlcv> list, DateTime startTime, DateTime endTime, int count)
        {
            var dateTime = new DateTime(0L);
            Ohlcv ohlcv = null;
            int index, firstIndex = 0, lastIndex = list.Count - 1;
            if (startTime.IsNotZero())
            {
                index = list.FindIndex(o => o.Time >= startTime);
                if (index >= 0)
                {
                    firstIndex = index;
                }
            }

            if (endTime.IsNotZero())
            {
                index = list.FindLastIndex(o => o.Time <= endTime);
                if (index >= 0)
                {
                    lastIndex = index;
                }
            }

            for (index = firstIndex; index <= lastIndex; index++)
            {
                Ohlcv currentOhlcv = list[index];
                if (dateTime > currentOhlcv.Time)
                {
                    if (ohlcv != null)
                    {
                        ohlcv.Aggregate(currentOhlcv);
                    }
                    else
                    {
                        ohlcv = currentOhlcv.CloneAggregation();
                    }
                }
                else
                {
                    if (ohlcv != null)
                    {
                        yield return ohlcv;
                    }

                    ohlcv = currentOhlcv.CloneAggregation();
                    dateTime = currentOhlcv.YearBinThreshold(count);
                }
            }

            if (ohlcv != null)
            {
                yield return ohlcv;
            }
        }

        /// <summary>
        /// Aggregates a range of an ohlcv list and converts it to a multi-unit enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="timeGranularity">The time granularity of the unit.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Ohlcv> AggregateUnits(this List<Ohlcv> list, DateTime startTime, DateTime endTime, TimeGranularity timeGranularity)
        {
            int index, firstIndex = 0, lastIndex = list.Count - 1;
            int numberOfUnits = timeGranularity.NumberOfUnits();
            if (startTime.IsNotZero())
            {
                index = list.FindIndex(o => o.Time >= startTime);
                if (index >= 0)
                {
                    firstIndex = index;
                }
            }

            if (endTime.IsNotZero())
            {
                index = list.FindLastIndex(o => o.Time <= endTime);
                if (index >= 0)
                {
                    lastIndex = index;
                }
            }

            Ohlcv aggregatedOhlcv = null;
            var dateTime = new DateTime(0L);
            Func<TemporalEntity, int, DateTime> thresholdDateTime = SelectThresholdDateTime(timeGranularity);
            for (index = firstIndex; index <= lastIndex; index++)
            {
                Ohlcv ohlcv = list[index];
                if (dateTime > ohlcv.Time)
                {
                    if (aggregatedOhlcv != null)
                    {
                        aggregatedOhlcv.Aggregate(ohlcv);
                    }
                    else
                    {
                        aggregatedOhlcv = ohlcv.CloneAggregation();
                    }
                }
                else
                {
                    if (aggregatedOhlcv != null)
                    {
                        yield return aggregatedOhlcv;
                    }

                    aggregatedOhlcv = ohlcv.CloneAggregation();
                    dateTime = thresholdDateTime(ohlcv, numberOfUnits);
                }
            }

            if (aggregatedOhlcv != null)
            {
                yield return aggregatedOhlcv;
            }
        }

        /// <summary>
        /// Aggregates a range of a scalar list and converts it to a multi-unit enumerable.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="startTime">The begin of the range.</param>
        /// <param name="endTime">The end of the range.</param>
        /// <param name="timeGranularity">The time granularity of the unit.</param>
        /// <returns>The enumerable.</returns>
        internal static IEnumerable<Scalar> AggregateUnits(this List<Scalar> list, DateTime startTime, DateTime endTime, TimeGranularity timeGranularity)
        {
            int index, firstIndex = 0, lastIndex = list.Count - 1;
            int numberOfUnits = timeGranularity.NumberOfUnits();
            if (startTime.IsNotZero())
            {
                index = list.FindIndex(o => o.Time >= startTime);
                if (index >= 0)
                {
                    firstIndex = index;
                }
            }

            if (endTime.IsNotZero())
            {
                index = list.FindLastIndex(o => o.Time <= endTime);
                if (index >= 0)
                {
                    lastIndex = index;
                }
            }

            var dateTime = new DateTime(0L);
            Func<TemporalEntity, int, DateTime> thresholdDateTime = SelectThresholdDateTime(timeGranularity);
            var aggregator = new ScalarAggregator();
            for (index = firstIndex; index <= lastIndex; index++)
            {
                Scalar scalar = list[index];
                if (dateTime > scalar.Time)
                {
                    aggregator.Aggregate(scalar);
                }
                else
                {
                    if (!aggregator.IsEmpty)
                    {
                        yield return aggregator.Emit(/*dateTime*/);
                    }

                    dateTime = thresholdDateTime(scalar, numberOfUnits);
                }
            }

            if (!aggregator.IsEmpty)
            {
                yield return aggregator.Emit();
            }
        }

        /// <summary>
        /// The date and time of the end of this entity's aggregating bin with the duration in seconds.
        /// <para />
        /// The bins are aligned at the beginning of a minute.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="duration">The duration in seconds.</param>
        /// <returns>The date and time.</returns>
        internal static DateTime SecondBinThreshold(this TemporalEntity entity, int duration)
        {
            DateTime dateTime = entity.Time;
            dateTime = new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                (dateTime.Second / duration) * duration);
            return dateTime.AddSeconds(duration);
        }

        /// <summary>
        /// The date and time of the end of this entity's aggregating bin with the duration in minutes.
        /// <para />
        /// The bins are aligned at the beginning of an hour.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="duration">The duration in minutes.</param>
        /// <returns>The date and time.</returns>
        internal static DateTime MinuteBinThreshold(this TemporalEntity entity, int duration)
        {
            DateTime dateTime = entity.Time;
            dateTime = new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                (dateTime.Minute / duration) * duration,
                0);
            return dateTime.AddMinutes(duration);
        }

        /// <summary>
        /// The date and time of the end of this entity's aggregating bin with the duration in hours.
        /// <para />
        /// The bins are aligned at the beginning of a day.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="duration">The duration in hours.</param>
        /// <returns>The date and time.</returns>
        internal static DateTime HourBinThreshold(this TemporalEntity entity, int duration)
        {
            DateTime dateTime = entity.Time;
            dateTime = new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                (dateTime.Hour / duration) * duration,
                0,
                0);
            return dateTime.AddHours(duration);
        }

        /// <summary>
        /// The date and time of the end of this entity's aggregating bin with the duration in days.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="duration">The duration in days.</param>
        /// <returns>The date and time.</returns>
        internal static DateTime DayBinThreshold(this TemporalEntity entity, int duration)
        {
            DateTime dateTime = entity.Time;
            return dateTime.AddDays(duration).Date;
        }

        /// <summary>
        /// The date and time of the end of this entity's aggregating bin with the duration in business days (weekends excluded, holidays included).
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="duration">The duration in business days.</param>
        /// <returns>The date and time.</returns>
        internal static DateTime BusinessDayBinThreshold(this TemporalEntity entity, int duration)
        {
            DateTime dateTime = entity.Time;
            double sign = Convert.ToDouble(Math.Sign(duration), CultureInfo.InvariantCulture);
            int unsignedDays = Math.Sign(duration) * duration;
            for (int i = 0; i < unsignedDays; i++)
            {
                do
                {
                    dateTime = dateTime.AddDays(sign);
                }
                while (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday);
            }

            return dateTime;
        }

        /// <summary>
        /// The date and time of the end of this entity's aggregating bin with the duration in Euronext trading days.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="duration">The duration in Euronext trading days.</param>
        /// <returns>The date and time.</returns>
        internal static DateTime EuronextTradingDayBinThreshold(this TemporalEntity entity, int duration)
        {
            DateTime dateTime = entity.Time;
            double sign = Convert.ToDouble(Math.Sign(duration), CultureInfo.InvariantCulture);
            int unsignedDays = Math.Sign(duration) * duration;
            for (int i = 0; i < unsignedDays; i++)
            {
                do
                {
                    dateTime = dateTime.AddDays(sign);
                }
                while (!dateTime.IsEuronextWorkday());
            }

            return dateTime;
        }

        /// <summary>
        /// The date and time of the end of this entity's aggregating bin with the duration in weeks.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="duration">The duration in weeks.</param>
        /// <returns>The date and time.</returns>
        internal static DateTime WeekBinThreshold(this TemporalEntity entity, int duration)
        {
            DateTime dateTime = entity.Time;
            int daysTillNextWeek =
                Convert.ToInt32(CultureInfo.InvariantCulture.DateTimeFormat.FirstDayOfWeek, CultureInfo.InvariantCulture) -
                Convert.ToInt32(dateTime.DayOfWeek, CultureInfo.InvariantCulture) + 7;
            if (daysTillNextWeek == 8)
            {
                daysTillNextWeek = 1;
            }

            return dateTime.AddDays(daysTillNextWeek + 7 * (duration - 1));
        }

        /// <summary>
        /// The date and time of the end of this entity's aggregating bin with the duration in months.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="duration">The duration in months.</param>
        /// <returns>The date and time.</returns>
        internal static DateTime MonthBinThreshold(this TemporalEntity entity, int duration)
        {
            DateTime dateTime = entity.Time;
            dateTime = new DateTime(
                dateTime.Year,
                ((dateTime.Month - 1) / duration) * duration + 1,
                1,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second);
            return dateTime.AddMonths(duration);
        }

        /// <summary>
        /// The date and time of the end of this entity's aggregating bin with the duration in years.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="duration">The duration in years.</param>
        /// <returns>The date and time.</returns>
        internal static DateTime YearBinThreshold(this TemporalEntity entity, int duration)
        {
            DateTime dateTime = entity.Time;
            dateTime = new DateTime(
                ((dateTime.Year - 1) / duration) * duration + 1,
                1,
                1,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second);
            return dateTime.AddYears(duration);
        }

        /// <summary>
        /// Given a time granularity, selects an appropriate date-time bin threshold function.
        /// </summary>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <returns>The selected date-time bin threshold function.</returns>
        internal static Func<TemporalEntity, int, DateTime> SelectThresholdDateTime(TimeGranularity timeGranularity)
        {
            // TODO: Here you can select a BusinessCalender/Euronext BinThreshold
            if (timeGranularity.IsDay())
            {
                return DayBinThreshold;
            }

            if (timeGranularity.IsHour())
            {
                return HourBinThreshold;
            }

            if (timeGranularity.IsMinute())
            {
                return MinuteBinThreshold;
            }

            if (timeGranularity.IsSecond())
            {
                return SecondBinThreshold;
            }

            if (timeGranularity.IsWeek())
            {
                return WeekBinThreshold;
            }

            if (timeGranularity.IsMonth())
            {
                return MonthBinThreshold;
            }

            if (timeGranularity.IsYear())
            {
                return YearBinThreshold;
            }

            return null; // Time granularity is in trades.
        }

        /// <summary>
        /// Determines if the <paramref name="timeGranularitySource"/> can be aggregated into the <paramref name="timeGranularityTarget"/>.
        /// </summary>
        /// <param name="timeGranularitySource">The source time granularity.</param>
        /// <param name="timeGranularityTarget">The target time granularity.</param>
        /// <returns>The boolean indicating if the aggregation is possible.</returns>
        internal static bool CanAggregate(TimeGranularity timeGranularitySource, TimeGranularity timeGranularityTarget)
        {
            if (timeGranularitySource > timeGranularityTarget)
            {
                return false;
            }

            if (timeGranularitySource == timeGranularityTarget)
            {
                int durationSource = timeGranularitySource.NumberOfUnits();
                int durationTarget = timeGranularityTarget.NumberOfUnits();
                if (durationSource > durationTarget)
                {
                    return false;
                }

                if (durationSource == durationTarget)
                {
                    return true;
                }

                return (durationTarget % durationSource) == 0;
            }

            if (timeGranularitySource.IsAperiodic())
            {
                return true;
            }

            if ((timeGranularitySource.IsIntraday() || timeGranularitySource.IsDay())
                && timeGranularityTarget.IsEndofday() && !timeGranularitySource.IsDay())
            {
                return true;
            }

            // Convert to seconds and compare.
            long secondsSource = ConvertToSeconds(timeGranularitySource);
            long secondsTarget = ConvertToSeconds(timeGranularityTarget);
            return secondsTarget % secondsSource == 0;
        }

        /// <summary>
        /// Converts the <see cref="Scalar"/> enumerable to the <see cref="Ohlcv"/> enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable to convert from.</param>
        /// <returns>The converted enumerable.</returns>
        internal static IEnumerable<Ohlcv> ConvertToOhlcv(IEnumerable<Scalar> enumerable)
            => enumerable.Select(Ohlcv.CloneAggregation);

        /// <summary>
        /// Converts the <see cref="Trade"/> enumerable to the <see cref="Ohlcv"/> enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable to convert from.</param>
        /// <returns>The converted enumerable.</returns>
        internal static IEnumerable<Ohlcv> ConvertToOhlcv(IEnumerable<Trade> enumerable)
            => enumerable.Select(Ohlcv.CloneAggregation);

        /// <summary>
        /// Aggregates the <see cref="Ohlcv"/> enumerable to the given time granularity.
        /// </summary>
        /// <param name="enumerable">The enumerable to aggregate.</param>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <param name="thresholdDateTime">The threshold date and time.</param>
        /// <returns>The aggregated enumerable.</returns>
        internal static IEnumerable<Ohlcv> Aggregate(
            IEnumerable<Ohlcv> enumerable,
            TimeGranularity timeGranularity,
            Func<TemporalEntity, int, DateTime> thresholdDateTime)
        {
            int numberOfUnits = timeGranularity.NumberOfUnits();
            Ohlcv ohlcv = null;
            if (thresholdDateTime == null)
            {
                int current = 0;
                foreach (var o in enumerable)
                {
                    if (ohlcv == null)
                    {
                        ohlcv = new Ohlcv(o.Time, o.Open, o.High, o.Low, o.Close, o.Volume);
                    }
                    else
                    {
                        ohlcv.Aggregate(o);
                    }

                    if (++current == numberOfUnits)
                    {
                        yield return ohlcv;
                        current = 0;
                        ohlcv = null;
                    }
                }
            }
            else
            {
                var dateTime = DateTime.MinValue;
                foreach (var o in enumerable)
                {
                    if (dateTime > o.Time)
                    {
                        if (ohlcv == null)
                        {
                            ohlcv = new Ohlcv(o.Time, o.Open, o.High, o.Low, o.Close, o.Volume);
                        }
                        else
                        {
                            ohlcv.Aggregate(o);
                        }
                    }
                    else
                    {
                        if (ohlcv != null)
                        {
                            yield return ohlcv;
                        }

                        ohlcv = new Ohlcv(o.Time, o.Open, o.High, o.Low, o.Close, o.Volume);
                        dateTime = thresholdDateTime(o, numberOfUnits);
                    }
                }
            }

            if (ohlcv != null)
            {
                yield return ohlcv;
            }
        }

        /// <summary>
        /// Aggregates the <see cref="Scalar"/> enumerable to the given time granularity
        /// and converts it to the <see cref="Ohlcv"/> enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable to aggregate.</param>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <param name="thresholdDateTime">The threshold date and time.</param>
        /// <returns>The aggregated and converted enumerable.</returns>
        internal static IEnumerable<Ohlcv> Aggregate(
            IEnumerable<Scalar> enumerable,
            TimeGranularity timeGranularity,
            Func<TemporalEntity, int, DateTime> thresholdDateTime)
        {
            int numberOfUnits = timeGranularity.NumberOfUnits();
            Ohlcv ohlcv = null;
            if (thresholdDateTime == null)
            {
                int current = 0;
                foreach (var scalar in enumerable)
                {
                    if (ohlcv == null)
                    {
                        ohlcv = Ohlcv.CloneAggregation(scalar);
                    }
                    else
                    {
                        ohlcv.Aggregate(scalar);
                    }

                    if (++current == numberOfUnits)
                    {
                        yield return ohlcv;
                        current = 0;
                        ohlcv = null;
                    }
                }
            }
            else
            {
                var dateTime = DateTime.MinValue;
                foreach (var scalar in enumerable)
                {
                    if (dateTime > scalar.Time)
                    {
                        if (ohlcv == null)
                        {
                            ohlcv = Ohlcv.CloneAggregation(scalar);
                        }
                        else
                        {
                            ohlcv.Aggregate(scalar);
                        }
                    }
                    else
                    {
                        if (ohlcv != null)
                        {
                            yield return ohlcv;
                        }

                        ohlcv = Ohlcv.CloneAggregation(scalar);
                        dateTime = thresholdDateTime(scalar, numberOfUnits);
                    }
                }
            }

            if (ohlcv != null)
            {
                yield return ohlcv;
            }
        }

        /// <summary>
        /// Aggregates the <see cref="Trade"/> enumerable to the given time granularity
        /// and converts it to the <see cref="Ohlcv"/> enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable to aggregate.</param>
        /// <param name="timeGranularity">The time granularity.</param>
        /// <param name="thresholdDateTime">The threshold date and time.</param>
        /// <returns>The aggregated and converted enumerable.</returns>
        internal static IEnumerable<Ohlcv> Aggregate(
            IEnumerable<Trade> enumerable,
            TimeGranularity timeGranularity,
            Func<TemporalEntity, int, DateTime> thresholdDateTime)
        {
            int numberOfUnits = timeGranularity.NumberOfUnits();
            Ohlcv ohlcv = null;
            if (thresholdDateTime == null)
            {
                int current = 0;
                foreach (var trade in enumerable)
                {
                    if (ohlcv == null)
                    {
                        ohlcv = Ohlcv.CloneAggregation(trade);
                    }
                    else
                    {
                        ohlcv.Aggregate(trade);
                    }

                    if (++current == numberOfUnits)
                    {
                        yield return ohlcv;
                        current = 0;
                        ohlcv = null;
                    }
                }
            }
            else
            {
                var dateTime = DateTime.MinValue;
                foreach (var trade in enumerable)
                {
                    if (dateTime > trade.Time)
                    {
                        if (ohlcv == null)
                        {
                            ohlcv = Ohlcv.CloneAggregation(trade);
                        }
                        else
                        {
                            ohlcv.Aggregate(trade);
                        }
                    }
                    else
                    {
                        if (ohlcv != null)
                        {
                            yield return ohlcv;
                        }

                        ohlcv = Ohlcv.CloneAggregation(trade);
                        dateTime = thresholdDateTime(trade, numberOfUnits);
                    }
                }
            }

            if (ohlcv != null)
            {
                yield return ohlcv;
            }
        }

        /// <summary>
        /// Aggregates the <see cref="Scalar"/> enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable to aggregate.</param>
        /// <param name="count">The count.</param>
        /// <param name="thresholdDateTime">The threshold date and time.</param>
        /// <returns>The aggregated and converted enumerable.</returns>
        internal static IEnumerable<Scalar> Aggregate(
            IEnumerable<Scalar> enumerable,
            int count,
            Func<TemporalEntity, int, DateTime> thresholdDateTime)
        {
            var aggregator = new ScalarAggregator();
            if (thresholdDateTime == null)
            {
                int current = 0;
                foreach (var scalar in enumerable)
                {
                    if (current++ == 0)
                    {
                        aggregator.Aggregate(scalar);
                    }
                    else
                    {
                        if (current > count)
                        {
                            current = 0;
                            yield return aggregator.Emit();
                        }
                        else
                        {
                            aggregator.Aggregate(scalar);
                        }
                    }
                }
            }
            else
            {
                var dateTime = DateTime.MinValue;
                foreach (var scalar in enumerable)
                {
                    if (dateTime > scalar.Time)
                    {
                        aggregator.Aggregate(scalar);
                    }
                    else
                    {
                        if (!aggregator.IsEmpty)
                        {
                            yield return aggregator.Emit(/*dateTime or endOfDayTime*/);
                        }

                        dateTime = thresholdDateTime(scalar, count);
                    }
                }
            }

            if (aggregator.IsNotEmpty)
            {
                yield return aggregator.Emit(/*dateTime or endOfDayTime*/);
            }
        }

        private static IEnumerable<Ohlcv> AggregateDays(List<Ohlcv> list, DateTime startTime, DateTime endTime, int count, Func<TemporalEntity, int, DateTime> thresholdDateTime)
        {
            Ohlcv ohlcv = null;
            var dateTime = new DateTime(0L);
            int index, firstIndex = 0, lastIndex = list.Count - 1;
            if (startTime.IsNotZero())
            {
                index = list.FindIndex(o => o.Time >= startTime);
                if (index >= 0)
                {
                    firstIndex = index;
                }
            }

            if (endTime.IsNotZero())
            {
                index = list.FindLastIndex(o => o.Time <= endTime);
                if (index >= 0)
                {
                    lastIndex = index;
                }
            }

            for (index = firstIndex; index <= lastIndex; index++)
            {
                Ohlcv currentOhlcv = list[index];
                if (dateTime > currentOhlcv.Time)
                {
                    if (ohlcv != null)
                    {
                        ohlcv.Aggregate(currentOhlcv);
                    }
                    else
                    {
                        ohlcv = currentOhlcv.CloneAggregation();
                    }
                }
                else
                {
                    if (ohlcv != null)
                    {
                        yield return ohlcv;
                    }

                    ohlcv = currentOhlcv.CloneAggregation();
                    dateTime = thresholdDateTime(currentOhlcv, count);
                }
            }

            if (ohlcv != null)
            {
                yield return ohlcv;
            }
        }

        private static IEnumerable<Scalar> AggregateDays(List<Scalar> list, DateTime startTime, DateTime endTime, int count, Func<TemporalEntity, int, DateTime> thresholdDateTime)
        {
            var aggregator = new ScalarAggregator();
            var dateTime = new DateTime(0L);
            int index, firstIndex = 0, lastIndex = list.Count - 1;
            if (startTime.IsNotZero())
            {
                index = list.FindIndex(o => o.Time >= startTime);
                if (index >= 0)
                {
                    firstIndex = index;
                }
            }

            if (endTime.IsNotZero())
            {
                index = list.FindLastIndex(o => o.Time <= endTime);
                if (index >= 0)
                {
                    lastIndex = index;
                }
            }

            for (index = firstIndex; index <= lastIndex; index++)
            {
                Scalar scalar = list[index];
                if (dateTime > scalar.Time)
                {
                    aggregator.Aggregate(scalar);
                }
                else
                {
                    if (!aggregator.IsEmpty)
                    {
                        yield return aggregator.Emit(/*dateTime*/);
                    }

                    dateTime = thresholdDateTime(scalar, count);
                }
            }

            if (!aggregator.IsEmpty)
            {
                yield return aggregator.Emit();
            }
        }

        private static long ConvertToSeconds(TimeGranularity timeGranularity)
        {
            int numberOfUnits = timeGranularity.NumberOfUnits();
            if (timeGranularity.IsSecond())
            {
                return numberOfUnits;
            }

            if (timeGranularity.IsMinute())
            {
                return 60L * numberOfUnits;
            }

            if (timeGranularity.IsHour())
            {
                return 3600L * numberOfUnits;
            }

            if (timeGranularity.IsDay())
            {
                return 24L * 3600L * numberOfUnits;
            }

            if (timeGranularity.IsWeek())
            {
                return 7L * 24L * 3600L * numberOfUnits;
            }

            if (timeGranularity.IsMonth())
            {
                return 30L * 7L * 24L * 3600L * numberOfUnits;
            }

            if (timeGranularity.IsYear())
            {
                return 365L * 24L * 3600L * numberOfUnits;
            }

            return 0;
        }
    }
}
