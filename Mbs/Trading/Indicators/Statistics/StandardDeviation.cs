using System;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;
using static System.FormattableString;

namespace Mbs.Trading.Indicators.Statistics
{
    /// <summary>
    /// Computes the standard deviation of the samples within a moving window of length <c>ℓ</c> as a square root of variance:
    /// <para><c>σ² = (∑xᵢ² - (∑xᵢ)²/ℓ)/ℓ</c>.</para><para>for the estimation of the population variance, or as:</para>
    /// <para><c>σ² = (∑xᵢ² - (∑xᵢ)²/ℓ)/(ℓ-1)</c>.</para><para>for the unbiased estimation of the sample variance, <c>i={0,…,ℓ-1}</c>.</para>
    /// </summary>
    public sealed class StandardDeviation : ScalarIndicator
    {
        private readonly string name;
        private readonly string description;
        private readonly Variance varianceIndicator;
#pragma warning disable S1450 // Private fields only used as local variables in methods should become local variables
        private double value = double.NaN;
#pragma warning restore S1450

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardDeviation"/> class.
        /// </summary>
        /// <param name="parameters">Parameters to create the indicator.</param>
        public StandardDeviation(Parameters parameters)
            : base(parameters.OhlcvComponent)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(parameters), "Length should be greater than 1.");
            }

            var length = parameters.Length;
            bool isUnbiased = parameters.IsUnbiased;
            varianceIndicator = new Variance(new Variance.Parameters
            {
                Length = length,
                IsUnbiased = isUnbiased,
                OhlcvComponent = parameters.OhlcvComponent,
            });

            var temp = isUnbiased ? "stdev.s" : "stdev.p";
            name = Invariant($"{temp}({length},{parameters.OhlcvComponent.ToShortString()})");
            temp = isUnbiased ? "Standard deviation based on sample variance " : "Standard deviation based on population variance ";
            description = string.Concat(temp, name);
        }

        /// <summary>
        /// Identifies possible outputs of the indicator.
        /// </summary>
        public enum OutputKind
        {
            /// <summary>
            /// The scalar value of the the standard deviation.
            /// </summary>
            Value,
        }

        /// <inheritdoc />
        public override bool IsPrimed => varianceIndicator.IsPrimed;

        /// <inheritdoc />
        public override IndicatorMetadata Metadata
        {
            get
            {
                return new IndicatorMetadata
                {
                    IndicatorType = IndicatorType.StandardDeviation,
                    Outputs = new[]
                    {
                        new Metadata
                        {
                            Kind = (int)OutputKind.Value,
                            Type = IndicatorOutputType.Scalar,
                            Name = name,
                            Description = description,
                        },
                    },
                };
            }
        }

        /// <inheritdoc />
        public override void Reset()
        {
            lock (UpdateLock)
            {
                varianceIndicator.Reset();
                value = double.NaN;
            }
        }

        /// <summary>
        /// Updates the value of the standard deviation, <c>σ</c>.
        /// <para />
        /// Depending on the <see cref="Parameters.IsUnbiased"/>, the value is based on the square root of the unbiased sample variance or on the square root of the square root of the population variance.
        /// <para />
        /// The indicator is not primed during the first <c>ℓ-1</c> updates.
        /// </summary>
        /// <param name="sample">A new sample.</param>
        /// <returns>A new value of the indicator.</returns>
        internal double UpdateSample(double sample)
        {
            if (double.IsNaN(sample))
            {
                return sample;
            }

            value = varianceIndicator.UpdateSample(sample);
            if (!double.IsNaN(value))
            {
                value = Math.Sqrt(value);
            }

            return value;
        }

        /// <inheritdoc />
        protected override double Update(double sample)
        {
            return UpdateSample(sample);
        }

        /// <summary>
        /// The parameters to create the indicator.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Gets or sets the length (the number of time periods, <c>ℓ</c>) of the moving window to calculate the standard deviation, should be greater than 1.
            /// </summary>
            public int Length { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the estimate of the standard deviation is based on the unbiased sample variance or on the population variance. The default value is true.
            /// </summary>
            public bool IsUnbiased { get; set; } = true;

            /// <summary>
            /// Gets or sets the <see cref="Ohlcv"/> component to use when calculating indicator from an <see cref="Ohlcv"/> data.
            /// </summary>
            public OhlcvComponent OhlcvComponent { get; set; } = OhlcvComponent.ClosingPrice;
        }
    }
}
