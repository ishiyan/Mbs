using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mbs.Trading.Data.Generators.Square
{
    /// <summary>
    /// The input parameters for the square impulse generator.
    /// </summary>
    public class SquareParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the amplitude of the square impulse, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SquareAmplitude)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double Amplitude { get; set; } = DefaultParameterValues.SquareAmplitude;

        /// <summary>
        /// Gets or sets the value corresponding to the minimum of square impulse, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SquareMinimalValue)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double MinimalValue { get; set; } = DefaultParameterValues.SquareMinimalValue;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
