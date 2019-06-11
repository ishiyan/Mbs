using System;
using System.Globalization;
using Mbs.Numerics.Random;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators.Square
{
    /// <summary>
    /// The square impulse waveform generator produces samples which form
    /// square impulse defined by the given amplitude and the length of the impulse.
    /// An optional noise may be added to the samples.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public abstract class SquareDataGenerator<T> : WaveformDataGenerator<T>
        where T : TemporalEntity, new()
    {
        /// <summary>
        /// Gets the amplitude of the square impulse in sample units, should be positive.
        /// </summary>
        public double SampleAmplitude { get; }

        /// <summary>
        /// Gets the sample value corresponding to the minimum of the square impulse, should be positive.
        /// </summary>
        public double SampleMinimum { get; }

        private readonly double sampleMaximum;
        private bool directionUp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="timeParameters">The time-related input parameters for the <see cref="TemporalDataGenerator{T}"/>.</param>
        /// <param name="waveformParameters">The input parameters for the <see cref="WaveformDataGenerator{T}"/>.</param>
        /// <param name="squareParameters">The input parameters for the <see cref="SquareDataGenerator{T}"/>.</param>
        protected SquareDataGenerator(
            TimeParameters timeParameters,
            WaveformParameters waveformParameters,
            SquareParameters squareParameters)
            : base(timeParameters, waveformParameters)
        {
            SampleAmplitude = squareParameters.Amplitude;
            SampleMinimum = squareParameters.MinimalValue;
            sampleMaximum = squareParameters.MinimalValue + squareParameters.Amplitude;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareDataGenerator{T}"/> class.
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
        /// <param name="sampleAmplitude">The amplitude of the square impulse in sample units, should be positive.</param>
        /// <param name="sampleMinimum">The sample value corresponding to the minimum of the square impulse, should be positive.</param>
        protected SquareDataGenerator(
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
            double sampleMinimum)
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
            sampleMaximum = sampleMinimum + sampleAmplitude;
            Initialize();
        }

        private void Initialize()
        {
            Moniker = string.Format(
                CultureInfo.InvariantCulture,
                "{0:0.####}∙square({1}) + {2:0.####}",
                SampleAmplitude,
                WaveformSamples,
                SampleMinimum);

            const double delta = 0.00005;
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
            double sample = directionUp ? sampleMaximum : SampleMinimum;
            if (CurrentSampleIndex == WaveformSamples)
                directionUp = !directionUp;
            return sample;
        }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            directionUp = true;
        }
    }
}
