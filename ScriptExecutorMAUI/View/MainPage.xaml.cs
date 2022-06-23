using ScriptExecutorMAUI.ViewModel;

namespace ScriptExecutorMAUI;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		viewModel.GetGamesCommand.Execute(null);
	}
}

