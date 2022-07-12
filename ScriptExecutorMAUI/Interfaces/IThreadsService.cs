namespace ScriptExecutorMAUI.Interfaces
{
    public interface IThreadsService
    {
        /// <summary>
        /// restart the system to look for opening/shutdonw of process
        /// </summary>
        /// <returns></returns>
        Task RestartThread();

        /// <summary>
        /// start the system to look for opening/shutdonw of process
        /// </summary>
        void Start();
    }
}