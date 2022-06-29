using ScriptExecutorMAUI.DTOModel;
using ScriptExecutorMAUI.View;

namespace ScriptExecutorMAUI.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<ProcessDto> Processes { get; } = new();
        private readonly IDataManager _dataManager;

        public MainPageViewModel(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [RelayCommand]
        private async Task GetProcesses()
        {
            try
            {
                var processes = await _dataManager.ReadJson();
                processes = processes.OrderBy(x => x.Name).ToList();

                if (Processes.Count != 0)
                    Processes.Clear();

                foreach (var process in processes)
                {
                    var g = new ProcessDto
                    {
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
                await Shell.Current.DisplayAlert("Error! Could not read JSON data", e.Message, "OK");
                //TODO add to logs
            }
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
    }
}
