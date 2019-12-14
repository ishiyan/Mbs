using System;
using System.Collections.Generic;
using System.Globalization;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;

namespace Mbs.Trading.Indicators
{
    /// <summary>
    /// Momentum is the absolute (not normalized) difference between today's sample and the sample <c>ℓ</c> periods ago.
    /// <para>The indicator is not primed during the first <c>ℓ</c> updates.</para>
    /// </summary>
    public sealed class Momentum : LineIndicator
    {
        #region Members and accessors
        /// <summary>
        /// The length <c>ℓ</c> (the number of time periods).
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// The current value of the momentum, or <c>NaN</c> if not primed.
        /// The indicator is not primed during the first <c>ℓ</c> updates, where <c>ℓ</c> is the length.
        /// </summary>
        public double Value { get { lock (updateLock) { return value; } } }

        private int windowCount;
        private readonly double[] window;
        private double value = double.NaN;

        private const string Mom = "mom";
        private const string MomFull = "Momentum";
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a new instance of the class.
        /// </summary>
        /// <param name="length">The number of time periods, <c>ℓ</c>.</param>
        /// <param name="ohlcvComponent">The Ohlcv component.</param>
        public Momentum(int length, OhlcvComponent ohlcvComponent = OhlcvComponent.ClosingPrice)
            : base(Mom, MomFull, ohlcvComponent)
        {
            if (1 > length)
                throw new ArgumentOutOfRangeException(nameof(length));
            Length = length;
            window = new double[length + 1];
            Moniker = string.Concat(Mom, length.ToString(CultureInfo.InvariantCulture));
        }
        #endregion

        #region Reset
        /// <inheritdoc />
        public override void Reset()
        {
            lock (updateLock)
            {
                primed = false;
                windowCount = 0;
                value = double.NaN;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the value of the indicator.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public override double Update(double sample)
        {
            if (double.IsNaN(sample))
                return sample;
            lock (updateLock)
            {
                if (primed)
                {
                    if (Length == 1)
                        window[0] = window[1];
                    else
                        Array.Copy(window, 1, window, 0, Length);
                    window[Length] = sample;
                    value = sample - window[0];
                }
                else
                {
                    window[windowCount] = sample;
                    if (Length + 1 == ++windowCount)
                    {
                        primed = true;
                        value = sample - window[0];
                    }
                }
                return value;
            }
        }
        #endregion

        #region Calculate
        /// <summary>
        /// Calculates a list of values of the momentum from the input array.
        /// The indicator is not primed during the first <c>ℓ</c> updates.
        /// </summary>
        /// <param name="sampleList">The sample list.</param>
        /// <param name="length">The number of time periods, <c>ℓ</c>.</param>
        /// <returns>A list of indicator values.</returns>
        public static List<double> Calculate(List<double> sampleList, int length)
        {
            if (1 > length)
                throw new ArgumentOutOfRangeException(nameof(length));
            int count = sampleList.Count;
            var resultList = new List<double>(count);
            if (count <= length)
            {
                for (int i = 0; i < count; ++i)
                    resultList.Add(double.NaN);
            }
            else
            {
                for (int i = 0; i < length; ++i)
                    resultList.Add(double.NaN);
                for (int i = 0, j = length; j < count; ++j, ++i)
                {
                    double temp = sampleList[j] - sampleList[i];
                    resultList.Add(temp);
                }
            }
            return resultList;
        }
        #endregion
    }
}
