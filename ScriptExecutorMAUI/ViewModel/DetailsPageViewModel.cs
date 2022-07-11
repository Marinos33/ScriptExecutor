using ScriptExecutorMAUI.DTOModel;

namespace ScriptExecutorMAUI.ViewModel;

[QueryProperty(nameof(Process), "Process")]
public partial class DetailsPageViewModel : ObservableObject
{
    [ObservableProperty]
    ProcessDto process;

    private readonly IDataManager _dataManager;
    private readonly IScriptRunner _scriptRunner;

    public DetailsPageViewModel(IDataManager dataManager, IScriptRunner scriptRunner)
    {
        _dataManager = dataManager;
        _scriptRunner = scriptRunner;
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

    [RelayCommand]
    public async Task TestScript()
    {
        await _scriptRunner.RunScript(process.Script);
    }
}

