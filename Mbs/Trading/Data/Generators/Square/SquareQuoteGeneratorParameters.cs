﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mbs.Trading.Data.Entities;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Generators.Square
{
    /// <summary>
    /// The input parameters for the <see cref="Quote"/> square impulse generator.
    /// </summary>
    public class SquareQuoteGeneratorParameters : IValidatableObject
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
        /// Gets or sets the square impulse related input parameters.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [ValidateObject]
        public SquareParameters SquareParameters { get; set; } = new SquareParameters();

        /// <summary>
        /// Gets or sets the <see cref="Quote"/> related input parameters.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [ValidateObject]
        public QuoteParameters QuoteParameters { get; set; } = new QuoteParameters();

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
