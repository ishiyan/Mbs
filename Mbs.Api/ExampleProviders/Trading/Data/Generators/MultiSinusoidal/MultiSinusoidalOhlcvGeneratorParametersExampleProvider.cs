using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.MultiSinusoidal;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.MultiSinusoidal
{
    /// <inheritdoc />
    internal class MultiSinusoidalOhlcvGeneratorParametersExampleProvider : IExamplesProvider<MultiSinusoidalOhlcvGeneratorParameters>
    {
        /// <inheritdoc />
        public MultiSinusoidalOhlcvGeneratorParameters GetExamples()
        {
            return new MultiSinusoidalOhlcvGeneratorParameters
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
                MultiSinusoidalParameters = new MultiSinusoidalParameters
                {
                    MinimalValue = DefaultParameterValues.MultiSinusoidalMinimalValue,
                    MultiSinusoidalComponents = new[]
                    {
                        new MultiSinusoidalComponentParameters
                        {
                            Amplitude = DefaultParameterValues.MultiSinusoidalAmplitude,
                            Period = DefaultParameterValues.MultiSinusoidalPeriod,
                            PhaseInPi = DefaultParameterValues.MultiSinusoidalPhaseInPi
                        }
                    }
                },
                OhlcvParameters = new OhlcvParameters
                {
                    CandlestickShadowFraction = DefaultParameterValues.CandlestickShadowFraction,
                    CandlestickBodyFraction = DefaultParameterValues.CandlestickBodyFraction,
                    Volume = DefaultParameterValues.CandlestickVolume
                }
            };
        }
    }
}
