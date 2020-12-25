using System;

namespace Mbs.Trading.Time.Timepieces
{
    /// <summary>
    /// The timepiece interface.
    /// </summary>
    public interface ITimepiece
    {
        /// <summary>
        /// Notifies at the beginning of a new day.
        /// </summary>
        event Action<DateTime> NewDay;

        /// <summary>
        /// Notifies at the beginning of a new day session.
        /// </summary>
        event Action<DateTime> DaySessionBegin;

        /// <summary>
        /// Notifies at the end of a day session.
        /// </summary>
        event Action<DateTime> DaySessionEnd;

        /// <summary>
        /// Gets the current date and time.
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// Gets a value indicating whether the current date is a weekend.
        /// </summary>
        bool IsWeekend { get; }

        /// <summary>
        /// Gets a value indicating whether the current date is a holiday.
        /// </summary>
        bool IsHoliday { get; }

        /// <summary>
        /// Adds a reminder action.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="action">The action.</param>
        void AddReminder(DateTime dateTime, Action action);

        /// <summary>
        /// Removes a first occurrence of a reminder action without execution.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="action">The action.</param>
        void RemoveReminder(DateTime dateTime, Action action);

        /// <summary>
        /// Removes a first occurrence of a reminder action without execution.
        /// </summary>
        /// <param name="action">The action.</param>
        void RemoveReminder(Action action);
    }
}
