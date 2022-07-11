using SQLite;
using System.Xml.Linq;

namespace ScriptExecutorMAUI.Services;

public class DataManager : IDataManager
{
    private const string DBNAME = "data";
    private SQLiteAsyncConnection conn;

    public DataManager()
{
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBNAME);
        conn = new SQLiteAsyncConnection(dbPath);
        conn.CreateTableAsync<Process>();
    }

    public async Task<IEnumerable<Process>> GetAllProcess()
    {
        return await conn.Table<Process>().ToListAsync();
    }

    public async Task<Process> GetProcess(int id)
    {
        return await conn.Table<Process>().FirstAsync(x => x.Id == id);
    }

    public async Task<bool> AddProcess(Process process)
    {
        var result = await conn.InsertAsync(process);
        if(result > 0)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> RemoveProcess(Process process)
    {
        var result = await conn.DeleteAsync(process);
        if (result > 0)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateProcess(Process process)
    {
        var result = await conn.UpdateAsync(process);
        if (result > 0)
        {
            return true;
        }
        return false;
    }

}

