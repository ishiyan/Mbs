using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.Sawtooth
{
    [TestClass]
    public class SawtoothTradeGeneratorParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothTradeGeneratorParameters_Validate_SampleCountOutOfRange_Exception()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                SampleCount = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_SampleCountOutOfRange_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                SampleCount = 1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(SawtoothTradeGeneratorParameters.SampleCount));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.SampleCount), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothTradeGeneratorParameters_Validate_TimeParametersIsNull_Exception()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                TimeParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_TimeParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                TimeParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SawtoothTradeGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothTradeGeneratorParameters_Validate_TimeParametersInvalid_Exception()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_TimeParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SawtoothTradeGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothTradeGeneratorParameters_Validate_WaveformParametersIsNull_Exception()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                WaveformParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_WaveformParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                WaveformParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SawtoothTradeGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothTradeGeneratorParameters_Validate_WaveformParametersInvalid_Exception()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_WaveformParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SawtoothTradeGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothTradeGeneratorParameters_Validate_SawtoothParametersIsNull_Exception()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                SawtoothParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_SawtoothParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                SawtoothParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SawtoothTradeGeneratorParameters.SawtoothParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.SawtoothParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothTradeGeneratorParameters_Validate_SawtoothParametersInvalid_Exception()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                SawtoothParameters = new SawtoothParameters { MinimalValue = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_SawtoothParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                SawtoothParameters = new SawtoothParameters { MinimalValue = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SawtoothTradeGeneratorParameters.SawtoothParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.SawtoothParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothTradeGeneratorParameters_Validate_TradeParametersIsNull_Exception()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                TradeParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_TradeParametersIsnull_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                TradeParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SawtoothTradeGeneratorParameters.TradeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.TradeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothTradeGeneratorParameters_Validate_TradeParametersInvalid_Exception()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                TradeParameters = new TradeParameters { Volume = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_TradeParametersInvalid_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                TradeParameters = new TradeParameters { Volume = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(SawtoothTradeGeneratorParameters.TradeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.TradeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters
            {
                TimeParameters = null,
                WaveformParameters = null,
                SawtoothParameters = null,
                TradeParameters = null
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(4, results.Count, "validation results collection has 4 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.WaveformParameters), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.SawtoothParameters), results[2].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[3].MemberNames.Count(), "validation result 3 has single member name");
            Assert.AreEqual(nameof(SawtoothTradeGeneratorParameters.TradeParameters), results[3].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new SawtoothTradeGeneratorParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new SawtoothTradeGeneratorParameters();

            Assert.IsNotNull(parameters.TimeParameters, "default time parameters");
            Assert.IsNotNull(parameters.WaveformParameters, "default waveform parameters");
            Assert.IsNotNull(parameters.SawtoothParameters, "default multi-sinusoidal parameters");
            Assert.IsNotNull(parameters.TradeParameters, "default trade parameters");
        }

        [TestMethod]
        public void SawtoothTradeGeneratorParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
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
sawtoothParameters: {
    amplitude: 100,
    minimalValue: 10,
    isBiDirectional: false,
    shape: ""Linear""
},
tradeParameters: {
    Volume: 10
}}";

            var parameters = JsonConvert.DeserializeObject<SawtoothTradeGeneratorParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
