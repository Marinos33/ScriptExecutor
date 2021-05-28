using ScriptExecutor.Model;
using System.Collections.Generic;

namespace ScriptExecutor.Interfaces
{
    public interface IData
    {
        /// <summary>
        /// the list of game to observe
        /// </summary>
        List<Game> ListOfGame { get; set; }

        /// <summary>
        /// the game currently observed
        /// </summary>
        Game CurrentGame { get; set; }

        /// <summary>
        /// reset the current observed game by the app
        /// </summary>
        void ResetCurrentGame();

        /// <summary>
        /// add the game to the list of game in data
        /// </summary>
        /// <param name="game">the game to add</param>
        void AddGame(Game game);

        /// <summary>
        /// remove the game to the list of game in data
        /// </summary>
        /// <param name="index">the index of the game to remove in the list</param>
        void RemoveGame(int index);

        /// <summary>
        /// edit the game selected
        /// </summary>
        /// <param name="game">the edited info of the game</param>
        /// <param name="index">the index fof the game in the list to edit</param>
        void EditGame(Game game, int index);
    }
}