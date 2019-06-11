using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mbs.Trading.Data.Generators.Sawtooth
{
    /// <summary>
    /// The input parameters for the sawtooth impulse generator.
    /// </summary>
    public class SawtoothParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the amplitude of the sawtooth impulse, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SawtoothAmplitude)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double Amplitude { get; set; } = DefaultParameterValues.SawtoothAmplitude;

        /// <summary>
        /// Gets or sets the value corresponding to the minimum of the sawtooth impulse, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SawtoothMinimalValue)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double MinimalValue { get; set; } = DefaultParameterValues.SawtoothMinimalValue;

        /// <summary>
        /// Gets or sets a value indicating whether the sawtooth impulse is reflected horizontally to form a triangle shape.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SawtoothIsBiDirectional)]
        public bool IsBiDirectional { get; set; } = DefaultParameterValues.SawtoothIsBiDirectional;

        /// <summary>
        /// Gets or sets the sawtooth shape.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SawtoothShape)]
        public SawtoothShape Shape { get; set; } = DefaultParameterValues.SawtoothShape;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
