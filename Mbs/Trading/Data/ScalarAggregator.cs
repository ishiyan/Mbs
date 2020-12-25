using System;

namespace Mbs.Trading.Data
{
    /// <summary>
    /// Aggregates scalars using a calculated average value over an aggregation bin.
    /// </summary>
    internal class ScalarAggregator
    {
        private double sum;
        private DateTime dateTime;
        private int count;

        /// <summary>
        /// Gets a value indicating whether the aggregation bin is empty.
        /// </summary>
        internal bool IsEmpty => count == 0;

        /// <summary>
        /// Gets a value indicating whether the aggregation bin is not empty.
        /// </summary>
        internal bool IsNotEmpty => count > 0;

        /// <summary>
        /// Accumulates the scalar by updating the average value and the date and time of the aggregation.
        /// </summary>
        /// <param name="scalar">The scalar to aggregate.</param>
        internal void Aggregate(Scalar scalar)
        {
            sum += scalar.Value;
            dateTime = scalar.Time;
            ++count;
        }

        /// <summary>
        /// Emits a scalar object from the aggregation bin with the date and time of the last aggregated scalar, emptying the aggregation bin.
        /// </summary>
        /// <returns>The scalar object.</returns>
        internal Scalar Emit()
        {
            var scalar = new Scalar(dateTime, sum / count);
            sum = 0.0;
            count = 0;
            return scalar;
        }

        /// <summary>
        /// Emits a scalar object from the aggregation bin with the date of the last aggregated scalar and the specified time, emptying the aggregation bin.
        /// </summary>
        /// <param name="timeSpan">The time of the scalar to emit.</param>
        /// <returns>The scalar object.</returns>
        internal Scalar Emit(TimeSpan timeSpan)
        {
            var scalar = new Scalar(dateTime.Date.Add(timeSpan), sum / count);
            sum = 0.0;
            count = 0;
            return scalar;
        }
    }
}
