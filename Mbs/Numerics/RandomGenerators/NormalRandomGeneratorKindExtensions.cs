using System;
using Mbs.Numerics.RandomGenerators.BoxMuller;
using Mbs.Numerics.RandomGenerators.Ziggurat;

namespace Mbs.Numerics.RandomGenerators
{
    /// <summary>
    /// The <see cref="NormalRandomGeneratorKind"/> extensions.
    /// </summary>
    public static class NormalRandomGeneratorKindExtensions
    {
        /// <summary>
        /// The Gaussian distributed random generator factory.
        /// </summary>
        /// <param name="kind">The <see cref="NormalRandomGeneratorKind"/> enum value.</param>
        /// <param name="seed">The seed to randomize the generator.</param>
        /// <param name="uniformGeneratorKind">The <see cref="UniformRandomGeneratorKind"/> enum value. Used only for the <see cref="NormalRandomGeneratorKind.ZigguratColinGreen"/> and the <see cref="NormalRandomGeneratorKind.BoxMuller"/> normal generator kinds.</param>
        /// <returns>The normally distributed random generator.</returns>
        public static INormalRandomGenerator NormalRandomGenerator(this NormalRandomGeneratorKind kind, int seed, UniformRandomGeneratorKind uniformGeneratorKind)
        {
            return kind switch
            {
                NormalRandomGeneratorKind.ZigguratColinGreen => new ZigguratNormalRandom(uniformGeneratorKind.RandomGenerator(seed)),
                NormalRandomGeneratorKind.ZigguratLeongZhang => new ZigguratLeongZhangNormalRandom((uint)seed),
                NormalRandomGeneratorKind.ZigguratMarsagliaTsang => new ZigguratMarsagliaTsangNormalRandom((uint)seed),
                NormalRandomGeneratorKind.BoxMuller => new BoxMullerNormalRandom(uniformGeneratorKind.RandomGenerator(seed)),
                _ => throw new ArgumentException("Unknown NormalRandomGeneratorKind enum value.", nameof(kind))
            };
        }
    }
}