using Newtonsoft.Json;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture.Services
{
    public class JsonManager : IJsonManager
    {
        /// <summary>
        /// the path to the CSV which contains all the game (use to do the save system)
        /// </summary>
        private const string CSV_PATH = "Data.json";

        private readonly IData _data;

        public JsonManager(IData data)
        {
            _data = data;
        }

        public async Task<IEnumerable<Game>> ReadJson()
        {
            if (File.Exists(CSV_PATH))
            {
                string json = await File.ReadAllTextAsync(CSV_PATH).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<IEnumerable<Game>>(json); //pass all record to the list
            }
            return Enumerable.Empty<Game>();
        }

        public async void WriteJson()
        {
            var records = _data.ListOfGame;

            string json = JsonConvert.SerializeObject(records);
            await File.WriteAllTextAsync(CSV_PATH, json).ConfigureAwait(false);
        }
    }
}