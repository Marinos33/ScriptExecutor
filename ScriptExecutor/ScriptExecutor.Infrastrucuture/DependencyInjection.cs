using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Infrastrucuture.Jobs;
using ScriptExecutor.Infrastrucuture.Persistence;
using ScriptExecutor.Infrastrucuture.Repositories;
using ScriptExecutor.Infrastrucuture.Services;
using System;
using System.Threading.Tasks;

namespace ScriptExecutor.Infrastrucuture
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddDebug().AddConsole());

            services.AddSingleton<IDataPersistence, DataPersistence>();

            services.AddScoped<IJsonManager, JsonManager>();
            services.AddScoped<ILogManager, LogManager>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IScriptRunner, ScriptRunner>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.ConfigureOptions<ProcessusObserverJobSetup>();

            services.AddSingleton<IQuartzService, QuartzService>();
        }
    }
}