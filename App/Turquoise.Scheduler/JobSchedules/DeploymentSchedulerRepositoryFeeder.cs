using System;
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
    public class DeploymentSchedulerRepositoryFeeder : IJob
    {
        private readonly ILogger<DeploymentSchedulerRepositoryFeeder> _logger;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.DeploymentV1> deploymentRepo;

        private readonly DeploymentSchedulerScaleUpRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentScaleUpRepository;
        private readonly DeploymentSchedulerScaleDownRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentScaleDownRepository;

        public DeploymentSchedulerRepositoryFeeder(
         ILogger<DeploymentSchedulerRepositoryFeeder> logger,
         MangoBaseRepo<Turquoise.Models.Mongo.DeploymentV1> deploymentRepo,
         DeploymentSchedulerScaleUpRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentScaleUpRepository,
         DeploymentSchedulerScaleDownRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentScaleDownRepository)
        {
            _logger = logger;
            this.deploymentRepo = deploymentRepo;

            this.deploymentScaleUpRepository = deploymentScaleUpRepository;
            this.deploymentScaleDownRepository = deploymentScaleDownRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            var filter = Builders<Turquoise.Models.Mongo.DeploymentV1>.Filter.ElemMatch(x => x.Metadata.Annotations, x => x.Key == "taka/downscale-crontab");
            var qq = await deploymentRepo.Items.FindAsync(filter);
            var cronitems = qq.ToList();

            _logger.LogCritical("DeploymentRepositoryFeeder Started " + cronitems.Count() + " element");
            foreach (var item in cronitems)
            {
                if (item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-crontab")?.Value != null &&
                    item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/downscale-crontab")?.Value != null)
                {
                    int? replicanumber = getReplicaNumber(item);

                    scaleUpAddEdit(item, replicanumber);
                    scaleDownAddEdit(item, replicanumber);
                }
                else
                {
                    _logger.LogCritical("taka/upscale-crontab or  taka/downscale-crontab Not Found and Skipped" + item.Name);
                }
            }

        }

        private void scaleUpAddEdit(Models.Mongo.DeploymentV1 item, int? replicanumber)
        {
            var repoitem = deploymentScaleUpRepository.Items.FirstOrDefault(p => p.Uid == item.Metadata.Uid);
            if (repoitem != null)
            {
                if (repoitem.Schedule != item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-crontab")?.Value ||
                    repoitem.ReplicaNumber.ToString() != item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-replica")?.Value)
                {
                    deploymentScaleUpRepository.Items.Remove(repoitem);
                    var newitem = new ScheduledTask<Models.Mongo.DeploymentV1>
                    {
                        Item = item,
                        Name = item.Name,
                        Uid = item.Metadata.Uid,
                        ReplicaNumber = replicanumber,
                        Schedule = item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-crontab")?.Value
                    };
                    deploymentScaleUpRepository.Items.Add(newitem);
                }
                else
                {
                    _logger.LogCritical("deploymentScaleUpRepository Values Haven't beenc chnaged kept original  " + item.Name);
                }
            }
            else
            {
                _logger.LogCritical("deploymentScaleUpRepository Item Added  " + item.Name);
                var newitem = new ScheduledTask<Models.Mongo.DeploymentV1>
                {
                    Item = item,
                    Name = item.Name,
                    Uid = item.Metadata.Uid,
                    Schedule = item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-crontab")?.Value,
                    ReplicaNumber = replicanumber
                };
                deploymentScaleUpRepository.Items.Add(newitem);
            }
        }

        private void scaleDownAddEdit(Models.Mongo.DeploymentV1 item, int? replicanumber)
        {
            var repoitem = deploymentScaleDownRepository.Items.FirstOrDefault(p => p.Uid == item.Metadata.Uid);
            if (repoitem != null)
            {
                if (repoitem.Schedule != item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/downscale-crontab")?.Value ||
                    repoitem.ReplicaNumber.ToString() != item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/downscale-replica")?.Value)
                {
                    deploymentScaleDownRepository.Items.Remove(repoitem);
                    var newitem = new ScheduledTask<Models.Mongo.DeploymentV1>
                    {
                        Item = item,
                        Name = item.Name,
                        Uid = item.Metadata.Uid,
                        ReplicaNumber = replicanumber,
                        Schedule = item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/downscale-crontab")?.Value
                    };
                    deploymentScaleDownRepository.Items.Add(newitem);
                }
                else
                {
                    _logger.LogCritical("deploymentScaleDownRepository Values Haven't beenc chnaged kept original  " + item.Name);
                }
            }
            else
            {
                _logger.LogCritical("deploymentScaleDownRepository Item Added  " + item.Name);
                var newitem = new ScheduledTask<Models.Mongo.DeploymentV1>
                {
                    Item = item,
                    Name = item.Name,
                    Uid = item.Metadata.Uid,
                    Schedule = item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/downscale-crontab")?.Value,
                    ReplicaNumber = replicanumber
                };
                deploymentScaleDownRepository.Items.Add(newitem);
            }
        }

        private static int? getReplicaNumber(Models.Mongo.DeploymentV1 item)
        {
            string replica = item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-replica")?.Value;
            int? replicanumber = null;
            int replicaout = 0;
            if (!string.IsNullOrWhiteSpace(replica) && Int32.TryParse(replica, out replicaout))
            {
                replicanumber = replicaout;
            }

            return replicanumber;
        }
        // var mapped = mapper.Map<List<ScheduledTask<Turquoise.Models.Mongo.ServiceV1>>>(cronitems);
        // var diffs = healthCheckSchedulerRepository.Items.AsParallel().Except(mapped.AsParallel());
        // foreach (var item in diffs)
        // {
        //     healthCheckSchedulerRepository.Items.Add(item);
        //     _logger.LogCritical("Feeder : " + item.Name);
        // }
    }
}
