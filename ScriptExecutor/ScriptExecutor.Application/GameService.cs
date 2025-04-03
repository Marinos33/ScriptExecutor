using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Threading.Tasks;

namespace ScriptExecutor.Application
{
    public interface IGameService
    {
        /// <summary>
        /// add a game to the list in data
        /// </summary>
        /// <param name="game">the game to add</param>
        Task AddGameAsync(Game game);

        /// <summary>
        /// edit the game in the list in data
        /// </summary>
        /// <param name="game">the edited info of the game</param>
        /// <param name="index">the index of the game to edit in the list</param>
        Task EditGameAsync(Game game, int index);

        /// <summary>
        /// Remove a game from the list in data
        /// </summary>
        /// <param name="index">the index of the game in the list</param>
        Task DeleteGameAsync(int index);
    }

    public class GameService : IGameService
    {
        private readonly ILogManager _logManager;
        private readonly IGameRepository _gameRepository;

        public GameService(ILogManager logManager, IGameRepository gameRepository)
        {
            _logManager = logManager;
            _gameRepository = gameRepository;
        }

        public async Task AddGameAsync(Game game)
        {
            await _gameRepository.AddGameAsync(game);

            await _logManager.WriteLogAsync(DateTime.Now.ToString() + " > the game : " + game.Name + " has been added");
        }

        public async Task EditGameAsync(Game game, int index)
        {
            await _gameRepository.EditGameAsync(game, index);

            await _logManager.WriteLogAsync(DateTime.Now.ToString() + "> the game : " + game.Name + " has been modified");
        }

        public async Task DeleteGameAsync(int index)
        {
            string oldGame = _gameRepository.GetGames()[index].Name;
            await _gameRepository.RemoveGameAsync(index);

            await _logManager.WriteLogAsync(DateTime.Now.ToString() + "> the game : " + oldGame + " has been deleted");
        }
    }
}