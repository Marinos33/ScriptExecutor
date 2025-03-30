using ScriptExecutor.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScriptExecutor.Application.Interfaces
{
    public interface IJsonManager
    {
        /// <summary>
        /// completely rewrite the json
        /// </summary>
        void WriteJson();

        /// <summary>
        /// read the Json, convert all entry to Game object and retunr a list of items
        /// </summary>
        /// <returns>the list of Game from the Json</returns>
        Task<IEnumerable<Game>> ReadJson();
    }
}