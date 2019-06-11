using System;

namespace Mbs.Numerics.Random
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
            switch (kind)
            {
                case NormalRandomGeneratorKind.ZigguratColinGreen:
                    return new ZigguratNormalRandom(uniformGeneratorKind.RandomGenerator(seed));
                case NormalRandomGeneratorKind.ZigguratLeongZhang:
                    return new ZigguratLeongZhangNormalRandom((uint)seed);
                case NormalRandomGeneratorKind.ZigguratMarsagliaTsang:
                    return new ZigguratMarsagliaTsangNormalRandom((uint)seed);
                case NormalRandomGeneratorKind.BoxMuller:
                    return new BoxMullerNormalRandom(uniformGeneratorKind.RandomGenerator(seed));
                default:
                    throw new ArgumentException("Unknown NormalRandomGeneratorKind enum value.", nameof(kind));
            }
        }
    }
}