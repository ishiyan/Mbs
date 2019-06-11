/*
using System;
using System.Linq;
using Mbs.Trading.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Orders
{
    [TestClass]
    public class EnumTests
    {
        // ReSharper disable InconsistentNaming
        [TestMethod]
        public void Enum_OrderType_Values_AreVerified()
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
*/