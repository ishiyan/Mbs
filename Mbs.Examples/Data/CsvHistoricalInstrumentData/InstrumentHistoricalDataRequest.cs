using System;
using System.ComponentModel.DataAnnotations;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;
using Mbs.Trading.Time;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace CsvHistoricalInstrumentData
{
    internal sealed class InstrumentHistoricalDataRequest
    {
        public string Symbol { get; set; }

        public string Isin { get; set; }

        public ExchangeMic Mic { get; set; }

        public TimeGranularity TimeGranularity { get; set; }

        public InstrumentType Type { get; set; }

        [DataType(DataType.Date)]

        public DateTime DateStart { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }

        public bool AdjustedDataIfPresent { get; set; }

        public TimeSpan EndofdayClosingTime { get; set; }

        [DataType("Type")]
        public string Entity { get; set; }

        public HistoricalDataRequest HistoricalDataRequest => new HistoricalDataRequest(
            new Instrument(Symbol, Mic, Isin) { Type = Type },
            DateStart,
            DateEnd,
            TimeGranularity,
            EndofdayClosingTime,
            AdjustedDataIfPresent);

        public string Moniker =>
            $"{Symbol} - {Isin}@{Mic}@{TimeGranularity} [{DateStart.ToShortDateString()} - {DateEnd.ToShortDateString()}]@{EndofdayClosingTime.ToString()}";

        public Type GetEntityType()
        {
            if (Entity == null)
            {
                throw new ArgumentException("Entity cannot be null");
            }

            return Entity.ToLowerInvariant() switch
            {
                "ohlcv" => typeof(Ohlcv),
                "trade" => typeof(Trade),
                "quote" => typeof(Quote),
                "scalar" => typeof(Scalar),
                _ => throw new ArgumentException(nameof(Entity), $"Unknown entity type: {Entity}")
            };
        }
    }
}
