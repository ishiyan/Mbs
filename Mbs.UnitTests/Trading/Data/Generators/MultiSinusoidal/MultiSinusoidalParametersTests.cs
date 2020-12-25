using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.MultiSinusoidal;
using Mbs.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class MultiSinusoidalParametersTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void MultiSinusoidalParameters_Validate_MinimalValueOutOfRange_Exception()
        {
            var parameters = new MultiSinusoidalParameters
            {
                MinimalValue = -1,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void MultiSinusoidalParameters_Validate_MinimalValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalParameters
            {
                MinimalValue = -1,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(MultiSinusoidalParameters.MinimalValue));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalParameters.MinimalValue), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void MultiSinusoidalParameters_Validate_MultiSinusoidalComponentsIsNull_Exception()
        {
            var parameters = new MultiSinusoidalParameters
            {
                MultiSinusoidalComponents = null,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void MultiSinusoidalParameters_Validate_MultiSinusoidalComponentsIsNull_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalParameters
            {
                MultiSinusoidalComponents = null,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(MultiSinusoidalParameters.MultiSinusoidalComponents));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalParameters.MultiSinusoidalComponents), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void MultiSinusoidalParameters_Validate_MultiSinusoidalComponentsIsEmpty_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalParameters
            {
                MultiSinusoidalComponents = Array.Empty<MultiSinusoidalComponentParameters>(),
            };

            var expectedMessage = ErrorMessages.FieldMultiSinusoidalComponentsMustNotBeEmpty;

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalParameters.MultiSinusoidalComponents), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void MultiSinusoidalParameters_Validate_MultiSinusoidalComponentsIsInvalid_Exception()
        {
            var parameters = new MultiSinusoidalParameters
            {
                MultiSinusoidalComponents = new[] { new MultiSinusoidalComponentParameters { Amplitude = -1, Period = 16, PhaseInPi = 0 } },
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void MultiSinusoidalParameters_Validate_MultiSinusoidalComponentsIsInvalid_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalParameters
            {
                MultiSinusoidalComponents = new[] { new MultiSinusoidalComponentParameters { Amplitude = -1, Period = 1, PhaseInPi = -2 } },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture,
                ValidationErrorMessages.ValidationOfArrayFieldFailed,
                nameof(MultiSinusoidalParameters.MultiSinusoidalComponents),
                0);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalParameters.MultiSinusoidalComponents) + "[0]", results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void MultiSinusoidalParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalParameters
            {
                MinimalValue = -1,
                MultiSinusoidalComponents = null,
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(2, results.Count, "validation results collection has 2 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalParameters.MinimalValue), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalParameters.MultiSinusoidalComponents), results[1].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void MultiSinusoidalParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void MultiSinusoidalParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new MultiSinusoidalParameters();

            Assert.AreEqual(DefaultParameterValues.SinusoidalMinimalValue, parameters.MinimalValue, "default minimal value");
            Assert.IsNotNull(parameters.MultiSinusoidalComponents, "default multi-sinusoidal components");
            Assert.AreEqual(1, parameters.MultiSinusoidalComponents.ToArray().Length, "default multi-sinusoidal components length");
        }

        [TestMethod]
        public void MultiSinusoidalParameters_Construction_Constructor_CorrectValues()
        {
            const double minimalValue = 7;
            MultiSinusoidalComponentParameters[] multiSinusoidalComponents = { new MultiSinusoidalComponentParameters { Amplitude = 10, Period = 16, PhaseInPi = 0 } };

            var parameters = new MultiSinusoidalParameters
            {
                MinimalValue = minimalValue,
                MultiSinusoidalComponents = multiSinusoidalComponents,
            };

            Assert.AreEqual(minimalValue, parameters.MinimalValue, "minimal value");
            Assert.IsNotNull(parameters.MultiSinusoidalComponents, "multi-sinusoidal components is not null");
            Assert.AreEqual(1, parameters.MultiSinusoidalComponents.ToArray().Length, "multi-sinusoidal components length");
            Assert.AreEqual(multiSinusoidalComponents, parameters.MultiSinusoidalComponents, "multi-sinusoidal components");
        }

        [TestMethod]
        public void MultiSinusoidalParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            const string json = @"{
minimalValue: 10,
multiSinusoidalComponentParameters: [
{
  amplitude: 100,
  period: 16,
  phaseInPi: 0
}]}";

            var parameters = JsonConvert.DeserializeObject<MultiSinusoidalParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
