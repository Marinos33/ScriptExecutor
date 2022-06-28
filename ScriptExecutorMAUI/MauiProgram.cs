using Microsoft.Maui.LifecycleEvents;
using ScriptExecutorMAUI.Services;
using ScriptExecutorMAUI.ViewModel;

namespace ScriptExecutorMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    //Set size and center on screen using WinUIEx extension method
                    window.CenterOnScreen(1024, 768);
                });
            });
        });

        builder.Services.AddSingleton<IDataManager, DataManager>();
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
