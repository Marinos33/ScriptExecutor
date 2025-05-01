using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptExecutor.Domain.Model;

namespace ScriptExecutor.Application.Interfaces
{
    public interface IProcessRepository
    {
        Task AddProcessAsync(Process process);

        Task EditProcessAsync(Process process, int index);

        Task<List<Process>> GetProcessesAsync();

        Task RemoveProcessAsync(int index);
    }
}