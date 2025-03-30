using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace ScriptExecutor.Infrastrucuture.Persistence
{
    public class Data : IData
    {
        public List<Game> ListOfGame { get; set; }

        public Game CurrentGame { get; set; }

        public void ResetCurrentGame()
        {
            CurrentGame.Name = null;
            CurrentGame.Script = null;
            CurrentGame.ExecutableFile = null;
            CurrentGame.RunOnStart = false;
            CurrentGame.RunAfterShutdown = true;
            CurrentGame.Update();
        }

        public void AddGame(Game game)
        {
            ListOfGame.Add(game); //add the game to the list
            ListOfGame = OrderByName(ListOfGame).ToList(); //alphabetical sort
        }

        public void RemoveGame(int index)
        {
            ListOfGame.RemoveAt(index); //remove the game from the list
            ListOfGame = OrderByName(ListOfGame).ToList(); //alphabetical sort
        }

        public void EditGame(Game game, int index)
        {
            //replace the oldGame with a new one
            ListOfGame[index] = game;
            ListOfGame = OrderByName(ListOfGame).ToList();
        }

        private static IEnumerable<Game> OrderByName(IEnumerable<Game> list)
        {
            return list.OrderBy(game => game.Name);
        }
    }
}