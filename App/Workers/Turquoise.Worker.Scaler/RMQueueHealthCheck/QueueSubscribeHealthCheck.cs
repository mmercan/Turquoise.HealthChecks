using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Turquoise.Worker.Scaler.RMQueueHealthCheck
{
    public class QueueSubscribeHealthCheck: IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var isStarted = HealthcheckQueueSubscriberStats.GetIsqueueSubscriberStarted();

            var lastProcess = HealthcheckQueueSubscriberStats.GetLastProcessTime();
            var timeAgo = DateTime.UtcNow.Subtract(lastProcess);

            var data = new Dictionary<string, object> {
            { "Last process", lastProcess },
            { "Time ago", timeAgo }
        } as IReadOnlyDictionary<string, object>;

            if (isStarted)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("Processing as much as we can", data));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy("Processing is stuck somewhere", null, data));
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