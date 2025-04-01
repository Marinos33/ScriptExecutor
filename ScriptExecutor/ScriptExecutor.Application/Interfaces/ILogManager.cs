using System.Threading.Tasks;

namespace ScriptExecutor.Application.Interfaces
{
    public interface ILogManager
    {
        /// <summary>
        /// read the log file
        /// </summary>
        /// <returns>return a string with the content of the file</returns>
        Task<string> ReadLogAsync();

        /// <summary>
        /// add an entry to the log file
        /// </summary>
        /// <param name="text"></param>
        Task WriteLogAsync(string text);
    }
}