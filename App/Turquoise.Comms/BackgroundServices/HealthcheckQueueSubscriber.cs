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
using Turquoise.Common.Mail;
using Turquoise.Common.Mongo;
using Turquoise.Models.Mongo;
using Turquoise.Models.RabbitMQ;

namespace Turquoise.Comms.BackgroundServices
{
    public class NotifyServiceHealthCheckQueueSubscriber : BackgroundService
    {
        IBus bus;
        private IConfiguration configuration;
        private MailService mailService;
        private ManualResetEventSlim _ResetEvent = new ManualResetEventSlim(false);
        private readonly ILogger<NotifyServiceHealthCheckQueueSubscriber> logger;

        private Task executingTask;


        public NotifyServiceHealthCheckQueueSubscriber(
            ILogger<NotifyServiceHealthCheckQueueSubscriber> logger,
            IBus bus,
            IConfiguration configuration,
            MailService mailService
            )
        {
            this.logger = logger;
            this.bus = bus;
            this.configuration = configuration;
            this.mailService = mailService;
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
                bus.Subscribe<Turquoise.Models.RabbitMQ.NotifyServiceHealthCheckError>(configuration["queue:nofity"], Handler); //, x => x.WithTopic("product.*"));
                Console.WriteLine("Listening on topic " + configuration["queue:nofity"]);
                _ResetEvent.Wait();
            }
            catch (Exception ex)
            {
                logger.LogError("Exception: " + ex.Message);
            }
        }

        private void Handler(Turquoise.Models.RabbitMQ.NotifyServiceHealthCheckError notify)
        {
            logger.LogCritical("TODO: first step  send email " + notify.ServiceName + " Code :" + notify.StatusCode);
            string body = notify.ServiceName + " " + notify.StatusCode;
            mailService.Send("test@test.com", "failure on HealthCheck", body);
        }
    }
}
