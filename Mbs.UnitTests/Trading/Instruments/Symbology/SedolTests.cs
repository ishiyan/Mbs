using Mbs.Trading.Instruments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Instruments
{
    [TestClass]
    public class SedolTests
    {
        private const string OldStyle = "old style";
        private const string OldStyleIncorrectCheckDigit = "old style with incorrect check digit";
        private const string NewStyle = "new style";
        private const string NewStyleIncorrectCheckDigit = "new style with incorrect check digit";
        private const string UserDefined = "user defined";
        private const string UserDefinedIncorrectCheckDigit = "user defined with incorrect check digit";
        private const string NonAlphaNumeric = "non alphanumeric";
        private const string AllVowels = "all vowels";
        private const string TooLong = "too long";
        private const string TooShort = "too short";
        private const string Empty = "empty";
        private const string Null = "null";

        /// <summary>
        /// A mix of valid SEDOL codes taken from http://rosettacode.org/wiki/SEDOLs and invalid ones.
        /// Verified on https://www.isincheck.com/.
        /// </summary>
        [DataTestMethod]
        [DataRow("0709954", SedolValidationError.None, true, OldStyle)]
        [DataRow("7108899", SedolValidationError.None, true, OldStyle)]
        [DataRow("4065663", SedolValidationError.None, true, OldStyle)]
        [DataRow("2282765", SedolValidationError.None, true, OldStyle)]
        [DataRow("5579107", SedolValidationError.None, true, OldStyle)]
        [DataRow("5852842", SedolValidationError.None, true, OldStyle)]
        [DataRow("B0YBKJ7", SedolValidationError.None, true, NewStyle)]
        [DataRow("B0YBLH2", SedolValidationError.None, true, NewStyle)]
        [DataRow("B0YBKL9", SedolValidationError.None, true, NewStyle)]
        [DataRow("B0YBKR5", SedolValidationError.None, true, NewStyle)]
        [DataRow("B0YBKT7", SedolValidationError.None, true, NewStyle)]
        [DataRow("B000300", SedolValidationError.None, true, NewStyle)]
        [DataRow("B0WNLY7", SedolValidationError.None, true, NewStyle)]
        [DataRow("B1XH2C0", SedolValidationError.None, true, NewStyle)]
        [DataRow("9123458", SedolValidationError.None, true, UserDefined)]
        [DataRow("9aBcDe1", SedolValidationError.None, true, UserDefined)]
        [DataRow(null, SedolValidationError.InvalidLength, false, Null)]
        [DataRow("", SedolValidationError.InvalidLength, false, Empty)]
        [DataRow("123456789", SedolValidationError.InvalidLength, false, TooLong)]
        [DataRow("12", SedolValidationError.InvalidLength, false, TooShort)]
        [DataRow("éz-^&**", SedolValidationError.HasNonAlphaNumerics, false, NonAlphaNumeric)]
        [DataRow("éz-^&*ó", SedolValidationError.HasNonAlphaNumerics, false, NonAlphaNumeric)]
        [DataRow("aeuioae", SedolValidationError.HasVowels, false, AllVowels)]
        [DataRow("AEIOUAE", SedolValidationError.HasVowels, false, AllVowels)]
        [DataRow("9123457", SedolValidationError.InvalidCheckDigit, false, UserDefinedIncorrectCheckDigit)]
        [DataRow("9aBcDe6", SedolValidationError.InvalidCheckDigit, false, UserDefinedIncorrectCheckDigit)]
        [DataRow("1234567", SedolValidationError.InvalidCheckDigit, false, OldStyleIncorrectCheckDigit)]
        [DataRow("1234566", SedolValidationError.InvalidCheckDigit, false, OldStyleIncorrectCheckDigit)]
        [DataRow("B0WNLY8", SedolValidationError.InvalidCheckDigit, false, NewStyleIncorrectCheckDigit)]
        [DataRow("B1XH2C1", SedolValidationError.InvalidCheckDigit, false, NewStyleIncorrectCheckDigit)]
        public void Sedol_ConformanceTest_ValidationCorrect(string input, SedolValidationError error, bool isValid, string description)
        {
            var e = input.SedolValidate();
            Assert.AreEqual(error, e,
                $"input {input} ({description}) is expected to have validation error {error} but has {e}.");

            var status = isValid ? "valid" : "not valid";
            Assert.AreEqual(isValid, input.SedolIsValid(),
                $"input {input} ({description}) is expected to be {status}.");
        }

        /// <summary>
        /// Data taken from http://rosettacode.org/wiki/SEDOLs.
        /// </summary>
        [DataTestMethod]
        [DataRow("070995", '4', OldStyle)]
        [DataRow("710889", '9', OldStyle)]
        [DataRow("406566", '3', OldStyle)]
        [DataRow("228276", '5', OldStyle)]
        [DataRow("557910", '7', OldStyle)]
        [DataRow("585284", '2', OldStyle)]
        [DataRow("B0YBKJ", '7', NewStyle)]
        [DataRow("B0YBLH", '2', NewStyle)]
        [DataRow("B0YBKL", '9', NewStyle)]
        [DataRow("B0YBKR", '5', NewStyle)]
        [DataRow("B0YBKT", '7', NewStyle)]
        [DataRow("B00030", '0', NewStyle)]
        [DataRow("B0WNLY", '7', NewStyle)]
        [DataRow("B1XH2C", '0', NewStyle)]
        [DataRow("912345", '8', UserDefined)]
        [DataRow("9aBcDe", '1', UserDefined)]
        public void Sedol_ConformanceTest_CalculatedCheckDigitCorrect(string input, char expectedDigit, string description)
        {
            var actualDigit = input.SedolCalculateCheckDigit();
            Assert.AreEqual(expectedDigit, actualDigit,
                $"input {input} ({description}) is expected to have a check digit {expectedDigit} but has {actualDigit}.");
        }
    }
}
