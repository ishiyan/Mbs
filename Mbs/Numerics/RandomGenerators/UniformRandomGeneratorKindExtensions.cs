using System;

namespace Mbs.Numerics.Random
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
            switch (kind)
            {
                case UniformRandomGeneratorKind.Well44497A:
                    return new Well44497AUniformRandom(seed);
                case UniformRandomGeneratorKind.Well44497B:
                    return new Well44497BUniformRandom(seed);
                case UniformRandomGeneratorKind.Well19937A:
                    return new Well19937AUniformRandom(seed);
                case UniformRandomGeneratorKind.Well19937C:
                    return new Well19937CUniformRandom(seed);
                case UniformRandomGeneratorKind.Well1024A:
                    return new Well1024AUniformRandom(seed);
                case UniformRandomGeneratorKind.Well512A:
                    return new Well512AUniformRandom(seed);

                case UniformRandomGeneratorKind.Mt19937Ar32:
                    return new MersenneTwister19937UniformRandom(seed);
                case UniformRandomGeneratorKind.Mt19937Ar64:
                    return new MersenneTwister19937UniformRandom64(seed);
                case UniformRandomGeneratorKind.Mt11213B32:
                    return new MersenneTwister11213BUniformRandom(seed);

                case UniformRandomGeneratorKind.Sfmt216091:
                    return new MersenneTwisterSfmt216091UniformRandom(seed);
                case UniformRandomGeneratorKind.Sfmt132049:
                    return new MersenneTwisterSfmt132049UniformRandom(seed);
                case UniformRandomGeneratorKind.Sfmt86243:
                    return new MersenneTwisterSfmt86243UniformRandom(seed);
                case UniformRandomGeneratorKind.Sfmt44497:
                    return new MersenneTwisterSfmt44497UniformRandom(seed);
                case UniformRandomGeneratorKind.Sfmt19937:
                    return new MersenneTwisterSfmt19937UniformRandom(seed);
                case UniformRandomGeneratorKind.Sfmt11213:
                    return new MersenneTwisterSfmt11213UniformRandom(seed);
                case UniformRandomGeneratorKind.Sfmt4253:
                    return new MersenneTwisterSfmt4253UniformRandom(seed);
                case UniformRandomGeneratorKind.Sfmt2281:
                    return new MersenneTwisterSfmt2281UniformRandom(seed);
                case UniformRandomGeneratorKind.Sfmt1279:
                    return new MersenneTwisterSfmt1279UniformRandom(seed);
                case UniformRandomGeneratorKind.Sfmt607:
                    return new MersenneTwisterSfmt607UniformRandom(seed);

                case UniformRandomGeneratorKind.LaggedFibonacci:
                    return new AdditiveLaggedFibonacciUniformRandom(seed);
                case UniformRandomGeneratorKind.MarsagliaMultiplyWithCarry:
                    return new MultiplyWithCarryUniformRandom(seed);
                case UniformRandomGeneratorKind.MarsagliaXorShift:
                    return new XorShiftUniformRandom(seed);
                case UniformRandomGeneratorKind.LinearCongruential:
                    return new LinearCongruentialUniformRandom(seed);
                case UniformRandomGeneratorKind.DotNet:
                    return new SystemUniformRandom(seed);
                default:
                    throw new ArgumentException("Unknown UniformRandomGeneratorKind enum value.", nameof(kind));
            }
        }
    }
}