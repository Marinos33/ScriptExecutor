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

        private readonly ILogManager _logManager;
        private readonly IGameRepository _gameRepository;

        public Form_MainController(ILogManager logManager, IGameRepository gameRepository)
        {
            _logManager = logManager;
            _gameRepository = gameRepository;
        }

        public void AddGame(Game game)
        {
            _gameRepository.AddGame(game);
            _logManager.WriteLogAsync(DateTime.Now.ToString() + " > the game : " + game.Name + " has been added");
        }

        public void OnModifyClick(Game game, int index)
        {
            //replace the oldGame with a new one
            _gameRepository.EditGame(game, index);
            _logManager.WriteLogAsync(DateTime.Now.ToString() + "> the game : " + game.Name + " has been modified");
        }

        public void OnDeleteClick(int index)
        {
            string oldGame = _gameRepository.GetGames()[index].Name;
            //remove the game
            _gameRepository.RemoveGame(index);
            _logManager.WriteLogAsync(DateTime.Now.ToString() + "> the game : " + oldGame + " has been deleted");
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