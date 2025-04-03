using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture.Persistence
{
    public class ThreadSystem : IThreadSystem
    {
        private readonly IGameRepository _gameRepository;
        private readonly IScriptRunner _scriptRunner;
        private readonly ILogManager _logManager;
        public Game? RunningGame { get; private set; }

        private const int timer = 2000;

        public ThreadSystem(IGameRepository gameRepository, IScriptRunner scriptRunner, ILogManager logManager)
        {
            _gameRepository = gameRepository;
            _scriptRunner = scriptRunner;
            _logManager = logManager;
        }

        public async void SearchProcess(EventHandler HandleEvent)
        {
            Thread.Sleep(timer); //wait 2 seconds

            bool found = false; //boolean to verify if a game has been found

            //iteration through list
            int i = 0;
            while (!found) //until a game has been found
            {
                var gamesList = _gameRepository.GetGames();
                if (gamesList.Count > 0
                    && i <= gamesList.Count
                    && Process.GetProcessesByName(Path.ChangeExtension(gamesList[i].ExecutableFile, null)).Length != 0) //check if the game to observe is not null or ""
                {
                    found = true;
                    RunningGame = gamesList[i].DeepCopy(); //take the game actually running in memory
                    RunningGame.SomethingHappened += HandleEvent; //event for observer pattern
                    RunningGame.Update();
                    break; //stop the loop
                }

                i++;

                if (i >= gamesList.Count) //go back to the start of the list
                {
                    i = 0;
                }

                Thread.Sleep(timer); //wait 2 seconds
            }

            Process runningApp = (from p
                             in Process.GetProcesses()
                                  where p.ProcessName == Path.ChangeExtension(RunningGame.ExecutableFile, null)
                                  select p)
                             .FirstOrDefault(); //select the first process with the given name in the process running

            if (RunningGame.RunOnStart && RunningGame.RunAfterShutdown)
            {
                await RunScript().ConfigureAwait(false);
                runningApp.WaitForExit(); //the thread wait until the process has stopped
                await RunScript().ConfigureAwait(false);
            }
            else if (RunningGame.RunAfterShutdown)
            {
                runningApp.WaitForExit(); //the thread wait until the process has stopped
                await RunScript().ConfigureAwait(false);
            }
            else if (RunningGame.RunOnStart)
            {
                await RunScript().ConfigureAwait(false);
                runningApp.WaitForExit(); //the thread wait until the process has stopped
            }

            RunningGame = null;
            RunningGame.SomethingHappened += HandleEvent; //event for observer pattern

            SearchProcess(HandleEvent);
        }

        private async Task RunScript()
        {
            bool isScriptExecuted = await _scriptRunner.RunScriptAsync(RunningGame.Script).ConfigureAwait(false); //run a script
            if (isScriptExecuted)
            {
                await _logManager.WriteLogAsync(DateTime.Now.ToString() + "> script for " + RunningGame.ExecutableFile + " has been launched");
            }
            else
            {
                // TODO
                //MessageBox.Show("ScriptExecutor: unable to run the script");
                await _logManager.WriteLogAsync(DateTime.Now.ToString() + "> unable to run the script for " + RunningGame.ExecutableFile);
            }
        }
    }
}