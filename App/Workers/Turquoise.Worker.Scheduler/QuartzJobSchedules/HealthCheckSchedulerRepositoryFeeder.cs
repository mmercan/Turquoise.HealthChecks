using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Quartz;
using Turquoise.Common.Mongo;
using Turquoise.Common.Scheduler;
using Turquoise.Common.Scheduler.HealthCheck;
using Turquoise.Models.Scheduler;

namespace Turquoise.Worker.Scheduler.QuartzJobSchedules
{
    [DisallowConcurrentExecution]
    public class HealthCheckSchedulerRepositoryFeeder : IJob
    {
        private readonly ILogger<HealthCheckSchedulerRepositoryFeeder> _logger;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo;

        private readonly HealthCheckSchedulerRepository<Turquoise.Models.Mongo.ServiceV1> healthCheckSchedulerRepository;

        public HealthCheckSchedulerRepositoryFeeder(
            ILogger<HealthCheckSchedulerRepositoryFeeder> logger,
            MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo,
            HealthCheckSchedulerRepository<Turquoise.Models.Mongo.ServiceV1> healthCheckSchedulerRepository)
        {
            _logger = logger;
            this.serviceRepo = serviceRepo;
            this.healthCheckSchedulerRepository = healthCheckSchedulerRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var filter = Builders<Turquoise.Models.Mongo.ServiceV1>.Filter.ElemMatch(x => x.Annotations, x => x.Key == "healthcheck/crontab");
            var qq = await serviceRepo.Items.FindAsync(filter);
            var cronitems = qq.ToList();

            _logger.LogCritical("HealthCheckSchedulerRepositoryFeeder Started " + cronitems.Count() + " element");

            // Add Modify 
            foreach (var item in cronitems)
            {
                var repoitem = healthCheckSchedulerRepository.Items.FirstOrDefault(p => p.Uid == item.Uid);
                if (repoitem != null)
                {
                    if (item.Annotations.FirstOrDefault(p => p.Key == "healthcheck/crontab")?.Value != null &&
                    repoitem.Schedule != item.Annotations.FirstOrDefault(p => p.Key == "healthcheck/crontab")?.Value
                    )
                    {

                        healthCheckSchedulerRepository.Items.Remove(repoitem);

                        var newitem = new ScheduledTask<Models.Mongo.ServiceV1>
                        {
                            Item = item,
                            Name = item.Name,
                            Namespace = item.Namespace,
                            Uid = item.Uid,
                            Schedule = item.Annotations.FirstOrDefault(p => p.Key == "healthcheck/crontab")?.Value
                        };

                        healthCheckSchedulerRepository.Items.Add(newitem);
                    }
                }
                else
                {
                    _logger.LogCritical("HealthCheckSchedulerRepositoryFeeder Item Added  " + item.Name);
                    var newitem = new ScheduledTask<Models.Mongo.ServiceV1>
                    {
                        Item = item,
                        Name = item.Name,
                        Namespace = item.Namespace,
                        Uid = item.Uid,
                        Schedule = item.Annotations.FirstOrDefault(p => p.Key == "healthcheck/crontab")?.Value
                    };

                    healthCheckSchedulerRepository.Items.Add(newitem);

                }
            }


            foreach (var item in healthCheckSchedulerRepository.Items)
            {
                var cronitem = cronitems.FirstOrDefault(p => p.Uid == item.Uid);
                if (cronitem == null)
                {
                    healthCheckSchedulerRepository.Items.Remove(item);
                }
            }
        }
    }
}