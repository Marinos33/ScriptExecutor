using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using ScriptExecutor.Domain.Model;
using ScriptExecutor.Application;
using System.Windows.Input;
using System.Reactive.Concurrency;
using ReactiveUI;
using DynamicData;
using System.Threading.Tasks;
using System.Reactive.Linq;
namespace ScriptExecutor.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IProcessService _processService;
    public ObservableCollection<Process> Processes { get; } = [];

    public MainViewModel(IProcessService processService)
    {
        _processService = processService;

        ShowAddProcessDialog = new Interaction<AddProcessViewModel, Process?>();

        AddProcessCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var addProcessViewModel = new AddProcessViewModel();

            var result = await ShowAddProcessDialog.Handle(addProcessViewModel);

            if (result != null)
            {
                await _processService.AddProcessAsync(result);
            }
        });

        RxApp.MainThreadScheduler.Schedule(LoadProcesses);
    }

    public ICommand AddProcessCommand { get; }
    public Interaction<AddProcessViewModel, Process?> ShowAddProcessDialog { get; }

    private async void LoadProcesses()
    {
        await Task.Run(() =>
        {
            var processes = _processService.GetProcesses();

            Processes.Clear();

            Processes.AddRange(processes);
        });
    }
}