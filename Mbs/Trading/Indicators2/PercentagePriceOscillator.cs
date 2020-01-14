﻿using System;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators2.Abstractions;

namespace Mbs.Trading.Indicators2
{
    /// <summary>
    /// The Percentage Price Oscillator (PPO) is calculated by subtracting the slower moving average from the faster moving average and then dividing the result by the slower moving average.
    /// </summary>
    public sealed class PercentagePriceOscillator : Indicator, ILineIndicator
    {
        #region Members and accessors

        /// <summary>
        /// The length (the number of time periods) of the fast moving average.
        /// </summary>
        public int FastLength { get; }

        /// <summary>
        /// The length (the number of time periods) of the slow moving average.
        /// </summary>
        public int SlowLength { get; }

        /// <summary>
        /// The fast moving average indicator.
        /// </summary>
        public ILineIndicator FastMovingAverageIndicator { get; }

        /// <summary>
        /// The slow moving average indicator.
        /// </summary>
        public ILineIndicator SlowMovingAverageIndicator { get; }

        private double value = double.NaN;
        /// <summary>
        /// The current value of the Percentage Price Oscillator, or <c>NaN</c> if not primed.
        /// </summary>
        public double Value { get { lock (updateLock) { return value; } } }

        private const double Epsilon = 0.00000001;
        private const string Ppo = "PPO";
        private const string PpoFull = "Percentage Price Oscillator";
        private const string SlowEx = "slowLength";
        private const string FastEx = "fastLength";
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a new instance of the <see cref="PercentagePriceOscillator"/> class.
        /// </summary>
        /// <param name="slowLength">The length (the number of time periods) of the slow Simple Moving Average.</param>
        /// <param name="fastLength">The length (the number of time periods) of the fast Simple Moving Average.</param>
        /// <param name="ohlcvComponent">The Ohlcv component.</param>
        public PercentagePriceOscillator(int slowLength, int fastLength, OhlcvComponent ohlcvComponent = OhlcvComponent.ClosingPrice)
            : base(Ppo, PpoFull, ohlcvComponent)
        {
            if (2 > slowLength)
                throw new ArgumentOutOfRangeException(SlowEx);
            if (2 > fastLength)
                throw new ArgumentOutOfRangeException(FastEx);
            SlowLength = slowLength;
            SlowMovingAverageIndicator = new SimpleMovingAverage(slowLength, ohlcvComponent);
            FastLength = fastLength;
            FastMovingAverageIndicator = new SimpleMovingAverage(fastLength, ohlcvComponent);
            Moniker = string.Concat(Ppo, "[", FastMovingAverageIndicator.Moniker, "/", SlowMovingAverageIndicator.Moniker, "]");
        }

        /// <summary>
        /// Constructs a new instance of the <see cref="PercentagePriceOscillator"/> class.
        /// </summary>
        /// <param name="slowLength">The length of the slow Moving Average line indicator.</param>
        /// <param name="slowLineIndicator">The slow Moving Average line indicator.</param>
        /// <param name="fastLength">The length of the fast Moving Average line indicator.</param>
        /// <param name="fastLineIndicator">The fast Moving Average line indicator.</param>
        /// <param name="ohlcvComponent">The Ohlcv component.</param>
        public PercentagePriceOscillator(int slowLength, ILineIndicator slowLineIndicator, int fastLength, ILineIndicator fastLineIndicator, OhlcvComponent ohlcvComponent = OhlcvComponent.ClosingPrice)
            : base(Ppo, PpoFull, ohlcvComponent)
        {
            if (2 > slowLength)
                throw new ArgumentOutOfRangeException(SlowEx);
            if (2 > fastLength)
                throw new ArgumentOutOfRangeException(FastEx);
            SlowLength = slowLength;
            SlowMovingAverageIndicator = slowLineIndicator;
            FastLength = fastLength;
            FastMovingAverageIndicator = fastLineIndicator;
            Moniker = string.Concat(Ppo, "[", FastMovingAverageIndicator.Moniker, "/", SlowMovingAverageIndicator.Moniker, "]");
        }
        #endregion

        #region Reset
        /// <inheritdoc />
        public override void Reset()
        {
            lock (updateLock)
            {
                primed = false;
                FastMovingAverageIndicator.Reset();
                SlowMovingAverageIndicator.Reset();
                value = double.NaN;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the value of the Percentage Price Oscillator.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public double Update(double sample)
        {
            if (double.IsNaN(sample))
                return sample;
            lock (updateLock)
            {
                double slow = SlowMovingAverageIndicator.Update(sample);
                double fast = FastMovingAverageIndicator.Update(sample);
                primed = SlowMovingAverageIndicator.IsPrimed && FastMovingAverageIndicator.IsPrimed;
                /*if (double.IsNaN(fast) || double.IsNaN(slow))
                    value = double.NaN;
                else*/ if (Epsilon > Math.Abs(slow))
                    value = 0d;
                else
                    value = 100d * (fast - slow) / slow;
                return value;
            }
        }

        /// <summary>
        /// Updates the value of the Percentage Price Oscillator.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <param name="dateTime">A date-time of the new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(double sample, DateTime dateTime)
        {
            return new Scalar(dateTime, Update(sample));
        }

        /// <summary>
        /// Updates the value of the Percentage Price Oscillator.
        /// </summary>
        /// <param name="scalar">A new scalar.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(Scalar scalar)
        {
            return new Scalar(scalar.Time, Update(scalar.Value));
        }

        /// <summary>
        /// Updates the value of the Percentage Price Oscillator.
        /// </summary>
        /// <param name="ohlcv">A new ohlcv.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(Ohlcv ohlcv)
        {
            return new Scalar(ohlcv.Time, Update(ohlcv.Component(OhlcvComponent)));
        }
        #endregion
    }
}
