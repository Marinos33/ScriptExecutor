using System.Threading.Tasks;

namespace ScriptExecutor.Application.Interfaces
{
    public interface IScriptRunner
    {
        /// <summary>
        /// the method to run the script associated with process curretnly observed
        /// </summary>
        /// <param name="script">the script to run</param>
        Task<bool> RunScript(string script);
    }
}