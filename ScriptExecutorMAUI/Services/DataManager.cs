using SQLite;

namespace ScriptExecutorMAUI.Services;

public class DataManager : IDataManager
{
    private const string DBNAME = "data";
    private SQLiteConnection conn;

    public DataManager()
    {
        conn = new SQLiteConnection(DBNAME);
        conn.CreateTable<Process>();
    }

    public IEnumerable<Process> GetAllProcess()
    {
        return conn.Table<Process>().ToList();
    }

    public Process GetProcess(int id)
    {
        return conn.Table<Process>().First(x => x.Id == id);
    }

    public bool AddProcess(Process process)
    {
        var result = conn.Insert(process);
        if(result > 0)
        {
            return true;
        }
        return false;
    }

    public bool RemoveProcess(Process process)
    {
        var result = conn.Delete(process);
        if (result > 0)
        {
            return true;
        }
        return false;
    }

    public bool UpdateProcess(Process process)
    {
        var result = conn.Update(process);
        if (result > 0)
        {
            return true;
        }
        return false;
    }

}

