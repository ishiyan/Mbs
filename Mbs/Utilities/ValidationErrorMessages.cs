namespace Mbs
{
    /// <summary>
    /// Error messages used in parameter validation.
    /// </summary>
    internal static class ValidationErrorMessages
    {
        /// <summary>
        /// The field is required.
        /// </summary>
        internal const string FieldIsRequired = "The field {0} is required.";

        /// <summary>
        /// The validation of a field failed.
        /// </summary>
        internal const string ValidationOfFieldFailed = "Validation for {0} failed.";

        /// <summary>
        /// The validation of an array field failed.
        /// </summary>
        internal const string ValidationOfArrayFieldFailed = "Validation for {0}[{1}] failed.";

        /// <summary>
        /// The field must be positive.
        /// </summary>
        internal const string FieldMustBePositive = "The field {0} must be positive.";

        /// <summary>
        /// The field must not be negative.
        /// </summary>
        internal const string FieldMustNotBeNegative = "The field {0} must not be negative.";

        /// <summary>
        /// The field must be in range [0, 1].
        /// </summary>
        internal const string FieldMustBeInRange01 = "The field {0} must be in range [0, 1].";

        /// <summary>
        /// The field must be in range [-1, 1].
        /// </summary>
        internal const string FieldMustBeInRangeMin1Plus1 = "The field {0} must be in range [-1, 1].";

        /// <summary>
        /// The field value doesn't exist within the enum.
        /// </summary>
        internal const string FieldEnumValueInvalid = "The field {0} value doesn't exist within the enum.";
    }
}