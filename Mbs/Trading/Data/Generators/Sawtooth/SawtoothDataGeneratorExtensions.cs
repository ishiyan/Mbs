using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Generators.Sawtooth
{
    /// <summary>
    /// A sawtooth data generator extensions.
    /// </summary>
    public static class SawtoothDataGeneratorExtensions
    {
        /// <summary>
        /// Generates a specified amount of <see cref="Ohlcv"/> samples from a sawtooth data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Ohlcv"/> sawtooth data generator parameters.</param>
        /// <param name="count">The number of <see cref="Ohlcv"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Ohlcv"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Ohlcv>> GenerateAsync(
            this SawtoothOhlcvGeneratorParameters parameters,
            int count)
        {
            var generator = new SawtoothOhlcvGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Ohlcv>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Quote"/> samples from a sawtooth data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Quote"/> sawtooth data generator parameters.</param>
        /// <param name="count">The number of <see cref="Quote"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Quote"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Quote>> GenerateAsync(
            this SawtoothQuoteGeneratorParameters parameters,
            int count)
        {
            var generator = new SawtoothQuoteGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Quote>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Trade"/> samples from a sawtooth data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Trade"/> sawtooth data generator parameters.</param>
        /// <param name="count">The number of <see cref="Trade"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Trade"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Trade>> GenerateAsync(
            this SawtoothTradeGeneratorParameters parameters,
            int count)
        {
            var generator = new SawtoothTradeGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Trade>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Scalar"/> samples from a sawtooth data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Scalar"/> sawtooth data generator parameters.</param>
        /// <param name="count">The number of <see cref="Scalar"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Scalar"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Scalar>> GenerateAsync(
            this SawtoothScalarGeneratorParameters parameters,
            int count)
        {
            var generator = new SawtoothScalarGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(generator, count);
        }
    }
}
