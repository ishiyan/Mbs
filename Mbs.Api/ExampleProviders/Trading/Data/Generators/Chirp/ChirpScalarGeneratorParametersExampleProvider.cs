using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Chirp;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Chirp
{
    /// <inheritdoc />
    internal class ChirpScalarGeneratorParametersExampleProvider : IExamplesProvider<ChirpScalarGeneratorParameters>
    {
        /// <inheritdoc />
        public ChirpScalarGeneratorParameters GetExamples()
        {
            return new ChirpScalarGeneratorParameters
            {
                SampleCount = DefaultParameterValues.SamplesCount,
                TimeParameters = new TimeParameters
                {
                    SessionStartTime = DefaultParameterValues.SessionStartTime,
                    SessionEndTime = DefaultParameterValues.SessionEndTime,
                    StartDate = DefaultParameterValues.StartDate,
                    TimeGranularity = DefaultParameterValues.TimeGranularity,
                    BusinessDayCalendar = DefaultParameterValues.BusinessDayCalendar,
                },
                WaveformParameters = new WaveformParameters
                {
                    WaveformSamples = DefaultParameterValues.WaveformSamples,
                    OffsetSamples = DefaultParameterValues.OffsetSamples,
                    RepetitionsCount = DefaultParameterValues.RepetitionsCount,
                    NoiseAmplitudeFraction = DefaultParameterValues.NoiseAmplitudeFraction,
                    NoiseUniformRandomGeneratorKind = DefaultParameterValues.NoiseUniformRandomGeneratorKind,
                    NoiseUniformRandomGeneratorSeed = DefaultParameterValues.NoiseUniformRandomGeneratorSeed,
                },
                ChirpParameters = new ChirpParameters
                {
                    Amplitude = DefaultParameterValues.ChirpAmplitude,
                    MinimalValue = DefaultParameterValues.ChirpMinimalValue,
                    InitialPeriod = DefaultParameterValues.ChirpInitialPeriod,
                    FinalPeriod = DefaultParameterValues.ChirpFinalPeriod,
                    PhaseInPi = DefaultParameterValues.ChirpPhaseInPi,
                    IsBiDirectional = DefaultParameterValues.ChirpIsBiDirectional,
                    ChirpSweep = DefaultParameterValues.ChirpSweep,
                },
            };
        }
    }
}
