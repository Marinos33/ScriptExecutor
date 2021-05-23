using ScriptExecutor.Model;
using System.Collections.Generic;

namespace ScriptExecutor.Interfaces
{
    public interface ICSVManager
    {
        void WriteCsv();

        IEnumerable<Game> ReadCsv();
    }
}