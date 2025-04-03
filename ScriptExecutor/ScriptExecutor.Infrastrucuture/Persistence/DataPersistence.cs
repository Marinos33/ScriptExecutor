using ScriptExecutor.Domain.Model;
using ScriptExecutor.Infrastrucuture.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture.Persistence
{
    public interface IDataPersistence
    {
        List<Game> GamesList { get; set; }

        Task<List<Game>> LoadDataAsync();

        Task SaveDataAsync();
    }

    public class DataPersistence : IDataPersistence
    {
        public List<Game> GamesList { get; set; } = new List<Game>();

        private readonly IJsonManager _jsonManager;

        public DataPersistence(IJsonManager jsonManager)
        {
            _jsonManager = jsonManager;
            GamesList = LoadDataAsync().Result;
        }

        public async Task<List<Game>> LoadDataAsync()
        {
            var games = await _jsonManager.ReadJsonAsync();

            return games.ToList();
        }

        public async Task SaveDataAsync()
        {
            await _jsonManager.WriteJsonAsync(GamesList);
        }
    }
}