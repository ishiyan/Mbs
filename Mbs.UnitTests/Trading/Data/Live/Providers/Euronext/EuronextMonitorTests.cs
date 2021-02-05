using Mbs.Trading.Data.Live;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Live.Providers.Euronext
{
    [TestClass]
    public class EuronextMonitorTests
    {
        [TestMethod]
        public void EuronextMonitor_UserAgent_WhenSet_GetsCorrectValue()
        {
            const string defaultValue = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            const string expectedValue = "FooBar";

            Assert.AreEqual(defaultValue, EuronextMonitor.UserAgent);

            EuronextMonitor.UserAgent = expectedValue;
            Assert.AreEqual(expectedValue, EuronextMonitor.UserAgent);
        }

        [TestMethod]
        public void EuronextMonitor_IsSubscriptionWithHistory_WhenSet_GetsCorrectValue()
        {
            const bool defaultValue = true;
            const bool expectedValue = false;

            Assert.AreEqual(defaultValue, EuronextMonitor.IsSubscriptionWithHistory);

            EuronextMonitor.IsSubscriptionWithHistory = expectedValue;
            Assert.AreEqual(expectedValue, EuronextMonitor.IsSubscriptionWithHistory);
        }

        [TestMethod]
        public void EuronextMonitor_TradePollingDownloadTimeoutMilliseconds_WhenSet_GetsCorrectValue()
        {
            const int defaultValue = EuronextMonitor.DefaultTradePollingDownloadTimeoutMilliseconds;
            const int truncatedValue = EuronextMonitor.DefaultMinimumTradePollingPeriodMilliseconds;
            const int positiveValue = 360000;
            const int negativeValue = -360000;

            Assert.AreEqual(defaultValue, EuronextMonitor.TradePollingDownloadTimeoutMilliseconds);

            EuronextMonitor.TradePollingDownloadTimeoutMilliseconds = positiveValue;
            Assert.AreEqual(positiveValue, EuronextMonitor.TradePollingDownloadTimeoutMilliseconds);

            EuronextMonitor.TradePollingDownloadTimeoutMilliseconds = negativeValue;
            Assert.AreEqual(truncatedValue, EuronextMonitor.TradePollingDownloadTimeoutMilliseconds);
        }

        [TestMethod]
        public void EuronextMonitor_QuotePollingDownloadTimeoutMilliseconds_WhenSet_GetsCorrectValue()
        {
            const int defaultValue = EuronextMonitor.DefaultQuotePollingDownloadTimeoutMilliseconds;
            const int truncatedValue = EuronextMonitor.DefaultMinimumQuotePollingPeriodMilliseconds;
            const int positiveValue = 360000;
            const int negativeValue = -360000;

            Assert.AreEqual(defaultValue, EuronextMonitor.QuotePollingDownloadTimeoutMilliseconds);

            EuronextMonitor.QuotePollingDownloadTimeoutMilliseconds = positiveValue;
            Assert.AreEqual(positiveValue, EuronextMonitor.QuotePollingDownloadTimeoutMilliseconds);

            EuronextMonitor.QuotePollingDownloadTimeoutMilliseconds = negativeValue;
            Assert.AreEqual(truncatedValue, EuronextMonitor.QuotePollingDownloadTimeoutMilliseconds);
        }

        [TestMethod]
        public void EuronextMonitor_MinimalTradePollingPeriodMilliseconds_WhenSet_GetsCorrectValue()
        {
            const long minimalValue = EuronextMonitor.DefaultMinimumTradePollingPeriodMilliseconds;
            const long maximalValue = EuronextMonitor.DefaultMaximumTradePollingPeriodMilliseconds;

            Assert.AreEqual(minimalValue, EuronextMonitor.MinimalTradePollingPeriodMilliseconds);

            EuronextMonitor.MinimalTradePollingPeriodMilliseconds = maximalValue / 10;
            Assert.AreEqual(maximalValue / 10, EuronextMonitor.MinimalTradePollingPeriodMilliseconds);

            EuronextMonitor.MinimalTradePollingPeriodMilliseconds = maximalValue;
            Assert.AreEqual(maximalValue, EuronextMonitor.MinimalTradePollingPeriodMilliseconds);

            // The period cannot be lower that the minimal one.
            EuronextMonitor.MinimalTradePollingPeriodMilliseconds = minimalValue / 10;
            Assert.AreEqual(minimalValue, EuronextMonitor.MinimalTradePollingPeriodMilliseconds);

            // The period cannot be higher that the maximal one.
            EuronextMonitor.MinimalTradePollingPeriodMilliseconds = maximalValue * 2;
            Assert.AreEqual(maximalValue, EuronextMonitor.MinimalTradePollingPeriodMilliseconds);

            EuronextMonitor.MinimalTradePollingPeriodMilliseconds = minimalValue;
            Assert.AreEqual(minimalValue, EuronextMonitor.MinimalTradePollingPeriodMilliseconds);
        }

        [TestMethod]
        public void EuronextMonitor_MaximalTradePollingPeriodMilliseconds_WhenSet_GetsCorrectValue()
        {
            const long maximalValue = EuronextMonitor.DefaultMaximumTradePollingPeriodMilliseconds;
            const long minimalValue = EuronextMonitor.DefaultMinimumTradePollingPeriodMilliseconds;

            Assert.AreEqual(maximalValue, EuronextMonitor.MaximalTradePollingPeriodMilliseconds);

            EuronextMonitor.MaximalTradePollingPeriodMilliseconds = minimalValue * 10;
            Assert.AreEqual(minimalValue * 10, EuronextMonitor.MaximalTradePollingPeriodMilliseconds);

            EuronextMonitor.MaximalTradePollingPeriodMilliseconds = maximalValue;
            Assert.AreEqual(maximalValue, EuronextMonitor.MaximalTradePollingPeriodMilliseconds);

            // The period cannot be lower that the minimal one.
            EuronextMonitor.MaximalTradePollingPeriodMilliseconds = minimalValue / 10;
            Assert.AreEqual(minimalValue, EuronextMonitor.MaximalTradePollingPeriodMilliseconds);

            EuronextMonitor.MaximalTradePollingPeriodMilliseconds = maximalValue;
            Assert.AreEqual(maximalValue, EuronextMonitor.MaximalTradePollingPeriodMilliseconds);
        }

        [TestMethod]
        public void EuronextMonitor_GetTradePollingPeriodMilliseconds_WhenGet_ReturnsDefaultValue()
        {
            const long defaultValue = EuronextMonitor.DefaultMinimumTradePollingPeriodMilliseconds;

            var instrument = new Instrument("GLE", EuronextMic.Xpar, "FR0000130809", InstrumentType.Stock);
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument));
        }

        [TestMethod]
        public void EuronextMonitor_SetTradePollingPeriodMilliseconds_WhenGet_ReturnsCorrectValue()
        {
            const long defaultValue = EuronextMonitor.DefaultMinimumTradePollingPeriodMilliseconds;
            const long otherValue = 360000L;
            const long anotherValue = 480000L;
            const long negativeValue = -360000L;

            var instrument1 = new Instrument("GLE", EuronextMic.Xpar, "FR0000130809", InstrumentType.Stock);
            var instrument2 = new Instrument("KPN", EuronextMic.Xams, "NL0000009082", InstrumentType.Stock);

            EuronextMonitor.SetTradePollingPeriodMilliseconds(instrument1, otherValue);
            Assert.AreEqual(otherValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument1));
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument2));

            EuronextMonitor.SetTradePollingPeriodMilliseconds(instrument2, anotherValue);
            Assert.AreEqual(otherValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument1));
            Assert.AreEqual(anotherValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument2));

            // The period cannot be lower that the minimal one.
            EuronextMonitor.SetTradePollingPeriodMilliseconds(instrument2, EuronextMonitor.MinimalTradePollingPeriodMilliseconds / 10);
            Assert.AreEqual(otherValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument1));
            Assert.AreEqual(EuronextMonitor.MinimalTradePollingPeriodMilliseconds, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument2));

            // The negative period results in the default one.
            EuronextMonitor.SetTradePollingPeriodMilliseconds(instrument2, negativeValue);
            Assert.AreEqual(otherValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument1));
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument2));

            EuronextMonitor.SetTradePollingPeriodMilliseconds(instrument1, negativeValue);
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument1));
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradePollingPeriodMilliseconds(instrument2));
        }

        [TestMethod]
        public void EuronextMonitor_GetQuotePollingPeriodMilliseconds_WhenGet_ReturnsDefaultValue()
        {
            const long defaultValue = EuronextMonitor.DefaultMinimumQuotePollingPeriodMilliseconds;

            var instrument = new Instrument("GLE", EuronextMic.Xpar, "FR0000130809", InstrumentType.Stock);
            Assert.AreEqual(defaultValue, EuronextMonitor.GetQuotePollingPeriodMilliseconds(instrument));
        }

        [TestMethod]
        public void EuronextMonitor_SetQuotePollingPeriodMilliseconds_WhenGet_ReturnsCorrectValue()
        {
            const long defaultValue = EuronextMonitor.DefaultMinimumQuotePollingPeriodMilliseconds;
            const long otherValue = 360000L;
            const long anotherValue = 480000L;
            const long negativeValue = -360000L;

            var instrument1 = new Instrument("GLE", EuronextMic.Xpar, "FR0000130809", InstrumentType.Stock);
            var instrument2 = new Instrument("KPN", EuronextMic.Xams, "NL0000009082", InstrumentType.Stock);

            EuronextMonitor.SetQuotePollingPeriodMilliseconds(instrument1, otherValue);
            Assert.AreEqual(otherValue, EuronextMonitor.GetQuotePollingPeriodMilliseconds(instrument1));
            Assert.AreEqual(defaultValue, EuronextMonitor.GetQuotePollingPeriodMilliseconds(instrument2));

            EuronextMonitor.SetQuotePollingPeriodMilliseconds(instrument2, anotherValue);
            Assert.AreEqual(otherValue, EuronextMonitor.GetQuotePollingPeriodMilliseconds(instrument1));
            Assert.AreEqual(anotherValue, EuronextMonitor.GetQuotePollingPeriodMilliseconds(instrument2));

            // The negative period results in the default one.
            EuronextMonitor.SetQuotePollingPeriodMilliseconds(instrument2, negativeValue);
            Assert.AreEqual(otherValue, EuronextMonitor.GetQuotePollingPeriodMilliseconds(instrument1));
            Assert.AreEqual(defaultValue, EuronextMonitor.GetQuotePollingPeriodMilliseconds(instrument2));

            EuronextMonitor.SetQuotePollingPeriodMilliseconds(instrument1, negativeValue);
            Assert.AreEqual(defaultValue, EuronextMonitor.GetQuotePollingPeriodMilliseconds(instrument1));
            Assert.AreEqual(defaultValue, EuronextMonitor.GetQuotePollingPeriodMilliseconds(instrument2));
        }

        [TestMethod]
        public void EuronextMonitor_GetTradeVolumeAccumulation_WhenGet_ReturnsDefaultValue()
        {
            const bool defaultValue = true;

            var instrument = new Instrument("GLE", EuronextMic.Xpar, "FR0000130809", InstrumentType.Stock);
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradeVolumeAccumulation(instrument));
        }

        [TestMethod]
        public void EuronextMonitor_SetTradeVolumeAccumulation_WhenGet_ReturnsCorrectValue()
        {
            const bool defaultValue = true;
            const bool otherValue = false;

            var instrument1 = new Instrument("GLE", EuronextMic.Xpar, "FR0000130809", InstrumentType.Stock);
            var instrument2 = new Instrument("KPN", EuronextMic.Xams, "NL0000009082", InstrumentType.Stock);

            EuronextMonitor.SetTradeVolumeAccumulation(instrument1, otherValue);
            Assert.AreEqual(otherValue, EuronextMonitor.GetTradeVolumeAccumulation(instrument1));
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradeVolumeAccumulation(instrument2));

            EuronextMonitor.SetTradeVolumeAccumulation(instrument2, otherValue);
            Assert.AreEqual(otherValue, EuronextMonitor.GetTradeVolumeAccumulation(instrument1));
            Assert.AreEqual(otherValue, EuronextMonitor.GetTradeVolumeAccumulation(instrument2));

            EuronextMonitor.SetTradeVolumeAccumulation(instrument1, defaultValue);
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradeVolumeAccumulation(instrument1));
            Assert.AreEqual(otherValue, EuronextMonitor.GetTradeVolumeAccumulation(instrument2));

            EuronextMonitor.SetTradeVolumeAccumulation(instrument2, defaultValue);
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradeVolumeAccumulation(instrument1));
            Assert.AreEqual(defaultValue, EuronextMonitor.GetTradeVolumeAccumulation(instrument2));
        }

        [TestMethod]
        public void EuronextMonitor_GetInstrumentTradeMonitor_WhenGet_ReturnsCorrectValue()
        {
            var instrument1 = new Instrument("GLE", EuronextMic.Xpar, "FR0000130809", InstrumentType.Stock);
            var instrument2 = new Instrument("KPN", EuronextMic.Xams, "NL0000009082", InstrumentType.Stock);

            EuronextMonitor.InstrumentTradeMonitor instrumentMonitor1 = EuronextMonitor.GetInstrumentTradeMonitor(instrument1);
            Assert.IsNotNull(instrumentMonitor1);

            EuronextMonitor.InstrumentTradeMonitor instrumentMonitor2 = EuronextMonitor.GetInstrumentTradeMonitor(instrument2);
            Assert.IsNotNull(instrumentMonitor2);

            Assert.AreNotEqual(instrumentMonitor1, instrumentMonitor2);
        }

        [TestMethod]
        public void EuronextMonitor_GetInstrumentQuoteMonitor_WhenGet_ReturnsCorrectValue()
        {
            var instrument1 = new Instrument("GLE", EuronextMic.Xpar, "FR0000130809", InstrumentType.Stock);
            var instrument2 = new Instrument("KPN", EuronextMic.Xams, "NL0000009082", InstrumentType.Stock);

            EuronextMonitor.InstrumentQuoteMonitor instrumentMonitor1 = EuronextMonitor.GetInstrumentQuoteMonitor(instrument1);
            Assert.IsNotNull(instrumentMonitor1);

            EuronextMonitor.InstrumentQuoteMonitor instrumentMonitor2 = EuronextMonitor.GetInstrumentQuoteMonitor(instrument2);
            Assert.IsNotNull(instrumentMonitor2);

            Assert.AreNotEqual(instrumentMonitor1, instrumentMonitor2);
        }
    }
}
