using ScriptExecutorMAUI.ViewModel;

namespace ScriptExecutorMAUI.View;

public partial class MainPage : ContentPage
{
    private readonly IThreadsService _threadsService;
    public MainPage(MainPageViewModel viewModel, IThreadsService threadsService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _threadsService = threadsService;
    }

    protected override async void OnAppearing()
    {
        var viewModel = (MainPageViewModel)BindingContext;
        viewModel.GetProcessesCommand.Execute(null);


        await _threadsService.RestartThread();
    }
}

