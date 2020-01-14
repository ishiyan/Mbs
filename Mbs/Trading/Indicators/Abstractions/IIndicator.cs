using System.Collections.Generic;
using Mbs.Trading.Data;

namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// An indicator interface.
    /// </summary>
    public interface IIndicator
    {
        /// <summary>
        /// Resets the indicator.
        /// </summary>
        void Reset();

        /// <summary>
        /// If indicator is primed.
        /// </summary>
        bool IsPrimed { get; }

        /// <summary>
        /// Describes output data of an indicator.
        /// </summary>
        IndicatorMetadata Metadata { get; }

        /// <summary>
        /// Updates the value of the indicator given the next <see cref="Scalar"/> sample.
        /// </summary>
        /// <param name="sample">The next sample to update the indicator.</param>
        /// <returns>The updated data.</returns>
        IndicatorOutput Update(Scalar sample);

        /// <summary>
        /// Updates the value of the indicator given the next <see cref="Ohlcv"/> sample.
        /// </summary>
        /// <param name="sample">The next sample to update the indicator.</param>
        /// <returns>The updated data.</returns>
        IndicatorOutput Update(Ohlcv sample);

        /// <summary>
        /// Updates the value of the indicator given the next sequence of <see cref="Scalar"/> samples.
        /// </summary>
        /// <param name="samples">>The next samples to update the indicator.</param>
        /// <returns>The updated data.</returns>
        IEnumerable<IndicatorOutput> Update(IEnumerable<Scalar> samples);

        /// <summary>
        /// Updates the value of the indicator given the next sequence of <see cref="Ohlcv"/> samples.
        /// </summary>
        /// <param name="samples">>The next samples to update the indicator.</param>
        /// <returns>The updated data.</returns>
        IEnumerable<IndicatorOutput> Update(IEnumerable<Ohlcv> samples);
    }
}
