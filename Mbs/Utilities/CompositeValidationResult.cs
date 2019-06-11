using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#pragma warning disable 1591

namespace Mbs
{
    /// <inheritdoc />
    public class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> results = new List<ValidationResult>();

        public IEnumerable<ValidationResult> Results => results;

        public CompositeValidationResult(string errorMessage)
            : base(errorMessage)
        {
        }

        public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames)
            : base(errorMessage, memberNames)
        {
        }

        protected CompositeValidationResult(ValidationResult validationResult)
            : base(validationResult)
        {
        }

        public void AddResult(ValidationResult validationResult)
        {
            results.Add(validationResult);
        }
    }
}
