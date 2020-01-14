using System;
using static System.FormattableString;

using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;

namespace Mbs.Trading.Indicators
{
    /// <summary>
    /// A simple, or arithmetic, moving average (SMA) is calculated by adding the samples
    /// for a number of time periods (length, <c>ℓ</c>) and then dividing this total by the number of time periods.
    /// In other words, this is an unweighted mean (gives equal weight to each sample) of the previous <c>ℓ</c> samples.
    /// <para>This implementation updates the value of the SMA incrementally using the formula:</para>
    /// <para><c>SMAᵢ = SMAᵢ₋₁ + (Pᵢ - Pᵢ₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
    /// <para>The indicator is not primed during the first <c>ℓ-1</c> updates.</para>
    /// </summary>
    public sealed class SimpleMovingAverage : ScalarIndicator
    {
        /// <summary>
        /// The parameters to create the indicator.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// The length (the number of time periods) of the simple moving average, should be greater than 1.
            /// </summary>
            public int Length;

            /// <summary>
            /// The <see cref="Ohlcv"/> component to use when calculating indicator from an <see cref="Ohlcv"/> data.
            /// </summary>
            public OhlcvComponent OhlcvComponent = OhlcvComponent.ClosingPrice;
        }

        /// <summary>
        /// Identifies possible outputs of the indicator.
        /// </summary>
        public enum OutputKind
        {
            /// <summary>
            /// The scalar value of the the simple moving average.
            /// </summary>
            Value
        }

        private readonly string name;
        private readonly string description;
        private readonly int length;
        private readonly int lastIndex;
        private readonly double[] window;
        private readonly bool useArrayCopy;

        private double value = double.NaN;
        private double windowSum;
        private int windowCount;
        private bool primed;

        /// <summary>
        /// Constructs a new instance of the <see cref="SimpleMovingAverage"/> class.
        /// </summary>
        /// <param name="parameters">Parameters to create the indicator.</param>
        public SimpleMovingAverage(Parameters parameters)
            : base(parameters.OhlcvComponent)
        {
            if (2 > parameters.Length)
                throw new ArgumentOutOfRangeException(nameof(parameters.Length), "Should be greater than 1.");
            length = parameters.Length;
            lastIndex = length - 1;
            window = new double[length];
            useArrayCopy = length > 32;
            name = Invariant($"sma({length},{parameters.OhlcvComponent.ToShortString()})");
            description = string.Concat("Simple moving average ", name);
        }

        #region IIndicator implementation
        /// <inheritdoc />
        public override void Reset()
        {
            lock (UpdateLock)
            {
                windowCount = 0;
                windowSum = 0d;
                value = double.NaN;
                primed = false;
            }
        }

        /// <inheritdoc />
        public override bool IsPrimed { get { lock (UpdateLock) { return primed; } } }

        /// <inheritdoc />
        public override IndicatorMetadata Metadata
        {
            get
            {
                return new IndicatorMetadata
                {
                    IndicatorType = IndicatorType.SimpleMovingAverage,
                    Outputs = new []
                    {
                        new Metadata
                        {
                            Kind = (int)OutputKind.Value,
                            Type = IndicatorOutputType.Scalar,
                            Name = name,
                            Description = description
                        }
                    }
                };
            }
        }
        #endregion

        /// <summary>
        /// Updates the value of the simple moving average using the formula:
        /// <para><c>SMAᵢ = SMAᵢ₋₁ + (Pᵢ - Pᵢ₋ℓ) / ℓ</c>, where <c>ℓ</c> is the length.</para>
        /// The indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <returns>A new value of the indicator.</returns>
        protected override double Update(double sample)
        {
            if (double.IsNaN(sample))
                return sample;
            if (primed)
            {
                windowSum += sample - window[0];
                if (useArrayCopy)
                {
                    Array.Copy(window, 1, window, 0, lastIndex);
                }
                else
                {
                    for (int i = 0; i < lastIndex; )
                        window[i] = window[++i];
                }

                window[lastIndex] = sample;
                value = windowSum / length;
            }
            else // Not primed.
            {
                windowSum += sample;
                window[windowCount] = sample;
                if (length == ++windowCount)
                {
                    primed = true;
                    value = windowSum / windowCount;
                }
            }
            return value;
        }
    }
}
