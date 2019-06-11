using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    public static class RandomGeneratorBenchmark
    {
        #pragma warning disable CA1034 // Nested types should not be visible
        [MemoryDiagnoser]
        public class MyBenchmark
        {
            private readonly byte[] bytes = new byte[1];

            [Benchmark]
            public bool Length() { return bytes.Length > 0; }

            [Benchmark]
            // ReSharper disable once UseMethodAny.0
            public bool Count() { return bytes.Count() > 0; }

            [Benchmark]
            public bool Any() { return bytes.Any(); }
        }

        public static void Run()
        {
            BenchmarkRunner.Run<MyBenchmark>();
        }
    }
}
