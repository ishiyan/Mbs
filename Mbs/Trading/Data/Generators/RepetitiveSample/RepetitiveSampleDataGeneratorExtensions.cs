using System.Threading.Tasks;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Generators.RepetitiveSample
{
    /// <summary>
    /// A repetitive sample data generator extensions.
    /// </summary>
    public static class RepetitiveSampleDataGeneratorExtensions
    {
        /// <summary>
        /// Generates a specified amount of <see cref="Ohlcv"/> samples from a repetitive sample data generator.
        /// </summary>
        /// <param name="parameters">The repetitive sample data generator parameters.</param>
        /// <param name="count">The number of <see cref="Ohlcv"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Ohlcv"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Ohlcv>> GenerateOhlcvAsync(
            this RepetitiveSampleGeneratorParameters parameters,
            int count)
        {
            var generator = new RepetitiveSampleOhlcvGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Ohlcv>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Quote"/> samples from a repetitive sample data generator.
        /// </summary>
        /// <param name="parameters">The repetitive sample data generator parameters.</param>
        /// <param name="count">The number of <see cref="Quote"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Quote"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Quote>> GenerateQuoteAsync(
            this RepetitiveSampleGeneratorParameters parameters,
            int count)
        {
            var generator = new RepetitiveSampleQuoteGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Quote>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Trade"/> samples from a repetitive sample data generator.
        /// </summary>
        /// <param name="parameters">The repetitive sample data generator parameters.</param>
        /// <param name="count">The number of <see cref="Trade"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Trade"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Trade>> GenerateTradeAsync(
            this RepetitiveSampleGeneratorParameters parameters,
            int count)
        {
            var generator = new RepetitiveSampleTradeGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Trade>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Scalar"/> samples from a repetitive sample data generator.
        /// </summary>
        /// <param name="parameters">The repetitive sample data generator parameters.</param>
        /// <param name="count">The number of <see cref="Scalar"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Scalar"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Scalar>> GenerateScalarAsync(
            this RepetitiveSampleGeneratorParameters parameters,
            int count)
        {
            var generator = new RepetitiveSampleScalarGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(generator, count);
        }
    }
}
