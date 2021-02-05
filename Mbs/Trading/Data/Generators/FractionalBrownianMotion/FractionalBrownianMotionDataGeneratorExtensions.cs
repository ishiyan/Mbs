using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Generators.FractionalBrownianMotion
{
    /// <summary>
    /// A fractional Brownian motion generator extensions.
    /// </summary>
    public static class FractionalBrownianMotionDataGeneratorExtensions
    {
        /// <summary>
        /// Generates a specified amount of <see cref="Ohlcv"/> samples from a fractional Brownian motion data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Ohlcv"/> fractional Brownian motion data generator parameters.</param>
        /// <param name="count">The number of <see cref="Ohlcv"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Ohlcv"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Ohlcv>> GenerateAsync(
            this FractionalBrownianMotionOhlcvGeneratorParameters parameters,
            int count)
        {
            var generator = new FractionalBrownianMotionOhlcvGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Ohlcv>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Quote"/> samples from a fractional Brownian motion data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Quote"/> fractional Brownian motion data generator parameters.</param>
        /// <param name="count">The number of <see cref="Quote"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Quote"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Quote>> GenerateAsync(
            this FractionalBrownianMotionQuoteGeneratorParameters parameters,
            int count)
        {
            var generator = new FractionalBrownianMotionQuoteGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Quote>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Trade"/> samples from a fractional Brownian motion data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Trade"/> fractional Brownian motion data generator parameters.</param>
        /// <param name="count">The number of <see cref="Trade"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Trade"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Trade>> GenerateAsync(
            this FractionalBrownianMotionTradeGeneratorParameters parameters,
            int count)
        {
            var generator = new FractionalBrownianMotionTradeGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Trade>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Scalar"/> samples from a fractional Brownian motion data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Scalar"/> fractional Brownian motion data generator parameters.</param>
        /// <param name="count">The number of <see cref="Scalar"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Scalar"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Scalar>> GenerateAsync(
            this FractionalBrownianMotionScalarGeneratorParameters parameters,
            int count)
        {
            var generator = new FractionalBrownianMotionScalarGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(generator, count);
        }
    }
}
