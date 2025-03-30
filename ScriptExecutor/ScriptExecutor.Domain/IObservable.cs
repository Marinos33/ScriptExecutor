using System;

namespace ScriptExecutor.Domain
{
    public interface IObservable
    {
        /// <summary>
        /// the 'variable' which contain another event to fire when this one is invoked
        /// </summary>
        event EventHandler SomethingHappened;

        /// <summary>
        /// notify that the observable has changed
        /// </summary>
        void Update();
    }
}