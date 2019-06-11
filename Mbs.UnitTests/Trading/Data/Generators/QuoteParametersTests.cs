using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators
{
    [TestClass]
    public class QuoteParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public void QuoteParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new QuoteParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void QuoteParameters_Validate_SpreadFractionOutOfRange_Exception()
        {
            var parameters = new QuoteParameters
            {
                SpreadFraction = -0.1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void QuoteParameters_Validate_SpreadFractionOutOfRange_CorrectValidationResults()
        {
            var parameters = new QuoteParameters
            {
                SpreadFraction = 1.1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBeInRange01, nameof(QuoteParameters.SpreadFraction));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(QuoteParameters.SpreadFraction), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void QuoteParameters_Validate_AskSizeOutOfRange_Exception()
        {
            var parameters = new QuoteParameters
            {
                AskSize = -11
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void QuoteParameters_Validate_AskSizeOutOfRange_CorrectValidationResults()
        {
            var parameters = new QuoteParameters
            {
                AskSize = -0.01
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(QuoteParameters.AskSize));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(QuoteParameters.AskSize), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void QuoteParameters_Validate_BidSizeOutOfRange_Exception()
        {
            var parameters = new QuoteParameters
            {
                BidSize = -11
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void QuoteParameters_Validate_BidSizeOutOfRange_CorrectValidationResults()
        {
            var parameters = new QuoteParameters
            {
                BidSize = -0.01
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(QuoteParameters.BidSize));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(QuoteParameters.BidSize), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void QuoteParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new QuoteParameters();

            Assert.AreEqual(DefaultParameterValues.SpreadFraction, parameters.SpreadFraction, "default spread fraction");
            Assert.AreEqual(DefaultParameterValues.AskSize, parameters.AskSize, "default ask size");
            Assert.AreEqual(DefaultParameterValues.BidSize, parameters.BidSize, "default bid size");
        }

        [TestMethod]
        public void QuoteParameters_Construction_Constructor_CorrectValues()
        {
            const double spreadFraction = 0.123;
            const int askSize = 123;
            const int bidSize = 234;

            var parameters = new QuoteParameters()
            {
                SpreadFraction = spreadFraction,
                AskSize = askSize,
                BidSize = bidSize
            };

            Assert.AreEqual(spreadFraction, parameters.SpreadFraction, "spread fraction");
            Assert.AreEqual(askSize, parameters.AskSize, "ask size");
            Assert.AreEqual(bidSize, parameters.BidSize, "bid size");
        }

        [TestMethod]
        public void QuoteParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            var json = @"{
spreadFraction: 0.123,
askSize: 11,
bidSize: 12
}";

            var parameters = JsonConvert.DeserializeObject<TimeParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
