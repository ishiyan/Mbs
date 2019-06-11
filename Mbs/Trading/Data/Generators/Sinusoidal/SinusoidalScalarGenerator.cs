using System;
using Mbs.Numerics.Random;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators.Sinusoidal
{
    /// <summary>
    /// The sinusoidal <see cref="Scalar"/> waveform generator produces scalars with the following traits.
    /// <para/>➊ The values of the scalars form a sinusoid defined by the given period, amplitude and phase.
    /// <para/>➋ The sinusoid is shifted upwards so it has minimal values at the specified minimal value level.
    /// <para/>➌ An optional noise may be added to the values.
    /// </summary>
    public sealed class SinusoidalScalarGenerator : SinusoidalDataGenerator<Scalar>
    {
        internal const string WaveformName = "Sinusoidal scalar waveform";

        /// <summary>
        /// Initializes a new instance of the <see cref="SinusoidalScalarGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters.</param>
        public SinusoidalScalarGenerator(SinusoidalScalarGeneratorParameters parameters)
            : base(parameters.TimeParameters, parameters.WaveformParameters, parameters.SinusoidalParameters)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SinusoidalScalarGenerator"/> class.
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
        /// <param name="sampleAmplitude">The amplitude of the sinusoid in price units, should be positive.</param>
        /// <param name="sampleMinimum">The price corresponding to the minimum of the sinusoid, should be positive.</param>
        /// <param name="period">The period of the sinusoid in samples, should be ≥ 2.</param>
        /// <param name="phaseInPi">The phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].</param>
        public SinusoidalScalarGenerator(
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
            double sampleAmplitude = DefaultParameterValues.SinusoidalAmplitude,
            double sampleMinimum = DefaultParameterValues.SinusoidalMinimalValue,
            double period = DefaultParameterValues.SinusoidalPeriod,
            double phaseInPi = DefaultParameterValues.SinusoidalPhaseInPi)
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
                period,
                phaseInPi)
        {
            Initialize();
        }

        private void Initialize()
        {
            Name = WaveformName;
        }

        /// <inheritdoc />
        public override Scalar GenerateNext()
        {
            Scalar scalar = base.GenerateNext();
            scalar.Value = CurrentSampleValue;
            return scalar;
        }
    }
}
