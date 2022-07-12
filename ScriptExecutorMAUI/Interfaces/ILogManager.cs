using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.Interfaces
{
    public interface ILogManager
    {
        void AddLog(string text);
        Task<string> ReadLog();
    }
}
