using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// A generic output of a synthetic data generator.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public class SyntheticDataGeneratorOutput<T> : IValidatableObject
        where T : TemporalEntity
    {
        /// <summary>
        /// Gets or sets the text which identifies the synthetic data generator.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the text which identifies an instance of the synthetic data generator.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public string Moniker { get; set; }

        /// <summary>
        /// Gets or sets the generated data.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public T[] Data { get; set; }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Data != null && Data.Length == 0)
            {
                yield return new ValidationResult(
                    $"{nameof(Data)} array should have positive length.",
                    new[] { nameof(Data) });
            }
        }
    }
}
