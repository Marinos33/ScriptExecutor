using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.Interfaces
{
    public interface IDataManager
    {
        bool AddProcess(Process process);
        IEnumerable<Process> GetAllProcess();
        Process GetProcess(int id);
        bool RemoveProcess(Process process);
        bool UpdateProcess(Process process);
    }
}
