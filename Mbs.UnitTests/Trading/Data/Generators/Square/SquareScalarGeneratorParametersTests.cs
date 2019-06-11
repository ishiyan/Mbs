using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Square;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.Square
{
    [TestClass]
    public class SquareScalarGeneratorParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SquareScalarGeneratorParameters_Validate_SampleCountOutOfRange_Exception()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                SampleCount = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Validate_SampleCountOutOfRange_CorrectValidationResults()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                SampleCount = 1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(SquareScalarGeneratorParameters.SampleCount));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.SampleCount), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SquareScalarGeneratorParameters_Validate_TimeParametersIsNull_Exception()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                TimeParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Validate_TimeParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                TimeParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SquareScalarGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SquareScalarGeneratorParameters_Validate_TimeParametersInvalid_Exception()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Validate_TimeParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SquareScalarGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SquareScalarGeneratorParameters_Validate_WaveformParametersIsNull_Exception()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                WaveformParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Validate_WaveformParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                WaveformParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SquareScalarGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SquareScalarGeneratorParameters_Validate_WaveformParametersInvalid_Exception()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Validate_WaveformParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SquareScalarGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SquareScalarGeneratorParameters_Validate_SquareParametersIsNull_Exception()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                SquareParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Validate_SquareParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                SquareParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SquareScalarGeneratorParameters.SquareParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.SquareParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SquareScalarGeneratorParameters_Validate_SquareParametersInvalid_Exception()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                SquareParameters = new SquareParameters { MinimalValue = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Validate_SquareParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                SquareParameters = new SquareParameters { MinimalValue = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SquareScalarGeneratorParameters.SquareParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.SquareParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new SquareScalarGeneratorParameters
            {
                TimeParameters = null,
                WaveformParameters = null,
                SquareParameters = null
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(3, results.Count, "validation results collection has 3 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.WaveformParameters), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(SquareScalarGeneratorParameters.SquareParameters), results[2].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new SquareScalarGeneratorParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new SquareScalarGeneratorParameters();

            Assert.IsNotNull(parameters.TimeParameters, "default time parameters");
            Assert.IsNotNull(parameters.WaveformParameters, "default waveform parameters");
            Assert.IsNotNull(parameters.SquareParameters, "default multi-sinusoidal parameters");
        }

        [TestMethod]
        public void SquareScalarGeneratorParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            var json = @"{
timeParameters: {
    sessionBeginTime: ""09:00:00"",
    sessionEndTime: ""18:00:00"",
    startDate: ""2000-01-03"",
    timeGranularity: ""Day1"",
    businessDayCalendar: ""WeekendsOnly""
},
waveformParameters: {
    waveformSamples: 128,
    offsetSamples: 13,
    repetitionsCount: 2,
    noiseAmplitudeFraction: 0.03,
    noiseUniformRandomGeneratorKind: ""Well44497A"",
    noiseUniformRandomGeneratorSeed: 654321
},
squareParameters: {
    amplitude: 100,
    minimalValue: 10
}}";

            var parameters = JsonConvert.DeserializeObject<SquareScalarGeneratorParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
