using System;
using System.Globalization;
using Mbs.Numerics.RandomGenerators;
using Mbs.Trading.Time;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Data.Generators.Sawtooth
{
    /// <summary>
    /// The sawtooth impulse waveform generator produces samples which form
    /// sawtooth impulse defined by the given amplitude and the length of the impulse.
    /// An optional noise may be added to the samples.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public abstract class SawtoothDataGenerator<T> : WaveformDataGenerator<T>
        where T : TemporalEntity, new()
    {
        private readonly double lengthMinusOne;
        private readonly double ratio;
        private readonly double sampleMaximum;
        private bool directionUp = true;
        private double currentValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="SawtoothDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="timeParameters">The time-related input parameters for the <see cref="TemporalDataGenerator{T}"/>.</param>
        /// <param name="waveformParameters">The input parameters for the <see cref="WaveformDataGenerator{T}"/>.</param>
        /// <param name="sawtoothParameters">The input parameters for the <see cref="SawtoothDataGenerator{T}"/>.</param>
        protected SawtoothDataGenerator(
            TimeParameters timeParameters,
            WaveformParameters waveformParameters,
            SawtoothParameters sawtoothParameters)
            : base(timeParameters, waveformParameters)
        {
            SampleAmplitude = sawtoothParameters.Amplitude;
            SampleMinimum = sawtoothParameters.MinimalValue;
            sampleMaximum = sawtoothParameters.MinimalValue + sawtoothParameters.Amplitude;
            IsBiDirectional = sawtoothParameters.IsBiDirectional;
            Shape = sawtoothParameters.Shape;
            lengthMinusOne = waveformParameters.WaveformSamples - 1;
            ratio = CalculateRatio(sawtoothParameters.Shape);
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SawtoothDataGenerator{T}"/> class.
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
        /// <param name="sampleAmplitude">The amplitude of the sawtooth impulse in sample units, should be positive.</param>
        /// <param name="sampleMinimum">The sample value corresponding to the minimum of the sawtooth impulse, should be positive.</param>
        /// <param name="sawtoothShape">The sawtooth shape.</param>
        /// <param name="isBiDirectional">If true, the sawtooth is reflected horizontally to form a triangle shape.</param>
        protected SawtoothDataGenerator(
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
            SawtoothShape sawtoothShape,
            bool isBiDirectional)
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
            IsBiDirectional = isBiDirectional;
            Shape = sawtoothShape;
            lengthMinusOne = waveformSamples - 1;
            ratio = CalculateRatio(sawtoothShape);
            Initialize();
        }

        /// <summary>
        /// Gets the amplitude of the sawtooth impulse in sample units, should be positive.
        /// </summary>
        public double SampleAmplitude { get; }

        /// <summary>
        /// Gets the sample value corresponding to the minimum of the sawtooth impulse, should be positive.
        /// </summary>
        public double SampleMinimum { get; }

        /// <summary>
        /// Gets a value indicating whether the sawtooth is reflected horizontally to form a triangle shape.
        /// </summary>
        public bool IsBiDirectional { get; }

        /// <summary>
        /// Gets the sawtooth shape.
        /// </summary>
        public SawtoothShape Shape { get; }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            directionUp = true;
            currentValue = SampleMinimum;
        }

        /// <inheritdoc />
        protected override double OutOfWaveformSample()
        {
            return SampleMinimum;
        }

        /// <inheritdoc />
        protected override double NextSample()
        {
            if (IsBiDirectional)
            {
                currentValue = directionUp ? NextUp() : NextDown();
                if (CurrentSampleIndex == WaveformSamples)
                {
                    directionUp = !directionUp;
                }
            }
            else
            {
                currentValue = NextUp();
            }

            return currentValue;
        }

        private static string ShapeName(SawtoothShape sawtoothShape)
        {
            return sawtoothShape switch
            {
                SawtoothShape.Linear => "linear",
                SawtoothShape.Quadratic => "quadratic",
                SawtoothShape.Logarithmic => "logarithmic",
                _ => throw new ArgumentException($"Unknown sawtooth shape ${sawtoothShape}", nameof(sawtoothShape))
            };
        }

        private double CalculateRatio(SawtoothShape sawtoothShape)
        {
            return Shape switch
            {
                SawtoothShape.Linear => SampleAmplitude / lengthMinusOne,
                SawtoothShape.Quadratic => SampleAmplitude / (lengthMinusOne * lengthMinusOne),
                SawtoothShape.Logarithmic => sampleMaximum / SampleMinimum,
                _ => throw new ArgumentException($"Unknown sawtooth shape ${sawtoothShape}", nameof(sawtoothShape))
            };
        }

        private void Initialize()
        {
            currentValue = SampleMinimum;
            Moniker = string.Format(
                CultureInfo.InvariantCulture,
                "{0:0.####}∙sawtooth({1}, {2}{3}) + {4:0.####}",
                SampleAmplitude,
                WaveformSamples,
                ShapeName(Shape),
                IsBiDirectional ? ", bidirectional" : string.Empty,
                SampleMinimum);

            const double delta = 0.00005;
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

        private double NextUp()
        {
            if (CurrentSampleIndex == 1)
            {
                return SampleMinimum;
            }

            switch (Shape)
            {
                case SawtoothShape.Linear:
                    return currentValue + ratio;
                case SawtoothShape.Quadratic:
                    int n = CurrentSampleIndex - 1;
                    return SampleMinimum + ratio * n * n;
                case SawtoothShape.Logarithmic:
                    return SampleMinimum * Math.Pow(ratio, (CurrentSampleIndex - 1) / lengthMinusOne);
            }

            return SampleMinimum;
        }

        private double NextDown()
        {
            if (CurrentSampleIndex == 1)
            {
                return sampleMaximum;
            }

            switch (Shape)
            {
                case SawtoothShape.Linear:
                    return currentValue - ratio;
                case SawtoothShape.Quadratic:
                    int n = WaveformSamples - CurrentSampleIndex + 1;
                    return SampleMinimum + ratio * n * n;
                case SawtoothShape.Logarithmic:
                    return SampleMinimum * Math.Pow(ratio, (WaveformSamples - CurrentSampleIndex + 1) / lengthMinusOne);
            }

            return sampleMaximum;
        }
    }
}
