﻿using System;

namespace ScriptExecutor.Interfaces
{
    internal interface IObserver
    {
        void HandleEvent(object sender, EventArgs args); //what to do when the observable has changed
    }
}