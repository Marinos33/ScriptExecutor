namespace ScriptExecutorMAUI.Services;

public class LogManager : ILogManager
{
    private const string LOGFILENAME = "Logs.txt";

    public async Task<string> ReadLog()
    {
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), LOGFILENAME);
        if (File.Exists(filePath))
        {
            return await File.ReadAllTextAsync(filePath).ConfigureAwait(false);
        }
        return string.Empty;
    }

    public async void AddLog(string logText)
    {
        try
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), LOGFILENAME);

            string currentContent = await ReadLog();

            File.WriteAllText(filePath, logText + currentContent);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }
}



