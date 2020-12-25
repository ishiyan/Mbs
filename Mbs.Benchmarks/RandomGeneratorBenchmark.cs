using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Mbs.Benchmarks
{
    public static class RandomGeneratorBenchmark
    {
        // ReSharper disable once UnusedMember.Global
        public static void Run()
        {
            BenchmarkRunner.Run<MyBenchmark>();
        }

        [MemoryDiagnoser]
        public class MyBenchmark
        {
            private readonly byte[] bytes = new byte[1];

            [Benchmark]
            public bool Length()
            {
                return bytes.Length > 0;
            }

#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
#pragma warning disable S1155 // "Any()" should be used to test for emptiness
            [Benchmark]
            public bool Count()
            {
                // ReSharper disable once UseMethodAny.0
                // ReSharper disable once UseCollectionCountProperty
                return bytes.Count() > 0;
            }
#pragma warning restore S1155
#pragma warning restore CA1829

            [Benchmark]
            public bool Any()
            {
                return bytes.Any();
            }
        }
    }
}
