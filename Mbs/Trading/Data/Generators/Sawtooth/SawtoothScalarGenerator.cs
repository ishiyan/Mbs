using System;
using Mbs.Numerics.RandomGenerators;
using Mbs.Trading.Time;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Data.Generators.Sawtooth
{
    /// <summary>
    /// The sawtooth impulse waveform <see cref="Scalar"/> generator produces scalars with the following traits.
    /// <para/>➊ The values of the scalars form a sawtooth impulses defined by the given amplitude and the length of the tooth.
    /// <para/>➋ The sawtooth impulse is shifted upwards so it has minimal values at the specified minimal value level.
    /// <para/>➌ An optional noise may be added to the values.
    /// </summary>
    public sealed class SawtoothScalarGenerator : SawtoothDataGenerator<Scalar>
    {
        internal const string WaveformName = "Sawtooth scalar waveform";

        /// <summary>
        /// Initializes a new instance of the <see cref="SawtoothScalarGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters.</param>
        public SawtoothScalarGenerator(SawtoothScalarGeneratorParameters parameters)
            : base(parameters.TimeParameters, parameters.WaveformParameters, parameters.SawtoothParameters)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SawtoothScalarGenerator"/> class.
        /// </summary>
        /// <param name="sessionBeginTime">The time of the beginning of the trading session.</param>
        /// <param name="sessionEndTime">The end time of the trading session.</param>
        /// <param name="startDate">The date of the first data sample.</param>
        /// <param name="timeGranularity">The time granularity of data samples.</param>
        /// <param name="businessDayCalendar">The value specifying an exchange holiday schedule or a general country holiday schedule.</param>
        /// <param name="waveformSamples">The number of samples in the waveform, should be ≥ 2.</param>
        /// <param name="offsetSamples">The number of samples before the very first waveform. The value of zero means the waveform starts immediately.</param>
        /// <param name="repetitionsCount">The number of repetitions of the waveform. The value of zero means infinite.</param>
        /// <param name="noiseAmplitudeFraction">The amplitude of the noise as a fraction of the mid price.</param>
        /// <param name="noiseRandomGenerator">The uniformly distributed random generator to produce the noise.</param>
        /// <param name="sampleAmplitude">The amplitude of the sawtooth impulse in sample units, should be positive.</param>
        /// <param name="sampleMinimum">The sample value corresponding to the sawtooth impulse, should be positive.</param>
        /// <param name="sawtoothShape">The sawtooth shape.</param>
        /// <param name="isBiDirectional">If true, the sawtooth is reflected horizontally to form a triangle shape.</param>
        public SawtoothScalarGenerator(
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
            double sampleAmplitude = DefaultParameterValues.SawtoothAmplitude,
            double sampleMinimum = DefaultParameterValues.SawtoothMinimalValue,
            SawtoothShape sawtoothShape = DefaultParameterValues.SawtoothShape,
            bool isBiDirectional = DefaultParameterValues.SawtoothIsBiDirectional)
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
                sampleAmplitude,
                sampleMinimum,
                sawtoothShape,
                isBiDirectional)
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
