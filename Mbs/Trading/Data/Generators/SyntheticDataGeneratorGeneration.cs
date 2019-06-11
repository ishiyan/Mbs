using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// A generic synthetic data generator generation function.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    internal static class SyntheticDataGeneratorGeneration<T>
        where T : TemporalEntity
    {
        /// <summary>
        /// Generates a specified amount of data form a synthetic data generator.
        /// </summary>
        /// <param name="generator">The synthetic data generator.</param>
        /// <param name="count">The number of samples of data to generate. Should be positive.</param>
        /// <returns>The generated data.</returns>
        internal static async Task<SyntheticDataGeneratorOutput<T>> GenerateOutputAsync(
            ISyntheticDataGenerator<T> generator,
            int count)
        {
            if (count <= 0)
                throw new ArgumentException("The number of samples to generate should be positive", nameof(count));

            return await Task.Run(() =>
            {
                var output = new SyntheticDataGeneratorOutput<T>
                {
                    Moniker = generator.Moniker,
                    Name = generator.Name,
                    Data = generator.GenerateNext(count).ToArray()
                };

                return output;
            });
        }
    }
}
