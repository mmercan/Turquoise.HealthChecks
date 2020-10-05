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
using TimeZoneConverter;
using Turquoise.Common.Scheduler;
using Turquoise.Common.Scheduler.Deployment;
using Turquoise.Models.RabbitMQ;

namespace Turquoise.Worker.Scheduler.Schedules
{
    public class DeploymentScaleScheduler : BackgroundService
    {
        private DeploymentSchedulerScaleRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentscaleRepo;
        private ILogger<DeploymentScaleScheduler> logger;
        private IBus bus;
        private IConfiguration configuration;

        public DeploymentScaleScheduler(
            DeploymentSchedulerScaleRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentscaleRepo,
            ILogger<DeploymentScaleScheduler> logger,
            EasyNetQ.IBus bus,
            IConfiguration configuration)
        {
            this.deploymentscaleRepo = deploymentscaleRepo;
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

            TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("Australia/Melbourne");
            var localtime = TimeZoneInfo.ConvertTime(referenceTime, tzi);

            logger.LogCritical("Local time zone: " + tzi.DisplayName + " and Local Time is " + localtime.ToString());
            logger.LogCritical("Checking for Scale ScheduledTasks " + deploymentscaleRepo.ScheduledTasks.Count.ToString() + " Counted");

            var tasksThatShouldRun = deploymentscaleRepo.ScheduledTasks.Where(t => t.ShouldRun(localtime)).ToList();
            foreach (var taskThatShouldRun in tasksThatShouldRun)
            {
                taskThatShouldRun.Increment();
                logger.LogCritical("Scaling " + taskThatShouldRun.Task.Name + " replica : " + taskThatShouldRun.Task.ScaleDetails.ReplicaNumber + " at " + DateTime.UtcNow.ToString());


                var scalemessage = new DeploymentScalerMessager
                {
                    Uid = taskThatShouldRun.Task.Uid,
                    Name = taskThatShouldRun.Task.Name,
                    nameSpace = taskThatShouldRun.Task.Namespace,
                    Schedule = taskThatShouldRun.Task.Schedule,
                    Timezone = taskThatShouldRun.Task.ScaleDetails.Timezone,
                    ReplicaNumber = taskThatShouldRun.Task.ScaleDetails.ReplicaNumber,
                    ScaleUpDown = taskThatShouldRun.Task.ScaleDetails.ScaleUpDown
                };

                bus.PublishAsync(scalemessage, configuration["queue:scale"]).ContinueWith(task =>
                         {
                             if (task.IsCompleted)
                             {
                                 logger.LogCritical("Task Added to RabbitMQ " + configuration["queue:scale"] + " " + taskThatShouldRun.Task.Name + " at " + DateTime.Now.ToString());
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
