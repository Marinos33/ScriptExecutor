using ScriptExecutorMAUI.DTOModel;

namespace ScriptExecutorMAUI.ViewModel;

[QueryProperty(nameof(Process), "Process")]
public partial class DetailsPageViewModel : ObservableObject
{
    [ObservableProperty]
    ProcessDto process;

    private readonly IDataManager _dataManager;

    public DetailsPageViewModel(IDataManager dataManager)
    {
        _dataManager = dataManager;
    }

    [RelayCommand]
    public async Task UpdateProcess()
    {
        var isSucceed = await _dataManager.UpdateProcess(new Process
        {
            Id = process.Id,
            Name = process.Name,
            Script = process.Script,
            ExecutableFile = process.ExecutableFile,
            RunOnStart = process.RunOnStart,
            RunAfterShutdown = process.RunAfterShutdown,
        });

        if (isSucceed)
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}

