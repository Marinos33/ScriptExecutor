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

        // Initialize commands
        EditProcessCommand = new CustomAsyncRelayCommand<Process>(EditProcessHandler);
        DeleteProcessCommand = new CustomAsyncRelayCommand<Process>(DeleteProcessHandler);

        // Load processes when model is initialized
        _ = LoadProcessesAsync();
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    // State to hold all processes
    public IState<List<Process>> Processes => State<List<Process>>.Value(this, () => new List<Process>());

    // State to track currently selected process index (for edit/delete operations)
    public IState<int> SelectedProcessIndex => State<int>.Value(this, () => -1);

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
            // Get the current list directly from the service rather than state
            var processList = _processService.GetProcesses();
            int index = processList.IndexOf(process);
            if (index >= 0)
            {
                // Update the index state
                await SelectedProcessIndex.UpdateAsync(_ => index);

                // Call the service
                await _processService.EditProcessAsync(process, index);

                // Refresh the list
                await LoadProcessesAsync();
            }
        }
    }

    public async Task DeleteProcessHandler(Process process)
    {
        if (process != null)
        {
            // Get the current list directly from the service rather than state
            var processList = _processService.GetProcesses();
            int index = processList.IndexOf(process);
            if (index >= 0)
            {
                // Call the service
                await _processService.DeleteProcessAsync(index);

                // Refresh the list
                await LoadProcessesAsync();
            }
        }
    }
}

public class CustomAsyncRelayCommand<T> : ICommand
{
    private readonly Func<T, Task> _execute;
    private bool _isExecuting;

    public CustomAsyncRelayCommand(Func<T, Task> execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => !_isExecuting;

    public async void Execute(object parameter)
    {
        if (_isExecuting || parameter == null)
            return;

        try
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();
            await _execute((T)parameter);
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    protected void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
