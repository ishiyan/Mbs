using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Generators.Sinusoidal
{
    /// <summary>
    /// A sinusoidal data generator extensions.
    /// </summary>
    public static class SinusoidalDataGeneratorExtensions
    {
        /// <summary>
        /// Generates a specified amount of <see cref="Ohlcv"/> samples from a sinusoidal data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Ohlcv"/> sinusoidal data generator parameters.</param>
        /// <param name="count">The number of <see cref="Ohlcv"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Ohlcv"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Ohlcv>> GenerateAsync(
            this SinusoidalOhlcvGeneratorParameters parameters,
            int count)
        {
            var generator = new SinusoidalOhlcvGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Ohlcv>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Quote"/> samples from a sinusoidal data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Quote"/> sinusoidal data generator parameters.</param>
        /// <param name="count">The number of <see cref="Quote"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Quote"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Quote>> GenerateAsync(
            this SinusoidalQuoteGeneratorParameters parameters,
            int count)
        {
            var generator = new SinusoidalQuoteGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Quote>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Trade"/> samples from a sinusoidal data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Trade"/> sinusoidal data generator parameters.</param>
        /// <param name="count">The number of <see cref="Trade"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Trade"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Trade>> GenerateAsync(
            this SinusoidalTradeGeneratorParameters parameters,
            int count)
        {
            var generator = new SinusoidalTradeGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Trade>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Scalar"/> samples from a sinusoidal data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Scalar"/> sinusoidal data generator parameters.</param>
        /// <param name="count">The number of <see cref="Scalar"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Scalar"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Scalar>> GenerateAsync(
            this SinusoidalScalarGeneratorParameters parameters,
            int count)
        {
            var generator = new SinusoidalScalarGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(generator, count);
        }
    }
}
