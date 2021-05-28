using System.Threading.Tasks;

namespace ScriptExecutor.Interfaces
{
    public interface ILogManager
    {
        /// <summary>
        /// read the log file
        /// </summary>
        /// <returns>return a string with the content of the file</returns>
        Task<string> ReadLog();

        /// <summary>
        /// add an entry to the log file
        /// </summary>
        /// <param name="text"></param>
        void AddLog(string text);
    }
}