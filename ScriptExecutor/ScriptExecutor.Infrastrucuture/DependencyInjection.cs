using Microsoft.Extensions.DependencyInjection;
using ScriptExecutor.Application.Interfaces;
using ScriptExecutor.Infrastrucuture.Persistence;
using ScriptExecutor.Infrastrucuture.Repositories;
using ScriptExecutor.Infrastrucuture.Services;

namespace ScriptExecutor.Infrastrucuture
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDataPersistence, DataPersistence>();
            services.AddScoped<IThreadSystem, ThreadSystem>();

            services.AddScoped<IJsonManager, JsonManager>();
            services.AddScoped<ILogManager, LogManager>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IScriptRunner, ScriptRunner>();
        }
    }
}
