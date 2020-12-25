using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Trading.Data.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Mbs.UnitTests.Trading.Data.Generators
{
    [TestClass]
    public class TradeParametersTests
    {
        [TestMethod]
        public void TradeParameters_Validate_ValidInput_CorrectValidationResults()
        {
            var parameters = new TradeParameters();

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TradeParameters_Validate_VolumeOutOfRange_Exception()
        {
            var parameters = new TradeParameters
            {
                Volume = -1,
            };

            var context = new ValidationContext(parameters);
            Validator.ValidateObject(parameters, context, true);
        }

        [TestMethod]
        public void TradeParameters_Validate_VolumeOutOfRange_CorrectValidationResults()
        {
            var parameters = new TradeParameters
            {
                Volume = -1,
            };

            var expectedMessage = string.Format(
                CultureInfo.InvariantCulture, ErrorMessages.FieldMustBePositive, nameof(TradeParameters.Volume));

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsFalse(isValid, "validation should fail");
            Assert.AreEqual(1, results.Count, "validation results collection has a single element");

            Assert.AreEqual(expectedMessage, results[0].ErrorMessage, "validation result has an expected error message");
            Assert.AreEqual(1, results[0].MemberNames.Count(), "validation result has single member name");
            Assert.AreEqual(nameof(TradeParameters.Volume), results[0].MemberNames.ElementAt(0));
        }

        [TestMethod]
        public void TradeParameters_Construction_DefaultConstructor_DefaultValues()
        {
            var parameters = new TradeParameters();

            Assert.AreEqual(DefaultParameterValues.TradeVolume, parameters.Volume, "default volume");
        }

        [TestMethod]
        public void TradeParameters_Construction_Constructor_CorrectValues()
        {
            const int volume = 123;

            var parameters = new TradeParameters()
            {
                Volume = volume,
            };

            Assert.AreEqual(volume, parameters.Volume, "volume");
        }

        [TestMethod]
        public void TradeParameters_DeserializeFromJson_ValidInput_CorrectValidationResults()
        {
            const string json = @"{
volume: 123
}";

            var parameters = JsonConvert.DeserializeObject<TimeParameters>(json);

            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(parameters, new ValidationContext(parameters), results, true);

            Assert.IsTrue(isValid, "validation should succeed");
            Assert.AreEqual(0, results.Count, "validation results collection is empty");
        }
    }
}
