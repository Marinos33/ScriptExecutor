using Quartz;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Collections.Generic;
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

        private static readonly HashSet<int> _processedGameInstances = new();
        private static readonly Dictionary<int, Process> _watchedProcesses = new();

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

                    // Use the process ID to uniquely identify this instance of the game
                    int processId = runningApp.Id;

                    // Check if we're already watching this process
                    if (!_watchedProcesses.ContainsKey(processId))
                    {
                        // Create a new Process object specifically for watching
                        Process watchProcess = Process.GetProcessById(processId);
                        watchProcess.EnableRaisingEvents = true;

                        // Set up event handler for both cases
                        watchProcess.Exited += async (sender, args) =>
                        {
                            _processedGameInstances.Remove(processId);

                            if (RunningGame?.RunAfterShutdown == true)
                            {
                                await RunScript().ConfigureAwait(false);
                                await _logManager.WriteLogAsync($"{DateTime.Now}> RunAfterShutdown script executed for {RunningGame.Name}");
                            }

                            // Clean up the watched process
                            _watchedProcesses.Remove(processId);

                            RunningGame = null;

                            await _logManager.WriteLogAsync($"{DateTime.Now}> Process exited: {processId}");
                        };

                        // Store the process for later cleanup
                        _watchedProcesses[processId] = watchProcess;

                        await _logManager.WriteLogAsync($"{DateTime.Now}> Started watching process: {processId}");
                    }

                    if (RunningGame.RunOnStart)
                    {
                        // Only run the script if we haven't processed this specific process instance
                        if (!_processedGameInstances.Contains(processId))
                        {
                            await RunScript().ConfigureAwait(false);

                            _processedGameInstances.Add(processId);

                            await _logManager.WriteLogAsync($"{DateTime.Now}> RunOnStart script executed for {RunningGame.Name} (PID: {processId})");
                        }
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