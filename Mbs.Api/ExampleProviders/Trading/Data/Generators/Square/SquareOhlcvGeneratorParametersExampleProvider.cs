﻿using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Square;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Square
{
    /// <inheritdoc />
    internal class SquareOhlcvGeneratorParametersExampleProvider : IExamplesProvider<SquareOhlcvGeneratorParameters>
    {
        /// <inheritdoc />
        public SquareOhlcvGeneratorParameters GetExamples()
        {
            return new SquareOhlcvGeneratorParameters
            {
                SampleCount = DefaultParameterValues.SamplesCount,
                TimeParameters = new TimeParameters
                {
                    SessionStartTime = DefaultParameterValues.SessionStartTime,
                    SessionEndTime = DefaultParameterValues.SessionEndTime,
                    StartDate = DefaultParameterValues.StartDate,
                    TimeGranularity = DefaultParameterValues.TimeGranularity,
                    BusinessDayCalendar = DefaultParameterValues.BusinessDayCalendar,
                },
                WaveformParameters = new WaveformParameters
                {
                    WaveformSamples = DefaultParameterValues.WaveformSamples,
                    OffsetSamples = DefaultParameterValues.OffsetSamples,
                    RepetitionsCount = DefaultParameterValues.RepetitionsCount,
                    NoiseAmplitudeFraction = DefaultParameterValues.NoiseAmplitudeFraction,
                    NoiseUniformRandomGeneratorKind = DefaultParameterValues.NoiseUniformRandomGeneratorKind,
                    NoiseUniformRandomGeneratorSeed = DefaultParameterValues.NoiseUniformRandomGeneratorSeed,
                },
                SquareParameters = new SquareParameters
                {
                    Amplitude = DefaultParameterValues.SquareAmplitude,
                    MinimalValue = DefaultParameterValues.SquareMinimalValue,
                },
                OhlcvParameters = new OhlcvParameters
                {
                    CandlestickShadowFraction = DefaultParameterValues.CandlestickShadowFraction,
                    CandlestickBodyFraction = DefaultParameterValues.CandlestickBodyFraction,
                    Volume = DefaultParameterValues.CandlestickVolume,
                },
            };
        }
    }
}
