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

namespace Turquoise.Scheduler.Services
{
    public class DeploymentScaleUpScheduler : BackgroundService
    {
        private DeploymentSchedulerScaleUpRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentscaleUpRepo;
        private ILogger<DeploymentScaleUpScheduler> logger;
        private IBus bus;
        private IConfiguration configuration;

        public DeploymentScaleUpScheduler(DeploymentSchedulerScaleUpRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentscaleUpRepo, ILogger<DeploymentScaleUpScheduler> logger, EasyNetQ.IBus bus, IConfiguration configuration)
        {
            this.deploymentscaleUpRepo = deploymentscaleUpRepo;
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
            logger.LogCritical("Checking for ScheduledTasks " + deploymentscaleUpRepo.ScheduledTasks.Count.ToString() + " Counted");
            var tasksThatShouldRun = deploymentscaleUpRepo.ScheduledTasks.Where(t => t.ShouldRun(referenceTime)).ToList();
            foreach (var taskThatShouldRun in tasksThatShouldRun)
            {
                taskThatShouldRun.Increment();
                logger.LogCritical("Scaling Up " + taskThatShouldRun.Task.Name + " replica : " + taskThatShouldRun.Task.ReplicaNumber);


                // bus.PublishAsync(taskThatShouldRun.Item, configuration["queue:servicev1"]).ContinueWith(task =>
                //          {
                //              if (task.IsCompleted)
                //              {

                //                  logger.LogInformation("Task Added to RabbitMQ " + configuration["queue:servicev1"] + " " + taskThatShouldRun.Task.Name);
                //              }
                //              if (task.IsFaulted)
                //              {
                //                  logger.LogCritical("\n\n");
                //                  logger.LogCritical(task.Exception.Message);
                //                  logger.LogCritical("\n\n");
                //              }
                //          });

            }
            return Task.FromResult("");
        }

    }
}
