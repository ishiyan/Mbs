using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.FractionalBrownianMotion
{
    [TestClass]
    public class FractionalBrownianMotionTradeGeneratorParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_SampleCountOutOfRange_Exception()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                SampleCount = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_SampleCountOutOfRange_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                SampleCount = 1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(FractionalBrownianMotionTradeGeneratorParameters.SampleCount));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.SampleCount), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_TimeParametersIsNull_Exception()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                TimeParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_TimeParametersIsnull_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                TimeParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(FractionalBrownianMotionTradeGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_TimeParametersInvalid_Exception()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_TimeParametersInvalid_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                TimeParameters = new TimeParameters { SessionEndTime = TimeSpan.MinValue, SessionStartTime = TimeSpan.MaxValue }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(FractionalBrownianMotionTradeGeneratorParameters.TimeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_WaveformParametersIsNull_Exception()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                WaveformParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_WaveformParametersIsnull_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                WaveformParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(FractionalBrownianMotionTradeGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_WaveformParametersInvalid_Exception()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_WaveformParametersInvalid_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                WaveformParameters = new WaveformParameters { WaveformSamples = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(FractionalBrownianMotionTradeGeneratorParameters.WaveformParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.WaveformParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_FbmParametersIsNull_Exception()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                FbmParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_FbmParametersIsnull_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                FbmParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(FractionalBrownianMotionTradeGeneratorParameters.FbmParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.FbmParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_FbmParametersInvalid_Exception()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                FbmParameters = new FractionalBrownianMotionParameters { MinimalValue = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_FbmParametersInvalid_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                FbmParameters = new FractionalBrownianMotionParameters { MinimalValue = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(FractionalBrownianMotionTradeGeneratorParameters.FbmParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.FbmParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_TradeParametersIsNull_Exception()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                TradeParameters = null
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_TradeParametersIsnull_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                TradeParameters = null
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(FractionalBrownianMotionTradeGeneratorParameters.TradeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.TradeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_TradeParametersInvalid_Exception()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                TradeParameters = new TradeParameters { Volume = -1 }
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_TradeParametersInvalid_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                TradeParameters = new TradeParameters { Volume = -1 }
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ValidationErrorMessages.ValidationOfFieldFailed, nameof(FractionalBrownianMotionTradeGeneratorParameters.TradeParameters));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.TradeParameters), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters
            {
                TimeParameters = null,
                WaveformParameters = null,
                FbmParameters = null,
                TradeParameters = null
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(4, results.Count, "validation results collection has 4 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.TimeParameters), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.WaveformParameters), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.FbmParameters), results[2].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[3].MemberNames.Count(), "validation result 3 has single member name");
            Assert.AreEqual(nameof(FractionalBrownianMotionTradeGeneratorParameters.TradeParameters), results[3].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new FractionalBrownianMotionTradeGeneratorParameters();

            Assert.IsNotNull(parameters.TimeParameters, "default time parameters");
            Assert.IsNotNull(parameters.WaveformParameters, "default waveform parameters");
            Assert.IsNotNull(parameters.FbmParameters, "default fractional Brownian motion parameters");
            Assert.IsNotNull(parameters.TradeParameters, "default trade parameters");
        }

        [TestMethod]
        public void FractionalBrownianMotionTradeGeneratorParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
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
fbmParameters: {
    amplitude: 100,
    minimalValue: 10,
    hurstExponent: 0.63,
    algorithm: ""Hosking"",
    normalRandomGeneratorKind: ""ZigguratColinGreen"",
    associatedUniformRandomGeneratorKind: ""Well44497A"",
    seed: 123456789
},
tradeParameters: {
    Volume: 10
}}";

            var parameters = JsonConvert.DeserializeObject<FractionalBrownianMotionTradeGeneratorParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
