using Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionTradeGeneratorParametersExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParametersExampleProvider_GetExamples_CorrectValues()
        {
            var example = new FractionalBrownianMotionTradeGeneratorParametersExampleProvider().GetExamples();

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

            Assert.AreEqual(DefaultParameterValues.FbmAmplitude, example.FbmParameters.Amplitude, "amplitude");
            Assert.AreEqual(DefaultParameterValues.FbmMinimalValue, example.FbmParameters.MinimalValue, "minimal value");
            Assert.AreEqual(DefaultParameterValues.FbmHurstExponent, example.FbmParameters.HurstExponent, "hurst exponent");
            Assert.AreEqual(DefaultParameterValues.FbmAlgorithm, example.FbmParameters.Algorithm, "algorithm");
            Assert.AreEqual(DefaultParameterValues.FbmNormalRandomGeneratorKind, example.FbmParameters.NormalRandomGeneratorKind, "normal random generator kind");
            Assert.AreEqual(DefaultParameterValues.FbmAssociatedUniformRandomGeneratorKind, example.FbmParameters.AssociatedUniformRandomGeneratorKind, "associated uniform random generator kind");
            Assert.AreEqual(DefaultParameterValues.FbmSeed, example.FbmParameters.Seed, "seed");

            Assert.AreEqual(DefaultParameterValues.TradeVolume, example.TradeParameters.Volume, "trade volume");
        }
    }
}
