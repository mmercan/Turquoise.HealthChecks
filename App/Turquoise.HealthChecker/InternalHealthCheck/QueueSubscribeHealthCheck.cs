using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Turquoise.HealthChecker.InternalHealthCheck
{
    public class QueueSubscribeHealthCheck : IHealthCheck
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
}
