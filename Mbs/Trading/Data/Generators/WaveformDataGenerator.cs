using System;
using Mbs.Numerics.Random;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// A waveform generator produces samples with an optional noise.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public abstract class WaveformDataGenerator<T> : TemporalDataGenerator<T>
        where T : TemporalEntity, new()
    {
        /// <summary>
        /// Gets the amplitude of the noise as a fraction of the signal.
        /// </summary>
        public double NoiseAmplitudeFraction { get; }

        /// <summary>
        /// Gets a value indicating whether if generator mixes the noise with the signal.
        /// </summary>
        public bool HasNoise { get; }

        /// <summary>
        /// Gets the number of samples before the very first waveform. The value of zero means the waveform starts immediately.
        /// </summary>
        public int OffsetSamples { get; }

        /// <summary>
        /// Gets the number of repetitions of the waveform. The value of zero means infinite.
        /// </summary>
        public int RepetitionsCount { get; }

        /// <summary>
        /// Gets a value indicating whether the number of repetitions of the waveform is infinite.
        /// </summary>
        public bool IsRepetitionsInfinite { get; }

        private readonly IRandomGenerator noiseRandomGenerator;
        private int currentRepetition;

        /// <summary>
        /// Gets the number of samples in the waveform.
        /// </summary>
        protected int WaveformSamples { get; }

        /// <summary>
        /// Gets the current sample number within the waveform, [1, waveform length].
        /// </summary>
        protected int CurrentSampleIndex { get; private set; }

        /// <summary>
        /// Gets the current sample value.
        /// </summary>
        protected double CurrentSampleValue { get; private set; } = double.NaN;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveformDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="timeParameters">The time-related input parameters for the <see cref="TemporalDataGenerator{T}"/>.</param>
        /// <param name="waveformParameters">The input parameters for the <see cref="WaveformDataGenerator{T}"/>.</param>
        protected WaveformDataGenerator(TimeParameters timeParameters, WaveformParameters waveformParameters)
            : base(timeParameters)
        {
            WaveformSamples = waveformParameters.WaveformSamples;
            OffsetSamples = waveformParameters.OffsetSamples;
            CurrentSampleIndex = -OffsetSamples;

            RepetitionsCount = waveformParameters.RepetitionsCount;
            IsRepetitionsInfinite = RepetitionsCount < 1;

            NoiseAmplitudeFraction = waveformParameters.NoiseAmplitudeFraction;
            if (NoiseAmplitudeFraction > double.Epsilon)
            {
                HasNoise = true;
                noiseRandomGenerator =
                    waveformParameters.NoiseUniformRandomGeneratorKind.RandomGenerator(
                        waveformParameters.NoiseUniformRandomGeneratorSeed);
            }
            else
            {
                HasNoise = false;
                noiseRandomGenerator = null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveformDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="sessionBeginTime">The time of the beginning of the trading session.</param>
        /// <param name="sessionEndTime">The end time of the trading session.</param>
        /// <param name="startDate">The date of the first data sample.</param>
        /// <param name="timeGranularity">The time granularity of data samples.</param>
        /// <param name="businessDayCalendar">The value specifying an exchange holiday schedule or a general country holiday schedule.</param>
        /// <param name="waveformSamples">The number of samples in the waveform.</param>
        /// <param name="offsetSamples">The number of samples before the very first waveform. The value of zero means the waveform starts immediately.</param>
        /// <param name="repetitionsCount">The number of repetitions of the waveform. The value of zero means infinite.</param>
        /// <param name="noiseAmplitudeFraction">The amplitude of the noise as a fraction of the mid price.</param>
        /// <param name="noiseRandomGenerator">The uniformly distributed random generator to produce the noise.</param>
        protected WaveformDataGenerator(
            TimeSpan sessionBeginTime,
            TimeSpan sessionEndTime,
            DateTime startDate,
            TimeGranularity timeGranularity,
            BusinessDayCalendar businessDayCalendar,
            int waveformSamples,
            int offsetSamples,
            int repetitionsCount,
            double noiseAmplitudeFraction,
            IRandomGenerator noiseRandomGenerator)
            : base(sessionBeginTime, sessionEndTime, startDate, timeGranularity, businessDayCalendar)
        {
            WaveformSamples = waveformSamples;
            if (offsetSamples < 0)
                offsetSamples = 0;
            OffsetSamples = offsetSamples;
            CurrentSampleIndex = -offsetSamples;

            if (repetitionsCount < 0)
                repetitionsCount = 0;
            RepetitionsCount = repetitionsCount;
            IsRepetitionsInfinite = repetitionsCount < 1;

            if (noiseAmplitudeFraction < 0)
                noiseAmplitudeFraction = -noiseAmplitudeFraction;
            NoiseAmplitudeFraction = noiseAmplitudeFraction;
            this.noiseRandomGenerator = noiseRandomGenerator;
            HasNoise = null != noiseRandomGenerator && noiseAmplitudeFraction > double.Epsilon;
        }

        /// <summary>
        /// Adds a noise to a given sample.
        /// </summary>
        /// <param name="sample">The sample.</param>
        /// <returns>The sample with an added noise.</returns>
        private double AddNoise(double sample)
        {
            if (HasNoise)
            {
                double noiseAmplitude = sample * NoiseAmplitudeFraction;
                sample += noiseRandomGenerator.NextDouble(-noiseAmplitude, noiseAmplitude);
            }

            return sample;
        }

        /// <summary>
        /// Generates a new sample.
        /// </summary>
        /// <returns>A new generated sample.</returns>
        protected abstract double NextSample();

        /// <summary>
        /// Generates samples before the first waveform sample and after the last waveform sample.
        /// </summary>
        /// <returns>A new generated sample.</returns>
        protected abstract double OutOfWaveformSample();

        /// <inheritdoc />
        public override T GenerateNext()
        {
            CurrentSampleValue = NextValue();
            return new T { Time = NextTime() };
        }

        private double NextValue()
        {
            if (CurrentSampleIndex < 0)
            {
                ++CurrentSampleIndex;
                return AddNoise(OutOfWaveformSample());
            }

            if (!IsRepetitionsInfinite)
            {
                if (RepetitionsCount <= currentRepetition)
                    return AddNoise(OutOfWaveformSample());
                ++CurrentSampleIndex;
                double sample = NextSample();
                if (CurrentSampleIndex == WaveformSamples)
                {
                    CurrentSampleIndex = 0;
                    ++currentRepetition;
                }

                return AddNoise(sample);
            }
            else
            {
                ++CurrentSampleIndex;
                double sample = NextSample();
                if (CurrentSampleIndex == WaveformSamples)
                    CurrentSampleIndex = 0;
                return AddNoise(sample);
            }
        }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            CurrentSampleValue = double.NaN;
            CurrentSampleIndex = -OffsetSamples;
            currentRepetition = 0;
            if (HasNoise && noiseRandomGenerator.CanReset)
                noiseRandomGenerator.Reset();
        }
    }
}
