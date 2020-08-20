using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Turquoise.Common.Scheduler;

namespace Turquoise.Scheduler.Services
{
    public class AppHealthCheckScheduler : BackgroundService
    {
        private HealthCheckSchedulerRepository healthCheckRepo;
        private ILogger<AppHealthCheckScheduler> logger;

        public AppHealthCheckScheduler(HealthCheckSchedulerRepository healthCheckRepo, ILogger<AppHealthCheckScheduler> logger)
        {
            this.healthCheckRepo = healthCheckRepo;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ExecuteOnceAsync(cancellationToken);
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }


        private Task ExecuteOnceAsync(CancellationToken cancellationToken)
        {
            var referenceTime = DateTime.UtcNow;
            logger.LogCritical("Checking for ScheduledTasks");
            var tasksThatShouldRun = healthCheckRepo.ScheduledTasks.Where(t => t.ShouldRun(referenceTime)).ToList();
            foreach (var taskThatShouldRun in tasksThatShouldRun)
            {
                taskThatShouldRun.Increment();
                logger.LogCritical("Task Added to RabbitMQ " + taskThatShouldRun.Task.Name);
            }
            return Task.FromResult("");
        }

    }
}
