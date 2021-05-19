using GameSaveBackup.Model;
using System.Collections.Generic;

namespace GameSaveBackup.Interfaces
{
    public interface ICSVManager
    {
        void AddGame(Game game);

        void RemoveGame(int index);

        void WriteCsv();

        List<Game> GetListOfGame();

        Game GetCurrentGame();

        void SetCurrentGame(Game game);

        void ResetCurrentGame();
    }
}