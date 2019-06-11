namespace Mbs.Numerics.Random
{
    /// <summary>
    /// Enumerates the different kinds of the normal random generators.
    /// </summary>
    public enum NormalRandomGeneratorKind
    {
        /// <summary>
        /// A fast Gaussian distribution sampler by Colin Green.
        /// </summary>
        ZigguratColinGreen = 0,

        /// <summary>
        /// The Gaussian random number generator by Marsaglia and Tsang with with modifications of Leong, Zhang et al.
        /// </summary>
        ZigguratLeongZhang,

        /// <summary>
        /// The Gaussian random number generator based on the original Marsaglia and Tsang article.
        /// </summary>
        ZigguratMarsagliaTsang,

        /// <summary>
        /// The Gaussian random number generator which uses the polar form of the Box-Muller method.
        /// </summary>
        BoxMuller
    }
}
