using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.RepetitiveSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.RepetitiveSample
{
    [TestClass]
    public class RepetitiveSampleGeneratorParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void RepetitiveSampleGeneratorParameters_Validate_TimeParametersIsNull_Exception()
        {
            var parameters = new RepetitiveSampleGeneratorParameters
            {
                TimeParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void RepetitiveSampleGeneratorParameters_Validate_OffsetSamplesOutOfRange_Exception()
        {
            var parameters = new RepetitiveSampleGeneratorParameters
            {
                OffsetSamples = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void RepetitiveSampleGeneratorParameters_Validate_OffsetSamplesOutOfRange_CorrectValidationResults()
        {
            var parameters = new RepetitiveSampleGeneratorParameters
            {
                OffsetSamples = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustNotBeNegative, nameof(RepetitiveSampleGeneratorParameters.OffsetSamples));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(WaveformParameters.OffsetSamples), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void RepetitiveSampleGeneratorParameters_Validate_RepetitionsCountOutOfRange_Exception()
        {
            var parameters = new RepetitiveSampleGeneratorParameters
            {
                RepetitionsCount = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void RepetitiveSampleGeneratorParameters_Validate_RepetitionsCountOutOfRange_CorrectValidationResults()
        {
            var parameters = new RepetitiveSampleGeneratorParameters
            {
                RepetitionsCount = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustNotBeNegative, nameof(RepetitiveSampleGeneratorParameters.RepetitionsCount));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(WaveformParameters.RepetitionsCount), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void RepetitiveSampleGeneratorParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new RepetitiveSampleGeneratorParameters
            {
                OffsetSamples = -1,
                RepetitionsCount = -1
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(2, results.Count, "validation results collection has 2 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(RepetitiveSampleGeneratorParameters.OffsetSamples), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(RepetitiveSampleGeneratorParameters.RepetitionsCount), results[1].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void RepetitiveSampleGeneratorParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new RepetitiveSampleGeneratorParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void RepetitiveSampleGeneratorParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new RepetitiveSampleGeneratorParameters();

            Assert.IsNotNull(parameters.TimeParameters, "default time parameters");
            Assert.AreEqual(DefaultParameterValues.OffsetSamples, parameters.OffsetSamples, "default offset samples");
            Assert.AreEqual(DefaultParameterValues.RepetitionsCount, parameters.RepetitionsCount, "default repetitions count");
        }

        [TestMethod]
        public void RepetitiveSampleGeneratorParameters_Construction_Constructor_CorrectValues()
        {
            const int offsetSamples = 13;
            const int repetitionsCount = 7;

            var parameters = new RepetitiveSampleGeneratorParameters()
            {
                OffsetSamples = offsetSamples,
                RepetitionsCount = repetitionsCount
            };

            Assert.IsNotNull(parameters.TimeParameters, "time parameters");
            Assert.AreEqual(offsetSamples, parameters.OffsetSamples, "offset samples");
            Assert.AreEqual(repetitionsCount, parameters.RepetitionsCount, "repetitions count");
        }

        [TestMethod]
        public void RepetitiveSampleGeneratorParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            var json = @"{
sampleCount: 128,
offsetSamples: 0,
repetitionsCount: 0,
timeParameters: {
  sessionBeginTime: ""09:00:00"",
  sessionEndTime: ""18:00:00"",
  startDate: ""2000-01-03"",
  timeGranularity: ""Day1"",
  businessDayCalendar: ""WeekendsOnly""
}}";

            var parameters = JsonConvert.DeserializeObject<RepetitiveSampleGeneratorParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
