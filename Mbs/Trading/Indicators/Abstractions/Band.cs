﻿using System;

namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// Holds two band values and a time stamp.
    /// </summary>
    public class Band
    {
        /// <summary>
        /// The first value.
        /// </summary>
        public double FirstValue { get; } = double.NaN;

        /// <summary>
        /// The second value.
        /// </summary>
        public double SecondValue { get; } = double.NaN;

        /// <summary>
        /// The time stamp.
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Indicates if this <see cref="Band"/> is not initialized.
        /// </summary>
        public bool IsEmpty => double.IsNaN(FirstValue) || double.IsNaN(SecondValue);

        /// <summary>
        /// Constructs a new instance of the <see cref="Band"/> class.
        /// </summary>
        /// <param name="time">The time stamp of the <see cref="Band"/>.</param>
        /// <param name="firstValue">The first value of the <see cref="Band"/>.</param>
        /// <param name="secondValue">The second value of the <see cref="Band"/>.</param>
        public Band(DateTime time, double firstValue = double.NaN, double secondValue = double.NaN)
        {
            Time = time;

            if (!double.IsNaN(firstValue))
            {
                FirstValue = firstValue;
            }

            if (!double.IsNaN(secondValue))
            {
                SecondValue = secondValue;
            }
        }
    }
}
