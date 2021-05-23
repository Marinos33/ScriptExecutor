using GameSaveBackup.Model;
using System.Collections.Generic;

namespace ScriptExecutor.Interfaces
{
    public interface IData
    {
        List<Game> ListOfGame { get; set; }
        Game CurrentGame { get; set; }

        void ResetCurrentGame();

        void AddGame(Game game);

        void RemoveGame(int index);
    }
}