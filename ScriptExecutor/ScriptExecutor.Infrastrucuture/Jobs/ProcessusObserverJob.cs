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
            try
            {
                var gamesList = _gameRepository.GetGames();

                if (gamesList.Count <= 0)
                {
                    return;
                }

                foreach (var game in gamesList)
                {
                    var processName = Path.GetFileNameWithoutExtension(game.ExecutableFile);
                    Process[] processes = null;

                    try
                    {
                        processes = Process.GetProcessesByName(processName);
                        if (processes.Length > 0)
                        {
                            RunningGame = game.DeepCopy();
                            RunningGame.Update();
                            await _logManager.WriteLogAsync($"{DateTime.Now}> Detected running game: {game.Name}");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        await _logManager.WriteLogAsync($"{DateTime.Now}> Error detecting processes: {ex.Message}");
                    }
                    finally
                    {
                        if (processes != null)
                        {
                            foreach (var process in processes)
                            {
                                process.Dispose();
                            }
                        }
                    }
                }

                if (RunningGame is null)
                {
                    return;
                }

                Process runningApp = null;
                try
                {
                    var processName = Path.GetFileNameWithoutExtension(RunningGame.ExecutableFile);
                    runningApp = Process.GetProcessesByName(processName).FirstOrDefault();

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
                        runningApp.EnableRaisingEvents = true;
                        runningApp.Exited += async (sender, args) =>
                        {
                            await RunScript().ConfigureAwait(false);
                            RunningGame = null;
                        };
                    }
                    else
                    {
                        RunningGame = null;
                    }
                }
                catch (Exception ex)
                {
                    await _logManager.WriteLogAsync($"{DateTime.Now}> Error handling process: {ex.Message}");
                }
                finally
                {
                    runningApp?.Dispose();
                }
            }
            catch (Exception ex)
            {
                await _logManager.WriteLogAsync($"{DateTime.Now}> Error in ProcessusObserverJob: {ex.Message}");
            }
        }

        private async Task RunScript()
        {
            try
            {
                bool isScriptExecuted = await _scriptRunner.RunScriptAsync(RunningGame.Script).ConfigureAwait(false);

                if (isScriptExecuted)
                {
                    await _logManager.WriteLogAsync($"{DateTime.Now}> Script for {RunningGame.ExecutableFile} has been launched");
                }
                else
                {
                    await _logManager.WriteLogAsync($"{DateTime.Now}> Unable to run the script for {RunningGame.ExecutableFile}");
                }
            }
            catch (Exception ex)
            {
                await _logManager.WriteLogAsync($"{DateTime.Now}> Error running script: {ex.Message}");
            }
        }
    }
}