using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Mbs.Utilities
{
    /// <summary>
    /// Validates an object.
    /// </summary>
    public class ValidateObjectAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value, null, null);

            Validator.TryValidateObject(value, context, results, true);
            if (results.Count <= 0)
            {
                return ValidationResult.Success;
            }

            var compositeResults = new CompositeValidationResult(
                string.Format(
                    CultureInfo.InvariantCulture,
                    ValidationErrorMessages.ValidationOfFieldFailed,
                    validationContext.DisplayName),
                new[] { validationContext.DisplayName });

            foreach (var result in results)
            {
                compositeResults.AddResult(result);
            }

            return compositeResults;
        }
    }
}