using System.Collections.Generic;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// A generic synthetic data generator interface.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public interface ISyntheticDataGenerator<out T>
        where T : TemporalEntity
    {
        /// <summary>
        /// Gets the text which identifies the synthetic data generator.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the text which identifies an instance of the synthetic data generator.
        /// </summary>
        string Moniker { get; }

        /// <summary>
        /// Generates a new synthetic data object.
        /// </summary>
        /// <returns>A new generated synthetic data object.</returns>
        T GenerateNext();

        /// <summary>
        /// Generates a collection of new synthetic data objects.
        /// </summary>
        /// <param name="count">A number of new objects to generate.</param>
        /// <returns>A collection of new synthetic data objects.</returns>
        IEnumerable<T> GenerateNext(int count);

        /// <summary>
        /// Resets the synthetic data generator.
        /// This allows to replay the same synthetic data sequence once more.
        /// </summary>
        void Reset();
    }
}
