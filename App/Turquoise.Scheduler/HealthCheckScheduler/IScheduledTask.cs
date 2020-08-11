using System.Threading;
using System.Threading.Tasks;

namespace Turquoise.Scheduler.HealthCheckScheduler
{
    public interface IHealthCheckScheduledTask
    {
        string Schedule { get; }
        string Name { get; set; }
        // Task ExecuteAsync(CancellationToken cancellationToken);
    }
}