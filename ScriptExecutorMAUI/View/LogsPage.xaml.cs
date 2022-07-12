using ScriptExecutorMAUI.ViewModel;

namespace ScriptExecutorMAUI.View;

public partial class LogsPage : ContentPage
{
	public LogsPage(LogsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        var viewModel = (LogsPageViewModel)BindingContext;
        viewModel.ReadLogsCommand.Execute(null);
    }
}