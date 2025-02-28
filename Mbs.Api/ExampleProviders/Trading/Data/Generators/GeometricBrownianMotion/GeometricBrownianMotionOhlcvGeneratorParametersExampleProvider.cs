﻿using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion
{
    /// <inheritdoc />
    internal class GeometricBrownianMotionOhlcvGeneratorParametersExampleProvider : IExamplesProvider<GeometricBrownianMotionOhlcvGeneratorParameters>
    {
        /// <inheritdoc />
        public GeometricBrownianMotionOhlcvGeneratorParameters GetExamples()
        {
            return new GeometricBrownianMotionOhlcvGeneratorParameters
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
                GbmParameters = new GeometricBrownianMotionParameters
                {
                    Amplitude = DefaultParameterValues.GbmAmplitude,
                    MinimalValue = DefaultParameterValues.GbmMinimalValue,
                    Drift = DefaultParameterValues.GbmDrift,
                    Volatility = DefaultParameterValues.GbmVolatility,
                    NormalRandomGeneratorKind = DefaultParameterValues.GbmNormalRandomGeneratorKind,
                    AssociatedUniformRandomGeneratorKind = DefaultParameterValues.GbmAssociatedUniformRandomGeneratorKind,
                    Seed = DefaultParameterValues.GbmSeed,
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
