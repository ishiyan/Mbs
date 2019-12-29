using System;
using System.Collections.Generic;
using Mbs.Trading.Indicators.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Mbs.Trading.Indicators.GeraldAppel
{
    [Flags]
    public enum MacdOutput
    {
        MacdLine = 0,
        SlowLine = 1,
        FastLine = 2,
        SignalLine = 4,
        MacdSignalBand = 8,
        HistogramLine = 16,
        HistogramBand = 32
    }
    public class Macd                             //  {ind_enum, param_block, output_enum} * 3, 
    {
        public List<MacdOutput> List = new List<MacdOutput>();
        public List<object> Outputs = new List<object>();
        public Macd(params MacdOutput[] outputs)
        {
            foreach (var v in outputs)
                List.Add(v);
            Outputs.Add(1.2345);
            Outputs.Add(MacdOutput.HistogramBand);
            Outputs.Add(new Band(DateTime.UtcNow,2.2,3.3));
        }
    }

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test1()
        {
            var qqq = new Macd(new[] { MacdOutput.MacdSignalBand, MacdOutput.HistogramLine });
            var value = MacdOutput.MacdSignalBand | MacdOutput.HistogramLine;
            var s1 = value.GetFlags();
            var s2 = value.GetIndividualFlags();
            var s3 = Enum.GetValues(value.GetType());
        }

    }

}
