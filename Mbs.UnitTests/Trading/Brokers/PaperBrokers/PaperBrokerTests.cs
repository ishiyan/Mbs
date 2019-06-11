using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mbs.Trading.Brokers.Commissions;
using Mbs.Trading.Brokers.PaperBrokers;
using Mbs.Trading.Currencies;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Data.Timelines;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;
using Mbs.Trading.Orders;
using Mbs.Trading.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Brokers.PaperBrokers
{
    [TestClass]
    public class PaperBrokerTests
    {
        private sealed class MockEnumerableProvider<T> : IHistoricalData<T>
            where T : TemporalEntity
        {
            private readonly List<T> list;

            public MockEnumerableProvider(IEnumerable<T> array)
            {
                list = array.ToList();
            }

            public string Provider => "Mock";

            public async Task<IEnumerable<T>> FetchAsync(HistoricalDataRequest historicalDataRequest)
            {
                return list; // await Task.Run(() => list);
            }
        }

        private static void MockTest1<T>(
            TimeGranularity monitorGranularity, TimeGranularity feedGranularity,
            Action<Instrument> actionInstrument, Action<SingleOrder> actionSingleOrder,
            Action<Timeline> actionTimeline, Action<DataEmitter> actionDataEmitter,
            Action<PaperBroker> actionPaperBroker, Action<ISingleOrderTicket,
            SingleOrderReport> actionOnReport, Action<ISingleOrderTicket> actionOnCompletion)
            where T : TemporalEntity
        {
            var instrument = new Instrument("GLE", EuronextMic.Xpar, "FR0000130809", InstrumentType.Stock)
            {
                Currency = CurrencyCode.Eur
            };
            actionInstrument?.Invoke(instrument);

            var singleOrder = new SingleOrder
            {
                Type = OrderType.Limit, TimeInForce = OrderTimeInForce.GoodTillCanceled, Text = "mockTest1",
                Side = OrderSide.Sell, Quantity = 100d, Instrument = instrument, LimitPrice = 39d
            };
            actionSingleOrder?.Invoke(singleOrder);

            var timepiece = new SlaveStepTimepiece(new TimeSpan(9, 0, 0), new TimeSpan(17, 30, 0));
            var timeline = new Timeline
            {
                IsAsynchronous = false, BeginTime = new DateTime(2012, 10, 11), EndTime = new DateTime(2012, 10, 19)
            };
            timeline.TimeChanged += timepiece.Synchronize;
            actionTimeline?.Invoke(timeline);

            var dataEmitter = new DataEmitter();
            dataEmitter.DefaultMonitorGranularity<T>(monitorGranularity);
            foreach (var p in timeline.SubscriptionProviders<T>())
                dataEmitter.Add(p);
            dataEmitter.Monitor<T>(instrument);
            actionDataEmitter?.Invoke(dataEmitter);

            var paperBroker = new PaperBroker(timepiece, dataEmitter, null, 0d, FillOnQuote.Next, FillOnTrade.Next, FillOnOhlcv.NextOpen)
            {
                SellSideAsynchronous = false
            };
            actionPaperBroker?.Invoke(paperBroker);

            bool ordered = false;
            dataEmitter.Subscribe<T>(instrument, feedGranularity, t =>
            {
                if (!ordered)
                {
                    ordered = true;
                    paperBroker.PlaceOrder(singleOrder, actionOnReport, actionOnCompletion);
                }
            });
            timeline.Replay();
        }

        [TestMethod]
        public void PaperBroker_Timepiece_WhenSet_GetsCorrectValue()
        {
            var target = new PaperBroker();
            Assert.IsNull(target.Timepiece, "(1)");

            var timepiece = new SlaveStepTimepiece(new TimeSpan(9, 0, 0), new TimeSpan(17, 30, 0));
            target.Timepiece = timepiece;
            Assert.AreEqual(timepiece, target.Timepiece, "(2)");

            target = new PaperBroker(timepiece, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(timepiece, target.Timepiece, "(3)");

            target.Timepiece = null;
            Assert.IsNull(target.Timepiece, "(4)");
        }

        [TestMethod]
        public void PaperBroker_SellSideAsynchronous_WhenSet_GetsCorrectValue()
        {
            var target = new PaperBroker();
            Assert.IsFalse(target.SellSideAsynchronous, "(1)");

            target.SellSideAsynchronous = true;
            Assert.IsTrue(target.SellSideAsynchronous, "(2)");

            target.SellSideAsynchronous = false;
            Assert.IsFalse(target.SellSideAsynchronous, "(3)");
        }

        [TestMethod]
        public void PaperBroker_FillOnTrade_WhenSet_GetsCorrectValue()
        {
            var target = new PaperBroker { FillOnTrade = FillOnTrade.Last };
            Assert.AreEqual(FillOnTrade.Last, target.FillOnTrade, "(1)");

            target.FillOnTrade = FillOnTrade.Next;
            Assert.AreEqual(FillOnTrade.Next, target.FillOnTrade, "(2)");

            target.FillOnTrade = FillOnTrade.None;
            Assert.AreEqual(FillOnTrade.None, target.FillOnTrade, "(3)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.Last, FillOnOhlcv.None);
            Assert.AreEqual(FillOnTrade.Last, target.FillOnTrade, "(4)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.Next, FillOnOhlcv.None);
            Assert.AreEqual(FillOnTrade.Next, target.FillOnTrade, "(5)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(FillOnTrade.None, target.FillOnTrade, "(6)");
        }

        [TestMethod]
        public void PaperBroker_FillOnQuote_WhenSet_GetsCorrectValue()
        {
            var target = new PaperBroker { FillOnQuote = FillOnQuote.Last };
            Assert.AreEqual(FillOnQuote.Last, target.FillOnQuote, "(1)");

            target.FillOnQuote = FillOnQuote.Next;
            Assert.AreEqual(FillOnQuote.Next, target.FillOnQuote, "(2)");

            target.FillOnQuote = FillOnQuote.None;
            Assert.AreEqual(FillOnQuote.None, target.FillOnQuote, "(3)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.Last, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(FillOnQuote.Last, target.FillOnQuote, "(4)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.Next, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(FillOnQuote.Next, target.FillOnQuote, "(5)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(FillOnQuote.None, target.FillOnQuote, "(6)");
        }

        [TestMethod]
        public void PaperBroker_FillOnOhlcv_WhenSet_GetsCorrectValue()
        {
            var target = new PaperBroker { FillOnOhlcv = FillOnOhlcv.LastClose };
            Assert.AreEqual(FillOnOhlcv.LastClose, target.FillOnOhlcv, "(1)");

            target.FillOnOhlcv = FillOnOhlcv.NextOpen;
            Assert.AreEqual(FillOnOhlcv.NextOpen, target.FillOnOhlcv, "(2)");

            target.FillOnOhlcv = FillOnOhlcv.NextClose;
            Assert.AreEqual(FillOnOhlcv.NextClose, target.FillOnOhlcv, "(3)");

            target.FillOnOhlcv = FillOnOhlcv.NextBest;
            Assert.AreEqual(FillOnOhlcv.NextBest, target.FillOnOhlcv, "(4)");

            target.FillOnOhlcv = FillOnOhlcv.NextWorst;
            Assert.AreEqual(FillOnOhlcv.NextWorst, target.FillOnOhlcv, "(5)");

            target.FillOnOhlcv = FillOnOhlcv.NextMedian;
            Assert.AreEqual(FillOnOhlcv.NextMedian, target.FillOnOhlcv, "(6)");

            target.FillOnOhlcv = FillOnOhlcv.NextTypical;
            Assert.AreEqual(FillOnOhlcv.NextTypical, target.FillOnOhlcv, "(7)");

            target.FillOnOhlcv = FillOnOhlcv.NextWeighted;
            Assert.AreEqual(FillOnOhlcv.NextWeighted, target.FillOnOhlcv, "(8)");

            target.FillOnOhlcv = FillOnOhlcv.None;
            Assert.AreEqual(FillOnOhlcv.None, target.FillOnOhlcv, "(9)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.LastClose);
            Assert.AreEqual(FillOnOhlcv.LastClose, target.FillOnOhlcv, "(10)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.NextOpen);
            Assert.AreEqual(FillOnOhlcv.NextOpen, target.FillOnOhlcv, "(11)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.NextClose);
            Assert.AreEqual(FillOnOhlcv.NextClose, target.FillOnOhlcv, "(12)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.NextBest);
            Assert.AreEqual(FillOnOhlcv.NextBest, target.FillOnOhlcv, "(13)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.NextWorst);
            Assert.AreEqual(FillOnOhlcv.NextWorst, target.FillOnOhlcv, "(14)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.NextMedian);
            Assert.AreEqual(FillOnOhlcv.NextMedian, target.FillOnOhlcv, "(15)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.NextTypical);
            Assert.AreEqual(FillOnOhlcv.NextTypical, target.FillOnOhlcv, "(16)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.NextWeighted);
            Assert.AreEqual(FillOnOhlcv.NextWeighted, target.FillOnOhlcv, "(17)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(FillOnOhlcv.None, target.FillOnOhlcv, "(18)");
        }

        [TestMethod]
        public void PaperBroker_DataPublisher_WhenSet_GetsCorrectValue()
        {
            var target = new PaperBroker();
            Assert.IsNull(target.DataPublisher, "(1)");

            var dataPublisher = new DataEmitter();
            target.DataPublisher = dataPublisher;
            Assert.AreEqual(dataPublisher, target.DataPublisher, "(2)");

            target = new PaperBroker(null, dataPublisher, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(dataPublisher, target.DataPublisher, "(3)");

            target.DataPublisher = null;
            Assert.IsNull(target.DataPublisher, "(4)");
        }

        [TestMethod]
        public void PaperBroker_Commission_WhenSet_GetsCorrectValue()
        {
            var target = new PaperBroker();
            Assert.IsNull(target.Commission, "(1)");

            ICommission commission = new MockCommission();
            target.Commission = commission;
            Assert.AreEqual(commission, target.Commission, "(2)");

            target = new PaperBroker(null, null, commission, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(commission, target.Commission, "(3)");

            target.Commission = null;
            Assert.IsNull(target.Commission, "(4)");
        }

        [TestMethod]
        public void PaperBroker_FillQuantityRatio_WhenSet_GetsCorrectValue()
        {
            var target = new PaperBroker();
            Assert.AreEqual(0d, target.FillQuantityRatio, "(1)");

            target.FillQuantityRatio = 0.5d;
            Assert.AreEqual(0.5d, target.FillQuantityRatio, "(2)");

            target.FillQuantityRatio = 1d;
            Assert.AreEqual(1d, target.FillQuantityRatio, "(3)");

            target.FillQuantityRatio = 1.01d;
            Assert.AreEqual(1d, target.FillQuantityRatio, "(4)");

            target.FillQuantityRatio = 0d;
            Assert.AreEqual(0d, target.FillQuantityRatio, "(5)");

            target.FillQuantityRatio = -0.1d;
            Assert.AreEqual(0d, target.FillQuantityRatio, "(6)");

            target = new PaperBroker(null, null, null, 0d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(0d, target.FillQuantityRatio, "(7)");

            target = new PaperBroker(null, null, null, -0.1d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(0d, target.FillQuantityRatio, "(8)");

            target = new PaperBroker(null, null, null, 0.5d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(0.5d, target.FillQuantityRatio, "(9)");

            target = new PaperBroker(null, null, null, 1d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(1d, target.FillQuantityRatio, "(10)");

            target = new PaperBroker(null, null, null, 1.1d, FillOnQuote.None, FillOnTrade.None, FillOnOhlcv.None);
            Assert.AreEqual(1d, target.FillQuantityRatio, "(11)");
        }

        [TestMethod]
        public void PaperBroker_Constructor_WhenConstructedWithArguments_PropertiesHaveCorrectValues()
        {
            ITimepiece timepiece = new SlaveStepTimepiece(new TimeSpan(), new TimeSpan());
            IDataPublisher dataPublisher = new DataEmitter();
            ICommission commission = new MockCommission();

            var target = new PaperBroker(timepiece, dataPublisher, commission, 1d, FillOnQuote.Next, FillOnTrade.Last, FillOnOhlcv.NextOpen);

            Assert.AreEqual(timepiece, target.Timepiece, "(1)");
            Assert.AreEqual(dataPublisher, target.DataPublisher, "(2)");
            Assert.AreEqual(commission, target.Commission, "(3)");
            Assert.AreEqual(1d, target.FillQuantityRatio, "(4)");
            Assert.AreEqual(FillOnQuote.Next, target.FillOnQuote, "(5)");
            Assert.AreEqual(FillOnTrade.Last, target.FillOnTrade, "(6)");
            Assert.AreEqual(FillOnOhlcv.NextOpen, target.FillOnOhlcv, "(7)");
        }

        [TestMethod]
        public void PaperBroker_Constructor_WhenConstructedWithoutArguments_PropertiesHaveCorrectValues()
        {
            var target = new PaperBroker();

            Assert.IsNull(target.Timepiece, "(1)");
            Assert.IsNull(target.DataPublisher, "(2)");
            Assert.IsNull(target.Commission, "(3)");
            Assert.AreEqual(0d, target.FillQuantityRatio, "(4)");
            Assert.AreEqual(FillOnQuote.Last, target.FillOnQuote, "(5)");
            Assert.AreEqual(FillOnTrade.Last, target.FillOnTrade, "(6)");
            Assert.AreEqual(FillOnOhlcv.LastClose, target.FillOnOhlcv, "(7)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnTradeIsNone_MarketBuyOrderNotFilledOnTradeData()
        {
            var array = new[] {
                new Trade(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d)};
            ISingleOrderTicket sot = null;

            MockTest1<Trade>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => { },
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Buy; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Trade>(array)),
                d => { }, p => { p.FillOnTrade = FillOnTrade.None; },
                (ti, tr) => { sot = ti; }, st => { });
            Thread.Sleep(5000);
            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.New, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(0d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(0d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(0d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(0d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(100d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(2, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[1], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[1] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[1] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[1] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[1] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[1] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[1] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[1] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[1] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[1] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[1] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[1] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[1] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[1] (13)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnLastTrade_MarketBuyOrderIsFilledOnTradeData()
        {
            var array = new[] {
                new Trade(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d)};
            ISingleOrderTicket sot = null;

            MockTest1<Trade>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => {},
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Buy; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Trade>(array)),
                d => {}, p => { p.FillOnTrade = FillOnTrade.Last; },
                (ti, tr) => {}, st => { sot = st; });

            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(37.1d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(100d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(37.1d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(100d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(0d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(3, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[2], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[1] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[1] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[1] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[1] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[1] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[1] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[1] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[1] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[1] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[1] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[1] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[1] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[1] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[2].TransactionTime.ToCompactString(), "reports[2] (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.Reports[2].Status, "reports[2] (2)");
            Assert.AreEqual(OrderReportType.Filled, sot.Reports[2].ReportType, "reports[2] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[2].Text, "reports[2] (4)");
            Assert.AreEqual(37.1d, sot.Reports[2].AveragePrice, "reports[2] (5)");
            Assert.AreEqual(100d, sot.Reports[2].CumulativeQuantity, "reports[2] (6)");
            Assert.AreEqual(0d, sot.Reports[2].CumulativeCommission, "reports[2] (7)");
            Assert.AreEqual(37.1d, sot.Reports[2].LastPrice, "reports[2] (8)");
            Assert.AreEqual(100d, sot.Reports[2].LastQuantity, "reports[2] (9)");
            Assert.AreEqual(0d, sot.Reports[2].LeavesQuantity, "reports[2] (10)");
            Assert.AreEqual(0d, sot.Reports[2].LastCommission, "reports[2] (11)");
            Assert.IsNull(sot.Reports[2].ReplaceSourceOrder, "reports[2] (12)");
            Assert.IsNull(sot.Reports[2].ReplaceTargetOrder, "reports[2] (13)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnNextTrade_MarketBuyOrderIsFilledOnTradeData()
        {
            var array = new[] {
                new Trade(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d)};
            ISingleOrderTicket sot = null;

            MockTest1<Trade>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => { },
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Buy; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Trade>(array)),
                d => { }, p => { p.FillOnTrade = FillOnTrade.Next; },
                (ti, tr) => { }, st => { sot = st; });

            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(38.1d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(100d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(38.1d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(100d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(0d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(3, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[2], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[1] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[1] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[1] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[1] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[1] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[1] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[1] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[1] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[1] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[1] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[1] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[1] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[1] (13)");

            Assert.AreEqual("20121011:090002", sot.Reports[2].TransactionTime.ToCompactString(), "reports[2] (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.Reports[2].Status, "reports[2] (2)");
            Assert.AreEqual(OrderReportType.Filled, sot.Reports[2].ReportType, "reports[2] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[2].Text, "reports[2] (4)");
            Assert.AreEqual(38.1d, sot.Reports[2].AveragePrice, "reports[2] (5)");
            Assert.AreEqual(100d, sot.Reports[2].CumulativeQuantity, "reports[2] (6)");
            Assert.AreEqual(0d, sot.Reports[2].CumulativeCommission, "reports[2] (7)");
            Assert.AreEqual(38.1d, sot.Reports[2].LastPrice, "reports[2] (8)");
            Assert.AreEqual(100d, sot.Reports[2].LastQuantity, "reports[2] (9)");
            Assert.AreEqual(0d, sot.Reports[2].LeavesQuantity, "reports[2] (10)");
            Assert.AreEqual(0d, sot.Reports[2].LastCommission, "reports[2] (11)");
            Assert.IsNull(sot.Reports[2].ReplaceSourceOrder, "reports[2] (12)");
            Assert.IsNull(sot.Reports[2].ReplaceTargetOrder, "reports[2] (13)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnTradeIsNone_MarketSellOrderNotFilledOnTradeData()
        {
            var array = new[] {
                new Trade(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d)};
            ISingleOrderTicket sot = null;

            MockTest1<Trade>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => { },
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Sell; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Trade>(array)),
                d => { }, p => { p.FillOnTrade = FillOnTrade.None; },
                (ti, tr) => { sot = ti; }, st => { });

            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.New, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(0d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(0d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(0d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(0d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(100d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(2, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[1], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[2] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[2] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[2] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[2] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[2] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[2] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[2] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[2] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[2] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[2] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[2] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[2] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[2] (13)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnLastTrade_MarketSellOrderIsFilledOnTradeData()
        {
            var array = new[] {
                new Trade(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d)};
            ISingleOrderTicket sot = null;

            MockTest1<Trade>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => { },
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Sell; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Trade>(array)),
                d => { }, p => { p.FillOnTrade = FillOnTrade.Last; },
                (ti, tr) => { }, st => { sot = st; });

            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(37.1d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(100d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(37.1d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(100d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(0d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(3, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[2], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[1] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[1] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[1] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[1] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[1] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[1] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[1] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[1] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[1] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[1] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[1] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[1] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[1] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[2].TransactionTime.ToCompactString(), "reports[2] (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.Reports[2].Status, "reports[2] (2)");
            Assert.AreEqual(OrderReportType.Filled, sot.Reports[2].ReportType, "reports[2] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[2].Text, "reports[2] (4)");
            Assert.AreEqual(37.1d, sot.Reports[2].AveragePrice, "reports[2] (5)");
            Assert.AreEqual(100d, sot.Reports[2].CumulativeQuantity, "reports[2] (6)");
            Assert.AreEqual(0d, sot.Reports[2].CumulativeCommission, "reports[2] (7)");
            Assert.AreEqual(37.1d, sot.Reports[2].LastPrice, "reports[2] (8)");
            Assert.AreEqual(100d, sot.Reports[2].LastQuantity, "reports[2] (9)");
            Assert.AreEqual(0d, sot.Reports[2].LeavesQuantity, "reports[2] (10)");
            Assert.AreEqual(0d, sot.Reports[2].LastCommission, "reports[2] (11)");
            Assert.IsNull(sot.Reports[2].ReplaceSourceOrder, "reports[2] (12)");
            Assert.IsNull(sot.Reports[2].ReplaceTargetOrder, "reports[2] (13)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnNextTrade_MarketSellOrderIsFilledOnTradeData()
        {
            var array = new[] {
                new Trade(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d),
                new Trade(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d)};
            ISingleOrderTicket sot = null;

            MockTest1<Trade>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => { },
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Sell; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Trade>(array)),
                d => { }, p => { p.FillOnTrade = FillOnTrade.Next; },
                (ti, tr) => { }, st => { sot = st; });

            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(38.1d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(100d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(38.1d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(100d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(0d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(3, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[2], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[1] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[1] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[1] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[1] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[1] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[1] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[1] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[1] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[1] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[1] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[1] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[1] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[1] (13)");

            Assert.AreEqual("20121011:090002", sot.Reports[2].TransactionTime.ToCompactString(), "reports[2] (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.Reports[2].Status, "reports[2] (2)");
            Assert.AreEqual(OrderReportType.Filled, sot.Reports[2].ReportType, "reports[2] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[2].Text, "reports[2] (4)");
            Assert.AreEqual(38.1d, sot.Reports[2].AveragePrice, "reports[2] (5)");
            Assert.AreEqual(100d, sot.Reports[2].CumulativeQuantity, "reports[2] (6)");
            Assert.AreEqual(0d, sot.Reports[2].CumulativeCommission, "reports[2] (7)");
            Assert.AreEqual(38.1d, sot.Reports[2].LastPrice, "reports[2] (8)");
            Assert.AreEqual(100d, sot.Reports[2].LastQuantity, "reports[2] (9)");
            Assert.AreEqual(0d, sot.Reports[2].LeavesQuantity, "reports[2] (10)");
            Assert.AreEqual(0d, sot.Reports[2].LastCommission, "reports[2] (11)");
            Assert.IsNull(sot.Reports[2].ReplaceSourceOrder, "reports[2] (12)");
            Assert.IsNull(sot.Reports[2].ReplaceTargetOrder, "reports[2] (13)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnLastQuote_MarketBuyOrderIsFilledOnQuoteData()
        {
            var array = new[] {
                new Quote(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d, 47.1d, 200d),
                new Quote(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d, 48.1d, 100d),
                new Quote(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d, 49.1d, 200d)};
            ISingleOrderTicket sot = null;

            MockTest1<Quote>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => { },
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Buy; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Quote>(array)),
                d => { }, p => { p.FillOnQuote = FillOnQuote.Last; },
                (ti, tr) => { }, st => { sot = st; });

            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(47.1d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(100d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(47.1d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(100d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(0d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(3, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[2], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[1] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[1] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[1] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[1] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[1] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[1] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[1] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[1] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[1] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[1] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[1] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[1] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[1] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[2].TransactionTime.ToCompactString(), "reports[2] (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.Reports[2].Status, "reports[2] (2)");
            Assert.AreEqual(OrderReportType.Filled, sot.Reports[2].ReportType, "reports[2] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[2].Text, "reports[2] (4)");
            Assert.AreEqual(47.1d, sot.Reports[2].AveragePrice, "reports[2] (5)");
            Assert.AreEqual(100d, sot.Reports[2].CumulativeQuantity, "reports[2] (6)");
            Assert.AreEqual(0d, sot.Reports[2].CumulativeCommission, "reports[2] (7)");
            Assert.AreEqual(47.1d, sot.Reports[2].LastPrice, "reports[2] (8)");
            Assert.AreEqual(100d, sot.Reports[2].LastQuantity, "reports[2] (9)");
            Assert.AreEqual(0d, sot.Reports[2].LeavesQuantity, "reports[2] (10)");
            Assert.AreEqual(0d, sot.Reports[2].LastCommission, "reports[2] (11)");
            Assert.IsNull(sot.Reports[2].ReplaceSourceOrder, "reports[2] (12)");
            Assert.IsNull(sot.Reports[2].ReplaceTargetOrder, "reports[2] (13)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnNextQuote_MarketBuyOrderIsFilledOnQuoteData()
        {
            var array = new[] {
                new Quote(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d, 47.1d, 200d),
                new Quote(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d, 48.1d, 100d),
                new Quote(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d, 49.1d, 200d)};
            ISingleOrderTicket sot = null;

            MockTest1<Quote>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => { },
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Buy; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Quote>(array)),
                d => { }, p => { p.FillOnQuote = FillOnQuote.Next; },
                (ti, tr) => { }, st => { sot = st; });

            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(48.1d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(100d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(48.1d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(100d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(0d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(3, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[2], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[1] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[1] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[1] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[1] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[1] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[1] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[1] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[1] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[1] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[1] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[1] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[1] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[1] (13)");

            Assert.AreEqual("20121011:090002", sot.Reports[2].TransactionTime.ToCompactString(), "reports[2] (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.Reports[2].Status, "reports[2] (2)");
            Assert.AreEqual(OrderReportType.Filled, sot.Reports[2].ReportType, "reports[2] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[2].Text, "reports[2] (4)");
            Assert.AreEqual(48.1d, sot.Reports[2].AveragePrice, "reports[2] (5)");
            Assert.AreEqual(100d, sot.Reports[2].CumulativeQuantity, "reports[6] (1)");
            Assert.AreEqual(0d, sot.Reports[2].CumulativeCommission, "reports[2] (7)");
            Assert.AreEqual(48.1d, sot.Reports[2].LastPrice, "reports[2] (8)");
            Assert.AreEqual(100d, sot.Reports[2].LastQuantity, "reports[2] (9)");
            Assert.AreEqual(0d, sot.Reports[2].LeavesQuantity, "reports[2] (10)");
            Assert.AreEqual(0d, sot.Reports[2].LastCommission, "reports[2] (11)");
            Assert.IsNull(sot.Reports[2].ReplaceSourceOrder, "reports[2] (12)");
            Assert.IsNull(sot.Reports[2].ReplaceTargetOrder, "reports[2] (13)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnLastQuote_MarketSellOrderIsFilledOnQuoteData()
        {
            var array = new[] {
                new Quote(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d, 47.1d, 200d),
                new Quote(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d, 48.1d, 100d),
                new Quote(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d, 49.1d, 200d)};
            ISingleOrderTicket sot = null;

            MockTest1<Quote>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => { },
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Sell; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Quote>(array)),
                d => { }, p => { p.FillOnQuote = FillOnQuote.Last; },
                (ti, tr) => { }, st => { sot = st; });

            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(37.1d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(100d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(37.1d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(100d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(0d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(3, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[2], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[1] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[1] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[1] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[1] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[1] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[1] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[1] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[1] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[1] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[1] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[1] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[1] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[1] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[2].TransactionTime.ToCompactString(), "reports[2] (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.Reports[2].Status, "reports[2] (2)");
            Assert.AreEqual(OrderReportType.Filled, sot.Reports[2].ReportType, "reports[2] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[2].Text, "reports[2] (4)");
            Assert.AreEqual(37.1d, sot.Reports[2].AveragePrice, "reports[2] (5)");
            Assert.AreEqual(100d, sot.Reports[2].CumulativeQuantity, "reports[2] (6)");
            Assert.AreEqual(0d, sot.Reports[2].CumulativeCommission, "reports[2] (7)");
            Assert.AreEqual(37.1d, sot.Reports[2].LastPrice, "reports[2] (8)");
            Assert.AreEqual(100d, sot.Reports[2].LastQuantity, "reports[2] (9)");
            Assert.AreEqual(0d, sot.Reports[2].LeavesQuantity, "reports[2] (10)");
            Assert.AreEqual(0d, sot.Reports[2].LastCommission, "reports[2] (11)");
            Assert.IsNull(sot.Reports[2].ReplaceSourceOrder, "reports[2] (12)");
            Assert.IsNull(sot.Reports[2].ReplaceTargetOrder, "reports[2] (13)");
        }

        [TestMethod]
        public void PaperBroker_PlaceOrder_WhenFillOnNextQuote_MarketSellOrderIsFilledOnQuoteData()
        {
            var array = new[] {
                new Quote(new DateTime(2012, 10, 11, 9, 0, 1), 37.1d, 200d, 47.1d, 200d),
                new Quote(new DateTime(2012, 10, 11, 9, 0, 2), 38.1d, 100d, 48.1d, 100d),
                new Quote(new DateTime(2012, 10, 11, 9, 0, 3), 39.1d, 300d, 49.1d, 200d)};
            ISingleOrderTicket sot = null;

            MockTest1<Quote>(TimeGranularity.Aperiodic, TimeGranularity.Aperiodic, i => { },
                s => { s.Type = OrderType.Market; s.Side = OrderSide.Sell; s.Quantity = 100d; },
                t => t.Add(new MockEnumerableProvider<Quote>(array)),
                d => { }, p => { p.FillOnQuote = FillOnQuote.Next; },
                (ti, tr) => { }, st => { sot = st; });

            Assert.IsNotNull(sot, "sot (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.OrderStatus, "sot (2)");
            Assert.AreEqual(38.1d, sot.AveragePrice, "sot (3)");
            Assert.AreEqual(100d, sot.CumulativeQuantity, "sot (4)");
            Assert.AreEqual(0d, sot.CumulativeCommission, "sot (5)");
            Assert.AreEqual(38.1d, sot.LastPrice, "sot (6)");
            Assert.AreEqual(100d, sot.LastQuantity, "sot (7)");
            Assert.AreEqual(0d, sot.LeavesQuantity, "sot (8)");
            Assert.AreEqual(0d, sot.LastCommission, "sot (9)");
            Assert.AreEqual(3, sot.Reports.Count, "sot (10)");
            Assert.AreEqual(sot.LastReport, sot.Reports[2], "sot (11)");

            Assert.AreEqual("20121011:090001", sot.Reports[0].TransactionTime.ToCompactString(), "reports[0] (1)");
            Assert.AreEqual(OrderStatus.PendingNew, sot.Reports[0].Status, "reports[0] (2)");
            Assert.AreEqual(OrderReportType.PendingNew, sot.Reports[0].ReportType, "reports[0] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[0].Text, "reports[0] (4)");
            Assert.AreEqual(0d, sot.Reports[0].AveragePrice, "reports[0] (5)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeQuantity, "reports[0] (6)");
            Assert.AreEqual(0d, sot.Reports[0].CumulativeCommission, "reports[0] (7)");
            Assert.AreEqual(0d, sot.Reports[0].LastPrice, "reports[0] (8)");
            Assert.AreEqual(0d, sot.Reports[0].LastQuantity, "reports[0] (9)");
            Assert.AreEqual(100d, sot.Reports[0].LeavesQuantity, "reports[0] (10)");
            Assert.AreEqual(0d, sot.Reports[0].LastCommission, "reports[0] (11)");
            Assert.IsNull(sot.Reports[0].ReplaceSourceOrder, "reports[0] (12)");
            Assert.IsNull(sot.Reports[0].ReplaceTargetOrder, "reports[0] (13)");

            Assert.AreEqual("20121011:090001", sot.Reports[1].TransactionTime.ToCompactString(), "reports[1] (1)");
            Assert.AreEqual(OrderStatus.New, sot.Reports[1].Status, "reports[1] (2)");
            Assert.AreEqual(OrderReportType.New, sot.Reports[1].ReportType, "reports[1] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[1].Text, "reports[1] (4)");
            Assert.AreEqual(0d, sot.Reports[1].AveragePrice, "reports[1] (5)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeQuantity, "reports[1] (6)");
            Assert.AreEqual(0d, sot.Reports[1].CumulativeCommission, "reports[1] (7)");
            Assert.AreEqual(0d, sot.Reports[1].LastPrice, "reports[1] (8)");
            Assert.AreEqual(0d, sot.Reports[1].LastQuantity, "reports[1] (9)");
            Assert.AreEqual(100d, sot.Reports[1].LeavesQuantity, "reports[1] (10)");
            Assert.AreEqual(0d, sot.Reports[1].LastCommission, "reports[1] (11)");
            Assert.IsNull(sot.Reports[1].ReplaceSourceOrder, "reports[1] (12)");
            Assert.IsNull(sot.Reports[1].ReplaceTargetOrder, "reports[1] (13)");

            Assert.AreEqual("20121011:090002", sot.Reports[2].TransactionTime.ToCompactString(), "reports[2] (1)");
            Assert.AreEqual(OrderStatus.Filled, sot.Reports[2].Status, "reports[2] (2)");
            Assert.AreEqual(OrderReportType.Filled, sot.Reports[2].ReportType, "reports[2] (3)");
            Assert.AreEqual(string.Empty, sot.Reports[2].Text, "reports[2] (4)");
            Assert.AreEqual(38.1d, sot.Reports[2].AveragePrice, "reports[2] (5)");
            Assert.AreEqual(100d, sot.Reports[2].CumulativeQuantity, "reports[2] (6)");
            Assert.AreEqual(0d, sot.Reports[2].CumulativeCommission, "reports[2] (7)");
            Assert.AreEqual(38.1d, sot.Reports[2].LastPrice, "reports[2] (8)");
            Assert.AreEqual(100d, sot.Reports[2].LastQuantity, "reports[2] (9)");
            Assert.AreEqual(0d, sot.Reports[2].LeavesQuantity, "reports[2] (10)");
            Assert.AreEqual(0d, sot.Reports[2].LastCommission, "reports[2] (11)");
            Assert.IsNull(sot.Reports[2].ReplaceSourceOrder, "reports[2] (12)");
            Assert.IsNull(sot.Reports[2].ReplaceTargetOrder, "reports[2] (13)");
        }
    }
}
