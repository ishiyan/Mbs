using System;
using Mbs.Trading.Currencies;
using Mbs.Trading.Portfolios.Enumerations;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// An immutable account transaction.
    /// </summary>
    public class AccountTransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountTransaction"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="value">The value.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="description">The textual description.</param>
        internal AccountTransaction(DateTime dateTime, double value, CurrencyCode currency, string description)
        {
            DateTime = dateTime;
            Value = value;
            Currency = currency;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountTransaction"/> class.
        /// </summary>
        /// <param name="portfolioExecution">The portfolio execution.</param>
        /// <param name="value">The value.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="commentToAppend">The textual comment. Will be appended to the portfolio execution comment.</param>
        internal AccountTransaction(PortfolioExecution portfolioExecution, double value, CurrencyCode currency, string commentToAppend = null)
        {
            DateTime = portfolioExecution.DateTime;
            Value = value;
            Currency = currency;
            if (string.IsNullOrEmpty(portfolioExecution.Comment))
            {
                if (commentToAppend != null)
                {
                    Description = commentToAppend;
                }
            }
            else
            {
                Description = commentToAppend == null
                    ? portfolioExecution.Comment
                    : $"{portfolioExecution.Comment} ({commentToAppend})";
            }
        }

        /// <summary>
        /// Gets if this is a deposit or a withdrawal.
        /// </summary>
        public AccountAction Action => Value > 0d ? AccountAction.Deposit : AccountAction.Withdraw;

        /// <summary>
        /// Gets the date and time.
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        public CurrencyCode Currency { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets the textual description.
        /// </summary>
        public string Description { get; }
    }
}
