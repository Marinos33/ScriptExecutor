using System.Reactive;
using ReactiveUI;
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

        private ProcessViewModel _process;
        public ReactiveCommand<Unit, ProcessViewModel> AddProcessCommand { get; }

        public AddProcessViewModel()
        {
            AddProcessCommand = ReactiveCommand.Create(() =>
            {
                var process = new Process
                {
                    Name = ProcessName,
                    ExecutableFile = ExecutableFile,
                    Script = Script,
                    RunOnStart = RunOnStart,
                    RunAfterShutdown = RunAfterShutdown
                };

                Process = new ProcessViewModel(process);

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

        public ProcessViewModel Process
        {
            get => _process;
            set
            {
                this.RaiseAndSetIfChanged(ref _process, value);
                if (value != null)
                {
                    ProcessName = value.ProcessName;
                    ExecutableFile = value.ExecutableFile;
                    Script = value.Script;
                    RunOnStart = value.RunOnStart;
                    RunAfterShutdown = value.RunAfterShutdown;
                }
            }
        }
    }
}