using System;
using System.Globalization;
using Mbs.Numerics;
using Mbs.Numerics.RandomGenerators;
using Mbs.Trading.Time;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Data.Generators.Sinusoidal
{
    /// <summary>
    /// The sinusoidal waveform generator produces samples which form a
    /// sinusoid defined by the given period, amplitude and phase.
    /// An optional noise may be added to the samples.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public abstract class SinusoidalDataGenerator<T> : WaveformDataGenerator<T>
        where T : TemporalEntity, new()
    {
        private readonly double frequency;
        private readonly double summand;
        private readonly double phase;
        private double angle;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinusoidalDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="timeParameters">The time-related input parameters for the <see cref="TemporalDataGenerator{T}"/>.</param>
        /// <param name="waveformParameters">The input parameters for the <see cref="WaveformDataGenerator{T}"/>.</param>
        /// <param name="sinusoidalParameters">The input parameters for the <see cref="SinusoidalDataGenerator{T}"/>.</param>
        protected SinusoidalDataGenerator(
            TimeParameters timeParameters,
            WaveformParameters waveformParameters,
            SinusoidalParameters sinusoidalParameters)
            : base(timeParameters, waveformParameters)
        {
            SampleAmplitude = sinusoidalParameters.Amplitude;
            SampleMinimum = sinusoidalParameters.MinimalValue;
            summand = sinusoidalParameters.MinimalValue + sinusoidalParameters.Amplitude;
            Period = sinusoidalParameters.Period;
            frequency = Constants.TwoPi / sinusoidalParameters.Period;
            PhaseInPi = sinusoidalParameters.PhaseInPi;
            phase = Constants.Pi * sinusoidalParameters.PhaseInPi;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SinusoidalDataGenerator{T}"/> class.
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
        /// <param name="sampleAmplitude">The amplitude of the sinusoid in price units, should be positive.</param>
        /// <param name="sampleMinimum">The price corresponding to the minimum of the sinusoid, should be positive.</param>
        /// <param name="period">The period of the sinusoid in samples, should be ≥ 2.</param>
        /// <param name="phaseInPi">The phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].</param>
        protected SinusoidalDataGenerator(
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
            double sampleAmplitude,
            double sampleMinimum,
            double period,
            double phaseInPi)
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
            SampleAmplitude = sampleAmplitude;
            SampleMinimum = sampleMinimum;
            summand = sampleMinimum + sampleAmplitude;
            Period = period;
            frequency = Constants.TwoPi / period;
            PhaseInPi = phaseInPi;
            phase = Constants.Pi * phaseInPi;
            Initialize();
        }

        /// <summary>
        /// Gets the amplitude of the sinusoid in sample units, should be positive.
        /// </summary>
        public double SampleAmplitude { get; }

        /// <summary>
        /// Gets the sample value corresponding to the minimum of the sinusoid, should be positive.
        /// </summary>
        public double SampleMinimum { get; }

        /// <summary>
        /// Gets the period of the sinusoid in samples, should be ≥ 2.
        /// </summary>
        public double Period { get; }

        /// <summary>
        /// Gets the phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </summary>
        public double PhaseInPi { get; }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            angle = 0;
        }

        /// <inheritdoc />
        protected override double OutOfWaveformSample()
        {
            return SampleMinimum;
        }

        /// <inheritdoc />
        protected override double NextSample()
        {
            double sample = summand + SampleAmplitude * Math.Cos(angle + phase);
            angle += frequency;
            if (angle > Constants.TwoPi)
            {
                angle -= Constants.TwoPi;
            }

            return sample;
        }

        private void Initialize()
        {
            const double delta = 0.00005;
            Moniker = Math.Abs(PhaseInPi) < delta
                ? string.Format(
                    CultureInfo.InvariantCulture,
                    "{0:0.####}∙cos(2π∙t/{1:0.####}) + {2:0.####}",
                    SampleAmplitude,
                    Period,
                    SampleMinimum)
                : string.Format(
                    CultureInfo.InvariantCulture,
                    "{0:0.####}∙cos(2π∙t/{1:0.####} + {2:0.####}∙π) + {3:0.####}",
                    SampleAmplitude,
                    Period,
                    PhaseInPi,
                    SampleMinimum);

            if (HasNoise && NoiseAmplitudeFraction > delta)
            {
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0} + noise(ρn={1:0.####})", Moniker, NoiseAmplitudeFraction);
            }

            if (OffsetSamples > 0)
            {
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, off={1}", Moniker, OffsetSamples);
            }

            if (!IsRepetitionsInfinite)
            {
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, rep={1}", Moniker, RepetitionsCount);
            }
        }
    }
}
