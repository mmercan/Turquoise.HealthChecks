using System;
using Turquoise.Scheduler.HealthCheckScheduler;
using Turquoise.Scheduler.HealthCheckScheduler.Cron;

namespace Turquoise.Scheduler.Services
{
    public class HealthCheckSchedulerTaskWrapper
    {
        public Guid Uid { get; set; }
        public CrontabSchedule Schedule { get; set; }
        public IHealthCheckScheduledTask Task { get; set; }

        public DateTime LastRunTime { get; set; }
        public DateTime NextRunTime { get; set; }

        public void Increment()
        {
            LastRunTime = NextRunTime;
            NextRunTime = Schedule.GetNextOccurrence(NextRunTime);
        }

        public bool ShouldRun(DateTime currentTime)
        {
            return NextRunTime < currentTime && LastRunTime != NextRunTime;
        }
    }
}