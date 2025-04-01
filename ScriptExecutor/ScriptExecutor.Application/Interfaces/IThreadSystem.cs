using ScriptExecutor.Domain.Model;
using System;

namespace ScriptExecutor.Application.Interfaces
{
    public interface IThreadSystem
    {
        Game RunningGame { get; }

        /// <summary>
        /// Search if one of the processes in the list is running
        /// </summary>
        /// <param name="HandleEvent">the event to fire</param>
        void SearchProcess(EventHandler HandleEvent);
    }
}