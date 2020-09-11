using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Turquoise.Common.Mail;
using Turquoise.Common.Mongo;
using Turquoise.Comms.Models;
using Turquoise.K8s.Services;
using Turquoise.Models.Mongo;
using Turquoise.Models.RabbitMQ;

namespace Turquoise.Comms.BackgroundServices
{
    public class NotifyServiceHealthCheckQueueSubscriber : BackgroundService
    {
        IBus bus;
        private IConfiguration configuration;
        private MailService mailService;
        private IFeatureManager featureManager;
        private K8sService k8sService;
        private ManualResetEventSlim _ResetEvent = new ManualResetEventSlim(false);
        private readonly ILogger<NotifyServiceHealthCheckQueueSubscriber> logger;

        private Task executingTask;


        public NotifyServiceHealthCheckQueueSubscriber(
            ILogger<NotifyServiceHealthCheckQueueSubscriber> logger,
            IBus bus,
            IConfiguration configuration,
            MailService mailService,
            IFeatureManager featureManager,
            K8sService k8sService

            )
        {
            this.logger = logger;
            this.bus = bus;
            this.configuration = configuration;
            this.mailService = mailService;
            this.featureManager = featureManager;
            this.k8sService = k8sService;
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
                bus.SubscribeAsync<Turquoise.Models.RabbitMQ.NotifyServiceHealthCheck>(configuration["queue:nofity"], Handler); //, x => x.WithTopic("product.*"));
                Console.WriteLine("Listening on topic " + configuration["queue:nofity"]);
                _ResetEvent.Wait();
            }
            catch (Exception ex)
            {
                logger.LogError("Exception: " + ex.Message);
            }
        }

        private async Task Handler(Turquoise.Models.RabbitMQ.NotifyServiceHealthCheck notify)
        {

            if (await featureManager.IsEnabledAsync(nameof(CommsFeatureFlags.SendEmail)))
            {
                if (notify.Status == NotifyServiceHealthCheckStatus.Warning)
                {
                    logger.LogCritical("Sending email " + notify.ServiceName + " Code :" + notify.StatusCode);
                    string body = notify.ServiceName + " " + notify.StatusCode;
                    mailService.Send("test@test.com", "failure on HealthCheck", body);
                    logger.LogCritical("Email sent " + notify.ServiceName + " Code :" + notify.StatusCode);
                }
            }



            if (await featureManager.IsEnabledAsync(nameof(CommsFeatureFlags.AddEvent)))
            {

                logger.LogCritical("Adding event " + notify.ServiceName + " Code :" + notify.StatusCode);
                // string body = notify.ServiceName + " " + notify.StatusCode;
                var namespaceParam = notify.ServiceNamespace;
                var serviceName = notify.ServiceName;
                var serviceUid = notify.ServiceUid;
                var serviceNamespace = notify.ServiceNamespace;
                var serviceApiVersion = notify.ServiceApiVersion;
                var serviceResourceVersion = notify.ServiceResourceVersion;
                var message = notify.Message;

                if (notify.Status == NotifyServiceHealthCheckStatus.Warning)
                {
                    await this.k8sService.EventClient.CountUpOrCreateEvent(
                         namespaceParam, serviceName, serviceUid,
                        serviceNamespace, serviceApiVersion, serviceResourceVersion,
                                  message);
                }
                else if (await featureManager.IsEnabledAsync(nameof(CommsFeatureFlags.AddEventonSuccess)))
                {
                    await this.k8sService.EventClient.CountUpOrCreateEvent(
                     namespaceParam, serviceName, serviceUid,
                             serviceNamespace, serviceApiVersion,
                              serviceResourceVersion,
                              message, type: "Normal");
                }

                logger.LogCritical("Event Added " + notify.ServiceName + " Code :" + notify.StatusCode);
            }


        }
    }

}