using Mbs.Trading.Markets;
using Mbs.Trading.Time;

namespace EuronextLiveInstrumentMonitor
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    internal class InstrumentContext
    {
        public string Symbol { get; set; }

        public string Isin { get; set; }

        public ExchangeMic Mic { get; set; }

        public TimeGranularity TimeGranularity { get; set; }

        public string Type { get; set; }
    }
}
