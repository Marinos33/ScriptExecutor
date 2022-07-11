using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.Interfaces
{
    public interface IDataManager
    {
        Task<bool> AddProcess(Process process);
        Task<IEnumerable<Process>> GetAllProcess();
        Task<Process> GetProcessByExecutableFileName(string executableFile);
        Task<Process> GetProcessById(int id);
        Task<bool> RemoveProcess(Process process);
        Task<bool> UpdateProcess(Process process);
    }
}
