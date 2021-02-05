using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Generators.GeometricBrownianMotion
{
    /// <summary>
    /// A geometric Brownian motion generator extensions.
    /// </summary>
    public static class GeometricBrownianMotionDataGeneratorExtensions
    {
        /// <summary>
        /// Generates a specified amount of <see cref="Ohlcv"/> samples from a geometric Brownian motion data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Ohlcv"/> geometric Brownian motion data generator parameters.</param>
        /// <param name="count">The number of <see cref="Ohlcv"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Ohlcv"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Ohlcv>> GenerateAsync(
            this GeometricBrownianMotionOhlcvGeneratorParameters parameters,
            int count)
        {
            var generator = new GeometricBrownianMotionOhlcvGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Ohlcv>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Quote"/> samples from a geometric Brownian motion data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Quote"/> geometric Brownian motion data generator parameters.</param>
        /// <param name="count">The number of <see cref="Quote"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Quote"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Quote>> GenerateAsync(
            this GeometricBrownianMotionQuoteGeneratorParameters parameters,
            int count)
        {
            var generator = new GeometricBrownianMotionQuoteGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Quote>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Trade"/> samples from a geometric Brownian motion data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Trade"/> geometric Brownian motion data generator parameters.</param>
        /// <param name="count">The number of <see cref="Trade"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Trade"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Trade>> GenerateAsync(
            this GeometricBrownianMotionTradeGeneratorParameters parameters,
            int count)
        {
            var generator = new GeometricBrownianMotionTradeGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Trade>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Scalar"/> samples from a geometric Brownian motion data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Scalar"/> geometric Brownian motion data generator parameters.</param>
        /// <param name="count">The number of <see cref="Scalar"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Scalar"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Scalar>> GenerateAsync(
            this GeometricBrownianMotionScalarGeneratorParameters parameters,
            int count)
        {
            var generator = new GeometricBrownianMotionScalarGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(generator, count);
        }
    }
}
