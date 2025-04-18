using System.Threading.Tasks;

namespace ScriptExecutor.Application.Interfaces
{
    public interface IQuartzService
    {
        Task StartAsync();

        Task StopAsync();
    }
}