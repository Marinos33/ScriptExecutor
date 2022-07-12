using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.Interfaces
{
    public interface ILogManager
    {

        /// <summary>
        /// add a log a the top of the file
        /// </summary>
        /// <param name="text">the content to add</param>
        void AddLog(string text);

        /// <summary>
        /// return the log in the file as a string
        /// </summary>
        /// <returns>a string</returns>
        Task<string> ReadLog();
    }
}
