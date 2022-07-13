namespace ScriptExecutorMAUI.ViewModel
{
    public partial class AddPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string executableFile;

        [ObservableProperty]
        private string script;

        [ObservableProperty]
        private bool runOnStart;

        [ObservableProperty]
        private bool runAfterShutdown = true;

        private readonly IDataManager _dataManager;
        private readonly IScriptRunner _scriptRunner;

        public AddPageViewModel(IDataManager dataManager, IScriptRunner scriptRunner)
        {
            _dataManager = dataManager;
            _scriptRunner = scriptRunner;
        }

        [RelayCommand]
        public async Task AddProcess()
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(executableFile))
            {
                await Shell.Current.DisplayAlert("form completion error", "either the name or the executable was not fullfilled", "ok");
                return;
            }

            if (!runOnStart && !runAfterShutdown)
            {
                await Shell.Current.DisplayAlert("choose when to run", "either choose between run on start or after shutdown", "ok");
                return;
            }

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
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        public async Task TestScript()
        {
            await _scriptRunner.RunScript(script);
        }
    }
}