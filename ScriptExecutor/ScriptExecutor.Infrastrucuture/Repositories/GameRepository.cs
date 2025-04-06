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
            var count = _dataPersistence.GamesList.Count;

            _dataPersistence
                .GamesList
                .Add(game);

            await _dataPersistence.SaveDataAsync();

            //check if add worked
            if (_dataPersistence.GamesList.Count == count)
            {
                throw new System.Exception("Game was not added");
            }
        }

        public async Task RemoveGameAsync(int index)
        {
            var count = _dataPersistence.GamesList.Count;

            _dataPersistence
                .GamesList
                .RemoveAt(index);

            await _dataPersistence.SaveDataAsync();

            //check if remove worked
            if (_dataPersistence.GamesList.Count == count)
            {
                throw new System.Exception("Game was not removed");
            }
        }

        public async Task EditGameAsync(Game game, int index)
        {
            _dataPersistence
                .GamesList[index] = game;

            await _dataPersistence.SaveDataAsync();

            //check if edit worked
            if (!CheckIfGamesAreEquals(game, _dataPersistence.GamesList[index]))
            {
                throw new System.Exception("Game was not edited");
            }
        }

        public List<Game> GetGames()
        {
            return _dataPersistence.GamesList.OrderBy(game => game.Name).ToList();
        }

        private bool CheckIfGamesAreEquals(Game game1, Game game2)
        {
            return game1.Name == game2.Name &&
                   game1.ExecutableFile == game2.ExecutableFile &&
                   game1.Script == game2.Script &&
                   game1.RunOnStart == game2.RunOnStart &&
                   game1.RunAfterShutdown == game2.RunAfterShutdown;
        }
    }
}