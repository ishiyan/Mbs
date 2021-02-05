using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// The <see cref="Quote"/> related input parameters for the waveform generators.
    /// A waveform generator produces quotes with the following traits.
    /// <para/>➊ The ask and bid prices are equidistant from the mid price.
    /// <para/>The half-length of the spread is defined as a ratio ∈[0, 1].
    /// <para/>When the ratio is 1, the bid price is zero and the ask price is twice the mid price.
    /// <para/>When the ratio is 0, the both ask and bid prices are equal to the the mid price.
    /// <para/>➋ The ask and bid sizes are constant values.
    /// </summary>
    public class QuoteParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the spread fraction, ρs, which determines the ask and bid prices as a fraction of the mid price.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SpreadFraction)]
        [Range(0.0, 1.0, ErrorMessage = ErrorMessages.FieldMustBeInRange01)]
        public double SpreadFraction { get; set; } = DefaultParameterValues.SpreadFraction;

        /// <summary>
        /// Gets or sets the value of the ask size, which is the same for all quotes; should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.AskSize)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double AskSize { get; set; } = DefaultParameterValues.AskSize;

        /// <summary>
        /// Gets or sets the value of the bid size, which is the same for all quotes; should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.BidSize)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double BidSize { get; set; } = DefaultParameterValues.BidSize;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
