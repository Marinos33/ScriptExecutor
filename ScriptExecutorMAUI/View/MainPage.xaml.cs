using ScriptExecutorMAUI.ViewModel;

namespace ScriptExecutorMAUI.View;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.GetProcessesCommand.Execute(null);
    }

    protected override async void OnAppearing()
    {
        var viewModel = (MainPageViewModel)BindingContext;
        viewModel.GetProcessesCommand.Execute(null);
    }
}

