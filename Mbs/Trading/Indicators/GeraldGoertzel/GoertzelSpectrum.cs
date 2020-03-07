using System;
using System.Collections.Generic;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;
using static System.FormattableString;

namespace Mbs.Trading.Indicators.GeraldGoertzel
{
    /// <summary>
    /// Calculates a power spectrum heat-map of the cyclic activity over a cycle period range using the Goertzel algorithm.
    /// </summary>
    public sealed class GoertzelSpectrum : IIndicator
    {
        /// <summary>
        /// The parameters to create the indicator.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// The length (the number of time periods) of the sample window.
            /// <para />
            /// This determines the minimum and maximum spectrum periods.
            /// </summary>
            public int Length = 64;

            /// <summary>
            /// The minimum period to calculate, must be less than the <c>MaxPeriod</c>.
            /// The lowest value, 2, corresponds to the Nyquist (the maximum representable) frequency
            /// </summary>
            public double MinPeriod = 2;

            /// <summary>
            /// The maximum period to calculate.
            /// The highest value is equal to the observed time lapse (Length samples).
            /// </summary>
            public double MaxPeriod = 64;

            /// <summary>
            /// The period resolution (positive number). A value of 10 means that spectrum is evaluated at every 0.1 of a period range.
            /// </summary>
            public double PeriodResolution = 1;

            /// <summary>
            /// If the first or the second order Goertzel algorithm should be used.
            /// </summary>
            public bool IsFirstOrder = false;

            /// <summary>
            /// Specifies if the spectral dilation should be compensated.
            /// </summary>
            public bool IsSpectralDilationCompensation = true;

            /// <summary>
            /// Specifies if the <c>fast attack − slow decay</c> automatic gain control should be used.
            /// </summary>
            public bool IsAutomaticGainControl = false;

            /// <summary>
            /// Specifies the decay factor for the <c>fast attack − slow decay</c> automatic gain control.
            /// </summary>
            public double AutomaticGainControlDecayFactor = 0.991;

            /// <summary>
            /// The <see cref="Ohlcv"/> component to use when calculating power spectrum from an <see cref="Ohlcv"/> data.
            /// </summary>
            public OhlcvComponent OhlcvComponent = OhlcvComponent.MedianPrice;
        }

        /// <summary>
        /// Identifies possible outputs of the indicator.
        /// </summary>
        public enum OutputKind
        {
            /// <summary>
            /// The power spectrum columns.
            /// </summary>
            PowerSpectrum,

            /// <summary>
            /// The normalized to [0,1] power spectrum columns.
            /// </summary>
            PowerSpectrumNormalizedToZeroOne,

            /// <summary>
            /// The natural logarithm of the power spectrum columns.
            /// </summary>
            LogPowerSpectrum,

            /// <summary>
            /// The normalized to [0,1]  natural logarithm of the power spectrum columns.
            /// </summary>
            LogPowerSpectrumNormalizedToZeroOne
        }

        /// <summary>
        /// The minimum ordinate (parameter) value of the heat-map. This value is the same for all heat-map columns.
        /// </summary>
        public double MinParameterValue => estimator.MinPeriod;

        /// <summary>
        /// The maximum ordinate (parameter) value of the heat-map. This value is the same for all heat-map columns.
        /// </summary>
        public double MaxParameterValue => estimator.MaxPeriod;

        /// <summary>
        /// The length (the number of time periods) of the sample window.
        /// This determines the minimum and maximum spectrum periods.
        /// </summary>
        public int Length => estimator.Length;

        /// <summary>
        /// If the first or the second order algorithm is used.
        /// </summary>
        public bool IsFirstOrder => estimator.IsFirstOrder;

        /// <summary>
        /// If the spectral dilation compensation is used.
        /// </summary>
        public bool IsSpectralDilationCompensation => estimator.IsSpectralDilationCompensation;

        /// <summary>
        /// An automatic gain control decay factor.
        /// </summary>
        public double AutomaticGainControlDecayFactor => estimator.AutomaticGainControlDecayFactor;

        /// <summary>
        /// If the <c>fast attack − slow decay</c> automatic gain control is used.
        /// </summary>
        public bool IsAutomaticGainControl => estimator.IsAutomaticGainControl;

        /// <summary>
        /// The period resolution (positive number).
        /// A value of 10 means that spectrum is evaluated at every 0.1 of a period range.
        /// </summary>
        public double PeriodResolution => estimator.PeriodResolution;

        private readonly object updateLock = new object();
        private readonly string name;
        private readonly string description;
        private readonly GoertzelSpectrumEstimator estimator;
        private readonly List<OutputKind> outputs;
        private readonly int lastIndex;
        private readonly OhlcvComponent ohlcvComponent;
        private int windowCount;
        private bool primed;

        /// <summary>
        /// Constructs a new instance of the <see cref="GoertzelSpectrum"/> class.
        /// </summary>
        /// <param name="parameters">Parameters to create the indicator.</param>
        /// <param name="outputKinds">Outputs of the indicator.</param>
        public GoertzelSpectrum(Parameters parameters, int[] outputKinds)
        {
            if (2 > parameters.Length)
                throw new ArgumentOutOfRangeException(nameof(parameters.Length), "Should be greater than 1.");

            estimator = new GoertzelSpectrumEstimator(parameters.Length, parameters.MinPeriod, parameters.MaxPeriod,
                parameters.PeriodResolution, parameters.IsFirstOrder, parameters.IsSpectralDilationCompensation,
                parameters.IsAutomaticGainControl, parameters.AutomaticGainControlDecayFactor);
            lastIndex = estimator.Length - 1;

            var order = IsFirstOrder ? ", 1st order" : "";
            var sdc = IsSpectralDilationCompensation ? ", sdc" : "";
            var agc = IsAutomaticGainControl ? Invariant($", agc {AutomaticGainControlDecayFactor}") : "";
            name = Invariant($"goertzel({Length}, [{MinParameterValue},{MaxParameterValue},1/{parameters.PeriodResolution}]{order}{sdc}{agc})");
            description = string.Concat("Goertzel power spectrum ", name);

            outputs = ConvertOutputKinds(outputKinds);
            ohlcvComponent = parameters.OhlcvComponent;
        }

        #region IIndicator implementation
        /// <inheritdoc />
        public void Reset()
        {
            lock (updateLock)
            {
                primed = false;
                windowCount = 0;
                estimator.Reset();
            }
        }

        /// <inheritdoc />
        public bool IsPrimed { get { lock (updateLock) { return primed; } } }

        /// <inheritdoc />
        public IndicatorMetadata Metadata
        {
            get
            {
                var result = new IndicatorMetadata
                {
                    IndicatorType = IndicatorType.GoertzelSpectrum,
                    Outputs = new Metadata[outputs.Count]
                };

                for (int i = 0; i < outputs.Count; ++i)
                {
                    var metadata = new Metadata { Kind = (int)outputs[i], Type = IndicatorOutputType.HeatMap };
                    result.Outputs[i] = metadata;

                    switch (outputs[i])
                    {
                        case OutputKind.PowerSpectrum:
                            metadata.Name = name;
                            metadata.Description = description;
                            break;

                        case OutputKind.PowerSpectrumNormalizedToZeroOne:
                            metadata.Name = name;
                            metadata.Description = string.Concat("Normalized to [0, 1] ", description);
                            break;

                        case OutputKind.LogPowerSpectrum:
                            metadata.Name = string.Concat("ln(", name, ")");
                            metadata.Description = string.Concat("Natural logarithm of ", description);
                            break;

                        case OutputKind.LogPowerSpectrumNormalizedToZeroOne:
                            metadata.Name = string.Concat("ln(", name, ")");
                            metadata.Description = string.Concat("Normalized to [0, 1] natural logarithm of ", description);
                            break;
                    }
                }

                return result;
            }
        }

        /// <inheritdoc />
        public IndicatorOutput Update(Scalar sample)
        {
            return Update(sample.Time, sample.Value);
        }

        /// <inheritdoc />
        public IndicatorOutput Update(Ohlcv sample)
        {
            return Update(sample.Time, sample.Component(ohlcvComponent));
        }

        /// <inheritdoc />
        public IEnumerable<IndicatorOutput> Update(IEnumerable<Scalar> samples)
        {
            var list = new List<IndicatorOutput>();
            lock (updateLock)
            {
                foreach (var sample in samples)
                {
                    var outputData = Update(sample.Time, sample.Value);
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
                    var outputData = Update(sample.Time, sample.Component(ohlcvComponent));
                    list.Add(outputData);
                }
            }

            return list;
        }
        #endregion

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

        private double[] Calculate(double sample)
        {
            if (double.IsNaN(sample))
            {
                return null;
            }

            double[] window = estimator.InputSeries;
            if (primed)
            {
                Array.Copy(window, 1, window, 0, lastIndex);
                window[lastIndex] = sample;
            }
            else // Not primed.
            {
                window[windowCount] = sample;
                if (estimator.Length == ++windowCount)
                    primed = true;
            }

            if (primed)
            {
                estimator.Calculate();
                return (double[])estimator.Spectrum.Clone();
            }

            return null;
        }

        private IndicatorOutput Update(DateTime time, double sample)
        {
            var outputData = new IndicatorOutput { Outputs = new object[outputs.Count] };
            lock (updateLock)
            {
                var intensity = Calculate(sample);
                FillOutput(outputData, time, intensity);
            }

            return outputData;
        }

        private void FillOutput(IndicatorOutput outputData, DateTime time, double[] intensity)
        {
            for (int i = 0; i < outputs.Count; ++i)
            {
                switch (outputs[i])
                {
                    case OutputKind.PowerSpectrum:
                        if (intensity == null)
                            outputData.Outputs[i] = new HeatMap(time,
                                estimator.MaxPeriod, estimator.MinPeriod, estimator.PeriodResolution);
                        else
                            outputData.Outputs[i] = new HeatMap(time,
                                estimator.MaxPeriod, estimator.MinPeriod, estimator.PeriodResolution,
                                estimator.SpectrumMin, estimator.SpectrumMax, intensity);
                        break;

                    case OutputKind.LogPowerSpectrum:
                        if (intensity == null)
                            outputData.Outputs[i] = new HeatMap(time,
                                estimator.MaxPeriod, estimator.MinPeriod, estimator.PeriodResolution);
                        else
                        {
                            var logCopy = LogCopy(intensity, estimator.SpectrumMin, estimator.SpectrumMax,
                                out double logMin, out double logMax);
                            outputData.Outputs[i] = new HeatMap(time,
                                estimator.MaxPeriod, estimator.MinPeriod, estimator.PeriodResolution,
                                logMin, logMax, logCopy);
                        }
                        break;

                    case OutputKind.PowerSpectrumNormalizedToZeroOne:
                        if (intensity == null)
                            outputData.Outputs[i] = new HeatMap(time,
                                estimator.MaxPeriod, estimator.MinPeriod, estimator.PeriodResolution);
                        else
                        {
                            var normCopy = NormCopy(intensity, estimator.SpectrumMin, estimator.SpectrumMax);
                            outputData.Outputs[i] = new HeatMap(time,
                                estimator.MaxPeriod, estimator.MinPeriod, estimator.PeriodResolution,
                                0, 1, normCopy);
                        }
                        break;

                    case OutputKind.LogPowerSpectrumNormalizedToZeroOne:
                        if (intensity == null)
                            outputData.Outputs[i] = new HeatMap(time,
                                estimator.MaxPeriod, estimator.MinPeriod, estimator.PeriodResolution);
                        else
                        {
                            var logCopy = LogCopy(intensity, estimator.SpectrumMin, estimator.SpectrumMax,
                                out double logMin, out double logMax);
                            var normCopy = NormCopy(logCopy, logMin, logMax);
                            outputData.Outputs[i] = new HeatMap(time,
                                estimator.MaxPeriod, estimator.MinPeriod, estimator.PeriodResolution,
                                0, 1, normCopy);
                        }
                        break;
                }
            }
        }

        private static double[] LogCopy(double[] source, double min, double max, out double logMin, out double logMax)
        {
            var copy = new double[source.Length];
            for (int i = 0; i < source.Length; ++i)
            {
                var value = Math.Log(source[i]);
                if (value < 0)
                    value = 0;
                copy[i] = value;
            }

            logMin = Math.Log(min);
            if (logMin < 0)
                logMin = 0;
            logMax = Math.Log(max);
            if (logMax < 0)
                logMax = 0;

            return copy;
        }

        private static double[] NormCopy(double[] source, double min, double max)
        {
            var delta = max - min;
            var copy = new double[source.Length];
            for (int i = 0; i < source.Length; ++i)
            {
                copy[i] = (source[i] - min) / delta;
            }

            return copy;
        }
    }
}
