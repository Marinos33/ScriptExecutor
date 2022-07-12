using ScriptExecutorMAUI.DTOModel;
using ScriptExecutorMAUI.View;

namespace ScriptExecutorMAUI.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<ProcessDto> Processes { get; } = new();
        private readonly IDataManager _dataManager;
        private readonly IThreadsService _threadsService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MainPageViewModel(IDataManager dataManager, IThreadsService threadsService, IServiceScopeFactory serviceScopeFactory)
        {
            _dataManager = dataManager;
            _threadsService = threadsService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [RelayCommand]
        private async Task GetProcesses()
        {
            try
            {
                var processes = await _dataManager.GetAllProcess();
                processes = processes.OrderBy(x => x.Name).ToList();

                if (Processes.Count != 0)
                    Processes.Clear();

                foreach (var process in processes)
                {
                    var g = new ProcessDto
                    {
                        Id = process.Id,
                        Name = process.Name,
                        Script = process.Script,
                        ExecutableFile = process.ExecutableFile,
                        RunOnStart = process.RunOnStart,
                        RunAfterShutdown = process.RunAfterShutdown,
                        ImagePath = !string.IsNullOrEmpty(process.ExecutableFile) && !string.IsNullOrEmpty(process.Name) ? "check.png" : "error.png"
                    };

                    Processes.Add(g);
                }

            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error! Could not get process from db", e.Message, "Such sadness");
            }
        }

        [RelayCommand]
        public async Task RemoveProcess(ProcessDto process)
        {
            Processes.Remove(process);

            await _dataManager.RemoveProcess(new Process
            {
                Id = process.Id,
                Name = process.Name,
                Script = process.Script,
                ExecutableFile = process.ExecutableFile,
                RunOnStart = process.RunOnStart,
                RunAfterShutdown = process.RunAfterShutdown
            });

            await _threadsService.RestartThread();
        }

        [RelayCommand]
        public async Task GoToDetails(ProcessDto process)
        {
            if (process == null)
                return;

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
            {
                {"Process", process }
            });
        }

        [RelayCommand]
        public async Task GoToNew()
        {
            await Shell.Current.GoToAsync(nameof(AddPage));
        }

        [RelayCommand]
        public void GoToLogs()
        {
            using var serviceScope = _serviceScopeFactory.CreateScope();
            var page = serviceScope.ServiceProvider.GetService<LogsPage>();

            var logsWindow = new Window
            {
                Page = page
            };

            Application.Current.OpenWindow(logsWindow);
        }
    }
}
