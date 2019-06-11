using Mbs.Api.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion
{
    [TestClass]
    public class GeometricBrownianMotionScalarGeneratorParametersExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParametersExampleProvider_GetExamples_CorrectValues()
        {
            var example = new GeometricBrownianMotionScalarGeneratorParametersExampleProvider().GetExamples();

            Assert.AreEqual(DefaultParameterValues.SamplesCount, example.SampleCount, "sample count");

            Assert.AreEqual(DefaultParameterValues.SessionStartTime, example.TimeParameters.SessionStartTime, "session start time");
            Assert.AreEqual(DefaultParameterValues.SessionEndTime, example.TimeParameters.SessionEndTime, "session end time");
            Assert.AreEqual(DefaultParameterValues.StartDate, example.TimeParameters.StartDate, "start date");
            Assert.AreEqual(DefaultParameterValues.TimeGranularity, example.TimeParameters.TimeGranularity, "time granularity");
            Assert.AreEqual(DefaultParameterValues.BusinessDayCalendar, example.TimeParameters.BusinessDayCalendar, "business day calendar");

            Assert.AreEqual(DefaultParameterValues.WaveformSamples, example.WaveformParameters.WaveformSamples, "waveform samples");
            Assert.AreEqual(DefaultParameterValues.OffsetSamples, example.WaveformParameters.OffsetSamples, "offset samples");
            Assert.AreEqual(DefaultParameterValues.RepetitionsCount, example.WaveformParameters.RepetitionsCount, "repetitions count");
            Assert.AreEqual(DefaultParameterValues.NoiseAmplitudeFraction, example.WaveformParameters.NoiseAmplitudeFraction, "noise amplitude fraction");
            Assert.AreEqual(DefaultParameterValues.NoiseUniformRandomGeneratorKind, example.WaveformParameters.NoiseUniformRandomGeneratorKind, "noise uniform random generator kind");
            Assert.AreEqual(DefaultParameterValues.NoiseUniformRandomGeneratorSeed, example.WaveformParameters.NoiseUniformRandomGeneratorSeed, "noise uniform random generator seed");

            Assert.AreEqual(DefaultParameterValues.GbmAmplitude, example.GbmParameters.Amplitude, "amplitude");
            Assert.AreEqual(DefaultParameterValues.GbmMinimalValue, example.GbmParameters.MinimalValue, "minimal value");
            Assert.AreEqual(DefaultParameterValues.GbmDrift, example.GbmParameters.Drift, "drift");
            Assert.AreEqual(DefaultParameterValues.GbmVolatility, example.GbmParameters.Volatility, "volatility");
            Assert.AreEqual(DefaultParameterValues.GbmNormalRandomGeneratorKind, example.GbmParameters.NormalRandomGeneratorKind, "normal random generator kind");
            Assert.AreEqual(DefaultParameterValues.GbmAssociatedUniformRandomGeneratorKind, example.GbmParameters.AssociatedUniformRandomGeneratorKind, "associated uniform random generator kind");
            Assert.AreEqual(DefaultParameterValues.GbmSeed, example.GbmParameters.Seed, "seed");
        }
    }
}
