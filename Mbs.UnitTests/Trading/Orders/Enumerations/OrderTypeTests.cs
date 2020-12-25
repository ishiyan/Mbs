using System;
using System.Linq;
using Mbs.Trading.Orders.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Orders.Enumerations
{
    [TestClass]
    public class OrderTypeTests
    {
        [TestMethod]
        public void OrderType_Values_AreVerified()
        {
            foreach (var v in Enum.GetValues(typeof(OrderType)).Cast<OrderType>().ToList())
            {
                switch (v)
                {
                    case OrderType.Limit:
                    case OrderType.LimitIfTouched:
                    case OrderType.LimitOnClose:
                    case OrderType.Market:
                    case OrderType.MarketIfTouched:
                    case OrderType.MarketOnClose:
                    case OrderType.MarketToLimit:
                    case OrderType.Stop:
                    case OrderType.StopLimit:
                    case OrderType.TrailingStop:
                        break;

                    default:
                        Assert.Fail($"Enum value OrderType.{v} is unknown.");
                        return;
                }
            }

            Assert.IsTrue(true);
        }
    }
}
