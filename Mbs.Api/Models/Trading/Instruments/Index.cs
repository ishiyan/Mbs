using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Mbs.Trading.Currencies;

namespace Mbs.Api.Models.Trading.Instruments
{
    /// <summary>
    /// An additional information for indices.
    /// </summary>
    [DataContract]
    public class Index : IValidatableObject
    {
        /// <summary>
        /// Gets or sets a currency code.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public CurrencyCode Currency { get; set; } = CurrencyCode.Xxx;

        /// <summary>
        /// Gets or sets a kind of an index.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets a family of an index.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets a calculation frequency of an index.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string CalculationFrequency { get; set; }

        /// <summary>
        /// Gets or sets a weighting of an index.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Weighting { get; set; }

        /// <summary>
        /// Gets or sets a capping factor of an index.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string CappingFactor { get; set; }

        /// <summary>
        /// Gets or sets an Industry Classification Benchmark code.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Icb { get; set; }

        /// <summary>
        /// Gets or sets a base date of an index.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string BaseDate { get; set; }

        /// <summary>
        /// Gets or sets a base level of an index.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string BaseLevel { get; set; }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
