using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Mbs.Api.Models.Trading.Instruments;
using Newtonsoft.Json;

namespace Mbs.Api.Services.Trading.Instruments
{
    /// <inheritdoc/>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class InstrumentListDataService : IInstrumentListDataService
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private static readonly List<Instrument> EmptyList = new List<Instrument>();
        private static readonly Dictionary<string, IEnumerable<Instrument>> ListDictionary = new Dictionary<string, IEnumerable<Instrument>>();
        private static readonly object ListDictionaryLock = new object();

        /// <inheritdoc/>
        public void AddListFromJsonFile(string name, string jsonFilePath)
        {
            var json = File.ReadAllText(jsonFilePath);
            AddListFromJsonString(name, json);
        }

        /// <inheritdoc/>
        public void AddListFromJsonString(string name, string json)
        {
            IEnumerable<Instrument> list = JsonConvert.DeserializeObject<IEnumerable<Instrument>>(json);
            AddToDictionary(name, list);
        }

        /// <inheritdoc/>
        public async Task AddListAsync(string name, IEnumerable<Instrument> list)
        {
            await Task.Run(() => AddToDictionary(name, list));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Instrument>> GetListAsync(string name)
        {
            return await Task.Run(() =>
            {
                IEnumerable<Instrument> list;
                lock (ListDictionaryLock)
                    if (!ListDictionary.TryGetValue(name, out list))
                        return EmptyList;
                return list;
            })
            .ConfigureAwait(false);
        }

        private static void AddToDictionary(string name, IEnumerable<Instrument> list)
        {
            lock (ListDictionaryLock)
                if (!ListDictionary.TryAdd(name, list))
                    throw new ArgumentException($"List name \"{name}\" is already in use.", nameof(name));
        }
    }
}
