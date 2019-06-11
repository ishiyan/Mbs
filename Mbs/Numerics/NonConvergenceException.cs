using System;
using System.Globalization;

namespace Mbs.Numerics
{
#pragma warning disable CA1032 // Implement standard exception constructors
    /// <summary>
    /// The exception that is thrown when an algorithm fails to converge.
    /// </summary>
    public sealed class NonConvergenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NonConvergenceException"/> class.
        /// </summary>
        public NonConvergenceException()
            : base("Algorithm fails to converge.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonConvergenceException"/> class.
        /// </summary>
        /// <param name="iterationCount">The number of iterations.</param>
        public NonConvergenceException(int iterationCount)
            : base($"Algorithm fails to converge after {iterationCount} iterations.".ToString(CultureInfo.InvariantCulture))
        {
        }
    }
}
