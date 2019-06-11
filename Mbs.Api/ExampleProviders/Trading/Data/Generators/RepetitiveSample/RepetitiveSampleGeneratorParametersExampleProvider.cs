using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.RepetitiveSample;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample
{
    /// <inheritdoc />
    internal class RepetitiveSampleGeneratorParametersExampleProvider : IExamplesProvider<RepetitiveSampleGeneratorParameters>
    {
        /// <inheritdoc />
        public RepetitiveSampleGeneratorParameters GetExamples()
        {
            return new RepetitiveSampleGeneratorParameters
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
                OffsetSamples = DefaultParameterValues.OffsetSamples,
                RepetitionsCount = DefaultParameterValues.RepetitionsCount
            };
        }
    }
}
