namespace Mbs.Numerics.RandomGenerators
{
    /// <summary>
    /// Enumerates the different kinds of the uniform random generators.
    /// </summary>
    public enum UniformRandomGeneratorKind
    {
        /// <summary>
        /// The pseudo-random number generator as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto.
        /// </summary>
        Well44497A,

        /// <summary>
        /// The pseudo-random number generator as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto.
        /// </summary>
        Well44497B,

        /// <summary>
        /// The pseudo-random number generator as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto.
        /// </summary>
        Well19937A,

        /// <summary>
        /// The pseudo-random number generator as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto.
        /// </summary>
        Well19937C,

        /// <summary>
        /// The pseudo-random number generator as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto.
        /// </summary>
        Well1024A,

        /// <summary>
        /// The pseudo-random number generator as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto.
        /// </summary>
        Well512A,

        /// <summary>
        /// The Mersenne Twister uniform 32-bit pseudo-random number generator with the period of 2^19937-1.
        /// </summary>
        Mt19937Ar32,

        /// <summary>
        /// The Mersenne Twister uniform 64-bit pseudo-random number generator with the period of 2^19937-1.
        /// </summary>
        Mt19937Ar64,

        /// <summary>
        /// The Mersenne Twister uniform 32-bit pseudo-random number generator with the period of 2^11213-1.
        /// </summary>
        Mt11213B32,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^216091-1.
        /// </summary>
        Sfmt216091,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^132049-1.
        /// </summary>
        Sfmt132049,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^86243-1.
        /// </summary>
        Sfmt86243,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^44497-1.
        /// </summary>
        Sfmt44497,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^19937-1.
        /// </summary>
        Sfmt19937,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^11213-1.
        /// </summary>
        Sfmt11213,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^4253-1.
        /// </summary>
        Sfmt4253,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^2281-1.
        /// </summary>
        Sfmt2281,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^1279-1.
        /// </summary>
        Sfmt1279,

        /// <summary>
        /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator with the period of 2^607-1.
        /// </summary>
        Sfmt607,

        /// <summary>
        /// An additive lagged Fibonacci uniform pseudo-random number generator.
        /// </summary>
        LaggedFibonacci,

        /// <summary>
        /// George Marsaglia's MWC (multiply with carry) uniform pseudo-random number generator.
        /// </summary>
        MarsagliaMultiplyWithCarry,

        /// <summary>
        /// George Marsaglia's XOR shift random generator has a period of 2^128 − 1 and passes the Diehard tests.
        /// </summary>
        MarsagliaXorShift,

        /// <summary>
        /// A linear congruential random generator is defined by the recurrence relation xᵢ₊₁ ≡ (axᵢ + c) mod m.
        /// </summary>
        LinearCongruential,

        /// <summary>
        /// The standard random number generator of the .NET framework.
        /// </summary>
        DotNet,
    }
}
