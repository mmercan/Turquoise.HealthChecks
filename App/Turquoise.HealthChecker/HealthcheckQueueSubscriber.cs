using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Turquoise.Common.Mongo;
using Turquoise.HealthChecker.Services;
using Turquoise.Models.Mongo;

namespace Turquoise.HealthChecker
{
    public class HealthcheckQueueSubscriber : BackgroundService
    {

        IBus bus;
        private IsAliveAndWellHealthChecker healthChecker;
        private MangoBaseRepo<AliveAndWellResult> serviceRepo;
        private IConfiguration configuration;
        private ManualResetEventSlim _ResetEvent = new ManualResetEventSlim(false);
        private readonly ILogger<HealthcheckQueueSubscriber> logger;

        private Task executingTask;


        public HealthcheckQueueSubscriber(
            ILogger<HealthcheckQueueSubscriber> logger,
            IBus bus,
            IsAliveAndWellHealthChecker healthChecker,
            MangoBaseRepo<Turquoise.Models.Mongo.AliveAndWellResult> serviceRepo,
            IConfiguration configuration
            )
        {
            this.logger = logger;
            this.bus = bus;
            this.healthChecker = healthChecker;
            this.serviceRepo = serviceRepo;
            this.configuration = configuration;
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
                bus.SubscribeAsync<Turquoise.Models.Mongo.ServiceV1>(configuration["queue:servicev1"], Handler); //, x => x.WithTopic("product.*"));
                Console.WriteLine("Listening on topic healthcheck.servicev1");
                _ResetEvent.Wait();
            }
            catch (Exception ex)
            {
                logger.LogError("Exception: " + ex.Message);
            }
        }

        private async Task Handler(Turquoise.Models.Mongo.ServiceV1 service)
        {

            var res = await healthChecker.DownloadAsync(service);


            var document = BsonSerializer.Deserialize<BsonDocument>(res.FirstOrDefault().Result);
            var result = new AliveAndWellResult
            {
                Result = document,
                ServiceName = service.Name,
                ServiceNamespace = service.Namespace,
                ServiceUid = service.Uid,
                CreationTime = DateTime.UtcNow,
                Status = res.FirstOrDefault().Status

            };

            await serviceRepo.AddAsync(result);
            logger.LogInformation("HTTP Status : " + res.FirstOrDefault().Status);
            //state.IngressUrl
            logger.LogCritical("ServiceV1 Sync message " + service.Name);
        }
    }
}
