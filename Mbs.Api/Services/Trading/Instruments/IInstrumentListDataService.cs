using System.Collections.Generic;
using System.Threading.Tasks;
using Mbs.Api.Models.Trading.Instruments;

namespace Mbs.Api.Services.Trading.Instruments
{
    /// <summary>
    /// Manages collections of <see cref="Instrument"/>.
    /// </summary>
    public interface IInstrumentListDataService
    {
        /// <summary>
        /// Adds a collection of <see cref="Instrument"/> from a given JSON file.
        /// </summary>
        /// <param name="name">A name of the list.</param>
        /// <param name="jsonFilePath">A file path to the JSON file.</param>
        void AddListFromJsonFile(string name, string jsonFilePath);

        /// <summary>
        /// Adds a collection of <see cref="Instrument"/> from a given JSON string.
        /// </summary>
        /// <param name="name">A name of the list.</param>
        /// <param name="json">A JSON string.</param>
        void AddListFromJsonString(string name, string json);

        /// <summary>
        /// Adds a collection of <see cref="Instrument"/> for a given list name.
        /// </summary>
        /// <param name="name">A name of the list.</param>
        /// <param name="list">A collection of instruments.</param>
        /// <returns>Nothing.</returns>
        Task AddListAsync(string name, IEnumerable<Instrument> list);

        /// <summary>
        /// Gets a collection of <see cref="Instrument"/> for a given list name.
        /// </summary>
        /// <param name="name">A name of the list.</param>
        /// <returns>A collection of <see cref="Instrument"/>.</returns>
        Task<IEnumerable<Instrument>> GetListAsync(string name);
    }
}
