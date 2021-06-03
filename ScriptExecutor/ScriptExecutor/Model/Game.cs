using ScriptExecutor.Interfaces;
using System;

namespace ScriptExecutor.Model
{
    public class Game : IObservable
    {
        public string Name { get; set; }
        public string ExecutableFile { get; set; }
        public string Script { get; set; }
        public bool Enabled { get; set; } = true;

        //from the interface
        public event EventHandler SomethingHappened; //the event which fire the handler event from the form_main

        public void Update() => SomethingHappened?.Invoke(this, EventArgs.Empty); //lambda, use Invoke if needed (invoke is used to to do something in the ui thread while in another thread)

        public Game DeepCopy()
        {
            return (Game)(new()
            {
                Name = Name,
                ExecutableFile = ExecutableFile,
                Script = Script,
                Enabled = Enabled
            });
        }
    }
}