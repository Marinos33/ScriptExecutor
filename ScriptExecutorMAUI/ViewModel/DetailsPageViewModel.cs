using ScriptExecutorMAUI.DTOModel;

namespace ScriptExecutorMAUI.ViewModel;

[QueryProperty(nameof(Process), "Process")]
public partial class DetailsPageViewModel : ObservableObject
{
    [ObservableProperty]
    ProcessDto process;
}

