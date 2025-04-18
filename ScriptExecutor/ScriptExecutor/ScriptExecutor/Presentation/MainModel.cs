namespace ScriptExecutor.Presentation;

public partial record MainModel
{
    private readonly INavigator _navigator;
    private readonly IProcessService _processService;

    public MainModel(
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        IProcessService processService)
    {
        _navigator = navigator;
        _processService = processService;
        Title = "Script Executor";
        Title += $" - {appInfo?.Value?.Environment}";

        // Load processes when model is initialized
        _ = LoadProcessesAsync();
    }

    public string? Title { get; }

    // State to hold all processes
    public IState<List<Process>> Processes => State<List<Process>>.Value(this, () => []);

    // Command properties
    public ICommand EditProcessCommand { get; }
    public ICommand DeleteProcessCommand { get; }

    // Async method to load processes from repository
    private async Task LoadProcessesAsync()
    {
        // Get processes from service
        var processList = _processService.GetProcesses();

        // Update the Processes state
        await Processes.UpdateAsync(_ => processList);
    }

    // Command handlers
    public async Task EditProcessHandler(Process process)
    {
        if (process != null)
        {
            throw new NotImplementedException("EditProcessHandler is not implemented yet.");
        }
    }

    public async Task DeleteProcessHandler(Process process)
    {
        if (process != null)
        {
            throw new NotImplementedException("DeleteProcessHandler is not implemented yet.");
        }
    }
}
