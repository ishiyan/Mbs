using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    /// <inheritdoc />
    internal class FractionalBrownianMotionQuoteGeneratorParametersExampleProvider : IExamplesProvider<FractionalBrownianMotionQuoteGeneratorParameters>
    {
        /// <inheritdoc />
        public FractionalBrownianMotionQuoteGeneratorParameters GetExamples()
        {
            return new FractionalBrownianMotionQuoteGeneratorParameters
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
                FbmParameters = new FractionalBrownianMotionParameters
                {
                    Amplitude = DefaultParameterValues.FbmAmplitude,
                    MinimalValue = DefaultParameterValues.FbmMinimalValue,
                    HurstExponent = DefaultParameterValues.FbmHurstExponent,
                    Algorithm = DefaultParameterValues.FbmAlgorithm,
                    NormalRandomGeneratorKind = DefaultParameterValues.FbmNormalRandomGeneratorKind,
                    AssociatedUniformRandomGeneratorKind = DefaultParameterValues.FbmAssociatedUniformRandomGeneratorKind,
                    Seed = DefaultParameterValues.FbmSeed,
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
