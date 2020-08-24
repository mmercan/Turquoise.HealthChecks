using System;
using Turquoise.Common.Scheduler;
using Turquoise.Common.Scheduler.Cron;

namespace Turquoise.Common.Scheduler
{
    public class HealthCheckSchedulerTaskWrapper<T> where T : new()
    {
        public string Uid { get; set; }
        public CrontabSchedule Schedule { get; set; }
        public IHealthCheckScheduledTask<T> Task { get; set; }

        public DateTime LastRunTime { get; set; }
        public DateTime NextRunTime { get; set; }

        public T Item { get; set; }

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