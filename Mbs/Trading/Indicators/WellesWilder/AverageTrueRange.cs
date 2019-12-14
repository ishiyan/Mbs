﻿using System;
using System.Globalization;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;

namespace Mbs.Trading.Indicators.WellesWilder
{
    /// <summary>
    /// Average True Range (ATR) averages True Range (TR) values over the specified length <c>ℓ</c> using the Wilder method:
    /// <para>➊ multiply the previous value by the <c>ℓ-1</c></para>
    /// <para>➋ add the current value</para>
    /// <para>➌ divide by <c>ℓ</c>.</para>
    /// This method have an unstable period comparable to an Exponential Moving Average.
    /// <para>The True Range is defined by Welles Wilder as the largest of:</para>
    /// <para>➊ the distance from today's <c>high</c> to today's <c>low</c></para>
    /// <para>➋ the distance from yesterday's <c>close</c> to today's <c>high</c></para>
    /// <para>➌ the distance from yesterday's <c>close</c> to today's <c>low</c>.</para>
    /// <para>The True Range is the basis of the Average True Range indicator.</para>
    /// <para>This implementation calculates the very first True Range value as the <c>high - low</c> of the first bar. However, the True Range is not primed until the second bar.</para>
    /// The indicator accepts only <c>ohlcv</c> samples and is not primed during the first <c>ℓ</c> updates.
    /// <para>The ATR default length is 14, this can be intraday, daily, weekly or monthly. To measure recent volatility use a shorter length, 2 to 10. For longer term volatility use longer length, 20 to 50.</para>
    /// </summary>
    public sealed class AverageTrueRange : Indicator, ILineIndicator
    {
        #region Members and accessors
        /// <summary>
        /// The length, <c>ℓ</c>, (the number of time periods) of the Average True Range.
        /// </summary>
        public int Length { get; }

        private double value = double.NaN;
        /// <summary>
        /// The current value of the the Average True Range, or <c>NaN</c> if not primed.
        /// The indicator is not primed during the first <c>ℓ</c> updates, where <c>ℓ</c> is the length.
        /// </summary>
        public double Value { get { lock (updateLock) { return primed ? value : double.NaN; } } }

        /// <summary>
        /// The current value of the the True Range, or <c>NaN</c> if not primed.
        /// The indicator is not primed during the first <c>ℓ</c> updates, where <c>ℓ</c> is the length.
        /// </summary>
        public double TrueRange => trueRange.Value;

        /// <summary>
        /// A line indicator façade to expose a value of the Average True Range as a separate line indicator with a fictitious update.
        /// <para />
        /// The line indicator façade do not perform actual updates. It just gets a current value of the Average True Range from this instance, assuming it is already updated.
        /// <para />
        /// Resetting the line indicator façade has no effect. This instance should be reset instead.
        /// </summary>
        public LineIndicatorFacade ValueFacade => new LineIndicatorFacade(Atr, Moniker, AtrFull, () => IsPrimed, () => Value);

        /// <summary>
        /// A line indicator façade to expose a value of the True Range as a separate line indicator with a fictitious update.
        /// <para />
        /// The line indicator façade do not perform actual updates. It just gets a current value of the True Range from this instance, assuming it is already updated.
        /// <para />
        /// Resetting the line indicator façade has no effect. This instance should be reset instead.
        /// </summary>
        public LineIndicatorFacade TrueRangeFacade => trueRange.ValueFacade;

        private int stage;
        private readonly int lastIndex;
        private int windowCount;
        private readonly double[] window;
        private double windowSum;
        private readonly TrueRange trueRange = new TrueRange();

        private const string Atr = "atr";
        private const string AtrFull = "Average True Range";
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a new instance of the <see cref="AverageTrueRange"/> class.
        /// </summary>
        /// <param name="length">The number of time periods of the Average True Range. The default value is 14.</param>
        public AverageTrueRange(int length)
            : base(Atr, AtrFull)
        {
            if (1 > length)
                throw new ArgumentOutOfRangeException(nameof(length));
            Length = length;
            lastIndex = length - 1;
            if (0 < lastIndex)
                window = new double[length];
            Moniker = string.Concat(Atr, "(", length.ToString(CultureInfo.InvariantCulture), ")");
        }
        #endregion

        #region Reset
        /// <inheritdoc />
        public override void Reset()
        {
            lock (updateLock)
            {
                primed = false;
                stage = 0;
                windowCount = 0;
                windowSum = 0d;
                value = double.NaN;
                trueRange.Reset();
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// The Average True Range indicator can be updated only with <c>ohlcv</c> samples.
        /// In this case, the same sample value is used as a substitute for <c>High</c>, <c>Low</c> and <c>Close</c> of an Ohlcv.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public double Update(double sample)
        {
            return Update(sample, sample, sample);
        }

        /// <summary>
        /// The Average True Range indicator can be updated only with <c>ohlcv</c> samples.
        /// In this case, the same sample value is used as a substitute for <c>High</c>, <c>Low</c> and <c>Close</c> of an Ohlcv.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <param name="dateTime">A date-time of the new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(double sample, DateTime dateTime)
        {
            return new Scalar(dateTime, Update(sample, sample, sample));
        }

        /// <summary>
        /// The Average True Range indicator can be updated only with <c>ohlcv</c> samples.
        /// In this case, the same scalar value is used as a substitute for <c>High</c>, <c>Low</c> and <c>Close</c> of an Ohlcv.
        /// </summary>
        /// <param name="scalar">A new scalar.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(Scalar scalar)
        {
            double sample = scalar.Value;
            return new Scalar(scalar.Time, Update(sample, sample, sample));
        }

        /// <summary>
        /// Updates the value of the Average True Range. The indicator is not primed during the first <c>ℓ</c> updates.
        /// </summary>
        /// <param name="sampleClose">The <c>close</c> value of a new sample.</param>
        /// <param name="sampleHigh">The <c>high</c> value of a new sample.</param>
        /// <param name="sampleLow">The <c>low</c> value of a new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public double Update(double sampleClose, double sampleHigh, double sampleLow)
        {
            if (double.IsNaN(sampleClose) || double.IsNaN(sampleHigh) || double.IsNaN(sampleLow))
                return double.NaN;
            lock (updateLock)
            {
                double trueRangeValue = trueRange.Update(sampleClose, sampleHigh, sampleLow);
                if (0 == lastIndex)
                {
                    value = trueRangeValue;
                    if (0 == stage)
                    {
                        // The very first sample is used by the True Range.
                        ++stage;
                    }
                    else if (1 == stage)
                    {
                        ++stage;
                        primed = true;
                    }
                    return value;
                }
                if (1 < stage)
                {
                    // The subsequent values are averaged using the Wilder method.
                    value *= lastIndex;
                    value += trueRangeValue;
                    value /= Length;
                    return value;
                }
                if (1 == stage)
                {
                    windowSum += trueRangeValue;
                    window[windowCount] = trueRangeValue;
                    if (Length == ++windowCount)
                    {
                        ++stage;
                        primed = true;
                        // The initial value is a simple average.
                        value = windowSum / Length;
                    }
                    return primed ? value : double.NaN;
                }
                // The very first sample is used by the True Range.
                ++stage;
                return double.NaN;
            }
        }

        /// <summary>
        /// Updates the value of the Average True Range. The indicator is not primed during the first <c>ℓ</c> updates.
        /// </summary>
        /// <param name="sampleClose">The <c>close</c> value of a new sample.</param>
        /// <param name="sampleHigh">The <c>high</c> value of a new sample.</param>
        /// <param name="sampleLow">The <c>low</c> value of a new sample.</param>
        /// <param name="dateTime">A date-time of the new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(double sampleClose, double sampleHigh, double sampleLow, DateTime dateTime)
        {
            return new Scalar(dateTime, Update(sampleClose, sampleHigh, sampleLow));
        }

        /// <summary>
        /// Updates the value of the Average True Range. The indicator is not primed during the first <c>ℓ</c> updates.
        /// </summary>
        /// <param name="ohlcv">A new ohlcv.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(Ohlcv ohlcv)
        {
            return new Scalar(ohlcv.Time, Update(ohlcv.Close, ohlcv.High, ohlcv.Low));
        }
        #endregion
    }
}
