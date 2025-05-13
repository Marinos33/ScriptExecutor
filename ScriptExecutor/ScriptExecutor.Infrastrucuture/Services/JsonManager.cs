using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScriptExecutor.Domain.Model;

namespace ScriptExecutor.Infrastrucuture.Services
{
    public interface IJsonManager
    {
        /// <summary>
        /// completely rewrite the json
        /// </summary>
        Task WriteJsonAsync(IEnumerable<Process> processes);

        /// <summary>
        /// read the Json, convert all entry to Process object and retunr a list of items
        /// </summary>
        /// <returns>the list of Process from the Json</returns>
        Task<IEnumerable<Process>> ReadJsonAsync();
    }

    public class JsonManager : IJsonManager
    {
        /// <summary>
        /// the path to the file data (json) which contains all the processes (use to do the save system)
        /// </summary>
        private const string DATAFILE_PATH = "C:\\Users\\loicm\\Downloads\\Data.json";
        public async Task<IEnumerable<Process>> ReadJsonAsync()
        {
            if (File.Exists(DATAFILE_PATH))
            {
                string json = await File.ReadAllTextAsync(DATAFILE_PATH).ConfigureAwait(false);
                var processes = JsonConvert.DeserializeObject<IEnumerable<Process>>(json);
                return processes ?? [];
            }

            return [];
        }

        public async Task WriteJsonAsync(IEnumerable<Process> processes)
        {
            string json = JsonConvert.SerializeObject(processes);
            await File.WriteAllTextAsync(DATAFILE_PATH, json).ConfigureAwait(false);
        }
    }
}