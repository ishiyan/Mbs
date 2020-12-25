using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Mbs.Numerics
{
    /// <summary>
    /// The exception that is thrown when an algorithm fails to converge.
    /// </summary>
    [Serializable]
    public class NonConvergenceException : Exception
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

        protected NonConvergenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
