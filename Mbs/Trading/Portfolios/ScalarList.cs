using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mbs.Trading.Data;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// An updateable list of scalars.
    /// </summary>
    internal class ScalarList
    {
        private readonly List<Scalar> list = new List<Scalar>();
        private readonly object updateLock = new object();
        private Scalar scalar = new Scalar(new DateTime(0L));
        // private double valueOld;

        /// <summary>
        /// Gets the read-only collection of scalars.
        /// </summary>
        internal ReadOnlyCollection<Scalar> Collection
        {
            get
            {
                lock (updateLock)
                {
                    return list.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        internal double Value
        {
            get
            {
                lock (updateLock)
                {
                    return scalar.Value;
                }
            }
        }

        /// <summary>
        /// Creates a new scalar and appends it to the list.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="value">The value.</param>
        internal void Add(DateTime dateTime, double value)
        {
            lock (updateLock)
            {
                if (list.Count == 0)
                {
                    scalar.Time = dateTime;
                    scalar.Value = value;
                    list.Add(scalar);
                }
                else if (scalar.Time < dateTime)
                {
                    scalar = new Scalar(dateTime, value);
                    list.Add(scalar);
                }
                else if (scalar.Time == dateTime)
                {
                    scalar.Value = value;
                }
            }
        }

        /// <summary>
        /// Creates a new scalar with an accumulated value and appends it to the list.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="value">The value.</param>
        internal void Accumulate(DateTime dateTime, double value)
        {
            lock (updateLock)
            {
                if (list.Count == 0)
                {
                    scalar.Time = dateTime;
                    scalar.Value = value;
                    list.Add(scalar);
                }
                else
                {
                    scalar = new Scalar(dateTime, value + scalar.Value);
                    list.Add(scalar);
                    //valueOld = scalar.Value;
                }
            }
        }

        /// <summary>
        /// Re-sets the list data.
        /// </summary>
        internal void Reset()
        {
            lock (updateLock)
            {
                scalar = new Scalar(new DateTime(0L));
                list.Clear();
            }
        }
    }
}
