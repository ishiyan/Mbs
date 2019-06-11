﻿using Mbs.Api.ExampleProviders.Trading.Data.Generators.Sawtooth;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.UnitTests.ExampleProviders.Trading.Data.Generators.Sawtooth
{
    [TestClass]
    public class SawtoothQuoteGeneratorParametersExampleProviderTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void SawtoothQuoteGeneratorParametersExampleProvider_GetExamples_CorrectValues()
        {
            var example = new SawtoothQuoteGeneratorParametersExampleProvider().GetExamples();

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

            Assert.AreEqual(DefaultParameterValues.SawtoothAmplitude, example.SawtoothParameters.Amplitude, "amplitude");
            Assert.AreEqual(DefaultParameterValues.SawtoothMinimalValue, example.SawtoothParameters.MinimalValue, "minimal value");
            Assert.AreEqual(DefaultParameterValues.SawtoothIsBiDirectional, example.SawtoothParameters.IsBiDirectional, "is bi-directional");
            Assert.AreEqual(DefaultParameterValues.SawtoothShape, example.SawtoothParameters.Shape, "shape");

            Assert.AreEqual(DefaultParameterValues.SpreadFraction, example.QuoteParameters.SpreadFraction, "spread fraction");
            Assert.AreEqual(DefaultParameterValues.AskSize, example.QuoteParameters.AskSize, "ask size");
            Assert.AreEqual(DefaultParameterValues.BidSize, example.QuoteParameters.BidSize, "bid size");
        }
    }
}
