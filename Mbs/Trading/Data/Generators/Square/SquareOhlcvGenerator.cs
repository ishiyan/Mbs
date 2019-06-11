﻿using System;
using System.Globalization;
using Mbs.Numerics.Random;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators.Square
{
    /// <summary>
    /// The square impulse waveform <see cref="Ohlcv"/> generator produces candlesticks with the following traits.
    /// <para/>➊ The mid prices of the candlesticks form a square impulses defined by the given amplitude and the length of the impulse.
    /// <para/>➋ The square impulse is shifted upwards so it has minimal values at the specified minimal price level.
    /// <para/>➌ The opening and closing prices are equidistant from the mid price.
    /// <para/>The half-length of the candlestick body is defined as a ratio ∈[0, 1].
    /// <para/>When the ratio is 1, the lower price is zero and the higher price is twice the mid price.
    /// <para/>When the ratio is 0, the both opening and closing prices are equal to the the mid price.
    /// <para/>➍ The highest and the lowest prices are equidistant from the mid price.
    /// <para/>The length of the candlestick shadows is defined as a ratio ∈[0, 1].
    /// <para/>When the ratio is 1, the lowest price is zero and the highest price is twice the mid price.
    /// <para/>When the ratio is 0, the both lowest and highest prices are equal to the the mid price.
    /// <para/>➎ The volume has a constant value.
    /// <para/>➏ An optional noise may be added to mid prices.
    /// </summary>
    public sealed class SquareOhlcvGenerator : SquareDataGenerator<Ohlcv>
    {
        /// <summary>
        /// Gets the shadow fraction, ρs, which determines the length of the candlestick shadows as a fraction of the mid price; ρs∈[0, 1].
        /// The value should be greater or equal to the candlestick body fraction.
        /// </summary>
        public double CandlestickShadowFraction { get; }

        /// <summary>
        /// Gets the body fraction, ρb, which determines the half-length of the candlestick body as a fraction of the mid price; ρb∈[0, 1].
        /// The value should be less or equal to the candlestick shadow fraction.
        /// </summary>
        public double CandlestickBodyFraction { get; }

        /// <summary>
        /// Gets the value of the volume, which is the same for all candlesticks; should be positive.
        /// </summary>
        public double Volume { get; }

        internal const string WaveformName = "Square ohlcv waveform";

        private double midPricePrevious = double.NaN;

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareOhlcvGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters.</param>
        public SquareOhlcvGenerator(SquareOhlcvGeneratorParameters parameters)
            : base(parameters.TimeParameters, parameters.WaveformParameters, parameters.SquareParameters)
        {
            CandlestickShadowFraction = parameters.OhlcvParameters.CandlestickShadowFraction;
            CandlestickBodyFraction = parameters.OhlcvParameters.CandlestickBodyFraction;
            Volume = parameters.OhlcvParameters.Volume;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareOhlcvGenerator"/> class.
        /// </summary>
        /// <param name="sessionBeginTime">The time of the beginning of the trading session.</param>
        /// <param name="sessionEndTime">The end time of the trading session.</param>
        /// <param name="startDate">The date of the first data sample.</param>
        /// <param name="timeGranularity">The time granularity of data samples.</param>
        /// <param name="businessDayCalendar">The value specifying an exchange holiday schedule or a general country holiday schedule.</param>
        /// <param name="waveformSamples">The number of samples in the waveform, should be ≥ 2.</param>
        /// <param name="offsetSamples">The number of samples before the very first waveform. The value of zero means the waveform starts immediately.</param>
        /// <param name="repetitionsCount">The number of repetitions of the waveform. The value of zero means infinite.</param>
        /// <param name="noiseAmplitudeFraction">The amplitude of the noise as a fraction of the mid price.</param>
        /// <param name="noiseRandomGenerator">The uniformly distributed random generator to produce the noise.</param>
        /// <param name="sampleAmplitude">The amplitude of the square impulse in sample units, should be positive.</param>
        /// <param name="sampleMinimum">The sample value corresponding to the square impulse, should be positive.</param>
        /// <param name="candlestickShadowFraction">The shadow fraction, ρs, determines the length of the candlestick shadows as a fraction of the mid price; ρs∈[0, 1]. The value should be greater or equal to the candlestick body fraction.</param>
        /// <param name="candlestickBodyFraction">The body fraction, ρb, determines the half-length of the candlestick body as a fraction of the mid price; ρb∈[0, 1]. The value should be less or equal to the candlestick shadow fraction.</param>
        /// <param name="volume">The value of the volume, which is the same for all candlesticks; should be positive.</param>
        public SquareOhlcvGenerator(
            TimeSpan sessionBeginTime,
            TimeSpan sessionEndTime,
            DateTime startDate,
            TimeGranularity timeGranularity = DefaultParameterValues.TimeGranularity,
            BusinessDayCalendar businessDayCalendar = DefaultParameterValues.BusinessDayCalendar,
            int waveformSamples = DefaultParameterValues.WaveformSamples,
            int offsetSamples = DefaultParameterValues.OffsetSamples,
            int repetitionsCount = DefaultParameterValues.RepetitionsCount,
            double noiseAmplitudeFraction = DefaultParameterValues.NoiseAmplitudeFraction,
            IRandomGenerator noiseRandomGenerator = null,
            double sampleAmplitude = DefaultParameterValues.SquareAmplitude,
            double sampleMinimum = DefaultParameterValues.SquareMinimalValue,
            double candlestickShadowFraction = DefaultParameterValues.CandlestickShadowFraction,
            double candlestickBodyFraction = DefaultParameterValues.CandlestickBodyFraction,
            double volume = DefaultParameterValues.CandlestickVolume)
            : base(
                sessionBeginTime,
                sessionEndTime,
                startDate,
                timeGranularity,
                businessDayCalendar,
                waveformSamples,
                offsetSamples,
                repetitionsCount,
                noiseAmplitudeFraction,
                noiseRandomGenerator,
                sampleAmplitude,
                sampleMinimum)
        {
            CandlestickShadowFraction = candlestickShadowFraction;
            CandlestickBodyFraction = candlestickBodyFraction;
            Volume = volume;
            Initialize();
        }

        private void Initialize()
        {
            const double delta = 0.00005;
            if (Math.Abs(Volume) > delta)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, v={1:0.####}", Moniker, Volume);

            if (Math.Abs(CandlestickBodyFraction) > delta)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, ρb={1:0.####}", Moniker, CandlestickBodyFraction);

            if (Math.Abs(CandlestickShadowFraction) > delta)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, ρs={1:0.####}", Moniker, CandlestickShadowFraction);

            Name = WaveformName;
        }

        /// <inheritdoc />
        public override Ohlcv GenerateNext()
        {
            Ohlcv ohlcv = base.GenerateNext();
            double midPrice = CurrentSampleValue;
            double deltaShadow = midPrice * CandlestickShadowFraction;
            double deltaBody = midPrice * CandlestickBodyFraction;
            if (double.IsNaN(midPricePrevious))
                midPricePrevious = midPrice;
            else if (midPricePrevious > midPrice)
                deltaBody *= -1;
            midPricePrevious = midPrice;
            ohlcv.Open = midPrice - deltaBody;
            ohlcv.High = midPrice + deltaShadow;
            ohlcv.Low = midPrice - deltaShadow;
            ohlcv.Close = midPrice + deltaBody;
            ohlcv.Volume = Volume;
            return ohlcv;
        }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            midPricePrevious = double.NaN;
        }
    }
}
