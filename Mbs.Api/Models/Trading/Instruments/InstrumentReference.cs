using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Mbs.Api.Models.Trading.Instruments
{
    /// <summary>
    /// Contains information to reference an instrument.
    /// </summary>
    [DataContract]
    public class InstrumentReference : IValidatableObject
    {
        /// <summary>
        /// Gets or sets an exchange representations according to ISO 10383 Market Identifier Code (MIC).
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Mic { get; set; }

        /// <summary>
        /// Gets or sets an ISIN.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Isin { get; set; }

        /// <summary>
        /// Gets or sets the optional symbol (ticker) of the security.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets a name of the instrument.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
