using System.Linq;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators
{
    [TestClass]
    public class WaveformDataGeneratorTests
    {
        private class MockWaveformDataGenerator : WaveformDataGenerator<Scalar>
        {
            internal const string MockName = "name";
            internal const string MockMoniker = "moniker";
            internal const double WaveformStartValue = 1.1;
            internal const double OutOfWaveformValue = 0.1;

            private double value = WaveformStartValue;

            internal int GetWaveformSamples() => WaveformSamples;

            public MockWaveformDataGenerator(TimeParameters timeParameters, WaveformParameters waveformParameters)
                : base(timeParameters, waveformParameters)
            {
                Name = MockName;
                Moniker = MockMoniker;
            }

            protected override double NextSample() => value++;

            protected override double OutOfWaveformSample() => OutOfWaveformValue;

            public override Scalar GenerateNext()
            {
                Scalar scalar = base.GenerateNext();
                scalar.Value = CurrentSampleValue;
                return scalar;
            }
            public override void Reset()
            {
                base.Reset();
                value = WaveformStartValue;
            }
        }

        private readonly TimeParameters defaultTimeParameters = new TimeParameters();
        private readonly WaveformParameters defaultWaveformParameters = new WaveformParameters
        {
            NoiseAmplitudeFraction = 0
        };

        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void WaveformDataGenerator_Construction_DefaultArgumentValues_CorrectProperties()
        {
            var generator = new MockWaveformDataGenerator(defaultTimeParameters, defaultWaveformParameters);

            Assert.AreEqual(MockWaveformDataGenerator.MockName, generator.Name, "name is correct");
            Assert.AreEqual(MockWaveformDataGenerator.MockMoniker, generator.Moniker, "moniker is correct");

            Assert.AreEqual(DefaultParameterValues.SessionStartTime, generator.SessionBeginTime, "session begin time is correct");
            Assert.AreEqual(DefaultParameterValues.SessionEndTime, generator.SessionEndTime, "session end time is correct");
            Assert.AreEqual(DefaultParameterValues.StartDate, generator.StartDate, "start date is correct");
            Assert.AreEqual(DefaultParameterValues.TimeGranularity, generator.TimeGranularity, "time granularity is correct");
            Assert.AreEqual(DefaultParameterValues.BusinessDayCalendar, generator.BusinessDayCalendar, "business day calendar is correct");

            Assert.AreEqual(DefaultParameterValues.WaveformSamples, generator.GetWaveformSamples(), "waveform samples is correct");
            Assert.AreEqual(DefaultParameterValues.OffsetSamples, generator.OffsetSamples, "offset samples is correct");
            Assert.AreEqual(DefaultParameterValues.RepetitionsCount, generator.RepetitionsCount, "repetitions count is correct");
            Assert.AreEqual(DefaultParameterValues.RepetitionsCount < 1, generator.IsRepetitionsInfinite, "is repetitions infinite is correct");
            Assert.AreEqual(defaultWaveformParameters.NoiseAmplitudeFraction, generator.NoiseAmplitudeFraction, "noise amplitude fraction is correct");
            Assert.AreEqual(defaultWaveformParameters.NoiseAmplitudeFraction > double.Epsilon, generator.HasNoise, "has noise is correct");
        }

        [TestMethod]
        public void WaveformDataGenerator_GenerateNext_RepetitionCountIsPositive_CorrectEnumerableValues()
        {
            const int count = 3;
            var waveformParameters = new WaveformParameters
            {
                RepetitionsCount = 1,
                NoiseAmplitudeFraction = 0
            };
            var generator = new MockWaveformDataGenerator(defaultTimeParameters, waveformParameters);

            var enumerable = generator.GenerateNext(count).ToArray();

            Assert.IsNotNull(enumerable, "enumerable is not null");
            Assert.AreEqual(count, enumerable.Length, "enumerable length is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.Add(DefaultParameterValues.SessionEndTime), enumerable[0].Time, "enumerable[0] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue, enumerable[0].Value, "enumerable[0] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), enumerable[1].Time, "enumerable[1] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 1, enumerable[1].Value, "enumerable[1] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(2).Add(DefaultParameterValues.SessionEndTime), enumerable[2].Time, "enumerable[2] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 2, enumerable[2].Value, "enumerable[2] value is correct");
        }

        [TestMethod]
        public void WaveformDataGenerator_GenerateNext_OffsetIsZero_CorrectEnumerableValues()
        {
            const int count = 3;
            var waveformParameters = new WaveformParameters
            {
                NoiseAmplitudeFraction = 0
            };
            var generator = new MockWaveformDataGenerator(defaultTimeParameters, waveformParameters);

            var enumerable = generator.GenerateNext(count).ToArray();

            Assert.IsNotNull(enumerable, "enumerable is not null");
            Assert.AreEqual(count, enumerable.Length, "enumerable length is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.Add(DefaultParameterValues.SessionEndTime), enumerable[0].Time, "enumerable[0] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue, enumerable[0].Value, "enumerable[0] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), enumerable[1].Time, "enumerable[1] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 1, enumerable[1].Value, "enumerable[1] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(2).Add(DefaultParameterValues.SessionEndTime), enumerable[2].Time, "enumerable[2] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 2, enumerable[2].Value, "enumerable[2] value is correct");
        }

        [TestMethod]
        public void WaveformDataGenerator_GenerateNext_OffsetIsPositive_CorrectEnumerableValues()
        {
            const int count = 3;
            var waveformParameters = new WaveformParameters
            {
                OffsetSamples = 1,
                NoiseAmplitudeFraction = 0
            };

            var generator = new MockWaveformDataGenerator(defaultTimeParameters, waveformParameters);

            var enumerable = generator.GenerateNext(count).ToArray();

            Assert.IsNotNull(enumerable, "enumerable is not null");
            Assert.AreEqual(count, enumerable.Length, "enumerable length is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.Add(DefaultParameterValues.SessionEndTime), enumerable[0].Time, "enumerable[0] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.OutOfWaveformValue, enumerable[0].Value, "enumerable[0] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), enumerable[1].Time, "enumerable[1] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue, enumerable[1].Value, "enumerable[1] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(2).Add(DefaultParameterValues.SessionEndTime), enumerable[2].Time, "enumerable[2] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 1, enumerable[2].Value, "enumerable[2] value is correct");
        }

        [TestMethod]
        public void WaveformDataGenerator_Reset_OffsetIsZero_ResetsEnumerableValues()
        {
            const int count1 = 3;
            const int count2 = 3;
            var waveformParameters = new WaveformParameters
            {
                NoiseAmplitudeFraction = 0
            };
            var generator = new MockWaveformDataGenerator(defaultTimeParameters, waveformParameters);

            var enumerable1 = generator.GenerateNext(count1).ToArray();
            generator.Reset();
            var enumerable2 = generator.GenerateNext(count2).ToArray();

            Assert.IsNotNull(enumerable1, "enumerable1 is not null");
            Assert.AreEqual(count1, enumerable1.Length, "enumerable1 length is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.Add(DefaultParameterValues.SessionEndTime), enumerable1[0].Time, "enumerable1[0] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue, enumerable1[0].Value, "enumerable1[0] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), enumerable1[1].Time, "enumerable1[1] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 1, enumerable1[1].Value, "enumerable1[1] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(2).Add(DefaultParameterValues.SessionEndTime), enumerable1[2].Time, "enumerable1[2] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 2, enumerable1[2].Value, "enumerable1[2] value is correct");

            Assert.IsNotNull(enumerable2, "enumerable2 is not null");
            Assert.AreEqual(count2, enumerable2.Length, "enumerable2 length is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.Add(DefaultParameterValues.SessionEndTime), enumerable2[0].Time, "enumerable2[0] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue, enumerable2[0].Value, "enumerable2[0] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), enumerable2[1].Time, "enumerable2[1] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 1, enumerable2[1].Value, "enumerable2[1] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(2).Add(DefaultParameterValues.SessionEndTime), enumerable2[2].Time, "enumerable2[2] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 2, enumerable2[2].Value, "enumerable2[2] value is correct");
        }

        [TestMethod]
        public void WaveformDataGenerator_Reset_OffsetIsPositive_ResetsEnumerableValues()
        {
            const int count1 = 3;
            const int count2 = 3;
            var waveformParameters = new WaveformParameters
            {
                OffsetSamples = 1,
                NoiseAmplitudeFraction = 0
            };
            var generator = new MockWaveformDataGenerator(defaultTimeParameters, waveformParameters);

            var enumerable1 = generator.GenerateNext(count1).ToArray();
            generator.Reset();
            var enumerable2 = generator.GenerateNext(count2).ToArray();

            Assert.IsNotNull(enumerable1, "enumerable1 is not null");
            Assert.AreEqual(count1, enumerable1.Length, "enumerable1 length is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.Add(DefaultParameterValues.SessionEndTime), enumerable1[0].Time, "enumerable1[0] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.OutOfWaveformValue, enumerable1[0].Value, "enumerable1[0] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), enumerable1[1].Time, "enumerable1[1] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue, enumerable1[1].Value, "enumerable1[1] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(2).Add(DefaultParameterValues.SessionEndTime), enumerable1[2].Time, "enumerable1[2] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 1, enumerable1[2].Value, "enumerable1[2] value is correct");

            Assert.IsNotNull(enumerable2, "enumerable2 is not null");
            Assert.AreEqual(count2, enumerable2.Length, "enumerable2 length is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.Add(DefaultParameterValues.SessionEndTime), enumerable2[0].Time, "enumerable2[0] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.OutOfWaveformValue, enumerable2[0].Value, "enumerable2[0] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), enumerable2[1].Time, "enumerable2[1] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue, enumerable2[1].Value, "enumerable2[1] value is correct");

            Assert.AreEqual(defaultTimeParameters.StartDate.AddDays(2).Add(DefaultParameterValues.SessionEndTime), enumerable2[2].Time, "enumerable2[2] time is correct");
            Assert.AreEqual(MockWaveformDataGenerator.WaveformStartValue + 1, enumerable2[2].Value, "enumerable2[2] value is correct");
        }
    }
}
