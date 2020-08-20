using System;
using System.Threading;
using System.Threading.Tasks;

namespace Turquoise.Common.Scheduler
{
    public interface IHealthCheckScheduledTask
    {
        string Schedule { get; }
        string Name { get; set; }
        Guid Uid { get; set; }


        // Task ExecuteAsync(CancellationToken cancellationToken);
    }
}