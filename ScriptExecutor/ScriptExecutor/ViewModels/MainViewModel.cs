using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using ScriptExecutor.Application;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Domain.Model;

namespace ScriptExecutor.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IProcessService _processService;
    private readonly IScriptRunner _scriptRunner;
    public ObservableCollection<ProcessViewModel> Processes { get; } = [];

    public MainViewModel(IProcessService processService, IScriptRunner scriptRunner)
    {
        _processService = processService;
        _scriptRunner = scriptRunner;

        ShowAddProcessDialog = new Interaction<AddProcessViewModel, ProcessViewModel?>();

        AddProcessCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var addProcessViewModel = new AddProcessViewModel
            {
                // Set the delegate that will be used by ExecuteScriptCommand
                ExecuteScriptHandler = async (script) =>
                {
                    var isSuccessful = await _scriptRunner.RunScriptAsync(script);

                    return isSuccessful;
                }
            };

            var result = await ShowAddProcessDialog.Handle(addProcessViewModel);

            if (result != null)
            {
                Processes.Add(result);

                var process = new Process
                {
                    Name = result.ProcessName,
                    ExecutableFile = result.ExecutableFile,
                    Script = result.Script,
                    RunOnStart = result.RunOnStart,
                    RunAfterShutdown = result.RunAfterShutdown
                };

                await _processService.AddProcessAsync(process);
            }
        });

        RefreshProcesses = ReactiveCommand.Create(LoadProcesses);

        RxApp.MainThreadScheduler.Schedule(LoadProcesses);
    }

    public ICommand AddProcessCommand { get; }
    public ICommand RefreshProcesses { get; }
    public Interaction<AddProcessViewModel, ProcessViewModel?> ShowAddProcessDialog { get; }

    private void LoadProcesses()
    {
        Task.Run(async () =>
        {
            var processes = (await _processService.GetProcessesAsync()).Select(x => new ProcessViewModel(x));

            Processes.Clear();

            Processes.AddRange(processes);
        });
    }
}