using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mbs.Trading.Data.Generators.RepetitiveSample
{
    /// <summary>
    /// The input parameters for the repetitive sample generator.
    /// </summary>
    public class RepetitiveSampleGeneratorParameters : IValidatableObject
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
        /// Gets or sets the number of samples before the very first waveform sample.
        /// The value of zero means the waveform starts immediately.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.OffsetSamples)]
        [Range(0, int.MaxValue, ErrorMessage = ErrorMessages.FieldMustNotBeNegative)]
        public int OffsetSamples { get; set; } = DefaultParameterValues.OffsetSamples;

        /// <summary>
        /// Gets or sets the number of repetitions of the waveform. The value of zero means infinite.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.RepetitionsCount)]
        [Range(0, int.MaxValue, ErrorMessage = ErrorMessages.FieldMustNotBeNegative)]
        public int RepetitionsCount { get; set; } = DefaultParameterValues.RepetitionsCount;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
