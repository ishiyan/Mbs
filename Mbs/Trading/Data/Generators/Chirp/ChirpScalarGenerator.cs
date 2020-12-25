using System;
using Mbs.Numerics.RandomGenerators;
using Mbs.Trading.Time;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Data.Generators.Chirp
{
    /// <summary>
    /// The chirp <see cref="Scalar"/> waveform (a period-swept cosine wave) generator produces scalars with the following traits.
    /// <para/>➊ The values of the scalars form a chirp defined by the given begin/end periods, amplitude and phase.
    /// <para/>➋ The chirp is shifted upwards so it has minimal values at the specified minimal value level.
    /// <para/>➌ An optional noise may be added to the values.
    /// </summary>
    public sealed class ChirpScalarGenerator : ChirpDataGenerator<Scalar>
    {
        internal const string WaveformName = "Chirp scalar waveform";

        /// <summary>
        /// Initializes a new instance of the <see cref="ChirpScalarGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters.</param>
        public ChirpScalarGenerator(ChirpScalarGeneratorParameters parameters)
            : base(parameters.TimeParameters, parameters.WaveformParameters, parameters.ChirpParameters)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChirpScalarGenerator"/> class.
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
        /// <param name="sampleAmplitude">The amplitude of the chirp in sample units, should be positive.</param>
        /// <param name="sampleMinimum">The sample value corresponding to the chirp, should be positive.</param>
        /// <param name="initialPeriod">The instantaneous initial period of the chirp in samples, should be ≥ 2.</param>
        /// <param name="finalPeriod">The instantaneous final period of the chirp in samples, should be ≥ 2.</param>
        /// <param name="chirpSweep">The chirp sweep.</param>
        /// <param name="phaseInPi">The initial phase, φ, of the chirp in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].</param>
        /// <param name="isBiDirectional">If true, the period (or frequency) of even chirps descends from the final period (or frequency) to the initial one, to form a symmetrical shape with odd chirps.</param>
        public ChirpScalarGenerator(
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
            double sampleAmplitude = DefaultParameterValues.ChirpAmplitude,
            double sampleMinimum = DefaultParameterValues.ChirpMinimalValue,
            double initialPeriod = DefaultParameterValues.ChirpInitialPeriod,
            double finalPeriod = DefaultParameterValues.ChirpFinalPeriod,
            ChirpSweep chirpSweep = DefaultParameterValues.ChirpSweep,
            double phaseInPi = DefaultParameterValues.ChirpPhaseInPi,
            bool isBiDirectional = DefaultParameterValues.ChirpIsBiDirectional)
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
                initialPeriod,
                finalPeriod,
                chirpSweep,
                phaseInPi,
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
