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
    public class SawtoothParametersTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothParameters_Validate_AmplitudeOutOfRange_Exception()
        {
            var parameters = new SawtoothParameters
            {
                Amplitude = -1,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothParameters_Validate_AmplitudeOutOfRange_CorrectValidationResults()
        {
            var parameters = new SawtoothParameters
            {
                Amplitude = -1,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(SawtoothParameters.Amplitude));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothParameters.Amplitude), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SawtoothParameters_Validate_MinimalValueOutOfRange_Exception()
        {
            var parameters = new SawtoothParameters
            {
                MinimalValue = -1,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SawtoothParameters_Validate_MinimalValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new SawtoothParameters
            {
                MinimalValue = -1,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(SawtoothParameters.MinimalValue));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SawtoothParameters.MinimalValue), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SawtoothParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new SawtoothParameters
            {
                Amplitude = -1,
                MinimalValue = -1,
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(2, results.Count, "validation results collection has 2 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(SawtoothParameters.Amplitude), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(SawtoothParameters.MinimalValue), results[1].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SawtoothParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new SawtoothParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void SawtoothParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new SawtoothParameters();

            Assert.AreEqual(DefaultParameterValues.SawtoothAmplitude, parameters.Amplitude, "default amplitude");
            Assert.AreEqual(DefaultParameterValues.SawtoothMinimalValue, parameters.MinimalValue, "default minimal value");
            Assert.AreEqual(DefaultParameterValues.SawtoothIsBiDirectional, parameters.IsBiDirectional, "default is bi-directional");
            Assert.AreEqual(DefaultParameterValues.SawtoothShape, parameters.Shape, "default shape");
        }

        [TestMethod]
        public void SawtoothParameters_Construction_Constructor_CorrectValues()
        {
            const double amplitude = 13;
            const double minimalValue = 7;
            const bool isBiDirectional = true;
            const SawtoothShape shape = SawtoothShape.Logarithmic;

            var parameters = new SawtoothParameters
            {
                Amplitude = amplitude,
                MinimalValue = minimalValue,
                IsBiDirectional = isBiDirectional,
                Shape = shape,
            };

            Assert.AreEqual(amplitude, parameters.Amplitude, "amplitude");
            Assert.AreEqual(minimalValue, parameters.MinimalValue, "minimal value");
            Assert.AreEqual(isBiDirectional, parameters.IsBiDirectional, "is bi-directional");
            Assert.AreEqual(shape, parameters.Shape, "shape");
        }

        [TestMethod]
        public void SawtoothParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            const string json = @"{
amplitude: 100,
minimalValue: 10,
isBiDirectional: false,
shape: ""Linear""
}";

            var parameters = JsonConvert.DeserializeObject<SawtoothParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
