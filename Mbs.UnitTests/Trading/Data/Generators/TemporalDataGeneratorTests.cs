using System;
using System.Linq;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Generators
{
    [TestClass]
    public class TemporalDataGeneratorTests
    {
        private readonly TimeParameters defaultTimeParameters = new TimeParameters();

        [TestMethod]
        public void TemporalDataGenerator_Construction_DefaultArgumentValues_CorrectProperties()
        {
            var generator = new MockTemporalDataGenerator(defaultTimeParameters);

            Assert.AreEqual(MockTemporalDataGenerator.MockName, generator.Name, "name is correct");
            Assert.AreEqual(MockTemporalDataGenerator.MockMoniker, generator.Moniker, "moniker is correct");

            Assert.AreEqual(DefaultParameterValues.SessionStartTime, generator.SessionBeginTime, "session begin time is correct");
            Assert.AreEqual(DefaultParameterValues.SessionEndTime, generator.SessionEndTime, "session end time is correct");
            Assert.AreEqual(DefaultParameterValues.StartDate, generator.StartDate, "start date is correct");
            Assert.AreEqual(DefaultParameterValues.TimeGranularity, generator.TimeGranularity, "time granularity is correct");
            Assert.AreEqual(DefaultParameterValues.BusinessDayCalendar, generator.BusinessDayCalendar, "business day calendar is correct");
        }

        [TestMethod]
        public void TemporalDataGenerator_GenerateNext_CountIsPositive_CorrectEnumerableValues()
        {
            const int count = 3;
            var generator = new MockTemporalDataGenerator(defaultTimeParameters);

            var enumerable = generator.GenerateNext(count).ToArray();

            Assert.IsNotNull(enumerable, "enumerable is not null");
            Assert.AreEqual(count, enumerable.Length, "enumerable length is correct");

            Assert.AreEqual(MockTemporalDataGenerator.StartDateTime.AddDays(1), enumerable[0].Time, "enumerable[0] time is correct");
            Assert.AreEqual(MockTemporalDataGenerator.StartValue + 1, enumerable[0].Value, "enumerable[0] value is correct");

            Assert.AreEqual(MockTemporalDataGenerator.StartDateTime.AddDays(2), enumerable[1].Time, "enumerable[1] time is correct");
            Assert.AreEqual(MockTemporalDataGenerator.StartValue + 2, enumerable[1].Value, "enumerable[1] value is correct");

            Assert.AreEqual(MockTemporalDataGenerator.StartDateTime.AddDays(3), enumerable[2].Time, "enumerable[2] time is correct");
            Assert.AreEqual(MockTemporalDataGenerator.StartValue + 3, enumerable[2].Value, "enumerable[2] value is correct");
        }

        [TestMethod]
        public void TemporalDataGenerator_GenerateNext_CountIsZero_EmptyEnumerable()
        {
            const int count = 0;
            var generator = new MockTemporalDataGenerator(defaultTimeParameters);

            var enumerable = generator.GenerateNext(count).ToArray();

            Assert.IsNotNull(enumerable, "enumerable is not null");
            Assert.AreEqual(count, enumerable.Length, "enumerable length is correct");
        }

        [TestMethod]
        public void TemporalDataGenerator_GenerateNext_CountIsNegative_EmptyEnumerable()
        {
            const int count = -3;
            var generator = new MockTemporalDataGenerator(defaultTimeParameters);

            var enumerable = generator.GenerateNext(count).ToArray();

            Assert.IsNotNull(enumerable, "enumerable is not null");
            Assert.AreEqual(0, enumerable.Length, "enumerable length is correct");
        }

        [TestMethod]
        public void TemporalDataGenerator_Reset_WhenCalled_ResetsEnumerableValues()
        {
            const int count1 = 3;
            const int count2 = 3;
            var generator = new MockTemporalDataGenerator(defaultTimeParameters);

            var enumerable1 = generator.GenerateNext(count1).ToArray();
            generator.Reset();
            var enumerable2 = generator.GenerateNext(count2).ToArray();

            Assert.IsNotNull(enumerable1, "enumerable1 is not null");
            Assert.AreEqual(count1, enumerable1.Length, "enumerable1 length is correct");

            Assert.AreEqual(MockTemporalDataGenerator.StartDateTime.AddDays(1), enumerable1[0].Time, "enumerable1[0] time is correct");
            Assert.AreEqual(MockTemporalDataGenerator.StartValue + 1, enumerable1[0].Value, "enumerable1[0] value is correct");

            Assert.AreEqual(MockTemporalDataGenerator.StartDateTime.AddDays(2), enumerable1[1].Time, "enumerable1[1] time is correct");
            Assert.AreEqual(MockTemporalDataGenerator.StartValue + 2, enumerable1[1].Value, "enumerable1[1] value is correct");

            Assert.AreEqual(MockTemporalDataGenerator.StartDateTime.AddDays(3), enumerable1[2].Time, "enumerable1[2] time is correct");
            Assert.AreEqual(MockTemporalDataGenerator.StartValue + 3, enumerable1[2].Value, "enumerable1[2] value is correct");

            Assert.IsNotNull(enumerable2, "enumerable2 is not null");
            Assert.AreEqual(count2, enumerable2.Length, "enumerable2 length is correct");

            Assert.AreEqual(MockTemporalDataGenerator.StartDateTime.AddDays(1), enumerable2[0].Time, "enumerable2[0] time is correct");
            Assert.AreEqual(MockTemporalDataGenerator.StartValue + 1, enumerable2[0].Value, "enumerable2[0] value is correct");

            Assert.AreEqual(MockTemporalDataGenerator.StartDateTime.AddDays(2), enumerable2[1].Time, "enumerable2[1] time is correct");
            Assert.AreEqual(MockTemporalDataGenerator.StartValue + 2, enumerable2[1].Value, "enumerable2[1] value is correct");

            Assert.AreEqual(MockTemporalDataGenerator.StartDateTime.AddDays(3), enumerable2[2].Time, "enumerable2[2] time is correct");
            Assert.AreEqual(MockTemporalDataGenerator.StartValue + 3, enumerable2[2].Value, "enumerable2[2] value is correct");
        }

        private class MockTemporalDataGenerator : TemporalDataGenerator<Scalar>
        {
            internal const string MockName = "name";
            internal const string MockMoniker = "moniker";
            internal const double StartValue = 1.1;

            internal static readonly DateTime StartDateTime = new DateTime(2000, 1, 1);

            private DateTime dateTime = StartDateTime;
            private double value = StartValue;

            public MockTemporalDataGenerator(TimeParameters timeParameters)
                : base(timeParameters)
            {
                Name = MockName;
                Moniker = MockMoniker;
            }

            public override Scalar GenerateNext()
            {
                dateTime = dateTime.AddDays(1);
                return new Scalar(dateTime, ++value);
            }

            public override void Reset()
            {
                dateTime = StartDateTime;
                value = StartValue;
            }
        }
    }
}
