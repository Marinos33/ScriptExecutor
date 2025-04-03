using ScriptExecutor.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScriptExecutor.Application.Interfaces
{
    public interface IGameRepository
    {
        Task AddGameAsync(Game game);

        Task EditGameAsync(Game game, int index);

        List<Game> GetGames();

        Task RemoveGameAsync(int index);
    }
}