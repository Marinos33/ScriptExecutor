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
        private readonly IData _data;
        private readonly IJsonManager _jsonManager;
        private readonly ILogManager _logManager;
        private readonly IThreadSystem _threadSystem;
        private readonly IScriptRunner _scriptRunner;

        private readonly Game gameTest = new()
        {
            Name = "Cmd",
            Enabled = false,
            Script = "echo \"Hello World\"",
            ExecutableFile = "cmd.exe",
        };

        public ScriptExecutorTests()
        {
            _data = new Data();
            _logManager = new LogManager();
            _scriptRunner = new ScriptRunner();
            _jsonManager = new JsonManager(_data);
            _threadSystem = new ThreadSystem(_data, _scriptRunner, _logManager);

            //fake data
            _data.ListOfGame = Enumerable.Empty<Game>().ToList();
        }

        [TestMethod]
        public void GameCanBeAdded()
        {
            int startCount = _data.ListOfGame.Count; //get the size of the list of the fake data

            _data.AddGame(gameTest);

            Assert.IsTrue(startCount < _data.ListOfGame.Count); //if the list is larger after the game ahs been added it's OK
        }

        [TestMethod]
        public void GameCanBeRemoved()
        {
            _data.AddGame(gameTest);

            int index = _data.ListOfGame.IndexOf(_data.ListOfGame.Find(game => game.Name.Equals(gameTest.Name))); //get the index of the game with the name "Test"

            _data.RemoveGame(index);

            Assert.IsFalse(_data.ListOfGame.Contains(_data.ListOfGame.Find(game => game.Name.Equals("Test")))); //if the list of the game does not contains the game "Test" it's OK
        }

        [TestMethod]
        public void GameCanBeEdited()
        {
            _data.AddGame(gameTest);

            Game oldGame = _data.ListOfGame.Find(game => game.Name.Equals(gameTest.Name)); //get the game with the name "Test"

            int index = _data.ListOfGame.IndexOf(oldGame); //get the index of the game with the name "Test"

            Game game2 = new()
            {
                Name = "Test2",
                Enabled = false,
                Script = "",
                ExecutableFile = "",
            };

            _data.EditGame(game2, index);

            Assert.AreNotEqual(_data.ListOfGame[index], oldGame);
        }

        [TestMethod]
        public void CurrentGameCanBeReset()
        {
            _data.CurrentGame = gameTest;

            _data.ResetCurrentGame();

            Assert.IsTrue(_data.CurrentGame.Name == null && _data.CurrentGame.Script == null && _data.CurrentGame.ExecutableFile == null);
        }

        [TestMethod]
        public void GameCanBeDeepCopied()
        {
            Assert.AreEqual(gameTest.Name, gameTest.DeepCopy().Name);
            Assert.AreEqual(gameTest.Script, gameTest.DeepCopy().Script);
            Assert.AreEqual(gameTest.ExecutableFile, gameTest.DeepCopy().ExecutableFile);
            Assert.AreEqual(gameTest.Enabled, gameTest.DeepCopy().Enabled);
        }

        [TestMethod]
        public void ProcessCanBeFound()
        {
            _data.AddGame(gameTest);

            static void HandleEvent(object sender, EventArgs args) => Console.WriteLine("hello world");

            Thread myThread = new(() => _threadSystem.SearchProcess(HandleEvent))
            {
                IsBackground = true
            };

            myThread.Start();
            Process.Start("CMD.exe");
            Thread.Sleep(5000);

            Assert.IsNotNull(_data.CurrentGame);
        }

        [TestMethod]
        public void ScriptCanBeRun()
        {
            _data.AddGame(gameTest);

            _data.CurrentGame = _data.ListOfGame[0];

            bool success = _scriptRunner.RunScript(_data.CurrentGame.Script).ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void JsonCanBeWrited()
        {
            _data.AddGame(gameTest);

            try
            {
                _jsonManager.WriteJson();
                File.Delete("Data.json");
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, e.Message);
            }
        }

        [TestMethod]
        public void JsonCanBeRead()
        {
            _data.AddGame(gameTest);

            _jsonManager.WriteJson();

            _data.ListOfGame.Clear();

            _data.ListOfGame = _jsonManager.ReadJson().Result.ToList();

            File.Delete("Data.json");

            Assert.IsTrue(_data.ListOfGame.Count > 0);
        }

        [TestMethod]
        public void ReadLogIfNotExistTest()
        {
            string log = _logManager.ReadLog().Result;

            Assert.AreEqual(log, string.Empty);
        }

        [TestMethod]
        public void LogCanBeWrite()
        {
            try
            {
                _logManager.AddLog("test");

                File.Delete("Logs.txt");

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, e.Message);
            }
        }

        [TestMethod]
        public void LogCanBeRead()
        {
            _logManager.AddLog("test");

            string log = _logManager.ReadLog().Result;

            File.Delete("Logs.txt");

            Assert.IsFalse(string.IsNullOrEmpty(log));
        }
    }
}