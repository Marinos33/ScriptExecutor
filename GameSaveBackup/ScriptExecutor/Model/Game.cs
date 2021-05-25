using ScriptExecutor.Interfaces;
using System;

namespace ScriptExecutor.Model
{
    public class Game : IObservable, ICloneable
    {
        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public string ScriptPath { get; set; }
        public bool Enabled { get; set; } = true;

        //from the interface
        public event EventHandler SomethingHappened; //the event which fire the handler event from the form_main

        public void Update() => SomethingHappened?.Invoke(this, EventArgs.Empty); //lambda, use Invoke if needed (invoke is used to to do something in the ui thread while in another thread)

        //do a copy without reference (deep copy)
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}