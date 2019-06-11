using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mbs.Numerics;
using Mbs.Numerics.Random;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators.MultiSinusoidal
{
    /// <summary>
    /// The multi-sinusoidal waveform generator produces samples which form a
    /// sum of component sinusoids defined by the given periods, amplitudes and phases.
    /// An optional noise may be added to the samples.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public abstract class MultiSinusoidalDataGenerator<T> : WaveformDataGenerator<T>
        where T : TemporalEntity, new()
    {
        /// <summary>
        /// Gets the number of the component sinusoids.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Gets the periods of the component sinusoids in samples, should be ≥ 2.
        /// </summary>
        public IReadOnlyCollection<double> Periods => Array.AsReadOnly(periods);

        /// <summary>
        /// Gets the amplitudes of the component sinusoids in price units, should be positive.
        /// </summary>
        public IReadOnlyCollection<double> Amplitudes => Array.AsReadOnly(amplitudes);

        /// <summary>
        /// Gets the phases, φ, of the component sinusoids in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </summary>
        public IReadOnlyCollection<double> PhasesInPi => Array.AsReadOnly(phasesInPi);

        /// <summary>
        /// Gets the sample value corresponding to the minimum of the sum of component sinusoids, should be positive.
        /// </summary>
        public double SampleMinimum { get; }

        private readonly double[] periods;
        private readonly double[] amplitudes;
        private readonly double[] phasesInPi;
        private readonly double[] angles;
        private readonly double[] frequencies;
        private readonly double[] phases;
        private readonly double sampleMaximum;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSinusoidalDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="timeParameters">The time-related input parameters for the <see cref="TemporalDataGenerator{T}"/>.</param>
        /// <param name="waveformParameters">The input parameters for the <see cref="WaveformDataGenerator{T}"/>.</param>
        /// <param name="multiSinusoidalParameters">The input parameters for the <see cref="MultiSinusoidalDataGenerator{T}"/>.</param>
        protected MultiSinusoidalDataGenerator(
            TimeParameters timeParameters,
            WaveformParameters waveformParameters,
            MultiSinusoidalParameters multiSinusoidalParameters)
            : base(timeParameters, waveformParameters)
        {
            var array = multiSinusoidalParameters.MultiSinusoidalComponents.ToArray();
            Count = /*multiSinusoidalParameters.MultiSinusoidalComponents*/array.Length;
            if (Count < 1)
            {
                throw new ArgumentException(
                    "The length of the input multi-sinusoidal components array must be positive.",
                    nameof(multiSinusoidalParameters));
            }

            periods = new double[Count];
            amplitudes = new double[Count];
            phasesInPi = new double[Count];
            angles = new double[Count];
            frequencies = new double[Count];
            phases = new double[Count];

            for (int i = 0; i < Count; ++i)
            {
                var item = /*multiSinusoidalParameters.MultiSinusoidalComponents*/array[i];
                periods[i] = item.Period;
                amplitudes[i] = item.Amplitude;
                phasesInPi[i] = item.PhaseInPi;
            }

            SampleMinimum = multiSinusoidalParameters.MinimalValue;
            sampleMaximum = SampleMinimum + MaximalAmplitude(amplitudes);
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSinusoidalDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="sessionBeginTime">The time of the beginning of the trading session.</param>
        /// <param name="sessionEndTime">The end time of the trading session.</param>
        /// <param name="startDate">The date of the first data sample.</param>
        /// <param name="timeGranularity">The time granularity of data samples.</param>
        /// <param name="businessDayCalendar">The value specifying an exchange holiday schedule or a general country holiday schedule.</param>
        /// <param name="waveformSamples">The number of samples in the waveform, should be ≥ 2..</param>
        /// <param name="offsetSamples">The number of samples before the very first waveform. The value of zero means the waveform starts immediately.</param>
        /// <param name="repetitionsCount">The number of repetitions of the waveform. The value of zero means infinite.</param>
        /// <param name="noiseAmplitudeFraction">The amplitude of the noise as a fraction of the mid price.</param>
        /// <param name="noiseRandomGenerator">The uniformly distributed random generator to produce the noise.</param>
        /// <param name="periods">The array of periods of the component sinusoids in samples, should be ≥ 2.</param>
        /// <param name="amplitudes">The array of amplitudes of the component sinusoids in price units, should be positive.</param>
        /// <param name="sampleMinimum">The value corresponding to the minimum of the sum of component sinusoids, should be positive.</param>
        /// <param name="phasesInPi">The phases, φ, of the component sinusoids in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].</param>
        protected MultiSinusoidalDataGenerator(
            TimeSpan sessionBeginTime,
            TimeSpan sessionEndTime,
            DateTime startDate,
            TimeGranularity timeGranularity,
            BusinessDayCalendar businessDayCalendar,
            int waveformSamples,
            int offsetSamples,
            int repetitionsCount,
            double noiseAmplitudeFraction,
            IRandomGenerator noiseRandomGenerator,
            double[] periods,
            double[] amplitudes,
            double sampleMinimum,
            double[] phasesInPi)
            : base(
                sessionBeginTime,
                sessionEndTime,
                startDate,
                timeGranularity,
                businessDayCalendar,
                waveformSamples,
                offsetSamples,
                repetitionsCount,
                noiseAmplitudeFraction,
                noiseRandomGenerator)
        {
            Count = periods.Length;
            if (Count < 1)
                throw new ArgumentException("The length of the input array must be positive.", nameof(periods));
            if (amplitudes.Length != Count || phasesInPi.Length != Count)
                throw new ArgumentException("All input arrays must have the same length.");
            this.periods = periods;
            this.amplitudes = amplitudes;
            this.phasesInPi = phasesInPi;
            SampleMinimum = sampleMinimum;
            sampleMaximum = sampleMinimum + MaximalAmplitude(amplitudes);
            angles = new double[Count];
            frequencies = new double[Count];
            phases = new double[Count];
            Initialize();
        }

        private static double MaximalAmplitude(IEnumerable<double> amplitudes)
        {
            return amplitudes.Concat(new double[] { 0 }).Max();
        }

        private void Initialize()
        {
            const double delta = 0.00005;
            Moniker = string.Empty;
            for (int i = 0; i < Count; ++i)
            {
                angles[i] = 0;
                frequencies[i] = Constants.TwoPi / periods[i];
                phases[i] = Constants.Pi * phasesInPi[i];
                if (i > 0)
                    Moniker += " + ";
                if (Math.Abs(phasesInPi[i]) < delta)
                {
                    Moniker += string.Format(
                        CultureInfo.InvariantCulture,
                        "{0:0.####}∙cos(2π∙t/{1:0.####})",
                        amplitudes[i],
                        periods[i]);
                }
                else
                {
                    Moniker += string.Format(
                        CultureInfo.InvariantCulture,
                        "{0:0.####}∙cos(2π∙t/{1:0.####} + {2:0.####}∙π)",
                        amplitudes[i],
                        periods[i],
                        phasesInPi[i]);
                }
            }

            Moniker += string.Format(CultureInfo.InvariantCulture, " + {0:0.####}", SampleMinimum);

            if (HasNoise && NoiseAmplitudeFraction > delta)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0} + noise(ρn={1:0.####})", Moniker, NoiseAmplitudeFraction);

            if (OffsetSamples > 0)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, off={1}", Moniker, OffsetSamples);

            if (!IsRepetitionsInfinite)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, rep={1}", Moniker, RepetitionsCount);
        }

        /// <inheritdoc />
        protected override double OutOfWaveformSample()
        {
            return SampleMinimum;
        }

        /// <inheritdoc />
        protected override double NextSample()
        {
            double sample = sampleMaximum;
            for (int i = 0; i < Count; ++i)
            {
                double angle = angles[i];
                sample += amplitudes[i] * Math.Cos(angle + phases[i]);
                angle += frequencies[i];
                if (angle > Constants.TwoPi)
                    angle -= Constants.TwoPi;
                angles[i] = angle;
            }

            return sample;
        }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            for (int i = 0; i < Count; ++i)
                angles[i] = 0;
        }
    }
}
