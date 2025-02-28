﻿using System.Collections.Generic;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// An abstract indicator calculating the single <see cref="Scalar"/> value.
    /// </summary>
    public abstract class ScalarIndicator : IIndicator
    {
        private readonly OhlcvComponent ohlcvComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScalarIndicator"/> class.
        /// </summary>
        /// <param name="ohlcvComponent">The <see cref="Ohlcv"/> component to use when calculating indicator from an <see cref="Ohlcv"/> data.</param>
        protected ScalarIndicator(OhlcvComponent ohlcvComponent)
        {
            this.ohlcvComponent = ohlcvComponent;
        }

        /// <inheritdoc />
        public abstract bool IsPrimed { get; }

        /// <inheritdoc />
        public abstract IndicatorMetadata Metadata { get; }

        /// <summary>
        /// Gets lock object to update an indicator.
        /// </summary>
        protected object UpdateLock { get; } = new object();

        /// <inheritdoc />
        public abstract void Reset();

        /// <inheritdoc />
        public IndicatorOutput Update(Scalar sample)
        {
            var outputData = new IndicatorOutput { Outputs = new object[1] };
            double v = sample.Value;
            lock (UpdateLock)
            {
                v = Update(v);
            }

            outputData.Outputs[0] = new Scalar(sample.Time, v);
            return outputData;
        }

        /// <inheritdoc />
        public IndicatorOutput Update(Ohlcv sample)
        {
            var outputData = new IndicatorOutput { Outputs = new object[1] };
            double v = sample.Component(ohlcvComponent);
            lock (UpdateLock)
            {
                v = Update(v);
            }

            outputData.Outputs[0] = new Scalar(sample.Time, v);
            return outputData;
        }

        /// <inheritdoc />
        public IEnumerable<IndicatorOutput> Update(IEnumerable<Scalar> samples)
        {
            var list = new List<IndicatorOutput>();
            lock (UpdateLock)
            {
                foreach (var sample in samples)
                {
                    var outputData = new IndicatorOutput { Outputs = new object[1] };
                    outputData.Outputs[0] = new Scalar(sample.Time, Update(sample.Value));
                    list.Add(outputData);
                }
            }

            return list;
        }

        /// <inheritdoc />
        public IEnumerable<IndicatorOutput> Update(IEnumerable<Ohlcv> samples)
        {
            var list = new List<IndicatorOutput>();
            lock (UpdateLock)
            {
                foreach (var sample in samples)
                {
                    var outputData = new IndicatorOutput { Outputs = new object[1] };
                    outputData.Outputs[0] = new Scalar(sample.Time, Update(sample.Component(ohlcvComponent)));
                    list.Add(outputData);
                }
            }

            return list;
        }

        /// <summary>
        /// Updates the value of the indicator.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <returns>A new value of the indicator.</returns>
        protected abstract double Update(double sample);
    }
}
