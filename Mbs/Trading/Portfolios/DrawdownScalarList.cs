using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mbs.Trading.Data;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// An updateable list of drawdown percentage scalars.
    /// </summary>
    internal class DrawdownScalarList
    {
        private readonly List<Scalar> drawdownList = new List<Scalar>();
        private readonly List<Scalar> drawdownMaxList = new List<Scalar>();
        private readonly object updateLock = new object();
        private Scalar drawdown = new Scalar(new DateTime(0L));
        private Scalar drawdownMax = new Scalar(new DateTime(0L));
        private double watermarkHigh = double.NaN;

        /// <summary>
        /// Gets the read-only collection of drawdown percentage [0, -1] scalars.
        /// </summary>
        internal ReadOnlyCollection<Scalar> DrawdownCollection
        {
            get
            {
                lock (updateLock)
                {
                    return drawdownList.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets the read-only collection of maximal drawdown percentage [0, -1] scalars.
        /// </summary>
        internal ReadOnlyCollection<Scalar> DrawdownMaxCollection
        {
            get
            {
                lock (updateLock)
                {
                    return drawdownMaxList.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets the current drawdown percentage [0, -1] value.
        /// </summary>
        internal double Drawdown
        {
            get
            {
                lock (updateLock)
                {
                    return drawdown.Value;
                }
            }
        }

        /// <summary>
        /// Gets the maximal drawdown percentage [0, -1] value.
        /// </summary>
        internal double DrawdownMax
        {
            get
            {
                lock (updateLock)
                {
                    return drawdownMax.Value;
                }
            }
        }

        /// <summary>
        /// Re-sets the drawdown percentage data.
        /// </summary>
        internal void Reset()
        {
            lock (updateLock)
            {
                watermarkHigh = double.NaN;
                drawdown = new Scalar(new DateTime(0L));
                drawdownMax = new Scalar(new DateTime(0L));
                drawdownList.Clear();
                drawdownMaxList.Clear();
            }
        }

        /// <summary>
        /// Updates the drawdown percentage data.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="value">The new equity value.</param>
        internal void Update(DateTime dateTime, double value)
        {
            lock (updateLock)
            {
                if (double.IsNaN(watermarkHigh))
                {
                    watermarkHigh = value;
                    return;
                }

                if (watermarkHigh < value)
                {
                    watermarkHigh = value;
                }

                double newDrawdown = Math.Abs(watermarkHigh) < double.Epsilon ? 0d : Math.Min(value - watermarkHigh, 0d) / watermarkHigh;
                if (drawdownList.Count == 0)
                {
                    drawdown.Time = dateTime;
                    drawdown.Value = newDrawdown;
                    drawdownList.Add(drawdown);
                    drawdownMax.Time = dateTime;
                    drawdownMax.Value = newDrawdown;
                    drawdownMaxList.Add(drawdownMax);
                }
                else if (drawdown.Time < dateTime)
                {
                    drawdown = new Scalar(dateTime, newDrawdown);
                    drawdownList.Add(drawdown);
                    double newDrawdownMax = Math.Min(newDrawdown, drawdownMax.Value);
                    drawdownMax = new Scalar(dateTime, newDrawdownMax);
                    drawdownMaxList.Add(drawdownMax);
                }
                else if (drawdown.Time == dateTime)
                {
                    if (drawdown.Value > newDrawdown)
                    {
                        drawdown.Value = newDrawdown;
                    }

                    double newDrawdownMax = Math.Min(newDrawdown, drawdownMax.Value);
                    drawdownMax.Value = newDrawdownMax;
                }
            }
        }
    }
}
