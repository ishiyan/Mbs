using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Generators.Chirp
{
    /// <summary>
    /// A chirp data generator extensions.
    /// </summary>
    public static class ChirpDataGeneratorExtensions
    {
        /// <summary>
        /// Generates a specified amount of <see cref="Ohlcv"/> samples from a chirp data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Ohlcv"/> chirp data generator parameters.</param>
        /// <param name="count">The number of <see cref="Ohlcv"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Ohlcv"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Ohlcv>> GenerateAsync(
            this ChirpOhlcvGeneratorParameters parameters,
            int count)
        {
            var generator = new ChirpOhlcvGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Ohlcv>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Quote"/> samples from a chirp data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Quote"/> chirp data generator parameters.</param>
        /// <param name="count">The number of <see cref="Quote"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Quote"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Quote>> GenerateAsync(
            this ChirpQuoteGeneratorParameters parameters,
            int count)
        {
            var generator = new ChirpQuoteGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Quote>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Trade"/> samples from a chirp data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Trade"/> chirp data generator parameters.</param>
        /// <param name="count">The number of <see cref="Trade"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Trade"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Trade>> GenerateAsync(
            this ChirpTradeGeneratorParameters parameters,
            int count)
        {
            var generator = new ChirpTradeGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Trade>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Scalar"/> samples from a chirp data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Scalar"/> chirp data generator parameters.</param>
        /// <param name="count">The number of <see cref="Scalar"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Scalar"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Scalar>> GenerateAsync(
            this ChirpScalarGeneratorParameters parameters,
            int count)
        {
            var generator = new ChirpScalarGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(generator, count);
        }
    }
}
