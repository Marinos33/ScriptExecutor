using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScriptExecutor.Domain.Model;
using ScriptExecutor.ViewModels;

namespace ScriptExecutor.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(action =>
         action(ViewModel!.ShowAddProcessDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(IInteractionContext<AddProcessViewModel,
                                            Process?> interaction)
    {
        var dialog = new AddProcessWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<Process?>(this);

        interaction.SetOutput(result);
    }
}