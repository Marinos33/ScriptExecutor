using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using ScriptExecutor.Infrastrucuture.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture.Repositories
{
    internal class GameRepository : IGameRepository
    {
        private readonly IDataPersistence _dataPersistence;

        public GameRepository(IDataPersistence dataPersistence)
        {
            _dataPersistence = dataPersistence;
        }

        public async Task AddGameAsync(Game game)
        {
            _dataPersistence
                .GamesList
                .Add(game);

            await _dataPersistence.SaveDataAsync();
        }

        public async Task RemoveGameAsync(int index)
        {
            _dataPersistence
                .GamesList
                .RemoveAt(index);

            await _dataPersistence.SaveDataAsync();
        }

        public async Task EditGameAsync(Game game, int index)
        {
            _dataPersistence
                .GamesList[index] = game;

            await _dataPersistence.SaveDataAsync();
        }

        public List<Game> GetGames()
        {
            return _dataPersistence.GamesList.ToList();
        }
    }
}