using Microsoft.Extensions.DependencyInjection;

namespace ScriptExecutor.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
        }
    }
}