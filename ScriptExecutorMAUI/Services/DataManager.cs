namespace ScriptExecutorMAUI.Services
{
    public class DataManager : IDataManager
    {
        private string PATH = $"{FileSystem.Current.AppDataDirectory}\\Data.json";

        public async Task<IEnumerable<Process>> ReadJson()
        {
            if (File.Exists(PATH))
            {
                string json = await File.ReadAllTextAsync(PATH).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<IEnumerable<Process>>(json); //pass all record to the list
            }
            return Enumerable.Empty<Process>();
        }

        public async Task WriteJson(IEnumerable<Process> gameList)
        {
            var records = gameList;

            string json = JsonConvert.SerializeObject(records);
            await File.WriteAllTextAsync(PATH, json).ConfigureAwait(false);
        }
    }
}
