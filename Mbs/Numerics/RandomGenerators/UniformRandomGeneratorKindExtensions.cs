using System;
using Mbs.Numerics.RandomGenerators.Marsaglia;
using Mbs.Numerics.RandomGenerators.MersenneTwister;
using Mbs.Numerics.RandomGenerators.Other;
using Mbs.Numerics.RandomGenerators.Well;

namespace Mbs.Numerics.RandomGenerators
{
    /// <summary>
    /// The <see cref="UniformRandomGeneratorKind"/> extensions.
    /// </summary>
    public static class UniformRandomGeneratorKindExtensions
    {
        /// <summary>
        /// The uniformly distributed random generator factory.
        /// </summary>
        /// <param name="kind">The <see cref="UniformRandomGeneratorKind"/> enum value.</param>
        /// <param name="seed">The seed to randomize the generator.</param>
        /// <returns>The uniformly distributed random generator with the specified seed.</returns>
        public static IRandomGenerator RandomGenerator(this UniformRandomGeneratorKind kind, int seed)
        {
            return kind switch
            {
                UniformRandomGeneratorKind.Well44497A => new Well44497AUniformRandom(seed),
                UniformRandomGeneratorKind.Well44497B => new Well44497BUniformRandom(seed),
                UniformRandomGeneratorKind.Well19937A => new Well19937AUniformRandom(seed),
                UniformRandomGeneratorKind.Well19937C => new Well19937CUniformRandom(seed),
                UniformRandomGeneratorKind.Well1024A => new Well1024AUniformRandom(seed),
                UniformRandomGeneratorKind.Well512A => new Well512AUniformRandom(seed),

                UniformRandomGeneratorKind.Mt19937Ar32 => new MersenneTwister19937UniformRandom(seed),
                UniformRandomGeneratorKind.Mt19937Ar64 => new MersenneTwister19937UniformRandom64(seed),
                UniformRandomGeneratorKind.Mt11213B32 => new MersenneTwister11213BUniformRandom(seed),

                UniformRandomGeneratorKind.Sfmt216091 => new MersenneTwisterSfmt216091UniformRandom(seed),
                UniformRandomGeneratorKind.Sfmt132049 => new MersenneTwisterSfmt132049UniformRandom(seed),
                UniformRandomGeneratorKind.Sfmt86243 => new MersenneTwisterSfmt86243UniformRandom(seed),
                UniformRandomGeneratorKind.Sfmt44497 => new MersenneTwisterSfmt44497UniformRandom(seed),
                UniformRandomGeneratorKind.Sfmt19937 => new MersenneTwisterSfmt19937UniformRandom(seed),
                UniformRandomGeneratorKind.Sfmt11213 => new MersenneTwisterSfmt11213UniformRandom(seed),
                UniformRandomGeneratorKind.Sfmt4253 => new MersenneTwisterSfmt4253UniformRandom(seed),
                UniformRandomGeneratorKind.Sfmt2281 => new MersenneTwisterSfmt2281UniformRandom(seed),
                UniformRandomGeneratorKind.Sfmt1279 => new MersenneTwisterSfmt1279UniformRandom(seed),
                UniformRandomGeneratorKind.Sfmt607 => new MersenneTwisterSfmt607UniformRandom(seed),

                UniformRandomGeneratorKind.LaggedFibonacci => new AdditiveLaggedFibonacciUniformRandom(seed),
                UniformRandomGeneratorKind.MarsagliaMultiplyWithCarry => new MultiplyWithCarryUniformRandom(seed),
                UniformRandomGeneratorKind.MarsagliaXorShift => new XorShiftUniformRandom(seed),
                UniformRandomGeneratorKind.LinearCongruential => new LinearCongruentialUniformRandom(seed),
                UniformRandomGeneratorKind.DotNet => new SystemUniformRandom(seed),
                _ => throw new ArgumentException("Unknown UniformRandomGeneratorKind enum value.", nameof(kind))
            };
        }
    }
}