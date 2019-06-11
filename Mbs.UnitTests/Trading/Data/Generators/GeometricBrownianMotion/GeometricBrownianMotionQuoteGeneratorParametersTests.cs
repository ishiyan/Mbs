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
    public class GeometricBrownianMotionQuoteGeneratorParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_SampleCountOutOfRange_Exception()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                SampleCount = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_SampleCountOutOfRange_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                SampleCount = 1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(GeometricBrownianMotionQuoteGeneratorParameters.SampleCount));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.SampleCount), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_TimeParametersIsNull_Exception()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                TimeParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_TimeParametersIsnull_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                TimeParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(GeometricBrownianMotionQuoteGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_TimeParametersInvalid_Exception()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_TimeParametersInvalid_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(GeometricBrownianMotionQuoteGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_WaveformParametersIsNull_Exception()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                WaveformParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_WaveformParametersIsnull_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                WaveformParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(GeometricBrownianMotionQuoteGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_WaveformParametersInvalid_Exception()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_WaveformParametersInvalid_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(GeometricBrownianMotionQuoteGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_GbmParametersIsNull_Exception()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                GbmParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_GbmParametersIsnull_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                GbmParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(GeometricBrownianMotionQuoteGeneratorParameters.GbmParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.GbmParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_GbmParametersInvalid_Exception()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                GbmParameters = new GeometricBrownianMotionParameters { MinimalValue = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_GbmParametersInvalid_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                GbmParameters = new GeometricBrownianMotionParameters { MinimalValue = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(GeometricBrownianMotionQuoteGeneratorParameters.GbmParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.GbmParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_QuoteParametersIsNull_Exception()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                QuoteParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_QuoteParametersIsnull_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                QuoteParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(GeometricBrownianMotionQuoteGeneratorParameters.QuoteParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.QuoteParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_QuoteParametersInvalid_Exception()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                QuoteParameters = new QuoteParameters { SpreadFraction = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_QuoteParametersInvalid_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                QuoteParameters = new QuoteParameters { SpreadFraction = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(GeometricBrownianMotionQuoteGeneratorParameters.QuoteParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.QuoteParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters
            {
                TimeParameters = null,
                WaveformParameters = null,
                GbmParameters = null,
                QuoteParameters = null
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(4, results.Count, "validation results collection has 4 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.WaveformParameters), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.GbmParameters), results[2].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[3].MemberNames.Count(), "validation result 3 has single member name");
            Assert.AreEqual(nameof(GeometricBrownianMotionQuoteGeneratorParameters.QuoteParameters), results[3].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new GeometricBrownianMotionQuoteGeneratorParameters();

            Assert.IsNotNull(parameters.TimeParameters, "default time parameters");
            Assert.IsNotNull(parameters.WaveformParameters, "default waveform parameters");
            Assert.IsNotNull(parameters.GbmParameters, "default geometric Brownian motion parameters");
            Assert.IsNotNull(parameters.QuoteParameters, "default quote parameters");
        }

        [TestMethod]
        public void GeometricBrownianMotionQuoteGeneratorParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
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
},
quoteParameters: {
    SpreadFraction: 0.1,
    AskSize: 10,
    BidSize: 10
}}";

            var parameters = JsonConvert.DeserializeObject<GeometricBrownianMotionQuoteGeneratorParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
