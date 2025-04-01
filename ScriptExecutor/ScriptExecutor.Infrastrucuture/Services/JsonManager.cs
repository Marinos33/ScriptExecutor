using Newtonsoft.Json;
using ScriptExecutor.Domain.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture.Services
{
    public interface IJsonManager
    {
        /// <summary>
        /// completely rewrite the json
        /// </summary>
        Task WriteJsonAsync(IEnumerable<Game> games);

        /// <summary>
        /// read the Json, convert all entry to Game object and retunr a list of items
        /// </summary>
        /// <returns>the list of Game from the Json</returns>
        Task<IEnumerable<Game>> ReadJsonAsync();
    }

    public class JsonManager : IJsonManager
    {
        /// <summary>
        /// the path to the CSV which contains all the game (use to do the save system)
        /// </summary>
        private const string CSV_PATH = "Data.json";

        public JsonManager()
        {
        }

        public async Task<IEnumerable<Game>> ReadJsonAsync()
        {
            if (File.Exists(CSV_PATH))
            {
                string json = await File.ReadAllTextAsync(CSV_PATH).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<IEnumerable<Game>>(json); //pass all record to the list
            }
            return Enumerable.Empty<Game>();
        }

        public async Task WriteJsonAsync(IEnumerable<Game> games)
        {
            string json = JsonConvert.SerializeObject(games);
            await File.WriteAllTextAsync(CSV_PATH, json).ConfigureAwait(false);
        }
    }
}