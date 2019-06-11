using System;
using Mbs.Trading.Data.Timelines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Timelines
{
    [TestClass]
    public class TimelineTests
    {
        [TestMethod]
        public void Timeline_InteractiveDelayMilliseconds_WhenSet_GetsCorrectValue()
        {
            const int defaultValue = 100;
            const int expectedValue = 3456;

            var timeline = new Mbs.Trading.Data.Timelines.Timeline();
            Assert.AreEqual(defaultValue, timeline.InteractiveDelayMilliseconds);

            timeline.InteractiveDelayMilliseconds = expectedValue;
            Assert.AreEqual(expectedValue, timeline.InteractiveDelayMilliseconds);
        }

        [TestMethod]
        public void Timeline_IsAsynchronous_WhenSet_GetsCorrectValue()
        {
            const bool defaultValue = true;
            const bool expectedValue = false;

            var timeline = new Mbs.Trading.Data.Timelines.Timeline();
            Assert.AreEqual(defaultValue, timeline.IsAsynchronous);

            timeline.IsAsynchronous = expectedValue;
            Assert.AreEqual(expectedValue, timeline.IsAsynchronous);
        }

        [TestMethod]
        public void Timeline_State_WhenConstructed_GetsCorrectValue()
        {
            const TimelineState defaultState = TimelineState.Stop;

            var timeline = new Mbs.Trading.Data.Timelines.Timeline();
            Assert.AreEqual(defaultState, timeline.State);
        }

        [TestMethod]
        public void Timeline_Time_WhenSet_GetsCorrectValue()
        {
            var defaultValue = new DateTime(0L);
            var expectedValue = DateTime.Now;

            var timeline = new Mbs.Trading.Data.Timelines.Timeline();
            Assert.AreEqual(defaultValue, timeline.Time);

            timeline.BeginTime = expectedValue;
            Assert.AreEqual(expectedValue, timeline.Time);
        }

        [TestMethod]
        public void Timeline_BeginTime_WhenSet_GetsCorrectValue()
        {
            var defaultValue = new DateTime(0L);
            var expectedValue = DateTime.Now;

            var timeline = new Mbs.Trading.Data.Timelines.Timeline();
            Assert.AreEqual(defaultValue, timeline.BeginTime);

            timeline.BeginTime = expectedValue;
            Assert.AreEqual(expectedValue, timeline.BeginTime);
        }

        [TestMethod]
        public void Timeline_EndTime_WhenSet_GetsCorrectValue()
        {
            var defaultValue = new DateTime(0L);
            var expectedValue = DateTime.Now;

            var timeline = new Mbs.Trading.Data.Timelines.Timeline();
            Assert.AreEqual(defaultValue, timeline.EndTime);

            timeline.EndTime = expectedValue;
            Assert.AreEqual(expectedValue, timeline.EndTime);
        }
    }
}
