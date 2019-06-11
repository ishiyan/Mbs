using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Square;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Square
{
    /// <inheritdoc />
    internal class SquareQuoteGeneratorParametersExampleProvider : IExamplesProvider<SquareQuoteGeneratorParameters>
    {
        /// <inheritdoc />
        public SquareQuoteGeneratorParameters GetExamples()
        {
            return new SquareQuoteGeneratorParameters
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
                SquareParameters = new SquareParameters
                {
                    Amplitude = DefaultParameterValues.SquareAmplitude,
                    MinimalValue = DefaultParameterValues.SquareMinimalValue
                },
                QuoteParameters = new QuoteParameters
                {
                    SpreadFraction = DefaultParameterValues.SpreadFraction,
                    AskSize = DefaultParameterValues.AskSize,
                    BidSize = DefaultParameterValues.BidSize
                }
            };
        }
    }
}
