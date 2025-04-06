using Quartz;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture.Jobs
{
    [DisallowConcurrentExecution]
    internal class ProcessusObserverJob : IJob
    {
        private readonly IGameRepository _gameRepository;
        private readonly IScriptRunner _scriptRunner;
        private readonly ILogManager _logManager;

        private static readonly Dictionary<int, Game> _runningGames = new();
        private static readonly HashSet<int> _processedGameInstances = new();
        private static readonly Dictionary<int, Process> _watchedProcesses = new();

        // Lock object for thread safety when updating static collections
        private static readonly object _lockObject = new();

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

                // Check for all running games
                foreach (var game in gamesList)
                {
                    var processName = Path.GetFileNameWithoutExtension(game.ExecutableFile);
                    Process[] processes = null;

                    try
                    {
                        processes = Process.GetProcessesByName(processName);
                        if (processes.Length > 0)
                        {
                            // Process each running instance of the game
                            foreach (var process in processes)
                            {
                                await HandleGameProcess(game, process).ConfigureAwait(false);
                            }
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

                // Check for games that no longer exist and clean up
                CleanupNonExistentProcesses();
            }
            catch (Exception ex)
            {
                await _logManager.WriteLogAsync($"{DateTime.Now}> Error in ProcessusObserverJob: {ex.Message}");
            }
        }

        private async Task HandleGameProcess(Game game, Process process)
        {
            Process gameProcess = null;
            try
            {
                int processId = process.Id;

                lock (_lockObject)
                {
                    // If we're already tracking this process, nothing more to do
                    if (_runningGames.ContainsKey(processId))
                    {
                        return;
                    }

                    _runningGames[processId] = game.DeepCopy();
                }

                await _logManager.WriteLogAsync($"{DateTime.Now}> Detected running game: {game.Name} (PID: {processId})");

                // Get a fresh process handle to use for monitoring
                gameProcess = Process.GetProcessById(processId);

                lock (_lockObject)
                {
                    // Set up process monitoring if not already watching
                    if (!_watchedProcesses.ContainsKey(processId))
                    {
                        gameProcess.EnableRaisingEvents = true;

                        gameProcess.Exited += async (sender, args) =>
                        {
                            Game exitedGame = null;

                            lock (_lockObject)
                            {
                                _processedGameInstances.Remove(processId);

                                if (_runningGames.TryGetValue(processId, out exitedGame))
                                {
                                    _runningGames.Remove(processId);
                                }

                                _watchedProcesses.Remove(processId);
                            }

                            if (exitedGame?.RunAfterShutdown == true)
                            {
                                await RunScriptAsync(exitedGame).ConfigureAwait(false);
                                await _logManager.WriteLogAsync($"{DateTime.Now}> RunAfterShutdown script executed for {exitedGame.Name} (PID: {processId})");
                            }

                            await _logManager.WriteLogAsync($"{DateTime.Now}> Process exited: {processId}");
                        };

                        _watchedProcesses[processId] = gameProcess;
                    }
                }

                // Move the logging outside of the lock
                await _logManager.WriteLogAsync($"{DateTime.Now}> Started watching process: {processId}");

                // Handle RunOnStart script execution
                lock (_lockObject)
                {
                    if (game.RunOnStart && !_processedGameInstances.Contains(processId))
                    {
                        Task.Run(async () =>
                        {
                            await RunScriptAsync(game).ConfigureAwait(false);

                            lock (_lockObject)
                            {
                                _processedGameInstances.Add(processId);
                            }

                            await _logManager.WriteLogAsync($"{DateTime.Now}> RunOnStart script executed for {game.Name} (PID: {processId})");
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await _logManager.WriteLogAsync($"{DateTime.Now}> Error handling game process: {ex.Message}");
            }
        }

        private void CleanupNonExistentProcesses()
        {
            List<int> processesToRemove = new();

            lock (_lockObject)
            {
                foreach (var pid in _runningGames.Keys)
                {
                    try
                    {
                        // Check if the process still exists
                        Process.GetProcessById(pid);
                    }
                    catch (ArgumentException)
                    {
                        // Process no longer exists, mark for removal
                        processesToRemove.Add(pid);
                    }
                }

                // Remove dead processes from tracking collections
                foreach (var pid in processesToRemove)
                {
                    _runningGames.Remove(pid);
                    _processedGameInstances.Remove(pid);

                    if (_watchedProcesses.TryGetValue(pid, out var process))
                    {
                        process.Dispose();
                        _watchedProcesses.Remove(pid);
                    }
                }
            }
        }

        private async Task RunScriptAsync(Game game)
        {
            try
            {
                bool isScriptExecuted = await _scriptRunner.RunScriptAsync(game.Script).ConfigureAwait(false);

                if (isScriptExecuted)
                {
                    await _logManager.WriteLogAsync($"{DateTime.Now}> Script for {game.ExecutableFile} has been launched");
                }
                else
                {
                    await _logManager.WriteLogAsync($"{DateTime.Now}> Unable to run the script for {game.ExecutableFile}");
                }
            }
            catch (Exception ex)
            {
                await _logManager.WriteLogAsync($"{DateTime.Now}> Error running script: {ex.Message}");
            }
        }
    }
}