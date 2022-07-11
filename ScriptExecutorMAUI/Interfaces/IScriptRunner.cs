using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.Interfaces
{
    public interface IScriptRunner
    {
        Task<bool> RunScript(string script);
    }
}
