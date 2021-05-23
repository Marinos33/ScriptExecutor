using GameSaveBackup.Model;
using ScriptExecutor.Interfaces;
using System.Collections.Generic;

namespace ScriptExecutor.Persistence
{
    public class Data : IData
    {
        public List<Game> ListOfGame { get; set; }
        public Game CurrentGame { get; set; }

        /*used to say to the program : there are no program from the list who is running*/

        public void ResetCurrentGame()
        {
            CurrentGame.Name = null;
            CurrentGame.ScriptPath = null;
            CurrentGame.ExecutablePath = null;
            CurrentGame.Update();
        }

        public void AddGame(Game game)
        {
            ListOfGame.Add(game); //add the game to the list
            ListOfGame.Sort((x, y) => x.Name.CompareTo(y.Name)); //alphabetical sort
        }

        public void RemoveGame(int index)
        {
            ListOfGame.RemoveAt(index); //remove the game from the list
            ListOfGame.Sort((x, y) => x.Name.CompareTo(y.Name)); //alphabetical sort
        }
    }
}