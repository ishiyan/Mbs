using System;
using System.Collections.Generic;

namespace Mbs.Trading.Time
{
    /// <summary>
    /// The local real-time timepiece.
    /// </summary>
    public class LocalTimepiece : ITimepiece
    {
        private readonly TimeSpan beginSessionTime;
        private readonly TimeSpan endSessionTime;
        private readonly object reminderDictionaryLock = new object();
        private readonly object newDayLock = new object();
        private readonly object daySessionBeginLock = new object();
        private readonly object daySessionEndLock = new object();
        private readonly SortedDictionary<DateTime, List<Action>> reminderDictionary = new SortedDictionary<DateTime, List<Action>>();

        private Action<DateTime> newDay;
        private Action<DateTime> daySessionBegin;
        private Action<DateTime> daySessionEnd;

        /// <inheritdoc />
        public DateTime Time => DateTime.Now;

        /// <inheritdoc />
        public bool IsWeekend
        {
            get
            {
                DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;
                return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
            }
        }

        /// <inheritdoc />
        public bool IsHoliday => false;

        private void OnNewDay(DateTime dateTime)
        {
            lock (newDayLock)
            {
                if (null != newDay)
                {
                    Delegate[] handlers = newDay.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var subscriber = handler as Action<DateTime>;
                        subscriber?.Invoke(dateTime);
                    }
                }
            }
        }

        /// <inheritdoc />
        public event Action<DateTime> NewDay
        {
            add
            {
                lock (newDayLock)
                {
                    newDay += value;
                }
            }

            remove
            {
                lock (newDayLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    newDay -= value;
                }
            }
        }

        private void OnDaySessionBegin(DateTime dateTime)
        {
            lock (daySessionBeginLock)
            {
                if (null != daySessionBegin)
                {
                    Delegate[] handlers = daySessionBegin.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var subscriber = handler as Action<DateTime>;
                        subscriber?.Invoke(dateTime);
                    }
                }
            }
        }

        /// <inheritdoc />
        public event Action<DateTime> DaySessionBegin
        {
            add
            {
                lock (daySessionBeginLock)
                {
                    daySessionBegin += value;
                }
            }

            remove
            {
                lock (daySessionBeginLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    daySessionBegin -= value;
                }
            }
        }

        private void OnDaySessionEnd(DateTime dateTime)
        {
            lock (daySessionEndLock)
            {
                if (null != daySessionEnd)
                {
                    Delegate[] handlers = daySessionEnd.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var subscriber = handler as Action<DateTime>;
                        subscriber?.Invoke(dateTime);
                    }
                }
            }
        }

        /// <inheritdoc />
        public event Action<DateTime> DaySessionEnd
        {
            add
            {
                lock (daySessionEndLock)
                {
                    daySessionEnd += value;
                }
            }

            remove
            {
                lock (daySessionEndLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    daySessionEnd -= value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalTimepiece"/> class.
        /// </summary>
        /// <param name="beginSessionTime">Session begin time.</param>
        /// <param name="endSessionTime">Session end time.</param>
        public LocalTimepiece(TimeSpan beginSessionTime, TimeSpan endSessionTime)
        {
            this.beginSessionTime = beginSessionTime;
            this.endSessionTime = endSessionTime;
        }

        /// <inheritdoc />
        public void AddReminder(DateTime dateTime, Action action)
        {
        }

        /// <inheritdoc />
        public void RemoveReminder(DateTime dateTime, Action action)
        {
        }

        /// <inheritdoc />
        public void RemoveReminder(Action action)
        {
        }

/*
        /// <summary>
        /// Removes all reminders optionally executing them.
        /// </summary>
        /// <param name="execute">Execute reminders.</param>
        public static void RemoveReminders(bool execute)
        {
        }
*/
    }
}
