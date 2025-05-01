using System;
using System.Threading.Tasks;
using Quartz;
using ScriptExecutor.Application.Interfaces;

namespace ScriptExecutor.Infrastrucuture.Services
{
    internal class QuartzService : IQuartzService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        public QuartzService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task StartAsync()
        {
            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Start();
            System.Diagnostics.Debug.WriteLine($"Quartz scheduler started at {DateTime.Now}");
        }

        public async Task StopAsync()
        {
            if (_scheduler != null && !_scheduler.IsShutdown)
            {
                await _scheduler.Shutdown(true);
                System.Diagnostics.Debug.WriteLine($"Quartz scheduler stopped at {DateTime.Now}");
            }
        }
    }
}