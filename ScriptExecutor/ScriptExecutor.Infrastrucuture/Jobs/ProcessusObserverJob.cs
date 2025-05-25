using Quartz;
using ScriptExecutor.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Process = System.Diagnostics.Process;
using ProcessEntity = ScriptExecutor.Domain.Model.Process;

namespace ScriptExecutor.Infrastrucuture.Jobs
{
    [DisallowConcurrentExecution]
    internal class ProcessusObserverJob : IJob
    {
        private readonly IProcessRepository _processRepository;
        private readonly IScriptRunner _scriptRunner;
        private readonly ILogManager _logManager;

        private static readonly Dictionary<int, ProcessEntity> _runningProcesses = [];
        private static readonly HashSet<int> _processedProcessInstances = [];
        private static readonly Dictionary<int, Process> _watchedProcesses = [];

        // Lock object for thread safety when updating static collections
        private static readonly Lock _lockObject = new();

        public ProcessusObserverJob(IProcessRepository processRepository, IScriptRunner scriptRunner, ILogManager logManager)
        {
            _processRepository = processRepository;
            _scriptRunner = scriptRunner;
            _logManager = logManager;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var processesList = (await _processRepository.GetProcessesAsync()).Where(
                    p => !string.IsNullOrEmpty(p.Name)
                    && !string.IsNullOrEmpty(p.ExecutableFile)
                    && !string.IsNullOrEmpty(p.Script)
                    && (p.RunOnStart || p.RunAfterShutdown))
                    .ToList();

                if (processesList.Count <= 0)
                {
                    return;
                }

                // Check for all running processes
                foreach (var item in processesList)
                {
                    var processName = Path.GetFileNameWithoutExtension(item.ExecutableFile);
                    Process[]? processes = null;

                    try
                    {
                        processes = Process.GetProcessesByName(processName);

                        if (processes.Length > 0)
                        {
                            // Process each running instance of the process
                            foreach (var process in processes)
                            {
                                await HandleProcessing(item, process).ConfigureAwait(false);
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

                // Check for processes that no longer exist and clean up
                CleanupNonExistentProcesses();
            }
            catch (Exception ex)
            {
                await _logManager.WriteLogAsync($"{DateTime.Now}> Error in ProcessusObserverJob: {ex.Message}");
            }
        }

        private async Task HandleProcessing(ProcessEntity entity, Process process)
        {
            Process? processToProcess = null;
            try
            {
                int processId = process.Id;

                lock (_lockObject)
                {
                    // If we're already tracking this process, nothing more to do
                    if (_runningProcesses.ContainsKey(processId))
                    {
                        return;
                    }

                    _runningProcesses[processId] = entity.DeepCopy();
                }

                await _logManager.WriteLogAsync($"{DateTime.Now}> Detected running process: {entity.Name} (PID: {processId})");

                // Get a fresh process handle to use for monitoring
                processToProcess = Process.GetProcessById(processId);

                lock (_lockObject)
                {
                    // Set up process monitoring if not already watching
                    if (!_watchedProcesses.ContainsKey(processId))
                    {
                        processToProcess.EnableRaisingEvents = true;

                        processToProcess.Exited += async (sender, args) =>
                        {
                            ProcessEntity? exitedProcess = null;

                            lock (_lockObject)
                            {
                                _processedProcessInstances.Remove(processId);

                                if (_runningProcesses.TryGetValue(processId, out exitedProcess))
                                {
                                    _runningProcesses.Remove(processId);
                                }

                                _watchedProcesses.Remove(processId);
                            }

                            if (exitedProcess?.RunAfterShutdown == true)
                            {
                                await RunScriptAsync(exitedProcess).ConfigureAwait(false);
                                await _logManager.WriteLogAsync($"{DateTime.Now}> RunAfterShutdown script executed for {exitedProcess.Name} (PID: {processId})");
                            }

                            await _logManager.WriteLogAsync($"{DateTime.Now}> Process exited: {processId}");
                        };

                        _watchedProcesses[processId] = processToProcess;
                    }
                }

                // Move the logging outside of the lock
                await _logManager.WriteLogAsync($"{DateTime.Now}> Started watching process: {processId}");

                // Handle RunOnStart script execution
                lock (_lockObject)
                {
                    if (entity.RunOnStart && !_processedProcessInstances.Contains(processId))
                    {
                        Task.Run(async () =>
                        {
                            await RunScriptAsync(entity).ConfigureAwait(false);

                            lock (_lockObject)
                            {
                                _processedProcessInstances.Add(processId);
                            }

                            await _logManager.WriteLogAsync($"{DateTime.Now}> RunOnStart script executed for {entity.Name} (PID: {processId})");
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await _logManager.WriteLogAsync($"{DateTime.Now}> Error handling process process: {ex.Message}");
            }
        }

        private static void CleanupNonExistentProcesses()
        {
            List<int> processesToRemove = [];

            lock (_lockObject)
            {
                foreach (var pid in _runningProcesses.Keys)
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
                    _runningProcesses.Remove(pid);
                    _processedProcessInstances.Remove(pid);

                    if (_watchedProcesses.TryGetValue(pid, out var process))
                    {
                        process.Dispose();
                        _watchedProcesses.Remove(pid);
                    }
                }
            }
        }

        private async Task RunScriptAsync(ProcessEntity process)
        {
            try
            {
                if (string.IsNullOrEmpty(process.Script))
                {
                    await _logManager.WriteLogAsync($"{DateTime.Now}> No script to run for {process.ExecutableFile}");
                    return;
                }

                bool isScriptExecuted = await _scriptRunner.RunScriptAsync(process.Script).ConfigureAwait(false);

                if (isScriptExecuted)
                {
                    await _logManager.WriteLogAsync($"{DateTime.Now}> Script for {process.ExecutableFile} has been launched");
                }
                else
                {
                    await _logManager.WriteLogAsync($"{DateTime.Now}> Unable to run the script for {process.ExecutableFile}");
                }
            }
            catch (Exception ex)
            {
                await _logManager.WriteLogAsync($"{DateTime.Now}> Error running script: {ex.Message}");
            }
        }
    }
}