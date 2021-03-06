using System.IO;
using Mbs.Trading.Data.Historical.Providers.Csv;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Historical.Providers.Csv
{
    [TestClass]
    public class CsvHistoricalDataTests
    {
        [TestMethod]
        public void CsvHistoricalData_Add_FileNameNull_NotAdded()
        {
            var instrument = Add(pathNull: true);

            var info = CsvHistoricalData.InstrumentInfo(instrument);

            Assert.IsNull(info);
        }

        [TestMethod]
        public void CsvHistoricalData_Add_FileNameEmpty_NotAdded()
        {
            var instrument = Add(pathEmpty: true);

            var info = CsvHistoricalData.InstrumentInfo(instrument);

            Assert.IsNull(info);
        }

        [TestMethod]
        public void CsvHistoricalData_Add_FileNotFound_NotAdded()
        {
            var instrument = Add(fileFound: false);

            var info = CsvHistoricalData.InstrumentInfo(instrument);

            Assert.IsNull(info);
        }

        [TestMethod]
        public void CsvHistoricalData_Add_FileFound_FirstInfo_Added()
        {
            var instrument = Add();

            var info = CsvHistoricalData.InstrumentInfo(instrument);

            Assert.IsNotNull(info);
            Assert.IsNotNull(info.GetFirstData(CsvDataType.Trade));
            Assert.IsNull(info.GetFirstData(CsvDataType.Ohlcv));
        }

        [TestMethod]
        public void CsvHistoricalData_Add_FileFound_SecondInfo_Added()
        {
            var instrument = Add();
            Add(columnSet: CsvColumnSet.Tohlcv);

            var info = CsvHistoricalData.InstrumentInfo(instrument);

            Assert.IsNotNull(info);
            Assert.IsNotNull(info.GetFirstData(CsvDataType.Trade));
            Assert.IsNotNull(info.GetFirstData(CsvDataType.Ohlcv));
        }

        private static Instrument Add(
            ExchangeMic mic = ExchangeMic.Xams,
            string symbol = "BESI",
            string isin = "NL0012866412",
            CsvColumnSet columnSet = CsvColumnSet.Tpv,
            bool fileFound = true,
            bool pathNull = false,
            bool pathEmpty = false)
        {
            var path = Path.GetTempFileName();
            if (fileFound)
            {
                File.WriteAllText(path, string.Empty);
            }

            try
            {
                var p = path;
                if (pathNull)
                {
                    p = null;
                }
                else if (pathEmpty)
                {
                    p = string.Empty;
                }
                else if (!fileFound)
                {
                    p = string.Concat(p, ".foo");
                }

                var csvInfo = new CsvInfo(p, columnSet);
                var instrument = new Instrument
                {
                    Exchange = new Exchange(mic),
                    Symbol = symbol,
                    SecurityIdSource = InstrumentSecurityIdSource.Isin,
                    SecurityId = isin,
                };

                CsvHistoricalData.Add(instrument, csvInfo);
                return instrument;
            }
            finally
            {
                if (fileFound)
                {
                    File.Delete(path);
                }
            }
        }
    }
}
