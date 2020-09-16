using System;
using System.Collections.Generic;

namespace Turquoise.Models.Mongo
{
    public class AliveAndWellResultStat
    {
        public DateTime syncDate { get; set; }
        public long AllServices { get; set; }
        public long AllRunsOnToday { get; set; }
        public long HealthyRunsOnToday { get; set; }

        public long UnhealthyRunsOnToday { get; set; }

        public List<string> UnhealthyServicesToday { get; set; }
    }
}