using System;
using Turquoise.Common.Scheduler;

namespace Turquoise.Scheduler.Services
{
    public class HealthCheckScheduledTask : IHealthCheckScheduledTask
    {
        public string Name { get; set; }
        public string Schedule { get; set; }
        public Guid Uid { get; set; }
    }
}