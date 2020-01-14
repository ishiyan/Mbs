using System.Collections.ObjectModel;

namespace Mbs.Trading.Indicators2.Abstractions
{
    /// <summary>
    /// A band indicator with over-writable history interface.
    /// </summary>
    public interface IBandIndicatorWithOverWritableHistory : IBandIndicator
    {
        /// <summary>
        /// Historical values of the indicator.
        /// </summary>
        ReadOnlyCollection<Band> HistoricalValues { set; }
    }
}
