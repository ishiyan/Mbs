using System;
using System.Globalization;
using Mbs.Trading.Time;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Data.Generators.RepetitiveSample
{
    /// <summary>
    /// The repetitive <see cref="Scalar"/> sample generator.
    /// </summary>
    public sealed class RepetitiveSampleScalarGenerator : TemporalDataGenerator<Scalar>
    {
        internal const string WaveformName = "Repetitive scalar sample";

        private readonly int offsetSamples;
        private readonly int repetitionsCount;
        private readonly bool isRepetitionsInfinite;
        private int sampleIndex;
        private int currentRepetition;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepetitiveSampleScalarGenerator"/> class.
        /// </summary>
        /// <param name="parameters">The input parameters for the <see cref="WaveformDataGenerator{T}"/>.</param>
        public RepetitiveSampleScalarGenerator(RepetitiveSampleGeneratorParameters parameters)
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
        /// Initializes a new instance of the <see cref="RepetitiveSampleScalarGenerator"/> class.
        /// </summary>
        /// <param name="sessionBeginTime">The time of the beginning of the trading session.</param>
        /// <param name="sessionEndTime">The end time of the trading session.</param>
        /// <param name="startDate">The date of the first data sample.</param>
        /// <param name="timeGranularity">The time granularity of data samples.</param>
        /// <param name="businessDayCalendar">The value specifying an exchange holiday schedule or a general country holiday schedule.</param>
        /// <param name="offsetSamples">The number of samples before the very first waveform. The value of zero means the waveform starts immediately.</param>
        /// <param name="repetitionsCount">The number of repetitions of the waveform. The value of zero means infinite.</param>
        public RepetitiveSampleScalarGenerator(
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
            if (offsetSamples < 0)
            {
                offsetSamples = 0;
            }

            this.offsetSamples = offsetSamples;
            sampleIndex = -offsetSamples;

            if (repetitionsCount < 0)
            {
                repetitionsCount = 0;
            }

            this.repetitionsCount = repetitionsCount;
            isRepetitionsInfinite = repetitionsCount < 1;

            Initialize();
        }

        /// <inheritdoc />
        public override Scalar GenerateNext()
        {
            DateTime dateTime = NextTime();
            if (sampleIndex < 0 && ++sampleIndex <= 0)
            {
                return ScalarByIndex(0, dateTime);
            }

            Scalar scalar;
            if (isRepetitionsInfinite)
            {
                scalar = ScalarByIndex(sampleIndex, dateTime);
                if (++sampleIndex == RepetitiveSampleData.SampleCount)
                {
                    sampleIndex = 0;
                }
            }
            else
            {
                if (repetitionsCount <= currentRepetition)
                {
                    return ScalarByIndex(0, dateTime);
                }

                scalar = ScalarByIndex(sampleIndex, dateTime);
                if (++sampleIndex == RepetitiveSampleData.SampleCount)
                {
                    sampleIndex = 0;
                    ++currentRepetition;
                }
            }

            return scalar;
        }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            sampleIndex = -offsetSamples;
            currentRepetition = 0;
        }

        private static Scalar ScalarByIndex(int index, DateTime dateTime)
        {
            return new Scalar(dateTime, RepetitiveSampleData.Close[index]);
        }

        private void Initialize()
        {
            Moniker = string.Concat("repetitive scalar sample (len=", RepetitiveSampleData.SampleCount, ")");

            if (offsetSamples > 0)
            {
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, off={1}", Moniker, offsetSamples);
            }

            if (!isRepetitionsInfinite)
            {
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, rep={1}", Moniker, repetitionsCount);
            }

            Name = WaveformName;
        }
    }
}
