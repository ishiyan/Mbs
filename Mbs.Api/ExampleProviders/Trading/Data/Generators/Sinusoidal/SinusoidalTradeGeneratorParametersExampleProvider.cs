using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sinusoidal
{
    /// <inheritdoc />
    internal class SinusoidalTradeGeneratorParametersExampleProvider : IExamplesProvider<SinusoidalTradeGeneratorParameters>
    {
        /// <inheritdoc />
        public SinusoidalTradeGeneratorParameters GetExamples()
        {
            return new SinusoidalTradeGeneratorParameters
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
                SinusoidalParameters = new SinusoidalParameters
                {
                    Amplitude = DefaultParameterValues.SinusoidalAmplitude,
                    MinimalValue = DefaultParameterValues.SinusoidalMinimalValue,
                    Period = DefaultParameterValues.SinusoidalPeriod,
                    PhaseInPi = DefaultParameterValues.SinusoidalPhaseInPi,
                },
                TradeParameters = new TradeParameters
                {
                    Volume = DefaultParameterValues.TradeVolume,
                },
            };
        }
    }
}
