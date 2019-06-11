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
// ReSharper disable IdentifierTypo
namespace CsvHistoricalInstrumentData
{
    internal class InstrumentHistoricalDataRequest
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

        public Type EntityType
        {
            get
            {
                if (Entity == null)
                    throw new ArgumentNullException(nameof(Entity));

                switch (Entity.ToLowerInvariant())
                {
                    case "ohlcv":
                        return typeof(Ohlcv);
                    case "trade":
                        return typeof(Trade);
                    case "quote":
                        return typeof(Quote);
                    case "scalar":
                        return typeof(Scalar);
                }

                throw new ArgumentException(nameof(Entity), $"Unknown entity type: {Entity}");
            }
        }

        public HistoricalDataRequest HistoricalDataRequest => new HistoricalDataRequest(
            new Instrument(Symbol, Mic, Isin) { Type = Type },
            DateStart,
            DateEnd,
            TimeGranularity,
            EndofdayClosingTime,
            AdjustedDataIfPresent);

        public string Moniker =>
            $"{Symbol} - {Isin}@{Mic}@{TimeGranularity} [{DateStart.ToShortDateString()} - {DateEnd.ToShortDateString()}]@{EndofdayClosingTime.ToString()}";

    }
}
