using System;
using System.Reflection;
using Mbs.Trading.Instruments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Instruments
{
    [TestClass]
    public class InstrumentSecurityAlternateIdTests
    {
        private static readonly ConstructorInfo Ctor = typeof(InstrumentSecurityAlternateId).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic, null,
            new[] { typeof(string), typeof(InstrumentSecurityIdSource) }, null);

        private static InstrumentSecurityAlternateId CreateInstance(string value, InstrumentSecurityIdSource source)
        {
            try
            {
                return Ctor.Invoke(new object[] { value, source }) as InstrumentSecurityAlternateId;
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void InstrumentSecurityAlternateId_Constructor_WhenConstructed_GettersReturnCorrectValues()
        {
            const InstrumentSecurityIdSource source = InstrumentSecurityIdSource.ExchangeSymbol;
            const string value = "foo";
            var id = CreateInstance(value, source);

            Assert.AreEqual(value, id.SecurityAlternateId, "value");
            Assert.AreEqual(source, id.SecurityAlternateIdSource, "source");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstrumentSecurityAlternateId_Constructor_WhenValueNull_Exception()
        {
            CreateInstance(null, InstrumentSecurityIdSource.ExchangeSymbol);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstrumentSecurityAlternateId_Constructor_WhenValueEmpty_Exception()
        {
            CreateInstance(string.Empty, InstrumentSecurityIdSource.ExchangeSymbol);
        }
    }
}
