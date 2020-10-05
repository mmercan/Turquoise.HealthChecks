using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Turquoise.Common.Mongo;
using Turquoise.K8sServices;
using Turquoise.Models.Mongo;
using Turquoise.Models.RabbitMQ;

namespace Turquoise.Worker.Scaler.JobSchedules
{
    public class DeploymentScalerQueueSubscriber : BackgroundService
    {
        IBus bus;
        private IConfiguration configuration;
        //  private MangoBaseRepo<ServiceV1> serviceRepo;
        private ManualResetEventSlim _ResetEvent = new ManualResetEventSlim(false);
        private readonly ILogger<DeploymentScalerQueueSubscriber> logger;

        private readonly MangoBaseRepo<DeploymentScaleHistory> scaleHistoryRepo;

        private readonly K8sGeneralService k8Service;
        private Task executingTask;


        public DeploymentScalerQueueSubscriber(
            ILogger<DeploymentScalerQueueSubscriber> logger, IBus bus,
            MangoBaseRepo<DeploymentScaleHistory> scaleHistoryRepo, IConfiguration configuration,
            K8sGeneralService k8Service
            )
        {
            this.logger = logger;
            this.bus = bus;
            this.scaleHistoryRepo = scaleHistoryRepo;
            this.configuration = configuration;
            this.k8Service = k8Service;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            executingTask = Task.Factory.StartNew(new Action(SubscribeQueue), TaskCreationOptions.LongRunning);
            if (executingTask.IsCompleted)
            {
                return executingTask;
            }
            return Task.CompletedTask;
        }

        private void SubscribeQueue()
        {
            try
            {
                logger.LogCritical("Connected to bus");
                bus.SubscribeAsync<DeploymentScalerMessager>(configuration["queue:scale"], Handler); //, x => x.WithTopic("product.*"));
                logger.LogCritical("Listening on topic " + configuration["queue:scale"]);
                ScalerQueueSubscriberStats.SetIsqueueSubscriberStarted(true);
                _ResetEvent.Wait();
            }
            catch (Exception ex)
            {
                ScalerQueueSubscriberStats.SetIsqueueSubscriberStarted(false);
                logger.LogError("Exception: " + ex.Message);
            }
        }

        private async Task Handler(DeploymentScalerMessager scalerMessage)
        {
            logger.LogCritical("new Scale started for " + scalerMessage.Name + " " +
                scalerMessage.nameSpace + " to " + scalerMessage.ReplicaNumber.ToString() + " at " + DateTime.Now.ToString());

            ScalerQueueSubscriberStats.SetIsqueueSubscriberStarted(true);
            ScalerQueueSubscriberStats.SetProcessTime();
            string status = "Success";
            int scaleNumber;
            int oldscaleNumber;
            if (!scalerMessage.ReplicaNumber.HasValue)
            {
                if (scalerMessage.ScaleUpDown == ScaleUpDown.ScaleDown)
                {
                    scaleNumber = 0;
                }
                else
                {
                    scaleNumber = 1;
                }
            }
            else
            {
                scaleNumber = scalerMessage.ReplicaNumber.Value;
            }

            try
            {
                var olddeploy = await k8Service.DeploymentClient.GetSingleAsync(scalerMessage.Name, scalerMessage.nameSpace);
                if (olddeploy == null)
                {
                    status = "Not Found";
                }
                else if (olddeploy.Spec.Replicas.HasValue)
                {
                    oldscaleNumber = olddeploy.Spec.Replicas.Value;
                }

                var newdeploy = await k8Service.DeploymentClient.ScaleDeployment(scalerMessage.Name, scalerMessage.nameSpace, scaleNumber);
                logger.LogCritical("Scale Completed for " + scalerMessage.Name + " " +
                    scalerMessage.nameSpace + " to " + scalerMessage.ReplicaNumber.ToString() + " at " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                status = "Exception :" + ex.Message;
            }


            var scale = new DeploymentScaleHistory
            {
                Uid = scalerMessage.Uid,
                Name = scalerMessage.Name,
                Namespace = scalerMessage.nameSpace,
                Schedule = scalerMessage.Schedule,
                Timezone = scalerMessage.Timezone,
                ScaledUtc = DateTime.UtcNow,
                NewScaleNumber = scaleNumber,
                Status = status

            };

            await scaleHistoryRepo.AddAsync(scale);
            logger.LogCritical("Scale History Added for " + scalerMessage.Name + " " +
                    scalerMessage.nameSpace + " to " + scalerMessage.ReplicaNumber.ToString() + " at " + DateTime.Now.ToString());
        }

    }



    public static class ScalerQueueSubscriberStats
    {
        private static bool _isqueueSubscriberStarted = true;
        private static DateTime _lastProcessTime;
        private static readonly object _lockObject = new object();

        public static DateTime GetLastProcessTime()
        {
            lock (_lockObject)
            {
                return _lastProcessTime;
            }
        }
        public static void SetProcessTime()
        {
            lock (_lockObject)
            {
                _lastProcessTime = DateTime.UtcNow;
            }
        }



        public static bool GetIsqueueSubscriberStarted()
        {
            lock (_lockObject)
            {
                return _isqueueSubscriberStarted;
            }
        }
        public static void SetIsqueueSubscriberStarted(bool status)
        {
            lock (_lockObject)
            {
                _isqueueSubscriberStarted = status;
            }
        }
    }
}