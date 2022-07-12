namespace ScriptExecutorMAUI.Interfaces
{
    public interface IDataManager
    {
        /// <summary>
        /// add a process to the DB
        /// </summary>
        /// <param name="process">the process ot add</param>
        /// <returns>true if at least one row was affected, otherwise false</returns>
        Task<bool> AddProcess(Process process);

        /// <summary>
        /// get all process from the DB
        /// </summary>
        /// <returns>a list of processes</returns>
        Task<IEnumerable<Process>> GetAllProcess();

        /// <summary>
        /// Get the process with the executable filename given
        /// </summary>
        /// <param name="executableFile">the name of the executable</param>
        /// <returns>the process asked</returns>
        Task<Process> GetProcessByExecutableFileName(string executableFile);

        /// <summary>
        /// Get the process with the id in DB
        /// </summary>
        /// <param name="id">the id</param>
        /// <returns>the process asked</returns>
        Task<Process> GetProcessById(int id);

        /// <summary>
        /// Remove a process from the DB
        /// </summary>
        /// <param name="process">the process to remove</param>
        /// <returns>true if at least one row was affected, otherwise false</returns>
        Task<bool> RemoveProcess(Process process);

        /// <summary>
        /// Update a process in the DB
        /// </summary>
        /// <param name="process">the process to update</param>
        /// <returns>true if at least one row was affected, otherwise false</returns>
        Task<bool> UpdateProcess(Process process);
    }
}