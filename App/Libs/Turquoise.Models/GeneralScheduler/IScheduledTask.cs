using System;
using System.Threading;
using System.Threading.Tasks;
using Turquoise.Models.RabbitMQ;

namespace Turquoise.Models.Scheduler
{
    public interface IScheduledTask<T> where T : new()
    {
        string Schedule { get; }
        string Name { get; set; }
        string Namespace { get; set; }
        string Uid { get; set; }
        T Item { get; set; }

        IScaleDetails ScaleDetails { get; set; }

        // Task ExecuteAsync(CancellationToken cancellationToken);
    }

    public interface IScaleDetails
    {
        int? ReplicaNumber { get; set; }
        ScaleUpDown ScaleUpDown { get; set; }

    }
}