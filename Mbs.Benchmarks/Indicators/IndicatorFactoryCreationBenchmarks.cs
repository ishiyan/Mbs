﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Mbs.Trading.Indicators;
using Mbs.Trading.Indicators.Abstractions;

namespace Mbs.Benchmarks.Indicators
{
    public static class IndicatorFactoryCreationBenchmarks
    {
        public static void Run()
        {
            BenchmarkRunner.Run<IndicatorFactoryCreation>();
        }

        [MemoryDiagnoser]
        public class IndicatorFactoryCreation
        {
            private static readonly IndicatorInput Input = new IndicatorInput
            {
                IndicatorType = IndicatorType.SimpleMovingAverage,
                Parameters = new SimpleMovingAverage.Parameters { Length = 7 },
                OutputKinds = new[] { (int)SimpleMovingAverage.OutputKind.Value }
            };

            private static readonly IndicatorInput[] Input2 = { Input, Input };
            private static readonly IndicatorInput[] Input3 = { Input, Input, Input };
            private static readonly IndicatorInput[] Input4 = { Input, Input, Input, Input };
            private static readonly IndicatorInput[] Input5 = { Input, Input, Input, Input, Input };
            private static readonly IndicatorInput[] Input6 = { Input, Input, Input, Input, Input, Input };
            private static readonly IndicatorInput[] Input7 = { Input, Input, Input, Input, Input, Input, Input };
            private static readonly IndicatorInput[] Input8 = { Input, Input, Input, Input, Input, Input, Input, Input };

            [Benchmark]
            public void Sequential2()
            {
                TestIndicatorFactory.CreateSequential(Input2);
            }

            [Benchmark]
            public void Parallel2()
            {
                TestIndicatorFactory.CreateParallel(Input2);
            }

            [Benchmark]
            public void Sequential3()
            {
                TestIndicatorFactory.CreateSequential(Input3);
            }

            [Benchmark]
            public void Parallel3()
            {
                TestIndicatorFactory.CreateParallel(Input3);
            }

            [Benchmark]
            public void Sequential4()
            {
                TestIndicatorFactory.CreateSequential(Input4);
            }

            [Benchmark]
            public void Parallel4()
            {
                TestIndicatorFactory.CreateParallel(Input4);
            }

            [Benchmark]
            public void Sequential5()
            {
                TestIndicatorFactory.CreateSequential(Input5);
            }

            [Benchmark]
            public void Parallel5()
            {
                TestIndicatorFactory.CreateParallel(Input5);
            }

            [Benchmark]
            public void Sequential6()
            {
                TestIndicatorFactory.CreateSequential(Input6);
            }

            [Benchmark]
            public void Parallel6()
            {
                TestIndicatorFactory.CreateParallel(Input6);
            }

            [Benchmark]
            public void Sequential7()
            {
                TestIndicatorFactory.CreateSequential(Input7);
            }

            [Benchmark]
            public void Parallel7()
            {
                TestIndicatorFactory.CreateParallel(Input7);
            }

            [Benchmark]
            public void Sequential8()
            {
                TestIndicatorFactory.CreateSequential(Input8);
            }

            [Benchmark]
            public void Parallel8()
            {
                TestIndicatorFactory.CreateParallel(Input8);
            }
        }

        [MemoryDiagnoser]
        public static class TestIndicatorFactory
        {
            public static IEnumerable<IIndicator> CreateParallel(IEnumerable<IndicatorInput> inputs)
            {
                var inputArray = inputs.ToArray();
                var length = inputArray.Length;
                var instanceArray = new IIndicator[length];

                Parallel.For(0, length,
                    index => { instanceArray[index] = IndicatorFactory.Create(inputArray[index]); });

                return instanceArray;
            }

            public static IEnumerable<IIndicator> CreateSequential(IEnumerable<IndicatorInput> inputs)
            {
                var list = new List<IIndicator>();
                foreach (var input in inputs)
                {
                    list.Add(IndicatorFactory.Create(input));
                }

                return list;
            }
        }
    }
}
