using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.Sinusoidal
{
    [TestClass]
    public class SinusoidalParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalParameters_Validate_AmplitudeOutOfRange_Exception()
        {
            var parameters = new SinusoidalParameters
            {
                Amplitude = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalParameters_Validate_AmplitudeOutOfRange_CorrectValidationResults()
        {
            var parameters = new SinusoidalParameters
            {
                Amplitude = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(SinusoidalParameters.Amplitude));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalParameters.Amplitude), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalParameters_Validate_MinimalValueOutOfRange_Exception()
        {
            var parameters = new SinusoidalParameters
            {
                MinimalValue = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalParameters_Validate_MinimalValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new SinusoidalParameters
            {
                MinimalValue = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(SinusoidalParameters.MinimalValue));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalParameters.MinimalValue), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalParameters_Validate_PeriodValueOutOfRange_Exception()
        {
            var parameters = new SinusoidalParameters
            {
                Period = 1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalParameters_Validate_PeriodValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new SinusoidalParameters
            {
                Period = 0
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(SinusoidalParameters.Period));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalParameters.Period), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SinusoidalParameters_Validate_PhaseInPiValueOutOfRange_Exception()
        {
            var parameters = new SinusoidalParameters
            {
                PhaseInPi = -2
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void SinusoidalParameters_Validate_PhaseInPiValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new SinusoidalParameters
            {
                PhaseInPi = 2
            };

            string expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBeInRangeMin1Plus1, nameof(SinusoidalParameters.PhaseInPi));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SinusoidalParameters.PhaseInPi), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SinusoidalParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new SinusoidalParameters
            {
                Amplitude = -1,
                MinimalValue = -1,
                Period = 1,
                PhaseInPi = -2
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(4, results.Count, "validation results collection has 4 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(SinusoidalParameters.Amplitude), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(SinusoidalParameters.MinimalValue), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(SinusoidalParameters.Period), results[2].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[3].MemberNames.Count(), "validation result 3 has single member name");
            Assert.AreEqual(nameof(SinusoidalParameters.PhaseInPi), results[3].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SinusoidalParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new SinusoidalParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void SinusoidalParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new SinusoidalParameters();

            Assert.AreEqual(DefaultParameterValues.SinusoidalAmplitude, parameters.Amplitude, "default amplitude");
            Assert.AreEqual(DefaultParameterValues.SinusoidalMinimalValue, parameters.MinimalValue, "default minimal value");
            Assert.AreEqual(DefaultParameterValues.SinusoidalPeriod, parameters.Period, "default period");
            Assert.AreEqual(DefaultParameterValues.SinusoidalPhaseInPi, parameters.PhaseInPi, "default phase in pi");
        }

        [TestMethod]
        public void SinusoidalParameters_Construction_Constructor_CorrectValues()
        {
            const double amplitude = 13;
            const double minimalValue = 7;
            const double period = 42;
            const double phaseInPi = 0.42;

            var parameters = new SinusoidalParameters
            {
                Amplitude = amplitude,
                MinimalValue = minimalValue,
                Period = period,
                PhaseInPi = phaseInPi
            };

            Assert.AreEqual(amplitude, parameters.Amplitude, "amplitude");
            Assert.AreEqual(minimalValue, parameters.MinimalValue, "minimal value");
            Assert.AreEqual(period, parameters.Period, "period");
            Assert.AreEqual(phaseInPi, parameters.PhaseInPi, "phase in pi");
        }

        [TestMethod]
        public void SinusoidalParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            var json = @"{
amplitude: 100,
minimalValue: 10,
period: 16,
phaseInPi: 0
}";

            var parameters = JsonConvert.DeserializeObject<SinusoidalParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
