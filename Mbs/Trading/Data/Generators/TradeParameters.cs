using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// The <see cref="Trade"/> related input parameters for the waveform generators.
    /// A waveform generator produces trades with a constant volume.
    /// </summary>
    public class TradeParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the value of the volume, which is the same for all trades; should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.TradeVolume)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double Volume { get; set; } = DefaultParameterValues.TradeVolume;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
