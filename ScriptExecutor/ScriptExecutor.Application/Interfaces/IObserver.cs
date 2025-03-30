using System;

namespace ScriptExecutor.Application.Interfaces
{
    public interface IObserver
    {
        /// <summary>
        /// what to do when the observable has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void HandleEvent(object sender, EventArgs args);
    }
}