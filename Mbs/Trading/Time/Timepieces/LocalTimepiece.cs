using System;
using System.Collections.Generic;

// ReSharper disable DelegateSubtraction
namespace Mbs.Trading.Time.Timepieces
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
                    newDay -= value;
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
                    daySessionEnd -= value;
                }
            }
        }

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

        /// <inheritdoc />
        public void AddReminder(DateTime dateTime, Action action)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc />
        public void RemoveReminder(DateTime dateTime, Action action)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc />
        public void RemoveReminder(Action action)
        {
            // Method intentionally left empty.
        }

        private void OnNewDay(DateTime dateTime)
        {
            lock (newDayLock)
            {
                if (newDay != null)
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

        private void OnDaySessionBegin(DateTime dateTime)
        {
            lock (daySessionBeginLock)
            {
                if (daySessionBegin != null)
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

        private void OnDaySessionEnd(DateTime dateTime)
        {
            lock (daySessionEndLock)
            {
                if (daySessionEnd != null)
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
    }
}
