using System;
using System.Linq;
using Mbs.Trading.Instruments;
using Mbs.Trading.Instruments.Groups;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Instruments.Groups
{
    [TestClass]
    public class InstrumentSecurityAlternateIdGroupTests
    {
        [TestMethod]
        public void InstrumentSecurityAlternateIdGroup_Constructor_WhenConstructed_CollectionIsEmpty()
        {
            var group = new InstrumentSecurityAlternateIdGroup();

            Assert.IsNotNull(group, "is not null");
            Assert.AreEqual(0, group.Count, "count is zero");
            Assert.IsNotNull(group.Collection, "collection is not null");
            Assert.AreEqual(0, group.Collection.Count, "collection count is zero");
        }

        [TestMethod]
        public void InstrumentSecurityAlternateIdGroup_Find_WhenConstructed_CannotFind()
        {
            var group = new InstrumentSecurityAlternateIdGroup();

            Assert.IsNull(group.Find(InstrumentSecurityIdSource.Isin), "cannot find Isin");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.Cusip), "cannot find Cusip");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.Sedol), "cannot find Sedol");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.RicCode), "cannot find RicCode");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.BloombergSymbol), "cannot find BloombergSymbol");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.BloombergOpenSymbologyBbgid), "cannot find BloombergOpenSymbologyBbgid");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.ExchangeSymbol), "ExchangeSymbol");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.IsoCurrencyCode), "cannot find IsoCurrencyCode");
        }

        [TestMethod]
        public void InstrumentSecurityAlternateIdGroup_Find_WhenAdded_CanFind()
        {
            const string value = "foo";
            var group = new InstrumentSecurityAlternateIdGroup();
            group.Add(value, InstrumentSecurityIdSource.ExchangeSymbol);

            Assert.IsNull(group.Find(InstrumentSecurityIdSource.Isin), "cannot find Isin");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.Cusip), "cannot find Cusip");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.Sedol), "cannot find Sedol");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.RicCode), "cannot find RicCode");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.BloombergSymbol), "cannot find BloombergSymbol");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.BloombergOpenSymbologyBbgid), "cannot find BloombergOpenSymbologyBbgid");
            Assert.AreEqual(value, group.Find(InstrumentSecurityIdSource.ExchangeSymbol).SecurityAlternateId, "ExchangeSymbol");
            Assert.IsNull(group.Find(InstrumentSecurityIdSource.IsoCurrencyCode), "cannot find IsoCurrencyCode");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstrumentSecurityAlternateIdGroup_Add_WhenAddingNull_Exception()
        {
            var group = new InstrumentSecurityAlternateIdGroup();
            group.Add(null, InstrumentSecurityIdSource.ExchangeSymbol);
        }

        [TestMethod]
        public void InstrumentSecurityAlternateIdGroup_Add_WhenAdded_CollectionNotEmpty()
        {
            const string value = "foo";
            var group = new InstrumentSecurityAlternateIdGroup();
            group.Add(value, InstrumentSecurityIdSource.ExchangeSymbol);

            Assert.AreEqual(1, group.Count, "count is one");
            Assert.AreEqual(1, group.Collection.Count, "collection count is one");
        }

        [TestMethod]
        public void InstrumentSecurityAlternateIdGroup_Add_WhenSameSource_ValueUpdated()
        {
            const string value1 = "foo";
            const string value2 = "bar";
            var group = new InstrumentSecurityAlternateIdGroup();
            group.Add(value1, InstrumentSecurityIdSource.ExchangeSymbol);
            group.Add(value2, InstrumentSecurityIdSource.ExchangeSymbol);

            Assert.AreEqual(1, group.Count, "count is zero");
            Assert.AreEqual(1, group.Collection.Count, "collection count is zero");
            Assert.AreEqual(value2, group.Find(InstrumentSecurityIdSource.ExchangeSymbol).SecurityAlternateId, "find");
            Assert.AreEqual(value2, group.Collection.ElementAt(0).SecurityAlternateId, "collection");
        }
    }
}
