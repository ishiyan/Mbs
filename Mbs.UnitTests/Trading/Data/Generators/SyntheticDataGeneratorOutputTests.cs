using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators
{
    [TestClass]
    public class SyntheticDataGeneratorOutputTests
    {
        [TestMethod]
        public void SyntheticDataGeneratorOutput_Validate_ValidInput_CorrectValidationResults()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = "a",
                Moniker = "b",
                Data = new[] { new Scalar() },
            };

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(output, new ValidationContext(output), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SyntheticDataGeneratorOutput_Validate_NameIsNull_Exception()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Moniker = "b",
                Data = new[] { new Scalar() },
            };

            var context = new ValidationContext(output);
            Validator.ValidateObject(output, context, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SyntheticDataGeneratorOutput_Validate_NameIsEmpty_Exception()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = string.Empty,
                Moniker = "b",
                Data = new[] { new Scalar() },
            };

            var context = new ValidationContext(output);
            Validator.ValidateObject(output, context, true);
        }

        [TestMethod]
        public void SyntheticDataGeneratorOutput_Validate_NameIsNull_CorrectValidationResults()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Moniker = "b",
                Data = new[] { new Scalar() },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SyntheticDataGeneratorOutput<Scalar>.Name));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(output, new ValidationContext(output), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SyntheticDataGeneratorOutput<Scalar>.Name), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SyntheticDataGeneratorOutput_Validate_NameIsEmpty_CorrectValidationResults()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = string.Empty,
                Moniker = "b",
                Data = new[] { new Scalar() },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SyntheticDataGeneratorOutput<Scalar>.Name));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(output, new ValidationContext(output), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SyntheticDataGeneratorOutput<Scalar>.Name), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SyntheticDataGeneratorOutput_Validate_MonikerIsNull_Exception()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = "a",
                Data = new[] { new Scalar() },
            };

            var context = new ValidationContext(output);
            Validator.ValidateObject(output, context, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SyntheticDataGeneratorOutput_Validate_MonikerIsEmpty_Exception()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = "a",
                Moniker = string.Empty,
                Data = new[] { new Scalar() },
            };

            var context = new ValidationContext(output);
            Validator.ValidateObject(output, context, true);
        }

        [TestMethod]
        public void SyntheticDataGeneratorOutput_Validate_MonikerIsNull_CorrectValidationResults()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = "a",
                Data = new[] { new Scalar() },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SyntheticDataGeneratorOutput<Scalar>.Moniker));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(output, new ValidationContext(output), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SyntheticDataGeneratorOutput<Scalar>.Moniker), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SyntheticDataGeneratorOutput_Validate_MonikerIsEmpty_CorrectValidationResults()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = "a",
                Moniker = string.Empty,
                Data = new[] { new Scalar() },
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SyntheticDataGeneratorOutput<Scalar>.Moniker));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(output, new ValidationContext(output), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SyntheticDataGeneratorOutput<Scalar>.Moniker), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SyntheticDataGeneratorOutput_Validate_DataIsNull_Exception()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = "a",
                Moniker = "b",
            };

            var context = new ValidationContext(output);
            Validator.ValidateObject(output, context, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void SyntheticDataGeneratorOutput_Validate_DataIsEmpty_Exception()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = "a",
                Moniker = "b",
                Data = Array.Empty<Scalar>(),
            };

            var context = new ValidationContext(output);
            Validator.ValidateObject(output, context, true);
        }

        [TestMethod]
        public void SyntheticDataGeneratorOutput_Validate_DataIsNull_CorrectValidationResults()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = "a",
                Moniker = "b",
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldIsRequired, nameof(SyntheticDataGeneratorOutput<Scalar>.Data));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(output, new ValidationContext(output), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SyntheticDataGeneratorOutput<Scalar>.Data), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SyntheticDataGeneratorOutput_Validate_DataIsEmpty_CorrectValidationResults()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = "a",
                Moniker = "b",
                Data = Array.Empty<Scalar>(),
            };

            var expectedMessage =
                $"{nameof(SyntheticDataGeneratorOutput<Scalar>.Data)} array should have positive length.";

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(output, new ValidationContext(output), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(SyntheticDataGeneratorOutput<Scalar>.Data), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void SyntheticDataGeneratorOutput_Construction_DefaultConstructor_DefaultValues()
        {
            var output = new SyntheticDataGeneratorOutput<Scalar>();

            Assert.AreEqual(null, output.Name, "default name");
            Assert.AreEqual(null, output.Moniker, "default moniker");
            Assert.AreEqual(null, output.Data, "default data");
        }

        [TestMethod]
        public void SyntheticDataGeneratorOutput_Construction_Constructor_CorrectValues()
        {
            const string name = "a";
            const string moniker = "b";
            var data = new[] { new Scalar() };

            var output = new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = name, Moniker = moniker, Data = data,
            };

            Assert.AreEqual(name, output.Name, "name");
            Assert.AreEqual(moniker, output.Moniker, "moniker");
            Assert.AreEqual(data, output.Data, "data");
            Assert.AreEqual(data.Length, output.Data.Length, "data length");
        }

        [TestMethod]
        public void SyntheticDataGeneratorOutput_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            const string json = @"{
name: ""name"",
moniker: ""moniker"",
data: [{time: ""2000-01-03"", value: 123.45}]
}";

            var output = JsonConvert.DeserializeObject<SyntheticDataGeneratorOutput<Scalar>>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(output, new ValidationContext(output), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
