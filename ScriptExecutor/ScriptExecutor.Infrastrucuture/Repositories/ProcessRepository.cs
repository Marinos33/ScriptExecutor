using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using ScriptExecutor.Infrastrucuture.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture.Repositories
{
    internal class ProcessRepository : IProcessRepository
    {
        private readonly IDataPersistence _dataPersistence;

        public ProcessRepository(IDataPersistence dataPersistence)
        {
            _dataPersistence = dataPersistence;
        }

        public async Task AddProcessAsync(Process process)
        {
            var count = _dataPersistence.ProcessesList.Count;

            _dataPersistence
                .ProcessesList
                .Add(process);

            await _dataPersistence.SaveDataAsync();

            //check if add worked
            if (_dataPersistence.ProcessesList.Count == count)
            {
                throw new System.Exception("Process was not added");
            }
        }

        public async Task RemoveProcessAsync(int index)
        {
            var count = _dataPersistence.ProcessesList.Count;

            _dataPersistence
                .ProcessesList
                .RemoveAt(index);

            await _dataPersistence.SaveDataAsync();

            //check if remove worked
            if (_dataPersistence.ProcessesList.Count == count)
            {
                throw new System.Exception("Process was not removed");
            }
        }

        public async Task EditProcessAsync(Process process, int index)
        {
            _dataPersistence
                .ProcessesList[index] = process;

            await _dataPersistence.SaveDataAsync();

            //check if edit worked
            if (!CheckIfProcessesAreEquals(process, _dataPersistence.ProcessesList[index]))
            {
                throw new System.Exception("Process was not edited");
            }
        }

        public List<Process> GetProcesses()
        {
            return _dataPersistence.ProcessesList.OrderBy(process => process.Name).ToList();
        }

        private bool CheckIfProcessesAreEquals(Process process1, Process process2)
        {
            return process1.Name == process2.Name &&
                   process1.ExecutableFile == process2.ExecutableFile &&
                   process1.Script == process2.Script &&
                   process1.RunOnStart == process2.RunOnStart &&
                   process1.RunAfterShutdown == process2.RunAfterShutdown;
        }
    }
}