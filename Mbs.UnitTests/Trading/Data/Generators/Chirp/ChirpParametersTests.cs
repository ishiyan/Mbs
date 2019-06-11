using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Chirp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators.Chirp
{
    [TestClass]
    public class ChirpParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpParameters_Validate_AmplitudeOutOfRange_Exception()
        {
            var parameters = new ChirpParameters
            {
                Amplitude = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpParameters_Validate_AmplitudeOutOfRange_CorrectValidationResults()
        {
            var parameters = new ChirpParameters
            {
                Amplitude = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(ChirpParameters.Amplitude));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpParameters.Amplitude), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpParameters_Validate_MinimalValueOutOfRange_Exception()
        {
            var parameters = new ChirpParameters
            {
                MinimalValue = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpParameters_Validate_MinimalValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new ChirpParameters
            {
                MinimalValue = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(ChirpParameters.MinimalValue));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpParameters.MinimalValue), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpParameters_Validate_InitialPeriodValueOutOfRange_Exception()
        {
            var parameters = new ChirpParameters
            {
                InitialPeriod = 1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpParameters_Validate_InitialPeriodValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new ChirpParameters
            {
                InitialPeriod = 0
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(ChirpParameters.InitialPeriod));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpParameters.InitialPeriod), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpParameters_Validate_FinalPeriodValueOutOfRange_Exception()
        {
            var parameters = new ChirpParameters
            {
                FinalPeriod = 1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpParameters_Validate_FinalPeriodValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new ChirpParameters
            {
                FinalPeriod = 0
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue2, nameof(ChirpParameters.FinalPeriod));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpParameters.FinalPeriod), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ChirpParameters_Validate_PhaseInPiValueOutOfRange_Exception()
        {
            var parameters = new ChirpParameters
            {
                PhaseInPi = -2
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void ChirpParameters_Validate_PhaseInPiValueOutOfRange_CorrectValidationResults()
        {
            var parameters = new ChirpParameters
            {
                PhaseInPi = 2
            };

            string expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBeInRangeMin1Plus1, nameof(ChirpParameters.PhaseInPi));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(ChirpParameters.PhaseInPi), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void ChirpParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new ChirpParameters
            {
                Amplitude = -1,
                MinimalValue = -1,
                InitialPeriod = 1,
                FinalPeriod = 0,
                PhaseInPi = -2
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(5, results.Count, "validation results collection has 5 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(ChirpParameters.Amplitude), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(ChirpParameters.MinimalValue), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(ChirpParameters.InitialPeriod), results[2].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[3].MemberNames.Count(), "validation result 3 has single member name");
            Assert.AreEqual(nameof(ChirpParameters.FinalPeriod), results[3].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[4].MemberNames.Count(), "validation result 4 has single member name");
            Assert.AreEqual(nameof(ChirpParameters.PhaseInPi), results[4].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void ChirpParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new ChirpParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void ChirpParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new ChirpParameters();

            Assert.AreEqual(DefaultParameterValues.ChirpAmplitude, parameters.Amplitude, "default amplitude");
            Assert.AreEqual(DefaultParameterValues.ChirpMinimalValue, parameters.MinimalValue, "default minimal value");
            Assert.AreEqual(DefaultParameterValues.ChirpInitialPeriod, parameters.InitialPeriod, "default initial period");
            Assert.AreEqual(DefaultParameterValues.ChirpFinalPeriod, parameters.FinalPeriod, "default final period");
            Assert.AreEqual(DefaultParameterValues.ChirpPhaseInPi, parameters.PhaseInPi, "default phase in pi");
            Assert.AreEqual(DefaultParameterValues.ChirpIsBiDirectional, parameters.IsBiDirectional, "default is bi-directional");
            Assert.AreEqual(DefaultParameterValues.ChirpSweep, parameters.ChirpSweep, "default chirp sweep");
        }

        [TestMethod]
        public void ChirpParameters_Construction_Constructor_CorrectValues()
        {
            const double amplitude = 13;
            const double minimalValue = 7;
            const double initialPeriod = 42;
            const double finalPeriod = 43;
            const double phaseInPi = 0.42;
            const bool isBiDirectional = true;
            const ChirpSweep chirpSweep = ChirpSweep.LogarithmicFrequency;

            var parameters = new ChirpParameters
            {
                Amplitude = amplitude,
                MinimalValue = minimalValue,
                InitialPeriod = initialPeriod,
                FinalPeriod = finalPeriod,
                PhaseInPi = phaseInPi,
                IsBiDirectional = isBiDirectional,
                ChirpSweep = chirpSweep
            };

            Assert.AreEqual(amplitude, parameters.Amplitude, "amplitude");
            Assert.AreEqual(minimalValue, parameters.MinimalValue, "minimal value");
            Assert.AreEqual(initialPeriod, parameters.InitialPeriod, "initial period");
            Assert.AreEqual(finalPeriod, parameters.FinalPeriod, "final period");
            Assert.AreEqual(phaseInPi, parameters.PhaseInPi, "phase in pi");
            Assert.AreEqual(isBiDirectional, parameters.IsBiDirectional, "is bi-directional");
            Assert.AreEqual(chirpSweep, parameters.ChirpSweep, "chirp sweep");
        }

        [TestMethod]
        public void ChirpParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            var json = @"{
amplitude: 100,
minimalValue: 10,
initialPeriod: 16,
finalPeriod: 32,
phaseInPi: 0,
isBiDirectional: true,
chirpSweep: ""LogarithmicFrequency""
}";

            var parameters = JsonConvert.DeserializeObject<ChirpParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
