﻿using System;
using System.Globalization;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;

namespace Mbs.Trading.Indicators
{
    /// <summary>
    /// Money Flow Index (MFI) is an oscillator calculated over an <c>ℓ</c> periods, showing money flow on up days as a percentage of the total of up and down days.
    /// <para />
    /// The value is calculated as
    /// <para />
    /// TypicaPriceᵢ = (Highᵢ + Lowᵢ + Closeᵢ) /3
    /// <para />
    /// MoneyFlowᵢ = TypicaPriceᵢ * Volumeᵢ
    /// <para />
    /// Totals of the money flow amounts over the given <c>ℓ</c> periods are then formed.
    /// Positive money flow is the total for those days where the typical price is higher than the previous day's typical price,
    /// and negative money flow where below. If typical price is unchanged then that day is discarded.
    /// <para />
    /// A money ratio is then formed
    /// <para />
    /// MoneyRatioᵢ = PositiveMoneyFlowᵢ / NegativeMoneyFlowᵢ
    /// <para />
    /// from which a money flow index ranging from 0 to 100 is formed,
    /// <para />
    /// MFIᵢ = 100 - 100/(1+MoneyRatioᵢ) = 100 PositiveMoneyFlowᵢ/(PositiveMoneyFlowᵢ+NegativeMoneyFlowᵢ)
    /// <para />
    /// MFI is used as an oscillator. A value of 80 is generally considered overbought, or a value of 20 oversold.
    /// <para />
    /// Divergences between MFI and price action are also considered significant, for instance if price makes a new rally high but the MFI high is less than its previous high then that may indicate a weak advance, likely to reverse.
    /// <para />
    /// For the purposes of the MFI, "money flow", i.e. dollar volume, on an up day is taken to represent the enthusiasm of buyers, and on a down day to represent the enthusiasm of sellers. An excessive proportion in one direction or the other is interpreted as an extreme, likely to result in a price reversal.
    /// <para />
    /// <para />
    /// </summary>
    public sealed class MoneyFlowIndex : Indicator, ILineIndicator
    {
        #region Members and accessors
        /// <summary>
        /// The length, <c>ℓ</c>, (the number of time periods) of the Money Flow Index.
        /// </summary>
        public int Length { get; }

        private double value = double.NaN;
        /// <summary>
        /// The current value of the Money Flow Index or <c>NaN</c> if not primed.
        /// </summary>
        public double Value { get { lock (updateLock) { return value; } } }

        private readonly int lengthMinOne;
        private int circularBufferIndex;
        private int circularBufferLowIndex;
        private int circularBufferCount;
        private readonly double[] negativeCircularBuffer;
        private readonly double[] positiveCircularBuffer;
        private double negativeSum;
        private double positiveSum;
        private double previousSample;

        private const string Mfi = "MFI";
        private const string MfiFull = "Money Flow Index";
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a new instance of the <see cref="MoneyFlowIndex"/> class.
        /// </summary>
        /// <param name="length">The number of time periods of the Money Flow Index. The default value is 14.</param>
        /// <param name="ohlcvComponent">The Ohlcv component. The original indicator uses the typical price (high+low+close)/3, which is the default.</param>
        public MoneyFlowIndex(int length = 14, OhlcvComponent ohlcvComponent = OhlcvComponent.TypicalPrice)
            : base(Mfi, MfiFull, ohlcvComponent)
        {
            if (1 > length)
                throw new ArgumentOutOfRangeException(nameof(length));
            Length = length;
            lengthMinOne = length - 1;
            Moniker = string.Concat(Mfi, "(", length.ToString(CultureInfo.InvariantCulture), ")");
            negativeCircularBuffer = new double[length];
            positiveCircularBuffer = new double[length];
        }
        #endregion

        #region Reset
        /// <inheritdoc />
        public override void Reset()
        {
            lock (updateLock)
            {
                primed = false;
                value = double.NaN;
                circularBufferIndex = 0;
                circularBufferLowIndex = 0;
                circularBufferCount = 0;
                negativeSum = 0d;
                positiveSum = 0d;
                previousSample = 0d;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// The Money Flow Index indicator can be updated only with <c>ohlcv</c> samples.
        /// In this case, the same sample value is used as a substitute for a <c>typical price</c> of an Ohlcv.
        /// The volume is assumed to be one.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public double Update(double sample)
        {
            return Update(sample, 1d);
        }

        /// <summary>
        /// The Money Flow Index indicator can be updated only with <c>ohlcv</c> samples.
        /// In this case, the same sample value is used as a substitute for a <c>typical price</c> of an Ohlcv.
        /// The volume is assumed to be one.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <param name="dateTime">A date-time of the new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(double sample, DateTime dateTime)
        {
            return new Scalar(dateTime, Update(sample, 1d));
        }

        /// <summary>
        /// The Money Flow Index indicator can be updated only with <c>ohlcv</c> samples.
        /// In this case, the scalar value is used as a substitute for a <c>typical price</c> of an Ohlcv.
        /// The volume is assumed to be one.
        /// </summary>
        /// <param name="scalar">A new scalar.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(Scalar scalar)
        {
            double sample = scalar.Value;
            return new Scalar(scalar.Time, Update(sample, 1d));
        }

        /// <summary>
        /// Updates the value of the Money Flow Index indicator.
        /// </summary>
        /// <param name="sample">The new sample (typical price).</param>
        /// <param name="volume">The volume value.</param>
        /// <returns>The new value of the indicator.</returns>
        public double Update(double sample, double volume)
        {
            if (double.IsNaN(sample) || double.IsNaN(volume))
                return double.NaN;
            lock (updateLock)
            {
                if (primed)
                {
                    negativeSum -= negativeCircularBuffer[circularBufferLowIndex];
                    positiveSum -= positiveCircularBuffer[circularBufferLowIndex];
                    double amount = sample * volume;
                    double diff = sample - previousSample;
                    if (diff < 0)
                    {
                        negativeCircularBuffer[circularBufferIndex] = amount;
                        positiveCircularBuffer[circularBufferIndex] = 0d;
                        negativeSum += amount;
                    }
                    else if (diff > 0)
                    {
                        negativeCircularBuffer[circularBufferIndex] = 0d;
                        positiveCircularBuffer[circularBufferIndex] = amount;
                        positiveSum += amount;
                    }
                    else
                    {
                        negativeCircularBuffer[circularBufferIndex] = 0d;
                        positiveCircularBuffer[circularBufferIndex] = 0d;
                    }
                    double sum = positiveSum + negativeSum;
                    value = sum < 1d ? 0d : (100d * positiveSum / sum);
                    if (++circularBufferIndex > lengthMinOne)
                        circularBufferIndex = 0;
                    if (++circularBufferLowIndex > lengthMinOne)
                        circularBufferLowIndex = 0;
                }
                else if (0 == circularBufferCount)
                {
                    ++circularBufferCount;
                }
                else
                {
                    double amount = sample * volume;
                    double diff = sample - previousSample;
                    if (diff < 0)
                    {
                        negativeCircularBuffer[circularBufferIndex] = amount;
                        positiveCircularBuffer[circularBufferIndex] = 0d;
                        negativeSum += amount;
                    }
                    else if (diff > 0)
                    {
                        negativeCircularBuffer[circularBufferIndex] = 0d;
                        positiveCircularBuffer[circularBufferIndex] = amount;
                        positiveSum += amount;
                    }
                    else
                    {
                        negativeCircularBuffer[circularBufferIndex] = 0d;
                        positiveCircularBuffer[circularBufferIndex] = 0d;
                    }
                    if (Length == circularBufferCount)
                    {
                        double sum = positiveSum + negativeSum;
                        value = sum < 1d ? 0d : (100d * positiveSum / sum);
                        primed = true;
                    }
                    if (++circularBufferIndex > lengthMinOne)
                        circularBufferIndex = 0;
                    ++circularBufferCount;
                }
                previousSample = sample;
                return value;
            }
        }

        /// <summary>
        /// Updates the value of the Money Flow Index.
        /// </summary>
        /// <param name="ohlcv">A new ohlcv.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(Ohlcv ohlcv)
        {
            return new Scalar(ohlcv.Time, Update(ohlcv.Component(OhlcvComponent), ohlcv.Volume));
        }
        #endregion
    }
}