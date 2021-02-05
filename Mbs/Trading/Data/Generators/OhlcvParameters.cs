using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// The <see cref="Ohlcv"/> related input parameters for the waveform generators.
    /// An <see cref="Ohlcv"/> waveform generator produces candlesticks with the following traits.
    /// <para/>➊ The opening and closing prices are equidistant from the mid price.
    /// <para/>The half-length of the candlestick body is defined as a ratio ∈[0, 1].
    /// <para/>When the ratio is 1, the lower price is zero and the higher price is twice the mid price.
    /// <para/>When the ratio is 0, the both opening and closing prices are equal to the the mid price.
    /// <para/>➋ The highest and the lowest prices are equidistant from the mid price.
    /// <para/>The length of the candlestick shadows is defined as a ratio ∈[0, 1].
    /// <para/>When the ratio is 1, the lowest price is zero and the highest price is twice the mid price.
    /// <para/>When the ratio is 0, the both lowest and highest prices are equal to the the mid price.
    /// <para/>➌ The volume has a constant value.
    /// </summary>
    public class OhlcvParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the shadow fraction, ρs, which determines the length of the candlestick shadows as a fraction of the mid price; ρs∈[0, 1].
        /// The value should be greater or equal to the candlestick body fraction.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.CandlestickShadowFraction)]
        [Range(0.0, 1.0, ErrorMessage = ErrorMessages.FieldMustBeInRange01)]
        public double CandlestickShadowFraction { get; set; } = DefaultParameterValues.CandlestickShadowFraction;

        /// <summary>
        /// Gets or sets the body fraction, ρb, which determines the half-length of the candlestick body as a fraction of the mid price; ρb∈[0, 1].
        /// The value should be less or equal to the candlestick shadow fraction.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.CandlestickBodyFraction)]
        [Range(0.0, 1.0, ErrorMessage = ErrorMessages.FieldMustBeInRange01)]
        public double CandlestickBodyFraction { get; set; } = DefaultParameterValues.CandlestickBodyFraction;

        /// <summary>
        /// Gets or sets the value of the volume, which is the same for all candlesticks; should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.CandlestickVolume)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustNotBeNegative)]
        public double Volume { get; set; } = DefaultParameterValues.CandlestickVolume;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CandlestickShadowFraction < CandlestickBodyFraction)
            {
                yield return new ValidationResult(
                    $"{nameof(CandlestickBodyFraction)} {CandlestickBodyFraction} should be less or equal than the {nameof(CandlestickShadowFraction)} {CandlestickShadowFraction}.",
                    new[] { nameof(CandlestickBodyFraction), nameof(CandlestickShadowFraction) });
            }
        }
    }
}
