using System;
using System.Collections.ObjectModel;
using Mbs.Trading.Currencies;
using Mbs.Trading.Time;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// An account interface.
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// Gets the account portfolio.
        /// </summary>
        IPortfolio Portfolio { get; }

        /// <summary>
        /// Gets the currency converter.
        /// </summary>
        ICurrencyConverter CurrencyConverter { get; }

        /// <summary>
        /// Gets the time provider.
        /// </summary>
        ITimepiece Timepiece { get; }

        /// <summary>
        /// Gets the holder of this account.
        /// </summary>
        string Holder { get; }

        /// <summary>
        /// Gets the description of this account.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the home currency of this account.
        /// </summary>
        CurrencyCode Currency { get; }

        /// <summary>
        /// Gets the collection of currency positions (including the home currency, which is always the first one).
        /// </summary>
        ReadOnlyCollection<AccountCurrencyPosition> CurrencyPositions { get; }

        /// <summary>
        /// Gets the collection of the account transactions.
        /// </summary>
        ReadOnlyCollection<AccountTransaction> TransactionHistory { get; }

        /// <summary>
        /// Notifies when an account transaction has been added.
        /// </summary>
        event Action<Account, AccountTransaction> TransactionAdded;

        /// <summary>
        /// Notifies when an account has been changed.
        /// </summary>
        event Action<Account> Changed;

        /// <summary>
        /// The current home currency balance.
        /// </summary>
        /// <returns>The value of the balance.</returns>
        double Balance();

        /// <summary>
        /// The current balance of the specified currency.
        /// </summary>
        /// <param name="currency">The ISO 4217 currency code.</param>
        /// <returns>The value of the balance.</returns>
        double Balance(CurrencyCode currency);

        /// <summary>
        /// The current total value (including converted foreign currencies) in home currency.
        /// </summary>
        /// <returns>The total value.</returns>
        double Value();

        /// <summary>
        /// The up-to specified date and time total value (including converted foreign currencies) in home currency.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <returns>The total value up to the specified date and time..</returns>
        double Value(DateTime dateTime);

        /// <summary>
        /// Deposits an amount of money in the specified currency to the account.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="amount">The positive amount to deposit.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="text">The text.</param>
        void Deposit(DateTime dateTime, double amount, CurrencyCode currency, string text);

        /// <summary>
        /// Deposits  an amount of money in home currency to the account.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="amount">The positive amount to deposit.</param>
        /// <param name="text">The text.</param>
        void Deposit(DateTime dateTime, double amount, string text);

        /// <summary>
        /// Withdraws an amount of money in the specified currency from the account.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="amount">The positive amount to withdraw.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="text">The text.</param>
        void Withdraw(DateTime dateTime, double amount, CurrencyCode currency, string text);

        /// <summary>
        /// Withdraws an amount of money in home currency from the account.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="amount">The positive amount to withdraw.</param>
        /// <param name="text">The text.</param>
        void Withdraw(DateTime dateTime, double amount, string text);
    }
}
