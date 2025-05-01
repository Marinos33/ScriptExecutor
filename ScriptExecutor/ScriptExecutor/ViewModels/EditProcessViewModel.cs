using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using ReactiveUI;
using ScriptExecutor.Domain.Model;

namespace ScriptExecutor.ViewModels
{
    public class EditProcessViewModel : ViewModelBase
    {
        private string _processName;
        private string _executableFile;
        private string _script;
        private bool _runOnStart;
        private bool _runAfterShutdown = true;

        private ProcessViewModel _process;

        public delegate Task<bool> ExecuteScriptDelegate(string script);

        public ExecuteScriptDelegate ExecuteScriptHandler { get; set; }

        public ReactiveCommand<Unit, ProcessViewModel> SaveProcessCommand { get; }
        public ReactiveCommand<Unit, ProcessViewModel?> CancelCommand { get; }
        public ICommand ExecuteScriptCommand { get; }
        public ICommand BrowseCommand { get; }

        public EditProcessViewModel()
        {
            SaveProcessCommand = ReactiveCommand.Create(() =>
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

            ExecuteScriptCommand = ReactiveCommand.Create(async () =>
            {
                return await ExecuteScriptHandler(Script);
            });

            BrowseCommand = ReactiveCommand.Create(async () =>
            {
                var window = Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                ? desktop.MainWindow
                : null;

                if (window != null)
                {
                    var filePickerOptions = new FilePickerOpenOptions
                    {
                        Title = "Select Executable File",
                        AllowMultiple = false,
                        FileTypeFilter =
                        [
                            new FilePickerFileType("Executable Files")
                            {
                                Patterns = ["*.exe", "*.bat", "*.cmd", "*.ps1"]
                            },
                            new FilePickerFileType("All Files")
                            {
                                Patterns = ["*.*"]
                            }
                        ]
                    };

                    var files = await window.StorageProvider.OpenFilePickerAsync(filePickerOptions);
                    if (files.Count > 0)
                    {
                        ExecutableFile = files[0].Name;
                    }
                }
            });

            CancelCommand = ReactiveCommand.Create<Unit, ProcessViewModel?>((_) =>
            {
                // Return null to indicate that the edit was cancelled
                return null;
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