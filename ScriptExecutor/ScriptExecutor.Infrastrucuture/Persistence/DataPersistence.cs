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
        private List<Game> _gamesList;
        public List<Game> GamesList
        {
            get => OrderByName(_gamesList).ToList();
            set => _gamesList = value;
        }

        private readonly IJsonManager _jsonManager;

        public DataPersistence(IJsonManager jsonManager)
        {
            _jsonManager = jsonManager;
            _gamesList = LoadDataAsync().Result;
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

        private static IEnumerable<Game> OrderByName(IEnumerable<Game> list)
        {
            return list.OrderBy(game => game.Name);
        }
    }
}