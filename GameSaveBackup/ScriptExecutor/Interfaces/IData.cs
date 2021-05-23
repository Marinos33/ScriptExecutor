using GameSaveBackup.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
