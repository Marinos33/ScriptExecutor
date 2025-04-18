using Microsoft.Extensions.Options;
using Quartz;
using System;

namespace ScriptExecutor.Infrastrucuture.Jobs
{
    internal class ProcessusObserverJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(ProcessusObserverJob));

            options
                .AddJob<ProcessusObserverJob>(jobBuilder => jobBuilder.WithIdentity(jobKey));

            options.AddTrigger(triggerBuilder => triggerBuilder
                .WithIdentity($"{jobKey.Name}.trigger")
                .ForJob(jobKey)
                .WithSchedule(SimpleScheduleBuilder
                    .Create()
                    .WithInterval(TimeSpan.FromSeconds(2))
                    .RepeatForever()));
        }
    }
}