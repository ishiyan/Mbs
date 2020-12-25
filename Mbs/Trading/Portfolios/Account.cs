using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Mbs.Trading.Currencies;
using Mbs.Trading.Data;
using Mbs.Trading.Environments;
using Mbs.Trading.Time.Timepieces;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// The account.
    /// </summary>
    public sealed class Account : IAccount, IDisposable
    {
        private readonly ReaderWriterLockSlim listLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private readonly object currencyPositionsLock = new object();
        private readonly List<AccountCurrencyPosition> currencyPositions = new List<AccountCurrencyPosition>();
        private readonly Dictionary<CurrencyCode, AccountCurrencyPosition> currencyDictionary = new Dictionary<CurrencyCode, AccountCurrencyPosition>();
        private readonly object transactionHistoryLock = new object();
        private readonly List<AccountTransaction> transactionHistory = new List<AccountTransaction>(512);
        private readonly object transactionAddedLock = new object();
        private readonly object accountChangedLock = new object();

        private Action<Account, AccountTransaction> transactionAdded;
        private Action<Account> accountChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="holder">Identifies the holder of the account.</param>
        /// <param name="accountCurrency">The home currency.</param>
        /// <param name="initialDeposit">The initial deposit in home currency.</param>
        /// <param name="tradingEnvironment">The trading environment interface.</param>
        public Account(
            string holder,
            CurrencyCode accountCurrency,
            double initialDeposit,
            IEnvironment tradingEnvironment)
            : this(
                holder,
                accountCurrency,
                initialDeposit,
                tradingEnvironment.Timepiece,
                tradingEnvironment.CurrencyConverter,
                tradingEnvironment.DataPublisher)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="holder">Identifies the holder of the account.</param>
        /// <param name="accountCurrency">The home currency.</param>
        /// <param name="initialDeposit">The initial deposit in home currency.</param>
        /// <param name="timepiece">The timepiece interface.</param>
        /// <param name="currencyConverter">The currency converter interface.</param>
        /// <param name="dataPublisher">The data publisher interface.</param>
        public Account(
            string holder,
            CurrencyCode accountCurrency,
            double initialDeposit,
            ITimepiece timepiece,
            ICurrencyConverter currencyConverter,
            IDataPublisher dataPublisher)
        {
            Holder = holder;
            Currency = accountCurrency;
            var position = new AccountCurrencyPosition(accountCurrency);
            currencyPositions.Add(position);
            currencyDictionary.Add(accountCurrency, position);
            Timepiece = timepiece;
            CurrencyConverter = currencyConverter;
            if (Math.Abs(initialDeposit) > double.Epsilon)
            {
                Add(new AccountTransaction(timepiece.Time, initialDeposit, accountCurrency, "Initial deposit."), accountCurrency, initialDeposit);
            }

            Portfolio = new Portfolio(this, dataPublisher);
        }

        /// <inheritdoc/>
        public event Action<Account, AccountTransaction> TransactionAdded
        {
            add
            {
                lock (transactionAddedLock)
                {
                    transactionAdded += value;
                }
            }

            remove
            {
                lock (transactionAddedLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    transactionAdded -= value;
                }
            }
        }

        /// <inheritdoc/>
        public event Action<Account> Changed
        {
            add
            {
                lock (accountChangedLock)
                {
                    accountChanged += value;
                }
            }

            remove
            {
                lock (accountChangedLock)
                {
                    // ReSharper disable once DelegateSubtraction
                    accountChanged -= value;
                }
            }
        }

        /// <inheritdoc/>
        public IPortfolio Portfolio { get; }

        /// <inheritdoc/>
        public ICurrencyConverter CurrencyConverter { get; }

        /// <inheritdoc/>
        public ITimepiece Timepiece { get; }

        /// <inheritdoc/>
        public string Holder { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc/>
        public CurrencyCode Currency { get; }

        /// <inheritdoc/>
        public ReadOnlyCollection<AccountCurrencyPosition> CurrencyPositions
        {
            get
            {
                lock (currencyPositionsLock)
                {
                    return currencyPositions.AsReadOnly();
                }
            }
        }

        /// <inheritdoc/>
        public ReadOnlyCollection<AccountTransaction> TransactionHistory
        {
            get
            {
                lock (transactionHistoryLock)
                {
                    return transactionHistory.AsReadOnly();
                }
            }
        }

        /// <inheritdoc/>
        public double Balance()
        {
            lock (currencyPositionsLock)
            {
                return currencyPositions[0].Balance;
            }
        }

        /// <inheritdoc/>
        public double Balance(CurrencyCode currency)
        {
            lock (currencyPositionsLock)
            {
                if (currencyDictionary.TryGetValue(currency, out AccountCurrencyPosition position))
                {
                    return position.Balance;
                }
            }

            return 0;
        }

        /// <inheritdoc/>
        public double Value()
        {
            lock (currencyPositionsLock)
            {
                double sum = 0;
                foreach (var position in currencyPositions)
                {
                    sum += CurrencyConverter.Convert(position.Balance, position.Currency, Currency);
                }

                return sum;
            }
        }

        /// <inheritdoc/>
        public double Value(DateTime dateTime)
        {
            using var account = new Account(Holder, Currency, 0d, Timepiece, CurrencyConverter, null /*dataPublisher*/);
            lock (currencyPositionsLock)
            {
                lock (transactionHistoryLock)
                {
                    foreach (AccountTransaction transaction in transactionHistory)
                    {
                        if (transaction.DateTime <= dateTime)
                        {
                            account.Add(transaction);
                        }
                    }
                }
            }

            double sum = 0;
            foreach (var position in account.CurrencyPositions)
            {
                sum += CurrencyConverter.Convert(position.Balance, position.Currency, Currency);
            }

            return sum;
        }

        /// <inheritdoc/>
        public void Deposit(DateTime dateTime, double amount, CurrencyCode currency, string text)
        {
            Add(new AccountTransaction(dateTime, amount, currency, text), currency, amount);
        }

        /// <inheritdoc/>
        public void Deposit(DateTime dateTime, double amount, string text)
        {
            Add(new AccountTransaction(dateTime, amount, Currency, text), Currency, amount);
        }

        /// <inheritdoc/>
        public void Withdraw(DateTime dateTime, double amount, CurrencyCode currency, string text)
        {
            amount = -amount;
            Add(new AccountTransaction(dateTime, amount, currency, text), currency, amount);
        }

        /// <inheritdoc/>
        public void Withdraw(DateTime dateTime, double amount, string text)
        {
            amount = -amount;
            Add(new AccountTransaction(dateTime, amount, Currency, text), Currency, amount);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Adds a transaction to the account.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        internal void Add(AccountTransaction transaction)
        {
            Add(transaction, transaction.Currency, transaction.Value);
        }

        /// <summary>
        /// Adds a portfolio execution to the account.
        /// </summary>
        /// <param name="portfolioExecution">The portfolio execution.</param>
        /// <param name="amount">The pre-calculated amount in execution instrument's currency.</param>
        internal void Add(PortfolioExecution portfolioExecution, double amount)
        {
            CurrencyCode ec = portfolioExecution.Currency;
            Add(new AccountTransaction(portfolioExecution, amount, ec), ec, amount);
        }

        /// <summary>
        /// Adds a portfolio execution to the account.
        /// </summary>
        /// <param name="portfolioExecution">The portfolio execution.</param>
        /// <param name="amount">The pre-calculated amount in execution instrument's currency.</param>
        /// <param name="amountCommission">The pre-calculated amount in execution commission currency. The value should be positive.</param>
        internal void Add(PortfolioExecution portfolioExecution, double amount, double amountCommission)
        {
            CurrencyCode ec = portfolioExecution.Currency;
            Add(new AccountTransaction(portfolioExecution, amount, ec), ec, amount);

            if (Math.Abs(amountCommission) > double.Epsilon)
            {
                CurrencyCode cc = portfolioExecution.SingleOrderReport.CommissionCurrency;
                Add(new AccountTransaction(portfolioExecution, -amountCommission, cc, "commission"), cc, -amountCommission);
            }
        }

        private void OnTransactionAdded(AccountTransaction accountTransaction)
        {
            lock (transactionAddedLock)
            {
                if (transactionAdded != null)
                {
                    var handlers = transactionAdded.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var theHandler = handler as Action<Account, AccountTransaction>;
                        theHandler?.Invoke(this, accountTransaction);
                    }
                }
            }
        }

        private void OnChanged()
        {
            lock (accountChangedLock)
            {
                if (accountChanged != null)
                {
                    var handlers = accountChanged.GetInvocationList();
                    foreach (Delegate handler in handlers)
                    {
                        var theHandler = handler as Action<Account>;
                        theHandler?.Invoke(this);
                    }
                }
            }
        }

        private void Add(AccountTransaction transaction, CurrencyCode currency, double amount)
        {
            lock (currencyPositionsLock)
            {
                lock (transactionHistoryLock)
                {
                    transactionHistory.Add(transaction);
                }

                if (!currencyDictionary.TryGetValue(currency, out AccountCurrencyPosition position))
                {
                    position = new AccountCurrencyPosition(currency, amount);
                    currencyDictionary.Add(currency, position);
                    currencyPositions.Add(position);
                }
                else
                {
                    position.Balance += amount;
                }
            }

            OnTransactionAdded(transaction);
            OnChanged();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                listLock?.Dispose();
            }
        }
    }
}
