using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;

namespace Mbs.Api.Models.Trading.Instruments
{
    /// <summary>
    /// Contains information to describe an instrument.
    /// </summary>
    [DataContract]
    public class Instrument : IValidatableObject
    {
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

        /// <summary>
        /// Gets or sets an optional textual description for the instrument.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the instrument type.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        [DefaultValue(InstrumentType.Undefined)]
        public InstrumentType Type { get; set; } = InstrumentType.Undefined;

        /// <summary>
        /// Gets or sets an exchange representations according to ISO 10383 Market Identifier Code (MIC).
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        [DefaultValue(ExchangeMic.Xxxx)]
        public ExchangeMic Mic { get; set; } = ExchangeMic.Xxxx;

        /// <summary>
        /// Gets or sets an ISIN.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Isin { get; set; }

        /// <summary>
        /// Gets or sets an additional information for stocks.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public Stock Stock { get; set; }

        /// <summary>
        /// Gets or sets an additional information for Exchange-Traded Vehicles.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public Etv Etv { get; set; }

        /// <summary>
        /// Gets or sets an additional information for Exchange-Traded Funds.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public Etf Etf { get; set; }

        /// <summary>
        /// Gets or sets an additional information for Indicative Net Asset Values.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public Inav Inav { get; set; }

        /// <summary>
        /// Gets or sets an additional information for funds.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public Fund Fund { get; set; }

        /// <summary>
        /// Gets or sets an additional information for indices.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public Index Index { get; set; }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Stock != null && Type != InstrumentType.Stock)
            {
                yield return new ValidationResult($"The Stock field is not allowed when the type is {Type}.", new[] { "Type", "Stock" });
            }

            if (Etv != null && Type != InstrumentType.Etv)
            {
                yield return new ValidationResult($"The Etv field is not allowed when the type is {Type}.", new[] { "Type", "Etv" });
            }

            if (Etf != null && Type != InstrumentType.Etf)
            {
                yield return new ValidationResult($"The Etf field is not allowed when the type is {Type}.", new[] { "Type", "Etf" });
            }

            if (Inav != null && Type != InstrumentType.Inav)
            {
                yield return new ValidationResult($"The Inav field is not allowed when the type is {Type}.", new[] { "Type", "Inav" });
            }

            if (Fund != null && Type != InstrumentType.Fund)
            {
                yield return new ValidationResult($"The Fund field is not allowed when the type is {Type}.", new[] { "Type", "Fund" });
            }

            if (Index != null && Type != InstrumentType.Index)
            {
                yield return new ValidationResult($"The Index field is not allowed when the type is {Type}.", new[] { "Type", "Index" });
            }
        }
    }
}
