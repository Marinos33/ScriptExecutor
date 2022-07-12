using SQLite;

namespace ScriptExecutorMAUI.Services;

public class DataManager : IDataManager
{
    private const string DBNAME = "data";
    private SQLiteAsyncConnection conn;
    private readonly ILogManager _logManager;

    public DataManager(ILogManager logManager)
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBNAME);
        conn = new SQLiteAsyncConnection(dbPath);
        conn.CreateTableAsync<Process>();
        _logManager = logManager;
    }

    public async Task<IEnumerable<Process>> GetAllProcess()
    {
        return await conn.Table<Process>().ToListAsync();
    }

    public async Task<Process> GetProcessById(int id)
    {
        return await conn.Table<Process>().FirstAsync(x => x.Id == id);
    }

    public async Task<Process> GetProcessByExecutableFileName(string executableFile)
    {
        return await conn.Table<Process>().FirstAsync(x => x.ExecutableFile == executableFile);
    }

    public async Task<bool> AddProcess(Process process)
    {
        var result = await conn.InsertAsync(process);
        if (result > 0)
        {
            _logManager.AddLog($"{DateTime.Now.ToString()}> the process : {process.Name} for the executable {process.ExecutableFile} has been added");
            return true;
        }
        return false;
    }

    public async Task<bool> RemoveProcess(Process process)
    {
        var result = await conn.DeleteAsync(process);
        if (result > 0)
        {
            _logManager.AddLog($"{DateTime.Now.ToString()}> the process : {process.Name} for the executable {process.ExecutableFile} has been removed");
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateProcess(Process process)
    {
        var result = await conn.UpdateAsync(process);
        if (result > 0)
        {
            _logManager.AddLog($"{DateTime.Now.ToString()}> the process : {process.Name} for the executable {process.ExecutableFile} has been updated");
            return true;
        }
        return false;
    }

}

