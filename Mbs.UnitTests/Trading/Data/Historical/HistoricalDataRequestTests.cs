using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Historical
{
    [TestClass]
    public class HistoricalDataRequestTests
    {
        private bool DoValidation(HistoricalDataRequest instance, out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, new ValidationContext(instance), validationResults, true);
        }

        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void Construction_DefaultConstructor_CorrectDefaultValues()
        {
            var instance = new HistoricalDataRequest();

            Assert.IsNull(instance.Instrument);
            Assert.IsTrue(instance.AdjustedDataIfPresent);
            Assert.IsFalse(instance.IsDataAdjusted.HasValue);
            Assert.AreEqual(TimeGranularity.Day1, instance.TimeGranularity);
            Assert.AreEqual(new TimeSpan(19, 0, 0), instance.EndofdayClosingTime);
            Assert.AreEqual(DateTime.MinValue, instance.StartDate);
            Assert.AreEqual(DateTime.MaxValue, instance.EndDate);
        }

        [TestMethod]
        public void Validation_InstrumentIsNull_SingleError()
        {
            var instance = new HistoricalDataRequest { Instrument = null };

            var validated = DoValidation(instance, out List<ValidationResult> validationResults);

            Assert.IsFalse(validated);
            Assert.AreEqual(1, validationResults.Count);
            Assert.IsTrue(validationResults[0].ErrorMessage.Contains("field Instrument is required"));
        }

        [TestMethod]
        public void Validation_InstrumentIsNullAndMisOrderedDates_SingleError()
        {
            var instance = new HistoricalDataRequest
            {
                Instrument = null,
                StartDate = new DateTime(2010, 1, 1),
                EndDate = new DateTime(2009, 1, 1)
            };

            var validated = DoValidation(instance, out List<ValidationResult> validationResults);

            Assert.IsFalse(validated);
            Assert.AreEqual(1, validationResults.Count);
            Assert.IsTrue(validationResults[0].ErrorMessage.Contains("field Instrument is required"));
        }

        [TestMethod]
        public void Validation_MisOrderedDates_SingleError()
        {
            var instance = new HistoricalDataRequest
            {
                Instrument = new Instrument(),
                StartDate = new DateTime(2010, 1, 1),
                EndDate = new DateTime(2009, 1, 1)
            };

            var validated = DoValidation(instance, out List<ValidationResult> validationResults);

            Assert.IsFalse(validated);
            Assert.AreEqual(1, validationResults.Count);
            Assert.IsTrue(validationResults[0].ErrorMessage.Contains("EndDate should be greater than the StartDate"));
            Assert.AreEqual(2, validationResults[0].MemberNames.Count());
            Assert.IsTrue(validationResults[0].MemberNames.ElementAt(0).Contains("StartDate"));
            Assert.IsTrue(validationResults[0].MemberNames.ElementAt(1).Contains("EndDate"));
        }

        [TestMethod]
        public void Validation_ValidInstance_NoErrors()
        {
            var instance = new HistoricalDataRequest { Instrument = new Instrument() };

            var validated = DoValidation(instance, out List<ValidationResult> validationResults);

            Assert.IsTrue(validated);
            Assert.AreEqual(0, validationResults.Count);
        }
    }
}
