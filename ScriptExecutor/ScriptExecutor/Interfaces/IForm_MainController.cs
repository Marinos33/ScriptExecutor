using ScriptExecutor.Model;

namespace ScriptExecutor.Interfaces
{
    public interface IForm_MainController
    {
        /// <summary>
        /// add a game to the list in data
        /// </summary>
        /// <param name="game">the game to add</param>
        void AddGame(Game game);

        /// <summary>
        /// edit the game in the list in data
        /// </summary>
        /// <param name="game">the edited info of the game</param>
        /// <param name="index">the index of the game to edit in the list</param>
        void OnModifyClick(Game game, int index);

        /// <summary>
        /// Remove a game from the list in data
        /// </summary>
        /// <param name="index">the index of the game in the list</param>
        void OnDeleteClick(int index);

        /// <summary>
        /// change the state of the game (enabled or not)
        /// </summary>
        /// <param name="index">the index of the game for which the state will be changed</param>
        /// <param name="c">the state (true or false)</param>
        void OnCheck(int index, bool c);

        /// <summary>
        /// open the logs file
        /// </summary>
        /// <returns> return true if the file has been open. false if not </returns>
        bool OpenLogs();
    }
}