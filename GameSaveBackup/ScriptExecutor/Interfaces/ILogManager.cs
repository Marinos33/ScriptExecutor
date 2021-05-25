using System.Threading.Tasks;

namespace ScriptExecutor.Interfaces
{
    public interface ILogManager
    {
        Task<string> ReadLog();

        void AddLog(string text);
    }
}