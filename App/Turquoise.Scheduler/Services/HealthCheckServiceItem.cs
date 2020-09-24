using System;
using Turquoise.Common.Scheduler;

namespace Turquoise.Scheduler.Services
{
    public class ScheduledTask<T> : IScheduledTask<T> where T : new()
    {
        public string Name { get; set; }
        public string Schedule { get; set; }
        public string Uid { get; set; }
        public T Item { get; set; }

        public int? ReplicaNumber { get; set; }
    }
}