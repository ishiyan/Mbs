using System.Collections.Generic;

namespace DomainColoring.ComplexFunctions
{
    internal sealed class Category
    {
        public string Label { get; set; }

        public IEnumerable<ComplexFunction> Functions { get; set; }
    }
}
