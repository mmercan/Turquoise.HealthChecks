using System.Collections.Generic;
using Turquoise.Scheduler.Services;

namespace Turquoise.Scheduler.Helpers
{
    public static class testCronData
    {
        public static List<HealthCheckServiceItem> GetData()
        {
            List<HealthCheckServiceItem> data = new List<HealthCheckServiceItem>();

            data.Add(new HealthCheckServiceItem { Name = "run every hour        1", Schedule = "0 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every 2 minutes   2", Schedule = "*/2 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every 5 minutes   3", Schedule = "*/5 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every 15 minutes  4", Schedule = "*/15 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every 15 minutes  5", Schedule = "*/15 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every 5 minutes   6", Schedule = "*/5 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every 5 minutes   7", Schedule = "*/5 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every hour        8", Schedule = "0 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every 15 minutes  9", Schedule = "*/15 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every hour        10", Schedule = "0 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every 5 minutes  11", Schedule = "*/5 * * * *" });
            data.Add(new HealthCheckServiceItem { Name = "run every  minute    12", Schedule = "* * * * *" });

            return data;
        }
    }

}