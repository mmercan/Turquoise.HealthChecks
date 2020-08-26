namespace Turquoise.Models.RabbitMQ
{
    public class NotifyServiceHealthCheckError
    {
        public string ID { get; set; }

        public string ServiceName { get; set; }

        public string StatusCode { get; set; }
    }
}