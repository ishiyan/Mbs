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
    public class SquareParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SquareParameters_Validate_AmplitudeOutOfRange_Exception()
        {
            var parameters = new SquareParameters
            {
                Amplitude = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SquareParameters_Validate_AmplitudeOutOfRange_CorrectValidationResults()
        {
            var parameters = new SquareParameters
            {
                Amplitude = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(SquareParameters.Amplitude));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SquareParameters.Amplitude), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SquareParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new SquareParameters
            {
                Amplitude = -1,
                MinimalValue = -1
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(2, results.Count, "validation results collection has 2 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(SquareParameters.Amplitude), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(SquareParameters.MinimalValue), results[1].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SquareParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new SquareParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void SquareParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new SquareParameters();

            Assert.AreEqual(DefaultParameterValues.SquareAmplitude, parameters.Amplitude, "default amplitude");
            Assert.AreEqual(DefaultParameterValues.SquareMinimalValue, parameters.MinimalValue, "default minimal value");
        }

        [TestMethod]
        public void SquareParameters_Construction_Constructor_CorrectValues()
        {
            const double amplitude = 13;
            const double minimalValue = 7;

            var parameters = new SquareParameters
            {
                Amplitude = amplitude,
                MinimalValue = minimalValue
            };

            Assert.AreEqual(amplitude, parameters.Amplitude, "amplitude");
            Assert.AreEqual(minimalValue, parameters.MinimalValue, "minimal value");
        }

        [TestMethod]
        public void SquareParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            var json = @"{
amplitude: 100,
minimalValue: 10
}";

            var parameters = JsonConvert.DeserializeObject<SquareParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
