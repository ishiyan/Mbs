using System;
using System.Globalization;
using Mbs.Numerics.Random;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators.MultiSinusoidal
{
    /// <summary>
    /// The multi-sinusoidal <see cref="Quote"/> waveform generator produces quotes with the following traits.
    /// <para/>➊ The mid prices of the quotes form a sum of sinusoids defined by the given periods, amplitudes and phases.
    /// <para/>➋ The sum of sinusoids is shifted upwards so it has minimal values at the specified minimal price level.
    /// <para/>➌ The ask and bid prices are equidistant from the mid price.
    /// <para/>The half-length of the spread is defined as a ratio ∈[0, 1].
    /// <para/>When the ratio is 1, the bid price is zero and the ask price is twice the mid price.
    /// <para/>When the ratio is 0, the both ask and bid prices are equal to the the mid price.
    /// <para/>➍ The ask and bid sizes are constant values.
    /// <para/>➎ An optional noise may be added to mid prices.
    /// </summary>
    public sealed class MultiSinusoidalQuoteGenerator : MultiSinusoidalDataGenerator<Quote>
    {
        /// <summary>
        /// Gets the spread fraction, ρs, which determines the ask and bid prices as a fraction of the mid price.
        /// </summary>
        public double SpreadFraction { get; }

        /// <summary>
        /// Gets the value of the ask size, which is the same for all quotes; should be positive.
        /// </summary>
        public double AskSize { get; }

        /// <summary>
        /// Gets the value of the bid size, which is the same for all quotes; should be positive.
        /// </summary>
        public double BidSize { get; }

        internal const string WaveformName = "Multi-sinusoidal quote waveform";

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSinusoidalQuoteGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters.</param>
        public MultiSinusoidalQuoteGenerator(MultiSinusoidalQuoteGeneratorParameters parameters)
            : base(parameters.TimeParameters, parameters.WaveformParameters, parameters.MultiSinusoidalParameters)
        {
            SpreadFraction = parameters.QuoteParameters.SpreadFraction;
            AskSize = parameters.QuoteParameters.AskSize;
            BidSize = parameters.QuoteParameters.BidSize;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSinusoidalQuoteGenerator"/> class.
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
        /// <param name="periods">The array of periods of the component sinusoids in samples, should be ≥ 2.</param>
        /// <param name="amplitudes">The array of amplitudes of the component sinusoids in price units, should be positive.</param>
        /// <param name="sampleMinimum">The value corresponding to the minimum of the sum of component sinusoids, should be positive.</param>
        /// <param name="phasesInPi">The phases, φ, of the component sinusoids in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].</param>
        /// <param name="spreadFraction">The spread fraction, ρs, which determines the ask and bid prices as a fraction of the mid price.</param>
        /// <param name="askSize">The value of the ask size, which is the same for all quotes.</param>
        /// <param name="bidSize">The value of the bid size, which is the same for all quotes.</param>
        public MultiSinusoidalQuoteGenerator(
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
            double[] periods = null,
            double[] amplitudes = null,
            double sampleMinimum = DefaultParameterValues.MultiSinusoidalMinimalValue,
            double[] phasesInPi = null,
            double spreadFraction = DefaultParameterValues.SpreadFraction,
            double askSize = DefaultParameterValues.AskSize,
            double bidSize = DefaultParameterValues.BidSize)
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
                periods,
                amplitudes,
                sampleMinimum,
                phasesInPi)
        {
            SpreadFraction = spreadFraction;
            AskSize = askSize;
            BidSize = bidSize;
            Initialize();
        }

        private void Initialize()
        {
            const double delta = 0.00005;
            if (Math.Abs(SpreadFraction) > delta)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, ρs={1:0.####}", Moniker, SpreadFraction);

            if (Math.Abs(AskSize) > delta)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, as={1:0.####}", Moniker, AskSize);

            if (Math.Abs(BidSize) > delta)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, bs={1:0.####}", Moniker, BidSize);

            Name = WaveformName;
        }

        /// <inheritdoc />
        public override Quote GenerateNext()
        {
            Quote quote = base.GenerateNext();
            double midPrice = CurrentSampleValue;
            double spread = midPrice * SpreadFraction;
            quote.AskPrice = midPrice + spread;
            quote.BidPrice = midPrice - spread;
            quote.AskSize = AskSize;
            quote.BidSize = BidSize;
            return quote;
        }
    }
}
