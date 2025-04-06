using Quartz;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture.Jobs
{
    [DisallowConcurrentExecution]
    internal class ProcessusObserverJob : IJob
    {
        private readonly IGameRepository _gameRepository;
        private readonly IScriptRunner _scriptRunner;
        private readonly ILogManager _logManager;
        public Game RunningGame { get; private set; }

        public ProcessusObserverJob(IGameRepository gameRepository, IScriptRunner scriptRunner, ILogManager logManager)
        {
            _gameRepository = gameRepository;
            _scriptRunner = scriptRunner;
            _logManager = logManager;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var gamesList = _gameRepository.GetGames();

            if (gamesList.Count <= 0)
            {
                return;
            }

            foreach (var game in gamesList)
            {
                if (Process.GetProcessesByName(Path.ChangeExtension(game.ExecutableFile, null)).Length != 0)
                {
                    RunningGame = game.DeepCopy();
                    RunningGame.Update();
                    break;
                }
            }

            if(RunningGame is null)
            {
                return;
            }

            Process runningApp = (from p
                             in Process.GetProcesses()
                                  where p.ProcessName == Path.ChangeExtension(RunningGame.ExecutableFile, null)
                                  select p)
                             .FirstOrDefault();

            if (runningApp is null)
            {
                return;
            }

            if (RunningGame.RunOnStart)
            {
                await RunScript().ConfigureAwait(false);
            }

            if (RunningGame.RunAfterShutdown)
            {
                runningApp.WaitForExit();

                await RunScript().ConfigureAwait(false);
            }

            RunningGame = null;
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
                await _logManager.WriteLogAsync(DateTime.Now.ToString() + "> unable to run the script for " + RunningGame.ExecutableFile);
            }
        }
    }
}