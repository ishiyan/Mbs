``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.17134.1792 (1803/April2018Update/Redstone4)
Intel Core i7-8850H CPU 2.60GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host]     : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  DefaultJob : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT


```
|      Method |       Mean |     Error |    StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------ |-----------:|----------:|----------:|-------:|-------:|------:|----------:|
| Sequential2 |   442.9 ns |   8.72 ns |  12.22 ns | 0.2022 |      - |     - |     952 B |
|   Parallel2 | 2,717.0 ns |  54.19 ns |  74.17 ns | 0.5569 | 0.0038 |     - |    2621 B |
| Sequential3 |   658.7 ns |  13.14 ns |  32.97 ns | 0.2899 | 0.0010 |     - |    1368 B |
|   Parallel3 | 3,466.7 ns |  68.66 ns |  70.51 ns | 0.6790 | 0.0038 |     - |    3184 B |
| Sequential4 |   868.2 ns |  17.26 ns |  16.15 ns | 0.3786 | 0.0019 |     - |    1784 B |
|   Parallel4 | 4,088.1 ns |  79.64 ns | 109.01 ns | 0.7896 | 0.0076 |     - |    3703 B |
| Sequential5 | 1,139.3 ns |  22.72 ns |  38.57 ns | 0.4845 | 0.0019 |     - |    2288 B |
|   Parallel5 | 4,499.1 ns |  82.79 ns |  98.56 ns | 0.9079 | 0.0076 |     - |    4236 B |
| Sequential6 | 1,323.5 ns |  24.07 ns |  39.54 ns | 0.5741 | 0.0038 |     - |    2704 B |
|   Parallel6 | 5,031.5 ns |  98.09 ns | 127.54 ns | 1.0147 | 0.0076 |     - |    4760 B |
| Sequential7 | 1,518.8 ns |  28.20 ns |  27.70 ns | 0.6618 | 0.0057 |     - |    3120 B |
|   Parallel7 | 5,584.5 ns | 106.53 ns | 109.40 ns | 1.1368 | 0.0153 |     - |    5298 B |
| Sequential8 | 1,719.9 ns |  33.74 ns |  52.53 ns | 0.7515 | 0.0076 |     - |    3536 B |
|   Parallel8 | 6,203.5 ns | 117.16 ns | 109.59 ns | 1.2512 | 0.0153 |     - |    5832 B |
