using Mbs.Trading.Currencies;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// An account currency position.
    /// </summary>
    public class AccountCurrencyPosition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountCurrencyPosition"/> class.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <param name="balance">The balance in the original currency.</param>
        internal AccountCurrencyPosition(CurrencyCode currency, double balance = 0)
        {
            Currency = currency;
            Balance = balance;
        }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        public CurrencyCode Currency { get; }

        /// <summary>
        /// Gets the balance in the original currency.
        /// </summary>
        public double Balance { get; internal set; }
    }
}
