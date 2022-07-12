using Microsoft.Maui.LifecycleEvents;
using ScriptExecutorMAUI.Services;
using ScriptExecutorMAUI.View;
using ScriptExecutorMAUI.ViewModel;
#if WINDOWS
using WinUIEx;
#endif

//TODO ajouter automapper
//TODO add comments
//TODO rajouter un dossier custom pour le stockage des logs et db si la version release ne le fait pas

//TODO mettre app en systemtray (feature missing for now)


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

#if WINDOWS
            //used to set the window size on startup
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        //Set size and center on screen using WinUIEx extension method
                        window.CenterOnScreen(662,950); 
                    });
                });
            });
#endif

        builder.Services.AddSingleton<IThreadsService, ThreadsService>();
		builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<DetailsPageViewModel>();
		builder.Services.AddTransient<DetailsPage>();
		builder.Services.AddTransient<AddPage>();
        builder.Services.AddTransient<AddPageViewModel>();
        builder.Services.AddTransient<LogsPage>();
        builder.Services.AddTransient<LogsPageViewModel>();

        builder.Services.AddScoped<IDataManager, DataManager>();
        builder.Services.AddScoped<IScriptRunner, ScriptRunner>();
        builder.Services.AddScoped<ILogManager, LogManager>();

        var serviceProcessingService = builder
            .Services
            .BuildServiceProvider()
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<IThreadsService>();

        serviceProcessingService.Start();

        return builder.Build();
	}
}
