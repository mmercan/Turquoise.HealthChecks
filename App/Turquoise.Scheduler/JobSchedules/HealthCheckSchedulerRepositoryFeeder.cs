using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Quartz;
using Turquoise.Common.Mongo;
using Turquoise.Common.Scheduler;

namespace Turquoise.Scheduler.JobSchedules
{
    [DisallowConcurrentExecution]
    public class HealthCheckSchedulerRepositoryFeeder : IJob
    {
        private readonly ILogger<HealthCheckSchedulerRepositoryFeeder> _logger;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo;
        private readonly IMapper mapper;
        private readonly HealthCheckSchedulerRepository healthCheckSchedulerRepository;

        public HealthCheckSchedulerRepositoryFeeder(ILogger<HealthCheckSchedulerRepositoryFeeder> logger, MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo, HealthCheckSchedulerRepository healthCheckSchedulerRepository, IMapper mapper)
        {
            _logger = logger;
            this.serviceRepo = serviceRepo;
            this.mapper = mapper;
            this.healthCheckSchedulerRepository = healthCheckSchedulerRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogCritical("HealthCheckSchedulerRepositoryFeeder Started");

            var filter = Builders<Turquoise.Models.Mongo.ServiceV1>.Filter.ElemMatch(x => x.Annotations, x => x.Key == "healthcheck/crontab");
            var qq = await serviceRepo.Items.FindAsync(filter);
            var cronitems = qq.ToList();


            foreach (var item in cronitems)
            {
                _logger.LogCritical("Feeder : " + item.Name);
            }
            //    .ElemMatch(
            //              x => x.Annotations,
            //              s=>criteria.Contains(s)));


            //     return Task.CompletedTask;
        }
    }
}