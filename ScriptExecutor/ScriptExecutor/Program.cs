using Microsoft.Extensions.DependencyInjection;
using ScriptExecutor.Application;
using ScriptExecutor.Infrastrucuture;
using ScriptExecutor.UI;
using System;
using System.Windows.Forms;
using App = System.Windows.Forms.Application;

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
            App.SetHighDpiMode(HighDpiMode.SystemAware);
            App.EnableVisualStyles();
            App.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            var formMain = serviceProvider.GetRequiredService<Form_Main>();
            App.Run(formMain);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddApplication();
        }
    }
}