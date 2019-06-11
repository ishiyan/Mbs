using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    /// <inheritdoc />
    internal class FractionalBrownianMotionTradeGeneratorParametersExampleProvider : IExamplesProvider<FractionalBrownianMotionTradeGeneratorParameters>
    {
        /// <inheritdoc />
        public FractionalBrownianMotionTradeGeneratorParameters GetExamples()
        {
            return new FractionalBrownianMotionTradeGeneratorParameters
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
                FbmParameters = new FractionalBrownianMotionParameters
                {
                    Amplitude = DefaultParameterValues.FbmAmplitude,
                    MinimalValue = DefaultParameterValues.FbmMinimalValue,
                    HurstExponent = DefaultParameterValues.FbmHurstExponent,
                    Algorithm = DefaultParameterValues.FbmAlgorithm,
                    NormalRandomGeneratorKind = DefaultParameterValues.FbmNormalRandomGeneratorKind,
                    AssociatedUniformRandomGeneratorKind = DefaultParameterValues.FbmAssociatedUniformRandomGeneratorKind,
                    Seed = DefaultParameterValues.FbmSeed
                },
                TradeParameters = new TradeParameters
                {
                    Volume = DefaultParameterValues.TradeVolume
                }
            };
        }
    }
}
