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

        public AddPageViewModel(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [RelayCommand]
        public async Task AddProcess()
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(executableFile))
            {
                await Shell.Current.DisplayAlert("form completion error", "either the name or the executable was not fullfilled", "ok" );
                return;
            }

            if(!runOnStart && !runAfterShutdown)
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
    }
}
