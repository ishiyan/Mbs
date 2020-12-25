using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
#pragma warning disable CA1822 // Mark members as static

namespace Mbs.Benchmarks.Indicators
{
    public static class ArrayCopyVersusForLoopBenchmarks
    {
        public static void Run()
        {
            BenchmarkRunner.Run<ArrayCopyVersusForLoop>();
        }

        [MemoryDiagnoser]
        public class ArrayCopyVersusForLoop
        {
            private static readonly double[] Input = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            [Benchmark]
            public void Copy2()
            {
                Tester.ShiftUsingArrayCopy(Input, 2);
            }

            [Benchmark]
            public void Loop2()
            {
                Tester.ShiftUsingForLoop(Input, 2);
            }

            [Benchmark]
            public void Optimal2()
            {
                Tester.ShiftOptimal(Input, 2);
            }

            [Benchmark]
            public void Copy3()
            {
                Tester.ShiftUsingArrayCopy(Input, 3);
            }

            [Benchmark]
            public void Loop3()
            {
                Tester.ShiftUsingForLoop(Input, 3);
            }

            [Benchmark]
            public void Optimal3()
            {
                Tester.ShiftOptimal(Input, 3);
            }

            [Benchmark]
            public void Copy4()
            {
                Tester.ShiftUsingArrayCopy(Input, 4);
            }

            [Benchmark]
            public void Loop4()
            {
                Tester.ShiftUsingForLoop(Input, 4);
            }

            [Benchmark]
            public void Optimal4()
            {
                Tester.ShiftOptimal(Input, 4);
            }

            [Benchmark]
            public void Copy5()
            {
                Tester.ShiftUsingArrayCopy(Input, 5);
            }

            [Benchmark]
            public void Loop5()
            {
                Tester.ShiftUsingForLoop(Input, 5);
            }

            [Benchmark]
            public void Optimal5()
            {
                Tester.ShiftOptimal(Input, 5);
            }

            [Benchmark]
            public void Copy6()
            {
                Tester.ShiftUsingArrayCopy(Input, 6);
            }

            [Benchmark]
            public void Loop6()
            {
                Tester.ShiftUsingForLoop(Input, 6);
            }

            [Benchmark]
            public void Optimal6()
            {
                Tester.ShiftOptimal(Input, 6);
            }

            [Benchmark]
            public void Copy7()
            {
                Tester.ShiftUsingArrayCopy(Input, 7);
            }

            [Benchmark]
            public void Loop7()
            {
                Tester.ShiftUsingForLoop(Input, 7);
            }

            [Benchmark]
            public void Optimal7()
            {
                Tester.ShiftOptimal(Input, 7);
            }

            [Benchmark]
            public void Copy8()
            {
                Tester.ShiftUsingArrayCopy(Input, 8);
            }

            [Benchmark]
            public void Loop8()
            {
                Tester.ShiftUsingForLoop(Input, 8);
            }

            [Benchmark]
            public void Optimal8()
            {
                Tester.ShiftOptimal(Input, 8);
            }

            [Benchmark]
            public void Copy9()
            {
                Tester.ShiftUsingArrayCopy(Input, 9);
            }

            [Benchmark]
            public void Loop9()
            {
                Tester.ShiftUsingForLoop(Input, 9);
            }

            [Benchmark]
            public void Optimal9()
            {
                Tester.ShiftOptimal(Input, 9);
            }

            [Benchmark]
            public void Copy10()
            {
                Tester.ShiftUsingArrayCopy(Input, 10);
            }

            [Benchmark]
            public void Loop10()
            {
                Tester.ShiftUsingForLoop(Input, 10);
            }

            [Benchmark]
            public void Optimal10()
            {
                Tester.ShiftOptimal(Input, 10);
            }

            [Benchmark]
            public void Copy11()
            {
                Tester.ShiftUsingArrayCopy(Input, 11);
            }

            [Benchmark]
            public void Loop11()
            {
                Tester.ShiftUsingForLoop(Input, 11);
            }

            [Benchmark]
            public void Optimal11()
            {
                Tester.ShiftOptimal(Input, 11);
            }

            [Benchmark]
            public void Copy12()
            {
                Tester.ShiftUsingArrayCopy(Input, 12);
            }

            [Benchmark]
            public void Loop12()
            {
                Tester.ShiftUsingForLoop(Input, 12);
            }

            [Benchmark]
            public void Optimal12()
            {
                Tester.ShiftOptimal(Input, 12);
            }

            [Benchmark]
            public void Copy13()
            {
                Tester.ShiftUsingArrayCopy(Input, 13);
            }

            [Benchmark]
            public void Loop13()
            {
                Tester.ShiftUsingForLoop(Input, 13);
            }

            [Benchmark]
            public void Optimal13()
            {
                Tester.ShiftOptimal(Input, 13);
            }

            [Benchmark]
            public void Copy14()
            {
                Tester.ShiftUsingArrayCopy(Input, 14);
            }

            [Benchmark]
            public void Loop14()
            {
                Tester.ShiftUsingForLoop(Input, 14);
            }

            [Benchmark]
            public void Optimal14()
            {
                Tester.ShiftOptimal(Input, 14);
            }

            [Benchmark]
            public void Copy15()
            {
                Tester.ShiftUsingArrayCopy(Input, 15);
            }

            [Benchmark]
            public void Loop15()
            {
                Tester.ShiftUsingForLoop(Input, 15);
            }

            [Benchmark]
            public void Optimal15()
            {
                Tester.ShiftOptimal(Input, 15);
            }

            [Benchmark]
            public void Copy16()
            {
                Tester.ShiftUsingArrayCopy(Input, 16);
            }

            [Benchmark]
            public void Loop16()
            {
                Tester.ShiftUsingForLoop(Input, 16);
            }

            [Benchmark]
            public void Optimal16()
            {
                Tester.ShiftOptimal(Input, 16);
            }

            [Benchmark]
            public void Copy17()
            {
                Tester.ShiftUsingArrayCopy(Input, 17);
            }

            [Benchmark]
            public void Loop17()
            {
                Tester.ShiftUsingForLoop(Input, 17);
            }

            [Benchmark]
            public void Optimal17()
            {
                Tester.ShiftOptimal(Input, 17);
            }

            [Benchmark]
            public void Copy18()
            {
                Tester.ShiftUsingArrayCopy(Input, 18);
            }

            [Benchmark]
            public void Loop18()
            {
                Tester.ShiftUsingForLoop(Input, 18);
            }

            [Benchmark]
            public void Optimal18()
            {
                Tester.ShiftOptimal(Input, 18);
            }

            [Benchmark]
            public void Copy19()
            {
                Tester.ShiftUsingArrayCopy(Input, 19);
            }

            [Benchmark]
            public void Loop19()
            {
                Tester.ShiftUsingForLoop(Input, 19);
            }

            [Benchmark]
            public void Optimal19()
            {
                Tester.ShiftOptimal(Input, 19);
            }

            [Benchmark]
            public void Copy20()
            {
                Tester.ShiftUsingArrayCopy(Input, 20);
            }

            [Benchmark]
            public void Loop20()
            {
                Tester.ShiftUsingForLoop(Input, 20);
            }

            [Benchmark]
            public void Optimal20()
            {
                Tester.ShiftOptimal(Input, 20);
            }

            [Benchmark]
            public void Copy21()
            {
                Tester.ShiftUsingArrayCopy(Input, 21);
            }

            [Benchmark]
            public void Loop21()
            {
                Tester.ShiftUsingForLoop(Input, 21);
            }

            [Benchmark]
            public void Optimal21()
            {
                Tester.ShiftOptimal(Input, 21);
            }

            [Benchmark]
            public void Copy22()
            {
                Tester.ShiftUsingArrayCopy(Input, 22);
            }

            [Benchmark]
            public void Loop22()
            {
                Tester.ShiftUsingForLoop(Input, 22);
            }

            [Benchmark]
            public void Optimal22()
            {
                Tester.ShiftOptimal(Input, 22);
            }

            [Benchmark]
            public void Copy23()
            {
                Tester.ShiftUsingArrayCopy(Input, 23);
            }

            [Benchmark]
            public void Loop23()
            {
                Tester.ShiftUsingForLoop(Input, 23);
            }

            [Benchmark]
            public void Optimal23()
            {
                Tester.ShiftOptimal(Input, 23);
            }

            [Benchmark]
            public void Copy24()
            {
                Tester.ShiftUsingArrayCopy(Input, 24);
            }

            [Benchmark]
            public void Loop24()
            {
                Tester.ShiftUsingForLoop(Input, 24);
            }

            [Benchmark]
            public void Optimal24()
            {
                Tester.ShiftOptimal(Input, 24);
            }

            [Benchmark]
            public void Copy25()
            {
                Tester.ShiftUsingArrayCopy(Input, 25);
            }

            [Benchmark]
            public void Loop25()
            {
                Tester.ShiftUsingForLoop(Input, 25);
            }

            [Benchmark]
            public void Optimal25()
            {
                Tester.ShiftOptimal(Input, 25);
            }

            [Benchmark]
            public void Copy26()
            {
                Tester.ShiftUsingArrayCopy(Input, 26);
            }

            [Benchmark]
            public void Loop26()
            {
                Tester.ShiftUsingForLoop(Input, 26);
            }

            [Benchmark]
            public void Optimal26()
            {
                Tester.ShiftOptimal(Input, 26);
            }

            [Benchmark]
            public void Copy27()
            {
                Tester.ShiftUsingArrayCopy(Input, 27);
            }

            [Benchmark]
            public void Loop27()
            {
                Tester.ShiftUsingForLoop(Input, 27);
            }

            [Benchmark]
            public void Optimal27()
            {
                Tester.ShiftOptimal(Input, 27);
            }

            [Benchmark]
            public void Copy28()
            {
                Tester.ShiftUsingArrayCopy(Input, 28);
            }

            [Benchmark]
            public void Loop28()
            {
                Tester.ShiftUsingForLoop(Input, 28);
            }

            [Benchmark]
            public void Optimal28()
            {
                Tester.ShiftOptimal(Input, 28);
            }

            [Benchmark]
            public void Copy29()
            {
                Tester.ShiftUsingArrayCopy(Input, 29);
            }

            [Benchmark]
            public void Loop29()
            {
                Tester.ShiftUsingForLoop(Input, 29);
            }

            [Benchmark]
            public void Optimal29()
            {
                Tester.ShiftOptimal(Input, 29);
            }

            [Benchmark]
            public void Copy30()
            {
                Tester.ShiftUsingArrayCopy(Input, 30);
            }

            [Benchmark]
            public void Loop30()
            {
                Tester.ShiftUsingForLoop(Input, 30);
            }

            [Benchmark]
            public void Optimal30()
            {
                Tester.ShiftOptimal(Input, 30);
            }

            [Benchmark]
            public void Copy31()
            {
                Tester.ShiftUsingArrayCopy(Input, 31);
            }

            [Benchmark]
            public void Loop31()
            {
                Tester.ShiftUsingForLoop(Input, 31);
            }

            [Benchmark]
            public void Optimal31()
            {
                Tester.ShiftOptimal(Input, 31);
            }

            [Benchmark]
            public void Copy32()
            {
                Tester.ShiftUsingArrayCopy(Input, 32);
            }

            [Benchmark]
            public void Loop32()
            {
                Tester.ShiftUsingForLoop(Input, 32);
            }

            [Benchmark]
            public void Optimal32()
            {
                Tester.ShiftOptimal(Input, 32);
            }

            [Benchmark]
            public void Copy33()
            {
                Tester.ShiftUsingArrayCopy(Input, 33);
            }

            [Benchmark]
            public void Loop33()
            {
                Tester.ShiftUsingForLoop(Input, 33);
            }

            [Benchmark]
            public void Optimal33()
            {
                Tester.ShiftOptimal(Input, 33);
            }

            [Benchmark]
            public void Copy34()
            {
                Tester.ShiftUsingArrayCopy(Input, 34);
            }

            [Benchmark]
            public void Loop34()
            {
                Tester.ShiftUsingForLoop(Input, 34);
            }

            [Benchmark]
            public void Optimal34()
            {
                Tester.ShiftOptimal(Input, 34);
            }

            [Benchmark]
            public void Copy35()
            {
                Tester.ShiftUsingArrayCopy(Input, 35);
            }

            [Benchmark]
            public void Loop35()
            {
                Tester.ShiftUsingForLoop(Input, 35);
            }

            [Benchmark]
            public void Optimal35()
            {
                Tester.ShiftOptimal(Input, 35);
            }

            [Benchmark]
            public void Copy36()
            {
                Tester.ShiftUsingArrayCopy(Input, 36);
            }

            [Benchmark]
            public void Loop36()
            {
                Tester.ShiftUsingForLoop(Input, 36);
            }

            [Benchmark]
            public void Optimal36()
            {
                Tester.ShiftOptimal(Input, 36);
            }

            [Benchmark]
            public void Copy37()
            {
                Tester.ShiftUsingArrayCopy(Input, 37);
            }

            [Benchmark]
            public void Loop37()
            {
                Tester.ShiftUsingForLoop(Input, 37);
            }

            [Benchmark]
            public void Optimal37()
            {
                Tester.ShiftOptimal(Input, 37);
            }

            [Benchmark]
            public void Copy38()
            {
                Tester.ShiftUsingArrayCopy(Input, 38);
            }

            [Benchmark]
            public void Loop38()
            {
                Tester.ShiftUsingForLoop(Input, 38);
            }

            [Benchmark]
            public void Optimal38()
            {
                Tester.ShiftOptimal(Input, 38);
            }

            [Benchmark]
            public void Copy39()
            {
                Tester.ShiftUsingArrayCopy(Input, 39);
            }

            [Benchmark]
            public void Loop39()
            {
                Tester.ShiftUsingForLoop(Input, 39);
            }

            [Benchmark]
            public void Optimal39()
            {
                Tester.ShiftOptimal(Input, 39);
            }

            [Benchmark]
            public void Copy40()
            {
                Tester.ShiftUsingArrayCopy(Input, 40);
            }

            [Benchmark]
            public void Loop40()
            {
                Tester.ShiftUsingForLoop(Input, 40);
            }

            [Benchmark]
            public void Optimal40()
            {
                Tester.ShiftOptimal(Input, 40);
            }

            [Benchmark]
            public void Copy41()
            {
                Tester.ShiftUsingArrayCopy(Input, 41);
            }

            [Benchmark]
            public void Loop41()
            {
                Tester.ShiftUsingForLoop(Input, 41);
            }

            [Benchmark]
            public void Optimal41()
            {
                Tester.ShiftOptimal(Input, 41);
            }

            [Benchmark]
            public void Copy42()
            {
                Tester.ShiftUsingArrayCopy(Input, 42);
            }

            [Benchmark]
            public void Loop42()
            {
                Tester.ShiftUsingForLoop(Input, 42);
            }

            [Benchmark]
            public void Optimal42()
            {
                Tester.ShiftOptimal(Input, 42);
            }

            [Benchmark]
            public void Copy43()
            {
                Tester.ShiftUsingArrayCopy(Input, 43);
            }

            [Benchmark]
            public void Loop43()
            {
                Tester.ShiftUsingForLoop(Input, 43);
            }

            [Benchmark]
            public void Optimal43()
            {
                Tester.ShiftOptimal(Input, 43);
            }

            [Benchmark]
            public void Copy44()
            {
                Tester.ShiftUsingArrayCopy(Input, 44);
            }

            [Benchmark]
            public void Loop44()
            {
                Tester.ShiftUsingForLoop(Input, 44);
            }

            [Benchmark]
            public void Optimal44()
            {
                Tester.ShiftOptimal(Input, 44);
            }

            [Benchmark]
            public void Copy45()
            {
                Tester.ShiftUsingArrayCopy(Input, 45);
            }

            [Benchmark]
            public void Loop45()
            {
                Tester.ShiftUsingForLoop(Input, 45);
            }

            [Benchmark]
            public void Optimal45()
            {
                Tester.ShiftOptimal(Input, 45);
            }

            [Benchmark]
            public void Copy46()
            {
                Tester.ShiftUsingArrayCopy(Input, 46);
            }

            [Benchmark]
            public void Loop46()
            {
                Tester.ShiftUsingForLoop(Input, 46);
            }

            [Benchmark]
            public void Optimal46()
            {
                Tester.ShiftOptimal(Input, 46);
            }

            [Benchmark]
            public void Copy47()
            {
                Tester.ShiftUsingArrayCopy(Input, 47);
            }

            [Benchmark]
            public void Loop47()
            {
                Tester.ShiftUsingForLoop(Input, 47);
            }

            [Benchmark]
            public void Optimal47()
            {
                Tester.ShiftOptimal(Input, 47);
            }

            [Benchmark]
            public void Copy48()
            {
                Tester.ShiftUsingArrayCopy(Input, 48);
            }

            [Benchmark]
            public void Loop48()
            {
                Tester.ShiftUsingForLoop(Input, 48);
            }

            [Benchmark]
            public void Optimal48()
            {
                Tester.ShiftOptimal(Input, 48);
            }

            [Benchmark]
            public void Copy49()
            {
                Tester.ShiftUsingArrayCopy(Input, 49);
            }

            [Benchmark]
            public void Loop49()
            {
                Tester.ShiftUsingForLoop(Input, 49);
            }

            [Benchmark]
            public void Optimal49()
            {
                Tester.ShiftOptimal(Input, 49);
            }

            [Benchmark]
            public void Copy50()
            {
                Tester.ShiftUsingArrayCopy(Input, 50);
            }

            [Benchmark]
            public void Loop50()
            {
                Tester.ShiftUsingForLoop(Input, 50);
            }

            [Benchmark]
            public void Optimal50()
            {
                Tester.ShiftOptimal(Input, 50);
            }

            [Benchmark]
            public void Copy51()
            {
                Tester.ShiftUsingArrayCopy(Input, 51);
            }

            [Benchmark]
            public void Loop51()
            {
                Tester.ShiftUsingForLoop(Input, 51);
            }

            [Benchmark]
            public void Optimal51()
            {
                Tester.ShiftOptimal(Input, 51);
            }

            [Benchmark]
            public void Copy52()
            {
                Tester.ShiftUsingArrayCopy(Input, 52);
            }

            [Benchmark]
            public void Loop52()
            {
                Tester.ShiftUsingForLoop(Input, 52);
            }

            [Benchmark]
            public void Optimal52()
            {
                Tester.ShiftOptimal(Input, 52);
            }

            [Benchmark]
            public void Copy53()
            {
                Tester.ShiftUsingArrayCopy(Input, 53);
            }

            [Benchmark]
            public void Loop53()
            {
                Tester.ShiftUsingForLoop(Input, 53);
            }

            [Benchmark]
            public void Optimal53()
            {
                Tester.ShiftOptimal(Input, 53);
            }

            [Benchmark]
            public void Copy54()
            {
                Tester.ShiftUsingArrayCopy(Input, 54);
            }

            [Benchmark]
            public void Loop54()
            {
                Tester.ShiftUsingForLoop(Input, 54);
            }

            [Benchmark]
            public void Optimal54()
            {
                Tester.ShiftOptimal(Input, 54);
            }

            [Benchmark]
            public void Copy55()
            {
                Tester.ShiftUsingArrayCopy(Input, 55);
            }

            [Benchmark]
            public void Loop55()
            {
                Tester.ShiftUsingForLoop(Input, 55);
            }

            [Benchmark]
            public void Optimal55()
            {
                Tester.ShiftOptimal(Input, 55);
            }

            [Benchmark]
            public void Copy56()
            {
                Tester.ShiftUsingArrayCopy(Input, 56);
            }

            [Benchmark]
            public void Loop56()
            {
                Tester.ShiftUsingForLoop(Input, 56);
            }

            [Benchmark]
            public void Optimal56()
            {
                Tester.ShiftOptimal(Input, 56);
            }

            [Benchmark]
            public void Copy57()
            {
                Tester.ShiftUsingArrayCopy(Input, 57);
            }

            [Benchmark]
            public void Loop57()
            {
                Tester.ShiftUsingForLoop(Input, 57);
            }

            [Benchmark]
            public void Optimal57()
            {
                Tester.ShiftOptimal(Input, 57);
            }

            [Benchmark]
            public void Copy58()
            {
                Tester.ShiftUsingArrayCopy(Input, 58);
            }

            [Benchmark]
            public void Loop58()
            {
                Tester.ShiftUsingForLoop(Input, 58);
            }

            [Benchmark]
            public void Optimal58()
            {
                Tester.ShiftOptimal(Input, 58);
            }

            [Benchmark]
            public void Copy59()
            {
                Tester.ShiftUsingArrayCopy(Input, 59);
            }

            [Benchmark]
            public void Loop59()
            {
                Tester.ShiftUsingForLoop(Input, 59);
            }

            [Benchmark]
            public void Optimal59()
            {
                Tester.ShiftOptimal(Input, 59);
            }

            [Benchmark]
            public void Copy60()
            {
                Tester.ShiftUsingArrayCopy(Input, 60);
            }

            [Benchmark]
            public void Loop60()
            {
                Tester.ShiftUsingForLoop(Input, 60);
            }

            [Benchmark]
            public void Optimal60()
            {
                Tester.ShiftOptimal(Input, 60);
            }

            [Benchmark]
            public void Copy61()
            {
                Tester.ShiftUsingArrayCopy(Input, 61);
            }

            [Benchmark]
            public void Loop61()
            {
                Tester.ShiftUsingForLoop(Input, 61);
            }

            [Benchmark]
            public void Optimal61()
            {
                Tester.ShiftOptimal(Input, 61);
            }

            [Benchmark]
            public void Copy62()
            {
                Tester.ShiftUsingArrayCopy(Input, 62);
            }

            [Benchmark]
            public void Loop62()
            {
                Tester.ShiftUsingForLoop(Input, 62);
            }

            [Benchmark]
            public void Optimal62()
            {
                Tester.ShiftOptimal(Input, 62);
            }

            [Benchmark]
            public void Copy63()
            {
                Tester.ShiftUsingArrayCopy(Input, 63);
            }

            [Benchmark]
            public void Loop63()
            {
                Tester.ShiftUsingForLoop(Input, 63);
            }

            [Benchmark]
            public void Optimal63()
            {
                Tester.ShiftOptimal(Input, 63);
            }

            [Benchmark]
            public void Copy64()
            {
                Tester.ShiftUsingArrayCopy(Input, 64);
            }

            [Benchmark]
            public void Loop64()
            {
                Tester.ShiftUsingForLoop(Input, 64);
            }

            [Benchmark]
            public void Optimal64()
            {
                Tester.ShiftOptimal(Input, 64);
            }

            [Benchmark]
            public void Copy128()
            {
                Tester.ShiftUsingArrayCopy(Input, 128);
            }

            [Benchmark]
            public void Loop128()
            {
                Tester.ShiftUsingForLoop(Input, 128);
            }

            [Benchmark]
            public void Optimal128()
            {
                Tester.ShiftOptimal(Input, 128);
            }

            [Benchmark]
            public void Copy256()
            {
                Tester.ShiftUsingArrayCopy(Input, 256);
            }

            [Benchmark]
            public void Loop256()
            {
                Tester.ShiftUsingForLoop(Input, 256);
            }

            [Benchmark]
            public void Optimal256()
            {
                Tester.ShiftOptimal(Input, 256);
            }

            [Benchmark]
            public void Copy512()
            {
                Tester.ShiftUsingArrayCopy(Input, 512);
            }

            [Benchmark]
            public void Loop512()
            {
                Tester.ShiftUsingForLoop(Input, 512);
            }

            [Benchmark]
            public void Optimal512()
            {
                Tester.ShiftOptimal(Input, 512);
            }
        }

        [MemoryDiagnoser]
        private static class Tester
        {
            public static void ShiftUsingArrayCopy(double[] array, int length)
            {
                int lastIndex = length - 1;
                Array.Copy(array, 1, array, 0, lastIndex);
                array[lastIndex] = 1;
            }

            public static void ShiftUsingForLoop(double[] array, int length)
            {
                int lastIndex = length - 1;
                for (int i = 0; i < lastIndex;)
                {
                    array[i] = array[++i];
                }

                array[lastIndex] = 1;
            }

            public static void ShiftOptimal(double[] array, int length)
            {
                int lastIndex = length - 1;

                if (length < 33)
                {
                    for (int i = 0; i < lastIndex;)
                    {
                        array[i] = array[++i];
                    }
                }
                else
                {
                    Array.Copy(array, 1, array, 0, lastIndex);
                }

                array[lastIndex] = 1;
            }
        }
    }
}
