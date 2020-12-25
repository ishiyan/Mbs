using System;
using System.ComponentModel.DataAnnotations;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;
using Mbs.Trading.Time;

namespace EuronextHistoricalInstrumentData
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable MemberCanBePrivate.Global
    internal class InstrumentHistoricalDataRequest
    {
        public string Symbol { get; set; }

        public string Isin { get; set; }

        public EuronextMic Mic { get; set; }

        public TimeGranularity TimeGranularity { get; set; }

        public InstrumentType Type { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }

        public bool AdjustedDataIfPresent { get; set; }

        public TimeSpan EndofdayClosingTime { get; set; }

        public HistoricalDataRequest HistoricalDataRequest => new HistoricalDataRequest(
            new Instrument(Symbol, Mic, Isin, Type),
            DateStart,
            DateEnd,
            TimeGranularity,
            EndofdayClosingTime,
            AdjustedDataIfPresent);

        public string Moniker =>
            $"{Symbol} - {Isin}@{Mic}@{TimeGranularity} [{DateStart.ToShortDateString()} - {DateEnd.ToShortDateString()}]@{EndofdayClosingTime.ToString()}";
    }
}
