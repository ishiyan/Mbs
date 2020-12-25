using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Generators.MultiSinusoidal
{
    /// <summary>
    /// The input parameters for the multi-sinusoidal generator.
    /// </summary>
    public class MultiSinusoidalParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the value corresponding to the minimum of the combined sinusoids, should be positive.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DefaultValue(DefaultParameterValues.SinusoidalMinimalValue)]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessages.FieldMustBePositive)]
        public double MinimalValue { get; set; } = DefaultParameterValues.MultiSinusoidalMinimalValue;

        /// <summary>
        /// Gets or sets a value corresponding to an array of the parameters for individual sinusoids.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public IEnumerable<MultiSinusoidalComponentParameters> MultiSinusoidalComponents { get; set; } = new[]
        {
            new MultiSinusoidalComponentParameters { Amplitude = 10, Period = 16, PhaseInPi = 0 },
        };

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var array = MultiSinusoidalComponents?.ToArray();
            if (array == null || array.Length < 1)
            {
                yield return new ValidationResult(
                    ErrorMessages.FieldMultiSinusoidalComponentsMustNotBeEmpty,
                    new[] { ErrorMessages.MultiSinusoidalComponents });
            }

            var results = new List<ValidationResult>();

            for (int i = 0; i < array?.Length; ++i)
            {
                var component = array[i];
                if (!Validator.TryValidateObject(component, new ValidationContext(component), results, true))
                {
                    var compositeResults = new CompositeValidationResult(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ValidationErrorMessages.ValidationOfArrayFieldFailed,
                            nameof(MultiSinusoidalComponents),
                            i),
                        new[] { $"{nameof(MultiSinusoidalComponents)}[{i}]" });

                    foreach (var result in results)
                    {
                        compositeResults.AddResult(result);
                    }

                    results.Clear();
                    yield return compositeResults;
                }
            }
        }
    }
}
