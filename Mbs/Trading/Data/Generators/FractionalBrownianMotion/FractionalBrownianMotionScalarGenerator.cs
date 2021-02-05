using System;
using Mbs.Numerics.RandomGenerators;
using Mbs.Numerics.RandomGenerators.FractionalBrownianMotion;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Time;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Data.Generators.FractionalBrownianMotion
{
    /// <summary>
    /// The fractional Brownian motion <see cref="Scalar"/> generator produces scalars with the following traits.
    /// <para/>➊ The values of the scalars form a fractional Brownian motion defined by the Hurst exponent, amplitude and range.
    /// <para/>➋ The fractional Brownian motion is shifted upwards so it has minimal values at the specified minimal value level.
    /// <para/>➌ An optional noise may be added to the values.
    /// </summary>
    public sealed class FractionalBrownianMotionScalarGenerator : FractionalBrownianMotionDataGenerator<Scalar>
    {
        internal const string WaveformName = "Fractional Brownian motion scalar waveform";

        /// <summary>
        /// Initializes a new instance of the <see cref="FractionalBrownianMotionScalarGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters.</param>
        public FractionalBrownianMotionScalarGenerator(FractionalBrownianMotionScalarGeneratorParameters parameters)
            : base(parameters.TimeParameters, parameters.WaveformParameters, parameters.FbmParameters)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FractionalBrownianMotionScalarGenerator"/> class.
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
        /// <param name="algorithm">The fractional Brownian motion algorithm.</param>
        /// <param name="normalRandomGenerator">The normal random generator.</param>
        /// <param name="sampleAmplitude">The amplitude of the fractional Brownian motion in sample units, should be positive.</param>
        /// <param name="sampleMinimum">The sample value corresponding to the minimum of the fractional Brownian motion, should be positive.</param>
        /// <param name="hurstExponent">The value of the Hurst exponent of the fractional Brownian motion; H∈[0, 1].</param>
        public FractionalBrownianMotionScalarGenerator(
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
            FractionalBrownianMotionAlgorithm algorithm = DefaultParameterValues.FbmAlgorithm,
            INormalRandomGenerator normalRandomGenerator = null,
            double sampleAmplitude = DefaultParameterValues.FbmAmplitude,
            double sampleMinimum = DefaultParameterValues.FbmMinimalValue,
            double hurstExponent = DefaultParameterValues.FbmHurstExponent)
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
                algorithm,
                normalRandomGenerator,
                sampleAmplitude,
                sampleMinimum,
                hurstExponent)
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
