using System;
using System.Collections.Generic;
using System.Globalization;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;

namespace Mbs.Trading.Indicators
{
    /// <summary>
    /// A simple, or arithmetic, moving average (SMA) is calculated by adding the prices
    /// for a number of time periods (length, <c>ℓ</c>) and then dividing this total by the number of time periods.
    /// In other words, this is an unweighted mean (gives equal weight to each price) of the previous <c>ℓ</c> prices.
    /// <para>
    /// This implementation updates the value of the SMA incrementally using the formula:</para>
    /// <para><c>SMA¡ = SMA¡₋₁ + (P¡ - P¡₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
    /// <para>
    /// The SMA indicator is not primed during the first <c>ℓ-1</c> updates.</para>
    /// </summary>
    public sealed class MovingAverageWithVariablePeriod : Indicator, ILineIndicator
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

        private int windowCount;
        private readonly double[] window;
        private double windowSum;

        private const string MavpName = "MVP";
        private const string MavpDescription = "Moving Average with Variable Period";
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="length">The length (the number of time periods) of the simple moving average.</param>
        public MovingAverageWithVariablePeriod(int length)
            : base(MavpName, MavpDescription)
        {
            if (2 > length || 10000 < length)
                throw new ArgumentOutOfRangeException(nameof(length));
            Length = length;
            window = new double[length];
            Moniker = string.Concat(MavpName, length.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="length">The length (the number of time periods) of the simple moving average.</param>
        /// <param name="ohlcvComponent">The Ohlcv component.</param>
        public MovingAverageWithVariablePeriod(int length, OhlcvComponent ohlcvComponent)
            : base(MavpName, MavpDescription, ohlcvComponent)
        {
            if (2 > length || 10000 < length)
                throw new ArgumentOutOfRangeException(nameof(length));
            this.Length = length;
            window = new double[length];
            Moniker = string.Concat(MavpName, length.ToString(CultureInfo.InvariantCulture));
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
        /// <para><c>SMA¡ = SMA¡₋₁ + (P¡ - P¡₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The simple moving average indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="price">A new price.</param>
        /// <returns>The new value of the simple moving average.</returns>
        public double Update(double price)
        {
            if (double.IsNaN(price))
                return price;
            lock (updateLock)
            {
                if (primed)
                {
                    windowSum += price - window[0];
                    int j = Length - 1;
                    for (int i = 0; i < j; )
                        window[i] = window[++i];
                    window[j] = price;
                    value = windowSum / Length;
                }
                else
                {
                    windowSum += price;
                    window[windowCount] = price;
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
        /// <para><c>SMA¡ = SMA¡₋₁ + (P¡ - P¡₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The simple moving average indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="price">A new price.</param>
        /// <param name="dateTime">A date-time of the new price.</param>
        /// <returns>The new value of the simple moving average.</returns>
        public Scalar Update(double price, DateTime dateTime)
        {
            return new Scalar(dateTime, Update(price));
        }

        /// <summary>
        /// Updates the value of the simple moving average using the formula:
        /// <para><c>SMA¡ = SMA¡₋₁ + (P¡ - P¡₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The simple moving average indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="scalar">A new scalar.</param>
        /// <returns>The new value of the simple moving average.</returns>
        public Scalar Update(Scalar scalar)
        {
            return new Scalar(scalar.Time, Update(scalar.Value));
        }

        /// <summary>
        /// Updates the value of the simple moving average using the formula:
        /// <para><c>SMA¡ = SMA¡₋₁ + (P¡ - P¡₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The simple moving average indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="ohlcv">A new ohlcv.</param>
        /// <returns>The new value of the simple moving average.</returns>
        public Scalar Update(Ohlcv ohlcv)
        {
            return new Scalar(ohlcv.Time, Update(ohlcv.Component(OhlcvComponent)));
        }
        #endregion

        #region Calculate
        /// <summary>
        /// Calculates the simple moving average from the input array using the formula:
        /// <para><c>SMA¡ = SMA¡₋₁ + (P¡ - P¡₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The simple moving average indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="priceList">The price list.</param>
        /// <param name="length">The length (the number of time periods) of the simple moving average.</param>
        /// <returns>A list of the simple moving average values.</returns>
        public static List<double> Calculate(List<double> priceList, int length)
        {
            if (2 > length || 10000 < length)
                throw new ArgumentOutOfRangeException(nameof(length));
            int i, count = priceList.Count;
            List<double> resultList = new List<double>(count);
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
                    v = priceList[i++];
                    if (!double.IsNaN(v))
                        sum += v;
                    resultList.Add(double.NaN);
                }
                v = priceList[l];
                if (!double.IsNaN(v))
                    sum += v;
                resultList.Add(sum / w);
                l = 0;
                for (i = length; i < count;)
                {
                    v = priceList[i++];
                    if (!double.IsNaN(v))
                        sum += v;
                    v = priceList[l++];
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
