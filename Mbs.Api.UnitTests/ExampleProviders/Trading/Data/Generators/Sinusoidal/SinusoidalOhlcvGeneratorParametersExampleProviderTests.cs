using Mbs.Api.ExampleProviders.Trading.Data.Generators.Sinusoidal;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.Sinusoidal
{
    [TestClass]
    public class SinusoidalOhlcvGeneratorParametersExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void SinusoidalOhlcvGeneratorParametersExampleProvider_GetExamples_CorrectValues()
        {
            var example = new SinusoidalOhlcvGeneratorParametersExampleProvider().GetExamples();

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

            Assert.AreEqual(DefaultParameterValues.SinusoidalAmplitude, example.SinusoidalParameters.Amplitude, "amplitude");
            Assert.AreEqual(DefaultParameterValues.SinusoidalMinimalValue, example.SinusoidalParameters.MinimalValue, "minimal value");
            Assert.AreEqual(DefaultParameterValues.SinusoidalPeriod, example.SinusoidalParameters.Period, "period");
            Assert.AreEqual(DefaultParameterValues.SinusoidalPhaseInPi, example.SinusoidalParameters.PhaseInPi, "phase in pi");

            Assert.AreEqual(DefaultParameterValues.CandlestickShadowFraction, example.OhlcvParameters.CandlestickShadowFraction, "candlestick shadow fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickBodyFraction, example.OhlcvParameters.CandlestickBodyFraction, "candlestick body fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, example.OhlcvParameters.Volume, "candlestick volume");
        }
    }
}
