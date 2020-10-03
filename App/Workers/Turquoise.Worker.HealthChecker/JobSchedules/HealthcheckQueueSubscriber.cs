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

namespace Turquoise.Worker.HealthChecker
{
    public class HealthcheckQueueSubscriber : BackgroundService
    {
        IBus bus;
        private IsAliveAndWellHealthChecker healthChecker;
        private MangoBaseRepo<AliveAndWellResult> healthresultRepo;
        private IConfiguration configuration;
        //  private MangoBaseRepo<ServiceV1> serviceRepo;
        private ManualResetEventSlim _ResetEvent = new ManualResetEventSlim(false);
        private readonly ILogger<HealthcheckQueueSubscriber> logger;

        private readonly MangoBaseRepo<ServiceHealthCheckResultSummary> serviceCheckSummaryRepo;

        private Task executingTask;


        public HealthcheckQueueSubscriber(
            ILogger<HealthcheckQueueSubscriber> logger,
            IBus bus,
            IsAliveAndWellHealthChecker healthChecker,
            MangoBaseRepo<Turquoise.Models.Mongo.AliveAndWellResult> healthresultRepo,
            MangoBaseRepo<ServiceHealthCheckResultSummary> serviceCheckSummaryRepo,
            MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceRepo,
            IConfiguration configuration
            )
        {
            this.logger = logger;
            this.bus = bus;
            this.healthChecker = healthChecker;
            this.healthresultRepo = healthresultRepo;
            this.configuration = configuration;
            this.serviceCheckSummaryRepo = serviceCheckSummaryRepo;
            //this.serviceRepo = serviceRepo;
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
                HealthcheckQueueSubscriberStats.SetIsqueueSubscriberStarted(true);
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

                stringResult = res.FirstOrDefault().Result;
            }
            catch (Exception ex)
            {
                // stringResult = res.FirstOrDefault().Result;
                logger.LogCritical("113 Line Result BsonDocument Deserializion Failed : " + ex);
            }


            var result = new AliveAndWellResult
            {
                Result = document,
                ServiceName = service.Name,
                ServiceNamespace = service.Namespace,
                ServiceUid = service.Uid,
                CreationTime = DateTime.UtcNow,
                Status = res.FirstOrDefault().Status,
                StringResult = stringResult,
                CheckedUrl = res.FirstOrDefault().CheckedUrl
            };

            try
            {
                logger.LogInformation("adding HealthCheck Results to Mongo");
                await healthresultRepo.AddAsync(result);

            }
            catch (Exception ex)
            {
                result.Result = null;
                result.BsonException = ex.Message;
                logger.LogInformation("bson failed.. adding HealthCheck Results to Mongo without result document");
                await healthresultRepo.AddAsync(result);
            }


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
                    itemstatus = res.FirstOrDefault().Status;
                }
            }

            var summary = new ServiceHealthCheckResultSummary();
            summary.Uid = service.Uid;
            summary.Name = service.Name;
            summary.Namespace = service.Namespace;
            summary.HealthIsaliveAndWellSyncDateUTC = DateTime.UtcNow;
            summary.HealthIsaliveAndWell = itemstatus;


            await serviceCheckSummaryRepo.Upsert(summary, p => p.NameandNamespace == service.NameandNamespace);


            logger.LogInformation("HTTP Status : " + res.FirstOrDefault().Status);

            logger.LogCritical("ServiceV1 Sync message " + service.Name);

            var notify = new NotifyServiceHealthCheck
            {
                ID = result.Id.ToString(),
                ServiceName = service.Name,

                ServiceUid = service.Name,
                ServiceNamespace = service.Namespace,
                ServiceApiVersion = service.ServiceApiVersion,
                ServiceResourceVersion = service.ServiceResourceVersion,
                StatusCode = res.FirstOrDefault().Status
            };

            if (!res.FirstOrDefault().IsSuccessStatusCode)
            {
                notify.Message = "Failed HealthCheck " + service.Name;
                notify.Status = NotifyServiceHealthCheckStatus.Warning;
            }
            else
            {
                notify.Message = "Success HealthCheck " + service.Name;
                notify.Status = NotifyServiceHealthCheckStatus.Normal;
            }

            await bus.PublishAsync(notify, configuration["queue:nofity"]).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    logger.LogInformation("Task Added to RabbitMQ Nofity on " + configuration["queue:nofity"] + " " + result.ServiceName);
                }
                if (task.IsFaulted)
                {
                    logger.LogCritical("\n\n");
                    logger.LogCritical("Error on adding to Nofity Queue " + configuration["queue:nofity"] + " " + task.Exception.Message);
                    logger.LogCritical("\n\n");
                }
            });

        }
    }


    public static class HealthcheckQueueSubscriberStats
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
