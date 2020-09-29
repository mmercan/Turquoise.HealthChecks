namespace Turquoise.Models.Mongo
{
    public class VirtualServiceV1
    {
        public string Host { get; set; }
        public string Service { get; set; }
        public string Port { get; set; }

        public string Name { get; set; }
        public string Namespace { get; set; }
    }
}