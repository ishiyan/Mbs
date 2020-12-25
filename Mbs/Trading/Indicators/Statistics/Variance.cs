using System;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;
using static System.FormattableString;

namespace Mbs.Trading.Indicators.Statistics
{
    /// <summary>
    /// Computes the variance of the samples within a moving window of length <c>ℓ</c>:
    /// <para><c>σ² = (∑xᵢ² - (∑xᵢ)²/ℓ)/ℓ</c>.</para><para>for the estimation of the population variance, or as:</para>
    /// <para><c>σ² = (∑xᵢ² - (∑xᵢ)²/ℓ)/(ℓ-1)</c>.</para><para>for the unbiased estimation of the sample variance, <c>i={0,…,ℓ-1}</c>.</para>
    /// </summary>
    public sealed class Variance : ScalarIndicator
    {
        private readonly string description;
        private readonly string name;
        private readonly double[] window;
        private readonly int length;
        private readonly int lastIndex;
        private readonly bool isUnbiased;
        private readonly bool useArrayCopy;
        private double windowSum;
        private double windowSquaredSum;
        private double value;
        private int windowCount;
        private bool primed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Variance"/> class.
        /// </summary>
        /// <param name="parameters">Parameters to create the indicator.</param>
        public Variance(Parameters parameters)
            : base(parameters.OhlcvComponent)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(parameters), "Length should be greater than 1.");
            }

            length = parameters.Length;
            window = new double[length];
            lastIndex = length - 1;
            isUnbiased = parameters.IsUnbiased;
            useArrayCopy = length > 32;
            var temp = isUnbiased ? "var.s" : "var.p";
            name = Invariant($"{temp}({length},{parameters.OhlcvComponent.ToShortString()})");
            temp = isUnbiased ? "Unbiased estimation of the sample variance " : "Estimation of the population variance ";
            description = string.Concat(temp, name);
        }

        /// <summary>
        /// Identifies possible outputs of the indicator.
        /// </summary>
        public enum OutputKind
        {
            /// <summary>
            /// The scalar value of the the variance.
            /// </summary>
            Value,
        }

        /// <inheritdoc />
        public override bool IsPrimed
        {
            get
            {
                lock (UpdateLock)
                {
                    return primed;
                }
            }
        }

        /// <inheritdoc />
        public override IndicatorMetadata Metadata
        {
            get
            {
                return new IndicatorMetadata
                {
                    IndicatorType = IndicatorType.Variance,
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
                primed = false;
                windowCount = 0;
                windowSum = 0d;
                windowSquaredSum = 0d;
                value = double.NaN;
            }
        }

        /// <summary>
        /// Updates the value of the variance, <c>σ²</c>.
        /// <para />
        /// Depending on the <see cref="isUnbiased"/>, the value is the unbiased sample variance or the population variance.
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

            double temp = sample;
            if (primed)
            {
                windowSum += temp;
                temp *= temp;
                windowSquaredSum += temp;
                temp = window[0];
                windowSum -= temp;
                temp *= temp;
                windowSquaredSum -= temp;
                if (isUnbiased)
                {
                    temp = windowSum;
                    temp *= temp;
                    temp /= length;
                    value = windowSquaredSum - temp;
                    value /= lastIndex;
                }
                else
                {
                    temp = windowSum / length;
                    temp *= temp;
                    value = windowSquaredSum / length - temp;
                }

                if (useArrayCopy)
                {
                    Array.Copy(window, 1, window, 0, lastIndex);
                }
                else
                {
                    for (int i = 0; i < lastIndex;)
                    {
                        window[i] = window[++i];
                    }
                }

                window[lastIndex] = sample;
            }
            else
            {
                windowSum += temp;
                window[windowCount] = temp;
                temp *= temp;
                windowSquaredSum += temp;
                if (length == ++windowCount)
                {
                    primed = true;
                    if (isUnbiased)
                    {
                        temp = windowSum;
                        temp *= temp;
                        temp /= length;
                        value = windowSquaredSum - temp;
                        value /= lastIndex;
                    }
                    else
                    {
                        temp = windowSum / length;
                        temp *= temp;
                        value = windowSquaredSum / length - temp;
                    }
                }
                else
                {
                    return double.NaN;
                }
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
            /// Gets or sets the length (the number of time periods, <c>ℓ</c>) of the moving window to calculate the variance, should be greater than 1.
            /// </summary>
            public int Length { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the estimate of the variance is the unbiased sample variance or the population variance. The default value is true.
            /// </summary>
            public bool IsUnbiased { get; set; } = true;

            /// <summary>
            /// Gets or sets the <see cref="Ohlcv"/> component to use when calculating indicator from an <see cref="Ohlcv"/> data.
            /// </summary>
            public OhlcvComponent OhlcvComponent { get; set; } = OhlcvComponent.ClosingPrice;
        }
    }
}
