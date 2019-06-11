using System.Linq;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.MultiSinusoidal;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class MultiSinusoidalTradeGeneratorParametersExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void MultiSinusoidalTradeGeneratorParametersExampleProvider_GetExamples_CorrectValues()
        {
            var example = new MultiSinusoidalTradeGeneratorParametersExampleProvider().GetExamples();

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

            Assert.AreEqual(DefaultParameterValues.MultiSinusoidalMinimalValue, example.MultiSinusoidalParameters.MinimalValue, "minimal value");
            Assert.IsNotNull(example.MultiSinusoidalParameters.MultiSinusoidalComponents);
            var array = example.MultiSinusoidalParameters.MultiSinusoidalComponents.ToArray();
            Assert.AreEqual(1, array.Length, "single multi sinusoidal component");
            Assert.AreEqual(DefaultParameterValues.MultiSinusoidalAmplitude, array[0].Amplitude, "amplitude");
            Assert.AreEqual(DefaultParameterValues.MultiSinusoidalPeriod, array[0].Period, "period");
            Assert.AreEqual(DefaultParameterValues.MultiSinusoidalPhaseInPi, array[0].PhaseInPi, "phase in pi");

            Assert.AreEqual(DefaultParameterValues.TradeVolume, example.TradeParameters.Volume, "trade volume");
        }
    }
}
