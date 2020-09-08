using System.Collections.Generic;

namespace Turquoise.Models.Mongo
{
    public class AliveAndWellResultStat
    {
        public int AllRunsOnToday { get; set; }
        public int HealthyRunsOnToday { get; set; }

        public int UnhealthyRunsOnToday { get; set; }

        public List<string> UnhealthyServicesToday { get; set; }
    }
}