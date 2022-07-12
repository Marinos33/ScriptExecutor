using AutoMapper;
using ScriptExecutorMAUI.DTOModel;

namespace ScriptExecutorMAUI.ViewModel;

[QueryProperty(nameof(Process), "Process")]
public partial class DetailsPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ProcessDto process;

    private readonly IDataManager _dataManager;
    private readonly IScriptRunner _scriptRunner;
    private readonly IMapper _mapper;

    public DetailsPageViewModel(IDataManager dataManager, IScriptRunner scriptRunner, IMapper mapper)
    {
        _dataManager = dataManager;
        _scriptRunner = scriptRunner;
        _mapper = mapper;
    }

    [RelayCommand]
    public async Task UpdateProcess()
    {
        if (string.IsNullOrEmpty(process.Name) || string.IsNullOrEmpty(process.ExecutableFile))
        {
            await Shell.Current.DisplayAlert("form completion error", "either the name or the executable was not fullfilled", "ok");
            return;
        }

        if (!process.RunOnStart && !process.RunAfterShutdown)
        {
            await Shell.Current.DisplayAlert("choose when to run", "either choose between run on start or after shutdown", "ok");
            return;
        }

        var isSucceed = await _dataManager.UpdateProcess(_mapper.Map<Process>(process));

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