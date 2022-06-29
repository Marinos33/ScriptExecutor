using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.Interfaces
{
    public interface IDataManager
    {
        /// <summary>
        /// rewrite the json with the given list
        /// </summary>
        /// <param name="processList">the list of processes registered</param>
        void WriteJson(IEnumerable<Process> processList);

        /// <summary>
        /// read the Json, convert all entry to Process object and retunr a list of items
        /// </summary>
        /// <returns>the list of Processes from the Json</returns>
        Task<IEnumerable<Process>> ReadJson();
    }
}
