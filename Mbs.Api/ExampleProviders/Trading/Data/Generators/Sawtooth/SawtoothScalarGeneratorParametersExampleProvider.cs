using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sawtooth
{
    /// <inheritdoc />
    internal class SawtoothScalarGeneratorParametersExampleProvider : IExamplesProvider<SawtoothScalarGeneratorParameters>
    {
        /// <inheritdoc />
        public SawtoothScalarGeneratorParameters GetExamples()
        {
            return new SawtoothScalarGeneratorParameters
            {
                SampleCount = DefaultParameterValues.SamplesCount,
                TimeParameters = new TimeParameters
                {
                    SessionStartTime = DefaultParameterValues.SessionStartTime,
                    SessionEndTime = DefaultParameterValues.SessionEndTime,
                    StartDate = DefaultParameterValues.StartDate,
                    TimeGranularity = DefaultParameterValues.TimeGranularity,
                    BusinessDayCalendar = DefaultParameterValues.BusinessDayCalendar
                },
                WaveformParameters = new WaveformParameters
                {
                    WaveformSamples = DefaultParameterValues.WaveformSamples,
                    OffsetSamples = DefaultParameterValues.OffsetSamples,
                    RepetitionsCount = DefaultParameterValues.RepetitionsCount,
                    NoiseAmplitudeFraction = DefaultParameterValues.NoiseAmplitudeFraction,
                    NoiseUniformRandomGeneratorKind = DefaultParameterValues.NoiseUniformRandomGeneratorKind,
                    NoiseUniformRandomGeneratorSeed = DefaultParameterValues.NoiseUniformRandomGeneratorSeed
                },
                SawtoothParameters = new SawtoothParameters
                {
                    Amplitude = DefaultParameterValues.SawtoothAmplitude,
                    MinimalValue = DefaultParameterValues.SawtoothMinimalValue,
                    IsBiDirectional = DefaultParameterValues.SawtoothIsBiDirectional,
                    Shape = DefaultParameterValues.SawtoothShape
                }
            };
        }
    }
}
