using System.Collections.Generic;

namespace Turquoise.Models.Mongo
{
    public class ContainerV1
    {
        public string LivenessProbe { get; set; }
        public string ReadinessProbe { get; set; }
        public string Image { get; set; }
    }

    public class ProbeV1
    {
        public string Exec { get; set; }
        public int FailureThreshold { get; set; }


        public string HttpGetHost { get; set; }
        public List<string> HttpGetHttpHeaders { get; set; }
        public string HttpGetPath { get; set; }
        public string HttpGetPort { get; set; }
        public string HttpGetScheme { get; set; }
    }


}