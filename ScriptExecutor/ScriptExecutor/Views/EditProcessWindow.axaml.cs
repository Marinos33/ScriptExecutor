using System;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScriptExecutor.ViewModels;

namespace ScriptExecutor.Views;

public partial class EditProcessWindow : ReactiveWindow<EditProcessViewModel>
{
    public EditProcessWindow()
    {
        InitializeComponent();

        // This line is needed to make the previewer happy (the previewer plugin cannot handle the following line).
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => action(ViewModel!.SaveProcessCommand.Subscribe(Close)));
        this.WhenActivated(action => action(ViewModel!.CancelCommand.Subscribe(Close)));
    }
}