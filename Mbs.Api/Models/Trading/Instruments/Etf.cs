using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Mbs.Trading.Currencies;

namespace Mbs.Api.Models.Trading.Instruments
{
    /// <summary>
    /// An additional information for Exchange-Traded Funds.
    /// </summary>
    [DataContract]
    public class Etf : IValidatableObject
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
        /// Gets or sets a dividend frequency.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string DividendFrequency { get; set; }

        /// <summary>
        /// Gets or sets an exposition type.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string ExpositionType { get; set; }

        /// <summary>
        /// Gets or sets a fraction.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Fraction { get; set; }

        /// <summary>
        /// Gets or sets a total expence ratio percentage.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string TotalExpenseRatio { get; set; }

        /// <summary>
        /// Gets or sets an index family.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string IndexFamily { get; set; }

        /// <summary>
        /// Gets or sets a launch date.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string LaunchDate { get; set; }

        /// <summary>
        /// Gets or sets an issuer.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets an Indicative Net Asset Value instrument reference.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public InstrumentReference Inav { get; set; }

        /// <summary>
        /// Gets or sets an underlying instrument reference.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public InstrumentReference Underlying { get; set; }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
