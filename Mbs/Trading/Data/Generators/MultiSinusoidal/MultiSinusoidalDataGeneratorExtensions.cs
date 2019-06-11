using System.Threading.Tasks;

namespace Mbs.Trading.Data.Generators.MultiSinusoidal
{
    /// <summary>
    /// A multi-sinusoidal data generator extensions.
    /// </summary>
    public static class MultiSinusoidalDataGeneratorExtensions
    {
        /// <summary>
        /// Generates a specified amount of <see cref="Ohlcv"/> samples from a multi-sinusoidal data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Ohlcv"/> multi-sinusoidal data generator parameters.</param>
        /// <param name="count">The number of <see cref="Ohlcv"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Ohlcv"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Ohlcv>> GenerateAsync(
            this MultiSinusoidalOhlcvGeneratorParameters parameters,
            int count)
        {
            var generator = new MultiSinusoidalOhlcvGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Ohlcv>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Quote"/> samples from a multi-sinusoidal data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Quote"/> multi-sinusoidal data generator parameters.</param>
        /// <param name="count">The number of <see cref="Quote"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Quote"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Quote>> GenerateAsync(
            this MultiSinusoidalQuoteGeneratorParameters parameters,
            int count)
        {
            var generator = new MultiSinusoidalQuoteGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Quote>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Trade"/> samples from a multi-sinusoidal data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Trade"/> multi-sinusoidal data generator parameters.</param>
        /// <param name="count">The number of <see cref="Trade"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Trade"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Trade>> GenerateAsync(
            this MultiSinusoidalTradeGeneratorParameters parameters,
            int count)
        {
            var generator = new MultiSinusoidalTradeGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Trade>.GenerateOutputAsync(generator, count);
        }

        /// <summary>
        /// Generates a specified amount of <see cref="Scalar"/> samples from a multi-sinusoidal data generator.
        /// </summary>
        /// <param name="parameters">The <see cref="Scalar"/> multi-sinusoidal data generator parameters.</param>
        /// <param name="count">The number of <see cref="Scalar"/> samples of data to generate.</param>
        /// <returns>The generated <see cref="Scalar"/> data.</returns>
        public static async Task<SyntheticDataGeneratorOutput<Scalar>> GenerateAsync(
            this MultiSinusoidalScalarGeneratorParameters parameters,
            int count)
        {
            var generator = new MultiSinusoidalScalarGenerator(parameters);
            return await SyntheticDataGeneratorGeneration<Scalar>.GenerateOutputAsync(generator, count);
        }
    }
}
