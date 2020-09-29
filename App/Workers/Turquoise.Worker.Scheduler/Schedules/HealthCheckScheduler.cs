using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Turquoise.Common.Scheduler;
using Turquoise.Common.Scheduler.HealthCheck;

namespace Turquoise.Worker.Scheduler.Schedules
{
    public class HealthCheckScheduler : BackgroundService
    {
        private HealthCheckSchedulerRepository<Turquoise.Models.Mongo.ServiceV1> healthCheckRepo;
        private ILogger<HealthCheckScheduler> logger;
        private IBus bus;
        private IConfiguration configuration;

        public HealthCheckScheduler(HealthCheckSchedulerRepository<Turquoise.Models.Mongo.ServiceV1> healthCheckRepo, ILogger<HealthCheckScheduler> logger, EasyNetQ.IBus bus, IConfiguration configuration)
        {
            this.healthCheckRepo = healthCheckRepo;
            this.logger = logger;
            this.bus = bus;
            this.configuration = configuration;
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
            logger.LogCritical("Checking for HealthCheck ScheduledTasks " + healthCheckRepo.ScheduledTasks.Count.ToString() + " Counted");
            var tasksThatShouldRun = healthCheckRepo.ScheduledTasks.Where(t => t.ShouldRun(referenceTime)).ToList();
            foreach (var taskThatShouldRun in tasksThatShouldRun)
            {
                taskThatShouldRun.Increment();
                logger.LogCritical("Task Adding to RabbitMQ " + taskThatShouldRun.Task.Name);

                bus.PublishAsync(taskThatShouldRun.Item, configuration["queue:servicev1"]).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        logger.LogInformation("Task Added to RabbitMQ " + configuration["queue:servicev1"] + " " + taskThatShouldRun.Task.Name);
                    }
                    if (task.IsFaulted)
                    {
                        logger.LogCritical("\n\n");
                        logger.LogCritical(task.Exception.Message);
                        logger.LogCritical("\n\n");
                    }
                });
            }
            return Task.FromResult("");
        }

    }
}
