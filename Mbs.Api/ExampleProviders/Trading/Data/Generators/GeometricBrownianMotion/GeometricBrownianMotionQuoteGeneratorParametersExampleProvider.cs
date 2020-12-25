using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion
{
    /// <inheritdoc />
    internal class GeometricBrownianMotionQuoteGeneratorParametersExampleProvider : IExamplesProvider<GeometricBrownianMotionQuoteGeneratorParameters>
    {
        /// <inheritdoc />
        public GeometricBrownianMotionQuoteGeneratorParameters GetExamples()
        {
            return new GeometricBrownianMotionQuoteGeneratorParameters
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
                GbmParameters = new GeometricBrownianMotionParameters
                {
                    Amplitude = DefaultParameterValues.GbmAmplitude,
                    MinimalValue = DefaultParameterValues.GbmMinimalValue,
                    Drift = DefaultParameterValues.GbmDrift,
                    Volatility = DefaultParameterValues.GbmVolatility,
                    NormalRandomGeneratorKind = DefaultParameterValues.GbmNormalRandomGeneratorKind,
                    AssociatedUniformRandomGeneratorKind = DefaultParameterValues.GbmAssociatedUniformRandomGeneratorKind,
                    Seed = DefaultParameterValues.GbmSeed,
                },
                QuoteParameters = new QuoteParameters
                {
                    SpreadFraction = DefaultParameterValues.SpreadFraction,
                    AskSize = DefaultParameterValues.AskSize,
                    BidSize = DefaultParameterValues.BidSize,
                },
            };
        }
    }
}
