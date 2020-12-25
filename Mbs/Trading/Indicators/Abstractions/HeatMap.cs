using System;

namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// Holds a time stamp (x) and an array of values (z) corresponding to parameter (y) range to paint a <see cref="HeatMap"/> column.
    /// </summary>
    public sealed class HeatMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeatMap"/> class.
        /// </summary>
        /// <param name="time">The time stamp (x) of the <see cref="HeatMap"/>.</param>
        /// <param name="parameterFirst">The first parameter (y) value of the <see cref="HeatMap"/>. This value is the same for all columns.</param>
        /// <param name="parameterLast">The last parameter (y) of the <see cref="HeatMap"/>. This value is the same for all columns.</param>
        /// <param name="parameterResolution">The parameter resolution (positive number). A value of 10 means that intensity is evaluated at every 0.1 of parameter range.</param>
        /// <param name="valueMin">The minimal value (z) of the <see cref="HeatMap"/> column.</param>
        /// <param name="valueMax">The maximal value (z) of the <see cref="HeatMap"/> column.</param>
        /// <param name="values">The values (z) of the <see cref="HeatMap"/> column.</param>
        public HeatMap(DateTime time, double parameterFirst, double parameterLast, double parameterResolution, double valueMin = double.NaN, double valueMax = double.NaN, double[] values = null)
        {
            Time = time;
            ParameterFirst = parameterFirst;
            ParameterLast = parameterLast;
            ParameterResolution = parameterResolution;
            ValueMin = valueMin;
            ValueMax = valueMax;
            Values = values;
        }

        /// <summary>
        /// Gets a time stamp (x).
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Gets the first parameter (y) value of the <see cref="HeatMap"/>. This value is the same for all columns.
        /// A parameter corresponding to the i-th value can be calculated as <c>min(ParameterFirst,ParameterLast) + i / ParameterResolution</c>.
        /// </summary>
        public double ParameterFirst { get; }

        /// <summary>
        /// Gets the last parameter (y) of the <see cref="HeatMap"/>. This value is the same for all columns.
        /// A parameter corresponding to the i-th value can be calculated as <c>min(ParameterFirst,ParameterLast) + i / ParameterResolution</c>.
        /// </summary>
        public double ParameterLast { get; }

        /// <summary>
        /// Gets a parameter resolution value (positive number). A value of 10 means that <see cref="HeatMap"/> values are evaluated at every 0.1 of parameter range.
        /// A parameter corresponding to the i-th value can be calculated as <c>min(ParameterFirst,ParameterLast) + i / ParameterResolution</c>.
        /// </summary>
        public double ParameterResolution { get; }

        /// <summary>
        /// Gets a minimal value (z) of the <see cref="HeatMap"/> column.
        /// </summary>
        public double ValueMin { get; }

        /// <summary>
        /// Gets a maximal value (z) of the <see cref="HeatMap"/> column.
        /// </summary>
        public double ValueMax { get; }

        /// <summary>
        /// Gets the values (z) of the <see cref="HeatMap"/> column.
        /// </summary>
        public double[] Values { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="HeatMap"/> is not initialized.
        /// </summary>
        public bool IsEmpty => Values == null;
    }
}
