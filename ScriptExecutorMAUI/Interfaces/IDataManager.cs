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
        /// <param name="gameList">the list of games registered</param>
        void WriteJson(IEnumerable<Game> gameList);

        /// <summary>
        /// read the Json, convert all entry to Game object and retunr a list of items
        /// </summary>
        /// <returns>the list of Games from the Json</returns>
        Task<IEnumerable<Game>> ReadJson();
    }
}
