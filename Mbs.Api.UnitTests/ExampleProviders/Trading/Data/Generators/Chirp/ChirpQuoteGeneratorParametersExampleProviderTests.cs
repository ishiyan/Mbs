using Mbs.Api.ExampleProviders.Trading.Data.Generators.Chirp;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpQuoteGeneratorParametersExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void ChirpQuoteGeneratorParametersExampleProvider_GetExamples_CorrectValues()
        {
            var example = new ChirpQuoteGeneratorParametersExampleProvider().GetExamples();

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

            Assert.AreEqual(DefaultParameterValues.ChirpAmplitude, example.ChirpParameters.Amplitude, "amplitude");
            Assert.AreEqual(DefaultParameterValues.ChirpMinimalValue, example.ChirpParameters.MinimalValue, "minimal value");
            Assert.AreEqual(DefaultParameterValues.ChirpInitialPeriod, example.ChirpParameters.InitialPeriod, "initial period");
            Assert.AreEqual(DefaultParameterValues.ChirpFinalPeriod, example.ChirpParameters.FinalPeriod, "final period");
            Assert.AreEqual(DefaultParameterValues.ChirpPhaseInPi, example.ChirpParameters.PhaseInPi, "phase in pi");
            Assert.AreEqual(DefaultParameterValues.ChirpIsBiDirectional, example.ChirpParameters.IsBiDirectional, "is bi-directional");
            Assert.AreEqual(DefaultParameterValues.ChirpSweep, example.ChirpParameters.ChirpSweep, "chirp sweep");

            Assert.AreEqual(DefaultParameterValues.SpreadFraction, example.QuoteParameters.SpreadFraction, "spread fraction");
            Assert.AreEqual(DefaultParameterValues.AskSize, example.QuoteParameters.AskSize, "ask size");
            Assert.AreEqual(DefaultParameterValues.BidSize, example.QuoteParameters.BidSize, "bid size");
        }
    }
}
