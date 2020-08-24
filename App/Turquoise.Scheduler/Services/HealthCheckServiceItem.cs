using System;
using Turquoise.Common.Scheduler;

namespace Turquoise.Scheduler.Services
{
    public class HealthCheckScheduledTask<T> : IHealthCheckScheduledTask<T> where T : new()
    {
        public string Name { get; set; }
        public string Schedule { get; set; }
        public string Uid { get; set; }
        public T Item { get; set; }
    }
}