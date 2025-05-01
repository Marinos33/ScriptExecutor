using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScriptExecutor.Domain.Model;
using ScriptExecutor.Infrastrucuture.Services;

namespace ScriptExecutor.Infrastrucuture.Persistence
{
    public interface IDataPersistence
    {
        List<Process> ProcessesList { get; set; }

        Task<List<Process>> LoadDataAsync();

        Task SaveDataAsync();
    }

    public class DataPersistence : IDataPersistence
    {
        public List<Process> ProcessesList { get; set; } = [];

        private readonly IJsonManager _jsonManager;

        public DataPersistence(IJsonManager jsonManager)
        {
            _jsonManager = jsonManager;
        }

        public async Task<List<Process>> LoadDataAsync()
        {
            var processes = await _jsonManager.ReadJsonAsync();

            ProcessesList = processes.ToList();

            return processes.ToList();
        }

        public async Task SaveDataAsync()
        {
            await _jsonManager.WriteJsonAsync(ProcessesList);
        }
    }
}