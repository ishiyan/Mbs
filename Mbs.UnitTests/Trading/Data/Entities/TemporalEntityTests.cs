// using System;
// using Mbs.Trading.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data
{
    [TestClass]
    public class TemporalEntityTests
    {
/*
        private const int CompareToLesserResult = 1;
        private const int CompareToGreaterResult = -1;

        private static readonly DateTime DefaultDateTime = new DateTime(2017, 3, 3, 10, 1, 4);
        private static readonly DateTime LesserDateTime = new DateTime(2009, 4, 4, 11, 3, 5);
        private static readonly DateTime GreaterDateTime = new DateTime(2099, 5, 5, 12, 4, 6);

        private readonly Ohlcv ohlcv = CreateOhlcv(DefaultDateTime);
        private readonly Ohlcv ohlcvCopy = CreateOhlcv(DefaultDateTime);
        private readonly Ohlcv ohlcvGreater = CreateOhlcv(GreaterDateTime);
        private readonly Ohlcv ohlcvLesser = CreateOhlcv(LesserDateTime);

        private readonly Quote quote = CreateQuote(DefaultDateTime);
        private readonly Quote quoteCopy = CreateQuote(DefaultDateTime);
        private readonly Quote quoteGreater = CreateQuote(GreaterDateTime);
        private readonly Quote quoteLesser = CreateQuote(LesserDateTime);

        private readonly Trade trade = CreateTrade(DefaultDateTime);
        private readonly Trade tradeCopy = CreateTrade(DefaultDateTime);
        private readonly Trade tradeGreater = CreateTrade(GreaterDateTime);
        private readonly Trade tradeLesser = CreateTrade(LesserDateTime);

        private readonly Scalar scalar = CreateScalar(DefaultDateTime);
        private readonly Scalar scalarCopy = CreateScalar(DefaultDateTime);
        private readonly Scalar scalarGreater = CreateScalar(GreaterDateTime);
        private readonly Scalar scalarLesser = CreateScalar(LesserDateTime);

        private static Ohlcv CreateOhlcv(DateTime dateTime)
        {
            return new Ohlcv(dateTime, 0d, 0d, 0d, 0d, 0d);
        }

        private static Quote CreateQuote(DateTime dateTime)
        {
            return new Quote(dateTime, 0d, 0d, 0d, 0d);
        }

        private static Trade CreateTrade(DateTime dateTime)
        {
            return new Trade(dateTime, 0d, 0d);
        }

        private static Scalar CreateScalar(DateTime dateTime)
        {
            return new Scalar(dateTime, 0d);
        }

        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void TemporalEntity_CompareTo_Ohlcv_CorrectResult()
        {
            Assert.IsTrue(ohlcv.CompareTo(null) == 1);
            Assert.IsTrue(ohlcv.CompareTo(ohlcv) == 0);
            Assert.IsTrue(ohlcv.CompareTo(ohlcvCopy) == 0);
            Assert.IsTrue(ohlcv.CompareTo(ohlcvLesser) == CompareToLesserResult);
            Assert.IsTrue(ohlcv.CompareTo(ohlcvGreater) == CompareToGreaterResult);

            Assert.IsTrue(ohlcv.CompareTo(quoteLesser) == CompareToLesserResult);
            Assert.IsTrue(ohlcv.CompareTo(quoteGreater) == CompareToGreaterResult);

            Assert.IsTrue(ohlcv.CompareTo(tradeLesser) == CompareToLesserResult);
            Assert.IsTrue(ohlcv.CompareTo(tradeGreater) == CompareToGreaterResult);

            Assert.IsTrue(ohlcv.CompareTo(scalarLesser) == CompareToLesserResult);
            Assert.IsTrue(ohlcv.CompareTo(scalarGreater) == CompareToGreaterResult);
        }

        [TestMethod]
        public void TemporalEntity_CompareTo_Quote_CorrectResult()
        {
            Assert.IsTrue(quote.CompareTo(null) == 1);
            Assert.IsTrue(quote.CompareTo(quote) == 0);
            Assert.IsTrue(quote.CompareTo(quoteCopy) == 0);
            Assert.IsTrue(quote.CompareTo(quoteLesser) == CompareToLesserResult);
            Assert.IsTrue(quote.CompareTo(quoteGreater) == CompareToGreaterResult);

            Assert.IsTrue(quote.CompareTo(ohlcvLesser) == CompareToLesserResult);
            Assert.IsTrue(quote.CompareTo(ohlcvGreater) == CompareToGreaterResult);

            Assert.IsTrue(quote.CompareTo(tradeLesser) == CompareToLesserResult);
            Assert.IsTrue(quote.CompareTo(tradeGreater) == CompareToGreaterResult);

            Assert.IsTrue(quote.CompareTo(scalarLesser) == CompareToLesserResult);
            Assert.IsTrue(quote.CompareTo(scalarGreater) == CompareToGreaterResult);
        }

        [TestMethod]
        public void TemporalEntity_CompareTo_Trade_CorrectResult()
        {
            Assert.IsTrue(trade.CompareTo(null) == 1);
            Assert.IsTrue(trade.CompareTo(trade) == 0);
            Assert.IsTrue(trade.CompareTo(tradeCopy) == 0);
            Assert.IsTrue(trade.CompareTo(tradeLesser) == CompareToLesserResult);
            Assert.IsTrue(trade.CompareTo(tradeGreater) == CompareToGreaterResult);

            Assert.IsTrue(trade.CompareTo(ohlcvLesser) == CompareToLesserResult);
            Assert.IsTrue(trade.CompareTo(ohlcvGreater) == CompareToGreaterResult);

            Assert.IsTrue(trade.CompareTo(quoteLesser) == CompareToLesserResult);
            Assert.IsTrue(trade.CompareTo(quoteGreater) == CompareToGreaterResult);

            Assert.IsTrue(trade.CompareTo(scalarLesser) == CompareToLesserResult);
            Assert.IsTrue(trade.CompareTo(scalarGreater) == CompareToGreaterResult);
        }

        [TestMethod]
        public void TemporalEntity_CompareTo_Scalar_CorrectResult()
        {
            Assert.IsTrue(scalar.CompareTo(null) == 1);
            Assert.IsTrue(scalar.CompareTo(scalar) == 0);
            Assert.IsTrue(scalar.CompareTo(scalarCopy) == 0);
            Assert.IsTrue(scalar.CompareTo(scalarLesser) == CompareToLesserResult);
            Assert.IsTrue(scalar.CompareTo(scalarGreater) == CompareToGreaterResult);

            Assert.IsTrue(scalar.CompareTo(ohlcvLesser) == CompareToLesserResult);
            Assert.IsTrue(scalar.CompareTo(ohlcvGreater) == CompareToGreaterResult);

            Assert.IsTrue(scalar.CompareTo(quoteLesser) == CompareToLesserResult);
            Assert.IsTrue(scalar.CompareTo(quoteGreater) == CompareToGreaterResult);

            Assert.IsTrue(scalar.CompareTo(tradeLesser) == CompareToLesserResult);
            Assert.IsTrue(scalar.CompareTo(tradeGreater) == CompareToGreaterResult);
        }
*/
    }
}
