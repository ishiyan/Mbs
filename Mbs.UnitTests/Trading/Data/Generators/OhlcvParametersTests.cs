using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators
{
    [TestClass]
    public class OhlcvParametersTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void OhlcvParameters_Validate_CandlestickBodyFractionGreaterThanCandlestickShadowFraction_Exception()
        {
            var parameters = new OhlcvParameters
            {
                CandlestickShadowFraction = DefaultParameterValues.CandlestickBodyFraction,
                CandlestickBodyFraction = DefaultParameterValues.CandlestickShadowFraction,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void OhlcvParameters_Validate_CandlestickBodyFractionGreaterThanCandlestickShadowFraction_CorrectValidationResults()
        {
            var parameters = new OhlcvParameters
            {
                CandlestickShadowFraction = DefaultParameterValues.CandlestickBodyFraction,
                CandlestickBodyFraction = DefaultParameterValues.CandlestickShadowFraction,
            };

            string expectedMessage =
                $"{nameof(OhlcvParameters.CandlestickBodyFraction)} {parameters.CandlestickBodyFraction} should be less or equal than the {nameof(OhlcvParameters.CandlestickShadowFraction)} {parameters.CandlestickShadowFraction}.";

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");
            var result = results[0];
            Assert.AreEqual(expectedMessage, result.ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(2, result.MemberNames.Count(), "validation result has two member names");
            Assert.AreEqual(nameof(OhlcvParameters.CandlestickBodyFraction), result.MemberNames.ElementAt(0));
            Assert.AreEqual(nameof(OhlcvParameters.CandlestickShadowFraction), result.MemberNames.ElementAt(1));
        }

        [TestMethod]
        public void OhlcvParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new OhlcvParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void OhlcvParameters_Validate_CandlestickShadowFractionOutOfRange_Exception()
        {
            var parameters = new OhlcvParameters
            {
                CandlestickShadowFraction = 1.1,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void OhlcvParameters_Validate_CandlestickShadowFractionOutOfRange_CorrectValidationResults()
        {
            var parameters = new OhlcvParameters
            {
                CandlestickShadowFraction = 1.1,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBeInRange01, nameof(OhlcvParameters.CandlestickShadowFraction));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(OhlcvParameters.CandlestickShadowFraction), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void OhlcvParameters_Validate_CandlestickBodyFractionOutOfRange_Exception()
        {
            var parameters = new OhlcvParameters
            {
                CandlestickBodyFraction = -0.1,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void OhlcvParameters_Validate_CandlestickBodyFractionOutOfRange_CorrectValidationResults()
        {
            var parameters = new OhlcvParameters
            {
                CandlestickBodyFraction = -0.1,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBeInRange01, nameof(OhlcvParameters.CandlestickBodyFraction));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(OhlcvParameters.CandlestickBodyFraction), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void OhlcvParameters_Validate_VolumeOutOfRange_Exception()
        {
            var parameters = new OhlcvParameters
            {
                Volume = -11,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void OhlcvParameters_Validate_VolumeOutOfRange_CorrectValidationResults()
        {
            var parameters = new OhlcvParameters
            {
                Volume = -0.01,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustNotBeNegative, nameof(OhlcvParameters.Volume));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(OhlcvParameters.Volume), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void OhlcvParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new OhlcvParameters();

            Assert.AreEqual(DefaultParameterValues.CandlestickShadowFraction, parameters.CandlestickShadowFraction, "default candlestick shadow fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickBodyFraction, parameters.CandlestickBodyFraction, "default candlestick body fraction");
            Assert.AreEqual(DefaultParameterValues.CandlestickVolume, parameters.Volume, "default volume");
        }

        [TestMethod]
        public void OhlcvParameters_Construction_Constructor_CorrectValues()
        {
            const double candlestickShadowFraction = 0.31;
            const double candlestickBodyFraction = 0.21;
            const int volume = 123;

            var parameters = new OhlcvParameters()
            {
                CandlestickShadowFraction = candlestickShadowFraction,
                CandlestickBodyFraction = candlestickBodyFraction,
                Volume = volume,
            };

            Assert.AreEqual(candlestickShadowFraction, parameters.CandlestickShadowFraction, "candlestick shadow fraction");
            Assert.AreEqual(candlestickBodyFraction, parameters.CandlestickBodyFraction, "candlestick body fraction");
            Assert.AreEqual(volume, parameters.Volume, "volume");
        }

        [TestMethod]
        public void OhlcvParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            const string json = @"{
candlestickShadowFraction: 0.3,
candlestickBodyFraction: 0.2,
volume: 100
}";

            var parameters = JsonConvert.DeserializeObject<TimeParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
