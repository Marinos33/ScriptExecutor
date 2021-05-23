using GameSaveBackup.Model;
using System.Collections.Generic;

namespace GameSaveBackup.Interfaces
{
    public interface ICSVManager
    {
        void WriteCsv();

        IEnumerable<Game> ReadCsv();
    }
}