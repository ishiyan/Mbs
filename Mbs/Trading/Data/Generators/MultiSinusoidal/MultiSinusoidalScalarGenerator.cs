using System;
using Mbs.Numerics.RandomGenerators;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Time;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Data.Generators.MultiSinusoidal
{
    /// <summary>
    /// The multi-sinusoidal <see cref="Scalar"/> waveform generator produces scalars with the following traits.
    /// <para/>➊ The values of the scalars form a sum of sinusoids defined by the given periods, amplitudes and phases.
    /// <para/>➋ The sum of sinusoids is shifted upwards so it has minimal values at the specified minimal value level.
    /// <para/>➌ An optional noise may be added to the values.
    /// </summary>
    public sealed class MultiSinusoidalScalarGenerator : MultiSinusoidalDataGenerator<Scalar>
    {
        internal const string WaveformName = "Multi-sinusoidal scalar waveform";

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSinusoidalScalarGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters.</param>
        public MultiSinusoidalScalarGenerator(MultiSinusoidalScalarGeneratorParameters parameters)
            : base(parameters.TimeParameters, parameters.WaveformParameters, parameters.MultiSinusoidalParameters)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSinusoidalScalarGenerator"/> class.
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
        /// <param name="periods">The array of periods of the component sinusoids in samples, should be ≥ 2.</param>
        /// <param name="amplitudes">The array of amplitudes of the component sinusoids in price units, should be positive.</param>
        /// <param name="sampleMinimum">The value corresponding to the minimum of the sum of component sinusoids, should be positive.</param>
        /// <param name="phasesInPi">The phases, φ, of the component sinusoids in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].</param>
        public MultiSinusoidalScalarGenerator(
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
            double[] periods = null,
            double[] amplitudes = null,
            double sampleMinimum = DefaultParameterValues.MultiSinusoidalMinimalValue,
            double[] phasesInPi = null)
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
                periods,
                amplitudes,
                sampleMinimum,
                phasesInPi)
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
