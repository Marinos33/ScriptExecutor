using ScriptExecutorMAUI.DTOModel;

namespace ScriptExecutorMAUI.ViewModel;

[QueryProperty(nameof(Game), "Game")]
public partial class DetailsPageViewModel : ObservableObject
{
    [ObservableProperty]
    GameDto game;
}

