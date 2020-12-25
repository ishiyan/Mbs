using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics
{
    /// <summary>
    /// Provides basic arithmetic of Chebyshev series of specified order over a specified interval.
    /// </summary>
    public sealed class ChebyshevSeries
    {
        /// <summary>
        /// The coefficients of a Chebyshev series.
        /// </summary>
        private readonly double[] coefficients;

        /// <summary>
        /// The order of expansion of a Chebyshev series.
        /// </summary>
        private readonly int orderOfExpansion;

        /// <summary>
        /// The upper point of an interval of a Chebyshev series.
        /// </summary>
        private readonly double upperIntervalPoint;

        /// <summary>
        /// The lower point of an interval of a Chebyshev series.
        /// </summary>
        private readonly double lowerIntervalPoint;

        private readonly double interval;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChebyshevSeries"/> class.
        /// </summary>
        /// <param name="lowerIntervalPoint">The lower point of an interval of a Chebyshev series.</param>
        /// <param name="upperIntervalPoint">The upper point of an interval of a Chebyshev series.</param>
        /// <param name="orderOfExpansion">The order of expansion of a Chebyshev series.</param>
        /// <param name="coefficients">An array of real coefficients. The length of the array should be <c>orderOfExpansion + 1</c>.</param>
        public ChebyshevSeries(double lowerIntervalPoint, double upperIntervalPoint, int orderOfExpansion, double[] coefficients)
        {
            if (lowerIntervalPoint >= upperIntervalPoint)
            {
                throw new ArgumentException("The lower interval point should be less than the upper one.");
            }

            if (coefficients == null)
            {
                throw new ArgumentException("No coefficients are specified.");
            }

            if (coefficients.Length != orderOfExpansion + 1)
            {
                throw new ArgumentException("The number of coefficients should be equal to the orderOfExpansion plus one.");
            }

            this.lowerIntervalPoint = lowerIntervalPoint;
            this.upperIntervalPoint = upperIntervalPoint;
            interval = upperIntervalPoint - lowerIntervalPoint;
            this.orderOfExpansion = orderOfExpansion;
            this.coefficients = coefficients;
        }

        /// <summary>
        /// Evaluates the Chebyshev series at a given point <c>x</c>.
        /// </summary>
        /// <param name="x">An argument to evaluate the Chebyshev series at.</param>
        /// <returns>The value of the evaluated Chebyshev series.</returns>
        public double Evaluate(double x)
        {
            return EvaluateOrder(orderOfExpansion, x);
        }

        /// <summary>
        /// Evaluates the Chebyshev series at a given point to (at most) the given order.
        /// </summary>
        /// <param name="order">The order to evaluate to.</param>
        /// <param name="x">An argument to evaluate the Chebyshev series at.</param>
        /// <returns>The value of the evaluated Chebyshev series.</returns>
        public double Evaluate(int order, double x)
        {
            return EvaluateOrder(Math.Min(order, orderOfExpansion), x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double EvaluateOrder(int order, double x)
        {
            double y = (2.0 * x - lowerIntervalPoint - upperIntervalPoint) / interval;
            double y2 = 2.0 * y;
            double d1 = 0.0, d2 = 0.0;
            for (int i = order; i > 0; --i)
            {
                double temp = d1;
                d1 = y2 * d1 - d2 + coefficients[i];
                d2 = temp;
            }

            return y * d1 - d2 + 0.5 * coefficients[0];
        }
    }
}
