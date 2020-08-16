using System;
using System.Collections.Generic;

namespace Turquoise.Models.Mongo
{
    public class NamespaceV1
    {
        public string Name { get; set; }
        public List<Label> Labels { get; set; }
        public DateTime CreationTime { get; set; }
        public string Status { get; set; }

        public string Uid { get; set; }

        public int DeploymentCount { get; set; }
        public int ServiceCount { get; set; }
        public int ErrorCount { get; set; }
        public int WarningCount { get; set; }
    }

}