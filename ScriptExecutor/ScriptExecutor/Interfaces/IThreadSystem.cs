using System;

namespace ScriptExecutor.Interfaces
{
    public interface IThreadSystem
    {
        /// <summary>
        /// Search if one of the processes in the list is running
        /// </summary>
        /// <param name="HandleEvent">the event to fire</param>
        void SearchProcess(EventHandler HandleEvent);
    }
}