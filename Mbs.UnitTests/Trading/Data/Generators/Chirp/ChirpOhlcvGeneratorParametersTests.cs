﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Chirp;
using Mbs.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpOhlcvGeneratorParametersTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpOhlcvGeneratorParameters_Validate_SampleCountOutOfRange_Exception()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                SampleCount = -1,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_SampleCountOutOfRange_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                SampleCount = 1,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(ChirpOhlcvGeneratorParameters.SampleCount));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.SampleCount), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpOhlcvGeneratorParameters_Validate_TimeParametersIsNull_Exception()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                TimeParameters = null,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_TimeParametersIsnull_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                TimeParameters = null,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(ChirpOhlcvGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpOhlcvGeneratorParameters_Validate_TimeParametersInvalid_Exception()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue },
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_TimeParametersInvalid_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(ChirpOhlcvGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpOhlcvGeneratorParameters_Validate_WaveformParametersIsNull_Exception()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                WaveformParameters = null,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_WaveformParametersIsnull_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                WaveformParameters = null,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(ChirpOhlcvGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpOhlcvGeneratorParameters_Validate_WaveformParametersInvalid_Exception()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 },
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_WaveformParametersInvalid_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(ChirpOhlcvGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpOhlcvGeneratorParameters_Validate_ChirpParametersIsNull_Exception()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                ChirpParameters = null,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_ChirpParametersIsnull_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                ChirpParameters = null,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(ChirpOhlcvGeneratorParameters.ChirpParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.ChirpParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpOhlcvGeneratorParameters_Validate_ChirpParametersInvalid_Exception()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                ChirpParameters = new ChirpParameters { MinimalValue = -1 },
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_ChirpParametersInvalid_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                ChirpParameters = new ChirpParameters { MinimalValue = -1 },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(ChirpOhlcvGeneratorParameters.ChirpParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.ChirpParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpOhlcvGeneratorParameters_Validate_OhlcvParametersIsNull_Exception()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                OhlcvParameters = null,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_OhlcvParametersIsnull_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                OhlcvParameters = null,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(ChirpOhlcvGeneratorParameters.OhlcvParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.OhlcvParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpOhlcvGeneratorParameters_Validate_OhlcvParametersInvalid_Exception()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                OhlcvParameters = new OhlcvParameters { CandlestickShadowFraction = -1 },
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_OhlcvParametersInvalid_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                OhlcvParameters = new OhlcvParameters { CandlestickShadowFraction = -1 },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(ChirpOhlcvGeneratorParameters.OhlcvParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.OhlcvParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters
            {
                TimeParameters = null,
                WaveformParameters = null,
                ChirpParameters = null,
                OhlcvParameters = null,
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(4, results.Count, "validation results collection has 4 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.WaveformParameters), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.ChirpParameters), results[2].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[3].MemberNames.Count(), "validation result 3 has single member name");
            Assert.AreEqual(nameof(ChirpOhlcvGeneratorParameters.OhlcvParameters), results[3].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new ChirpOhlcvGeneratorParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new ChirpOhlcvGeneratorParameters();

            Assert.IsNotNull(parameters.TimeParameters, "default time parameters");
            Assert.IsNotNull(parameters.WaveformParameters, "default waveform parameters");
            Assert.IsNotNull(parameters.ChirpParameters, "default multi-Chirp parameters");
            Assert.IsNotNull(parameters.OhlcvParameters, "default ohlcv parameters");
        }

        [TestMethod]
        public void ChirpOhlcvGeneratorParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
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
chirpParameters: {
    amplitude: 100,
    minimalValue: 10,
    initialPeriod: 128,
    finalPeriod: 16,
    phaseInPi: 0,
    isBiDirectional: false,
    chirpSweep: ""LinearPeriod""
},
ohlcvParameters: {
    candlestickShadowFraction: 0.3,
    candlestickBodyFraction: 0.2,
    volume: 100
}}";

            var parameters = JsonConvert.DeserializeObject<ChirpOhlcvGeneratorParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
