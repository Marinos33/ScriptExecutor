using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptExecutor.Interfaces;
using ScriptExecutor.Model;
using ScriptExecutor.Persistence;
using System.Linq;

namespace ScriptExecutorTests
{
    [TestClass]
    public class ScriptExecutorTests
    {
        [TestMethod]
        public void AddGame()
        {
            //create fake data
            IData data = new Data
            {
                ListOfGame = Enumerable.Empty<Game>().ToList(),
            };

            int startCount = data.ListOfGame.Count; //get the size of the list of the fake data

            Game game = new()
            {
                Name = "Test",
                Enabled = false,
                Script = "",
                ExecutableFile = "",
            };

            data.AddGame(game);

            Assert.IsTrue(startCount < data.ListOfGame.Count); //if the list is larger after the game ahs been added it's OK
        }

        [TestMethod]
        public void RemoveGame()
        {
            //create fake data
            IData data = new Data
            {
                ListOfGame = Enumerable.Empty<Game>().ToList(),
            };

            Game game = new()
            {
                Name = "Test",
                Enabled = false,
                Script = "",
                ExecutableFile = "",
            };

            data.AddGame(game);

            int index = data.ListOfGame.IndexOf(data.ListOfGame.Find(game => game.Name.Equals("Test"))); //get the index of the game with the name "Test"

            data.RemoveGame(index);

            Assert.IsFalse(data.ListOfGame.Contains(data.ListOfGame.Find(game => game.Name.Equals("Test")))); //if the list of the game does not contains the game "Test" it's OK
        }

        [TestMethod]
        public void ResetCurrentGame()
        {
            //create fake data
            IData data = new Data
            {
                ListOfGame = Enumerable.Empty<Game>().ToList(),
            };

            data.CurrentGame = (Game)(new()
            {
                Name = "Test",
                Enabled = false,
                Script = "",
                ExecutableFile = "",
            });

            data.ResetCurrentGame();

            Assert.IsTrue(data.CurrentGame.Name == null && data.CurrentGame.Script == null && data.CurrentGame.ExecutableFile == null);
        }
    }
}