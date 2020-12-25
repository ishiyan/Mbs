﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Generators.Chirp
{
    /// <summary>
    /// The input parameters for the <see cref="Scalar"/> chirp generator.
    /// </summary>
    public class ChirpScalarGeneratorParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the number of samples to generate.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [Range(2, int.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositiveMinValue2)]
        public int SampleCount { get; set; } = DefaultParameterValues.SamplesCount;

        /// <summary>
        /// Gets or sets the time related input parameters.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [ValidateObject]
        public TimeParameters TimeParameters { get; set; } = new TimeParameters();

        /// <summary>
        /// Gets or sets the waveform related input parameters.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [ValidateObject]
        public WaveformParameters WaveformParameters { get; set; } = new WaveformParameters();

        /// <summary>
        /// Gets or sets the chirp related input parameters.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [ValidateObject]
        public ChirpParameters ChirpParameters { get; set; } = new ChirpParameters();

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
