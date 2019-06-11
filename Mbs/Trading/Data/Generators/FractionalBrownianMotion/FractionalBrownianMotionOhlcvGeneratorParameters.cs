using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mbs.Numerics;
using Mbs.Numerics.Random;

namespace Mbs.Trading.Data.Generators.FractionalBrownianMotion
{
    /// <summary>
    /// The input parameters for the <see cref="Ohlcv"/> fractional Brownian motion generator.
    /// </summary>
    public class FractionalBrownianMotionOhlcvGeneratorParameters : IValidatableObject
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
        /// Gets or sets the fractal Brownian motion related input parameters.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [ValidateObject]
        public FractionalBrownianMotionParameters FbmParameters { get; set; } = new FractionalBrownianMotionParameters();

        /// <summary>
        /// Gets or sets the <see cref="Ohlcv"/> related input parameters.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [ValidateObject]
        public OhlcvParameters OhlcvParameters { get; set; } = new OhlcvParameters();

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FbmParameters == null || FbmParameters.Algorithm == FractionalBrownianMotionAlgorithm.Hosking ||
                WaveformParameters == null || ElementaryFunctions.IsPowerOfTwo(WaveformParameters.WaveformSamples))
                yield break;

            yield return new ValidationResult(
                ErrorMessages.WaveformParametersWaveformSamplesPowerOfTwo,
                new[] { ErrorMessages.WaveformParametersWaveformSamples });
        }
    }
}
