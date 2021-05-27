using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptExecutor.Interfaces;
using ScriptExecutor.Model;
using ScriptExecutor.Persistence;
using ScriptExecutor.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

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
        public void EditGame()
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

            Game oldGame = data.ListOfGame.Find(game => game.Name.Equals("Test")); //get the game with the name "Test"

            int index = data.ListOfGame.IndexOf(oldGame); //get the index of the game with the name "Test"

            Game game2 = new()
            {
                Name = "Test2",
                Enabled = false,
                Script = "",
                ExecutableFile = "",
            };

            data.EditGame(game2, index);

            Assert.AreNotEqual(data.ListOfGame[index], oldGame);
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

        [TestMethod]
        public void CloneGame()
        {
            Game game = new()
            {
                Name = "Test",
                Enabled = false,
                Script = "",
                ExecutableFile = "",
            };

            Assert.AreEqual(game, game.Clone());
        }

        [TestMethod]
        public void ProcessCanBeFound()
        {
            //create fake data
            IData data = new Data
            {
                ListOfGame = Enumerable.Empty<Game>().ToList(),
            };

            Game game = new()
            {
                Name = "Cmd",
                Enabled = false,
                Script = "",
                ExecutableFile = "cmd.exe",
            };

            data.AddGame(game);

            ILogManager logManager = new LogManager();

            IThreadSystem threadSystem = new ThreadSystem(data, logManager);

            static void HandleEvent(object sender, EventArgs args) => Console.WriteLine("hello world");

            Thread myThread = new(() => threadSystem.SearchProcess(HandleEvent))
            {
                IsBackground = true
            };

            myThread.Start();
            Process.Start("CMD.exe");
            Thread.Sleep(5000);

            Assert.IsNotNull(data.CurrentGame);
        }

        [TestMethod]
        public void ScriptCanBeRun()
        {
            //create fake data
            IData data = new Data
            {
                ListOfGame = Enumerable.Empty<Game>().ToList(),
            };

            Game game = new()
            {
                Name = "Cmd",
                Enabled = false,
                Script = "echo \"Hello World\"",
                ExecutableFile = "cmd.exe",
            };

            data.AddGame(game);

            data.CurrentGame = data.ListOfGame[0];

            bool success;

            var fileName = Guid.NewGuid().ToString() + ".bat"; //generate random name for the file
            var batchPath = Path.Combine(Environment.GetEnvironmentVariable("temp"), fileName); //set the path of the file to write in the appdata/temp

            var batchCode = data.CurrentGame.Script; //the script
            try
            {
                File.WriteAllTextAsync(batchPath, batchCode); //create the file in appdata/temp with the script as content

                Process.Start(batchPath).WaitForExit(); //run the script

                File.Delete(batchPath); //delete the script

                success = true;
            }
            catch
            {
                success = false;
            }

            Assert.IsTrue(success);
        }
    }
}