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
using Turquoise.Models.RabbitMQ;

namespace Turquoise.HealthChecker
{
    public class HealthcheckQueueSubscriber : BackgroundService
    {
        IBus bus;
        private IsAliveAndWellHealthChecker healthChecker;
        private MangoBaseRepo<AliveAndWellResult> healthresultRepo;
        private IConfiguration configuration;
        private MangoBaseRepo<ServiceV1> serviceRepo;
        private ManualResetEventSlim _ResetEvent = new ManualResetEventSlim(false);
        private readonly ILogger<HealthcheckQueueSubscriber> logger;

        private Task executingTask;


        public HealthcheckQueueSubscriber(
            ILogger<HealthcheckQueueSubscriber> logger,
            IBus bus,
            IsAliveAndWellHealthChecker healthChecker,
            MangoBaseRepo<Turquoise.Models.Mongo.AliveAndWellResult> healthresultRepo,
            MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo,
            IConfiguration configuration
            )
        {
            this.logger = logger;
            this.bus = bus;
            this.healthChecker = healthChecker;
            this.healthresultRepo = healthresultRepo;
            this.configuration = configuration;
            this.serviceRepo = serviceRepo;
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
                HealthcheckQueueSubscriberStats.SetIsqueueSubscriberStarted(false);
                logger.LogError("Exception: " + ex.Message);
            }
        }

        private async Task Handler(Turquoise.Models.Mongo.ServiceV1 service)
        {
            HealthcheckQueueSubscriberStats.SetIsqueueSubscriberStarted(true);
            HealthcheckQueueSubscriberStats.SetProcessTime();

            var res = await healthChecker.DownloadAsync(service);

            string stringResult = "";
            var itemstatus = "";
            BsonDocument document = new BsonDocument();
            try
            {
                document = BsonSerializer.Deserialize<BsonDocument>(res.FirstOrDefault().Result);


                if (document["status"].IsString)
                {
                    itemstatus = document["status"].ToString();
                    logger.LogCritical("Status Found : " + itemstatus);

                }
                else
                {
                    logger.LogCritical("Status NOT Found ");
                }
                // BsonValue value;
                // if (document.TryGetValue("Status", out value))
                // {
                //     itemstatus = value.ToString();
                // }
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
            await healthresultRepo.AddAsync(result);


            var mongoservice = serviceRepo.Find(p => p.Uid == service.Uid);
            if (mongoservice.FirstOrDefault() != null)
            {
                if (itemstatus == "")
                {
                    if (res.FirstOrDefault().IsSuccessStatusCode)
                    {
                        itemstatus = "OK";
                    }
                    else if (res.FirstOrDefault().Status == "NotFound")
                    {
                        itemstatus = "NotFound";
                    }
                    else
                    {
                        itemstatus = "ServiceUnavailable";
                    }
                }

                mongoservice.FirstOrDefault().HealthIsaliveAndWell = itemstatus;
                mongoservice.FirstOrDefault().HealthIsaliveAndWellSyncDateUTC = DateTime.UtcNow;
                await serviceRepo.UpdateAsync(mongoservice.FirstOrDefault());
            }

            logger.LogInformation("HTTP Status : " + res.FirstOrDefault().Status);

            logger.LogCritical("ServiceV1 Sync message " + service.Name);


            if (!res.FirstOrDefault().IsSuccessStatusCode)
            {
                var notify = new NotifyServiceHealthCheckError { ID = result.Id.ToString(), ServiceName = service.Name, StatusCode = res.FirstOrDefault().Status };
                await bus.PublishAsync(notify, configuration["queue:nofity"]).ContinueWith(task =>
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


    public static class HealthcheckQueueSubscriberStats
    {
        private static bool _isqueueSubscriberStarted;
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
