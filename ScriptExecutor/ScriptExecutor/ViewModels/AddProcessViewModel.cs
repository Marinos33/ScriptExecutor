using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ScriptExecutor.Application;
using ScriptExecutor.Domain.Model;

namespace ScriptExecutor.ViewModels
{
    public class AddProcessViewModel : ViewModelBase
    {
        private string _processName;
        private string _executableFile;
        private string _script;
        private bool _runOnStart;
        private bool _runAfterShutdown = true;

        private Process _process;
        public ReactiveCommand<Unit, Process> AddProcessCommand { get; }

        public AddProcessViewModel()
        {
            AddProcessCommand = ReactiveCommand.Create(() =>
            {
                Process = new Process
                {
                    Name = ProcessName,
                    ExecutableFile = ExecutableFile,
                    Script = Script,
                    RunOnStart = RunOnStart,
                    RunAfterShutdown = RunAfterShutdown
                };

                return Process;
            });
        }


        public string ProcessName
        {
            get => _processName;
            set => this.RaiseAndSetIfChanged(ref _processName, value);
        }

        public string ExecutableFile
        {
            get => _executableFile;
            set => this.RaiseAndSetIfChanged(ref _executableFile, value);
        }

        public string Script
        {
            get => _script;
            set => this.RaiseAndSetIfChanged(ref _script, value);
        }

        public bool RunOnStart
        {
            get => _runOnStart;
            set => this.RaiseAndSetIfChanged(ref _runOnStart, value);
        }

        public bool RunAfterShutdown
        {
            get => _runAfterShutdown;
            set => this.RaiseAndSetIfChanged(ref _runAfterShutdown, value);
        }

        public Process Process
        {
            get => _process;
            set
            {
                this.RaiseAndSetIfChanged(ref _process, value);
                if (value != null)
                {
                    ProcessName = value.Name;
                    ExecutableFile = value.ExecutableFile;
                    Script = value.Script;
                    RunOnStart = value.RunOnStart;
                    RunAfterShutdown = value.RunAfterShutdown;
                }
            }
        }
    }
}
