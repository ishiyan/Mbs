using System;
using System.Globalization;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators.RepetitiveSample
{
    /// <summary>
    /// The repetitive <see cref="Quote"/> sample generator.
    /// </summary>
    public sealed class RepetitiveSampleQuoteGenerator : TemporalDataGenerator<Quote>
    {
        internal const string WaveformName = "Repetitive quote sample";

        private readonly bool isOneDay;
        private readonly double volumeRatio;
        private readonly int offsetSamples;
        private readonly int repetitionsCount;
        private readonly bool isRepetitionsInfinite;
        private int sampleIndex;
        private int currentRepetition;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepetitiveSampleQuoteGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters for the <see cref="WaveformDataGenerator{T}"/>.</param>
        public RepetitiveSampleQuoteGenerator(RepetitiveSampleGeneratorParameters parameters)
            : this(
                parameters.TimeParameters.SessionStartTime,
                parameters.TimeParameters.SessionEndTime,
                parameters.TimeParameters.StartDate,
                parameters.TimeParameters.TimeGranularity,
                parameters.TimeParameters.BusinessDayCalendar,
                parameters.OffsetSamples,
                parameters.RepetitionsCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepetitiveSampleQuoteGenerator"/> class.
        /// </summary>
        /// <param name="sessionBeginTime">The time of the beginning of the trading session.</param>
        /// <param name="sessionEndTime">The end time of the trading session.</param>
        /// <param name="startDate">The date of the first data sample.</param>
        /// <param name="timeGranularity">The time granularity of data samples.</param>
        /// <param name="businessDayCalendar">The value specifying an exchange holiday schedule or a general country holiday schedule.</param>
        /// <param name="offsetSamples">The number of samples before the very first waveform. The value of zero means the waveform starts immediately.</param>
        /// <param name="repetitionsCount">The number of repetitions of the waveform. The value of zero means infinite.</param>
        public RepetitiveSampleQuoteGenerator(
            TimeSpan sessionBeginTime,
            TimeSpan sessionEndTime,
            DateTime startDate,
            TimeGranularity timeGranularity = DefaultParameterValues.TimeGranularity,
            BusinessDayCalendar businessDayCalendar = DefaultParameterValues.BusinessDayCalendar,
            int offsetSamples = DefaultParameterValues.OffsetSamples,
            int repetitionsCount = DefaultParameterValues.RepetitionsCount)
            : base(
                sessionBeginTime,
                sessionEndTime,
                startDate,
                timeGranularity,
                businessDayCalendar)
        {
            long ticks = timeGranularity.TimeSpan().Ticks;
            isOneDay = ticks == RepetitiveSampleData.OneDayTicks;
            volumeRatio = isOneDay ? 1 : (double)ticks / RepetitiveSampleData.OneDayTicks;

            if (offsetSamples < 0)
                offsetSamples = 0;
            this.offsetSamples = offsetSamples;
            sampleIndex = -offsetSamples;

            if (repetitionsCount < 0)
                repetitionsCount = 0;
            this.repetitionsCount = repetitionsCount;
            isRepetitionsInfinite = repetitionsCount < 1;

            Initialize();
        }

        private void Initialize()
        {
            Moniker = string.Concat("repetitive quote sample (len=", RepetitiveSampleData.SampleCount, ")");

            if (offsetSamples > 0)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, off={1}", Moniker, offsetSamples);

            if (!isRepetitionsInfinite)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, rep={1}", Moniker, repetitionsCount);

            Name = WaveformName;
        }

        /// <inheritdoc />
        public override Quote GenerateNext()
        {
            DateTime dateTime = NextTime();
            if (sampleIndex < 0)
            {
                if (0 >= ++sampleIndex)
                    return QuoteByIndex(0, dateTime);
            }

            Quote quote;
            if (isRepetitionsInfinite)
            {
                quote = QuoteByIndex(sampleIndex, dateTime);
                if (++sampleIndex == RepetitiveSampleData.SampleCount)
                    sampleIndex = 0;
            }
            else
            {
                if (repetitionsCount <= currentRepetition)
                    return QuoteByIndex(0, dateTime);
                quote = QuoteByIndex(sampleIndex, dateTime);
                if (++sampleIndex == RepetitiveSampleData.SampleCount)
                {
                    sampleIndex = 0;
                    ++currentRepetition;
                }
            }

            return quote;
        }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            sampleIndex = -offsetSamples;
            currentRepetition = 0;
        }

        private Quote QuoteByIndex(int index, DateTime dateTime)
        {
            double vol = RepetitiveSampleData.Volume[index];
            if (!isOneDay)
            {
                vol *= volumeRatio;
                if (vol < 1)
                    vol = 1;
            }

            return new Quote(dateTime, RepetitiveSampleData.Low[index], vol, RepetitiveSampleData.High[index], vol);
        }
    }
}
