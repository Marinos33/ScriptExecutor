using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Microsoft.Extensions.DependencyInjection;
using ScriptExecutor.Application;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Infrastrucuture;
using ScriptExecutor.ViewModels;
using ScriptExecutor.Views;
using System;

namespace ScriptExecutor;

public partial class App : Avalonia.Application
{
    private Window? _mainWindow;
    private TrayIcon? _trayIcon;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        BindingPlugins.DataValidators.RemoveAt(0);

        var collection = new ServiceCollection();
        collection.AddInfrastructure();
        collection.AddApplication();

        collection.AddTransient<MainViewModel>();

        var services = collection.BuildServiceProvider();

        var vm = services.GetRequiredService<MainViewModel>();

        var quartzService = services.GetRequiredService<IQuartzService>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _mainWindow = new MainWindow
            {
                DataContext = vm
            };

            _mainWindow.Closing += (s, e) =>
            {
                ((Window)s).Hide();
                e.Cancel = true;
            };

            _trayIcon = new TrayIcon
            {
                Icon = new WindowIcon(AssetLoader.Open(new Uri("avares://ScriptExecutor/Assets/logo.ico"))),
                ToolTipText = "Tray Menu Example",
                Menu = []
            };

            var openItem = new NativeMenuItem("Open");
            openItem.Click += (_, _) => ShowMainWindow();

            var exitItem = new NativeMenuItem("Close");
            exitItem.Click += (_, _) => desktop.Shutdown();

            // Add to tray icon
            _trayIcon.Menu.Items.Add(openItem);
            _trayIcon.Menu.Items.Add(exitItem);

            // Show it
            _trayIcon.IsVisible = true;

            desktop.MainWindow = _mainWindow;
        }

        quartzService.StartAsync();

        base.OnFrameworkInitializationCompleted();
    }

    private void ShowMainWindow()
    {
        _mainWindow ??= new MainWindow();

        if (!_mainWindow.IsVisible)
        {
            _mainWindow.Show();
        }
        else
        {
            _mainWindow.Activate();
        }
    }
}