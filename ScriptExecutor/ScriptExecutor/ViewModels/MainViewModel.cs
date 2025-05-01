using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using ScriptExecutor.Domain.Model;
using ScriptExecutor.Application;
namespace ScriptExecutor.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IProcessService _processService;
    public ObservableCollection<Process> Processes { get; }

    public MainViewModel(IProcessService processService)
    {
        _processService = processService;
        Processes = new ObservableCollection<Process>(_processService.GetProcesses());
    }
}