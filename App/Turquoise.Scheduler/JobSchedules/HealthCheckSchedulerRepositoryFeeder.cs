using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Quartz;
using Turquoise.Common.Mongo;
using Turquoise.Common.Scheduler;
using Turquoise.Scheduler.Services;

namespace Turquoise.Scheduler.JobSchedules
{
    [DisallowConcurrentExecution]
    public class HealthCheckSchedulerRepositoryFeeder : IJob
    {
        private readonly ILogger<HealthCheckSchedulerRepositoryFeeder> _logger;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo;
        private readonly IMapper mapper;
        private readonly HealthCheckSchedulerRepository<Turquoise.Models.Mongo.ServiceV1> healthCheckSchedulerRepository;

        public HealthCheckSchedulerRepositoryFeeder(ILogger<HealthCheckSchedulerRepositoryFeeder> logger, MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo, HealthCheckSchedulerRepository<Turquoise.Models.Mongo.ServiceV1> healthCheckSchedulerRepository, IMapper mapper)
        {
            _logger = logger;
            this.serviceRepo = serviceRepo;
            this.mapper = mapper;
            this.healthCheckSchedulerRepository = healthCheckSchedulerRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {


            var filter = Builders<Turquoise.Models.Mongo.ServiceV1>.Filter.ElemMatch(x => x.Annotations, x => x.Key == "healthcheck/crontab");
            var qq = await serviceRepo.Items.FindAsync(filter);
            var cronitems = qq.ToList();

            _logger.LogCritical("HealthCheckSchedulerRepositoryFeeder Started " + cronitems.Count() + " element");
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

                        var newitem = new HealthCheckScheduledTask<Models.Mongo.ServiceV1>
                        {
                            Item = item,
                            Name = item.Name,
                            Uid = item.Uid,
                            Schedule = item.Annotations.FirstOrDefault(p => p.Key == "healthcheck/crontab")?.Value
                        };

                        healthCheckSchedulerRepository.Items.Add(newitem);

                    }
                }
                else
                {
                    _logger.LogCritical("HealthCheckSchedulerRepositoryFeeder Item Added  " + item.Name);
                    var newitem = new HealthCheckScheduledTask<Models.Mongo.ServiceV1>
                    {
                        Item = item,
                        Name = item.Name,
                        Uid = item.Uid,
                        Schedule = item.Annotations.FirstOrDefault(p => p.Key == "healthcheck/crontab")?.Value
                    };

                    healthCheckSchedulerRepository.Items.Add(newitem);

                }
            }
            // var mapped = mapper.Map<List<IHealthCheckScheduledTask<Turquoise.Models.Mongo.ServiceV1>>>(cronitems);
            // var diffs = healthCheckSchedulerRepository.Items.AsParallel().Except(mapped.AsParallel());
            // foreach (var item in diffs)
            // {
            //     healthCheckSchedulerRepository.Items.Add(item);
            //     _logger.LogCritical("Feeder : " + item.Name);
            // }
        }
    }
}