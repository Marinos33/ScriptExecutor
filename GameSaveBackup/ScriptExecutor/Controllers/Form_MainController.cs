using ScriptExecutor.Interfaces;
using ScriptExecutor.Model;
using System;

namespace ScriptExecutor.Controllers
{
    public class Form_MainController : IForm_MainController
    {
        private readonly ICSVManager _csvManager; //the model from MVC pattern
        private readonly ILogManager _logManager; //the model from MVC pattern
        private readonly IData _data; //the model wihch contains the data

        public Form_MainController(ICSVManager csvManager, ILogManager logManager, IData data)
        {
            _csvManager = csvManager;
            _logManager = logManager;
            _data = data;
        }

        public void AddGame(Game game)
        {
            _data.AddGame(game);
            _csvManager.WriteCsv();
            _logManager.AddLog(DateTime.Now.ToString() + " > the game : " + game.Name + " has been added");
        }

        public void OnModifyClick(Game game, int index)
        {
            //replace the oldGame with a new one
            _data.ListOfGame[index] = game;
            _data.ListOfGame.Sort((x, y) => x.Name.CompareTo(y.Name));
            _csvManager.WriteCsv();
            _logManager.AddLog(DateTime.Now.ToString() + "> the game : " + game.Name + " has been modified");
        }

        public void OnDeleteClick(int index)
        {
            string oldGame = _data.ListOfGame[index].Name; //get the game name to delete for the log
            //remove the game
            _data.RemoveGame(index);
            _csvManager.WriteCsv();
            _logManager.AddLog(DateTime.Now.ToString() + "> the game : " + oldGame + " has been deleted");
        }

        public void OnCheck(int index, bool c)
        {
            _data.ListOfGame[index].Enabled = c;
            _csvManager.WriteCsv();
        }
    }
}