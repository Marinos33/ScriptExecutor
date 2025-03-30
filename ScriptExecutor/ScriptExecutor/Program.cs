using Microsoft.Extensions.DependencyInjection;
using ScriptExecutor.UI;
using System;
using System.Windows.Forms;

namespace ScriptExecutor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            var formMain = serviceProvider.GetRequiredService<Form_Main>();
            Application.Run(formMain);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IData, Data>()
                    .AddSingleton<IThreadSystem, ThreadSystem>()
                    .AddScoped<IScriptRunner, ScriptRunner>()
                    .AddScoped<ILogManager, LogManager>()
                    .AddScoped<IJsonManager, JsonManager>()
                    .AddScoped<IForm_MainController, Form_MainController>()
                    .AddScoped<IForm_AddGameController, Form_AddGameController>();

            services.AddScoped<Form_Main>();
        }
    }
}