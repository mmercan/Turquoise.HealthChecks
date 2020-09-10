namespace Turquoise.Models.RabbitMQ
{
    public class NotifyServiceHealthCheck
    {


        public string ID { get; set; }

        public string ServiceName { get; set; }
        public string ServiceUid { get; set; }
        public string ServiceNamespace { get; set; }
        public string ServiceApiVersion { get; set; }
        public string ServiceResourceVersion { get; set; }

        public string StatusCode { get; set; }
    }

    public enum NotifyServiceHealthCheckStatus
    {

    }
}