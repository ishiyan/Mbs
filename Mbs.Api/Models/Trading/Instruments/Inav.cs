using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Mbs.Trading.Currencies;

namespace Mbs.Api.Models.Trading.Instruments
{
    /// <summary>
    /// An additional information for Indicative Net Asset Values.
    /// </summary>
    [DataContract]
    public class Inav : IValidatableObject
    {
        /// <summary>
        /// Gets or sets a currency code.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public CurrencyCode Currency { get; set; } = CurrencyCode.Xxx;

        /// <summary>
        /// Gets or sets a target instrument reference.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public InstrumentReference Target { get; set; }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
