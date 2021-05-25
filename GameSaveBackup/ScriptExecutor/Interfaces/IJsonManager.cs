using ScriptExecutor.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScriptExecutor.Interfaces
{
    public interface IJsonManager
    {
        void WriteJson();

        Task<IEnumerable<Game>> ReadJson();
    }
}