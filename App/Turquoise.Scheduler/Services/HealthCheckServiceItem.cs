using System;
using Turquoise.Scheduler.HealthCheckScheduler;

namespace Turquoise.Scheduler.Services
{
    public class HealthCheckServiceItem : IHealthCheckScheduledTask
    {
        public string Name { get; set; }
        public string Schedule { get; set; }
        public Guid Uid { get; set; }
    }
}