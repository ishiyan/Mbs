using Mbs.Trading.Currencies;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;
using Mbs.Trading.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Instruments
{
    [TestClass]
    public class InstrumentTests
    {
        [TestMethod]
        public void Instrument_SecurityId_WhenSet_GettersReturnCorrectValues()
        {
            const InstrumentSecurityIdSource source = InstrumentSecurityIdSource.ExchangeSymbol;
            const string value = "foo";

            var instrument = new Instrument();
            Assert.IsNull(instrument.SecurityId, "(1)");
            Assert.IsNull(instrument.SecurityIdSource, "(2)");

            instrument.SetSecurityIdAs(source, value);
            Assert.AreEqual(value, instrument.SecurityId, "(3)");
            Assert.AreEqual(source, instrument.SecurityIdSource, "(4)");
            Assert.AreEqual(value, instrument.GetSecurityIdAs(source, false), "(5)");
            Assert.IsNull(instrument.GetSecurityIdAs(InstrumentSecurityIdSource.Isin, false), "(6)");
        }

        [TestMethod]
        public void Instrument_AlternateSecurityId_WhenSet_GettersReturnCorrectValues()
        {
            const InstrumentSecurityIdSource source = InstrumentSecurityIdSource.ExchangeSymbol;
            const string value = "foo";

            var instrument = new Instrument();
            Assert.IsNotNull(instrument.SecurityAlternateIdGroup, "(1)");

            instrument.SetSecurityAlternateIdAs(source, value);
            Assert.IsNull(instrument.GetSecurityIdAs(source, false), "(2)");
            Assert.AreEqual(value, instrument.GetSecurityIdAs(source), "(3)");
            Assert.IsNull(instrument.GetSecurityIdAs(InstrumentSecurityIdSource.Isin), "(4)");
        }

        [TestMethod]
        public void Instrument_Symbol_WhenSet_GettersReturnCorrectValues()
        {
            const string expected = "foo";

            var instrument = new Instrument();
            Assert.IsNull(instrument.Symbol);

            instrument.Symbol = expected;
            Assert.AreEqual(expected, instrument.Symbol);
        }

        [TestMethod]
        public void Instrument_Name_WhenSet_GettersReturnCorrectValues()
        {
            const string expected = "foo";

            var instrument = new Instrument();
            Assert.IsNull(instrument.Name);

            instrument.Name = expected;
            Assert.AreEqual(expected, instrument.Name);
        }

        [TestMethod]
        public void Instrument_Description_WhenSet_GettersReturnCorrectValues()
        {
            const string expected = "foo";

            var instrument = new Instrument();
            Assert.IsNull(instrument.Description);

            instrument.Description = expected;
            Assert.AreEqual(expected, instrument.Description);
        }

        [TestMethod]
        public void Instrument_Cfi_WhenSet_GettersReturnCorrectValues()
        {
            const string expected = "foo";

            var instrument = new Instrument();
            Assert.IsNull(instrument.Cfi);

            instrument.Cfi = expected;
            Assert.AreEqual(expected, instrument.Cfi);
        }

        [TestMethod]
        public void Instrument_Type_WhenSet_GettersReturnCorrectValues()
        {
            var instrument = new Instrument();
            Assert.AreEqual(InstrumentType.Undefined, instrument.Type);

            instrument.Type = InstrumentType.Stock;
            Assert.AreEqual(InstrumentType.Stock, instrument.Type);

            instrument.Type = InstrumentType.Cfd;
            Assert.AreEqual(InstrumentType.Cfd, instrument.Type);

            instrument.Type = InstrumentType.Crypto;
            Assert.AreEqual(InstrumentType.Crypto, instrument.Type);

            instrument.Type = InstrumentType.Etf;
            Assert.AreEqual(InstrumentType.Etf, instrument.Type);

            instrument.Type = InstrumentType.Etv;
            Assert.AreEqual(InstrumentType.Etv, instrument.Type);

            instrument.Type = InstrumentType.Forex;
            Assert.AreEqual(InstrumentType.Forex, instrument.Type);

            instrument.Type = InstrumentType.Fund;
            Assert.AreEqual(InstrumentType.Fund, instrument.Type);

            instrument.Type = InstrumentType.Future;
            Assert.AreEqual(InstrumentType.Future, instrument.Type);

            instrument.Type = InstrumentType.Option;
            Assert.AreEqual(InstrumentType.Option, instrument.Type);

            instrument.Type = InstrumentType.Index;
            Assert.AreEqual(InstrumentType.Index, instrument.Type);
        }

        [TestMethod]
        public void Instrument_Exchange_WhenSet_GettersReturnCorrectValues()
        {
            var instrument = new Instrument();
            Assert.AreEqual(new Exchange(ExchangeMic.Xxxx), instrument.Exchange);

            var expected = new Exchange(ExchangeMic.Xnys);
            instrument.Exchange = expected;
            Assert.AreEqual(expected, instrument.Exchange);
        }

        [TestMethod]
        public void Instrument_Currency_WhenSet_GettersReturnCorrectValues()
        {
            var instrument = new Instrument();
            Assert.AreEqual(CurrencyCode.Xxx, instrument.Currency);

            instrument.Currency = CurrencyCode.Usd;
            Assert.AreEqual(CurrencyCode.Usd, instrument.Currency);
        }

        [TestMethod]
        public void Instrument_BusinessDayCalendar_WhenSet_GettersReturnCorrectValues()
        {
            var instrument = new Instrument();
            Assert.AreEqual(BusinessDayCalendar.WeekendsOnly, instrument.BusinessDayCalendar);

            instrument.BusinessDayCalendar = BusinessDayCalendar.NoHolidays;
            Assert.AreEqual(BusinessDayCalendar.NoHolidays, instrument.BusinessDayCalendar);
        }

        [TestMethod]
        public void Instrument_SecurityStatus_WhenSet_GettersReturnCorrectValues()
        {
            var instrument = new Instrument();
            Assert.AreEqual(InstrumentSecurityStatus.Active, instrument.SecurityStatus);

            instrument.SecurityStatus = InstrumentSecurityStatus.ActiveClosingOrdersOnly;
            Assert.AreEqual(InstrumentSecurityStatus.ActiveClosingOrdersOnly, instrument.SecurityStatus);

            instrument.SecurityStatus = InstrumentSecurityStatus.Delisted;
            Assert.AreEqual(InstrumentSecurityStatus.Delisted, instrument.SecurityStatus);

            instrument.SecurityStatus = InstrumentSecurityStatus.Expired;
            Assert.AreEqual(InstrumentSecurityStatus.Expired, instrument.SecurityStatus);

            instrument.SecurityStatus = InstrumentSecurityStatus.Inactive;
            Assert.AreEqual(InstrumentSecurityStatus.Inactive, instrument.SecurityStatus);

            instrument.SecurityStatus = InstrumentSecurityStatus.KnockOutRevoked;
            Assert.AreEqual(InstrumentSecurityStatus.KnockOutRevoked, instrument.SecurityStatus);

            instrument.SecurityStatus = InstrumentSecurityStatus.KnockedOut;
            Assert.AreEqual(InstrumentSecurityStatus.KnockedOut, instrument.SecurityStatus);

            instrument.SecurityStatus = InstrumentSecurityStatus.PendingDeletion;
            Assert.AreEqual(InstrumentSecurityStatus.PendingDeletion, instrument.SecurityStatus);

            instrument.SecurityStatus = InstrumentSecurityStatus.PendingExpiry;
            Assert.AreEqual(InstrumentSecurityStatus.PendingExpiry, instrument.SecurityStatus);

            instrument.SecurityStatus = InstrumentSecurityStatus.Suspended;
            Assert.AreEqual(InstrumentSecurityStatus.Suspended, instrument.SecurityStatus);
        }
    }
}
