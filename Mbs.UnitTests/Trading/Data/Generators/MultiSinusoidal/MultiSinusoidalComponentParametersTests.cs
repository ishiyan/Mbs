using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.MultiSinusoidal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.MultiSinusoidal
{
    [TestClass]
    public class MultiSinusoidalComponentParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void MultiSinusoidalComponentParameters_Validate_AmplitudeOutOfRange_Exception()
        {
            var parameters = new MultiSinusoidalComponentParameters
            {
                Amplitude = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void MultiSinusoidalComponentParameters_Validate_AmplitudeOutOfRange_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalComponentParameters
            {
                Amplitude = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(MultiSinusoidalComponentParameters.Amplitude));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalComponentParameters.Amplitude), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void MultiSinusoidalComponentParameters_Validate_PeriodValueOutOfRange_Exception()
        {
            var parameters = new MultiSinusoidalComponentParameters
            {
                Period = 1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void MultiSinusoidalComponentParameters_Validate_PeriodValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalComponentParameters
            {
                Period = 0
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(MultiSinusoidalComponentParameters.Period));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalComponentParameters.Period), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void MultiSinusoidalComponentParameters_Validate_PhaseInPiValueOutOfRange_Exception()
        {
            var parameters = new MultiSinusoidalComponentParameters
            {
                PhaseInPi = -2
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void MultiSinusoidalComponentParameters_Validate_PhaseInPiValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalComponentParameters
            {
                PhaseInPi = 2
            };

            string expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBeInRangeMin1Plus1, nameof(MultiSinusoidalComponentParameters.PhaseInPi));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalComponentParameters.PhaseInPi), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void MultiSinusoidalComponentParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalComponentParameters
            {
                Amplitude = -1,
                Period = 1,
                PhaseInPi = -2
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(3, results.Count, "validation results collection has 3 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalComponentParameters.Amplitude), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalComponentParameters.Period), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(MultiSinusoidalComponentParameters.PhaseInPi), results[2].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void MultiSinusoidalComponentParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new MultiSinusoidalComponentParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void MultiSinusoidalComponentParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new MultiSinusoidalComponentParameters();

            Assert.AreEqual(DefaultParameterValues.MultiSinusoidalAmplitude, parameters.Amplitude, "default amplitude");
            Assert.AreEqual(DefaultParameterValues.MultiSinusoidalPeriod, parameters.Period, "default period");
            Assert.AreEqual(DefaultParameterValues.MultiSinusoidalPhaseInPi, parameters.PhaseInPi, "default phase in pi");
        }

        [TestMethod]
        public void MultiSinusoidalComponentParameters_Construction_Constructor_CorrectValues()
        {
            const double amplitude = 13;
            const double period = 42;
            const double phaseInPi = 0.42;

            var parameters = new MultiSinusoidalComponentParameters
            {
                Amplitude = amplitude,
                Period = period,
                PhaseInPi = phaseInPi
            };

            Assert.AreEqual(amplitude, parameters.Amplitude, "amplitude");
            Assert.AreEqual(period, parameters.Period, "period");
            Assert.AreEqual(phaseInPi, parameters.PhaseInPi, "phase in pi");
        }

        [TestMethod]
        public void MultiSinusoidalComponentParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            var json = @"{
amplitude: 100,
period: 16,
phaseInPi: 0
}";

            var parameters = JsonConvert.DeserializeObject<MultiSinusoidalComponentParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
