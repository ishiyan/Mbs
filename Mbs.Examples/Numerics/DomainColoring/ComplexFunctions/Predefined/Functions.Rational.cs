using System.Collections.Generic;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of rational complex functions.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetRational()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = "z", TexKey = "Rational|Z1", Function = z => z,
                },
            };
        }
    }
}
