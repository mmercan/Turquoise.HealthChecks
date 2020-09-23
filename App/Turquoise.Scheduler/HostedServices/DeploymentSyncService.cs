using System;
using System.Threading;
using System.Threading.Tasks;

using k8s.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.Services;
using k8s;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Turquoise.Scheduler.HostedServices
{
    public class DeploymentSyncService : BackgroundService
    {
        private ILogger<DeploymentSyncService> logger;
        private IConfiguration configuration;
        private Task executingTask;
        private DateTime lastrestart = DateTime.UtcNow;
        private readonly K8sService k8sService;

        private readonly IDistributedCache cache;

        public DeploymentSyncService(
            ILogger<DeploymentSyncService> logger,
            IConfiguration configuration,
            K8sService k8sService,
            IDistributedCache cache
            )
        {
            this.logger = logger;
            this.configuration = configuration;
            this.k8sService = k8sService;
            this.cache = cache;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            executingTask = Task.Factory.StartNew(new Action(deployWatchStarter), TaskCreationOptions.LongRunning);
            if (executingTask.IsCompleted)
            {
                return executingTask;
            }
            return Task.CompletedTask;
        }


        private void deployWatchStarter()
        {
            var deploylistResp = k8sService.client.ListNamespacedDeploymentWithHttpMessagesAsync("", watch: true);
            var podlistResp = k8sService.client.ListNamespacedPodWithHttpMessagesAsync("", watch: true);

            // var podlistResp123 = k8sService.client.ListPodForAllNamespacesAsync(watch: true);

            this.logger.LogCritical("Watch Started");
            //     using (podlistResp.Watch<V1Pod, V1PodList>((type, item) =>
            //    {
            //        this.logger.LogCritical("==on watch event==");
            //        this.logger.LogCritical(type.ToString());
            //        this.logger.LogCritical(item.Metadata.Name);
            //        this.logger.LogCritical("===on watch event===");
            //    }))
            //     {
            //         var ctrlc = new ManualResetEventSlim(false);
            //         ctrlc.Wait();
            //     }



            using (deploylistResp.Watch<V1Deployment, V1DeploymentList>(watcher, onError: OnError, onClosed: OnClosed))
            {
                this.logger.LogCritical("=== on watch Done ===");
                var ctrlc = new ManualResetEventSlim(false);
                ctrlc.Wait();
            }


        }

        private void watcher(WatchEventType type, V1Deployment item)
        {
            this.logger.LogCritical("==on watch event==");
            this.logger.LogCritical(type.ToString());
            this.logger.LogCritical(item.Metadata.Name);
            this.logger.LogCritical("===on watch event===");
            SavetoCache(item).Wait();
        }

        private void OnError(Exception ex)
        {
            this.logger.LogError("===on watch Exception : " + ex.Message);
        }

        private void OnClosed()
        {
            var utc = DateTime.UtcNow.ToString();
            var howlongran = (DateTime.UtcNow - lastrestart);

            this.logger.LogError("===on watch Connection  Closed after " + howlongran.TotalMinutes.ToString() + ":" + howlongran.Seconds.ToString() + " min:sec : re-running delay 30 seconds " + utc);


            Task.Delay(TimeSpan.FromSeconds(30)).Wait();
            lastrestart = DateTime.UtcNow;
            this.logger.LogError("=== on watch Restarting Now.... ===" + lastrestart.ToString());
            executingTask = Task.Factory.StartNew(new Action(deployWatchStarter), TaskCreationOptions.LongRunning);
        }



        private async Task SavetoCache(V1Deployment item)
        {
            string key = item.Metadata.Namespace() + ":" + item.Name();
            await SavetoCache(key, item);
        }
        private async Task SavetoCache(string key, object data)
        {
            byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(key);
            // var options = new DistributedCacheEntryOptions()
            //     .SetSlidingExpiration(TimeSpan.FromSeconds(20));
            await cache.SetAsync(key, encodedCurrentTimeUTC);
        }

    }
}