using System;
using System.Threading;
using System.Threading.Tasks;

namespace Turquoise.Common.Scheduler
{
    public interface IScheduledTask<T> where T : new()
    {
        string Schedule { get; }
        string Name { get; set; }
        string Uid { get; set; }
        T Item { get; set; }
        int? ReplicaNumber { get; set; }

        // Task ExecuteAsync(CancellationToken cancellationToken);
    }
}