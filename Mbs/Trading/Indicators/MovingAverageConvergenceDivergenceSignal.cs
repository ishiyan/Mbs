﻿using System;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;

namespace Mbs.Trading.Indicators
{
    /// <summary>
    /// A facade class to represent the signal of the master Moving Average Convergence/Divergence indicator as a Value property and to simulate the update.
    /// <para />
    /// Assumes the master Moving Average Convergence/Divergence indicator is updated before the update on this class is called.
    /// </summary>
    public sealed class MovingAverageConvergenceDivergenceSignal : Indicator, ILineIndicator
    {
        #region Members and accessors
        /// <summary>
        /// The current value of the MACD signal or <c>NaN</c> if not primed.
        /// </summary>
        public double Value => Parent.Signal;

        /// <summary>
        /// The parent <see cref="MovingAverageConvergenceDivergence"/> instance.
        /// </summary>
        public MovingAverageConvergenceDivergence Parent { get; }

        private const string Macd = "MACDs";
        private const string MacdFull = "Moving Average Convergence/Divergence signal";
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a new instance of the <see cref="MovingAverageConvergenceDivergenceSignal"/> class.
        /// </summary>
        /// <param name="movingAverageConvergenceDivergence">The parent MovingAverageConvergenceDivergence line indicator.</param>
        public MovingAverageConvergenceDivergenceSignal(MovingAverageConvergenceDivergence movingAverageConvergenceDivergence)
            : base(Macd, MacdFull, movingAverageConvergenceDivergence.OhlcvComponent)
        {
            Parent = movingAverageConvergenceDivergence;
            Moniker = movingAverageConvergenceDivergence.Moniker.Insert(4, "s");
        }
        #endregion

        #region Reset
        /// <inheritdoc />
        public override void Reset()
        {
            lock (updateLock)
            {
                primed = false;
            }
        }
        #endregion

        #region Update
        /// <inheritdoc />
        public double Update(double sample)
        {
            if (double.IsNaN(sample))
                return double.NaN;
            lock (updateLock)
            {
                if (!primed)
                    primed = Parent.IsPrimed;
            }
            return Parent.Signal;
        }

        /// <summary>
        /// Updates the value of the Moving Average Convergence/Divergence signal.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <param name="dateTime">A date-time of the new sample.</param>
        /// <returns>The new value of the indicator.</returns>
        public Scalar Update(double sample, DateTime dateTime) => new Scalar(dateTime, Update(sample));

        /// <inheritdoc />
        public Scalar Update(Scalar scalar) => Update(scalar.Value, scalar.Time);

        /// <inheritdoc />
        public Scalar Update(Ohlcv ohlcv) => Update(ohlcv.Component(OhlcvComponent), ohlcv.Time);
        #endregion
    }
}
