using System;
using Mbs.Numerics.RandomGenerators;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Time;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Data.Generators.GeometricBrownianMotion
{
    /// <summary>
    /// The geometric Brownian motion <see cref="Scalar"/> generator produces scalars with the following traits.
    /// <para/>➊ The values of the scalars form a geometric Brownian motion defined by the drift, volatility, amplitude and range.
    /// <para/>➋ The geometric Brownian motion is shifted upwards so it has minimal values at the specified minimal value level.
    /// <para/>➌ An optional noise may be added to the values.
    /// </summary>
    public sealed class GeometricBrownianMotionScalarGenerator : GeometricBrownianMotionDataGenerator<Scalar>
    {
        internal const string WaveformName = "Geometric Brownian motion scalar waveform";

        /// <summary>
        /// Initializes a new instance of the <see cref="GeometricBrownianMotionScalarGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters.</param>
        public GeometricBrownianMotionScalarGenerator(GeometricBrownianMotionScalarGeneratorParameters parameters)
            : base(parameters.TimeParameters, parameters.WaveformParameters, parameters.GbmParameters)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeometricBrownianMotionScalarGenerator"/> class.
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
        /// <param name="normalRandomGenerator">The normal random generator.</param>
        /// <param name="sampleAmplitude">The amplitude of the geometric Brownian motion in sample units, should be positive.</param>
        /// <param name="sampleMinimum">The sample value corresponding to the minimum of the geometric Brownian motion, should be positive.</param>
        /// <param name="drift">The value of the drift, μ, of the geometric Brownian motion.</param>
        /// <param name="volatility">The value of the volatility, σ, of the geometric Brownian motion.</param>
        public GeometricBrownianMotionScalarGenerator(
            TimeSpan sessionBeginTime,
            TimeSpan sessionEndTime,
            DateTime startDate,
            TimeGranularity timeGranularity = DefaultParameterValues.TimeGranularity,
            BusinessDayCalendar businessDayCalendar = DefaultParameterValues.BusinessDayCalendar,
            int waveformSamples = DefaultParameterValues.WaveformSamples,
            int offsetSamples = DefaultParameterValues.OffsetSamples,
            int repetitionsCount = DefaultParameterValues.RepetitionsCount,
            double noiseAmplitudeFraction = DefaultParameterValues.NoiseAmplitudeFraction,
            IRandomGenerator noiseRandomGenerator = null,
            INormalRandomGenerator normalRandomGenerator = null,
            double sampleAmplitude = DefaultParameterValues.GbmAmplitude,
            double sampleMinimum = DefaultParameterValues.GbmMinimalValue,
            double drift = DefaultParameterValues.GbmDrift,
            double volatility = DefaultParameterValues.GbmVolatility)
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
                noiseRandomGenerator,
                normalRandomGenerator,
                sampleAmplitude,
                sampleMinimum,
                drift,
                volatility)
        {
            Initialize();
        }

        /// <inheritdoc />
        public override Scalar GenerateNext()
        {
            Scalar scalar = base.GenerateNext();
            scalar.Value = CurrentSampleValue;
            return scalar;
        }

        private void Initialize()
        {
            Name = WaveformName;
        }
    }
}
