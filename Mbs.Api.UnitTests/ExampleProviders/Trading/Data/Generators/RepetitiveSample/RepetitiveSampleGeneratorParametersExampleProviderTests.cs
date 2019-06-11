using Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.RepetitiveSample
{
    [TestClass]
    public class RepetitiveSampleGeneratorParametersExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void RepetitiveSampleGeneratorParametersExampleProvider_GetExamples_CorrectValues()
        {
            var example = new RepetitiveSampleGeneratorParametersExampleProvider().GetExamples();

            Assert.AreEqual(DefaultParameterValues.SamplesCount, example.SampleCount, "sample count");

            Assert.AreEqual(DefaultParameterValues.SessionStartTime, example.TimeParameters.SessionStartTime, "session start time");
            Assert.AreEqual(DefaultParameterValues.SessionEndTime, example.TimeParameters.SessionEndTime, "session end time");
            Assert.AreEqual(DefaultParameterValues.StartDate, example.TimeParameters.StartDate, "start date");
            Assert.AreEqual(DefaultParameterValues.TimeGranularity, example.TimeParameters.TimeGranularity, "time granularity");
            Assert.AreEqual(DefaultParameterValues.BusinessDayCalendar, example.TimeParameters.BusinessDayCalendar, "business day calendar");

            Assert.AreEqual(DefaultParameterValues.OffsetSamples, example.OffsetSamples, "offset samples");
            Assert.AreEqual(DefaultParameterValues.RepetitionsCount, example.RepetitionsCount, "repetitions count");
        }
    }
}
