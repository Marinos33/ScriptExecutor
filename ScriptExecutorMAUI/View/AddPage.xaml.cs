using ScriptExecutorMAUI.ViewModel;

namespace ScriptExecutorMAUI.View;

public partial class AddPage : ContentPage
{
    public AddPage(AddPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}