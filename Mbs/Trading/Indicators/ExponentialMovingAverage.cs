using System;
using static System.FormattableString;

using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;

namespace Mbs.Trading.Indicators
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
    public sealed class ExponentialMovingAverage : ScalarIndicator
    {
        /// <summary>
        /// The parameters to create the indicator based on length.
        /// </summary>
        public class ParametersLength
        {
            /// <summary>
            /// The length (the number of time periods) of the exponential moving average, should be greater than 1.
            /// </summary>
            public int Length;

            /// <summary>
            /// If the very first exponential moving average value is a simple average of the first 'period' (the most widely documented approach) or the first input value (used in Metastock).
            /// </summary>
            public bool FirstIsAverage = true;

            /// <summary>
            /// The <see cref="Ohlcv"/> component to use when calculating indicator from an <see cref="Ohlcv"/> data.
            /// </summary>
            public OhlcvComponent OhlcvComponent = OhlcvComponent.ClosingPrice;
        }

        /// <summary>
        /// The parameters to create the indicator based on smoothing factor.
        /// </summary>
        public class ParametersSmoothingFactor
        {
            /// <summary>
            /// The smoothing factor, <c>α</c>, of the exponential moving average.
            /// The equivalent length <c>ℓ</c> is
            /// <para><c>ℓ = 2/α - 1, 0 &lt; α ≤ 1, 1 ≤ ℓ</c></para>
            /// </summary>
            public double SmoothingFactor;

            /// <summary>
            /// If the very first exponential moving average value is a simple average of the first 'period' (the most widely documented approach) or the first input value (used in Metastock).
            /// </summary>
            public bool FirstIsAverage = true;

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
            /// The scalar value of the the exponential moving average.
            /// </summary>
            Value
        }

        private const double Epsilon = 0.00000001;

        private readonly string name;
        private readonly string description;
        private readonly int length;
        private readonly double smoothingFactor;
        private readonly bool firstIsAverage;

        private double value = double.NaN;
        private double sum;
        private int count;
        private bool primed;

        /// <summary>
        /// Constructs a new instance of the <see cref="ExponentialMovingAverage"/> class based on length.
        /// </summary>
        /// <param name="parameters">Parameters to create the indicator based on length.</param>
        public ExponentialMovingAverage(ParametersLength parameters)
            : base(parameters.OhlcvComponent)
        {
            if (1 > parameters.Length)
                throw new ArgumentOutOfRangeException(nameof(parameters.Length), "Should be positive.");
            length = parameters.Length;
            firstIsAverage = parameters.FirstIsAverage;
            smoothingFactor = 2d / (1 + length);
            name = Invariant($"ema({length},{parameters.OhlcvComponent.ToShortString()})");
            description = string.Concat("Exponential moving average ", name);
        }

        /// <summary>
        /// Constructs a new instance of the <see cref="ExponentialMovingAverage"/> class based on smoothing factor.
        /// </summary>
        /// <param name="parameters">Parameters to create the indicator based on smoothing factor.</param>
        public ExponentialMovingAverage(ParametersSmoothingFactor parameters)
            : base(parameters.OhlcvComponent)
        {
            smoothingFactor = parameters.SmoothingFactor;
            if (0d > smoothingFactor || 1d < smoothingFactor)
                throw new ArgumentOutOfRangeException(nameof(parameters.SmoothingFactor), "Should be in range [0, 1].");
            if (Epsilon > smoothingFactor)
                length = int.MaxValue;
            else
                length = (int)Math.Round(2d / smoothingFactor) - 1;
            firstIsAverage = parameters.FirstIsAverage;
            name = Invariant($"ema({length},{parameters.OhlcvComponent.ToShortString()})");
            description = string.Concat("Exponential moving average ", name);
        }

        #region IIndicator implementation
        /// <inheritdoc />
        public override void Reset()
        {
            lock (UpdateLock)
            {
                primed = false;
                count = 0;
                sum = 0d;
                value = double.NaN;
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
                    IndicatorType = IndicatorType.ExponentialMovingAverage,
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
        /// Updates the value of the exponential moving average from the input array using the formula:
        /// <para><c>EMAᵢ = EMAᵢ₋₁ + α(Pᵢ - EMAᵢ₋₁), 0 ≤ α ≤ 1</c></para>
        /// The indicator is primed after the first update.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <returns>A new value of the indicator.</returns>
        protected override double Update(double sample)
        {
            if (double.IsNaN(sample))
                return sample;
            if (primed)
                value += (sample - value) * smoothingFactor;
            else
            {
                if (firstIsAverage)
                {
                    sum += sample;
                    if (length == ++count)
                    {
                        primed = true;
                        value = sum / length;
                    }
                    else
                        return double.NaN;
                }
                else
                {
                    if (1 == ++count)
                        value = sample;
                    else
                        value += (sample - value) * smoothingFactor;
                    if (length == count)
                        primed = true;
                    else
                        return double.NaN;
                }
            }

            return value;
        }
    }
}
