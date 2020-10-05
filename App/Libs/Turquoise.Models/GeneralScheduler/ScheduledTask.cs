using System;
using System.Threading;
using System.Threading.Tasks;
using Turquoise.Models.RabbitMQ;

namespace Turquoise.Models.Scheduler
{
    public class ScheduledTask<T> : IScheduledTask<T> where T : new()
    {
        public string Name { get; set; }

        public string Namespace { get; set; }
        public string Schedule { get; set; }
        public string Uid { get; set; }
        public T Item { get; set; }

        public IScaleDetails ScaleDetails { get; set; }
    }

    public class ScaleDetails : IScaleDetails
    {
        public int? ReplicaNumber { get; set; }

        public ScaleUpDown ScaleUpDown { get; set; }

        public string Timezone { get; set; }
    }
}