using Mbs.Trading.Currencies;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Data.Historical.Providers.Csv;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;
using Mbs.Trading.Time;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IdentifierTypo
namespace CsvHistoricalInstrumentData
{
    internal class CsvInstrumentInfo
    {
        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Isin { get; set; }

        public ExchangeMic Mic { get; set; }

        public CurrencyCode Currency { get; set; }

        public InstrumentType Type { get; set; }

        public string Cfi { get; set; }

        public string FilePath { get; set; }

        public string ColumnIndices { get; set; }

        public string DateTimeFormat { get; set; }

        public TimeGranularity TimeGranularity { get; set; }

        public string SeparatorCharacter { get; set; }

        public string CommentCharacter { get; set; }

        public bool IsAdjustedData { get; set; }

        public CsvInfo CsvInfo => new CsvInfo(FilePath, ColumnIndices, DateTimeFormat, TimeGranularity.ToString(), SeparatorCharacter, CommentCharacter, IsAdjustedData.ToString());

        public Instrument Instrument => new Instrument(Symbol, Mic, Isin) { Name = Name, Currency = Currency, Type = Type, Cfi = Cfi };
    }
}