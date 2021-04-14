using System;

namespace GameSaveBackup
{
    internal interface IObserver
    {
        void HandleEvent(object sender, EventArgs args); //what to do when the observable has changed
    }
}