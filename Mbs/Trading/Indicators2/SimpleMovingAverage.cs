using System;
using System.Collections.Generic;
using System.Globalization;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators2.Abstractions;

namespace Mbs.Trading.Indicators2
{
    /// <summary>
    /// A simple, or arithmetic, moving average (SMA) is calculated by adding the samples
    /// for a number of time periods (length, <c>ℓ</c>) and then dividing this total by the number of time periods.
    /// In other words, this is an unweighted mean (gives equal weight to each sample) of the previous <c>ℓ</c> samples.
    /// <para>This implementation updates the value of the SMA incrementally using the formula:</para>
    /// <para><c>SMAᵢ = SMAᵢ₋₁ + (Pᵢ - Pᵢ₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
    /// <para>The indicator is not primed during the first <c>ℓ-1</c> updates.</para>
    /// </summary>
    public sealed class SimpleMovingAverage : Indicator, ILineIndicator
    {
        #region Members and accessors
        /// <summary>
        /// The length (the number of time periods) of the simple moving average.
        /// </summary>
        public int Length { get; }

        private double value = double.NaN;
        /// <summary>
        /// The current value of the the simple moving average, or <c>NaN</c> if not primed.
        /// The indicator is not primed during the first <c>ℓ-1</c> updates, where e <c>ℓ</c> is the length.
        /// </summary>
        public double Value { get { lock (updateLock) { return value; } } }

        private readonly int lastIndex;
        private int windowCount;
        private readonly double[] window;
        private double windowSum;

        private const string Sma = "SMA";
        private const string SmaFull = "Simple Moving Average";
        private const string ArgumentLength = "length";
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a new instance of the <see cref="SimpleMovingAverage"/> class.
        /// </summary>
        /// <param name="length">The number of time periods of the simple moving average.</param>
        /// <param name="ohlcvComponent">The Ohlcv component.</param>
        public SimpleMovingAverage(int length, OhlcvComponent ohlcvComponent = OhlcvComponent.ClosingPrice)
            : base(Sma, SmaFull, ohlcvComponent)
        {
            if (2 > length)
                throw new ArgumentOutOfRangeException(ArgumentLength);
            Length = length;
            lastIndex = length - 1;
            window = new double[length];
            Moniker = string.Concat(Sma, length.ToString(CultureInfo.InvariantCulture));
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
                windowSum = 0d;
                value = double.NaN;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the value of the simple moving average using the formula:
        /// <para><c>SMAᵢ = SMAᵢ₋₁ + (Pᵢ - Pᵢ₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public double Update(double sample)
        {
            if (double.IsNaN(sample))
                return sample;
            lock (updateLock)
            {
                if (primed)
                {
                    windowSum += sample - window[0];
                    Array.Copy(window, 1, window, 0, lastIndex);
                    //for (int i = 0; i < lastIndex; )
                    //    window[i] = window[++i];
                    window[lastIndex] = sample;
                    value = windowSum / Length;
                }
                else // Not primed.
                {
                    windowSum += sample;
                    window[windowCount] = sample;
                    if (Length == ++windowCount)
                    {
                        primed = true;
                        value = windowSum / windowCount;
                    }
                }
                return value;
            }
        }

        /// <summary>
        /// Updates the value of the simple moving average using the formula:
        /// <para><c>SMAᵢ = SMAᵢ₋₁ + (Pᵢ - Pᵢ₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <param name="dateTime">A date-time of the new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(double sample, DateTime dateTime) => new Scalar(dateTime, Update(sample));

        /// <summary>
        /// Updates the value of the simple moving average using the formula:
        /// <para><c>SMAᵢ = SMAᵢ₋₁ + (Pᵢ - Pᵢ₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="scalar">A new <see cref="Scalar"/>.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(Scalar scalar) => new Scalar(scalar.Time, Update(scalar.Value));

        /// <summary>
        /// Updates the value of the simple moving average using the formula:
        /// <para><c>SMAᵢ = SMAᵢ₋₁ + (Pᵢ - Pᵢ₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="ohlcv">A new <see cref="Ohlcv"/>.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(Ohlcv ohlcv) => new Scalar(ohlcv.Time, Update(ohlcv.Component(OhlcvComponent)));
        #endregion

        #region Calculate
        /// <summary>
        /// Calculates the simple moving average from the input array using the formula:
        /// <para><c>SMAᵢ = SMAᵢ₋₁ + (Pᵢ - Pᵢ₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="sampleList">The sample list.</param>
        /// <param name="length">The number of time periods of the simple moving average.</param>
        /// <returns>A list of the simple moving average values.</returns>
        public static List<double> Calculate(List<double> sampleList, int length)
        {
            if (2 > length)
                throw new ArgumentOutOfRangeException(ArgumentLength);
            int i, count = sampleList.Count;
            var resultList = new List<double>(count);
            if (count < length)
            {
                for (i = 0; i < count; i++)
                    resultList.Add(double.NaN);
            }
            else
            {
                double v, sum = 0d, w = length;
                int l = length - 1;
                for (i = 0; i < l;)
                {
                    v = sampleList[i++];
                    if (!double.IsNaN(v))
                        sum += v;
                    resultList.Add(double.NaN);
                }
                v = sampleList[l];
                if (!double.IsNaN(v))
                    sum += v;
                resultList.Add(sum / w);
                l = 0;
                for (i = length; i < count;)
                {
                    v = sampleList[i++];
                    if (!double.IsNaN(v))
                        sum += v;
                    v = sampleList[l++];
                    if (!double.IsNaN(v))
                        sum -= v;
                    resultList.Add(sum / w);
                }
            }
            return resultList;
        }
        #endregion
    }
}
