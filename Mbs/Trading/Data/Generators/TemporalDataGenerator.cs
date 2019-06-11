using System;
using System.Collections.Generic;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// A generic temporal data generator.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public abstract class TemporalDataGenerator<T> : ISyntheticDataGenerator<T>
        where T : TemporalEntity
    {
        /// <inheritdoc />
        public string Name { get; protected set; }

        /// <inheritdoc />
        public string Moniker { get; protected set; }

        /// <summary>
        /// Gets the time of the beginning of the trading session.
        /// </summary>
        public TimeSpan SessionBeginTime { get; }

        /// <summary>
        /// Gets the end time of the trading session.
        /// </summary>
        public TimeSpan SessionEndTime { get; }

        /// <summary>
        /// Gets the date of the first data sample.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the time granularity of data samples.
        /// </summary>
        public TimeGranularity TimeGranularity { get; }

        /// <summary>
        /// Gets a value specifying an exchange holiday schedule or a general country holiday schedule.
        /// Business days do not form a continuous sequence (weekends, holidays etc.),
        /// so there is a need in differentiating between the business time and the physical time.
        /// </summary>
        public BusinessDayCalendar BusinessDayCalendar { get; }

        private readonly bool isIntraday;
        private readonly TimeSpan timeSpan;
        private DateTime dateTimePrevious;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporalDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="sessionBeginTime">The time of the beginning of the trading session.</param>
        /// <param name="sessionEndTime">The end time of the trading session.</param>
        /// <param name="startDate">The date of the first data sample.</param>
        /// <param name="timeGranularity">The time granularity of data samples.</param>
        /// <param name="businessDayCalendar">The value specifying an exchange holiday schedule or a general country holiday schedule.</param>
        protected TemporalDataGenerator(
            TimeSpan sessionBeginTime,
            TimeSpan sessionEndTime,
            DateTime startDate,
            TimeGranularity timeGranularity,
            BusinessDayCalendar businessDayCalendar)
        {
            SessionBeginTime = sessionBeginTime;
            SessionEndTime = sessionEndTime;
            StartDate = startDate;
            TimeGranularity = timeGranularity;
            BusinessDayCalendar = businessDayCalendar;

            timeSpan = TimeGranularity.TimeSpan();
            isIntraday = TimeGranularity.IsIntraday();
            dateTimePrevious = DateTime.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporalDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="timeParameters">The time-related input parameters for temporal data generators.</param>
        protected TemporalDataGenerator(TimeParameters timeParameters)
            : this(
                timeParameters.SessionStartTime,
                timeParameters.SessionEndTime,
                timeParameters.StartDate,
                timeParameters.TimeGranularity,
                timeParameters.BusinessDayCalendar)
        {
        }

        /// <summary>
        /// Generates the date and time for the next data sample.
        /// </summary>
        /// <returns>The generated date and time.</returns>
        protected DateTime NextTime()
        {
            if (dateTimePrevious == DateTime.MinValue)
            {
                dateTimePrevious = StartDate.Add(isIntraday ? SessionBeginTime : SessionEndTime);
                return dateTimePrevious;
            }

            DateTime t = dateTimePrevious.Advance(timeSpan, BusinessDayCalendar);
            if (isIntraday && t.TimeOfDay > SessionEndTime)
            {
                t = new DateTime(t.Year, t.Month, t.Day).Add(SessionBeginTime);
                while (t.IsBusinessHoliday(BusinessDayCalendar))
                    t = t.AddDays(1);
            }

            dateTimePrevious = t;
            return t;
        }

        /// <inheritdoc />
        public abstract T GenerateNext();

        /// <inheritdoc />
        public IEnumerable<T> GenerateNext(int count)
        {
            while (--count >= 0)
                yield return GenerateNext();
        }

        /// <inheritdoc />
        public virtual void Reset()
        {
            dateTimePrevious = DateTime.MinValue;
        }
    }
}
