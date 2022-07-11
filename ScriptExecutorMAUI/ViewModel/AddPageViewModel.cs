using ScriptExecutorMAUI.View;

namespace ScriptExecutorMAUI.ViewModel
{
    public partial class AddPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string executableFile;
        [ObservableProperty]
        string script;
        [ObservableProperty]
        bool runOnStart;
        [ObservableProperty]
        bool runAfterShutdown = true;

        private readonly IDataManager _dataManager;
        private readonly IThreadsService _threadsService;

        public AddPageViewModel(IDataManager dataManager, IThreadsService threadsService)
        {
            _dataManager = dataManager;
            _threadsService = threadsService;
        }

        [RelayCommand]
        public async Task AddProcess()
        {
            var isSucceed = await _dataManager.AddProcess(new Process
            {
                Name = name,
                Script = script,
                ExecutableFile = executableFile,
                RunOnStart = runOnStart,
                RunAfterShutdown = runAfterShutdown
            });

            if (isSucceed)
            {
                await _threadsService.RestartThread();
                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
