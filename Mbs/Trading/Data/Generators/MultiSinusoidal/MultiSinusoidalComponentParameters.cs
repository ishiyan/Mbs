using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mbs.Trading.Data.Generators.MultiSinusoidal
{
    /// <summary>
    /// The input parameters for an individual component of the multi-sinusoidal generator.
    /// </summary>
    public class MultiSinusoidalComponentParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the amplitude of the sinusoidal component, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.MultiSinusoidalAmplitude)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double Amplitude { get; set; } = DefaultParameterValues.MultiSinusoidalAmplitude;

        /// <summary>
        /// Gets or sets a value corresponding to the period of the sinusoidal component in samples, should be ≥ 2.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.MultiSinusoidalPeriod)]
        [Range(2.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositiveMinValue2)]
        public double Period { get; set; } = DefaultParameterValues.MultiSinusoidalPeriod;

        /// <summary>
        /// Gets or sets a value corresponding to the phase, φ, of the sinusoidal component in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.MultiSinusoidalPhaseInPi)]
        [Range(-1.0, 1.0, ErrorMessage = ErrorMessages.FieldMustBeInRangeMin1Plus1)]
        public double PhaseInPi { get; set; } = DefaultParameterValues.MultiSinusoidalPhaseInPi;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
