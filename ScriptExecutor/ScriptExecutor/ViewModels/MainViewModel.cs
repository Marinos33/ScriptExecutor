using DynamicData;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using ScriptExecutor.Application;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScriptExecutor.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IProcessService _processService;
    private readonly IScriptRunner _scriptRunner;
    private readonly ILogManager _logManager;
    public ObservableCollection<ProcessViewModel> Processes { get; } = [];

    public MainViewModel(IProcessService processService, IScriptRunner scriptRunner, ILogManager logManager)
    {
        _processService = processService;
        _scriptRunner = scriptRunner;
        _logManager = logManager;

        ShowAddProcessDialog = new Interaction<EditProcessViewModel, ProcessViewModel?>();

        AddProcessCommand = ReactiveCommand.CreateFromTask(AddProcess);

        EditProcessCommand = ReactiveCommand.CreateFromTask<ProcessViewModel>(EditProcess);

        DeleteProcessCommand = ReactiveCommand.CreateFromTask<ProcessViewModel>(DeleteProcess);

        RefreshProcessesCommand = ReactiveCommand.Create(LoadProcesses);

        ExecuteScriptCommand = ReactiveCommand.CreateFromTask<ProcessViewModel>(ExecuteScript);

        ShowLogsCommand = ReactiveCommand.CreateFromTask(ShowLogs);

        RxApp.MainThreadScheduler.Schedule(LoadProcesses);
    }

    public ICommand AddProcessCommand { get; }
    public ICommand EditProcessCommand { get; }
    public ICommand DeleteProcessCommand { get; }
    public ICommand RefreshProcessesCommand { get; }
    public ICommand ExecuteScriptCommand { get; }
    public ICommand ShowLogsCommand { get; set; }
    public Interaction<EditProcessViewModel, ProcessViewModel?> ShowAddProcessDialog { get; }

    private void LoadProcesses()
    {
        Task.Run(async () =>
        {
            var processes = (await _processService.GetProcessesAsync())
            .Select(x => new ProcessViewModel(x))
            .ToList();

            RxApp.MainThreadScheduler.Schedule(() =>
            {
                Processes.Clear();
                Processes.AddRange(processes);
            });
        });
    }

    private async Task DeleteProcess(ProcessViewModel process)
    {
        int index = Processes.IndexOf(process);
        if (index >= 0)
        {
            await _processService.DeleteProcessAsync(index);
            Processes.RemoveAt(index);
        }
    }

    private async Task AddProcess()
    {
        var addProcessViewModel = new EditProcessViewModel
        {
            // Set the delegate that will be used by ExecuteScriptCommand
            ExecuteScriptHandler = async (script) =>
            {
                var isSuccessful = await _scriptRunner.RunScriptAsync(script, true);

                return isSuccessful;
            }
        };

        var result = await ShowAddProcessDialog.Handle(addProcessViewModel);

        if (result != null)
        {
            var process = new Process
            {
                Name = result.ProcessName,
                ExecutableFile = result.ExecutableFile,
                Script = result.Script,
                RunOnStart = result.RunOnStart,
                RunAfterShutdown = result.RunAfterShutdown
            };

            await _processService.AddProcessAsync(process);

            int insertIndex = 0;
            while (insertIndex < Processes.Count &&
                   string.Compare(Processes[insertIndex].ProcessName, result.ProcessName) < 0)
            {
                insertIndex++;
            }

            // Insert at the correct position alphabetically
            Processes.Insert(insertIndex, result);
        }
    }

    private async Task EditProcess(ProcessViewModel process)
    {
        var editProcessViewModel = new EditProcessViewModel
        {
            // Set the delegate that will be used by ExecuteScriptCommand
            ExecuteScriptHandler = async (script) =>
            {
                var isSuccessful = await _scriptRunner.RunScriptAsync(script, true);

                return isSuccessful;
            },
            Process = process
        };

        var result = await ShowAddProcessDialog.Handle(editProcessViewModel);

        if (result != null)
        {
            int index = Processes.IndexOf(process);

            var editedProcess = new Process
            {
                Name = result.ProcessName,
                ExecutableFile = result.ExecutableFile,
                Script = result.Script,
                RunOnStart = result.RunOnStart,
                RunAfterShutdown = result.RunAfterShutdown
            };

            await _processService.EditProcessAsync(editedProcess, index);

            LoadProcesses();
        }
    }

    public async Task ExecuteScript(ProcessViewModel process)
    {
        var script = process.Script;

        if (string.IsNullOrEmpty(script))
        {
            var scriptBox = MessageBoxManager
                .GetMessageBoxStandard("Error", "Script cannot be empty.",
                    ButtonEnum.Ok, Icon.Error);

            await scriptBox.ShowAsync();

            return;
        }

        var isSuccesful = await _scriptRunner.RunScriptAsync(script);

        var message = isSuccesful ?
            "Script executed successfully." :
            "Script execution failed.";

        var box = MessageBoxManager
            .GetMessageBoxStandard("Result", message,
                ButtonEnum.Ok, Icon.Info);

        await box.ShowAsync();
    }

    public async Task ShowLogs()
    {
        await Task.Run(
            () =>
            {
                _logManager.OpenLogs();
            }
        );
    }
}