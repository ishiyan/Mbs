using System;
using System.Collections.Generic;
using System.Linq;

namespace Mbs.Trading.Currencies
{
    /// <summary>
    /// The externally updated fixed-rate currency converter.
    /// </summary>
    public class FixedRateCurrencyConverter : ICurrencyConverter
    {
        private class Rate
        {
            private double value;

            /// <summary>
            /// Gets or sets the exchange rate value.
            /// </summary>
            internal double Value
            {
                get => value;
                set
                {
                    if (Math.Abs(this.value - value) > double.Epsilon)
                    {
                        this.value = value;
                        lock (changedLock)
                        {
                            if (null != changed)
                            {
                                Delegate[] handlers = changed.GetInvocationList();
                                foreach (Delegate handler in handlers)
                                {
                                    var subscriber = handler as Action<double>;
                                    subscriber?.Invoke(value);
                                }
                            }
                        }
                    }
                }
            }

            private readonly object changedLock = new object();
            private Action<double> changed;

            /// <summary>
            /// Notifies when a rate has been changed.
            /// </summary>
            internal event Action<double> Changed
            {
                add
                {
                    lock (changedLock)
                    {
                        changed += value;
                    }
                }

                remove
                {
                    lock (changedLock)
                    {
                        // ReSharper disable once DelegateSubtraction
                        changed -= value;
                    }
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Rate"/> class.
            /// </summary>
            /// <param name="rate">The ininial rate.</param>
            internal Rate(double rate)
            {
                value = rate;
            }
        }

        private readonly object rateDictionaryLock = new object();

        /// <summary>
        /// Dictionary(baseCurrency, Dictionary(termCurrency, Rate)).
        /// </summary>
        private readonly Dictionary<CurrencyCode, Dictionary<CurrencyCode, Rate>> rateDictionary = new Dictionary<CurrencyCode, Dictionary<CurrencyCode, Rate>>();

        /// <summary>
        /// The list of all accessible base currencies for the specified term currency.
        /// </summary>
        /// <param name="termCurrency">The term currency.</param>
        /// <returns>The list of base currencies.</returns>
        public IEnumerable<CurrencyCode> BaseCurrencies(CurrencyCode termCurrency)
        {
            var baseList = new List<CurrencyCode>();
            lock (rateDictionaryLock)
            {
                // foreach (var pair in rateDictionary)
                // {
                //     Dictionary<CurrencyCode, Rate> termDictionary = pair.Value;
                //     if (termDictionary.ContainsKey(termCurrency))
                //         baseList.Add(pair.Key);
                // }
                baseList.AddRange(from pair in rateDictionary let termDictionary = pair.Value where termDictionary.ContainsKey(termCurrency) select pair.Key);
            }

            return baseList;
        }

        /// <summary>
        /// The list of all accessible term currencies for the specified base currency.
        /// </summary>
        /// <param name="baseCurrency">The base currency.</param>
        /// <returns>The list of term currencies.</returns>
        public IEnumerable<CurrencyCode> TermCurrencies(CurrencyCode baseCurrency)
        {
            var termList = new List<CurrencyCode>();
            lock (rateDictionaryLock)
            {
                if (rateDictionary.ContainsKey(baseCurrency))
                    termList.AddRange(rateDictionary[baseCurrency].Keys);
            }

            return termList;
        }

        /// <summary>
        /// Gets or sets the exchange rate value for non-existent currency pairs.
        /// </summary>
        public double NonExistentRate { get; set; }

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
        public double ExchangeRate(CurrencyCode baseCurrency, CurrencyCode termCurrency)
        {
            lock (rateDictionaryLock)
            {
                if (rateDictionary.ContainsKey(baseCurrency))
                {
                    Dictionary<CurrencyCode, Rate> termDictionary = rateDictionary[baseCurrency];
                    if (termDictionary.ContainsKey(termCurrency))
                        return termDictionary[termCurrency].Value;
                }
            }

            return NonExistentRate;
        }

        /// <summary>
        /// Converts an amount in base currency to the amount in the term currency.
        /// </summary>
        /// <param name="baseAmount">The amount in base currency.</param>
        /// <param name="baseCurrency">The base currency.</param>
        /// <param name="termCurrency">The term currency.</param>
        /// <returns>The amount in the term currency.</returns>
        public double Convert(double baseAmount, CurrencyCode baseCurrency, CurrencyCode termCurrency)
        {
            lock (rateDictionaryLock)
            {
                if (rateDictionary.ContainsKey(baseCurrency))
                {
                    Dictionary<CurrencyCode, Rate> termDictionary = rateDictionary[baseCurrency];
                    if (termDictionary.ContainsKey(termCurrency))
                        return baseAmount * termDictionary[termCurrency].Value;
                }
            }

            return baseAmount * NonExistentRate;
        }

        /// <summary>
        /// Activates a subscription to a currency exchange rate notification for a specified base and term currency pair.
        /// </summary>
        /// <param name="baseCurrency">The base currency.</param>
        /// <param name="termCurrency">The term currency.</param>
        /// <param name="action">The action.</param>
        public void RateSubscribe(CurrencyCode baseCurrency, CurrencyCode termCurrency, Action<double> action)
        {
            lock (rateDictionaryLock)
            {
                if (rateDictionary.ContainsKey(baseCurrency))
                {
                    Dictionary<CurrencyCode, Rate> termDictionary = rateDictionary[baseCurrency];
                    if (termDictionary.ContainsKey(termCurrency))
                    {
                        termDictionary[termCurrency].Changed += action;
                    }
                    else
                    {
                        var rate = new Rate(NonExistentRate);
                        rate.Changed += action;
                        termDictionary.Add(termCurrency, rate);
                    }
                }
                else
                {
                    var rate = new Rate(NonExistentRate);
                    rate.Changed += action;
                    var termDictionary = new Dictionary<CurrencyCode, Rate> { { termCurrency, rate } };
                    rateDictionary.Add(baseCurrency, termDictionary);
                }
            }
        }

        /// <summary>
        /// Cancels an active subscription to a currency exchange rate notification for a specified base and term currency pair.
        /// </summary>
        /// <param name="baseCurrency">The base currency.</param>
        /// <param name="termCurrency">The term currency.</param>
        /// <param name="action">The action.</param>
        public void RateUnsubscribe(CurrencyCode baseCurrency, CurrencyCode termCurrency, Action<double> action)
        {
            lock (rateDictionaryLock)
            {
                if (rateDictionary.ContainsKey(baseCurrency))
                {
                    Dictionary<CurrencyCode, Rate> termDictionary = rateDictionary[baseCurrency];
                    if (termDictionary.ContainsKey(termCurrency))
                        termDictionary[termCurrency].Changed -= action;
                }
            }
        }

        /// <summary>
        /// Adds or updates the direct currency exchange rate for a currency pair.
        /// </summary>
        /// <param name="baseCurrency">The base currency.</param>
        /// <param name="termCurrency">The term currency.</param>
        /// <param name="exchangeRate">The direct currency exchange rate.</param>
        public void ExchangeRate(CurrencyCode baseCurrency, CurrencyCode termCurrency, double exchangeRate)
        {
            lock (rateDictionaryLock)
            {
                if (rateDictionary.ContainsKey(baseCurrency))
                {
                    Dictionary<CurrencyCode, Rate> termDictionary = rateDictionary[baseCurrency];
                    if (termDictionary.ContainsKey(termCurrency))
                    {
                        termDictionary[termCurrency].Value = exchangeRate;
                    }
                    else
                    {
                        var rate = new Rate(exchangeRate);
                        termDictionary.Add(termCurrency, rate);
                    }
                }
                else
                {
                    var rate = new Rate(exchangeRate);
                    var termDictionary = new Dictionary<CurrencyCode, Rate> { { termCurrency, rate } };
                    rateDictionary.Add(baseCurrency, termDictionary);
                }
            }
        }
    }
}
