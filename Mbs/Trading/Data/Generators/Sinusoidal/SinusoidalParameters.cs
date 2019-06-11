using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mbs.Trading.Data.Generators.Sinusoidal
{
    /// <summary>
    /// The input parameters for the sinusoidal generator.
    /// </summary>
    public class SinusoidalParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the amplitude of the sinusoid, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SinusoidalAmplitude)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double Amplitude { get; set; } = DefaultParameterValues.SinusoidalAmplitude;

        /// <summary>
        /// Gets or sets the value corresponding to the minimum of the sinusoid, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SinusoidalMinimalValue)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double MinimalValue { get; set; } = DefaultParameterValues.SinusoidalMinimalValue;

        /// <summary>
        /// Gets or sets a value corresponding to the period of the sinusoid in samples, should be ≥ 2.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SinusoidalPeriod)]
        [Range(2.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositiveMinValue2)]
        public double Period { get; set; } = DefaultParameterValues.SinusoidalPeriod;

        /// <summary>
        /// Gets or sets a value corresponding to the phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SinusoidalPhaseInPi)]
        [Range(-1.0, 1.0, ErrorMessage = ErrorMessages.FieldMustBeInRangeMin1Plus1)]
        public double PhaseInPi { get; set; } = DefaultParameterValues.SinusoidalPhaseInPi;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
