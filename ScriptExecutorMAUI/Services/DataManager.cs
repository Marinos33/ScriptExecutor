namespace ScriptExecutorMAUI.Services
{
    public class DataManager : IDataManager
    {
        private const string PATH = "Data.json";

        public async Task<IEnumerable<Process>> ReadJson()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(PATH);
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Process>>(contents); //pass all record to the list
        }

        public async void WriteJson(IEnumerable<Process> gameList)
        {
            var records = gameList;

            string json = JsonConvert.SerializeObject(records);
            await File.WriteAllTextAsync(PATH, json).ConfigureAwait(false);
        }
    }
}
