using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mbs.Numerics.Random;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// The input parameters for the waveform generators.
    /// A waveform generator produces samples with an optional noise.
    /// </summary>
    public class WaveformParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the number of samples in the waveform.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.WaveformSamples)]
        [Range(4, int.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositiveMinValue4)]
        public int WaveformSamples { get; set; } = DefaultParameterValues.WaveformSamples;

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

        /// <summary>
        /// Gets or sets the amplitude of the noise as a fraction of the sample value.
        /// The noise will be not produced if the <see cref="NoiseAmplitudeFraction"/> is less than <see cref="double.Epsilon"/>.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.NoiseAmplitudeFraction)]
        [Range(0.0, 1.0, ErrorMessage = ErrorMessages.FieldMustBeInRange01)]
        public double NoiseAmplitudeFraction { get; set; } = DefaultParameterValues.NoiseAmplitudeFraction;

        /// <summary>
        /// Gets or sets the kind of an uniform random generator to produce the noise.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [EnumDataType(typeof(UniformRandomGeneratorKind), ErrorMessage = ErrorMessages.FieldEnumValueInvalid)]
        [DefaultValue(DefaultParameterValues.NoiseUniformRandomGeneratorKind)]
        public UniformRandomGeneratorKind NoiseUniformRandomGeneratorKind { get; set; } = DefaultParameterValues.NoiseUniformRandomGeneratorKind;

        /// <summary>
        /// Gets or sets the seed of a random generator to produce the noise.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.NoiseUniformRandomGeneratorSeed)]
        public int NoiseUniformRandomGeneratorSeed { get; set; } = DefaultParameterValues.NoiseUniformRandomGeneratorSeed;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
