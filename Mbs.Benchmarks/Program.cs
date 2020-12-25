using EuronextEndofdayJsonParsing;
using Mbs.Benchmarks.Indicators;

namespace Mbs.Benchmarks
{
    public static class Program
    {
        public static void Main()
        {
            EuronextEndofdayJsonParsingBenchmark.Run();
            IndicatorFactoryCreationBenchmarks.Run();
            ArrayCopyVersusForLoopBenchmarks.Run();
        }
    }
}
