using System;
using System.Collections.Generic;

namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// A factory to create indicator instances.
    /// </summary>
    public static class IndicatorFactory
    {
        /// <summary>
        /// Creates a collection of indicator instances.
        /// </summary>
        /// <param name="inputs">An input data to create indicators.</param>
        /// <returns>A collection of indicator instances.</returns>
        public static IEnumerable<IIndicator> Create(IEnumerable<IndicatorInput> inputs)
        {
            var list = new List<IIndicator>();
            foreach (var input in inputs)
            {
                list.Add(Create(input));
            }

            return list;
        }

        /// <summary>
        /// Creates an instance of an indicator.
        /// </summary>
        /// <param name="input">An input data to create an indicator.</param>
        /// <returns>An instance of the indicator.</returns>
        public static IIndicator Create(IndicatorInput input)
        {
            switch (input.IndicatorType)
            {
                case IndicatorType.SimpleMovingAverage:
                {
                    if (input.Parameters is SimpleMovingAverage.Parameters parameters)
                        return new SimpleMovingAverage(parameters);
                    throw InvalidParametersType(input, typeof(SimpleMovingAverage));
                }

                case IndicatorType.ExponentialMovingAverage:
                {
                    if (input.Parameters is ExponentialMovingAverage.ParametersLength parametersLength)
                        return new ExponentialMovingAverage(parametersLength);
                    if (input.Parameters is ExponentialMovingAverage.ParametersSmoothingFactor parametersSmoothingFactor)
                        return new ExponentialMovingAverage(parametersSmoothingFactor);
                    throw InvalidParametersType(input, typeof(ExponentialMovingAverage));
                }

                default:
                    throw new ArgumentException($"Creation of {input.IndicatorType} indicator type is not supported.", nameof(input.IndicatorType));
            }
        }

        private static ArgumentException InvalidParametersType(IndicatorInput input, Type indicatorType)
        {
            return new ArgumentException(
                $"Invalid parameters type {input.Parameters.GetType().FullName} for {indicatorType.Name} indicator.");
        }
    }
}
