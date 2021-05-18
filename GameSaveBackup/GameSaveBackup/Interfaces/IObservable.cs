using System;

namespace GameSaveBackup.Interfaces
{
    internal interface IObservable
    {
        event EventHandler SomethingHappened; //the 'variable' which contain another event to fire when this one is invoked

        void Update(); //notify that the observable has changed
    }
}