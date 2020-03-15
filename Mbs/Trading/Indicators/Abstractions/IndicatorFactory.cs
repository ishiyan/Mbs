using System;
using System.Collections.Generic;
using Mbs.Trading.Indicators.GeraldGoertzel;
using Mbs.Trading.Indicators.JohnBollinger;
using Mbs.Trading.Indicators.Statistics;

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

                case IndicatorType.BollingerBands:
                {
                    if (input.Parameters is BollingerBands.Parameters parameters)
                        return new BollingerBands(parameters, input.OutputKinds);
                    throw InvalidParametersType(input, typeof(BollingerBands));
                }

                case IndicatorType.Variance:
                {
                    if (input.Parameters is Variance.Parameters parameters)
                        return new Variance(parameters);
                    throw InvalidParametersType(input, typeof(Variance));
                }

                case IndicatorType.StandardDeviation:
                {
                    if (input.Parameters is StandardDeviation.Parameters parameters)
                        return new StandardDeviation(parameters);
                    throw InvalidParametersType(input, typeof(StandardDeviation));
                }

                case IndicatorType.GoertzelSpectrum:
                {
                    if (input.Parameters is GoertzelSpectrum.Parameters parameters)
                        return new GoertzelSpectrum(parameters, input.OutputKinds);
                    throw InvalidParametersType(input, typeof(GoertzelSpectrum));
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
