using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mbs.Trading.Data.Generators.Chirp
{
    /// <summary>
    /// The input parameters for the chirp generator.
    /// </summary>
    public class ChirpParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the amplitude of the chirp, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.ChirpAmplitude)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double Amplitude { get; set; } = DefaultParameterValues.ChirpAmplitude;

        /// <summary>
        /// Gets or sets the value corresponding to the minimum of the chirp, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.ChirpMinimalValue)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double MinimalValue { get; set; } = DefaultParameterValues.ChirpMinimalValue;

        /// <summary>
        /// Gets or sets the value corresponding to the instantaneous initial period of the chirp in samples, should be ≥ 2.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.ChirpInitialPeriod)]
        [Range(2.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositiveMinValue2)]
        public double InitialPeriod { get; set; } = DefaultParameterValues.ChirpInitialPeriod;

        /// <summary>
        /// Gets or sets the value corresponding to the instantaneous final period of the chirp in samples, should be ≥ 2.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.ChirpFinalPeriod)]
        [Range(2.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositiveMinValue2)]
        public double FinalPeriod { get; set; } = DefaultParameterValues.ChirpFinalPeriod;

        /// <summary>
        /// Gets or sets the value corresponding to the initial phase, φ, of the chirp in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.ChirpPhaseInPi)]
        [Range(-1.0, 1.0, ErrorMessage = ErrorMessages.FieldMustBeInRangeMin1Plus1)]
        public double PhaseInPi { get; set; } = DefaultParameterValues.ChirpPhaseInPi;

        /// <summary>
        /// Gets or sets a value indicating whether the period of even chirps descends from the final period to the initial one, to form a symmetrical shape with odd chirps.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.ChirpIsBiDirectional)]
        public bool IsBiDirectional { get; set; } = DefaultParameterValues.ChirpIsBiDirectional;

        /// <summary>
        /// Gets or sets the chirp sweep.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.ChirpSweep)]
        public ChirpSweep ChirpSweep { get; set; } = DefaultParameterValues.ChirpSweep;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
