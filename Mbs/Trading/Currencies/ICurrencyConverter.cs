using System;
using System.Collections.Generic;

namespace Mbs.Trading.Currencies
{
    /// <summary>
    /// A currency converter interface.
    /// </summary>
    public interface ICurrencyConverter
    {
        /// <summary>
        /// The list of all accessible base currencies for the specified term currency.
        /// </summary>
        /// <param name="termCurrency">The term currency.</param>
        /// <returns>The list of base currencies.</returns>
        IEnumerable<CurrencyCode> BaseCurrencies(CurrencyCode termCurrency);

        /// <summary>
        /// The list of all accessible term currencies for the specified base currency.
        /// </summary>
        /// <param name="baseCurrency">The base currency.</param>
        /// <returns>The list of term currencies.</returns>
        IEnumerable<CurrencyCode> TermCurrencies(CurrencyCode baseCurrency);

        /// <summary>
        /// The current direct currency exchange rate.
        /// </summary>
        /// <remarks>
        /// <para>
        /// X units of base currency are equal to the <c>X * Rate</c> units of the term currency.
        /// </para>
        /// <para>
        /// If USD is the base currency and EUR is the term currency, then the currency pair USDEUR gives the required rate.
        /// </para>
        /// </remarks>
        /// <param name="baseCurrency">The base currency.</param>
        /// <param name="termCurrency">The term currency.</param>
        /// <returns>The direct currency exchange rate.</returns>
        double ExchangeRate(CurrencyCode baseCurrency, CurrencyCode termCurrency);

        /// <summary>
        /// Converts an amount in base currency to the amount in the term currency.
        /// </summary>
        /// <param name="baseAmount">The amount in base currency.</param>
        /// <param name="baseCurrency">The base currency.</param>
        /// <param name="termCurrency">The term currency.</param>
        /// <returns>The amount in the term currency.</returns>
        double Convert(double baseAmount, CurrencyCode baseCurrency, CurrencyCode termCurrency);

        /// <summary>
        /// Activates a subscription to a currency exchange rate notification for a specified base and term currency pair.
        /// </summary>
        /// <param name="baseCurrency">The base currency.</param>
        /// <param name="termCurrency">The term currency.</param>
        /// <param name="action">The action.</param>
        void RateSubscribe(CurrencyCode baseCurrency, CurrencyCode termCurrency, Action<double> action);

        /// <summary>
        /// Cancels an active subscription to a currency exchange rate notification for a specified base and term currency pair.
        /// </summary>
        /// <param name="baseCurrency">The base currency.</param>
        /// <param name="termCurrency">The term currency.</param>
        /// <param name="action">The action.</param>
        void RateUnsubscribe(CurrencyCode baseCurrency, CurrencyCode termCurrency, Action<double> action);
    }
}
