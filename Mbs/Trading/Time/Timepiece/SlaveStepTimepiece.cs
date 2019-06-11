using System;
using System.Collections.Generic;

namespace Mbs.Trading.Time
{
    /// <summary>
    /// An externally synchronized step-time timepiece.
    /// </summary>
    public class SlaveStepTimepiece : ITimepiece
    {
        private readonly object reminderDictionaryLock = new object();
        private readonly SortedDictionary<DateTime, List<Action>> reminderDictionary = new SortedDictionary<DateTime, List<Action>>();
        private readonly object timeChangedLock = new object();
        private readonly object newDayLock = new object();
        private readonly object daySessionBeginLock = new object();
        private readonly object daySessionEndLock = new object();

        private Action<DateTime> timeChanged;
        private DateTime currentDateTime;
        private Action<DateTime> newDay;
        private Action<DateTime> daySessionBegin;
        private Action<DateTime> daySessionEnd;

        /// <summary>
        /// Gets or sets the day trading session begin time.
        /// </summary>
        public TimeSpan SessionBeginTime { get; set; }

        /// <summary>
        /// Gets or sets the day trading session end time.
        /// </summary>
        public TimeSpan SessionEndTime { get; set; }

        private void OnTimeChanged(DateTime dateTime)
        {
            lock (timeChangedLock)
            {
                if (null != timeChanged)
                {
                    Delegate[] handlers = timeChanged.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var subscriber = handler as Action<DateTime>;
                        subscriber?.Invoke(dateTime);
                    }
                }
            }
        }

        /// <summary>
        /// Notifies the time change.
        /// </summary>
        public event Action<DateTime> TimeChanged
        {
            add
            {
                lock (timeChangedLock)
                {
                    timeChanged += value;
                }
            }

            remove
            {
                lock (timeChangedLock)
                {
                    timeChanged -= value;
                }
            }
        }

        /// <inheritdoc />
        public DateTime Time
        {
            get
            {
                return currentDateTime;
            }

            private set
            {
                if (value > currentDateTime)
                {
                    CheckReminder(value, true);
                    currentDateTime = value;
                    OnTimeChanged(value);
                    CheckReminder(value, false);
                }
            }
        }

        /// <summary>
        /// Sets the initial date and time.
        /// </summary>
        /// <param name="dateTime">The date and time to set.</param>
        public void SetInitialTime(DateTime dateTime)
        {
            currentDateTime = dateTime;
        }

        /// <inheritdoc />
        public bool IsWeekend
        {
            get
            {
                DayOfWeek dayOfWeek = currentDateTime.DayOfWeek;
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
                    daySessionEnd -= value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlaveStepTimepiece"/> class.
        /// </summary>
        /// <param name="beginSessionTime">Session begin time.</param>
        /// <param name="endSessionTime">Session end time.</param>
        public SlaveStepTimepiece(TimeSpan beginSessionTime, TimeSpan endSessionTime)
        {
            SessionBeginTime = beginSessionTime;
            SessionEndTime = endSessionTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlaveStepTimepiece"/> class.
        /// </summary>
        public SlaveStepTimepiece()
        {
            SessionBeginTime = new TimeSpan(9, 0, 0);
            SessionEndTime = new TimeSpan(17, 30, 0);
        }

        /// <summary>
        /// Synchronize with the given date and time.
        /// </summary>
        /// <param name="dateTime">The date and time to synchronize with.</param>
        public void Synchronize(DateTime dateTime)
        {
            DateTime date = dateTime.Date;
            TimeSpan time = dateTime.TimeOfDay;
            DateTime previousDateTime = currentDateTime;
            DateTime previousDate = previousDateTime.Date;
            TimeSpan previousTime = previousDateTime.TimeOfDay;
            if (date > previousDate)
            {
                int fullDays = (int)(date - previousDate).TotalDays - 1;
                if (previousTime <= SessionEndTime)
                {
                    if (previousTime < SessionBeginTime)
                    {
                        Time = previousDate.Add(SessionBeginTime);
                        OnDaySessionBegin(currentDateTime);
                    }

                    Time = previousDate.Add(SessionEndTime);
                    OnDaySessionEnd(currentDateTime);
                }

                for (int i = 0; i < fullDays; ++i)
                {
                    previousDate = previousDate.AddDays(1);
                    Time = previousDate;
                    OnNewDay(currentDateTime);

                    // TODO: If not weekend or something.
                    Time = previousDate.Add(SessionBeginTime);
                    OnDaySessionBegin(currentDateTime);
                    Time = previousDate.Add(SessionEndTime);
                    OnDaySessionEnd(currentDateTime);
                }

                previousDate = previousDate.AddDays(1);
                Time = previousDate;
                OnNewDay(currentDateTime);
                if (time >= SessionBeginTime)
                {
                    Time = previousDate.Add(SessionBeginTime);
                    OnDaySessionBegin(currentDateTime);
                    if (time > SessionEndTime)
                    {
                        Time = previousDate.Add(SessionEndTime);
                        OnDaySessionEnd(currentDateTime);
                    }
                }

                if (currentDateTime != dateTime)
                    Time = dateTime;
            }
            else if (time > previousTime)
            {
                if (previousTime < SessionBeginTime)
                {
                    if (time >= SessionBeginTime)
                    {
                        Time = previousDate.Add(SessionBeginTime);
                        OnDaySessionBegin(currentDateTime);
                        if (time > SessionEndTime)
                        {
                            Time = previousDate.Add(SessionEndTime);
                            OnDaySessionEnd(currentDateTime);
                        }
                    }
                }
                else
                {
                    if (previousTime <= SessionEndTime)
                    {
                        if (time > SessionEndTime)
                        {
                            Time = previousDate.Add(SessionEndTime);
                            OnDaySessionEnd(currentDateTime);
                        }
                    }
                }

                if (currentDateTime != dateTime)
                    Time = dateTime;
            }
        }

        /// <inheritdoc />
        public void AddReminder(DateTime dateTime, Action action)
        {
            if (dateTime <= currentDateTime)
                return;
            lock (reminderDictionaryLock)
            {
                if (reminderDictionary.TryGetValue(dateTime, out List<Action> actionList))
                {
                    actionList.Add(action);
                }
                else
                {
                    actionList = new List<Action> { action };
                    reminderDictionary.Add(dateTime, actionList);
                }
            }
        }

        /// <inheritdoc />
        public void RemoveReminder(DateTime dateTime, Action action)
        {
            lock (reminderDictionaryLock)
            {
                if (reminderDictionary.TryGetValue(dateTime, out List<Action> actionList))
                {
                    actionList.Remove(action);
                    if (actionList.Count == 0)
                        reminderDictionary.Remove(dateTime);
                }
            }
        }

        /// <inheritdoc />
        public void RemoveReminder(Action action)
        {
            List<Action> actionList = null;
            bool removed = false;
            var removedKey = default(DateTime);
            lock (reminderDictionaryLock)
            {
                foreach (DateTime key in reminderDictionary.Keys)
                {
                    actionList = reminderDictionary[key];
                    if (actionList.Remove(action))
                    {
                        removed = true;
                        removedKey = key;
                        break;
                    }
                }

                if (removed && actionList.Count == 0)
                    reminderDictionary.Remove(removedKey);
            }
        }

        /// <summary>
        /// Removes all reminders optionally executing them.
        /// </summary>
        /// <param name="execute">Execute reminders.</param>
        public void RemoveReminders(bool execute)
        {
            var actionList = new List<Action>();
            lock (reminderDictionaryLock)
            {
                foreach (DateTime key in reminderDictionary.Keys)
                {
                    // TODO: ??????
                    if (execute)
                        actionList.AddRange(reminderDictionary[key]);
                }

                reminderDictionary.Clear();
            }

            if (execute)
            {
                foreach (Action action in actionList)
                    action();
            }
        }

        private void CheckReminder(DateTime dateTime, bool before)
        {
            var actionList = new List<Action>();
            var keyList = new List<DateTime>();
            lock (reminderDictionaryLock)
            {
                if (before)
                {
                    foreach (DateTime key in reminderDictionary.Keys)
                    {
                        if (key >= dateTime)
                            break;
                        actionList.AddRange(reminderDictionary[key]);
                        keyList.Add(key);
                    }
                }
                else
                {
                    foreach (DateTime key in reminderDictionary.Keys)
                    {
                        if (key > dateTime)
                            break;
                        actionList.AddRange(reminderDictionary[key]);
                        keyList.Add(key);
                    }
                }

                foreach (DateTime key in keyList)
                    reminderDictionary.Remove(key);
            }

            foreach (Action action in actionList)
                action();
        }
    }
}
