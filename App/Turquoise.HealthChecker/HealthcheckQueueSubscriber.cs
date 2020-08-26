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
                Console.WriteLine("Listening on topic " + configuration["queue:servicev1"]);
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

            string stringResult = "";
            BsonDocument document = new BsonDocument();
            try
            {
                document = BsonSerializer.Deserialize<BsonDocument>(res.FirstOrDefault().Result);
            }
            catch (Exception ex)
            {
                stringResult = res.FirstOrDefault().Result;
                logger.LogCritical("Result BsonDocument Deserializion Failed : " + ex);
            }
            var result = new AliveAndWellResult
            {
                Result = document,
                ServiceName = service.Name,
                ServiceNamespace = service.Namespace,
                ServiceUid = service.Uid,
                CreationTime = DateTime.UtcNow,
                Status = res.FirstOrDefault().Status,
                StringResult = stringResult
            };
            await serviceRepo.AddAsync(result);


            logger.LogInformation("HTTP Status : " + res.FirstOrDefault().Status);

            logger.LogCritical("ServiceV1 Sync message " + service.Name);


            if (!res.FirstOrDefault().IsSuccessStatusCode)
            {
                await bus.PublishAsync(result, configuration["queue:nofity"]).ContinueWith(task =>
                        {
                            if (task.IsCompleted)
                            {

                                logger.LogInformation("Task Added to RabbitMQ " + configuration["queue:nofity"] + " " + result.ServiceName);
                            }
                            if (task.IsFaulted)
                            {
                                logger.LogCritical("\n\n");
                                logger.LogCritical("Error on adding to Queue " + configuration["queue:nofity"] + " " + task.Exception.Message);
                                logger.LogCritical("\n\n");
                            }
                        });

            }
        }
    }
}
