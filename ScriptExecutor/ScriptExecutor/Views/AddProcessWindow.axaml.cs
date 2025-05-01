using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScriptExecutor;
using ScriptExecutor.ViewModels;

namespace ScriptExecutor.Views;

public partial class AddProcessWindow : ReactiveWindow<AddProcessViewModel>
{
    public AddProcessWindow()
    {
        InitializeComponent();

        // This line is needed to make the previewer happy (the previewer plugin cannot handle the following line).
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => action(ViewModel!.AddProcessCommand.Subscribe(Close)));
    }
}
