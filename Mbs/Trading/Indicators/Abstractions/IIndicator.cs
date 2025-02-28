﻿using System.Collections.Generic;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// An indicator interface.
    /// </summary>
    public interface IIndicator
    {
        /// <summary>
        /// Gets a value indicating whether an indicator is primed.
        /// </summary>
        bool IsPrimed { get; }

        /// <summary>
        /// Gets description of output data of an indicator.
        /// </summary>
        IndicatorMetadata Metadata { get; }

        /// <summary>
        /// Resets the indicator.
        /// </summary>
        void Reset();

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
