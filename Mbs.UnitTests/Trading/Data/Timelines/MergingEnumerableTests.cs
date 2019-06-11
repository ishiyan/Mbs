using Mbs.Trading.Data;
using Mbs.Trading.Data.Timelines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Timelines
{
    [TestClass]
    public class MergingEnumerableTests
    {
        [TestMethod]
        public void MergingEnumerable_Ohlcv_WhenConstructed_InstanceIsCreated()
        {
            var target = new MergingEnumerable<Ohlcv>();
            Assert.IsNotNull(target);
        }
        [TestMethod]
        public void MergingEnumerable_Trade_WhenConstructed_InstanceIsCreated()
        {
            var target = new MergingEnumerable<Trade>();
            Assert.IsNotNull(target);
        }
        [TestMethod]
        public void MergingEnumerable_Quote_WhenConstructed_InstanceIsCreated()
        {
            var target = new MergingEnumerable<Quote>();
            Assert.IsNotNull(target);
        }
        [TestMethod]
        public void MergingEnumerable_Scalar_WhenConstructed_InstanceIsCreated()
        {
            var target = new MergingEnumerable<Scalar>();
            Assert.IsNotNull(target);
        }
    }
}
