using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using ScriptExecutor.Infrastrucuture.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace ScriptExecutor.Infrastrucuture.Repositories
{
    internal class GameRepository : IGameRepository
    {
        private readonly IDataPersistence _dataPersistence;

        public GameRepository(IDataPersistence dataPersistence)
        {
            _dataPersistence = dataPersistence;
        }

        public void AddGame(Game game)
        {
            _dataPersistence
                .GamesList
                .Add(game);

            _dataPersistence.SaveDataAsync();
        }

        public void RemoveGame(int index)
        {
            _dataPersistence
                .GamesList
                .RemoveAt(index);

            _dataPersistence.SaveDataAsync();
        }

        public void EditGame(Game game, int index)
        {
            _dataPersistence
                .GamesList[index] = game;

            _dataPersistence.SaveDataAsync();
        }

        public List<Game> GetGames()
        {
            return _dataPersistence.GamesList.ToList();
        }
    }
}
