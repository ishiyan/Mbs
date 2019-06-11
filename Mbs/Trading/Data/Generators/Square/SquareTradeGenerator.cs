﻿using System;
using System.Globalization;
using Mbs.Numerics.Random;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators.Square
{
    /// <summary>
    /// The square impulse waveform <see cref="Trade"/> generator produces trades with the following traits.
    /// <para/>➊ The prices of the trades form a square impulses defined by the given amplitude and the length of the impulse.
    /// <para/>➋ The square impulse is shifted upwards so it has minimal values at the specified minimal price level.
    /// <para/>➌ The volume has a constant value.
    /// <para/>➍ An optional noise may be added to the prices.
    /// </summary>
    public sealed class SquareTradeGenerator : SquareDataGenerator<Trade>
    {
        /// <summary>
        /// Gets the value of the volume, which is the same for all trades; should be positive.
        /// </summary>
        public double Volume { get; }

        internal const string WaveformName = "Square trade waveform";

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareTradeGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters.</param>
        public SquareTradeGenerator(SquareTradeGeneratorParameters parameters)
            : base(parameters.TimeParameters, parameters.WaveformParameters, parameters.SquareParameters)
        {
            Volume = parameters.TradeParameters.Volume;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareTradeGenerator"/> class.
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
        /// <param name="volume">The value of the volume, which is the same for all trades.</param>
        public SquareTradeGenerator(
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
            double volume = DefaultParameterValues.TradeVolume)
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
            Volume = volume;
            Initialize();
        }

        private void Initialize()
        {
            const double delta = 0.00005;
            if (Math.Abs(Volume) > delta)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, v={1:0.####}", Moniker, Volume);

            Name = WaveformName;
        }

        /// <inheritdoc />
        public override Trade GenerateNext()
        {
            Trade trade = base.GenerateNext();
            trade.Price = CurrentSampleValue;
            trade.Volume = Volume;
            return trade;
        }
    }
}
