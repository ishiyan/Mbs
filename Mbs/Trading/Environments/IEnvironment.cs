using Mbs.Trading.Brokers;
using Mbs.Trading.Brokers.Commissions;
using Mbs.Trading.Brokers.Slippages;
using Mbs.Trading.Currencies;
using Mbs.Trading.Data;
using Mbs.Trading.Time.Timepieces;

namespace Mbs.Trading.Environments
{
    /// <summary>
    /// Provides a trading environment for a live or a simulated trading.
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// Gets a data publisher interface.
        /// </summary>
        IDataPublisher DataPublisher { get; }

        /// <summary>
        /// Gets a timepiece interface.
        /// </summary>
        ITimepiece Timepiece { get; }

        /// <summary>
        /// Gets a broker interface.
        /// </summary>
        IBroker Broker { get; }

        /// <summary>
        /// Gets a broker commission interface.
        /// </summary>
        ICommission Commission { get; }

        /// <summary>
        /// Gets a broker slippage interface.
        /// </summary>
        ISlippage Slippage { get; }

        /// <summary>
        /// Gets a currency converter interface.
        /// </summary>
        ICurrencyConverter CurrencyConverter { get; }
    }
}
