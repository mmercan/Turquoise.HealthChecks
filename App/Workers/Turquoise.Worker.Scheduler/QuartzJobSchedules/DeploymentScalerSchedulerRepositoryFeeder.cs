using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Quartz;
using Turquoise.Common.Mongo;
using Turquoise.Common.Scheduler;
using Turquoise.Common.Scheduler.Deployment;
using Turquoise.Models.RabbitMQ;
using Turquoise.Models.Scheduler;

namespace Turquoise.Worker.Scheduler.QuartzJobSchedules
{
    [DisallowConcurrentExecution]
    public class DeploymentScalerSchedulerRepositoryFeeder : IJob
    {
        private readonly ILogger<DeploymentScalerSchedulerRepositoryFeeder> _logger;
        private readonly MangoBaseRepo<Turquoise.Models.Mongo.DeploymentV1> deploymentRepo;
        private readonly DeploymentSchedulerScaleRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentScaleRepository;

        public DeploymentScalerSchedulerRepositoryFeeder(
         ILogger<DeploymentScalerSchedulerRepositoryFeeder> logger,
         MangoBaseRepo<Turquoise.Models.Mongo.DeploymentV1> deploymentRepo,
         DeploymentSchedulerScaleRepository<Turquoise.Models.Mongo.DeploymentV1> deploymentScaleRepository)
        {
            _logger = logger;
            this.deploymentRepo = deploymentRepo;

            this.deploymentScaleRepository = deploymentScaleRepository;
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
                    scaleUpAddEdit(item);
                    scaleDownAddEdit(item);
                }
                else
                {
                    _logger.LogCritical("taka/upscale-crontab or  taka/downscale-crontab Not Found and Skipped" + item.Name);
                }
            }


            foreach (var item in deploymentScaleRepository.Items)
            {
                var cronitem = cronitems.FirstOrDefault(p => p.Metadata.Uid == item.Uid);
                if (cronitem == null)
                {
                    deploymentScaleRepository.Items.Remove(item);
                }
            }
        }

        private void scaleUpAddEdit(Models.Mongo.DeploymentV1 item)
        {
            int? replicanumber = getReplicaNumber(item, "taka/upscale-replica");
            var repoitem = deploymentScaleRepository.Items.FirstOrDefault(p => p.Uid == item.Metadata.Uid && p.ScaleDetails.ScaleUpDown == ScaleUpDown.ScaleUp);
            if (repoitem != null)
            {
                if (repoitem.Schedule != item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-crontab")?.Value ||
                    repoitem.ScaleDetails.ReplicaNumber.ToString() != item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-replica")?.Value)
                {
                    _logger.LogCritical(item.NameandNamespace + " Upscale is Changed Updading now");
                    deploymentScaleRepository.Items.Remove(repoitem);
                    var newitem = new ScheduledTask<Models.Mongo.DeploymentV1>
                    {
                        Item = item,
                        Name = item.Name,
                        Namespace = item.Namespace,
                        Uid = item.Metadata.Uid,
                        Schedule = item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-crontab")?.Value,
                        ScaleDetails = new ScaleDetails { ReplicaNumber = replicanumber, ScaleUpDown = ScaleUpDown.ScaleUp }
                    };
                    deploymentScaleRepository.Items.Add(newitem);
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
                    Namespace = item.Namespace,
                    Uid = item.Metadata.Uid,
                    Schedule = item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/upscale-crontab")?.Value,
                    ScaleDetails = new ScaleDetails { ReplicaNumber = replicanumber, ScaleUpDown = ScaleUpDown.ScaleUp },
                };
                deploymentScaleRepository.Items.Add(newitem);
            }
        }

        private void scaleDownAddEdit(Models.Mongo.DeploymentV1 item)
        {

            int? replicanumber = getReplicaNumber(item, "taka/downscale-replica");
            var repoitem = deploymentScaleRepository.Items.FirstOrDefault(p => p.Uid == item.Metadata.Uid && p.ScaleDetails.ScaleUpDown == ScaleUpDown.ScaleDown);
            if (repoitem != null)
            {
                if (repoitem.Schedule != item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/downscale-crontab")?.Value ||
                    repoitem.ScaleDetails.ReplicaNumber.ToString() != item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/downscale-replica")?.Value)
                {
                    _logger.LogCritical(item.NameandNamespace + " Downscale is Changed Updading now");
                    deploymentScaleRepository.Items.Remove(repoitem);
                    var newitem = new ScheduledTask<Models.Mongo.DeploymentV1>
                    {
                        Item = item,
                        Name = item.Name,
                        Namespace = item.Namespace,
                        Uid = item.Metadata.Uid,
                        Schedule = item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/downscale-crontab")?.Value,
                        ScaleDetails = new ScaleDetails { ReplicaNumber = replicanumber, ScaleUpDown = ScaleUpDown.ScaleDown }
                    };
                    deploymentScaleRepository.Items.Add(newitem);
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
                    Namespace = item.Namespace,
                    Uid = item.Metadata.Uid,
                    Schedule = item.Metadata.Annotations.FirstOrDefault(p => p.Key == "taka/downscale-crontab")?.Value,
                    ScaleDetails = new ScaleDetails { ReplicaNumber = replicanumber, ScaleUpDown = ScaleUpDown.ScaleDown }
                };
                deploymentScaleRepository.Items.Add(newitem);
            }
        }

        private static int? getReplicaNumber(Models.Mongo.DeploymentV1 item, string replicatag)
        {
            string replica = item.Metadata.Annotations.FirstOrDefault(p => p.Key == replicatag)?.Value;
            int? replicanumber = null;
            int replicaout = 0;
            if (!string.IsNullOrWhiteSpace(replica) && Int32.TryParse(replica, out replicaout))
            {
                replicanumber = replicaout;
            }

            return replicanumber;
        }
    }
}
