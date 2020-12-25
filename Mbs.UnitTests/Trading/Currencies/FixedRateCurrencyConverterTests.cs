using System.Collections.Generic;
using System.Linq;
using Mbs.Trading.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Currencies
{
    [TestClass]
    public class FixedRateCurrencyConverterTests
    {
        [TestMethod]
        public void FixedRateCurrencyConverter_NonExistentRate_RateSet_CorrectExchangeRateReturned()
        {
            var cc = new FixedRateCurrencyConverter();
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk));

            const double rate = 12345.6789;
            cc.NonExistentRate = rate;
            Assert.AreEqual(rate, cc.NonExistentRate);
            Assert.AreEqual(rate, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk));

            cc.NonExistentRate = double.NaN;
            Assert.IsTrue(double.IsNaN(cc.NonExistentRate));
            Assert.IsTrue(double.IsNaN(cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk)));

            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, rate);
            Assert.IsTrue(double.IsNaN(cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek)));
        }

        [TestMethod]
        public void FixedRateCurrencyConverter_TermCurrencies_ExchangeRateSet_CorrectTermCurrenciesReturned()
        {
            var cc = new FixedRateCurrencyConverter();
            List<CurrencyCode> list = cc.TermCurrencies(CurrencyCode.Nok).ToList();
            Assert.AreEqual(0, list.Count);

            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 123.456);
            list = cc.TermCurrencies(CurrencyCode.Nok).ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(CurrencyCode.Dkk, list[0]);

            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek, 456.789);
            list = cc.TermCurrencies(CurrencyCode.Nok).ToList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(CurrencyCode.Dkk, list[0]);
            Assert.AreEqual(CurrencyCode.Sek, list[1]);

            list = cc.TermCurrencies(CurrencyCode.Dkk).ToList();
            Assert.AreEqual(0, list.Count);

            list = cc.TermCurrencies(CurrencyCode.Sek).ToList();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void FixedRateCurrencyConverter_BaseCurrencies_ExchangeRateSet_CorrectBaseCurrenciesReturned()
        {
            var cc = new FixedRateCurrencyConverter();
            List<CurrencyCode> list = cc.BaseCurrencies(CurrencyCode.Nok).ToList();
            Assert.AreEqual(0, list.Count);

            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 123.456);
            list = cc.BaseCurrencies(CurrencyCode.Dkk).ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(CurrencyCode.Nok, list[0]);

            cc.ExchangeRate(CurrencyCode.Sek, CurrencyCode.Dkk, 456.789);
            list = cc.BaseCurrencies(CurrencyCode.Dkk).ToList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(CurrencyCode.Nok, list[0]);
            Assert.AreEqual(CurrencyCode.Sek, list[1]);

            list = cc.BaseCurrencies(CurrencyCode.Nok).ToList();
            Assert.AreEqual(0, list.Count);

            list = cc.BaseCurrencies(CurrencyCode.Sek).ToList();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void FixedRateCurrencyConverter_RateSubscribe_ExchangeRateSet_CorrectRateHistoryReported()
        {
            var actual = new List<double>();
            var cc = new FixedRateCurrencyConverter();
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek));

            cc.RateSubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.RateSubscribe(CurrencyCode.Nok, CurrencyCode.Sek, x => actual.Add(x));

            cc.ExchangeRate(CurrencyCode.Dkk, CurrencyCode.Nok, 0.1);
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.1);

            cc.RateSubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek, 2.1);
            cc.ExchangeRate(CurrencyCode.Sek, CurrencyCode.Nok, 0.2);
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.2);

            cc.RateUnsubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.3);
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek, 2.2);

            cc.RateUnsubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek, 2.3);
            cc.ExchangeRate(CurrencyCode.Sek, CurrencyCode.Dkk, 0.3);
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.4);
            cc.ExchangeRate(CurrencyCode.Dkk, CurrencyCode.Sek, 0.4);

            Assert.AreEqual(7, actual.Count);
            Assert.AreEqual(1.1, actual[0]);
            Assert.AreEqual(2.1, actual[1]);
            Assert.AreEqual(1.2, actual[2]);
            Assert.AreEqual(1.2, actual[3]);
            Assert.AreEqual(1.3, actual[4]);
            Assert.AreEqual(2.2, actual[5]);
            Assert.AreEqual(2.3, actual[6]);
        }

        [TestMethod]
        public void FixedRateCurrencyConverter_RateUnsubscribe_ExchangeRateSet_CorrectRateHistoryReported()
        {
            var actual = new List<double>();
            var cc = new FixedRateCurrencyConverter();
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk));

            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.1);

            cc.RateSubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.2);

            cc.RateUnsubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.3);

            cc.RateSubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.4);

            cc.RateSubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.5);

            cc.RateSubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.6);

            cc.RateUnsubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.RateUnsubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.RateUnsubscribe(CurrencyCode.Nok, CurrencyCode.Dkk, x => actual.Add(x));
            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, 1.7);

            Assert.AreEqual(7, actual.Count);
            Assert.AreEqual(1.2, actual[0]);
            Assert.AreEqual(1.4, actual[1]);
            Assert.AreEqual(1.5, actual[2]);
            Assert.AreEqual(1.5, actual[3]);
            Assert.AreEqual(1.6, actual[4]);
            Assert.AreEqual(1.6, actual[5]);
            Assert.AreEqual(1.6, actual[6]);
        }

        [TestMethod]
        public void FixedRateCurrencyConverter_ExchangeRate_ExchangeRateSet_CorrectRateReturned()
        {
            const double nokDkk = 123.456;
            const double nokSek = 456.789;
            const double dkkNok = 321.456;
            const double sekNok = 654.789;

            var cc = new FixedRateCurrencyConverter();
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Dkk, CurrencyCode.Nok));

            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, nokDkk);
            Assert.AreEqual(nokDkk, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek));

            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek, nokSek);
            Assert.AreEqual(nokDkk, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(nokSek, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek));
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Dkk, CurrencyCode.Nok));
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Sek, CurrencyCode.Nok));

            cc.ExchangeRate(CurrencyCode.Dkk, CurrencyCode.Nok, dkkNok);
            Assert.AreEqual(nokDkk, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(nokSek, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek));
            Assert.AreEqual(dkkNok, cc.ExchangeRate(CurrencyCode.Dkk, CurrencyCode.Nok));
            Assert.AreEqual(0, cc.ExchangeRate(CurrencyCode.Sek, CurrencyCode.Nok));

            cc.ExchangeRate(CurrencyCode.Sek, CurrencyCode.Nok, sekNok);
            Assert.AreEqual(nokDkk, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(nokSek, cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Sek));
            Assert.AreEqual(dkkNok, cc.ExchangeRate(CurrencyCode.Dkk, CurrencyCode.Nok));
            Assert.AreEqual(sekNok, cc.ExchangeRate(CurrencyCode.Sek, CurrencyCode.Nok));
        }

        [TestMethod]
        public void FixedRateCurrencyConverter_Convert_ExchangeRateSet_CorrectConversionReturned()
        {
            const double amount1 = 123.45;
            const double amount2 = 67.89;
            const double two = 2.0;
            const double three = 3.0;
            const double four = 4.0;

            var cc = new FixedRateCurrencyConverter();
            Assert.AreEqual(0, cc.Convert(amount1, CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(0, cc.Convert(amount2, CurrencyCode.Dkk, CurrencyCode.Nok));

            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, two);
            Assert.AreEqual(amount1 * two, cc.Convert(amount1, CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(0, cc.Convert(amount2, CurrencyCode.Dkk, CurrencyCode.Nok));

            cc.ExchangeRate(CurrencyCode.Nok, CurrencyCode.Dkk, three);
            Assert.AreEqual(amount1 * three, cc.Convert(amount1, CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(0, cc.Convert(amount2, CurrencyCode.Dkk, CurrencyCode.Nok));

            cc.ExchangeRate(CurrencyCode.Dkk, CurrencyCode.Nok, four);
            Assert.AreEqual(amount1 * three, cc.Convert(amount1, CurrencyCode.Nok, CurrencyCode.Dkk));
            Assert.AreEqual(amount2 * four, cc.Convert(amount2, CurrencyCode.Dkk, CurrencyCode.Nok));
        }
    }
}
