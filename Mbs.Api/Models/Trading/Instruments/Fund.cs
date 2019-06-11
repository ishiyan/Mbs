using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Mbs.Trading.Currencies;

namespace Mbs.Api.Models.Trading.Instruments
{
    /// <summary>
    /// An additional information for funds.
    /// </summary>
    [DataContract]
    public class Fund : IValidatableObject
    {
        /// <summary>
        /// Gets or sets a currency code.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public CurrencyCode Currency { get; set; } = CurrencyCode.Xxx;

        /// <summary>
        /// Gets or sets a trading mode.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string TradingMode { get; set; }

        /// <summary>
        /// Gets or sets an ISO 10962 Classification of Financial Instruments code.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Cfi { get; set; }

        /// <summary>
        /// Gets or sets an issuer.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets a number of shares outstanding.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long SharesOutstanding { get; set; }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
