using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Diagnostics;
using System.IO;

namespace ScriptExecutor.Application.Controllers
{
    public class Form_MainController : IForm_MainController
    {
        private const string LOGS_PATH = "Logs.txt";

        private readonly IJsonManager _jsonManager; //the model from MVC pattern
        private readonly ILogManager _logManager; //the model from MVC pattern
        private readonly IData _data; //the model wihch contains the data

        public Form_MainController(IJsonManager jsonManager, ILogManager logManager, IData data)
        {
            _jsonManager = jsonManager;
            _logManager = logManager;
            _data = data;
        }

        public void AddGame(Game game)
        {
            _data.AddGame(game);
            _jsonManager.WriteJson();
            _logManager.AddLog(DateTime.Now.ToString() + " > the game : " + game.Name + " has been added");
        }

        public void OnModifyClick(Game game, int index)
        {
            //replace the oldGame with a new one
            _data.EditGame(game, index);
            _jsonManager.WriteJson();
            _logManager.AddLog(DateTime.Now.ToString() + "> the game : " + game.Name + " has been modified");
        }

        public void OnDeleteClick(int index)
        {
            string oldGame = _data.ListOfGame[index].Name; //get the game's name to delete for the log
            //remove the game
            _data.RemoveGame(index);
            _jsonManager.WriteJson();
            _logManager.AddLog(DateTime.Now.ToString() + "> the game : " + oldGame + " has been deleted");
        }

        public bool OpenLogs()
        {
            if (File.Exists(LOGS_PATH))
            {
                var p = new Process
                {
                    StartInfo = new ProcessStartInfo(LOGS_PATH)
                    {
                        UseShellExecute = true
                    }
                };
                return p.Start();
            }
            return false;
        }
    }
}