namespace Mbs.Trading.Instruments
{
    /// <summary>
    /// Enumerates SEDOL validation errors.
    /// </summary>
    public enum SedolValidationError
    {
        /// <summary>
        /// This is a valid SEDOL.
        /// </summary>
        None,

        /// <summary>
        /// The length should be 7 characters.
        /// </summary>
        InvalidLength,

        /// <summary>
        /// An old style SEDOL should contain only digits.
        /// </summary>
        OldStyleHasAlpha,

        /// <summary>
        /// Alpha-numeric characters should be 0-9, A-Z.
        /// </summary>
        HasNonAlphaNumerics,

        /// <summary>
        /// Alpha characters should not be vowels AEUIO.
        /// </summary>
        HasVowels,

        /// <summary>
        /// Trailing check digit is invalid.
        /// </summary>
        InvalidCheckDigit
    }
}
