﻿using Microsoft.Maui.LifecycleEvents;
using ScriptExecutorMAUI.Services;
using ScriptExecutorMAUI.View;
using ScriptExecutorMAUI.ViewModel;
#if WINDOWS
using WinUIEx;
#endif

//TODO faire marcher les scripts
//TODO mettre app en systemtray
//TODO ajouter des logs dans un fichier texte pour les erreurs et les lancement de scripts
//TODO ajouter ecran pour lire les logs
//TODO ajouter popup alert pour le remplissage des form
//TODO ajouter theming
//TODO ajouter automapper

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

        builder.Services.AddSingleton<IDataManager, DataManager>();
        builder.Services.AddSingleton<IThreadsService, ThreadsService>();
		builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<DetailsPageViewModel>();
		builder.Services.AddTransient<DetailsPage>();
		builder.Services.AddTransient<AddPage>();
        builder.Services.AddTransient<AddPageViewModel>();


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
