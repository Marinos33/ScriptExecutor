using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;

namespace ScriptExecutor.Application
{
    public interface IProcessService
    {
        /// <summary>
        /// add a process to the list in data
        /// </summary>
        /// <param name="process">the process to add</param>
        Task AddProcessAsync(Process process);

        /// <summary>
        /// edit the process in the list in data
        /// </summary>
        /// <param name="process">the edited info of the process</param>
        /// <param name="index">the index of the process to edit in the list</param>
        Task EditProcessAsync(Process process, int index);

        /// <summary>
        /// Remove a process from the list in data
        /// </summary>
        /// <param name="index">the index of the process in the list</param>
        Task DeleteProcessAsync(int index);

        List<Process> GetProcesses();
    }

    public class ProcessService : IProcessService
    {
        private readonly ILogManager _logManager;
        private readonly IProcessRepository _processRepository;

        public ProcessService(ILogManager logManager, IProcessRepository processRepository)
        {
            _logManager = logManager;
            _processRepository = processRepository;
        }

        public async Task AddProcessAsync(Process process)
        {
            await _processRepository.AddProcessAsync(process);

            await _logManager.WriteLogAsync(DateTime.Now.ToString() + " > the process : " + process.Name + " has been added");
        }

        public async Task EditProcessAsync(Process process, int index)
        {
            await _processRepository.EditProcessAsync(process, index);

            await _logManager.WriteLogAsync(DateTime.Now.ToString() + "> the process : " + process.Name + " has been modified");
        }

        public async Task DeleteProcessAsync(int index)
        {
            string oldProcess = _processRepository.GetProcesses()[index].Name;
            await _processRepository.RemoveProcessAsync(index);

            await _logManager.WriteLogAsync(DateTime.Now.ToString() + "> the process : " + oldProcess + " has been deleted");
        }

        public List<Process> GetProcesses()
        {
            return _processRepository.GetProcesses();
        }
    }
}