using ScriptExecutor.Domain.Model;
using System.Collections.Generic;

namespace ScriptExecutor.Application.Interfaces
{
    public interface IGameRepository
    {
        void AddGame(Game game);
        void EditGame(Game game, int index);
        List<Game> GetGames();
        void RemoveGame(int index);
    }
}