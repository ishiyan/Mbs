using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.GeometricBrownianMotion
{
    [TestClass]
    public class GeometricBrownianMotionScalarGeneratorParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_SampleCountOutOfRange_Exception()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                SampleCount = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_SampleCountOutOfRange_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                SampleCount = 1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(GeometricBrownianMotionScalarGeneratorParameters.SampleCount));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.SampleCount), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_TimeParametersIsNull_Exception()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                TimeParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_TimeParametersIsnull_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                TimeParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(GeometricBrownianMotionScalarGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_TimeParametersInvalid_Exception()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_TimeParametersInvalid_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(GeometricBrownianMotionScalarGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_WaveformParametersIsNull_Exception()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                WaveformParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_WaveformParametersIsnull_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                WaveformParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(GeometricBrownianMotionScalarGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_WaveformParametersInvalid_Exception()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_WaveformParametersInvalid_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(GeometricBrownianMotionScalarGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_GbmParametersIsNull_Exception()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                GbmParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_GbmParametersIsnull_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                GbmParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(GeometricBrownianMotionScalarGeneratorParameters.GbmParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.GbmParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_GbmParametersInvalid_Exception()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                GbmParameters = new GeometricBrownianMotionParameters { MinimalValue = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_GbmParametersInvalid_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                GbmParameters = new GeometricBrownianMotionParameters { MinimalValue = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(GeometricBrownianMotionScalarGeneratorParameters.GbmParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.GbmParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters
            {
                TimeParameters = null,
                WaveformParameters = null,
                GbmParameters = null
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(3, results.Count, "validation results collection has 3 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.WaveformParameters), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionScalarGeneratorParameters.GbmParameters), results[2].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new GeometricBrownianMotionScalarGeneratorParameters();

            Assert.IsNotNull(parameters.TimeParameters, "default time parameters");
            Assert.IsNotNull(parameters.WaveformParameters, "default waveform parameters");
            Assert.IsNotNull(parameters.GbmParameters, "default geometric Brownian motion parameters");
        }

        [TestMethod]
        public void GeometricBrownianMotionScalarGeneratorParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
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
gbmParameters: {
    amplitude: 100,
    minimalValue: 10,
    drift: 0.003,
    volatility: 0.3,
    normalRandomGeneratorKind: ""ZigguratColinGreen"",
    associatedUniformRandomGeneratorKind: ""Well44497A"",
    seed: 123456789
}}";

            var parameters = JsonConvert.DeserializeObject<GeometricBrownianMotionScalarGeneratorParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
