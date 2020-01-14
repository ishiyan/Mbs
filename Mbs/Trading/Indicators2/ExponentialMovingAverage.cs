using System;
using System.Collections.Generic;
using System.Globalization;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators2.Abstractions;

namespace Mbs.Trading.Indicators2
{
    /// <summary>
    /// Given a constant smoothing percentage factor <c>0 &lt; α ≤ 1</c>, an exponential, or exponentially weighted, moving average (EMA)
    /// is calculated by applying a constant smoothing factor <c>α</c> to a difference of today's sample and yesterday's EMA value
    /// <para><c>EMAᵢ = αPᵢ + (1-α)EMAᵢ₋₁ = EMAᵢ₋₁ + α(Pᵢ - EMAᵢ₋₁), 0 &lt; α ≤ 1</c></para>
    /// Thus, the weighting for each older sample is given by the geometric progression <c>1, α, α², α³, …</c>, giving much
    /// more importance to recent observations while not discarding older ones: all data previously used are always part of the new EMA value.
    /// <para>
    /// <c>α</c> may be expressed as a percentage, so a smoothing factor of 10% is equivalent to <c>α = 0.1</c>. A higher <c>α</c>
    /// discounts older observations faster. Alternatively, <c>α</c> may be expressed in terms of <c>ℓ</c> time periods (length), where
    /// <para><c>α = 2 / (ℓ + 1)</c> and <c>ℓ = 2/α - 1</c></para>
    /// <para>The indicator is not primed during the first <c>ℓ-1</c> updates.</para>
    /// The 12- and 26-day EMAs are the most popular short-term averages, and they are used to create indicators like MACD and PPO.
    /// In general, the 50- and 200-day EMAs are used as signals of long-term trends.
    /// </para>
    /// <para>The very first EMA value (the seed for subsequent values) is calculated differently. This implementation allows for two algorithms for this seed.</para>
    /// <para>❶ Use a simple average of the first 'period'. This is the most widely documented approach.</para>
    /// <para>❷ Use first sample value as a seed. This is used in Metastock.</para>
    /// </summary>
    public sealed class ExponentialMovingAverage : LineIndicator
    {
        #region Members and accessors
        /// <summary>
        /// The length (<c>ℓ</c>) of the exponential moving average. The equivalent smoothing factor (<c>α</c>) is
        /// <para><c>α = 2/(ℓ + 1), 0 &lt; α ≤ 1, 1 ≤ ℓ</c></para>
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// The smoothing factor (<c>α</c>) of the exponential moving average. The equivalent length (<c>ℓ</c>) is
        /// <para><c>ℓ = 2/α - 1, 0 &lt; α ≤ 1, 1 ≤ ℓ</c></para>
        /// </summary>
        public double SmoothingFactor { get; }

        /// <summary>
        /// The current value of the exponential moving average, or <c>NaN</c> if not primed.
        /// The indicator is not primed during the first <c>ℓ-1</c> updates, where the <c>ℓ</c> is the length.
        /// </summary>
        public double Value { get { lock (updateLock) { return primed ? value : double.NaN; } } }

        /// <summary>
        /// If the very first EMA value is a simple average of the first 'period' (the most widely documented approach) or the first input value (used in Metastock).
        /// </summary>
        public bool FirstIsAverage { get; }

        private int count;
        private double sum;
        private double value = double.NaN;

        private const double Epsilon = 0.00000001;
        private const string Ema = "ema";
        private const string EmaFull = "Exponential Moving Average";
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a new instance of the class.
        /// </summary>
        /// <param name="length">The length, <c>ℓ</c>, of the exponential moving average.
        /// The equivalent smoothing factor <c>α</c> is
        /// <para><c>α = 2/(ℓ + 1), 0 &lt; α ≤ 1, 1 ≤ ℓ</c></para>
        /// </param>
        /// <param name="firstIsAverage">If the very first EMA value is a simple average of the first 'period' (the most widely documented approach) or the first input value (used in Metastock).</param>
        /// <param name="ohlcvComponent">The Ohlcv component.</param>
        public ExponentialMovingAverage(int length, bool firstIsAverage = true, OhlcvComponent ohlcvComponent = OhlcvComponent.ClosingPrice)
            : base(Ema, EmaFull, ohlcvComponent)
        {
            if (1 > length)
                throw new ArgumentOutOfRangeException(nameof(length));
            Length = length;
            FirstIsAverage = firstIsAverage;
            SmoothingFactor = 2d / (1 + length);
            Moniker = string.Concat(Ema, length.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Constructs a new instance of the class.
        /// </summary>
        /// <param name="smoothingFactor">The smoothing factor, <c>α</c>, of the exponential moving average.
        /// The equivalent length <c>ℓ</c> is
        /// <para><c>ℓ = 2/α - 1, 0 &lt; α ≤ 1, 1 ≤ ℓ</c></para>
        /// </param>
        /// <param name="firstIsAverage">If the very first EMA value is a simple average of the first 'period' (the most widely documented approach) or the first input value (used in Metastock).</param>
        /// <param name="ohlcvComponent">The Ohlcv component.</param>
        public ExponentialMovingAverage(double smoothingFactor, bool firstIsAverage = true, OhlcvComponent ohlcvComponent = OhlcvComponent.ClosingPrice)
            : base(Ema, EmaFull, ohlcvComponent)
        {
            if (0d > smoothingFactor || 1d < smoothingFactor)
                throw new ArgumentOutOfRangeException(nameof(smoothingFactor));
            SmoothingFactor = smoothingFactor;
            if (Epsilon > smoothingFactor)
                Length = int.MaxValue;
            else
                Length = (int)Math.Round(2d / smoothingFactor) - 1;
            FirstIsAverage = firstIsAverage;
            Moniker = string.Concat(Ema, Length.ToString(CultureInfo.InvariantCulture));
        }
        #endregion

        #region Reset
        /// <inheritdoc />
        public override void Reset()
        {
            lock (updateLock)
            {
                primed = false;
                count = 0;
                sum = 0d;
                value = double.NaN;
            }
        }
        #endregion

        #region Update
        /// <inheritdoc />
        public override double Update(double sample)
        {
            if (double.IsNaN(sample))
                return sample;
            lock (updateLock)
            {
                if (primed)
                    value += (sample - value) * SmoothingFactor;
                else
                {
                    if (FirstIsAverage)
                    {
                        sum += sample;
                        if (Length == ++count)
                        {
                            primed = true;
                            value = sum / Length;
                        }
                        else
                            return double.NaN;
                    }
                    else
                    {
                        if (1 == ++count)
                            value = sample;
                        else
                            value += (sample - value) * SmoothingFactor;
                        if (Length == count)
                            primed = true;
                        else
                            return double.NaN;
                    }
                }
                return value;
            }
        }
        #endregion

        #region Calculate
        /// <summary>
        /// Calculates the exponential moving average from the input array using the formula:
        /// <para><c>EMAᵢ = EMAᵢ₋₁ + α(Pᵢ - EMAᵢ₋₁), 0 &lt; α ≤ 1</c></para>
        /// The indicator is primed after the first update.
        /// </summary>
        /// <param name="sampleList">The sample list.</param>
        /// <param name="length">The length, <c>ℓ</c>, of the exponential moving average.
        /// The equivalent smoothing factor <c>α</c> is
        /// <para><c>α = 2/(ℓ + 1), 0 &lt; α ≤ 1, 1 ≤ ℓ</c></para>
        /// </param>
        /// <param name="firstIsAverage">If the very first EMA value is a simple average of the first 'period' (the most widely documented approach) or the first input value (used in Metastock).</param>
        /// <returns>A list of the exponential moving average values.</returns>
        public static List<double> Calculate(List<double> sampleList, int length, bool firstIsAverage = true)
        {
            if (1 > length)
                throw new ArgumentOutOfRangeException(nameof(length));
            double smoothingFactor = 2d / (1d + length);
            int count = sampleList.Count;
            var resultList = new List<double>(count);
            if (count < length)
            {
                for (int i = 0; i < count; ++i)
                    resultList.Add(double.NaN);
            }
            else
            {
                int length1 = length - 1;
                for (int i = 0; i < length1; ++i)
                    resultList.Add(double.NaN);
                double value = sampleList[0];
                if (firstIsAverage)
                {
                    for (int i = 1; i < length; ++i)
                        value += sampleList[i];
                    value /= length;
                }
                else
                {
                    for (int i = 1; i < length; ++i)
                        value += (sampleList[i] - value) * smoothingFactor;
                }
                resultList.Add(value);
                for (int i = length; i < count; ++i)
                {
                    value += (sampleList[i] - value) * smoothingFactor;
                    resultList.Add(value);
                }
            }
            return resultList;
        }

        /// <summary>
        /// Calculates the exponential moving average from the input array using the formula:
        /// <para><c>EMAᵢ = EMAᵢ₋₁ + α(Pᵢ - EMAᵢ₋₁), 0 ≤ α ≤ 1</c></para>
        /// The indicator is primed after the first update.
        /// </summary>
        /// <param name="sampleList">The sample list.</param>
        /// <param name="smoothingFactor">The smoothing factor, <c>α</c>, of the exponential moving average.
        /// The equivalent length <c>ℓ</c> is
        /// <para><c>ℓ = 2/α - 1, 0 &lt; α ≤ 1, 1 ≤ ℓ</c></para>
        /// </param>
        /// <param name="firstIsAverage">If the very first EMA value is a simple average of the first 'period' (the most widely documented approach) or the first input value (used in Metastock).</param>
        /// <returns>A list of the exponential moving average values.</returns>
        public static List<double> Calculate(List<double> sampleList, double smoothingFactor, bool firstIsAverage = true)
        {
            if (0d > smoothingFactor || 1d < smoothingFactor)
                throw new ArgumentOutOfRangeException(nameof(smoothingFactor));
            int count = sampleList.Count;
            var resultList = new List<double>(count);
            int length = (Epsilon > smoothingFactor) ?
                int.MaxValue : ((int)Math.Round(2d / smoothingFactor) - 1);
            if (count < length)
            {
                for (int i = 0; i < count; ++i)
                    resultList.Add(double.NaN);
            }
            else
            {
                int length1 = length - 1;
                for (int i = 0; i < length1; ++i)
                    resultList.Add(double.NaN);
                double value = sampleList[0];
                if (firstIsAverage)
                {
                    for (int i = 1; i < length; ++i)
                        value += sampleList[i];
                    value /= length;
                }
                else
                {
                    for (int i = 1; i < length; ++i)
                        value += (sampleList[i] - value) * smoothingFactor;
                }
                resultList.Add(value);
                for (int i = length; i < count; ++i)
                {
                    value += (sampleList[i] - value) * smoothingFactor;
                    resultList.Add(value);
                }
            }
            return resultList;
        }
        #endregion
    }
}
