using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.Interfaces
{
    public interface IScriptRunner
    {
        /// <summary>
        /// Run the script given
        /// </summary>
        /// <param name="script">the script to run</param>
        /// <returns>true if succeed, otherwise false</returns>
        Task<bool> RunScript(string script);
    }
}
