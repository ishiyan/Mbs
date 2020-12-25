using System;
using System.Collections.Generic;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;
using Mbs.Trading.Indicators.Statistics;
using static System.FormattableString;

namespace Mbs.Trading.Indicators.JohnBollinger
{
    /// <summary>
    /// Bollinger Bands are a type of price envelope invented by John Bollinger in the 1980s. Bollinger bands consist of:
    /// <para>❶ a middle band being an <c>ℓ</c>-period moving average (MA).</para>
    /// <para>❷ an upper band at <c>K</c> times an <c>ℓ</c>-period standard deviation <c>σ</c> above the middle band (<c>MA + Kσ</c>).</para>
    /// <para>❸ a lower band at <c>K</c> times an <c>ℓ</c>-period standard deviation <c>σ</c> below the middle band (<c>MA - Kσ</c>).</para>
    /// <para>Typical values for <c>ℓ</c> and <c>K</c> are 20 and 2, respectively. The default choice for the average is a simple moving average, but other types of averages can be employed as needed.</para>
    /// <para>Exponential moving averages are a common second choice. Usually the same period is used for both the middle band and the calculation of standard deviation.</para>
    /// </summary>
    public sealed class BollingerBands : IIndicator
    {
        private readonly object updateLock = new object();
        private readonly double multiplier;
        private readonly OhlcvComponent percentBOhlcvComponent;
        private readonly OhlcvComponent stdevOhlcvComponent;
        private readonly IIndicator movingAverageIndicator;
        private readonly StandardDeviation standardDeviationIndicator;
        private readonly string name;
        private readonly string description;
        private readonly string percentBandName;
        private readonly string percentBandDescription;
        private readonly List<OutputKind> outputs;
        private double movingAverageValue = double.NaN;
        private double stdevValue = double.NaN;
        private double upperValue = double.NaN;
        private double lowerValue = double.NaN;
        private double bandWidthValue = double.NaN;
        private double percentBandValue = double.NaN;
        private bool primed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BollingerBands"/> class.
        /// </summary>
        /// <param name="parameters">Parameters to create the indicator.</param>
        /// <param name="outputKinds">Outputs of the indicator.</param>
        public BollingerBands(Parameters parameters, int[] outputKinds)
        {
            if (parameters.StandardDeviationLength < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(parameters), "StandardDeviationLength should be greater than 1.");
            }

            var standardDeviationLength = parameters.StandardDeviationLength;
            stdevOhlcvComponent = parameters.StandardDeviationOhlcvComponent;
            try
            {
                standardDeviationIndicator = new StandardDeviation(new StandardDeviation.Parameters
                {
                    Length = standardDeviationLength,
                    OhlcvComponent = stdevOhlcvComponent,
                    IsUnbiased = parameters.StandardDeviationIsUnbiased,
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(
                    $"Failed to create standard deviation indicator of length {standardDeviationLength}.", ex);
            }

            multiplier = parameters.Multiplier;
            percentBOhlcvComponent = parameters.PercentBOhlcvComponent;
            try
            {
                movingAverageIndicator = IndicatorFactory.Create(parameters.MovingAverageParameters);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Failed to create moving average indicator.", ex);
            }

            var maMetaData = movingAverageIndicator.Metadata;
            if (maMetaData.Outputs.Length != 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(parameters),
                    $"Moving average indicator of type '{maMetaData.IndicatorType}' should have a single output specified.");
            }

            if (maMetaData.Outputs[0].Type != IndicatorOutputType.Scalar)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(parameters),
                    $"Moving average indicator of type '{maMetaData.IndicatorType}' should have a scalar as the only output.");
            }

            var stdevMetaData = standardDeviationIndicator.Metadata;
            if (stdevMetaData.Outputs.Length != 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(parameters),
                    "Standard deviation indicator should have a single output specified.");
            }

            if (stdevMetaData.Outputs[0].Type != IndicatorOutputType.Scalar)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(parameters),
                    "Standard deviation indicator should have a scalar as the only output.");
            }

            name = Invariant($"bb({stdevMetaData.Outputs[0].Name},{multiplier},{maMetaData.Outputs[0].Name})");
            description = string.Concat("Bollinger Bands ", name);
            percentBandName = Invariant($"%b({percentBOhlcvComponent.ToShortString()})-{name}");
            percentBandDescription = Invariant($"%B({percentBOhlcvComponent.ToShortString()}) of {description}");

            outputs = ConvertOutputKinds(outputKinds);
        }

        /// <summary>
        /// Identifies possible outputs of the indicator.
        /// </summary>
        public enum OutputKind
        {
            /// <summary>
            /// The <see cref="Scalar"/> value of the the middle moving average.
            /// </summary>
            MiddleMovingAverageValue,

            /// <summary>
            /// The <see cref="Scalar"/> value of the standard deviation.
            /// </summary>
            StandardDeviationValue,

            /// <summary>
            /// The <see cref="Scalar"/> value of the the lower Bollinger band.
            /// </summary>
            LowerBandValue,

            /// <summary>
            /// The <see cref="Scalar"/> value of the the upper Bollinger band.
            /// </summary>
            UpperBandValue,

            /// <summary>
            /// The <see cref="Scalar"/> value of the the %B.
            /// <para/>
            /// It measures the price (an input to the moving average) relative to the upper and lower band.
            /// <para/>
            /// <c>%B = (Price - LowerBand) / (UpperBand - LowerBand)</c>.
            /// </summary>
            PercentBandValue,

            /// <summary>
            /// The <see cref="Scalar"/> value of the the Bollinger BandWidth.
            /// <para/>
            /// It measures the percentage difference between the upper band and the lower band.
            /// <para/>
            /// <c>BandWidth = (UpperBand - LowerBand) / MiddleMovingAverage</c>.
            /// </summary>
            BandWidthValue,

            /// <summary>
            /// The <see cref="Band"/> containing the lower and the upper band values.
            /// </summary>
            LowerUpperBand,
        }

        /// <inheritdoc />
        public bool IsPrimed
        {
            get
            {
                lock (updateLock)
                {
                    return primed;
                }
            }
        }

        /// <inheritdoc />
        public IndicatorMetadata Metadata
        {
            get
            {
                var result = new IndicatorMetadata
                {
                    IndicatorType = IndicatorType.BollingerBands,
                    Outputs = new Metadata[outputs.Count],
                };

                for (int i = 0; i < outputs.Count; ++i)
                {
                    var metadata = new Metadata { Kind = (int)outputs[i], Type = IndicatorOutputType.Scalar };
                    result.Outputs[i] = metadata;

                    switch (outputs[i])
                    {
                        case OutputKind.LowerUpperBand:
                            metadata.Type = IndicatorOutputType.Band;
                            metadata.Name = name;
                            metadata.Description = description;
                            break;

                        case OutputKind.MiddleMovingAverageValue:
                            metadata.Name = string.Concat("ma-", name);
                            metadata.Description = string.Concat("Moving Average used by ", description);
                            break;

                        case OutputKind.LowerBandValue:
                            metadata.Name = string.Concat("lo-", name);
                            metadata.Description = string.Concat("Lower Band of ", description);
                            break;

                        case OutputKind.UpperBandValue:
                            metadata.Name = string.Concat("up-", name);
                            metadata.Description = string.Concat("Upper Band of ", description);
                            break;

                        case OutputKind.StandardDeviationValue:
                            metadata.Name = string.Concat("stdev-", name);
                            metadata.Description = string.Concat("Standard Deviation of ", description);
                            break;

                        case OutputKind.BandWidthValue:
                            metadata.Name = string.Concat("bw-", name);
                            metadata.Description = string.Concat("Band Width of ", description);
                            break;

                        case OutputKind.PercentBandValue:
                            metadata.Name = percentBandName;
                            metadata.Description = percentBandDescription;
                            break;
                    }
                }

                return result;
            }
        }

        /// <inheritdoc />
        public void Reset()
        {
            lock (updateLock)
            {
                movingAverageValue = double.NaN;
                stdevValue = double.NaN;
                lowerValue = double.NaN;
                upperValue = double.NaN;
                bandWidthValue = double.NaN;
                percentBandValue = double.NaN;
                primed = false;
                movingAverageIndicator.Reset();
                standardDeviationIndicator.Reset();
            }
        }

        /// <inheritdoc />
        public IndicatorOutput Update(Scalar sample)
        {
            double v = sample.Value;
            return Update(new Ohlcv(sample.Time, v, v, v, v));
        }

        /// <inheritdoc />
        public IndicatorOutput Update(Ohlcv sample)
        {
            var outputData = new IndicatorOutput { Outputs = new object[outputs.Count] };
            lock (updateLock)
            {
                Calculate(sample);
                FillOutput(outputData, sample.Time);
            }

            return outputData;
        }

        /// <inheritdoc />
        public IEnumerable<IndicatorOutput> Update(IEnumerable<Scalar> samples)
        {
            var list = new List<IndicatorOutput>();
            lock (updateLock)
            {
                foreach (var sample in samples)
                {
                    var outputData = new IndicatorOutput { Outputs = new object[outputs.Count] };
                    double v = sample.Value;
                    Calculate(new Ohlcv(sample.Time, v, v, v, v));
                    FillOutput(outputData, sample.Time);
                    list.Add(outputData);
                }
            }

            return list;
        }

        /// <inheritdoc />
        public IEnumerable<IndicatorOutput> Update(IEnumerable<Ohlcv> samples)
        {
            var list = new List<IndicatorOutput>();
            lock (updateLock)
            {
                foreach (var sample in samples)
                {
                    var outputData = new IndicatorOutput { Outputs = new object[outputs.Count] };
                    Calculate(sample);
                    FillOutput(outputData, sample.Time);
                    list.Add(outputData);
                }
            }

            return list;
        }

        private static List<OutputKind> ConvertOutputKinds(int[] outputKinds)
        {
            var list = new List<OutputKind>();
            foreach (var outputKind in outputKinds)
            {
                try
                {
                    list.Add((OutputKind)outputKind);
                }
                catch (Exception ex)
                {
                    throw new ArgumentOutOfRangeException(Invariant($"Output kind {outputKind} does not match any value in {nameof(OutputKind)} enumeration"), ex);
                }
            }

            return list;
        }

        private void FillOutput(IndicatorOutput outputData, DateTime time)
        {
            for (int i = 0; i < outputs.Count; ++i)
            {
                outputData.Outputs[i] = outputs[i] switch
                {
                    OutputKind.LowerUpperBand => new Band(time, lowerValue, upperValue),
                    OutputKind.MiddleMovingAverageValue => new Scalar(time, movingAverageValue),
                    OutputKind.LowerBandValue => new Scalar(time, lowerValue),
                    OutputKind.UpperBandValue => new Scalar(time, upperValue),
                    OutputKind.StandardDeviationValue => new Scalar(time, stdevValue),
                    OutputKind.BandWidthValue => new Scalar(time, bandWidthValue),
                    OutputKind.PercentBandValue => new Scalar(time, percentBandValue),
                    _ => outputData.Outputs[i]
                };
            }
        }

        private void Calculate(Ohlcv sample)
        {
            var movingAverageOutput = movingAverageIndicator.Update(sample);
            movingAverageValue = ((Scalar)movingAverageOutput.Outputs[0]).Value;
            stdevValue = standardDeviationIndicator.UpdateSample(sample.Component(stdevOhlcvComponent));

            if (double.IsNaN(stdevValue))
            {
                lowerValue = double.NaN;
                upperValue = double.NaN;
                bandWidthValue = double.NaN;
                percentBandValue = double.NaN;
                return;
            }

            if (!primed)
            {
                primed = standardDeviationIndicator.IsPrimed && movingAverageIndicator.IsPrimed;
                if (!primed)
                {
                    return;
                }
            }

            var delta = stdevValue * multiplier;
            lowerValue = movingAverageValue - delta;
            upperValue = movingAverageValue + delta;
            delta = upperValue - lowerValue;
            bandWidthValue = movingAverageValue < double.Epsilon ? 1d : delta / movingAverageValue;
            percentBandValue = delta < double.Epsilon ? 1d : (sample.Component(percentBOhlcvComponent) - lowerValue) / delta;
        }

        /// <summary>
        /// The parameters to create the indicator.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Gets or sets the length (the number of time periods) to calculate the standard deviation.
            /// <para />
            /// Typically this should be equal to the length of the moving average.
            /// </summary>
            public int StandardDeviationLength { get; set; } = 20;

            /// <summary>
            /// Gets or sets the <see cref="Ohlcv"/> component to use when calculating standard deviation from an <see cref="Ohlcv"/> data.
            /// </summary>
            public OhlcvComponent StandardDeviationOhlcvComponent { get; set; } = OhlcvComponent.ClosingPrice;

            /// <summary>
            /// Gets or sets a value indicating whether the estimate of the standard deviation is based on the unbiased sample variance or on the population variance.
            /// </summary>
            public bool StandardDeviationIsUnbiased { get; set; }

            /// <summary>
            /// Gets or sets the multiplier to multiply the standard deviation.
            /// </summary>
            public double Multiplier { get; set; } = 2.0;

            /// <summary>
            /// Gets or sets the parameters to create a middle moving average.
            /// <para />
            /// Typically the length of the moving average should be equal to the length used to calculate the standard deviation.
            /// </summary>
            public IndicatorInput MovingAverageParameters { get; set; } = new IndicatorInput
            {
                IndicatorType = IndicatorType.SimpleMovingAverage,
                Parameters = new SimpleMovingAverage.Parameters { Length = 20, OhlcvComponent = OhlcvComponent.ClosingPrice },
                OutputKinds = new[] { (int)SimpleMovingAverage.OutputKind.Value },
            };

            /// <summary>
            /// Gets or sets the <see cref="Ohlcv"/> component to use when calculating <c>%B</c> value from an <see cref="Ohlcv"/> data.
            /// </summary>
            public OhlcvComponent PercentBOhlcvComponent { get; set; } = OhlcvComponent.ClosingPrice;
        }
    }
}
