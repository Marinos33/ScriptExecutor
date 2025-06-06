﻿using Avalonia.ReactiveUI;
using ReactiveUI;
using ScriptExecutor.ViewModels;
using System.Threading.Tasks;

namespace ScriptExecutor.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(action =>
         action(ViewModel!.ShowAddProcessDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(IInteractionContext<EditProcessViewModel,
                                            ProcessViewModel?> interaction)
    {
        var dialog = new EditProcessWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<ProcessViewModel?>(this);

        interaction.SetOutput(result);
    }
}