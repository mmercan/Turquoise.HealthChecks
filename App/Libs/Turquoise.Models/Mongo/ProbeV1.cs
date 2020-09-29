using System.Collections.Generic;

namespace Turquoise.Models.Mongo
{
    public class ProbeV1
    {
        public IList<string> Exec { get; set; }
        public int FailureThreshold { get; set; }


        public string HttpGetHost { get; set; }
        public List<HttpHeaderV1> HttpGetHttpHeaders { get; set; }
        public string HttpGetPath { get; set; }
        public string HttpGetPort { get; set; }
        public string HttpGetScheme { get; set; }

        public int? InitialDelaySeconds { get; set; }
    }

    public class HttpHeaderV1
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}