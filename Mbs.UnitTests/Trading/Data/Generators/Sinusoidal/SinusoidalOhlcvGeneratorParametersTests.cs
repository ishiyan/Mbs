﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Mbs.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.Sinusoidal
{
    [TestClass]
    public class SinusoidalOhlcvGeneratorParametersTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalOhlcvGeneratorParameters_Validate_SampleCountOutOfRange_Exception()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                SampleCount = -1,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_SampleCountOutOfRange_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                SampleCount = 1,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(SinusoidalOhlcvGeneratorParameters.SampleCount));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.SampleCount), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalOhlcvGeneratorParameters_Validate_TimeParametersIsNull_Exception()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                TimeParameters = null,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_TimeParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                TimeParameters = null,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SinusoidalOhlcvGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalOhlcvGeneratorParameters_Validate_TimeParametersInvalid_Exception()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue },
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_TimeParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SinusoidalOhlcvGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalOhlcvGeneratorParameters_Validate_WaveformParametersIsNull_Exception()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                WaveformParameters = null,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_WaveformParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                WaveformParameters = null,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SinusoidalOhlcvGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalOhlcvGeneratorParameters_Validate_WaveformParametersInvalid_Exception()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 },
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_WaveformParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SinusoidalOhlcvGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalOhlcvGeneratorParameters_Validate_SinusoidalParametersIsNull_Exception()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                SinusoidalParameters = null,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_SinusoidalParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                SinusoidalParameters = null,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SinusoidalOhlcvGeneratorParameters.SinusoidalParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.SinusoidalParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalOhlcvGeneratorParameters_Validate_SinusoidalParametersInvalid_Exception()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                SinusoidalParameters = new SinusoidalParameters { MinimalValue = -1 },
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_SinusoidalParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                SinusoidalParameters = new SinusoidalParameters { MinimalValue = -1 },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SinusoidalOhlcvGeneratorParameters.SinusoidalParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.SinusoidalParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalOhlcvGeneratorParameters_Validate_OhlcvParametersIsNull_Exception()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                OhlcvParameters = null,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_OhlcvParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                OhlcvParameters = null,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SinusoidalOhlcvGeneratorParameters.OhlcvParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.OhlcvParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalOhlcvGeneratorParameters_Validate_OhlcvParametersInvalid_Exception()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                OhlcvParameters = new OhlcvParameters { CandlestickShadowFraction = -1 },
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_OhlcvParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                OhlcvParameters = new OhlcvParameters { CandlestickShadowFraction = -1 },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SinusoidalOhlcvGeneratorParameters.OhlcvParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.OhlcvParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters
            {
                TimeParameters = null,
                WaveformParameters = null,
                SinusoidalParameters = null,
                OhlcvParameters = null,
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(4, results.Count, "validation results collection has 4 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.WaveformParameters), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.SinusoidalParameters), results[2].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[3].MemberNames.Count(), "validation result 3 has single member name");
            Assert.AreEqual(nameof(SinusoidalOhlcvGeneratorParameters.OhlcvParameters), results[3].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new SinusoidalOhlcvGeneratorParameters();

            Assert.IsNotNull(parameters.TimeParameters, "default time parameters");
            Assert.IsNotNull(parameters.WaveformParameters, "default waveform parameters");
            Assert.IsNotNull(parameters.SinusoidalParameters, "default multi-sinusoidal parameters");
            Assert.IsNotNull(parameters.OhlcvParameters, "default ohlcv parameters");
        }

        [TestMethod]
        public void SinusoidalOhlcvGeneratorParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            const string json = @"{
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
sinusoidalParameters: {
    amplitude: 100,
    minimalValue: 10,
    period: 16,
    phaseInPi: 0
},
ohlcvParameters: {
    candlestickShadowFraction: 0.3,
    candlestickBodyFraction: 0.2,
    volume: 100
}}";

            var parameters = JsonConvert.DeserializeObject<SinusoidalOhlcvGeneratorParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
