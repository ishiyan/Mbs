using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators
{
    [TestClass]
    public class TimeParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TimeParameters_Validate_SessionEndTimeLessThanBeginTime_Exception()
        {
            var parameters = new TimeParameters
            {
                SessionStartTime = DefaultParameterValues.SessionEndTime,
                SessionEndTime = DefaultParameterValues.SessionStartTime
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void TimeParameters_Validate_SessionEndTimeLessThanBeginTime_CorrectValidationResults()
        {
            var parameters = new TimeParameters
            {
                SessionStartTime = DefaultParameterValues.SessionEndTime,
                SessionEndTime = DefaultParameterValues.SessionStartTime
            };

            var expectedMessage =
                $"{nameof(TimeParameters.SessionStartTime)} {parameters.SessionStartTime} should be less than the {nameof(TimeParameters.SessionEndTime)} {parameters.SessionEndTime}.";

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");
            var result = results[0];
            Assert.AreEqual(expectedMessage, result.ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(2, result.MemberNames.Count(), "validation result has two member names");
            Assert.AreEqual(nameof(TimeParameters.SessionStartTime), result.MemberNames.ElementAt(0));
            Assert.AreEqual(nameof(TimeParameters.SessionEndTime), result.MemberNames.ElementAt(1));
        }

        [TestMethod]
        public void TimeParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new TimeParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void TimeParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new TimeParameters();

            Assert.AreEqual(DefaultParameterValues.SessionStartTime, parameters.SessionStartTime, "default session begin time");
            Assert.AreEqual(DefaultParameterValues.SessionEndTime, parameters.SessionEndTime, "default session end time");
            Assert.AreEqual(DefaultParameterValues.StartDate, parameters.StartDate, "default start date");
            Assert.AreEqual(DefaultParameterValues.TimeGranularity, parameters.TimeGranularity, "default time granularity");
            Assert.AreEqual(DefaultParameterValues.BusinessDayCalendar, parameters.BusinessDayCalendar, "default business day calendar");
        }

        [TestMethod]
        public void TimeParameters_Construction_Constructor_CorrectValues()
        {
            var sessionBeginTime = new TimeSpan(8, 7, 6);
            var sessionEndTime = new TimeSpan(18, 17, 16);
            var startDate = new DateTime(2010, 3, 4);
            const TimeGranularity timeGranularity = TimeGranularity.Hour1;
            const BusinessDayCalendar businessDayCalendar = BusinessDayCalendar.Switzerland;

            var parameters = new TimeParameters()
            {
                SessionStartTime = sessionBeginTime,
                SessionEndTime = sessionEndTime,
                StartDate = startDate,
                TimeGranularity = timeGranularity,
                BusinessDayCalendar = businessDayCalendar
            };

            Assert.AreEqual(sessionBeginTime, parameters.SessionStartTime, "session begin time");
            Assert.AreEqual(sessionEndTime, parameters.SessionEndTime, "session end time");
            Assert.AreEqual(startDate, parameters.StartDate, "start date");
            Assert.AreEqual(timeGranularity, parameters.TimeGranularity, "time granularity");
            Assert.AreEqual(businessDayCalendar, parameters.BusinessDayCalendar, "business day calendar");
        }

        [TestMethod]
        public void TimeParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            var json = @"{
sessionBeginTime: ""09:00:00"",
sessionEndTime: ""18:00:00"",
startDate: ""2000-01-03"",
timeGranularity: ""Day1"",
businessDayCalendar: ""WeekendsOnly""
}";

            var parameters = JsonConvert.DeserializeObject<TimeParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
