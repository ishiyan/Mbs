using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Numerics.Random;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators
{
    [TestClass]
    public class WaveformParametersTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void WaveformParameters_Validate_WaveformSamplesOutOfRange_Exception()
        {
            var parameters = new WaveformParameters
            {
                WaveformSamples = 3
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void WaveformParameters_Validate_WaveformSamplesOutOfRange_CorrectValidationResults()
        {
            var parameters = new WaveformParameters
            {
                WaveformSamples = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositiveMinValue4, nameof(WaveformParameters.WaveformSamples));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(WaveformParameters.WaveformSamples), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void WaveformParameters_Validate_OffsetSamplesOutOfRange_Exception()
        {
            var parameters = new WaveformParameters
            {
                OffsetSamples = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void WaveformParameters_Validate_OffsetSamplesOutOfRange_CorrectValidationResults()
        {
            var parameters = new WaveformParameters
            {
                OffsetSamples = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustNotBeNegative, nameof(WaveformParameters.OffsetSamples));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(WaveformParameters.OffsetSamples), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void WaveformParameters_Validate_RepetitionsCountOutOfRange_Exception()
        {
            var parameters = new WaveformParameters
            {
                RepetitionsCount = -1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void WaveformParameters_Validate_RepetitionsCountOutOfRange_CorrectValidationResults()
        {
            var parameters = new WaveformParameters
            {
                RepetitionsCount = -1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustNotBeNegative, nameof(WaveformParameters.RepetitionsCount));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(WaveformParameters.RepetitionsCount), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void WaveformParameters_Validate_NoiseAmplitudeFractionOutOfRange_Exception()
        {
            var parameters = new WaveformParameters
            {
                NoiseAmplitudeFraction = -0.1
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void WaveformParameters_Validate_NoiseAmplitudeFractionOutOfRange_CorrectValidationResults()
        {
            var parameters = new WaveformParameters
            {
                NoiseAmplitudeFraction = 1.1
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBeInRange01, nameof(WaveformParameters.NoiseAmplitudeFraction));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(WaveformParameters.NoiseAmplitudeFraction), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void WaveformParameters_Validate_ManyFieldsOutOfRange_CorrectValidationResults()
        {
            var parameters = new WaveformParameters
            {
                WaveformSamples = -1,
                OffsetSamples = -1,
                RepetitionsCount = -1,
                NoiseAmplitudeFraction = 1.1
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(4, results.Count, "validation results collection has 4 elements");

            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result 0 has single member name");
            Assert.AreEqual(nameof(WaveformParameters.WaveformSamples), results[0].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[1].MemberNames.Count(), "validation result 1 has single member name");
            Assert.AreEqual(nameof(WaveformParameters.OffsetSamples), results[1].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[2].MemberNames.Count(), "validation result 2 has single member name");
            Assert.AreEqual(nameof(WaveformParameters.RepetitionsCount), results[2].MemberNames.ElementAt(0));

            Assert.AreEqual(1, results[3].MemberNames.Count(), "validation result 3 has single member name");
            Assert.AreEqual(nameof(WaveformParameters.NoiseAmplitudeFraction), results[3].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void WaveformParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new WaveformParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        public void WaveformParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new WaveformParameters();

            Assert.AreEqual(DefaultParameterValues.WaveformSamples, parameters.WaveformSamples, "default waveform samples");
            Assert.AreEqual(DefaultParameterValues.OffsetSamples, parameters.OffsetSamples, "default offset samples");
            Assert.AreEqual(DefaultParameterValues.RepetitionsCount, parameters.RepetitionsCount, "default repetitions count");
            Assert.AreEqual(DefaultParameterValues.NoiseAmplitudeFraction, parameters.NoiseAmplitudeFraction, "default noise/amplitude fraction");
            Assert.AreEqual(DefaultParameterValues.NoiseUniformRandomGeneratorKind, parameters.NoiseUniformRandomGeneratorKind, "default noise uniform random generator kind");
            Assert.AreEqual(DefaultParameterValues.NoiseUniformRandomGeneratorSeed, parameters.NoiseUniformRandomGeneratorSeed, "default noise uniform random generator seed");
        }

        [TestMethod]
        public void WaveformParameters_Construction_Constructor_CorrectValues()
        {
            const int waveformSamples = 42;
            const int offsetSamples = 13;
            const int repetitionsCount = 7;
            const double noiseAmplitudeFraction = 0.03;
            const UniformRandomGeneratorKind noiseUniformRandomGeneratorKind = UniformRandomGeneratorKind.Well44497A;
            const int noiseUniformRandomGeneratorSeed = 654321;

            var parameters = new WaveformParameters()
            {
                WaveformSamples = waveformSamples,
                OffsetSamples = offsetSamples,
                RepetitionsCount = repetitionsCount,
                NoiseAmplitudeFraction = noiseAmplitudeFraction,
                NoiseUniformRandomGeneratorKind = noiseUniformRandomGeneratorKind,
                NoiseUniformRandomGeneratorSeed = noiseUniformRandomGeneratorSeed
            };

            Assert.AreEqual(waveformSamples, parameters.WaveformSamples, "waveform samples");
            Assert.AreEqual(offsetSamples, parameters.OffsetSamples, "offset samples");
            Assert.AreEqual(repetitionsCount, parameters.RepetitionsCount, "repetitions count");
            Assert.AreEqual(noiseAmplitudeFraction, parameters.NoiseAmplitudeFraction, "noise/amplitude fraction");
            Assert.AreEqual(noiseUniformRandomGeneratorKind, parameters.NoiseUniformRandomGeneratorKind, "noise uniform random generator kind");
            Assert.AreEqual(noiseUniformRandomGeneratorSeed, parameters.NoiseUniformRandomGeneratorSeed, "noise uniform random generator seed");
        }

        [TestMethod]
        public void WaveformParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            var json = @"{
waveformSamples: 128,
offsetSamples: 13,
repetitionsCount: 2,
noiseAmplitudeFraction: 0.03,
noiseUniformRandomGeneratorKind: ""Well44497A"",
noiseUniformRandomGeneratorSeed: 654321
}";

            var parameters = JsonConvert.DeserializeObject<WaveformParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
